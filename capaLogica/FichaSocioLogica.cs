using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;
using exxen2._0.capaLogica.Auditoria;
using exxen2._0.capaLogica.Reportes;

namespace exxen2._0.capaLogica
{
    public sealed class FichaSocioResultado
    {
        public Socio DatosSocio { get; set; }
        public EstadoSocioDashboard Estado { get; set; }
        public DateTime? InicioMembresia { get; set; }
        public DateTime? CubiertaHasta { get; set; }
        public int LimiteDeuda { get; set; }
        public string Entrenador { get; set; }
        public string EstadoAsignacion { get; set; }
        public string Rutina { get; set; }
        public int CantidadEjercicios { get; set; }
        public bool TieneRutina { get; set; }
        public int IdRutina { get; set; }
        public string Antiguedad { get; set; }
        public decimal TotalAbonado { get; set; }
        public int CantidadPagos { get; set; }
        public int CuotasPagadas { get; set; }
        public DateTime? UltimoPago { get; set; }
        public List<ReportesPagosServicio.PagoExportable> Pagos { get; set; }
    }

    /// <summary>Consulta sin cambios de estado; comparte clasificación y proyección del historial PDF.</summary>
    public sealed class FichaSocioLogica
    {
        private readonly int idUsuario;
        public FichaSocioLogica(int idUsuarioAutenticado) { idUsuario = idUsuarioAutenticado; }
        public void ValidarAcceso() { new AuditoriaLogica(idUsuario).ValidarOperador(); }

        public FichaSocioResultado Obtener(int idSocio)
        {
            ValidarAcceso();
            if (idSocio <= 0) throw new ArgumentException("Seleccioná un socio válido.");
            var estado = new EstadoSociosLogica().ObtenerSocioSoloLectura(idSocio);
            if (estado == null) throw new InvalidOperationException("El socio no existe.");
            var historial = new ReportesPagosServicio(idUsuario).ConsultarHistorial(idSocio);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var socio = datos.Socios.ConsultarSoloLectura().Single(s => s.IdSocio == idSocio);
                var membresia = estado.IdMembresia == 0 ? null : datos.Membresias.ConsultarSoloLectura(
                    "Entrenadores.Entrenador", "Rutina.Ejercicios", "Cuotas").Single(m => m.IdMembresia == estado.IdMembresia);
                var asignacion = membresia == null ? null : membresia.Entrenadores
                    .Where(a => a.Estado).OrderByDescending(a => a.IdMembresiaEntrenador).FirstOrDefault();
                var entrenador = asignacion == null ? null : asignacion.Entrenador;
                var rutina = membresia == null ? null : membresia.Rutina;
                return new FichaSocioResultado
                {
                    DatosSocio = socio, Estado = estado,
                    InicioMembresia = membresia == null ? (DateTime?)null : membresia.FechaInicio,
                    CubiertaHasta = membresia == null ? (DateTime?)null : CuotaMembresiaLogica.CubiertaHasta(membresia.Cuotas),
                    LimiteDeuda = ConfiguracionSistemaLogica.ObtenerEnContexto(datos).MaxCuotasVencidasPermitidas,
                    Entrenador = entrenador == null ? "Sin asignar" : entrenador.Nombre + " " + entrenador.Apellido,
                    EstadoAsignacion = asignacion == null ? "Sin asignación activa" : entrenador != null && entrenador.Estado ? "Activa" : "Entrenador inactivo",
                    Rutina = rutina == null ? "Sin rutina" : rutina.Nombre + (rutina.Estado ? "" : " (inactiva)"),
                    TieneRutina = rutina != null, IdRutina = rutina == null ? 0 : rutina.IdRutina,
                    CantidadEjercicios = rutina == null ? 0 : rutina.Ejercicios.Count(e => e.Estado),
                    Antiguedad = Antiguedad(socio.FechaAlta),
                    Pagos = historial.Pagos, TotalAbonado = historial.Pagos.Sum(p => p.Importe), CantidadPagos = historial.Pagos.Count,
                    CuotasPagadas = datos.CuotasMembresia.ConsultarSoloLectura().Count(c => c.Membresia.IdSocio == idSocio && c.EstadoPago == EstadosCuota.Pagada),
                    UltimoPago = historial.Pagos.Count == 0 ? (DateTime?)null : historial.Pagos[0].Fecha
                };
            }
        }

        private static string Antiguedad(DateTime? alta)
        {
            if (!alta.HasValue) return "Sin información";
            var dias = Math.Max(0, (DateTime.Today - alta.Value.Date).Days);
            return dias + (dias == 1 ? " día" : " días");
        }
    }
}
