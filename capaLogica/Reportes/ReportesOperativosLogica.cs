using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;
using exxen2._0.capaLogica.Auditoria;

namespace exxen2._0.capaLogica.Reportes
{
    public enum TipoReporteOperativo { DeudaPendiente, LimiteDeuda, SinEntrenador, SinRutina, Vencimientos, MembresiasInactivas }
    public sealed class ReporteOperativoFila
    {
        public int IdSocio { get; set; }
        public string[] Valores { get; set; }
    }
    public sealed class ResultadoReporteOperativo
    {
        public TipoReporteOperativo Tipo { get; set; }
        public string Titulo { get; set; }
        public string[] Columnas { get; set; }
        public List<ReporteOperativoFila> Filas { get; set; }
        public string Filtros { get; set; }
        public string Resumen { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaConsulta { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
    }
    public sealed class IndicadoresOperativos
    {
        public int Socios { get; set; }
        public int Usuarios { get; set; }
        public int Membresias { get; set; }
        public int Rutinas { get; set; }
        public int Ejercicios { get; set; }
    }

    public sealed class ReportesOperativosLogica
    {
        private static readonly CultureInfo Argentina = CultureInfo.GetCultureInfo("es-AR");
        private readonly AuditoriaLogica auditoria;
        public ReportesOperativosLogica(int idUsuarioAutenticado) { auditoria = new AuditoriaLogica(idUsuarioAutenticado); }
        public void ValidarAcceso() { auditoria.ValidarAcceso(); }
        public static string[] Nombres()
        {
            return new[] { "Deuda pendiente", "Socios en límite de deuda", "Socios sin entrenador", "Socios sin rutina", "Próximos vencimientos", "Membresías inactivas" };
        }
        public IndicadoresOperativos ObtenerIndicadores()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                auditoria.ObtenerUsuario(datos, true);
                return new IndicadoresOperativos { Socios = datos.Socios.ConsultarSoloLectura().Count(s => s.Estado),
                    Usuarios = datos.UsuariosSistema.ConsultarSoloLectura().Count(u => u.Estado && u.Rol.Estado),
                    Membresias = datos.Membresias.ConsultarSoloLectura().Count(m => m.Estado),
                    Rutinas = datos.Rutinas.ConsultarSoloLectura().Count(r => r.Estado),
                    Ejercicios = datos.Ejercicios.ConsultarSoloLectura().Count(e => e.Estado) };
            }
        }
        public ResultadoReporteOperativo Consultar(TipoReporteOperativo tipo, string busqueda, DateTime? desde = null, DateTime? hasta = null)
        {
            if (!Enum.IsDefined(typeof(TipoReporteOperativo), tipo)) throw new ArgumentException("Seleccioná un tipo de reporte válido.");
            if (tipo == TipoReporteOperativo.Vencimientos && desde.HasValue && hasta.HasValue && desde.Value.Date > hasta.Value.Date)
                throw new ArgumentException("Desde debe ser anterior o igual a Hasta.");
            var texto = (busqueda ?? string.Empty).Trim();
            if (texto.Length > 100) throw new ArgumentException("La búsqueda admite hasta 100 caracteres.");
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                auditoria.ObtenerUsuario(datos, true);
                var configuracion = ConfiguracionSistemaLogica.ObtenerEnContexto(datos);
                var resultado = new ResultadoReporteOperativo { Tipo = tipo, Titulo = Nombres()[(int)tipo],
                    Filas = new List<ReporteOperativoFila>(), FechaConsulta = DateTime.Now,
                    Filtros = "Socio/DNI: " + (texto.Length == 0 ? "Todos" : texto) };
                if (tipo == TipoReporteOperativo.Vencimientos)
                {
                    var inicio = DateTime.Today;
                    var fin = inicio.AddDays(configuracion.DiasAvisoVencimiento);
                    if (desde.HasValue && desde.Value.Date > inicio) inicio = desde.Value.Date;
                    if (hasta.HasValue && hasta.Value.Date < fin) fin = hasta.Value.Date;
                    resultado.Desde = inicio; resultado.Hasta = fin;
                    resultado.Filtros += "; vencimientos: " + Fecha(inicio) + " - " + Fecha(fin) + "; aviso: " + configuracion.DiasAvisoVencimiento + " días";
                    resultado.Columnas = new[] { "Socio", "DNI", "Período", "Fecha vencimiento", "Importe", "Estado" };
                    var limiteExclusivo = fin.AddDays(1);
                    var cuotas = datos.CuotasMembresia.ConsultarSoloLectura().Where(c => c.EstadoPago == EstadosCuota.Pendiente && c.FechaHasta >= inicio && c.FechaHasta < limiteExclusivo);
                    if (texto.Length > 0) cuotas = cuotas.Where(c => (c.Membresia.Socio.Nombre + " " + c.Membresia.Socio.Apellido).Contains(texto) || (c.Membresia.Socio.Apellido + ", " + c.Membresia.Socio.Nombre).Contains(texto) || c.Membresia.Socio.DNI.Contains(texto));
                    var filas = cuotas.OrderBy(c => c.FechaHasta).ThenBy(c => c.Membresia.Socio.Apellido).ThenBy(c => c.IdCuotaMembresia)
                        .Select(c => new { c.Membresia.IdSocio, c.Membresia.Socio.Apellido, c.Membresia.Socio.Nombre, c.Membresia.Socio.DNI, c.FechaDesde, c.FechaHasta, c.Importe, c.EstadoPago }).ToList();
                    resultado.Total = filas.Sum(c => c.Importe);
                    foreach (var c in filas) Agregar(resultado, c.IdSocio, c.Apellido + ", " + c.Nombre, c.DNI, Fecha(c.FechaDesde) + " - " + Fecha(c.FechaHasta), Fecha(c.FechaHasta), Moneda(c.Importe), c.EstadoPago);
                }
                else if (tipo == TipoReporteOperativo.MembresiasInactivas)
                {
                    resultado.Columnas = new[] { "Socio", "DNI", "Plan", "Fecha inicio", "Estado", "Cuotas vencidas", "Deuda" };
                    var consulta = datos.Membresias.ConsultarSoloLectura("Socio", "Plan", "Cuotas.Pago").Where(m => !m.Estado);
                    if (texto.Length > 0) consulta = consulta.Where(m => (m.Socio.Nombre + " " + m.Socio.Apellido).Contains(texto) || (m.Socio.Apellido + ", " + m.Socio.Nombre).Contains(texto) || m.Socio.DNI.Contains(texto));
                    foreach (var m in consulta.OrderBy(m => m.Socio.Apellido).ThenBy(m => m.Socio.Nombre).ThenByDescending(m => m.FechaInicio).ToList())
                    {
                        var estado = EstadoSociosLogica.ProyectarEstado(m.Socio, m, configuracion);
                        Agregar(resultado, m.IdSocio, estado.Socio, estado.DNI, estado.Plan, Fecha(m.FechaInicio), "Inactiva", estado.CuotasVencidas.ToString(), Moneda(estado.DeudaTotal));
                    }
                }
                else
                {
                    var deuda = tipo == TipoReporteOperativo.DeudaPendiente || tipo == TipoReporteOperativo.LimiteDeuda;
                    var estados = new EstadoSociosLogica().ConsultarParaReportes(texto, deuda, !deuda, configuracion);
                    if (deuda)
                    {
                        resultado.Filtros += "; límite: " + configuracion.MaxCuotasVencidasPermitidas + " cuotas vencidas";
                        resultado.Columnas = tipo == TipoReporteOperativo.DeudaPendiente ? new[] { "Socio", "DNI", "Plan", "Cuotas vencidas", "Deuda total", "Estado" }
                            : new[] { "Socio", "DNI", "Vencidas", "Límite", "Deuda", "Estado membresía" };
                        foreach (var s in estados.Where(s => s.CuotasVencidas > 0 && (tipo != TipoReporteOperativo.LimiteDeuda || s.LimiteAlcanzado)))
                        {
                            resultado.Total += s.DeudaTotal;
                            if (tipo == TipoReporteOperativo.DeudaPendiente) Agregar(resultado, s.IdSocio, s.Socio, s.DNI, s.Plan, s.CuotasVencidas.ToString(), Moneda(s.DeudaTotal), s.EstadoDeuda);
                            else Agregar(resultado, s.IdSocio, s.Socio, s.DNI, s.CuotasVencidas.ToString(), configuracion.MaxCuotasVencidasPermitidas.ToString(), Moneda(s.DeudaTotal), s.EstadoMembresia);
                        }
                    }
                    else
                    {
                        var activos = estados.Where(s => s.Activo).ToList();
                        var idsMembresias = activos.Where(s => s.IdMembresia > 0).Select(s => s.IdMembresia).ToList();
                        // Mismo criterio de entrenador válido de Análisis; las asignaciones históricas no cuentan.
                        var asignaciones = datos.MembresiasEntrenadores.ConsultarSoloLectura().Where(a => a.Estado && idsMembresias.Contains(a.IdMembresia) && a.Entrenador.Estado && a.Entrenador.Rol.Descripcion == "Entrenador")
                            .Select(a => new { a.IdMembresia, a.IdMembresiaEntrenador, a.Entrenador.Nombre, a.Entrenador.Apellido }).ToList();
                        var porMembresia = asignaciones.GroupBy(a => a.IdMembresia)
                            .ToDictionary(g => g.Key, g => g.OrderByDescending(a => a.IdMembresiaEntrenador).First());
                        if (tipo == TipoReporteOperativo.SinEntrenador)
                        {
                            resultado.Columnas = new[] { "Socio", "DNI", "Plan", "Estado membresía" };
                            foreach (var s in activos.Where(s => !porMembresia.ContainsKey(s.IdMembresia))) Agregar(resultado, s.IdSocio, s.Socio, s.DNI, s.Plan, s.EstadoMembresia);
                        }
                        else
                        {
                            resultado.Columnas = new[] { "Socio", "DNI", "Plan", "Entrenador", "Estado" };
                            var conRutina = new HashSet<int>(datos.Membresias.ConsultarSoloLectura().Where(m => idsMembresias.Contains(m.IdMembresia) && m.IdRutina.HasValue && m.Rutina.Estado).Select(m => m.IdSocio).ToList());
                            foreach (var s in activos.Where(s => !conRutina.Contains(s.IdSocio)))
                            {
                                var a = porMembresia.ContainsKey(s.IdMembresia) ? porMembresia[s.IdMembresia] : null;
                                Agregar(resultado, s.IdSocio, s.Socio, s.DNI, s.Plan, a == null ? "Sin asignar" : a.Nombre + " " + a.Apellido, s.EstadoMembresia);
                            }
                            resultado.Filtros += "; sin rutina activa asignada";
                        }
                        resultado.Filtros += "; socios activos (estado efectivo)";
                    }
                }
                var unidad = tipo == TipoReporteOperativo.Vencimientos ? "cuotas" : tipo == TipoReporteOperativo.MembresiasInactivas ? "membresías" : "socios";
                resultado.Resumen = "Cantidad: " + resultado.Filas.Count + " " + unidad;
                if (tipo == TipoReporteOperativo.DeudaPendiente || tipo == TipoReporteOperativo.LimiteDeuda) resultado.Resumen += " · Deuda total: " + Moneda(resultado.Total);
                if (tipo == TipoReporteOperativo.Vencimientos) resultado.Resumen += " · Importe total pendiente: " + Moneda(resultado.Total);
                return resultado;
            }
        }
        private static void Agregar(ResultadoReporteOperativo r, int idSocio, params string[] valores) { r.Filas.Add(new ReporteOperativoFila { IdSocio = idSocio, Valores = valores }); }
        private static string Fecha(DateTime valor) { return valor.ToString("dd/MM/yyyy", Argentina); }
        private static string Moneda(decimal valor) { return valor.ToString("C2", Argentina); }
    }
}
