using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class RutinaEjercicio
    {
        public RutinaEjercicio()
        {
            Estado = true;
        }

        [Key]
        public int IdRutinaEjercicio { get; set; }

        public int IdRutina { get; set; }

        public int IdEjercicio { get; set; }

        public int? Series { get; set; }

        public int? Repeticiones { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? Peso { get; set; }

        public int Descanso { get; set; }

        public int Orden { get; set; }

        public bool Estado { get; set; }

        [ForeignKey("IdRutina")]
        public virtual Rutina Rutina { get; set; }

        [ForeignKey("IdEjercicio")]
        public virtual Ejercicio Ejercicio { get; set; }
    }
}
