using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class Asistencia
    {
        public Asistencia()
        {
            Estado = true;
        }

        [Key]
        public int IdAsistencia { get; set; }

        public DateTime Fecha { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public int IdSocio { get; set; }

        [ForeignKey("IdSocio")]
        public virtual Socio Socio { get; set; }
    }
}
