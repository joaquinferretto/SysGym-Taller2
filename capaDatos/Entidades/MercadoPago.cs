using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa datos de Mercado Pago y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla MercadoPago y cada propiedad { get; set; } es una columna.
    public class MercadoPago
    {
        /* Inicializa los valores y colecciones necesarios para crear datos de Mercado Pago. */
        public MercadoPago()
        {
            MetodosPago = new HashSet<MetodoPago>();  // Colección vacía para no trabajar con null.
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdNroPagoMP { get; set; }

        [StringLength(100)]  // Máximo 100 caracteres.
        [Column("MercadoPagoPaymentId")]  // En SQL la columna se llama MercadoPagoPaymentId.
        public string IdentificadorPago { get; set; }  // Id de pago que devuelve Mercado Pago (texto, no es FK).

        [StringLength(100)]  // Máximo 100 caracteres.
        [Column("MercadoPagoPreferenceId")]  // En SQL la columna se llama MercadoPagoPreferenceId.
        public string IdentificadorPreferencia { get; set; }  // Id de preferencia de Mercado Pago (texto, no es FK).

        [StringLength(150)]  // Máximo 150 caracteres.
        [Column("ExternalReference")]  // En SQL la columna se llama ExternalReference.
        public string ReferenciaExterna { get; set; }  // Referencia propia para reconocer la operación.

        [StringLength(200)]  // Máximo 200 caracteres.
        [Column("StatusDetail")]  // En SQL la columna se llama StatusDetail.
        public string DetalleEstado { get; set; }  // Estado detallado informado por Mercado Pago.
        public DateTime? FechaAprobacion { get; set; }  // Cuándo se aprobó (NULL si no se aprobó).
        public virtual ICollection<MetodoPago> MetodosPago { get; set; }  // Navegación: lista de MetodoPago relacionados (no es columna).
    }
}
