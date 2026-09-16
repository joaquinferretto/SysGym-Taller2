using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de membresías. */
    public class MembresiaLogica
    {
        /* Identifica al usuario reservado que representa el entrenador inicial de toda membresía. */
        public const string NombreUsuarioEntrenadorGeneral = "entrenador.general";

        /* Crea una membresía, su primera cuota y su entrenador general en una única transacción. */
        public Membresia Crear(Membresia membresia)
        {
            ValidarMembresia(membresia);
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var socio = datos.Socios.Buscar(membresia.IdSocio);
                var plan = datos.Planes.Buscar(membresia.IdPlan);
                var usuario = datos.UsuariosSistema.Consultar("Rol").SingleOrDefault(u => u.IdUsuarioSistema == membresia.IdUsuarioSistema);
                ValidarReferenciasActivas(socio, plan, usuario);
                if (datos.Membresias.Any(m => m.IdSocio == membresia.IdSocio && m.Estado))
                    throw new InvalidOperationException("El socio ya posee una membresía habilitada.");

                membresia.Estado = true;
                membresia.IdRutina = null;
                if (membresia.FechaInicio == default(DateTime))
                    membresia.FechaInicio = DateTime.Today;
                if (membresia.FechaVencimiento == default(DateTime))
                    membresia.FechaVencimiento = CuotaMembresiaLogica.CalcularPeriodoHasta(membresia.FechaInicio);
                if (membresia.FechaVencimiento < membresia.FechaInicio)
                    throw new InvalidOperationException("La fecha de vencimiento no puede ser anterior a la fecha de inicio.");

                datos.Membresias.Agregar(membresia);
                datos.GuardarCambios();
                socio.Estado = true;
                datos.MembresiasEntrenadores.Agregar(new MembresiaEntrenador
                {
                    IdMembresia = membresia.IdMembresia,
                    IdEntrenador = ObtenerEntrenadorGeneral(datos).IdUsuarioSistema,
                    Estado = true
                });
                CuotaMembresiaLogica.CrearPrimeraCuotaEnContexto(datos, membresia, plan);
                datos.GuardarCambios();
                transaccion.Confirmar();
                return membresia;
            }
        }

        /* Actualiza fechas y datos propios sin permitir cambiar referencias históricas desde este método. */
        public Membresia Modificar(Membresia membresia)
        {
            if (membresia == null)
                throw new ArgumentNullException("membresia");

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.Membresias.Buscar(membresia.IdMembresia);
                if (existente == null)
                    throw new InvalidOperationException("La membresía no existe.");
                if (existente.IdPlan != membresia.IdPlan)
                    throw new InvalidOperationException("El plan debe cambiarse mediante CambiarPlan.");
                if (existente.IdSocio != membresia.IdSocio || existente.IdUsuarioSistema != membresia.IdUsuarioSistema)
                    throw new InvalidOperationException("No se pueden cambiar las referencias históricas de la membresía.");
                ValidarFechas(membresia);

                existente.FechaInicio = membresia.FechaInicio;
                existente.FechaVencimiento = membresia.FechaVencimiento;
                if (existente.Estado != membresia.Estado)
                    CambiarEstadoEnContexto(datos, existente, membresia.Estado);
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca una membresía con sus relaciones comerciales y de entrenador. */
        public Membresia ObtenerPorId(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio", "UsuarioSistema", "Rutina", "Cuotas.Pago", "Entrenadores.Entrenador").SingleOrDefault(m => m.IdMembresia == idMembresia);
        }

        /* Busca el historial de membresías de un socio ordenado desde la más reciente. */
        public List<Membresia> ObtenerPorSocio(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Membresias.ConsultarSoloLectura("Plan", "Rutina").Where(m => m.IdSocio == idSocio).OrderByDescending(m => m.FechaInicio).ToList();
        }

        /* Consulta membresías activas para operaciones que requieren una relación vigente. */
        public List<Membresia> ListarHabilitadas()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio", "Rutina").Where(m => m.Estado).OrderBy(m => m.FechaInicio).ToList();
        }

        /* Consulta membresías activas e históricas para la gestión administrativa. */
        public List<Membresia> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio", "Rutina").OrderByDescending(m => m.Estado).ThenBy(m => m.Socio.Apellido).ThenBy(m => m.Socio.Nombre).ToList();
        }

        /* Cambia solamente el plan y conserva las cuotas y asociaciones históricas de la membresía. */
        public void CambiarPlan(int idMembresia, int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var membresia = datos.Membresias.Buscar(idMembresia);
                var plan = datos.Planes.Buscar(idPlan);
                if (membresia == null)
                    throw new InvalidOperationException("La membresía no existe.");
                if (plan == null || !plan.Estado)
                    throw new InvalidOperationException("El plan seleccionado se encuentra inactivo o no existe.");

                membresia.IdPlan = plan.IdPlan;
                datos.GuardarCambios();
            }
        }

        /* Habilita una membresía y sincroniza el estado del socio. */
        public void Habilitar(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var membresia = ObtenerMembresia(datos, idMembresia);
                CambiarEstadoEnContexto(datos, membresia, true);
                datos.GuardarCambios();
            }
        }

        /* Da de baja lógicamente una membresía sin borrar cuotas, pagos, rutina ni entrenador. */
        public void Deshabilitar(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var membresia = ObtenerMembresia(datos, idMembresia);
                CambiarEstadoEnContexto(datos, membresia, false);
                datos.GuardarCambios();
            }
        }

        /* Conserva el nombre histórico de la operación para los consumidores existentes. */
        public void DarDeBaja(int idMembresia)
        {
            Deshabilitar(idMembresia);
        }

        /* Devuelve la membresía seguida por la unidad de trabajo o informa una clave inválida. */
        private static Membresia ObtenerMembresia(IUnidadDeTrabajo datos, int idMembresia)
        {
            var membresia = datos.Membresias.Buscar(idMembresia);
            if (membresia == null)
                throw new InvalidOperationException("La membresía no existe.");
            return membresia;
        }

        /* Cambia el estado de la membresía y refleja si el socio conserva otra membresía activa. */
        private static void CambiarEstadoEnContexto(IUnidadDeTrabajo datos, Membresia membresia, bool estado)
        {
            membresia.Estado = estado;
            var socio = datos.Socios.Buscar(membresia.IdSocio);
            if (socio == null)
                throw new InvalidOperationException("El socio de la membresía no existe.");

            socio.Estado = estado || datos.Membresias.Any(m => m.IdSocio == membresia.IdSocio && m.IdMembresia != membresia.IdMembresia && m.Estado);
        }

        /* Busca el usuario reservado y exige que mantenga rol Entrenador activo. */
        private static UsuarioSistema ObtenerEntrenadorGeneral(IUnidadDeTrabajo datos)
        {
            var general = datos.UsuariosSistema.Consultar("Rol").SingleOrDefault(u => u.NombreUsuario == NombreUsuarioEntrenadorGeneral);
            if (!ValidacionesGimnasio.EsEntrenadorActivo(general))
                throw new InvalidOperationException("No existe un Entrenador General activo para crear la membresía.");
            return general;
        }

        /* Exige los identificadores de socio, plan y usuario que registra la membresía. */
        private static void ValidarMembresia(Membresia membresia)
        {
            if (membresia == null)
                throw new ArgumentNullException("membresia");
            if (membresia.IdSocio <= 0 || membresia.IdPlan <= 0 || membresia.IdUsuarioSistema <= 0)
                throw new InvalidOperationException("Socio, plan actual y usuario de alta son obligatorios.");
        }

        /* Comprueba que las fechas de la membresía sean válidas. */
        private static void ValidarFechas(Membresia membresia)
        {
            if (membresia.FechaInicio == default(DateTime))
                throw new InvalidOperationException("La fecha de inicio es obligatoria.");
            if (membresia.FechaVencimiento == default(DateTime))
                membresia.FechaVencimiento = CuotaMembresiaLogica.CalcularPeriodoHasta(membresia.FechaInicio);
            if (membresia.FechaVencimiento < membresia.FechaInicio)
                throw new InvalidOperationException("La fecha de vencimiento no puede ser anterior a la fecha de inicio.");
        }

        /* Comprueba que socio, plan y usuario de alta estén disponibles para una nueva membresía. */
        private static void ValidarReferenciasActivas(Socio socio, Plan plan, UsuarioSistema usuario)
        {
            if (socio == null)
                throw new InvalidOperationException("El socio seleccionado no existe.");
            if (plan == null || !plan.Estado)
                throw new InvalidOperationException("El plan seleccionado no existe o está inactivo.");
            if (!ValidacionesGimnasio.PuedeRegistrarMembresia(usuario))
                throw new InvalidOperationException("El usuario de alta debe ser Administrador o Recepcionista activo.");
        }
    }
}
