using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa roles y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla Rol y cada propiedad { get; set; } es una columna.
    public class Rol
    {
        /* Inicializa los valores y colecciones necesarios para crear roles. */
        public Rol()
        {
            Usuarios = new HashSet<UsuarioSistema>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdRol { get; set; }

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(50)]  // Máximo 50 caracteres.
        [Index("UX_Rol_Descripcion", IsUnique = true)]  // Índice único: no puede haber dos filas con el mismo valor.
        public string Descripcion { get; set; }  // Nombre del rol: Administrador, Entrenador o Recepcionista.
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).
        public virtual ICollection<UsuarioSistema> Usuarios { get; set; }  // Navegación: lista de UsuarioSistema relacionados (no es columna).
    }
}
