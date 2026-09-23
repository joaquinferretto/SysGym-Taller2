using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa ejercicios y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla Ejercicio y cada propiedad { get; set; } es una columna.
    public class Ejercicio
    {
        /* Inicializa los valores y colecciones necesarios para crear ejercicios. */
        public Ejercicio()
        {
            Rutinas = new HashSet<RutinaEjercicio>();  // Colección vacía para no trabajar con null.
            EjercicioImagenes = new HashSet<EjercicioImagen>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdEjercicio { get; set; }

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(100)]  // Máximo 100 caracteres.
        public string Nombre { get; set; }  // Nombre que se ve en rutinas y en el PDF.

        [StringLength(500)]  // Máximo 500 caracteres.
        public string Descripcion { get; set; }  // Observación: músculos o técnica.
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).
        public virtual ICollection<RutinaEjercicio> Rutinas { get; set; }  // Navegación: rutinas en las que aparece.
        public virtual ICollection<EjercicioImagen> EjercicioImagenes { get; set; }  // Navegación: imágenes del ejercicio (hasta 4).
    }
}
