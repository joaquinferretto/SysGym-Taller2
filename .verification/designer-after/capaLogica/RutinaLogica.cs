using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de plantillas de rutina. */
    public class RutinaLogica
    {
        /* Valida y registra plantillas de rutina mediante la unidad de trabajo, conservando sus reglas de alta. */
        public Rutina Crear(Rutina rutina)
        {
            ValidarDatos(rutina);
            using (var datos = new GymUnidadDeTrabajo())
            {
                ValidarEntrenador(datos, rutina.IdEntrenador);
                rutina.Estado = true;
                if (rutina.FechaCreacion == default(DateTime))
                {
                    rutina.FechaCreacion = DateTime.Now;
                }

                datos.Rutinas.Agregar(rutina);
                datos.GuardarCambios();
                return rutina;
            }
        }

        /* Valida y guarda los cambios de plantillas de rutina sobre el registro existente. */
        public Rutina Modificar(Rutina rutina)
        {
            ValidarDatos(rutina);
            using (var datos = new GymUnidadDeTrabajo())
            {
                var existente = datos.Rutinas.Buscar(rutina.IdRutina);
                if (existente == null)
                {
                    throw new InvalidOperationException("La rutina no existe.");
                }

                ValidarEntrenador(datos, rutina.IdEntrenador);
                existente.Nombre = rutina.Nombre;
                existente.Descripcion = rutina.Descripcion;
                existente.FechaInicio = rutina.FechaInicio;
                existente.FechaFin = rutina.FechaFin;
                existente.IdEntrenador = rutina.IdEntrenador;
                existente.Estado = rutina.Estado;
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca el registro de plantillas de rutina por identificador y devuelve los datos disponibles. */
        public Rutina ObtenerPorId(int idRutina)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Ejercicios.Ejercicio", "Asignaciones.Membresia.Socio").SingleOrDefault(r => r.IdRutina == idRutina);
            }
        }

        /* Consulta plantillas de rutina del catálogo reutilizable para devolver los datos a la capa visual. */
        public List<Rutina> ListarGenerales()
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Asignaciones").Where(r => r.Estado).OrderBy(r => r.Nombre).ToList();
            }
        }

        /* Consulta plantillas de rutina del entrenador indicado para devolver los datos a la capa visual. */
        public List<Rutina> ListarPorEntrenador(int idEntrenador)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Asignaciones").Where(r => r.IdEntrenador == idEntrenador).OrderByDescending(r => r.FechaCreacion).ToList();
            }
        }

        /* Consulta plantillas de rutina del entrenador indicado, incluyendo bajas para devolver los datos a la capa visual. */
        public List<Rutina> ListarPorEntrenadorParaGestion(int idEntrenador)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Rutinas.ConsultarSoloLectura("Entrenador", "Asignaciones").Where(r => r.IdEntrenador == idEntrenador).OrderByDescending(r => r.Estado).ThenByDescending(r => r.FechaCreacion).ToList();
            }
        }

        /* Consulta plantillas de rutina activas para devolver los datos a la capa visual. */
        public List<Rutina> ListarActivas()
        {
            return ListarGenerales();
        }

        /* Desactiva el registro de plantillas de rutina sin eliminar su historial. */
        public void DarDeBaja(int idRutina)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                var rutina = datos.Rutinas.Buscar(idRutina);
                if (rutina == null)
                {
                    throw new InvalidOperationException("La rutina no existe.");
                }

                rutina.Estado = false;
                var asignaciones = datos.RutinaAsignaciones.Where(a => a.IdRutina == idRutina && a.Estado).ToList();
                foreach (var asignacion in asignaciones)
                {
                    asignacion.Estado = false;
                    asignacion.FechaFin = DateTime.Now;
                }

                datos.GuardarCambios();
            }
        }

        /* Recupera el estado activo del registro de plantillas de rutina según las validaciones de la operación. */
        public void Reactivar(int idRutina)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                var rutina = datos.Rutinas.Buscar(idRutina);
                if (rutina == null)
                {
                    throw new InvalidOperationException("La rutina no existe.");
                }

                ValidarEntrenador(datos, rutina.IdEntrenador);
                rutina.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Exige que el creador de la rutina sea un usuario activo con rol de entrenador. */
        private static void ValidarEntrenador(IUnidadDeTrabajo datos, int idEntrenador)
        {
            var entrenador = datos.UsuariosSistema.Consultar("Rol").SingleOrDefault(u => u.IdUsuarioSistema == idEntrenador);
            if (!ValidacionesGym.EsEntrenadorActivo(entrenador))
            {
                throw new InvalidOperationException("El usuario no posee rol de Entrenador activo.");
            }
        }

        /* Comprueba los campos y rangos obligatorios de plantillas de rutina antes de persistirlos. */
        private static void ValidarDatos(Rutina rutina)
        {
            if (rutina == null)
            {
                throw new ArgumentNullException("rutina");
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
