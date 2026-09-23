using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Resume la membresia y la rutina vigente para la pantalla Socios y rutinas. */
    public sealed class SocioRutinaItem
    {
        public int IdSocio { get; set; }
        public int IdMembresia { get; set; }
        public int? IdRutina { get; set; }
        public string NombreSocio { get; set; }
        public string DNI { get; set; }
        public string NombrePlan { get; set; }
        public DateTime? CuotaHasta { get; set; }
        public string NombreEntrenador { get; set; }
        public string NombreRutina { get; set; }
        public bool TieneRutina { get { return IdRutina.HasValue; } }  // HasValue: ¿el valor opcional (int?, DateTime?) tiene dato?
    }

    /* Coordina las operaciones del catalogo de rutinas y su asociacion directa a membresias. */
    // Rutinas: catálogo compartido entre entrenadores (todos ven y asignan todas),
    // pero solo el autor o un administrador puede editar. También asigna rutinas a membresías.
    // La usan RutinasEntrenadorFormulario, MisSociosFormulario, ConsultaRutinasAdministradorFormulario y ReportesFormulario.
    public class RutinaLogica
    {
        /* Valida y registra una rutina reutilizable en el catalogo. */
        public Rutina Crear(Rutina rutina)
        {
            ValidarDatos(rutina);
            using (var datos = new UnidadDeTrabajoGimnasio())  // Abre la conexión; al salir del bloque se cierra sola, aunque haya error (try-with-resources).
            {
                ValidarEntrenador(datos, rutina.IdEntrenador);
                rutina.Estado = true;
                if (rutina.FechaCreacion == default(DateTime))
                {
                    rutina.FechaCreacion = DateTime.Now;
                }

                datos.Rutinas.Agregar(rutina);  // Deja el objeto listo para INSERT (se ejecuta en GuardarCambios).
                datos.GuardarCambios();  // EF envía a SQL los INSERT/UPDATE pendientes.
                return rutina;
            }
        }

        /* Crea una rutina, guarda sus ejercicios desde el editor y la vincula a la membresia. */
        public Rutina CrearPersonalizada(Rutina rutina, int idMembresia)
        {
            ValidarDatos(rutina);
            if (idMembresia <= 0)
            {
                throw new InvalidOperationException("La membresia del socio es obligatoria.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.
            }

            new MembresiaLogica().ActualizarEstadoPorDeuda(idMembresia);
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())  // Transacción: si algo falla antes de Confirmar(), se deshace todo.
            {
                ValidarEntrenador(datos, rutina.IdEntrenador);
                var membresia = datos.Membresias.Consultar("Plan", "Socio")  // Consulta con seguimiento: si se modifica el objeto, GuardarCambios hace el UPDATE.
                    .SingleOrDefault(m => m.IdMembresia == idMembresia);  // Devuelve el único que cumple o null.
                ValidarMembresiaParaRutina(membresia);
                ValidarPermisoSobreMembresia(datos, idMembresia, rutina.IdEntrenador);

                rutina.Estado = true;
                if (rutina.FechaCreacion == default(DateTime))
                {
                    rutina.FechaCreacion = DateTime.Now;
                }

                datos.Rutinas.Agregar(rutina);
                datos.GuardarCambios();
                membresia.IdRutina = rutina.IdRutina;
                datos.GuardarCambios();
                transaccion.Confirmar();  // Recién acá quedan grabados todos los cambios de la transacción.
                return rutina;
            }
        }

        /* Vincula una rutina activa del catalogo a la membresia sin copiar ni borrar ejercicios. */
        public void AsignarRutina(int idMembresia, int idRutina)
        {
            if (idMembresia <= 0 || idRutina <= 0)
            {
                throw new InvalidOperationException("La membresia y la rutina son obligatorias.");
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(datos, idMembresia);
                datos.GuardarCambios();
                var membresia = datos.Membresias.Consultar("Plan", "Socio")
                    .SingleOrDefault(m => m.IdMembresia == idMembresia);
                ValidarMembresiaParaRutina(membresia);

                var rutina = datos.Rutinas.Buscar(idRutina);  // Busca por clave primaria; si no existe devuelve null.
                if (rutina == null || !rutina.Estado)
                {
                    throw new InvalidOperationException("La rutina seleccionada no existe o esta inactiva.");
                }

                membresia.IdRutina = idRutina;
                datos.GuardarCambios();
            }
        }

        /* Valida y guarda los cambios de una rutina sin alterar sus membresias asociadas ni su autor. */
        public Rutina Modificar(Rutina rutina, int idEditor)
        {
            ValidarDatos(rutina);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.Rutinas.Buscar(rutina.IdRutina);
                if (existente == null)
                {
                    throw new InvalidOperationException("La rutina no existe.");
                }

                ValidarEntrenador(datos, idEditor);
                ValidarEdicion(datos, existente.IdRutina, idEditor);
                existente.Nombre = rutina.Nombre;
                existente.Descripcion = rutina.Descripcion;
                existente.FechaInicio = rutina.FechaInicio;
                existente.FechaFin = rutina.FechaFin;
                existente.Estado = rutina.Estado;
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca una rutina e incluye sus ejercicios y membresias para el editor y el catalogo. */
        public Rutina ObtenerPorId(int idRutina)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Ejercicios.Ejercicio", "Membresias")  // Solo lectura: EF no vigila cambios (más liviano para listar).
                    .SingleOrDefault(r => r.IdRutina == idRutina);
            }
        }

        /* Consulta todas las rutinas para gestionarlas sin perder las bajas logicas. */
        public List<Rutina> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Ejercicios.Ejercicio", "Membresias")
                    .OrderByDescending(r => r.Estado)  // Ordena (ORDER BY).
                    .ThenByDescending(r => r.FechaCreacion)
                    .ToList();  // Acá se ejecuta la consulta en SQL y se trae la lista.
            }
        }

        /* Busca la membresia activa del socio para asignar o crear su rutina. */
        public Membresia ObtenerMembresiaActivaParaSocio(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                return datos.Membresias.ConsultarSoloLectura("Socio", "Plan", "Rutina")
                    .Where(m => m.IdSocio == idSocio && m.Estado && m.Socio.Estado && m.Plan.Estado)  // Where = filtro (como filter de Streams / WHERE de SQL). "x => ..." es una lambda.
                    .OrderByDescending(m => m.FechaInicio)
                    .FirstOrDefault();
            }
        }

        /* Devuelve la rutina directa de la membresia activa del socio, si existe. */
        public Rutina ObtenerRutinaActivaPorSocio(int idSocio)
        {
            var membresia = ObtenerMembresiaActivaParaSocio(idSocio);
            return membresia == null ? null : membresia.Rutina;
        }

        /* Consulta el catalogo activo que puede vincularse a cualquier membresia habilitada. */
        public List<Rutina> ListarActivas()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Membresias")
                    .Where(r => r.Estado)
                    .OrderBy(r => r.Nombre)
                    .ToList();
            }
        }

        /* Consulta las rutinas creadas por el entrenador indicado. */
        public List<Rutina> ListarPorEntrenador(int idEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Ejercicios.Ejercicio", "Membresias")
                    .Where(r => r.IdEntrenador == idEntrenador && r.Estado)
                    .OrderByDescending(r => r.FechaCreacion)
                    .ToList();
            }
        }

        /* Catalogo compartido para un entrenador: todas las rutinas activas de cualquier autor mas sus propias bajas, que puede reactivar. */
        public List<Rutina> ListarParaEntrenador(int idEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Ejercicios.Ejercicio", "Membresias")
                    .Where(r => r.Estado || r.IdEntrenador == idEntrenador)
                    .OrderByDescending(r => r.Estado)
                    .ThenByDescending(r => r.FechaCreacion)
                    .ToList();
            }
        }

        /* Indica si el usuario puede modificar la rutina: el catalogo se comparte para ver y asignar, pero solo su autor o un administrador la editan. */
        public bool PuedeEditar(int idRutina, int idUsuario)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return PuedeEditarEnContexto(datos, idRutina, idUsuario);
            }
        }

        /* Exige que quien modifica la rutina o sus ejercicios sea su autor o un administrador activo. */
        internal static void ValidarEdicion(IUnidadDeTrabajo datos, int idRutina, int idEditor)
        {
            if (!PuedeEditarEnContexto(datos, idRutina, idEditor))
            {
                throw new InvalidOperationException("Solo el entrenador que creó la rutina o un administrador pueden modificarla.");
            }
        }

        private static bool PuedeEditarEnContexto(IUnidadDeTrabajo datos, int idRutina, int idUsuario)
        {
            var rutina = datos.Rutinas.ConsultarSoloLectura().SingleOrDefault(r => r.IdRutina == idRutina);
            if (rutina == null)
            {
                return false;
            }

            if (rutina.IdEntrenador == idUsuario)  // Regla: el autor de la rutina puede editarla...
            {
                return true;
            }

            var usuario = datos.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.IdUsuarioSistema == idUsuario);
            return ValidacionesGimnasio.EsAdministradorActivo(usuario);  // ...y si no es el autor, solo un administrador.
        }

        /* Consulta las rutinas del entrenador incluyendo bajas para la gestion del catalogo. */
        public List<Rutina> ListarPorEntrenadorParaGestion(int idEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Ejercicios.Ejercicio", "Membresias")
                    .Where(r => r.IdEntrenador == idEntrenador)
                    .OrderByDescending(r => r.Estado)
                    .ThenByDescending(r => r.FechaCreacion)
                    .ToList();
            }
        }

        /* Lista socios con membresia activa y la rutina directa vigente para la pantalla master/detail. */
        public List<SocioRutinaItem> ListarSociosPorEntrenador(int idEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                var membresias = datos.Membresias.ConsultarSoloLectura("Socio", "Plan", "Rutina", "Cuotas", "Entrenadores.Entrenador")
                    .Where(m => m.Estado && m.Socio.Estado && m.Plan.Estado &&
                        (idEntrenador <= 0 || m.Entrenadores.Any(e => e.Estado && e.IdEntrenador == idEntrenador)))  // ¿Existe al menos uno? (no trae filas).
                    .OrderBy(m => m.Socio.Apellido)
                    .ThenBy(m => m.Socio.Nombre)
                    .ToList();

                return membresias.Select(CrearItemSocioRutina).ToList();  // Select = transforma cada elemento (como map de Streams).
            }
        }

        /* Lista todos los socios con membresia activa para la vista administrativa. */
        public List<SocioRutinaItem> ListarSociosParaAdministracion()
        {
            return ListarSociosPorEntrenador(0);
        }

        /* Da de baja logicamente una rutina sin romper asociaciones existentes ni borrar ejercicios. */
        public void DarDeBaja(int idRutina, int idEditor)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var rutina = datos.Rutinas.Buscar(idRutina);
                if (rutina == null)
                {
                    throw new InvalidOperationException("La rutina no existe.");
                }

                ValidarEdicion(datos, idRutina, idEditor);
                rutina.Estado = false;
                datos.GuardarCambios();
            }
        }

        /* Reactiva logicamente una rutina del catalogo. */
        public void Reactivar(int idRutina, int idEditor)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var rutina = datos.Rutinas.Buscar(idRutina);
                if (rutina == null)
                {
                    throw new InvalidOperationException("La rutina no existe.");
                }

                ValidarEntrenador(datos, rutina.IdEntrenador);
                ValidarEdicion(datos, idRutina, idEditor);
                rutina.Estado = true;
                datos.GuardarCambios();
            }
        }

        private static SocioRutinaItem CrearItemSocioRutina(Membresia membresia)
        {
            var entrenador = membresia.Entrenadores == null ? null : membresia.Entrenadores.FirstOrDefault(e => e.Estado);
            var usuario = entrenador == null ? null : entrenador.Entrenador;
            return new SocioRutinaItem
            {
                IdSocio = membresia.IdSocio,
                IdMembresia = membresia.IdMembresia,
                IdRutina = membresia.IdRutina,
                NombreSocio = membresia.Socio == null ? "Socio no disponible" : membresia.Socio.Apellido + ", " + membresia.Socio.Nombre,
                DNI = membresia.Socio == null ? "-" : membresia.Socio.DNI,
                NombrePlan = membresia.Plan == null ? "-" : membresia.Plan.Nombre,
                CuotaHasta = CuotaMembresiaLogica.CubiertaHasta(membresia.Cuotas),
                NombreEntrenador = usuario == null ? "Sin asignar" : usuario.Apellido + ", " + usuario.Nombre,
                NombreRutina = membresia.Rutina == null ? string.Empty : membresia.Rutina.Nombre
            };
        }

        private static void ValidarMembresiaParaRutina(Membresia membresia)
        {
            if (membresia == null || !membresia.Estado || membresia.Socio == null || !membresia.Socio.Estado || membresia.Plan == null || !membresia.Plan.Estado)
            {
                throw new InvalidOperationException("La membresia seleccionada no esta habilitada.");
            }
        }

        private static void ValidarPermisoSobreMembresia(IUnidadDeTrabajo datos, int idMembresia, int idEntrenador)
        {
            var creador = datos.UsuariosSistema.ConsultarSoloLectura("Rol")
                .SingleOrDefault(u => u.IdUsuarioSistema == idEntrenador);
            if (!ValidacionesGimnasio.EsAdministradorActivo(creador) && !datos.MembresiasEntrenadores.Any(me =>
                me.IdMembresia == idMembresia && me.IdEntrenador == idEntrenador && me.Estado))
            {
                throw new InvalidOperationException("El socio no esta asignado a este entrenador.");
            }
        }

        private static void ValidarEntrenador(IUnidadDeTrabajo datos, int idEntrenador)
        {
            var entrenador = datos.UsuariosSistema.Consultar("Rol")
                .SingleOrDefault(u => u.IdUsuarioSistema == idEntrenador);
            if (!ValidacionesGimnasio.PuedeGestionarRutinas(entrenador))
            {
                throw new InvalidOperationException("El usuario no posee rol de Entrenador o Administrador activo.");
            }
        }

        private static void ValidarDatos(Rutina rutina)
        {
            if (rutina == null)
            {
                throw new ArgumentNullException("rutina");  // Se recibió null donde no corresponde.
            }

            if (string.IsNullOrWhiteSpace(rutina.Nombre))
            {
                throw new InvalidOperationException("El nombre de la rutina es obligatorio.");
            }

            if (rutina.FechaInicio.HasValue && rutina.FechaFin.HasValue && rutina.FechaFin.Value < rutina.FechaInicio.Value)
            {
                throw new InvalidOperationException("La fecha de fin no puede ser anterior a la fecha de inicio.");
            }
        }
    }
}
