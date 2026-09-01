using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class Socio
    {
        public Socio()
        {
            Membresias = new HashSet<Membresia>();
            Asistencias = new HashSet<Asistencia>();
            Rutinas = new HashSet<Rutina>();
            Estado = true;
        }

        [Key]
        public int IdSocio { get; set; }

        [Required]
        [StringLength(20)]
        [Index("UX_Socio_DNI", IsUnique = true)]
        public string DNI { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? Peso { get; set; }

        [Column(TypeName = "decimal")]
        public decimal? Altura { get; set; }

        public bool Estado { get; set; }

        [InverseProperty("Socio")]
        public virtual ICollection<Membresia> Membresias { get; set; }

        [InverseProperty("Socio")]
        public virtual ICollection<Asistencia> Asistencias { get; set; }

        [InverseProperty("Socio")]
        public virtual ICollection<Rutina> Rutinas { get; set; }
    }
}
