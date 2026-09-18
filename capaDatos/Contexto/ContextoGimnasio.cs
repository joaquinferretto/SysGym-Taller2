using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaDatos.Contexto
{
    /* Mapea las entidades del gimnasio a SQL Server mediante Entity Framework 6. */
    public class ContextoGimnasio : DbContext
    {
        private const string ConexionPrincipal = "GymContext";
        private const string ConexionRespaldo = "GymContextRespaldo";
        private static readonly object bloqueoConexion = new object();
        private static string conexionActiva;

        static ContextoGimnasio()
        {
            // La base se crea y versiona mediante el script SQL del proyecto.
            Database.SetInitializer<ContextoGimnasio>(null);
        }

        /* Configura el contexto para exigir relaciones explícitas. */
        public ContextoGimnasio() : base(ObtenerNombreConexionActiva())
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        /* Elige la conexion principal y usa la de respaldo solo si la instancia principal no responde. */
        private static string ObtenerNombreConexionActiva()
        {
            if (!string.IsNullOrEmpty(conexionActiva))
            {
                return "name=" + conexionActiva;
            }

            lock (bloqueoConexion)
            {
                if (!string.IsNullOrEmpty(conexionActiva))
                {
                    return "name=" + conexionActiva;
                }

                try
                {
                    ProbarConexion(ConexionPrincipal);
                    conexionActiva = ConexionPrincipal;
                }
                catch (SqlException ex) when (EsFallaDeServidorNoDisponible(ex))
                {
                    try
                    {
                        ProbarConexion(ConexionRespaldo);
                        conexionActiva = ConexionRespaldo;
                    }
                    catch (Exception exRespaldo)
                    {
                        throw new InvalidOperationException(
                            "No se pudo conectar a SQL Server con la conexion principal ni con la conexion de respaldo.",
                            new AggregateException(ex, exRespaldo));
                    }
                }

                return "name=" + conexionActiva;
            }
        }

        /* Abre y cierra una conexion para validar disponibilidad sin modificar la base. */
        private static void ProbarConexion(string nombreConexion)
        {
            var configuracion = ConfigurationManager.ConnectionStrings[nombreConexion];
            if (configuracion == null)
            {
                throw new InvalidOperationException("No existe la cadena de conexion '" + nombreConexion + "' en App.config.");
            }

            using (var conexion = new SqlConnection(configuracion.ConnectionString))
            {
                conexion.Open();
            }
        }

        /* Detecta errores de red, instancia o servidor no disponible; otros errores no activan respaldo. */
        private static bool EsFallaDeServidorNoDisponible(SqlException ex)
        {
            foreach (SqlError error in ex.Errors)
            {
                switch (error.Number)
                {
                    case -2:
                    case -1:
                    case 2:
                    case 26:
                    case 53:
                    case 10060:
                    case 10061:
                    case 11001:
                        return true;
                }
            }

            return false;
        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioSistema> UsuariosSistema { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<CuotaMembresia> CuotasMembresia { get; set; }
        public DbSet<MembresiaEntrenador> MembresiasEntrenadores { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<MercadoPago> MercadoPagos { get; set; }
        public DbSet<PagoEfectivo> PagosEfectivo { get; set; }
        public DbSet<Divisa> Divisas { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<RutinaEjercicio> RutinaEjercicios { get; set; }
        public DbSet<EjercicioImagen> EjercicioImagenes { get; set; }

        /* Configura tablas, tipos, precisión decimal y relaciones sin borrado en cascada. */
        protected override void OnModelCreating(DbModelBuilder modelo)
        {
            base.OnModelCreating(modelo);
            modelo.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();
            modelo.Properties<System.DateTime>().Configure(p => p.HasColumnType("datetime2"));
            modelo.Entity<Rol>().ToTable("Rol");
            modelo.Entity<UsuarioSistema>().ToTable("UsuarioSistema");
            modelo.Entity<Socio>().ToTable("Socio");
            modelo.Entity<Plan>().ToTable("Plan");
            modelo.Entity<Membresia>().ToTable("Membresia");
            modelo.Entity<CuotaMembresia>().ToTable("CuotaMembresia");
            modelo.Entity<MembresiaEntrenador>().ToTable("MembresiaEntrenador");
            modelo.Entity<Pago>().ToTable("Pago");
            modelo.Entity<MetodoPago>().ToTable("MetodoPago");
            modelo.Entity<MercadoPago>().ToTable("MercadoPago");
            modelo.Entity<PagoEfectivo>().ToTable("PagoEfectivo");
            modelo.Entity<Divisa>().ToTable("Divisa");
            modelo.Entity<Rutina>().ToTable("Rutina");
            modelo.Entity<Ejercicio>().ToTable("Ejercicio");
            modelo.Entity<RutinaEjercicio>().ToTable("RutinaEjercicio");
            modelo.Entity<EjercicioImagen>().ToTable("EjercicioImagen");
            modelo.Entity<Socio>().Property(s => s.Peso).HasPrecision(6, 2);
            modelo.Entity<Socio>().Property(s => s.Altura).HasPrecision(5, 2);
            modelo.Entity<UsuarioSistema>().Property(u => u.Salario).HasPrecision(18, 2);
            modelo.Entity<Plan>().Property(p => p.Precio).HasPrecision(18, 2);
            modelo.Entity<CuotaMembresia>().Property(c => c.Importe).HasPrecision(18, 2);
            modelo.Entity<Pago>().Property(p => p.Importe).HasPrecision(18, 2);
            modelo.Entity<Divisa>().Property(d => d.CambioHoy).HasPrecision(18, 2);
            modelo.Entity<RutinaEjercicio>().Property(r => r.Peso).HasPrecision(8, 2);
            modelo.Entity<UsuarioSistema>().HasRequired(u => u.Rol).WithMany(r => r.Usuarios).HasForeignKey(u => u.IdRol).WillCascadeOnDelete(false);
            modelo.Entity<Membresia>().HasRequired(m => m.Plan).WithMany(p => p.Membresias).HasForeignKey(m => m.IdPlan).WillCascadeOnDelete(false);
            modelo.Entity<Membresia>().HasRequired(m => m.Socio).WithMany(s => s.Membresias).HasForeignKey(m => m.IdSocio).WillCascadeOnDelete(false);
            modelo.Entity<Membresia>().HasRequired(m => m.UsuarioSistema).WithMany(u => u.MembresiasRegistradas).HasForeignKey(m => m.IdUsuarioSistema).WillCascadeOnDelete(false);
            modelo.Entity<Membresia>().HasOptional(m => m.Rutina).WithMany(r => r.Membresias).HasForeignKey(m => m.IdRutina).WillCascadeOnDelete(false);
            modelo.Entity<MembresiaEntrenador>().HasRequired(me => me.Membresia).WithMany(m => m.Entrenadores).HasForeignKey(me => me.IdMembresia).WillCascadeOnDelete(false);
            modelo.Entity<MembresiaEntrenador>().HasRequired(me => me.Entrenador).WithMany(u => u.MembresiasComoEntrenador).HasForeignKey(me => me.IdEntrenador).WillCascadeOnDelete(false);
            modelo.Entity<CuotaMembresia>().HasRequired(c => c.Membresia).WithMany(m => m.Cuotas).HasForeignKey(c => c.IdMembresia).WillCascadeOnDelete(false);
            modelo.Entity<CuotaMembresia>().HasOptional(c => c.Pago).WithMany(p => p.Cuotas).HasForeignKey(c => c.IdRegistroPago).WillCascadeOnDelete(false);
            modelo.Entity<Pago>().HasRequired(p => p.MetodoPago).WithMany(mp => mp.Pagos).HasForeignKey(p => p.IdMetodoPago).WillCascadeOnDelete(false);
            modelo.Entity<MetodoPago>().HasOptional(mp => mp.MercadoPago).WithMany(mp => mp.MetodosPago).HasForeignKey(mp => mp.IdNroPagoMP).WillCascadeOnDelete(false);
            modelo.Entity<MetodoPago>().HasOptional(mp => mp.PagoEfectivo).WithMany(pe => pe.MetodosPago).HasForeignKey(mp => mp.IdPagoEfectivo).WillCascadeOnDelete(false);
            modelo.Entity<PagoEfectivo>().HasRequired(pe => pe.Divisa).WithMany(d => d.PagosEfectivo).HasForeignKey(pe => pe.IdDivisa).WillCascadeOnDelete(false);
            modelo.Entity<Rutina>().HasRequired(r => r.Entrenador).WithMany(u => u.RutinasComoEntrenador).HasForeignKey(r => r.IdEntrenador).WillCascadeOnDelete(false);
            modelo.Entity<RutinaEjercicio>().HasRequired(re => re.Rutina).WithMany(r => r.Ejercicios).HasForeignKey(re => re.IdRutina).WillCascadeOnDelete(false);
            modelo.Entity<RutinaEjercicio>().HasRequired(re => re.Ejercicio).WithMany(e => e.Rutinas).HasForeignKey(re => re.IdEjercicio).WillCascadeOnDelete(false);
            modelo.Entity<EjercicioImagen>().HasRequired(i => i.Ejercicio).WithMany(e => e.EjercicioImagenes).HasForeignKey(i => i.IdEjercicio).WillCascadeOnDelete(false);
        }
    }
}
