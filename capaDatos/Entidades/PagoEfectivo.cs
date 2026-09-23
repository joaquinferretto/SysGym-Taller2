using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa detalles de efectivo y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla PagoEfectivo y cada propiedad { get; set; } es una columna.
    public class PagoEfectivo
    {
        /* Inicializa los valores y colecciones necesarios para crear detalles de efectivo. */
        public PagoEfectivo()
        {
            MetodosPago = new HashSet<MetodoPago>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdPagoEfectivo { get; set; }
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).
        public virtual ICollection<MetodoPago> MetodosPago { get; set; }  // Navegación: lista de MetodoPago relacionados (no es columna).
    }
}
