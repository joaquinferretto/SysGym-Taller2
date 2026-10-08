using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica.Auditoria
{
    public sealed class FilaAuditoria
    {
        public DateTime FechaHora { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public string Operacion { get; set; }
        public string Entidad { get; set; }
        public string Detalle { get; set; }
    }

    public sealed class OpcionAuditoria
    {
        public string Codigo { get; set; }
        public string Texto { get; set; }
    }

    public sealed class UsuarioFiltroAuditoria
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
    }

    /* Reutiliza el ID autenticado que ya recibe cada formulario del panel; no mantiene otra sesión. */
    public sealed class AuditoriaLogica
    {
        public const int LimiteRegistros = 500;
        public const string RegistrarPago = "REGISTRAR_PAGO";
        public const string CrearUsuario = "CREAR_USUARIO";
        public const string CrearMembresia = "CREAR_MEMBRESIA";
        public const string ReactivarMembresia = "REACTIVAR_MEMBRESIA";
        public const string AsignarEntrenador = "ASIGNAR_ENTRENADOR";
        public const string CambiarEntrenador = "CAMBIAR_ENTRENADOR";
        public const string QuitarEntrenador = "QUITAR_ENTRENADOR";
        public const string ModificarConfiguracion = "MODIFICAR_CONFIGURACION";
        public const string GenerarReporteAnalisis = "GENERAR_REPORTE_ANALISIS";
        public const string ExportarComprobantePago = "EXPORTAR_COMPROBANTE_PAGO";
        public const string ExportarHistorialPagos = "EXPORTAR_HISTORIAL_PAGOS";
        private readonly int idUsuarioAutenticado;

        public AuditoriaLogica(int idUsuarioAutenticado)
        {
            this.idUsuarioAutenticado = idUsuarioAutenticado;
        }

        public static List<OpcionAuditoria> Operaciones()
        {
            return new List<OpcionAuditoria>
            {
                new OpcionAuditoria { Codigo = RegistrarPago, Texto = "Registró pago" },
                new OpcionAuditoria { Codigo = CrearUsuario, Texto = "Creó usuario" },
                new OpcionAuditoria { Codigo = CrearMembresia, Texto = "Creó membresía" },
                new OpcionAuditoria { Codigo = ReactivarMembresia, Texto = "Reactivó membresía" },
                new OpcionAuditoria { Codigo = AsignarEntrenador, Texto = "Asignó entrenador" },
                new OpcionAuditoria { Codigo = CambiarEntrenador, Texto = "Cambió entrenador" },
                new OpcionAuditoria { Codigo = QuitarEntrenador, Texto = "Quitó entrenador" },
                new OpcionAuditoria { Codigo = ModificarConfiguracion, Texto = "Modificó configuración" },
                new OpcionAuditoria { Codigo = GenerarReporteAnalisis, Texto = "Generó reporte de análisis" },
                new OpcionAuditoria { Codigo = ExportarComprobantePago, Texto = "Exportó comprobante de pago" },
                new OpcionAuditoria { Codigo = ExportarHistorialPagos, Texto = "Exportó historial de pagos" }
            };
        }

        /* Autoriza contra el usuario/rol actual de SQL, no contra una selección o etiqueta visual. */
        internal UsuarioSistema ObtenerUsuario(IUnidadDeTrabajo datos, bool soloAdministrador = false)
        {
            if (idUsuarioAutenticado <= 0)
                throw new InvalidOperationException("No hay un usuario autenticado para esta operación.");
            var usuario = datos.UsuariosSistema.ConsultarSoloLectura("Rol")
                .SingleOrDefault(u => u.IdUsuarioSistema == idUsuarioAutenticado);
            if (usuario == null || !usuario.Estado || usuario.Rol == null || !usuario.Rol.Estado)
                throw new InvalidOperationException("El usuario autenticado o su rol no está activo.");
            var admin = ValidacionesGimnasio.EsAdministradorActivo(usuario);
            var recepcion = ValidacionesGimnasio.EsRecepcionistaActivo(usuario);
            if (!admin && (soloAdministrador || !recepcion))
                throw new UnauthorizedAccessException(soloAdministrador
                    ? "Esta operación requiere un Administrador activo."
                    : "La operación requiere Administrador o Recepcionista.");
            return usuario;
        }

        public void ValidarAcceso()
        {
            using (var datos = new UnidadDeTrabajoGimnasio()) ObtenerUsuario(datos, true);
        }

        internal void ValidarOperador()
        {
            using (var datos = new UnidadDeTrabajoGimnasio()) ObtenerUsuario(datos);
        }

        /* Agrega el historial a la misma unidad de trabajo. El caso de uso guarda y confirma ambos juntos. */
        internal void RegistrarOperacion(IUnidadDeTrabajo datos, string operacion, string entidad, int idEntidad, string detalle)
        {
            if (!Operaciones().Any(o => o.Codigo == operacion))
                throw new ArgumentException("La operación no pertenece a esta etapa de auditoría.", "operacion");
            if (idEntidad <= 0 || string.IsNullOrWhiteSpace(entidad) || entidad.Length > 50 ||
                string.IsNullOrWhiteSpace(detalle) || detalle.Length > 1000)
                throw new ArgumentException("Los datos de la auditoría no son válidos.");
            var usuario = ObtenerUsuario(datos, operacion == CrearUsuario || operacion == ModificarConfiguracion || operacion == GenerarReporteAnalisis);
            datos.AuditoriasOperaciones.Agregar(new AuditoriaOperacion
            {
                FechaHora = DateTime.Now, IdUsuario = usuario.IdUsuarioSistema,
                UsuarioNombre = usuario.Nombre + " " + usuario.Apellido, Rol = usuario.Rol.Descripcion,
                Operacion = operacion, Entidad = entidad, IdEntidad = idEntidad, Detalle = detalle
            });
        }

        public List<UsuarioFiltroAuditoria> ListarUsuarios()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                ObtenerUsuario(datos, true);
                return datos.UsuariosSistema.ConsultarSoloLectura().OrderBy(u => u.Apellido).ThenBy(u => u.Nombre)
                    .Select(u => new UsuarioFiltroAuditoria { IdUsuario = u.IdUsuarioSistema,
                        Nombre = u.Nombre + " " + u.Apellido + " (" + u.NombreUsuario + ")" }).ToList();
            }
        }

        public List<FilaAuditoria> Consultar(DateTime? desde, DateTime? hasta, int? idUsuario, string operacion, string busqueda)
        {
            if (desde.HasValue && hasta.HasValue && desde.Value.Date > hasta.Value.Date)
                throw new ArgumentException("Desde debe ser anterior o igual a Hasta.");
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                ObtenerUsuario(datos, true);
                var consulta = datos.AuditoriasOperaciones.ConsultarSoloLectura();
                if (desde.HasValue) { var inicio = desde.Value.Date; consulta = consulta.Where(a => a.FechaHora >= inicio); }
                if (hasta.HasValue) { var fin = hasta.Value.Date.AddDays(1); consulta = consulta.Where(a => a.FechaHora < fin); }
                if (idUsuario.HasValue) { var id = idUsuario.Value; consulta = consulta.Where(a => a.IdUsuario == id); }
                if (!string.IsNullOrWhiteSpace(operacion)) consulta = consulta.Where(a => a.Operacion == operacion);
                var texto = (busqueda ?? string.Empty).Trim();
                if (texto.Length > 0) consulta = consulta.Where(a => a.Detalle.Contains(texto) || a.UsuarioNombre.Contains(texto) || a.Entidad.Contains(texto));
                var filas = consulta.OrderByDescending(a => a.FechaHora).ThenByDescending(a => a.IdAuditoria)
                    .Take(LimiteRegistros).Select(a => new FilaAuditoria { FechaHora = a.FechaHora,
                        Usuario = a.UsuarioNombre, Rol = a.Rol, Operacion = a.Operacion, Entidad = a.Entidad, Detalle = a.Detalle }).ToList();
                var nombres = Operaciones().ToDictionary(o => o.Codigo, o => o.Texto);
                foreach (var fila in filas) fila.Operacion = nombres.ContainsKey(fila.Operacion) ? nombres[fila.Operacion] : fila.Operacion;
                return filas;
            }
        }
    }
}
