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
        public string NombreEntrenador { get; set; }  // null si la membresía no tiene entrenador asignado.
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
    // Arma los datos de la rutina de un socio para el PDF (sin tocar la base). La usa MisSociosFormulario.
    public sealed class RutinaExportacionLogica
    {
        public RutinaParaExportar Obtener(int idSocio, int idMembresia, int idUsuario)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())  // Abre la conexión; al salir del bloque se cierra sola, aunque haya error (try-with-resources).
            {
                var usuario = datos.UsuariosSistema.ConsultarSoloLectura("Rol")  // Solo lectura: EF no vigila cambios (más liviano para listar).
                    .SingleOrDefault(u => u.IdUsuarioSistema == idUsuario);  // Devuelve el único que cumple o null.
                if (!ValidacionesGimnasio.EsAdministradorActivo(usuario) &&
                    !ValidacionesGimnasio.EsEntrenadorActivo(usuario))
                    throw new InvalidOperationException("El usuario no está habilitado para consultar rutinas.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.

                var membresia = datos.Membresias.ConsultarSoloLectura("Socio", "Plan", "Rutina")
                    .SingleOrDefault(m => m.IdMembresia == idMembresia && m.IdSocio == idSocio);
                if (membresia == null || !membresia.Estado || !membresia.Socio.Estado || !membresia.Plan.Estado)
                    throw new InvalidOperationException("La membresía seleccionada no está habilitada para consultar.");
                if (!ValidacionesGimnasio.EsAdministradorActivo(usuario) &&
                    !datos.MembresiasEntrenadores.ConsultarSoloLectura().Any(a =>  // ¿Existe al menos uno? (no trae filas).
                        a.IdMembresia == idMembresia && a.IdEntrenador == idUsuario && a.Estado))
                    throw new InvalidOperationException("El socio no está asignado a este entrenador.");
                if (membresia.Rutina == null || !membresia.Rutina.Estado)
                    throw new InvalidOperationException("El socio seleccionado no tiene una rutina activa asignada.");

                // Carga las relaciones en una consulta; el PDF usa solo la primera por orden.
                var ejercicios = datos.RutinaEjercicios.ConsultarSoloLectura("Ejercicio.EjercicioImagenes")
                    .Where(e => e.IdRutina == membresia.IdRutina && e.Estado)  // Where = filtro (como filter de Streams / WHERE de SQL). "x => ..." es una lambda.
                    .OrderBy(e => e.DiaSemana).ThenBy(e => e.Orden).ThenBy(e => e.IdRutinaEjercicio).ToList();  // Acá se ejecuta la consulta en SQL y se trae la lista.
                if (ejercicios.Count == 0)
                    throw new InvalidOperationException("La rutina asignada no contiene ejercicios para exportar.");

                // Entrenador de la asignación activa; si no hay, el PDF no muestra la línea.
                var entrenador = datos.MembresiasEntrenadores.ConsultarSoloLectura("Entrenador")
                    .Where(a => a.IdMembresia == idMembresia && a.Estado)
                    .Select(a => a.Entrenador)
                    .FirstOrDefault();

                return new RutinaParaExportar
                {
                    NombreSocio = membresia.Socio.Nombre,
                    ApellidoSocio = membresia.Socio.Apellido,
                    NombreRutina = membresia.Rutina.Nombre,
                    NombreEntrenador = entrenador == null ? null : (entrenador.Nombre + " " + entrenador.Apellido).Trim(),
                    Ejercicios = ejercicios.Select(e => new EjercicioParaExportar  // Select = transforma cada elemento (como map de Streams).
                    {
                        DiaSemana = e.DiaSemana, Orden = e.Orden, Nombre = e.Ejercicio.Nombre,
                        Descripcion = e.Ejercicio.Descripcion, Series = e.Series,
                        Repeticiones = e.Repeticiones, Peso = e.Peso, Descanso = e.Descanso,
                        Imagenes = e.Ejercicio.EjercicioImagenes.OrderBy(i => i.Orden)  // Ordena (ORDER BY).
                            .ThenBy(i => i.IdEjercicioImagen).Take(1).Select(i => i.RutaRelativa).ToList()
                    }).ToList()
                };
            }
        }
    }
}
