using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<UsuarioSistema>();
            Estado = true;
        }

        [Key]
        public int IdRol { get; set; }

        [Required]
        [StringLength(50)]
        [Index("UX_Rol_Descripcion", IsUnique = true)]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public virtual ICollection<UsuarioSistema> Usuarios { get; set; }
    }
}
