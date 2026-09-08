using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class Pago
    {
        public Pago()
        {
            Estado = EstadosTransaccionPago.Pendiente;
            Cuotas = new HashSet<CuotaMembresia>();
        }

        [Key]
        public int IdRegistroPago { get; set; }

        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Importe { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        public int IdMetodoPago { get; set; }

        [ForeignKey("IdMetodoPago")]
        public virtual MetodoPago MetodoPago { get; set; }

        public virtual ICollection<CuotaMembresia> Cuotas { get; set; }
    }
}
