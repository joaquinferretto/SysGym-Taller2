using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa usuarios del sistema y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla UsuarioSistema y cada propiedad { get; set; } es una columna.
    public class UsuarioSistema
    {
        /* Inicializa los valores y colecciones necesarios para crear usuarios del sistema. */
        public UsuarioSistema()
        {
            MembresiasRegistradas = new HashSet<Membresia>();  // Colección vacía para no trabajar con null.
            MembresiasComoEntrenador = new HashSet<MembresiaEntrenador>();  // Colección vacía para no trabajar con null.
            RutinasComoEntrenador = new HashSet<Rutina>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdUsuarioSistema { get; set; }

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(100)]  // Máximo 100 caracteres.
        public string Nombre { get; set; }  // Nombre del empleado.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(100)]  // Máximo 100 caracteres.
        public string Apellido { get; set; }  // Apellido del empleado.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(20)]  // Máximo 20 caracteres.
        [Index("UX_UsuarioSistema_DNI", IsUnique = true)]  // Índice único: no puede haber dos filas con el mismo valor.
        public string DNI { get; set; }  // Documento; no se puede repetir.

        [StringLength(30)]  // Máximo 30 caracteres.
        public string Telefono { get; set; }  // Teléfono de contacto (opcional).
        public DateTime? FechaNacimiento { get; set; }  // Fecha de nacimiento (DateTime? = opcional).

        [Column(TypeName = "decimal")]  // En SQL es DECIMAL: número exacto, sin errores de redondeo.
        public decimal Salario { get; set; }  // Sueldo; decimal para no perder centavos.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(50)]  // Máximo 50 caracteres.
        [Index("UX_UsuarioSistema_Username", IsUnique = true)]  // Índice único: no puede haber dos filas con el mismo valor.
        [Column("Username")]  // En SQL la columna se llama Username.
        public string NombreUsuario { get; set; }  // Usuario para iniciar sesión.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(500)]  // Máximo 500 caracteres.
        [Column("Password")]  // En SQL la columna se llama Password.
        public string Clave { get; set; }  // Hash Argon2id de la contraseña, nunca el texto real.
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).

        [StringLength(260)]  // Máximo 260 caracteres.
        public string FotoRuta { get; set; }  // Ruta relativa de la foto; la imagen está en disco, no en SQL.

        [StringLength(1)]  // Máximo 1 carácter.
        [Column(TypeName = "char")]  // En SQL es CHAR (texto de largo fijo).
        public string Sexo { get; set; }  // Un carácter: M o F (opcional).
        public int IdRol { get; set; }  // FK: qué rol tiene (define qué panel abre).

        [ForeignKey("IdRol")]  // Une esta navegación con la columna IdRol (clave foránea).
        public virtual Rol Rol { get; set; }  // Navegación: objeto Rol relacionado (no es columna; se carga con Include).

        [InverseProperty("UsuarioSistema")]  // Indica que del otro lado la relación es la propiedad UsuarioSistema.
        public virtual ICollection<Membresia> MembresiasRegistradas { get; set; }  // Navegación: membresías que dio de alta este usuario.

        [InverseProperty("Entrenador")]  // Indica que del otro lado la relación es la propiedad Entrenador.
        public virtual ICollection<MembresiaEntrenador> MembresiasComoEntrenador { get; set; }  // Navegación: asignaciones donde es el entrenador.

        [InverseProperty("Entrenador")]  // Indica que del otro lado la relación es la propiedad Entrenador.
        public virtual ICollection<Rutina> RutinasComoEntrenador { get; set; }  // Navegación: rutinas de las que es autor.
    }
}
