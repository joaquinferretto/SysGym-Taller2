using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa ejercicios y sus relaciones persistidas en SQL Server. */
    public class Ejercicio
    {
        /* Inicializa los valores y colecciones necesarios para crear ejercicios. */
        public Ejercicio()
        {
            Rutinas = new HashSet<RutinaEjercicio>();
            Estado = true;
        }

        [Key]
        public int IdEjercicio { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public virtual ICollection<RutinaEjercicio> Rutinas { get; set; }
    }
}
