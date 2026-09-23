using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa asignaciones de entrenador y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla MembresiaEntrenador y cada propiedad { get; set; } es una columna.
    public class MembresiaEntrenador
    {
        /* Inicializa los valores y colecciones necesarios para crear asignaciones de entrenador. */
        public MembresiaEntrenador()
        {
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdMembresiaEntrenador { get; set; }
        public int IdMembresia { get; set; }  // FK: membresía del socio.
        public int IdEntrenador { get; set; }  // FK: usuario con rol entrenador.
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).

        [ForeignKey("IdMembresia")]  // Une esta navegación con la columna IdMembresia (clave foránea).
        public virtual Membresia Membresia { get; set; }  // Navegación: objeto Membresia relacionado (no es columna; se carga con Include).

        [ForeignKey("IdEntrenador")]  // Une esta navegación con la columna IdEntrenador (clave foránea).
        public virtual UsuarioSistema Entrenador { get; set; }  // Navegación: objeto UsuarioSistema relacionado (no es columna; se carga con Include).
    }
}
