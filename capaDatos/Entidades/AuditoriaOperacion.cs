using System;
using System.ComponentModel.DataAnnotations;

namespace exxen2._0.capaDatos.Entidades
{
    /* Historial de operaciones exitosas. Nombre y rol conservan el valor al realizar la acción. */
    public class AuditoriaOperacion
    {
        [Key]
        public int IdAuditoria { get; set; }
        public DateTime FechaHora { get; set; }
        public int IdUsuario { get; set; }
        [Required, StringLength(201)]
        public string UsuarioNombre { get; set; }
        [Required, StringLength(50)]
        public string Rol { get; set; }
        [Required, StringLength(50)]
        public string Operacion { get; set; }
        [Required, StringLength(50)]
        public string Entidad { get; set; }
        public int IdEntidad { get; set; }
        [Required, StringLength(1000)]
        public string Detalle { get; set; }
        public virtual UsuarioSistema Usuario { get; set; }
    }
}
