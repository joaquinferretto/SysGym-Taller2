using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa planes y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla Plan y cada propiedad { get; set; } es una columna.
    public class Plan
    {
        /* Inicializa los valores y colecciones necesarios para crear planes. */
        public Plan()
        {
            Membresias = new HashSet<Membresia>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdPlan { get; set; }

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(100)]  // Máximo 100 caracteres.
        public string Nombre { get; set; }  // Nombre comercial del plan.

        [StringLength(500)]  // Máximo 500 caracteres.
        public string Descripcion { get; set; }  // Qué incluye el plan.

        [Column(TypeName = "decimal")]  // En SQL es DECIMAL: número exacto, sin errores de redondeo.
        public decimal Precio { get; set; }  // Precio mensual; se copia a cada cuota nueva.
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).
        public virtual ICollection<Membresia> Membresias { get; set; }  // Navegación: lista de Membresia relacionados (no es columna).
    }
}
