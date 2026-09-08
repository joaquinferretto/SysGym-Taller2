using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class RutinaAsignacion
    {
        [Key]
        public int IdRutinaAsignacion { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public DateTime? FechaFin { get; set; }

        public bool Estado { get; set; }

        public int IdRutina { get; set; }

        public int IdMembresia { get; set; }

        [ForeignKey("IdRutina")]
        public virtual Rutina Rutina { get; set; }

        [ForeignKey("IdMembresia")]
        public virtual Membresia Membresia { get; set; }
    }
}
