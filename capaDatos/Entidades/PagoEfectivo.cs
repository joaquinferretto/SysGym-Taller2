using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class PagoEfectivo
    {
        public PagoEfectivo()
        {
            MetodosPago = new HashSet<MetodoPago>();
            Estado = true;
        }

        [Key]
        public int IdPagoEfectivo { get; set; }

        public bool Estado { get; set; }

        public int IdDivisa { get; set; }

        [ForeignKey("IdDivisa")]
        public virtual Divisa Divisa { get; set; }

        public virtual ICollection<MetodoPago> MetodosPago { get; set; }
    }
}
