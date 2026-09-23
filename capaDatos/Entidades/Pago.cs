using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa pagos y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla Pago y cada propiedad { get; set; } es una columna.
    public class Pago
    {
        /* Inicializa los valores y colecciones necesarios para crear pagos. */
        public Pago()
        {
            Estado = EstadosTransaccionPago.Pendiente;
            Cuotas = new HashSet<CuotaMembresia>();  // Colección vacía para no trabajar con null.
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdRegistroPago { get; set; }
        public DateTime Fecha { get; set; }  // Fecha del cobro.

        [Column(TypeName = "decimal")]  // En SQL es DECIMAL: número exacto, sin errores de redondeo.
        public decimal Importe { get; set; }  // Monto cobrado.

        [StringLength(500)]  // Máximo 500 caracteres.
        public string Descripcion { get; set; }  // Detalle libre del cobro.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(20)]  // Máximo 20 caracteres.
        public string Estado { get; set; }  // Pendiente, Aprobado, Rechazado, Anulado o Reembolsado.
        public int IdMetodoPago { get; set; }  // FK: con qué se pagó.

        [ForeignKey("IdMetodoPago")]  // Une esta navegación con la columna IdMetodoPago (clave foránea).
        public virtual MetodoPago MetodoPago { get; set; }  // Navegación: objeto MetodoPago relacionado (no es columna; se carga con Include).
        public virtual ICollection<CuotaMembresia> Cuotas { get; set; }  // Navegación: cuota que este pago canceló.
    }
}
