using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa métodos de pago y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla MetodoPago y cada propiedad { get; set; } es una columna.
    public class MetodoPago
    {
        /* Inicializa los valores y colecciones necesarios para crear métodos de pago. */
        public MetodoPago()
        {
            Pagos = new HashSet<Pago>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdMetodoPago { get; set; }
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).

        [StringLength(500)]  // Máximo 500 caracteres.
        public string Observaciones { get; set; }  // Nota libre (opcional).
        public int? IdNroPagoMP { get; set; }  // FK opcional: detalle de Mercado Pago.
        public int? IdPagoEfectivo { get; set; }  // FK opcional: detalle de efectivo. Solo una de las dos FK tiene valor.

        [ForeignKey("IdNroPagoMP")]  // Une esta navegación con la columna IdNroPagoMP (clave foránea).
        public virtual MercadoPago MercadoPago { get; set; }  // Navegación: objeto MercadoPago relacionado (no es columna; se carga con Include).

        [ForeignKey("IdPagoEfectivo")]  // Une esta navegación con la columna IdPagoEfectivo (clave foránea).
        public virtual PagoEfectivo PagoEfectivo { get; set; }  // Navegación: objeto PagoEfectivo relacionado (no es columna; se carga con Include).
        public virtual ICollection<Pago> Pagos { get; set; }  // Navegación: lista de Pago relacionados (no es columna).
    }
}
