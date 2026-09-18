using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa asignaciones de entrenador y sus relaciones persistidas en SQL Server. */
    public class MembresiaEntrenador
    {
        /* Inicializa los valores y colecciones necesarios para crear asignaciones de entrenador. */
        public MembresiaEntrenador()
        {
            Estado = true;
        }

        [Key]
        public int IdMembresiaEntrenador { get; set; }
        public int IdMembresia { get; set; }
        public int IdEntrenador { get; set; }
        public bool Estado { get; set; }

        [ForeignKey("IdMembresia")]
        public virtual Membresia Membresia { get; set; }

        [ForeignKey("IdEntrenador")]
        public virtual UsuarioSistema Entrenador { get; set; }
    }
}
