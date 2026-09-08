using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class RutinaEjercicioLogica
    {
        public RutinaEjercicio AgregarEjercicio(RutinaEjercicio rutinaEjercicio)
        {
            ValidarDatos(rutinaEjercicio);
            using (var datos = new GymUnidadDeTrabajo())
            {
                var rutina = datos.Rutinas.Buscar(rutinaEjercicio.IdRutina);
                var ejercicio = datos.Ejercicios.Buscar(rutinaEjercicio.IdEjercicio);
                if (rutina == null || !rutina.Estado)
                {
                    throw new InvalidOperationException("La rutina no existe o está inactiva.");
                }

                if (ejercicio == null || !ejercicio.Estado)
                {
                    throw new InvalidOperationException("El ejercicio no existe o está inactivo.");
                }

                rutinaEjercicio.Estado = true;
                datos.RutinaEjercicios.Agregar(rutinaEjercicio);
                datos.GuardarCambios();
                return rutinaEjercicio;
            }
        }

        public RutinaEjercicio Modificar(RutinaEjercicio rutinaEjercicio)
        {
            ValidarDatos(rutinaEjercicio);
            using (var datos = new GymUnidadDeTrabajo())
            {
                var existente = datos.RutinaEjercicios.Buscar(rutinaEjercicio.IdRutinaEjercicio);
                if (existente == null)
                {
                    throw new InvalidOperationException("El ejercicio de la rutina no existe.");
                }

                var rutina = datos.Rutinas.Buscar(existente.IdRutina);
                var ejercicio = datos.Ejercicios.Buscar(rutinaEjercicio.IdEjercicio);
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
                datos.GuardarCambios();
                return existente;
            }
        }

        public void Quitar(int idRutinaEjercicio)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                var rutinaEjercicio = datos.RutinaEjercicios.Buscar(idRutinaEjercicio);
                if (rutinaEjercicio == null)
                {
                    throw new InvalidOperationException("El ejercicio de la rutina no existe.");
                }

                rutinaEjercicio.Estado = false;
                datos.GuardarCambios();
            }
        }

        public List<RutinaEjercicio> ListarPorRutina(int idRutina)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.RutinaEjercicios.Consultar("Ejercicio")
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
