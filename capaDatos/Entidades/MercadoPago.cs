using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa datos de Mercado Pago y sus relaciones persistidas en SQL Server. */
    public class MercadoPago
    {
        /* Inicializa los valores y colecciones necesarios para crear datos de Mercado Pago. */
        public MercadoPago()
        {
            MetodosPago = new HashSet<MetodoPago>();
        }

        [Key]
        public int IdNroPagoMP { get; set; }

        [StringLength(100)]
        [Column("MercadoPagoPaymentId")]
        public string IdentificadorPago { get; set; }

        [StringLength(100)]
        [Column("MercadoPagoPreferenceId")]
        public string IdentificadorPreferencia { get; set; }

        [StringLength(150)]
        [Column("ExternalReference")]
        public string ReferenciaExterna { get; set; }

        [StringLength(200)]
        [Column("StatusDetail")]
        public string DetalleEstado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public virtual ICollection<MetodoPago> MetodosPago { get; set; }
    }
}
