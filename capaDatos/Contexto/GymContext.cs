using System.Data.Entity;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaDatos.Contexto
{
    public class GymContext : DbContext
    {
        static GymContext()
        {
            // La base se crea y versiona mediante el script SQL del proyecto.
            Database.SetInitializer<GymContext>(null);
        }

        public GymContext()
            : base("name=GymContext")
        {
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
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<RutinaEjercicio> RutinaEjercicios { get; set; }
        public DbSet<RutinaAsignacion> RutinaAsignaciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<UsuarioSistema>().ToTable("UsuarioSistema");
            modelBuilder.Entity<Socio>().ToTable("Socio");
            modelBuilder.Entity<Plan>().ToTable("Plan");
            modelBuilder.Entity<Membresia>().ToTable("Membresia");
            modelBuilder.Entity<CuotaMembresia>().ToTable("CuotaMembresia");
            modelBuilder.Entity<MembresiaEntrenador>().ToTable("MembresiaEntrenador");
            modelBuilder.Entity<Pago>().ToTable("Pago");
            modelBuilder.Entity<MetodoPago>().ToTable("MetodoPago");
            modelBuilder.Entity<MercadoPago>().ToTable("MercadoPago");
            modelBuilder.Entity<PagoEfectivo>().ToTable("PagoEfectivo");
            modelBuilder.Entity<Divisa>().ToTable("Divisa");
            modelBuilder.Entity<Asistencia>().ToTable("Asistencia");
            modelBuilder.Entity<Rutina>().ToTable("Rutina");
            modelBuilder.Entity<Ejercicio>().ToTable("Ejercicio");
            modelBuilder.Entity<RutinaEjercicio>().ToTable("RutinaEjercicio");
            modelBuilder.Entity<RutinaAsignacion>().ToTable("RutinaAsignacion");

            modelBuilder.Entity<Socio>().Property(s => s.Peso).HasPrecision(6, 2);
            modelBuilder.Entity<Socio>().Property(s => s.Altura).HasPrecision(5, 2);
            modelBuilder.Entity<UsuarioSistema>().Property(u => u.Salario).HasPrecision(18, 2);
            modelBuilder.Entity<Plan>().Property(p => p.Precio).HasPrecision(18, 2);
            modelBuilder.Entity<CuotaMembresia>().Property(c => c.Importe).HasPrecision(18, 2);
            modelBuilder.Entity<Pago>().Property(p => p.Importe).HasPrecision(18, 2);
            modelBuilder.Entity<Divisa>().Property(d => d.CambioHoy).HasPrecision(18, 2);
            modelBuilder.Entity<RutinaEjercicio>().Property(r => r.Peso).HasPrecision(8, 2);

            modelBuilder.Entity<UsuarioSistema>()
                .HasRequired(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.IdRol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Membresia>()
                .HasRequired(m => m.Plan)
                .WithMany(p => p.Membresias)
                .HasForeignKey(m => m.IdPlan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Membresia>()
                .HasRequired(m => m.Socio)
                .WithMany(s => s.Membresias)
                .HasForeignKey(m => m.IdSocio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Membresia>()
                .HasRequired(m => m.UsuarioSistema)
                .WithMany(u => u.MembresiasRegistradas)
                .HasForeignKey(m => m.IdUsuarioSistema)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MembresiaEntrenador>()
                .HasRequired(me => me.Membresia)
                .WithMany(m => m.Entrenadores)
                .HasForeignKey(me => me.IdMembresia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MembresiaEntrenador>()
                .HasRequired(me => me.Entrenador)
                .WithMany(u => u.MembresiasComoEntrenador)
                .HasForeignKey(me => me.IdEntrenador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CuotaMembresia>()
                .HasRequired(c => c.Membresia)
                .WithMany(m => m.Cuotas)
                .HasForeignKey(c => c.IdMembresia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CuotaMembresia>()
                .HasOptional(c => c.Pago)
                .WithMany(p => p.Cuotas)
                .HasForeignKey(c => c.IdRegistroPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pago>()
                .HasRequired(p => p.MetodoPago)
                .WithMany(mp => mp.Pagos)
                .HasForeignKey(p => p.IdMetodoPago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MetodoPago>()
                .HasOptional(mp => mp.MercadoPago)
                .WithMany(mp => mp.MetodosPago)
                .HasForeignKey(mp => mp.IdNroPagoMP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MetodoPago>()
                .HasOptional(mp => mp.PagoEfectivo)
                .WithMany(pe => pe.MetodosPago)
                .HasForeignKey(mp => mp.IdPagoEfectivo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PagoEfectivo>()
                .HasRequired(pe => pe.Divisa)
                .WithMany(d => d.PagosEfectivo)
                .HasForeignKey(pe => pe.IdDivisa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Asistencia>()
                .HasRequired(a => a.Socio)
                .WithMany(s => s.Asistencias)
                .HasForeignKey(a => a.IdSocio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rutina>()
                .HasRequired(r => r.Entrenador)
                .WithMany(u => u.RutinasComoEntrenador)
                .HasForeignKey(r => r.IdEntrenador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RutinaEjercicio>()
                .HasRequired(re => re.Rutina)
                .WithMany(r => r.Ejercicios)
                .HasForeignKey(re => re.IdRutina)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RutinaEjercicio>()
                .HasRequired(re => re.Ejercicio)
                .WithMany(e => e.Rutinas)
                .HasForeignKey(re => re.IdEjercicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RutinaAsignacion>()
                .HasRequired(ra => ra.Rutina)
                .WithMany(r => r.Asignaciones)
                .HasForeignKey(ra => ra.IdRutina)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RutinaAsignacion>()
                .HasRequired(ra => ra.Membresia)
                .WithMany(m => m.Rutinas)
                .HasForeignKey(ra => ra.IdMembresia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Plan>()
                .HasRequired(p => p.Rutina)
                .WithMany(r => r.Planes)
                .HasForeignKey(p => p.IdRutina)
                .WillCascadeOnDelete(false);
        }
    }
}
