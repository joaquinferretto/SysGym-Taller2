using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa usuarios del sistema y sus relaciones persistidas en SQL Server. */
    public class UsuarioSistema
    {
        /* Inicializa los valores y colecciones necesarios para crear usuarios del sistema. */
        public UsuarioSistema()
        {
            MembresiasRegistradas = new HashSet<Membresia>();
            MembresiasComoEntrenador = new HashSet<MembresiaEntrenador>();
            RutinasComoEntrenador = new HashSet<Rutina>();
            Estado = true;
        }

        [Key]
        public int IdUsuarioSistema { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(20)]
        [Index("UX_UsuarioSistema_DNI", IsUnique = true)]
        public string DNI { get; set; }

        [StringLength(30)]
        public string Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Salario { get; set; }

        [Required]
        [StringLength(50)]
        [Index("UX_UsuarioSistema_Username", IsUnique = true)]
        [Column("Username")]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(500)]
        [Column("Password")]
        public string Clave { get; set; }
        public bool Estado { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[] Foto { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char")]
        public string Sexo { get; set; }
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public virtual Rol Rol { get; set; }

        [InverseProperty("UsuarioSistema")]
        public virtual ICollection<Membresia> MembresiasRegistradas { get; set; }

        [InverseProperty("Entrenador")]
        public virtual ICollection<MembresiaEntrenador> MembresiasComoEntrenador { get; set; }

        [InverseProperty("Entrenador")]
        public virtual ICollection<Rutina> RutinasComoEntrenador { get; set; }
    }
}
