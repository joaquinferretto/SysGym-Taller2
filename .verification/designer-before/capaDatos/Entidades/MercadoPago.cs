using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace exxen2._0.capaDatos.Entidades
{
    public class MercadoPago
    {
        public MercadoPago()
        {
            MetodosPago = new HashSet<MetodoPago>();
        }

        [Key]
        public int IdNroPagoMP { get; set; }

        [StringLength(100)]
        public string MercadoPagoPaymentId { get; set; }

        [StringLength(100)]
        public string MercadoPagoPreferenceId { get; set; }

        [StringLength(150)]
        public string ExternalReference { get; set; }

        [StringLength(200)]
        public string StatusDetail { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public virtual ICollection<MetodoPago> MetodosPago { get; set; }
    }
}
