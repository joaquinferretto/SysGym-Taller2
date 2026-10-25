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
        public bool LimiteAlcanzado { get; set; }
        public string EstadoDeuda { get; set; }
        public int CuotasPendientes { get; set; }
        public int CuotasPorVencer { get; set; }
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
        public int SociosInactivosTotales { get; set; }
        public int AlDia { get; set; }
        public int ConDeuda { get; set; }
        public int LimiteAlcanzado { get; set; }
        public int CuotasPendientes { get; set; }
        public int VencenHoy { get; set; }
        public int CuotasVencidas { get; set; }
        public int VencenEnPlazoAviso { get; set; }
        public int DiasAvisoVencimiento { get; set; }
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

        internal List<EstadoSocioDashboard> ConsultarParaReportes(string busqueda, bool conVencidas, bool activosRegistrados, ConfiguracionSistema configuracion)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var configuracionAplicada = configuracion ?? ConfiguracionSistemaLogica.ObtenerEnContexto(datos);
                var texto = (busqueda ?? string.Empty).Trim();
                return conVencidas
                    ? ConsultarParaReportesConDeuda(datos, texto, activosRegistrados, configuracionAplicada)
                    : ConsultarParaReportesSinDeuda(datos, texto, activosRegistrados, configuracionAplicada);
            }
        }

        /* Proyecta el estado mínimo para reportes sin deuda; solo consulta un conteo SQL de vencidas. */
        private static List<EstadoSocioDashboard> ConsultarParaReportesSinDeuda(IUnidadDeTrabajo datos, string texto, bool activosRegistrados, ConfiguracionSistema configuracion)
        {
            var sociosConsulta = datos.Socios.ConsultarSoloLectura();
            if (activosRegistrados) sociosConsulta = sociosConsulta.Where(s => s.Estado);
            if (texto.Length > 0)
                sociosConsulta = sociosConsulta.Where(s => (s.Nombre + " " + s.Apellido).Contains(texto) ||
                    (s.Apellido + ", " + s.Nombre).Contains(texto) || s.DNI.Contains(texto));

            var socios = sociosConsulta.OrderBy(s => s.Apellido).ThenBy(s => s.Nombre)
                .Select(s => new { s.IdSocio, s.Nombre, s.Apellido, s.DNI, s.Estado }).ToList();
            var idsSocios = socios.Select(s => s.IdSocio).ToList();
            var hoy = DateTime.Today;
            var membresias = datos.Membresias.ConsultarSoloLectura().Where(m => idsSocios.Contains(m.IdSocio))
                .Select(m => new MembresiaReporteResumen
                {
                    IdMembresia = m.IdMembresia,
                    IdSocio = m.IdSocio,
                    Estado = m.Estado,
                    FechaInicio = m.FechaInicio,
                    Plan = m.Plan.Nombre,
                    CuotasVencidas = m.Cuotas.Count(c => c.EstadoPago == EstadosCuota.Pendiente && c.FechaHasta < hoy)
                }).ToList();
            var porSocio = membresias.GroupBy(m => m.IdSocio)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(m => m.Estado).ThenByDescending(m => m.FechaInicio).First());
            var limite = configuracion.MaxCuotasVencidasPermitidas;

            return socios.Select(s =>
            {
                MembresiaReporteResumen membresia;
                porSocio.TryGetValue(s.IdSocio, out membresia);
                var tieneMembresia = membresia != null;
                var vencidas = tieneMembresia ? membresia.CuotasVencidas : 0;
                var moroso = tieneMembresia && MembresiaLogica.DebeDarseDeBajaPorDeuda(vencidas, limite);
                var activo = s.Estado && (!tieneMembresia || membresia.Estado) && !moroso;
                return new EstadoSocioDashboard
                {
                    IdSocio = s.IdSocio,
                    IdMembresia = tieneMembresia ? membresia.IdMembresia : 0,
                    Socio = (s.Apellido + ", " + s.Nombre).Trim(' ', ','),
                    DNI = s.DNI,
                    Plan = !tieneMembresia || string.IsNullOrEmpty(membresia.Plan) ? "Sin membresía" : membresia.Plan,
                    EstadoMembresia = !tieneMembresia ? "Sin membresía" : activo ? "Activa" : "Inactiva",
                    CuotasVencidas = vencidas,
                    LimiteAlcanzado = moroso,
                    EstadoDeuda = ClasificarDeuda(vencidas, limite),
                    Activo = activo
                };
            }).ToList();
        }

        /* Para reportes de deuda, resume todas las membresías y carga cuotas/pagos solo de la seleccionada. */
        private static List<EstadoSocioDashboard> ConsultarParaReportesConDeuda(IUnidadDeTrabajo datos, string texto, bool activosRegistrados, ConfiguracionSistema configuracion)
        {
            var membresiasConsulta = datos.Membresias.ConsultarSoloLectura();
            if (activosRegistrados) membresiasConsulta = membresiasConsulta.Where(m => m.Socio.Estado);
            if (texto.Length > 0)
                membresiasConsulta = membresiasConsulta.Where(m => (m.Socio.Nombre + " " + m.Socio.Apellido).Contains(texto) ||
                    (m.Socio.Apellido + ", " + m.Socio.Nombre).Contains(texto) || m.Socio.DNI.Contains(texto));

            var hoy = DateTime.Today;
            var resumenes = membresiasConsulta.Select(m => new MembresiaReporteResumen
            {
                IdMembresia = m.IdMembresia,
                IdSocio = m.IdSocio,
                Estado = m.Estado,
                FechaInicio = m.FechaInicio,
                NombreSocio = m.Socio.Nombre,
                ApellidoSocio = m.Socio.Apellido,
                DNI = m.Socio.DNI,
                EstadoSocio = m.Socio.Estado,
                Plan = m.Plan.Nombre,
                CuotasVencidas = m.Cuotas.Count(c => c.EstadoPago == EstadosCuota.Pendiente && c.FechaHasta < hoy),
                DeudaTotal = m.Cuotas.Where(c => c.EstadoPago == EstadosCuota.Pendiente)
                    .Select(c => (decimal?)(c.Importe > (c.Pago != null && c.Pago.Estado == EstadosTransaccionPago.Aprobado ? c.Pago.Importe : 0m)
                        ? c.Importe - (c.Pago != null && c.Pago.Estado == EstadosTransaccionPago.Aprobado ? c.Pago.Importe : 0m)
                        : 0m)).Sum() ?? 0m
            }).ToList();
            var seleccionadas = resumenes.GroupBy(m => m.IdSocio)
                .Select(g => new
                {
                    TodasTienenDeuda = g.Any(m => m.CuotasVencidas > 0),
                    Actual = g.OrderByDescending(m => m.Estado).ThenByDescending(m => m.FechaInicio).First()
                })
                .Where(g => g.TodasTienenDeuda && g.Actual.CuotasVencidas > 0)
                .Select(g => g.Actual)
                .OrderBy(m => m.ApellidoSocio)
                .ThenBy(m => m.NombreSocio)
                .ToList();
            var limite = configuracion.MaxCuotasVencidasPermitidas;
            return seleccionadas.Select(m =>
                {
                    var moroso = MembresiaLogica.DebeDarseDeBajaPorDeuda(m.CuotasVencidas, limite);
                    var activo = m.EstadoSocio && m.Estado && !moroso;
                    return new EstadoSocioDashboard
                {
                        IdSocio = m.IdSocio,
                        IdMembresia = m.IdMembresia,
                        Socio = (m.ApellidoSocio + ", " + m.NombreSocio).Trim(' ', ','),
                        DNI = m.DNI,
                        Plan = string.IsNullOrEmpty(m.Plan) ? "Sin membresía" : m.Plan,
                        EstadoMembresia = activo ? "Activa" : "Inactiva",
                        CuotasVencidas = m.CuotasVencidas,
                        LimiteAlcanzado = moroso,
                        EstadoDeuda = ClasificarDeuda(m.CuotasVencidas, limite),
                        DeudaTotal = m.DeudaTotal,
                        Activo = activo
                    };
                }).ToList();
        }

        private sealed class MembresiaReporteResumen
        {
            public int IdMembresia { get; set; }
            public int IdSocio { get; set; }
            public bool Estado { get; set; }
            public DateTime FechaInicio { get; set; }
            public string NombreSocio { get; set; }
            public string ApellidoSocio { get; set; }
            public string DNI { get; set; }
            public bool EstadoSocio { get; set; }
            public string Plan { get; set; }
            public int CuotasVencidas { get; set; }
            public decimal DeudaTotal { get; set; }
        }

        public static string ClasificarDeuda(int cuotasVencidas)
        {
            return ClasificarDeuda(cuotasVencidas, new ConfiguracionSistemaLogica().Obtener().MaxCuotasVencidasPermitidas);
        }

        public static string ClasificarDeuda(int cuotasVencidas, int limite)
        {
            if (MembresiaLogica.DebeDarseDeBajaPorDeuda(cuotasVencidas, limite)) return "Límite alcanzado";
            return cuotasVencidas > 0 ? "Con deuda" : "Al día";
        }

        private ResumenEstadoSocios ObtenerResumen(bool actualizarEstados, int? idSocio = null, string busqueda = null, bool conVencidas = false, bool activosRegistrados = false, ConfiguracionSistema configuracionAplicada = null)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var configuracion = configuracionAplicada ?? ConfiguracionSistemaLogica.ObtenerEnContexto(datos);
                if (actualizarEstados)
                {
                    using (var transaccion = datos.IniciarTransaccion())
                    {
                        MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                        datos.GuardarCambios();
                        transaccion.Confirmar();
                    }
                }
                var consulta = datos.Socios.ConsultarSoloLectura();
                if (idSocio.HasValue) consulta = consulta.Where(s => s.IdSocio == idSocio.Value);
                var texto = (busqueda ?? string.Empty).Trim();
                if (texto.Length > 0) consulta = consulta.Where(s => (s.Nombre + " " + s.Apellido).Contains(texto) || (s.Apellido + ", " + s.Nombre).Contains(texto) || s.DNI.Contains(texto));
                if (activosRegistrados) consulta = consulta.Where(s => s.Estado);
                if (conVencidas)
                {
                    var hoyFiltro = DateTime.Today;
                    consulta = consulta.Where(s => s.Membresias.Any(m => m.Cuotas.Any(c => c.EstadoPago == EstadosCuota.Pendiente && c.FechaHasta < hoyFiltro)));
                }
                var socios = consulta.OrderBy(s => s.Apellido).ThenBy(s => s.Nombre)
                    .Select(s => new SocioEstadoResumen
                    {
                        IdSocio = s.IdSocio,
                        Nombre = s.Nombre,
                        Apellido = s.Apellido,
                        DNI = s.DNI,
                        FechaAlta = s.FechaAlta,
                        Activo = s.Estado
                    }).ToList();
                var idsSocios = socios.Select(s => s.IdSocio).ToList();
                var membresias = idsSocios.Count == 0
                    ? new List<MembresiaEstadoResumen>()
                    : datos.Membresias.ConsultarSoloLectura("Plan")
                        .Where(m => idsSocios.Contains(m.IdSocio))
                        .Select(m => new MembresiaEstadoResumen
                        {
                            IdMembresia = m.IdMembresia,
                            IdSocio = m.IdSocio,
                            Estado = m.Estado,
                            FechaInicio = m.FechaInicio,
                            NombrePlan = m.Plan.Nombre
                        }).ToList();
                var idsMembresias = membresias.Select(m => m.IdMembresia).ToList();
                var cuotas = idsMembresias.Count == 0
                    ? new List<CuotaEstadoResumen>()
                    : datos.CuotasMembresia.ConsultarSoloLectura("Pago")
                        .Where(c => idsMembresias.Contains(c.IdMembresia))
                        .Select(c => new CuotaEstadoResumen
                        {
                            IdCuotaMembresia = c.IdCuotaMembresia,
                            IdMembresia = c.IdMembresia,
                            FechaHasta = c.FechaHasta,
                            Importe = c.Importe,
                            EstadoPago = c.EstadoPago,
                            IdRegistroPago = c.Pago == null ? (int?)null : c.Pago.IdRegistroPago,
                            EstadoTransaccion = c.Pago == null ? null : c.Pago.Estado,
                            ImportePago = c.Pago == null ? (decimal?)null : c.Pago.Importe,
                            FechaPago = c.Pago == null ? (DateTime?)null : c.Pago.Fecha
                        }).ToList();
                var cuotasPorMembresia = cuotas.GroupBy(c => c.IdMembresia)
                    .ToDictionary(g => g.Key, g => g.Select(c => new CuotaMembresia
                    {
                        IdCuotaMembresia = c.IdCuotaMembresia,
                        IdMembresia = c.IdMembresia,
                        FechaHasta = c.FechaHasta,
                        Importe = c.Importe,
                        EstadoPago = c.EstadoPago,
                        IdRegistroPago = c.IdRegistroPago,
                        Pago = c.IdRegistroPago.HasValue
                            ? new Pago
                            {
                                IdRegistroPago = c.IdRegistroPago.Value,
                                Estado = c.EstadoTransaccion,
                                Importe = c.ImportePago ?? 0m,
                                Fecha = c.FechaPago ?? DateTime.MinValue
                            }
                            : null
                    }).ToList());
                var membresiasPorSocio = membresias.GroupBy(m => m.IdSocio)
                    .ToDictionary(g => g.Key, g => g.OrderByDescending(m => m.Estado).ThenByDescending(m => m.FechaInicio).FirstOrDefault());
                var resultado = new List<EstadoSocioDashboard>();
                foreach (var socio in socios)
                {
                    MembresiaEstadoResumen membresiaResumen;
                    membresiasPorSocio.TryGetValue(socio.IdSocio, out membresiaResumen);
                    var socioEntidad = new Socio
                    {
                        IdSocio = socio.IdSocio,
                        Nombre = socio.Nombre,
                        Apellido = socio.Apellido,
                        DNI = socio.DNI,
                        FechaAlta = socio.FechaAlta,
                        Estado = socio.Activo
                    };
                    Membresia membresia = null;
                    if (membresiaResumen != null)
                    {
                        List<CuotaMembresia> cuotasMembresia;
                        cuotasPorMembresia.TryGetValue(membresiaResumen.IdMembresia, out cuotasMembresia);
                        membresia = new Membresia
                        {
                            IdMembresia = membresiaResumen.IdMembresia,
                            IdSocio = membresiaResumen.IdSocio,
                            Estado = membresiaResumen.Estado,
                            FechaInicio = membresiaResumen.FechaInicio,
                            Plan = new Plan { Nombre = membresiaResumen.NombrePlan },
                            Cuotas = cuotasMembresia ?? new List<CuotaMembresia>()
                        };
                    }
                    resultado.Add(ProyectarEstado(socioEntidad, membresia, configuracion));
                }

                var hoy = DateTime.Today;
                return new ResumenEstadoSocios
                {
                    Socios = resultado,
                    SociosActivos = resultado.Count(s => s.Activo),
                    SociosInactivos = resultado.Count(s => !s.Activo && !s.LimiteAlcanzado),
                    SociosInactivosTotales = resultado.Count(s => !s.Activo),
                    AlDia = resultado.Count(s => s.Activo && s.CuotasVencidas == 0),
                    ConDeuda = resultado.Count(s => s.Activo && s.CuotasVencidas > 0 && !s.LimiteAlcanzado),
                    LimiteAlcanzado = resultado.Count(s => s.LimiteAlcanzado),
                    CuotasPendientes = cuotas.Count(c => c.EstadoPago == EstadosCuota.Pendiente),
                    VencenHoy = cuotas.Count(c => c.EstadoPago == EstadosCuota.Pendiente && c.FechaHasta.Date == hoy),
                    CuotasVencidas = cuotas.Count(c => c.EstadoPago == EstadosCuota.Pendiente && c.FechaHasta.Date < hoy),
                    DiasAvisoVencimiento = configuracion.DiasAvisoVencimiento,
                    VencenEnPlazoAviso = cuotas.Count(c => c.EstadoPago == EstadosCuota.Pendiente &&
                        c.FechaHasta.Date >= hoy && c.FechaHasta.Date <= hoy.AddDays(configuracion.DiasAvisoVencimiento))
                };
            }
        }

        private sealed class SocioEstadoResumen
        {
            public int IdSocio { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string DNI { get; set; }
            public DateTime? FechaAlta { get; set; }
            public bool Activo { get; set; }
        }

        private sealed class MembresiaEstadoResumen
        {
            public int IdMembresia { get; set; }
            public int IdSocio { get; set; }
            public bool Estado { get; set; }
            public DateTime FechaInicio { get; set; }
            public string NombrePlan { get; set; }
        }

        private sealed class CuotaEstadoResumen
        {
            public int IdCuotaMembresia { get; set; }
            public int IdMembresia { get; set; }
            public DateTime FechaHasta { get; set; }
            public decimal Importe { get; set; }
            public string EstadoPago { get; set; }
            public int? IdRegistroPago { get; set; }
            public string EstadoTransaccion { get; set; }
            public decimal? ImportePago { get; set; }
            public DateTime? FechaPago { get; set; }
        }

        // Misma proyección para membresías específicas en reportes, sin una segunda fórmula de deuda.
        internal static EstadoSocioDashboard ProyectarEstado(Socio socio, Membresia membresia, ConfiguracionSistema configuracion)
        {
            var cuotas = membresia == null ? new List<CuotaMembresia>() : membresia.Cuotas.ToList();
            var pendientes = cuotas.Where(c => c.EstadoPago == EstadosCuota.Pendiente).ToList();
            var vencidas = pendientes.Where(c => c.FechaHasta.Date < DateTime.Today).ToList();
            var moroso = MembresiaLogica.DebeDarseDeBajaPorDeuda(vencidas.Count, configuracion.MaxCuotasVencidasPermitidas);
            var activo = socio.Estado && (membresia == null || membresia.Estado) && !moroso;
            var estadoPago = ClasificarEstadoPago(socio.Estado, membresia != null && membresia.Estado,
                vencidas.Count, pendientes.Count, membresia != null, configuracion.MaxCuotasVencidasPermitidas);
            var pagos = cuotas.Where(c => c.EstadoPago == EstadosCuota.Pagada && c.Pago != null &&
                c.Pago.Estado == EstadosTransaccionPago.Aprobado).Select(c => c.Pago.Fecha).ToList();
            var proximo = pendientes.Where(c => c.FechaHasta.Date >= DateTime.Today)
                .OrderBy(c => c.FechaHasta).Select(c => (DateTime?)c.FechaHasta.Date).FirstOrDefault();
            return new EstadoSocioDashboard
            {
                IdSocio = socio.IdSocio,
                IdMembresia = membresia == null ? 0 : membresia.IdMembresia,
                Socio = (socio.Apellido + ", " + socio.Nombre).Trim(' ', ','),
                DNI = socio.DNI,
                Plan = membresia == null || membresia.Plan == null ? "Sin membresía" : membresia.Plan.Nombre,
                EstadoMembresia = membresia == null ? "Sin membresía" : activo ? "Activa" : "Inactiva",
                EstadoPago = estadoPago,
                CuotasVencidas = vencidas.Count,
                LimiteAlcanzado = moroso,
                EstadoDeuda = ClasificarDeuda(vencidas.Count, configuracion.MaxCuotasVencidasPermitidas),
                CuotasPendientes = pendientes.Count,
                CuotasPorVencer = pendientes.Count(c => c.FechaHasta.Date >= DateTime.Today &&
                    c.FechaHasta.Date <= DateTime.Today.AddDays(configuracion.DiasAvisoVencimiento)),
                FechaAlta = socio.FechaAlta,
                DeudaTotal = pendientes.Sum(c => Math.Max(0m, c.Importe - (c.Pago != null && c.Pago.Estado == EstadosTransaccionPago.Aprobado ? c.Pago.Importe : 0m))),
                UltimoPago = pagos.Count == 0 ? (DateTime?)null : pagos.Max(),
                ProximoVencimiento = proximo,
                Activo = activo
            };
        }

        public static string ClasificarEstadoPago(bool socioActivo, bool membresiaActiva, int cuotasVencidas,
            int cuotasPendientes, bool tieneMembresia, int limite)
        {
            if (MembresiaLogica.DebeDarseDeBajaPorDeuda(cuotasVencidas, limite)) return "Límite alcanzado";
            if (!socioActivo || (tieneMembresia && !membresiaActiva)) return "Inactivo";
            if (!tieneMembresia) return "Sin membresía";
            if (cuotasVencidas > 0) return "Con deuda";
            return "Al día";
        }
    }
}
