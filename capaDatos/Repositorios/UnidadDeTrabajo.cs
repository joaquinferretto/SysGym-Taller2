using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaDatos.Repositorios
{
    public interface IRepositorio<T> : IQueryable<T> where T : class
    {
        IQueryable<T> Consultar(params string[] relaciones);
        T Buscar(params object[] claves);
        T Find(params object[] claves);
        T Primero(Expression<Func<T, bool>> condicion);
        bool Existe(Expression<Func<T, bool>> condicion);
        void Agregar(T entidad);
        void Add(T entidad);
    }

    public interface ITransaccion : IDisposable
    {
        void Confirmar();
    }

    public interface IUnidadDeTrabajo : IDisposable
    {
        IRepositorio<Rol> Roles { get; }
        IRepositorio<UsuarioSistema> UsuariosSistema { get; }
        IRepositorio<Socio> Socios { get; }
        IRepositorio<Plan> Planes { get; }
        IRepositorio<Membresia> Membresias { get; }
        IRepositorio<CuotaMembresia> CuotasMembresia { get; }
        IRepositorio<MembresiaEntrenador> MembresiasEntrenadores { get; }
        IRepositorio<Pago> Pagos { get; }
        IRepositorio<MetodoPago> MetodosPago { get; }
        IRepositorio<MercadoPago> MercadosPago { get; }
        IRepositorio<PagoEfectivo> PagosEfectivo { get; }
        IRepositorio<Divisa> Divisas { get; }
        IRepositorio<Asistencia> Asistencias { get; }
        IRepositorio<Rutina> Rutinas { get; }
        IRepositorio<RutinaEjercicio> RutinaEjercicios { get; }
        IRepositorio<RutinaAsignacion> RutinaAsignaciones { get; }
        IRepositorio<Ejercicio> Ejercicios { get; }

        int GuardarCambios();
        ITransaccion IniciarTransaccion();
    }

    public sealed class GymUnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly GymContext contexto;

        public GymUnidadDeTrabajo()
        {
            contexto = new GymContext();
            Roles = CrearRepositorio<Rol>();
            UsuariosSistema = CrearRepositorio<UsuarioSistema>();
            Socios = CrearRepositorio<Socio>();
            Planes = CrearRepositorio<Plan>();
            Membresias = CrearRepositorio<Membresia>();
            CuotasMembresia = CrearRepositorio<CuotaMembresia>();
            MembresiasEntrenadores = CrearRepositorio<MembresiaEntrenador>();
            Pagos = CrearRepositorio<Pago>();
            MetodosPago = CrearRepositorio<MetodoPago>();
            MercadosPago = CrearRepositorio<MercadoPago>();
            PagosEfectivo = CrearRepositorio<PagoEfectivo>();
            Divisas = CrearRepositorio<Divisa>();
            Asistencias = CrearRepositorio<Asistencia>();
            Rutinas = CrearRepositorio<Rutina>();
            RutinaEjercicios = CrearRepositorio<RutinaEjercicio>();
            RutinaAsignaciones = CrearRepositorio<RutinaAsignacion>();
            Ejercicios = CrearRepositorio<Ejercicio>();
        }

        public IRepositorio<Rol> Roles { get; private set; }
        public IRepositorio<UsuarioSistema> UsuariosSistema { get; private set; }
        public IRepositorio<Socio> Socios { get; private set; }
        public IRepositorio<Plan> Planes { get; private set; }
        public IRepositorio<Membresia> Membresias { get; private set; }
        public IRepositorio<CuotaMembresia> CuotasMembresia { get; private set; }
        public IRepositorio<MembresiaEntrenador> MembresiasEntrenadores { get; private set; }
        public IRepositorio<Pago> Pagos { get; private set; }
        public IRepositorio<MetodoPago> MetodosPago { get; private set; }
        public IRepositorio<MercadoPago> MercadosPago { get; private set; }
        public IRepositorio<PagoEfectivo> PagosEfectivo { get; private set; }
        public IRepositorio<Divisa> Divisas { get; private set; }
        public IRepositorio<Asistencia> Asistencias { get; private set; }
        public IRepositorio<Rutina> Rutinas { get; private set; }
        public IRepositorio<RutinaEjercicio> RutinaEjercicios { get; private set; }
        public IRepositorio<RutinaAsignacion> RutinaAsignaciones { get; private set; }
        public IRepositorio<Ejercicio> Ejercicios { get; private set; }

        public int GuardarCambios()
        {
            return contexto.SaveChanges();
        }

        public ITransaccion IniciarTransaccion()
        {
            return new Transaccion(contexto.Database.BeginTransaction());
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        private IRepositorio<T> CrearRepositorio<T>() where T : class
        {
            return new Repositorio<T>(contexto);
        }

        private sealed class Repositorio<T> : IRepositorio<T> where T : class
        {
            private readonly GymContext contexto;

            public Repositorio(GymContext contexto)
            {
                this.contexto = contexto;
            }

            public IQueryable<T> Consultar(params string[] relaciones)
            {
                IQueryable<T> consulta = contexto.Set<T>();
                if (relaciones == null)
                {
                    return consulta;
                }

                foreach (var relacion in relaciones)
                {
                    if (!string.IsNullOrWhiteSpace(relacion))
                    {
                        consulta = consulta.Include(relacion);
                    }
                }

                return consulta;
            }

            public T Buscar(params object[] claves)
            {
                return contexto.Set<T>().Find(claves);
            }

            public T Find(params object[] claves)
            {
                return Buscar(claves);
            }

            public T Primero(Expression<Func<T, bool>> condicion)
            {
                return Consultar().SingleOrDefault(condicion);
            }

            public bool Existe(Expression<Func<T, bool>> condicion)
            {
                return Consultar().Any(condicion);
            }

            public void Agregar(T entidad)
            {
                contexto.Set<T>().Add(entidad);
            }

            public void Add(T entidad)
            {
                Agregar(entidad);
            }

            public Type ElementType
            {
                get { return ((IQueryable<T>)contexto.Set<T>()).ElementType; }
            }

            public Expression Expression
            {
                get { return ((IQueryable<T>)contexto.Set<T>()).Expression; }
            }

            public IQueryProvider Provider
            {
                get { return ((IQueryable<T>)contexto.Set<T>()).Provider; }
            }

            public System.Collections.Generic.IEnumerator<T> GetEnumerator()
            {
                return ((IQueryable<T>)contexto.Set<T>()).GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private sealed class Transaccion : ITransaccion
        {
            private readonly DbContextTransaction transaccion;
            private bool confirmada;

            public Transaccion(DbContextTransaction transaccion)
            {
                this.transaccion = transaccion;
            }

            public void Confirmar()
            {
                transaccion.Commit();
                confirmada = true;
            }

            public void Dispose()
            {
                if (!confirmada)
                {
                    transaccion.Rollback();
                }

                transaccion.Dispose();
            }
        }
    }
}
