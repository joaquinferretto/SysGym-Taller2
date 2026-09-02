using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class RutinaLogica
    {
        // Las rutinas son plantillas generales. No se guardan con un socio.
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

        public Rutina ObtenerPorId(int idRutina)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Rutinas.Consultar("Entrenador", "Ejercicios.Ejercicio", "Asignaciones.Membresia.Socio")
                    .SingleOrDefault(r => r.IdRutina == idRutina);
            }
        }

        public List<Rutina> ListarGenerales()
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Rutinas.Consultar("Entrenador", "Asignaciones")
                    .Where(r => r.Estado)
                    .OrderBy(r => r.Nombre).ToList();
            }
        }

        public List<Rutina> ListarPorEntrenador(int idEntrenador)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Rutinas.Consultar("Entrenador", "Asignaciones")
                    .Where(r => r.IdEntrenador == idEntrenador)
                    .OrderByDescending(r => r.FechaCreacion).ToList();
            }
        }

        // Devuelve el catálogo, no una fila por cada socio asignado.
        public List<Rutina> ListarActivas()
        {
            return ListarGenerales();
        }

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
                var asignaciones = datos.RutinaAsignaciones
                    .Where(a => a.IdRutina == idRutina && a.Estado).ToList();
                foreach (var asignacion in asignaciones)
                {
                    asignacion.Estado = false;
                    asignacion.FechaFin = DateTime.Now;
                }

                datos.GuardarCambios();
            }
        }

        private static void ValidarEntrenador(IUnidadDeTrabajo datos, int idEntrenador)
        {
            var entrenador = datos.UsuariosSistema.Consultar("Rol")
                .SingleOrDefault(u => u.IdUsuarioSistema == idEntrenador);
            if (!ValidacionesGym.EsEntrenadorActivo(entrenador))
            {
                throw new InvalidOperationException("El usuario no posee rol de Entrenador activo.");
            }
        }

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

            if (rutina.FechaInicio.HasValue && rutina.FechaFin.HasValue
                && rutina.FechaFin.Value < rutina.FechaInicio.Value)
            {
                throw new InvalidOperationException("La fecha de fin no puede ser anterior a la fecha de inicio.");
            }
        }
    }
}
