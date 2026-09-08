using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class Plan
    {
        public Plan()
        {
            Membresias = new HashSet<Membresia>();
            Estado = true;
        }

        [Key]
        public int IdPlan { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Precio { get; set; }

        public bool IncluyeEntrenador { get; set; }

        public bool IncluyeRutinaPersonal { get; set; }

        public bool Estado { get; set; }

        public int IdRutina { get; set; }

        [ForeignKey("IdRutina")]
        public virtual Rutina Rutina { get; set; }

        public virtual ICollection<Membresia> Membresias { get; set; }
    }
}
