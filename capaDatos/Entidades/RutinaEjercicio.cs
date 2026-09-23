using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa ejercicios de una rutina y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla RutinaEjercicio y cada propiedad { get; set; } es una columna.
    public class RutinaEjercicio
    {
        /* Inicializa los valores y colecciones necesarios para crear ejercicios de una rutina. */
        public RutinaEjercicio()
        {
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdRutinaEjercicio { get; set; }
        public int IdRutina { get; set; }  // FK: rutina.
        public int IdEjercicio { get; set; }  // FK: ejercicio del catálogo.
        public int? Series { get; set; }  // Cantidad de series (opcional).
        public int? Repeticiones { get; set; }  // Repeticiones por serie (opcional).

        [Column(TypeName = "decimal")]  // En SQL es DECIMAL: número exacto, sin errores de redondeo.
        public decimal? Peso { get; set; }  // Peso sugerido en kg (opcional).
        public int Descanso { get; set; }  // Descanso entre series, en segundos.
        public int Orden { get; set; }  // Posición dentro del día.
        public int? DiaSemana { get; set; }  // 1 = lunes ... 5 = viernes; NULL = sin día.
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).

        [ForeignKey("IdRutina")]  // Une esta navegación con la columna IdRutina (clave foránea).
        public virtual Rutina Rutina { get; set; }  // Navegación: objeto Rutina relacionado (no es columna; se carga con Include).

        [ForeignKey("IdEjercicio")]  // Une esta navegación con la columna IdEjercicio (clave foránea).
        public virtual Ejercicio Ejercicio { get; set; }  // Navegación: objeto Ejercicio relacionado (no es columna; se carga con Include).
    }
}
