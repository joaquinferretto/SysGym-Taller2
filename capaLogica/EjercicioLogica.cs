using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    public class EjercicioLogica
    {
        public Ejercicio Crear(Ejercicio ejercicio)
        {
            ValidarDatos(ejercicio);
            using (var context = new GymContext())
            {
                if (context.Ejercicios.Any(e => e.Nombre == ejercicio.Nombre))
                {
                    throw new InvalidOperationException("El ejercicio ya existe.");
                }

                ejercicio.Estado = true;
                context.Ejercicios.Add(ejercicio);
                context.SaveChanges();
                return ejercicio;
            }
        }

        public Ejercicio Modificar(Ejercicio ejercicio)
        {
            ValidarDatos(ejercicio);
            using (var context = new GymContext())
            {
                var existente = context.Ejercicios.Find(ejercicio.IdEjercicio);
                if (existente == null)
                {
                    throw new InvalidOperationException("El ejercicio no existe.");
                }

                if (context.Ejercicios.Any(e => e.Nombre == ejercicio.Nombre && e.IdEjercicio != ejercicio.IdEjercicio))
                {
                    throw new InvalidOperationException("El ejercicio ya existe.");
                }

                existente.Nombre = ejercicio.Nombre;
                existente.Descripcion = ejercicio.Descripcion;
                existente.Estado = ejercicio.Estado;
                context.SaveChanges();
                return existente;
            }
        }

        public Ejercicio ObtenerPorId(int idEjercicio)
        {
            using (var context = new GymContext())
            {
                return context.Ejercicios.Find(idEjercicio);
            }
        }

        public List<Ejercicio> ListarActivos()
        {
            using (var context = new GymContext())
            {
                return context.Ejercicios.Where(e => e.Estado).OrderBy(e => e.Nombre).ToList();
            }
        }

        public void DarDeBaja(int idEjercicio)
        {
            using (var context = new GymContext())
            {
                var ejercicio = context.Ejercicios.Find(idEjercicio);
                if (ejercicio == null)
                {
                    throw new InvalidOperationException("El ejercicio no existe.");
                }

                ejercicio.Estado = false;
                context.SaveChanges();
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
