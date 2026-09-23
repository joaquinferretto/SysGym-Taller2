using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa socios y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla Socio y cada propiedad { get; set; } es una columna.
    public class Socio
    {
        /* Inicializa los valores y colecciones necesarios para crear socios. */
        public Socio()
        {
            Membresias = new HashSet<Membresia>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdSocio { get; set; }

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(20)]  // Máximo 20 caracteres.
        [Index("UX_Socio_DNI", IsUnique = true)]  // Índice único: no puede haber dos filas con el mismo valor.
        public string DNI { get; set; }  // Documento; no se puede repetir.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(100)]  // Máximo 100 caracteres.
        public string Nombre { get; set; }  // Nombre del socio.

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(100)]  // Máximo 100 caracteres.
        public string Apellido { get; set; }  // Apellido del socio.
        public DateTime? FechaNacimiento { get; set; }  // Fecha de nacimiento (DateTime? = opcional).

        [Column(TypeName = "decimal")]  // En SQL es DECIMAL: número exacto, sin errores de redondeo.
        public decimal? Peso { get; set; }  // Peso en kg (opcional).

        [Column(TypeName = "decimal")]  // En SQL es DECIMAL: número exacto, sin errores de redondeo.
        public decimal? Altura { get; set; }  // Altura en metros (opcional); se usa para el IMC.
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).

        [StringLength(260)]  // Máximo 260 caracteres.
        public string FotoRuta { get; set; }  // Ruta relativa de la foto; la imagen está en disco, no en SQL.

        [StringLength(1)]  // Máximo 1 carácter.
        [Column(TypeName = "char")]  // En SQL es CHAR (texto de largo fijo).
        public string Sexo { get; set; }  // Un carácter: M o F (opcional).

        [InverseProperty("Socio")]  // Indica que del otro lado la relación es la propiedad Socio.
        public virtual ICollection<Membresia> Membresias { get; set; }  // Navegación: lista de Membresia relacionados (no es columna).

    }
}
