using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class CuotaMembresia
    {
        public CuotaMembresia()
        {
            EstadoPago = EstadosCuota.Pendiente;
        }

        [Key]
        public int IdCuotaMembresia { get; set; }

        [Index("UX_CuotaMembresia_Periodo", 1, IsUnique = true)]
        public int IdMembresia { get; set; }

        public int? IdRegistroPago { get; set; }

        [Index("UX_CuotaMembresia_Periodo", 2, IsUnique = true)]
        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Importe { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoPago { get; set; }

        [ForeignKey("IdMembresia")]
        public virtual Membresia Membresia { get; set; }

        [ForeignKey("IdRegistroPago")]
        public virtual Pago Pago { get; set; }

    }
}
