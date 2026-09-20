using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public sealed class RutinaParaExportar
    {
        public string NombreSocio { get; set; }
        public string ApellidoSocio { get; set; }
        public string NombreRutina { get; set; }
        public List<EjercicioParaExportar> Ejercicios { get; set; } = new List<EjercicioParaExportar>();
    }

    public sealed class EjercicioParaExportar
    {
        public int? DiaSemana { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Series { get; set; }
        public int? Repeticiones { get; set; }
        public decimal? Peso { get; set; }
        public int Descanso { get; set; }
        public List<string> Imagenes { get; set; } = new List<string>();
    }

    /* Obtiene una instantánea de lectura; no evalúa deuda ni persiste cambios. */
    public sealed class RutinaExportacionLogica
    {
        public RutinaParaExportar Obtener(int idSocio, int idMembresia, int idUsuario)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var usuario = datos.UsuariosSistema.ConsultarSoloLectura("Rol")
                    .SingleOrDefault(u => u.IdUsuarioSistema == idUsuario);
                if (!ValidacionesGimnasio.EsAdministradorActivo(usuario) &&
                    !ValidacionesGimnasio.EsEntrenadorActivo(usuario))
                    throw new InvalidOperationException("El usuario no está habilitado para consultar rutinas.");

                var membresia = datos.Membresias.ConsultarSoloLectura("Socio", "Plan", "Rutina")
                    .SingleOrDefault(m => m.IdMembresia == idMembresia && m.IdSocio == idSocio);
                if (membresia == null || !membresia.Estado || !membresia.Socio.Estado || !membresia.Plan.Estado)
                    throw new InvalidOperationException("La membresía seleccionada no está habilitada para consultar.");
                if (!ValidacionesGimnasio.EsAdministradorActivo(usuario) &&
                    !datos.MembresiasEntrenadores.ConsultarSoloLectura().Any(a =>
                        a.IdMembresia == idMembresia && a.IdEntrenador == idUsuario && a.Estado))
                    throw new InvalidOperationException("El socio no está asignado a este entrenador.");
                if (membresia.Rutina == null || !membresia.Rutina.Estado)
                    throw new InvalidOperationException("El socio seleccionado no tiene una rutina activa asignada.");

                // Incluye todas las imágenes en la misma consulta, sin una consulta por ejercicio.
                var ejercicios = datos.RutinaEjercicios.ConsultarSoloLectura("Ejercicio.EjercicioImagenes")
                    .Where(e => e.IdRutina == membresia.IdRutina && e.Estado)
                    .OrderBy(e => e.DiaSemana).ThenBy(e => e.Orden).ThenBy(e => e.IdRutinaEjercicio).ToList();
                if (ejercicios.Count == 0)
                    throw new InvalidOperationException("La rutina asignada no contiene ejercicios para exportar.");

                return new RutinaParaExportar
                {
                    NombreSocio = membresia.Socio.Nombre,
                    ApellidoSocio = membresia.Socio.Apellido,
                    NombreRutina = membresia.Rutina.Nombre,
                    Ejercicios = ejercicios.Select(e => new EjercicioParaExportar
                    {
                        DiaSemana = e.DiaSemana, Orden = e.Orden, Nombre = e.Ejercicio.Nombre,
                        Descripcion = e.Ejercicio.Descripcion, Series = e.Series,
                        Repeticiones = e.Repeticiones, Peso = e.Peso, Descanso = e.Descanso,
                        Imagenes = e.Ejercicio.EjercicioImagenes.OrderBy(i => i.Orden)
                            .ThenBy(i => i.IdEjercicioImagen).Select(i => i.RutaRelativa).ToList()
                    }).ToList()
                };
            }
        }
    }
}
