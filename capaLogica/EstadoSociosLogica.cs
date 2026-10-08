using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public sealed class EstadoSocioDashboard
    {
        public int IdSocio { get; set; }
        public int IdMembresia { get; set; }
        public string Socio { get; set; }
        public string DNI { get; set; }
        public string Plan { get; set; }
        public string EstadoMembresia { get; set; }
        public string EstadoPago { get; set; }
        public int CuotasVencidas { get; set; }
        public int CuotasPendientes { get; set; }
        public DateTime? FechaAlta { get; set; }
        public decimal DeudaTotal { get; set; }
        public DateTime? UltimoPago { get; set; }
        public DateTime? ProximoVencimiento { get; set; }
        public bool Activo { get; set; }
    }

    public sealed class ResumenEstadoSocios
    {
        public List<EstadoSocioDashboard> Socios { get; set; }
        public int SociosActivos { get; set; }
        public int SociosInactivos { get; set; }
        public int AlDia { get; set; }
        public int UnaVencida { get; set; }
        public int DosOMasVencidas { get; set; }
        public int CuotasPendientes { get; set; }
        public int VencenHoy { get; set; }
        public int VencenEnSieteDias { get; set; }
    }

    /// <summary>Proyecta el estado real de socios y cuotas usando el umbral de deuda de MembresiaLogica.</summary>
    public sealed class EstadoSociosLogica
    {
        public ResumenEstadoSocios ObtenerResumen()
        {
            return ObtenerResumen(true);
        }

        /// <summary>Reutiliza la clasificación actual sin persistir bajas automáticas durante el análisis.</summary>
        public ResumenEstadoSocios ObtenerResumenSoloLectura()
        {
            return ObtenerResumen(false);
        }

        public EstadoSocioDashboard ObtenerSocioSoloLectura(int idSocio)
        {
            return ObtenerResumen(false, idSocio).Socios.SingleOrDefault();
        }

        public static string ClasificarDeuda(int cuotasVencidas)
        {
            if (MembresiaLogica.DebeDarseDeBajaPorDeuda(cuotasVencidas)) return "Límite alcanzado";
            return cuotasVencidas > 0 ? "Con deuda" : "Al día";
        }

        private ResumenEstadoSocios ObtenerResumen(bool actualizarEstados, int? idSocio = null)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                if (actualizarEstados)
                {
                    using (var transaccion = datos.IniciarTransaccion())
                    {
                        MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                        datos.GuardarCambios();
                        transaccion.Confirmar();
                    }
                }
                var consulta = datos.Socios.ConsultarSoloLectura("Membresias.Plan", "Membresias.Cuotas.Pago");
                if (idSocio.HasValue) consulta = consulta.Where(s => s.IdSocio == idSocio.Value);
                var socios = consulta.OrderBy(s => s.Apellido).ThenBy(s => s.Nombre).ToList();
                var resultado = new List<EstadoSocioDashboard>();
                foreach (var socio in socios)
                {
                    var membresia = socio.Membresias.OrderByDescending(m => m.Estado)
                        .ThenByDescending(m => m.FechaInicio).FirstOrDefault();
                    var cuotas = membresia == null ? new List<CuotaMembresia>() : membresia.Cuotas.ToList();
                    var pendientes = cuotas.Where(c => c.EstadoPago == EstadosCuota.Pendiente).ToList();
                    var vencidas = pendientes.Where(c => c.FechaHasta.Date < DateTime.Today).ToList();
                    var moroso = MembresiaLogica.DebeDarseDeBajaPorDeuda(vencidas.Count);
                    var activo = socio.Estado && (membresia == null || membresia.Estado) && !moroso;
                    var estadoPago = ClasificarEstadoPago(socio.Estado, membresia != null && membresia.Estado,
                        vencidas.Count, pendientes.Count, membresia != null);
                    var pagos = cuotas.Where(c => c.EstadoPago == EstadosCuota.Pagada && c.Pago != null &&
                        c.Pago.Estado == EstadosTransaccionPago.Aprobado).Select(c => c.Pago.Fecha).ToList();
                    var proximo = pendientes.Where(c => c.FechaHasta.Date >= DateTime.Today)
                        .OrderBy(c => c.FechaHasta).Select(c => (DateTime?)c.FechaHasta.Date).FirstOrDefault();
                    resultado.Add(new EstadoSocioDashboard
                    {
                        IdSocio = socio.IdSocio,
                        IdMembresia = membresia == null ? 0 : membresia.IdMembresia,
                        Socio = (socio.Apellido + ", " + socio.Nombre).Trim(' ', ','),
                        DNI = socio.DNI,
                        Plan = membresia == null || membresia.Plan == null ? "Sin membresía" : membresia.Plan.Nombre,
                        EstadoMembresia = membresia == null ? "Sin membresía" : activo ? "Activa" : "Inactiva",
                        EstadoPago = estadoPago,
                        CuotasVencidas = vencidas.Count,
                        CuotasPendientes = pendientes.Count,
                        FechaAlta = socio.FechaAlta,
                        DeudaTotal = pendientes.Sum(c => Math.Max(0m, c.Importe - (c.Pago != null && c.Pago.Estado == EstadosTransaccionPago.Aprobado ? c.Pago.Importe : 0m))),
                        UltimoPago = pagos.Count == 0 ? (DateTime?)null : pagos.Max(),
                        ProximoVencimiento = proximo,
                        Activo = activo
                    });
                }

                var todasLasCuotas = socios.SelectMany(s => s.Membresias)
                    .SelectMany(m => m.Cuotas).Where(c => c.EstadoPago == EstadosCuota.Pendiente).ToList();
                var hoy = DateTime.Today;
                return new ResumenEstadoSocios
                {
                    Socios = resultado,
                    SociosActivos = resultado.Count(s => s.Activo),
                    SociosInactivos = resultado.Count(s => !s.Activo),
                    AlDia = resultado.Count(s => s.EstadoPago == "AL DÍA"),
                    UnaVencida = resultado.Count(s => s.CuotasVencidas == 1),
                    DosOMasVencidas = resultado.Count(s => s.CuotasVencidas >= 2),
                    CuotasPendientes = todasLasCuotas.Count,
                    VencenHoy = todasLasCuotas.Count(c => c.FechaHasta.Date == hoy),
                    VencenEnSieteDias = todasLasCuotas.Count(c => c.FechaHasta.Date > hoy && c.FechaHasta.Date <= hoy.AddDays(7))
                };
            }
        }

        public static string ClasificarEstadoPago(bool socioActivo, bool membresiaActiva, int cuotasVencidas,
            int cuotasPendientes, bool tieneMembresia)
        {
            if (MembresiaLogica.DebeDarseDeBajaPorDeuda(cuotasVencidas)) return "MOROSO";
            if (!socioActivo || (tieneMembresia && !membresiaActiva)) return "INACTIVO";
            if (!tieneMembresia) return "SIN MEMBRESÍA";
            if (cuotasVencidas == 1) return "1 CUOTA VENCIDA";
            return cuotasPendientes == 0 ? "AL DÍA" : "PAGO PENDIENTE";
        }
    }
}
