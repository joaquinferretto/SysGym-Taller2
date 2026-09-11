using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa métodos de pago y sus relaciones persistidas en SQL Server. */
    public class MetodoPago
    {
        /* Inicializa los valores y colecciones necesarios para crear métodos de pago. */
        public MetodoPago()
        {
            Pagos = new HashSet<Pago>();
            Estado = true;
        }

        [Key]
        public int IdMetodoPago { get; set; }
        public bool Estado { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }
        public int? IdNroPagoMP { get; set; }
        public int? IdPagoEfectivo { get; set; }

        [ForeignKey("IdNroPagoMP")]
        public virtual MercadoPago MercadoPago { get; set; }

        [ForeignKey("IdPagoEfectivo")]
        public virtual PagoEfectivo PagoEfectivo { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
