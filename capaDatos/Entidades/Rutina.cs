using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa plantillas de rutina y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla Rutina y cada propiedad { get; set; } es una columna.
    public class Rutina
    {
        /* Inicializa los valores y colecciones necesarios para crear plantillas de rutina. */
        public Rutina()
        {
            Ejercicios = new HashSet<RutinaEjercicio>();  // Colección vacía para no trabajar con null.
            Membresias = new HashSet<Membresia>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdRutina { get; set; }

        [Required]  // Obligatorio: la columna no acepta NULL.
        [StringLength(100)]  // Máximo 100 caracteres.
        public string Nombre { get; set; }  // Nombre de la rutina.

        [StringLength(500)]  // Máximo 500 caracteres.
        public string Descripcion { get; set; }  // Objetivo o notas.
        public DateTime FechaCreacion { get; set; }  // Cuándo se creó.
        public DateTime? FechaInicio { get; set; }  // Inicio de vigencia (opcional).
        public DateTime? FechaFin { get; set; }  // Fin de vigencia (opcional).
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).
        public int IdEntrenador { get; set; }  // FK: autor; solo él o un administrador pueden editarla.

        [ForeignKey("IdEntrenador")]  // Une esta navegación con la columna IdEntrenador (clave foránea).
        public virtual UsuarioSistema Entrenador { get; set; }  // Navegación: objeto UsuarioSistema relacionado (no es columna; se carga con Include).
        public virtual ICollection<RutinaEjercicio> Ejercicios { get; set; }  // Navegación: ejercicios de la rutina, por día y orden.
        public virtual ICollection<Membresia> Membresias { get; set; }  // Navegación: membresías que tienen asignada esta rutina.
    }
}
