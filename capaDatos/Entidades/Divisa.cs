using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace exxen2._0.capaDatos.Entidades
{
    public class Divisa
    {
        public Divisa()
        {
            PagosEfectivo = new HashSet<PagoEfectivo>();
            Estado = true;
        }

        [Key]
        public int IdDivisa { get; set; }

        public decimal CambioHoy { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public bool Estado { get; set; }

        public virtual ICollection<PagoEfectivo> PagosEfectivo { get; set; }
    }
}
