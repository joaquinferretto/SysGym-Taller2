using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa una imagen del catálogo de ejercicios, ordenada para usos posteriores como PDF. */
    public class EjercicioImagen
    {
        [Key]
        public int IdEjercicioImagen { get; set; }

        public int IdEjercicio { get; set; }

        [Required]
        [StringLength(260)]
        public string RutaRelativa { get; set; }

        public int Orden { get; set; }

        [ForeignKey("IdEjercicio")]
        public virtual Ejercicio Ejercicio { get; set; }
    }
}
