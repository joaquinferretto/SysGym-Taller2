using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class MembresiaEntrenador
    {
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
