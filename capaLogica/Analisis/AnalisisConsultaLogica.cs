using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica.Analisis
{
    public sealed class DatoImporte
    {
        public string Nombre { get; set; }
        public decimal Importe { get; set; }
    }

    public sealed class EjercicioSinUso
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public sealed class IndicadorComparacion
    {
        public string Nombre { get; set; }
        public decimal ValorA { get; set; }
        public decimal ValorB { get; set; }
        public decimal? VariacionPorcentual { get; set; }
        public bool EsMoneda { get; set; }
    }

    public sealed class OpcionSocioAnalisis
    {
        public int IdSocio { get; set; }
        public string NombreCompleto { get; set; }
    }

    public sealed class AnalisisSocio
    {
        public EstadoSocioDashboard Estado { get; set; }
        public string Entrenador { get; set; }
        public string Rutina { get; set; }
        public decimal TotalAbonado { get; set; }
        public int CantidadPagos { get; set; }
        public DateTime? UltimoPago { get; set; }
        public int? AntiguedadMeses { get; set; }
        public string EstadoDeuda { get; set; }
        public List<DatoMensual> PagosPorMes { get; set; }
        public bool TieneHistoriaParaGraficar { get; set; }
    }

    /// <summary>Comparación de movimientos y consulta individual de solo lectura, sin reconstruir estados históricos.</summary>
    public sealed class AnalisisConsultaLogica
    {
        public List<IndicadorComparacion> Comparar(DateTime desdeA, DateTime hastaA, DateTime desdeB, DateTime hastaB)
        {
            ValidarPeriodo(desdeA, hastaA);
            ValidarPeriodo(desdeB, hastaB);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var a = ObtenerIndicadores(datos, desdeA.Date, hastaA.Date.AddDays(1));
                var b = ObtenerIndicadores(datos, desdeB.Date, hastaB.Date.AddDays(1));
                return new List<IndicadorComparacion>
                {
                    CrearIndicador("Ingresos", a[0], b[0], true),
                    CrearIndicador("Pagos", a[1], b[1], false),
                    CrearIndicador("Altas", a[2], b[2], false)
                };
            }
        }

        private static IndicadorComparacion CrearIndicador(string nombre, decimal a, decimal b, bool moneda)
        {
            return new IndicadorComparacion { Nombre = nombre, ValorA = a, ValorB = b, EsMoneda = moneda,
                VariacionPorcentual = AnalisisLogica.CalcularVariacion(b, a) };
        }

        private static decimal[] ObtenerIndicadores(IUnidadDeTrabajo datos, DateTime desde, DateTime fin)
        {
            var pagos = datos.Pagos.ConsultarSoloLectura().Where(p => p.Estado == EstadosTransaccionPago.Aprobado && p.Fecha >= desde && p.Fecha < fin);
            return new[] { pagos.Select(p => (decimal?)p.Importe).Sum() ?? 0m, pagos.Count(),
                datos.Socios.ConsultarSoloLectura().Count(s => s.FechaAlta.HasValue && s.FechaAlta >= desde && s.FechaAlta < fin) };
        }

        private static void ValidarPeriodo(DateTime desde, DateTime hasta)
        {
            if (desde.Date > hasta.Date || hasta.Date == DateTime.MaxValue.Date)
                throw new ArgumentException("Cada período debe tener Desde anterior o igual a Hasta y una fecha final válida.");
        }

        public List<OpcionSocioAnalisis> BuscarSocios(string texto)
        {
            texto = (texto ?? string.Empty).Trim();
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Socios.ConsultarSoloLectura()
                    .Where(s => texto == "" || s.DNI.Contains(texto) || (s.Nombre + " " + s.Apellido).Contains(texto) ||
                        (s.Apellido + " " + s.Nombre).Contains(texto))
                    .OrderBy(s => s.Apellido).ThenBy(s => s.Nombre).Take(100)
                    .Select(s => new OpcionSocioAnalisis { IdSocio = s.IdSocio,
                        NombreCompleto = s.Apellido + ", " + s.Nombre + " — " + s.DNI }).ToList();
        }

        public AnalisisSocio ObtenerSocio(int idSocio)
        {
            var estado = new EstadoSociosLogica().ObtenerSocioSoloLectura(idSocio);
            if (estado == null) return null;
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var pagos = datos.Pagos.ConsultarSoloLectura().Where(p => p.Estado == EstadosTransaccionPago.Aprobado &&
                    p.Cuotas.Any(c => c.Membresia.IdSocio == idSocio));
                var agrupados = pagos.GroupBy(p => new { p.Fecha.Year, p.Fecha.Month })
                    .Select(g => new { g.Key.Year, g.Key.Month, Total = g.Sum(p => p.Importe) }).ToList()
                    .ToDictionary(x => new DateTime(x.Year, x.Month, 1), x => x.Total);
                var resultado = new AnalisisSocio
                {
                    Estado = estado,
                    CantidadPagos = pagos.Count(),
                    TotalAbonado = agrupados.Values.Sum(),
                    UltimoPago = pagos.Select(p => (DateTime?)p.Fecha).Max(),
                    EstadoDeuda = estado.EstadoDeuda,
                    Entrenador = "Sin entrenador activo",
                    Rutina = "Sin rutina activa",
                    TieneHistoriaParaGraficar = agrupados.Count >= 2,
                    PagosPorMes = agrupados.Count == 0 ? new List<DatoMensual>() :
                        AnalisisLogica.CompletarMeses(agrupados.Keys.Min(), agrupados.Keys.Max(), agrupados)
                };
                if (estado.Activo && estado.IdMembresia > 0)
                {
                    var entrenadores = datos.MembresiasEntrenadores.ConsultarSoloLectura()
                        .Where(a => a.IdMembresia == estado.IdMembresia && a.Estado && a.Entrenador.Estado && a.Entrenador.Rol.Descripcion == "Entrenador")
                        .Select(a => a.Entrenador.Apellido + ", " + a.Entrenador.Nombre).Distinct().OrderBy(x => x).ToList();
                    if (entrenadores.Count > 0) resultado.Entrenador = string.Join("; ", entrenadores);
                    resultado.Rutina = datos.Membresias.ConsultarSoloLectura()
                        .Where(m => m.IdMembresia == estado.IdMembresia && m.IdRutina.HasValue && m.Rutina.Estado)
                        .Select(m => m.Rutina.Nombre).SingleOrDefault() ?? "Sin rutina activa";
                }
                if (estado.FechaAlta.HasValue)
                {
                    var fecha = estado.FechaAlta.Value;
                    resultado.AntiguedadMeses = Math.Max(0, (DateTime.Today.Year - fecha.Year) * 12 + DateTime.Today.Month - fecha.Month -
                        (DateTime.Today.Day < fecha.Day ? 1 : 0));
                }
                return resultado;
            }
        }
    }
}
