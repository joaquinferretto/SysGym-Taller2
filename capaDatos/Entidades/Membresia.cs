using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class Membresia
    {
        public Membresia()
        {
            Entrenadores = new HashSet<MembresiaEntrenador>();
            Cuotas = new HashSet<CuotaMembresia>();
            Estado = true;
        }

        [Key]
        public int IdMembresia { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public bool Estado { get; set; }

        public int IdPlan { get; set; }

        public int IdSocio { get; set; }

        public int IdUsuarioSistema { get; set; }

        [ForeignKey("IdPlan")]
        public virtual Plan Plan { get; set; }

        [ForeignKey("IdSocio")]
        public virtual Socio Socio { get; set; }

        [ForeignKey("IdUsuarioSistema")]
        public virtual UsuarioSistema UsuarioSistema { get; set; }

        public virtual ICollection<CuotaMembresia> Cuotas { get; set; }

        public virtual ICollection<MembresiaEntrenador> Entrenadores { get; set; }
    }
}
