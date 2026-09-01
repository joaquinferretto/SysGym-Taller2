using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    public class RutinaLogica
    {
        public Rutina Crear(Rutina rutina)
        {
            ValidarDatos(rutina);
            using (var context = new GymContext())
            {
                var socio = context.Socios.Find(rutina.IdSocio);
                var membresia = context.Membresias.Include(m => m.Plan)
                    .Where(m => m.IdSocio == rutina.IdSocio && m.Estado)
                    .OrderByDescending(m => m.FechaInicio).FirstOrDefault();
                var entrenador = context.UsuariosSistema.Include(u => u.Rol)
                    .SingleOrDefault(u => u.IdUsuarioSistema == rutina.IdEntrenador);
                ValidarReferencias(socio, membresia, entrenador);

                rutina.Estado = true;
                if (rutina.FechaCreacion == default(DateTime))
                {
                    rutina.FechaCreacion = DateTime.Now;
                }

                context.Rutinas.Add(rutina);
                context.SaveChanges();
                return rutina;
            }
        }

        public Rutina Modificar(Rutina rutina)
        {
            ValidarDatos(rutina);
            using (var context = new GymContext())
            {
                var existente = context.Rutinas.Find(rutina.IdRutina);
                if (existente == null)
                {
                    throw new InvalidOperationException("La rutina no existe.");
                }

                var entrenador = context.UsuariosSistema.Include(u => u.Rol)
                    .SingleOrDefault(u => u.IdUsuarioSistema == rutina.IdEntrenador);
                if (!ValidacionesGym.EsEntrenadorActivo(entrenador))
                {
                    throw new InvalidOperationException("El usuario no posee rol de Entrenador activo.");
                }

                existente.Nombre = rutina.Nombre;
                existente.Descripcion = rutina.Descripcion;
                existente.FechaInicio = rutina.FechaInicio;
                existente.FechaFin = rutina.FechaFin;
                existente.IdEntrenador = rutina.IdEntrenador;
                existente.Estado = rutina.Estado;
                context.SaveChanges();
                return existente;
            }
        }

        public Rutina ObtenerPorId(int idRutina)
        {
            using (var context = new GymContext())
            {
                return context.Rutinas.Include(r => r.Socio).Include(r => r.Entrenador)
                    .Include("Ejercicios.Ejercicio")
                    .SingleOrDefault(r => r.IdRutina == idRutina);
            }
        }

        public List<Rutina> ListarPorSocio(int idSocio)
        {
            using (var context = new GymContext())
            {
                return context.Rutinas.Include(r => r.Entrenador)
                    .Where(r => r.IdSocio == idSocio).OrderByDescending(r => r.FechaCreacion).ToList();
            }
        }

        public List<Rutina> ListarPorEntrenador(int idEntrenador)
        {
            using (var context = new GymContext())
            {
                return context.Rutinas.Include(r => r.Socio)
                    .Where(r => r.IdEntrenador == idEntrenador).OrderByDescending(r => r.FechaCreacion).ToList();
            }
        }

        public List<Rutina> ListarActivas()
        {
            using (var context = new GymContext())
            {
                return context.Rutinas.Include(r => r.Socio).Include(r => r.Entrenador)
                    .Where(r => r.Estado).OrderByDescending(r => r.FechaCreacion).ToList();
            }
        }

        public void DarDeBaja(int idRutina)
        {
            using (var context = new GymContext())
            {
                var rutina = context.Rutinas.Find(idRutina);
                if (rutina == null)
                {
                    throw new InvalidOperationException("La rutina no existe.");
                }

                rutina.Estado = false;
                context.SaveChanges();
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

        private static void ValidarReferencias(Socio socio, Membresia membresia, UsuarioSistema entrenador)
        {
            if (socio == null || !socio.Estado)
            {
                throw new InvalidOperationException("El socio no existe o está inactivo.");
            }

            if (membresia == null || membresia.Plan == null
                || !membresia.Plan.Estado || !membresia.Plan.IncluyeRutinaPersonal)
            {
                throw new InvalidOperationException("El socio no tiene una membresía habilitada con rutina personalizada.");
            }

            if (!ValidacionesGym.EsEntrenadorActivo(entrenador))
            {
                throw new InvalidOperationException("El usuario no posee rol de Entrenador activo.");
            }
        }
    }
}
