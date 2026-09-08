using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class Rutina
    {
        public Rutina()
        {
            Ejercicios = new HashSet<RutinaEjercicio>();
            Planes = new HashSet<Plan>();
            Asignaciones = new HashSet<RutinaAsignacion>();
            Estado = true;
        }

        [Key]
        public int IdRutina { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public bool Estado { get; set; }

        public int IdEntrenador { get; set; }

        [ForeignKey("IdEntrenador")]
        public virtual UsuarioSistema Entrenador { get; set; }

        public virtual ICollection<RutinaEjercicio> Ejercicios { get; set; }

        public virtual ICollection<Plan> Planes { get; set; }

        public virtual ICollection<RutinaAsignacion> Asignaciones { get; set; }
    }
}
