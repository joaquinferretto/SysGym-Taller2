using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa cuotas de membresía y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla CuotaMembresia y cada propiedad { get; set; } es una columna.
    public class CuotaMembresia
    {
        /* Inicializa los valores y colecciones necesarios para crear cuotas de membresía. */
        public CuotaMembresia()
        {
            EstadoPago = EstadosCuota.Pendiente;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdCuotaMembresia { get; set; }

        [Index("UX_CuotaMembresia_Periodo", 1, IsUnique = true)]  // Parte 1 de un índice único doble: no se repite el mismo período en una membresía.
        public int IdMembresia { get; set; }  // FK: membresía a la que pertenece la cuota.
        public int? IdRegistroPago { get; set; }  // FK opcional: pago que la canceló (NULL = sin pagar).

        [Index("UX_CuotaMembresia_Periodo", 2, IsUnique = true)]  // Parte 2 de un índice único doble: no se repite el mismo período en una membresía.
        public DateTime FechaDesde { get; set; }  // Primer día del mes cubierto.
        public DateTime FechaHasta { get; set; }  // Último día del mes cubierto.

        [Column(TypeName = "decimal")]  // En SQL es DECIMAL: número exacto, sin errores de redondeo.
        public decimal Importe { get; set; }  // Precio del plan al momento de generar la cuota.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(20)]  // Máximo 20 caracteres.
        public string EstadoPago { get; set; }  // Pendiente, Pagada o Anulada (ver EstadosCuota).

        [ForeignKey("IdMembresia")]  // Une esta navegación con la columna IdMembresia (clave foránea).
        public virtual Membresia Membresia { get; set; }  // Navegación: objeto Membresia relacionado (no es columna; se carga con Include).

        [ForeignKey("IdRegistroPago")]  // Une esta navegación con la columna IdRegistroPago (clave foránea).
        public virtual Pago Pago { get; set; }  // Navegación: objeto Pago relacionado (no es columna; se carga con Include).
    }
}
