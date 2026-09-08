using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de ejercicios. */
    public class EjercicioLogica
    {
        /* Valida y registra ejercicios mediante la unidad de trabajo, conservando sus reglas de alta. */
        public Ejercicio Crear(Ejercicio ejercicio)
        {
            ValidarDatos(ejercicio);
            using (var datos = new GymUnidadDeTrabajo())
            {
                if (datos.Ejercicios.Existe(e => e.Nombre == ejercicio.Nombre))
                {
                    throw new InvalidOperationException("El ejercicio ya existe.");
                }

                ejercicio.Estado = true;
                datos.Ejercicios.Agregar(ejercicio);
                datos.GuardarCambios();
                return ejercicio;
            }
        }

        /* Valida y guarda los cambios de ejercicios sobre el registro existente. */
        public Ejercicio Modificar(Ejercicio ejercicio)
        {
            ValidarDatos(ejercicio);
            using (var datos = new GymUnidadDeTrabajo())
            {
                var existente = datos.Ejercicios.Buscar(ejercicio.IdEjercicio);
                if (existente == null)
                {
                    throw new InvalidOperationException("El ejercicio no existe.");
                }

                if (datos.Ejercicios.Existe(e => e.Nombre == ejercicio.Nombre && e.IdEjercicio != ejercicio.IdEjercicio))
                {
                    throw new InvalidOperationException("El ejercicio ya existe.");
                }

                existente.Nombre = ejercicio.Nombre;
                existente.Descripcion = ejercicio.Descripcion;
                existente.Estado = ejercicio.Estado;
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca el registro de ejercicios por identificador y devuelve los datos disponibles. */
        public Ejercicio ObtenerPorId(int idEjercicio)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Ejercicios.ConsultarSoloLectura().SingleOrDefault(e => e.IdEjercicio == idEjercicio);
            }
        }

        /* Consulta ejercicios activos para devolver los datos a la capa visual. */
        public List<Ejercicio> ListarActivos()
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Ejercicios.ConsultarSoloLectura().Where(e => e.Estado).OrderBy(e => e.Nombre).ToList();
            }
        }

        /* Consulta ejercicios activos e inactivos para su gestión para devolver los datos a la capa visual. */
        public List<Ejercicio> ListarParaGestion()
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Ejercicios.ConsultarSoloLectura().OrderByDescending(e => e.Estado).ThenBy(e => e.Nombre).ToList();
            }
        }

        /* Desactiva el registro de ejercicios sin eliminar su historial. */
        public void DarDeBaja(int idEjercicio)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                var ejercicio = datos.Ejercicios.Buscar(idEjercicio);
                if (ejercicio == null)
                {
                    throw new InvalidOperationException("El ejercicio no existe.");
                }

                ejercicio.Estado = false;
                datos.GuardarCambios();
            }
        }

        /* Recupera el estado activo del registro de ejercicios según las validaciones de la operación. */
        public void Reactivar(int idEjercicio)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                var ejercicio = datos.Ejercicios.Buscar(idEjercicio);
                if (ejercicio == null)
                {
                    throw new InvalidOperationException("El ejercicio no existe.");
                }

                ejercicio.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Comprueba los campos y rangos obligatorios de ejercicios antes de persistirlos. */
        private static void ValidarDatos(Ejercicio ejercicio)
        {
            if (ejercicio == null)
            {
                throw new ArgumentNullException("ejercicio");
            }

            if (string.IsNullOrWhiteSpace(ejercicio.Nombre))
            {
                throw new InvalidOperationException("El nombre del ejercicio es obligatorio.");
            }
        }
    }
}
