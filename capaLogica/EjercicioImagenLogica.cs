using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las imágenes del catálogo sin duplicarlas en las relaciones de rutinas. */
    // Imágenes de un ejercicio: hasta 4, guardadas como archivo; SQL solo guarda la ruta.
    // La usa GestionEjerciciosFormulario.
    public class EjercicioImagenLogica
    {
        /* Lista las imágenes de un ejercicio en el orden definido para futuras exportaciones. */
        public List<EjercicioImagen> ListarPorEjercicio(int idEjercicio)
        {
            if (idEjercicio <= 0)
                return new List<EjercicioImagen>();

            using (var datos = new UnidadDeTrabajoGimnasio())  // Abre la conexión; al salir del bloque se cierra sola, aunque haya error (try-with-resources).
            {
                return datos.EjercicioImagenes.ConsultarSoloLectura()  // Solo lectura: EF no vigila cambios (más liviano para listar).
                    .Where(i => i.IdEjercicio == idEjercicio)  // Where = filtro (como filter de Streams / WHERE de SQL). "x => ..." es una lambda.
                    .OrderBy(i => i.Orden)  // Ordena (ORDER BY).
                    .ThenBy(i => i.IdEjercicioImagen)
                    .ToList();  // Acá se ejecuta la consulta en SQL y se trae la lista.
            }
        }

        /* Copia una imagen administrada y registra su siguiente orden para el ejercicio. */
        public EjercicioImagen Agregar(int idEjercicio, byte[] contenido)
        {
            var rutaNueva = (string)null;
            try
            {
                using (var datos = new UnidadDeTrabajoGimnasio())
                {
                    var ejercicio = datos.Ejercicios.Buscar(idEjercicio);  // Busca por clave primaria; si no existe devuelve null.
                    if (ejercicio == null || !ejercicio.Estado)
                        throw new InvalidOperationException("El ejercicio no existe o está inactivo.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.

                    var imagenesExistentes = datos.EjercicioImagenes
                        .Where(i => i.IdEjercicio == idEjercicio)
                        .OrderBy(i => i.Orden)
                        .ThenBy(i => i.IdEjercicioImagen)
                        .ToList();
                    ValidarLimiteParaAgregar(imagenesExistentes.Count);

                    for (var indice = 0; indice < imagenesExistentes.Count; indice++)
                        imagenesExistentes[indice].Orden = indice + 1;
                    rutaNueva = AlmacenamientoImagenes.GuardarEjercicio(contenido, idEjercicio);
                    var imagen = new EjercicioImagen
                    {
                        IdEjercicio = idEjercicio,
                        RutaRelativa = rutaNueva,
                        Orden = imagenesExistentes.Count + 1
                    };
                    datos.EjercicioImagenes.Agregar(imagen);  // Deja el objeto listo para INSERT (se ejecuta en GuardarCambios).
                    datos.GuardarCambios();  // EF envía a SQL los INSERT/UPDATE pendientes.
                    return imagen;
                }
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(rutaNueva))
                    AlmacenamientoImagenes.Eliminar(rutaNueva);
                throw;
            }
        }

        /* Mantiene la regla en lógica aunque la pantalla deshabilite preventivamente el botón. */
        public static void ValidarLimiteParaAgregar(int cantidadActual)
        {
            if (cantidadActual < 0)
                throw new ArgumentOutOfRangeException("cantidadActual");
            if (cantidadActual >= ProcesadorImagenes.MaxImagenesPorEjercicio)
                throw new InvalidOperationException("Un ejercicio puede tener como máximo 4 imágenes.");
        }

        /* Quita la relación y borra el archivo si no quedó referenciado por otra imagen. */
        public void Quitar(int idEjercicioImagen)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var imagen = datos.EjercicioImagenes.Buscar(idEjercicioImagen);
                if (imagen == null)
                    throw new InvalidOperationException("La imagen del ejercicio no existe.");

                var ruta = imagen.RutaRelativa;
                var idEjercicio = imagen.IdEjercicio;
                var compartida = datos.EjercicioImagenes.Existe(i => i.RutaRelativa == ruta && i.IdEjercicioImagen != idEjercicioImagen);
                datos.EjercicioImagenes.Eliminar(imagen);
                var restantes = datos.EjercicioImagenes.Where(i => i.IdEjercicio == idEjercicio && i.IdEjercicioImagen != idEjercicioImagen)
                    .OrderBy(i => i.Orden).ThenBy(i => i.IdEjercicioImagen).ToList();
                for (var indice = 0; indice < restantes.Count; indice++)
                    restantes[indice].Orden = indice + 1;
                datos.GuardarCambios();
                if (!compartida)
                    AlmacenamientoImagenes.Eliminar(ruta);
            }
        }
    }
}
