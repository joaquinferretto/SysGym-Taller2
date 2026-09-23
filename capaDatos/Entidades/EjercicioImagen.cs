using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa una imagen del catálogo de ejercicios, ordenada para usos posteriores como PDF. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla EjercicioImagen y cada propiedad { get; set; } es una columna.
    public class EjercicioImagen
    {
        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdEjercicioImagen { get; set; }

        public int IdEjercicio { get; set; }  // FK: ejercicio al que pertenece la imagen.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(260)]  // Máximo 260 caracteres.
        public string RutaRelativa { get; set; }  // Ruta del archivo (Imagenes\Ejercicios\...); la imagen no se guarda en SQL.

        public int Orden { get; set; }  // Posición; la primera es la que va al PDF.

        [ForeignKey("IdEjercicio")]  // Une esta navegación con la columna IdEjercicio (clave foránea).
        public virtual Ejercicio Ejercicio { get; set; }  // Navegación: objeto Ejercicio relacionado (no es columna; se carga con Include).
    }
}
