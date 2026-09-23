using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    /* Representa membresías y sus relaciones persistidas en SQL Server. */
    // Entidad de EF6 (como @Entity en Java): cada objeto es una fila de la tabla Membresia y cada propiedad { get; set; } es una columna.
    public class Membresia
    {
        /* Inicializa los valores y colecciones necesarios para crear membresías. */
        public Membresia()
        {
            Entrenadores = new HashSet<MembresiaEntrenador>();  // Colección vacía para no trabajar con null.
            Cuotas = new HashSet<CuotaMembresia>();  // Colección vacía para no trabajar con null.
            Estado = true;
        }

        [Key]  // Clave primaria (PK): SQL la numera sola (IDENTITY).
        public int IdMembresia { get; set; }
        public DateTime FechaInicio { get; set; }  // Desde cuándo el socio tiene este plan (inicio de la 1.ª cuota).
        public DateTime FechaVencimiento { get; set; }  // Columna heredada: fin del primer mes. La cobertura real la dan las cuotas.
        public bool Estado { get; set; }  // true = activo; false = dado de baja (baja lógica, no se borra).
        public int IdPlan { get; set; }  // FK: plan contratado.
        public int IdSocio { get; set; }  // FK: socio titular.
        public int IdUsuarioSistema { get; set; }  // FK: empleado que registró la membresía.
        public int? IdRutina { get; set; }  // FK opcional: rutina asignada (int? = puede ser NULL).

        [ForeignKey("IdPlan")]  // Une esta navegación con la columna IdPlan (clave foránea).
        public virtual Plan Plan { get; set; }  // Navegación: objeto Plan relacionado (no es columna; se carga con Include).

        [ForeignKey("IdSocio")]  // Une esta navegación con la columna IdSocio (clave foránea).
        public virtual Socio Socio { get; set; }  // Navegación: objeto Socio relacionado (no es columna; se carga con Include).

        [ForeignKey("IdUsuarioSistema")]  // Une esta navegación con la columna IdUsuarioSistema (clave foránea).
        public virtual UsuarioSistema UsuarioSistema { get; set; }  // Navegación: objeto UsuarioSistema relacionado (no es columna; se carga con Include).
        [ForeignKey("IdRutina")]  // Une esta navegación con la columna IdRutina (clave foránea).
        public virtual Rutina Rutina { get; set; }  // Navegación: objeto Rutina relacionado (no es columna; se carga con Include).
        public virtual ICollection<CuotaMembresia> Cuotas { get; set; }  // Navegación: cuotas mensuales de esta membresía.
        public virtual ICollection<MembresiaEntrenador> Entrenadores { get; set; }  // Navegación: asignaciones de entrenador (actual e históricas).
    }
}
