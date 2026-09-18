using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de ejercicios de una rutina. */
    public class RutinaEjercicioLogica
    {
        /* Valida la rutina, el ejercicio y sus parámetros antes de incorporarlo a la plantilla. */
        public RutinaEjercicio AgregarEjercicio(RutinaEjercicio rutinaEjercicio)
        {
            ValidarDatos(rutinaEjercicio);
            using (var datos = new UnidadDeTrabajoGimnasio())
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

        /* Valida y guarda los cambios de ejercicios de una rutina sobre el registro existente. */
        public RutinaEjercicio Modificar(RutinaEjercicio rutinaEjercicio)
        {
            ValidarDatos(rutinaEjercicio);
            using (var datos = new UnidadDeTrabajoGimnasio())
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
                existente.DiaSemana = rutinaEjercicio.DiaSemana;
                existente.Estado = rutinaEjercicio.Estado;
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Da de baja el ejercicio de la rutina sin eliminar su registro. */
        public void Quitar(int idRutinaEjercicio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
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

        /* Consulta ejercicios de una rutina de la rutina indicada, ordenados para entrenar para devolver los datos a la capa visual. */
        public List<RutinaEjercicio> ListarPorRutina(int idRutina)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.RutinaEjercicios.ConsultarSoloLectura("Ejercicio").Where(re => re.IdRutina == idRutina && re.Estado).OrderBy(re => re.DiaSemana.HasValue ? re.DiaSemana.Value : int.MaxValue).ThenBy(re => re.Orden).ToList();
            }
        }

        /* Consulta los ejercicios de la rutina vigente del socio, ordenados por día y por orden dentro del día. */
        public List<RutinaEjercicio> ListarSemanaPorSocio(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                var membresia = datos.Membresias.ConsultarSoloLectura("Rutina")
                    .Where(m => m.IdSocio == idSocio && m.Estado && m.IdRutina.HasValue)
                    .OrderByDescending(m => m.FechaInicio)
                    .FirstOrDefault();
                if (membresia == null || !membresia.IdRutina.HasValue)
                {
                    return new List<RutinaEjercicio>();
                }

                var idRutina = membresia.IdRutina.Value;
                return datos.RutinaEjercicios.ConsultarSoloLectura("Ejercicio").Where(re => re.IdRutina == idRutina && re.Estado).OrderBy(re => re.DiaSemana.HasValue ? re.DiaSemana.Value : int.MaxValue).ThenBy(re => re.Orden).ToList();
            }
        }

        /* Comprueba los campos y rangos obligatorios de ejercicios de una rutina antes de persistirlos. */
        private static void ValidarDatos(RutinaEjercicio rutinaEjercicio)
        {
            if (rutinaEjercicio == null)
            {
                throw new ArgumentNullException("rutinaEjercicio");
            }

            if (rutinaEjercicio.IdRutina <= 0)
            {
                throw new InvalidOperationException("Seleccione una rutina.");
            }

            if (rutinaEjercicio.IdEjercicio <= 0)
            {
                throw new InvalidOperationException("Seleccione un ejercicio.");
            }

            if (!rutinaEjercicio.Series.HasValue)
            {
                throw new InvalidOperationException("Las series son obligatorias.");
            }

            if (rutinaEjercicio.Series.Value <= 0)
            {
                throw new InvalidOperationException("Las series deben ser mayores que cero.");
            }

            if (!rutinaEjercicio.Repeticiones.HasValue)
            {
                throw new InvalidOperationException("Las repeticiones son obligatorias.");
            }

            if (rutinaEjercicio.Repeticiones.Value <= 0)
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

            if (!rutinaEjercicio.DiaSemana.HasValue)
            {
                throw new InvalidOperationException("Seleccione el dia de la rutina.");
            }

            ValidacionesGimnasio.ValidarDiaRutina(rutinaEjercicio.DiaSemana);
        }
    }
}
