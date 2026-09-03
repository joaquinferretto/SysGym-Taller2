using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class EjercicioLogica
    {
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

        public Ejercicio ObtenerPorId(int idEjercicio)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Ejercicios.Buscar(idEjercicio);
            }
        }

        public List<Ejercicio> ListarActivos()
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Ejercicios.Consultar().Where(e => e.Estado).OrderBy(e => e.Nombre).ToList();
            }
        }

        public List<Ejercicio> ListarParaGestion()
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.Ejercicios.Consultar()
                    .OrderByDescending(e => e.Estado).ThenBy(e => e.Nombre).ToList();
            }
        }

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
