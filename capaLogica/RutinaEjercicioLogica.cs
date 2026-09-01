using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    public class RutinaEjercicioLogica
    {
        public RutinaEjercicio AgregarEjercicio(RutinaEjercicio rutinaEjercicio)
        {
            ValidarDatos(rutinaEjercicio);
            using (var context = new GymContext())
            {
                var rutina = context.Rutinas.Find(rutinaEjercicio.IdRutina);
                var ejercicio = context.Ejercicios.Find(rutinaEjercicio.IdEjercicio);
                if (rutina == null || !rutina.Estado)
                {
                    throw new InvalidOperationException("La rutina no existe o está inactiva.");
                }

                if (ejercicio == null || !ejercicio.Estado)
                {
                    throw new InvalidOperationException("El ejercicio no existe o está inactivo.");
                }

                rutinaEjercicio.Estado = true;
                context.RutinaEjercicios.Add(rutinaEjercicio);
                context.SaveChanges();
                return rutinaEjercicio;
            }
        }

        public RutinaEjercicio Modificar(RutinaEjercicio rutinaEjercicio)
        {
            ValidarDatos(rutinaEjercicio);
            using (var context = new GymContext())
            {
                var existente = context.RutinaEjercicios.Find(rutinaEjercicio.IdRutinaEjercicio);
                if (existente == null)
                {
                    throw new InvalidOperationException("El ejercicio de la rutina no existe.");
                }

                var rutina = context.Rutinas.Find(existente.IdRutina);
                var ejercicio = context.Ejercicios.Find(rutinaEjercicio.IdEjercicio);
                if (rutina == null || !rutina.Estado)
                {
                    throw new InvalidOperationException("La rutina no existe o está inactiva.");
                }

                if (ejercicio == null || !ejercicio.Estado)
                {
                    throw new InvalidOperationException("El ejercicio no existe o está inactivo.");
                }

                existente.IdEjercicio = rutinaEjercicio.IdEjercicio;
                existente.Series = rutinaEjercicio.Series;
                existente.Repeticiones = rutinaEjercicio.Repeticiones;
                existente.Peso = rutinaEjercicio.Peso;
                existente.Descanso = rutinaEjercicio.Descanso;
                existente.Orden = rutinaEjercicio.Orden;
                existente.Estado = rutinaEjercicio.Estado;
                context.SaveChanges();
                return existente;
            }
        }

        public void Quitar(int idRutinaEjercicio)
        {
            using (var context = new GymContext())
            {
                var rutinaEjercicio = context.RutinaEjercicios.Find(idRutinaEjercicio);
                if (rutinaEjercicio == null)
                {
                    throw new InvalidOperationException("El ejercicio de la rutina no existe.");
                }

                rutinaEjercicio.Estado = false;
                context.SaveChanges();
            }
        }

        public List<RutinaEjercicio> ListarPorRutina(int idRutina)
        {
            using (var context = new GymContext())
            {
                return context.RutinaEjercicios.Include(re => re.Ejercicio)
                    .Where(re => re.IdRutina == idRutina && re.Estado)
                    .OrderBy(re => re.Orden).ToList();
            }
        }

        private static void ValidarDatos(RutinaEjercicio rutinaEjercicio)
        {
            if (rutinaEjercicio == null)
            {
                throw new ArgumentNullException("rutinaEjercicio");
            }

            if (rutinaEjercicio.Series.HasValue && rutinaEjercicio.Series.Value <= 0)
            {
                throw new InvalidOperationException("Las series deben ser mayores que cero.");
            }

            if (rutinaEjercicio.Repeticiones.HasValue && rutinaEjercicio.Repeticiones.Value <= 0)
            {
                throw new InvalidOperationException("Las repeticiones deben ser mayores que cero.");
            }

            if (rutinaEjercicio.Peso.HasValue && rutinaEjercicio.Peso.Value < 0)
            {
                throw new InvalidOperationException("El peso no puede ser negativo.");
            }

            if (rutinaEjercicio.Descanso < 0)
            {
                throw new InvalidOperationException("El descanso no puede ser negativo.");
            }

            if (rutinaEjercicio.Orden <= 0)
            {
                throw new InvalidOperationException("El orden debe ser mayor que cero.");
            }
        }
    }
}
