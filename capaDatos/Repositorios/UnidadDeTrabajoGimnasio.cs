using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaDatos.Repositorios
{
    /* Define las consultas y altas comunes de las entidades persistidas. */
    // Repositorio genérico: <T> es "cualquier entidad" (como List<T> en Java). "where T : class" exige que T sea una clase.
    // Hereda de IQueryable<T>, por eso sobre un repositorio se puede escribir LINQ directo: datos.Planes.Where(...).
    public interface IRepositorio<T> : IQueryable<T> where T : class
    {
        /* Prepara una consulta con seguimiento e incluye las relaciones solicitadas explícitamente. */
        IQueryable<T> Consultar(params string[] relaciones);  // params: se pasan varios textos sueltos, ej. Consultar("Socio", "Plan").
        /* Prepara una consulta sin seguimiento con las relaciones solicitadas, evitando cargas implícitas. */
        IQueryable<T> ConsultarSoloLectura(params string[] relaciones);
        /* Busca una entidad por su clave y la mantiene asociada a la unidad de trabajo para modificarla. */
        T Buscar(params object[] claves);
        /* Obtiene el único registro que cumple la condición o null si no existe. */
        T Primero(Expression<Func<T, bool>> condicion);  // La condición es una lambda (x => x.Id == 5) que EF traduce a WHERE.
        /* Comprueba si hay algún registro que cumpla la condición sin cargar toda la lista. */
        bool Existe(Expression<Func<T, bool>> condicion);
        /* Registra una entidad nueva para insertarla cuando se confirmen los cambios. */
        void Agregar(T entidad);
        /* Marca una entidad para eliminarla de forma explícita cuando el caso de uso lo requiere. */
        void Eliminar(T entidad);
    }

    /* Define la confirmación y liberación de una operación atómica. */
    // IDisposable: permite usarla dentro de "using (...)" para que se libere sola, como try-with-resources en Java.
    public interface ITransaccion : IDisposable
    {
        /* Confirma la transacción para conservar todos los cambios de la operación. */
        void Confirmar();
    }

    /* Define los repositorios y las operaciones de persistencia que utiliza la capa lógica. */
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

        IRepositorio<Rutina> Rutinas { get; }

        IRepositorio<RutinaEjercicio> RutinaEjercicios { get; }

        IRepositorio<Ejercicio> Ejercicios { get; }
        IRepositorio<EjercicioImagen> EjercicioImagenes { get; }

        /* Persiste los cambios y conserva la excepción original al informar errores de validación o actualización. */
        int GuardarCambios();
        /* Abre una transacción compartida por los repositorios de la unidad de trabajo. */
        ITransaccion IniciarTransaccion();
    }

    /* Comparte un contexto y una transacción entre los repositorios de una operación. */
    // Unidad de trabajo: UN contexto compartido por todos los repositorios de una operación. Así, lo que se agrega o
    // modifica en varios repositorios se guarda junto con un solo GuardarCambios(). La lógica la usa siempre así:
    //   using (var datos = new UnidadDeTrabajoGimnasio()) { datos.Planes.Agregar(plan); datos.GuardarCambios(); }
    public sealed class UnidadDeTrabajoGimnasio : IUnidadDeTrabajo
    {
        private readonly ContextoGimnasio contexto;
        /* Crea el contexto y los repositorios que comparten la persistencia de la operación. */
        public UnidadDeTrabajoGimnasio()
        {
            contexto = new ContextoGimnasio();  // Un solo contexto (una conexión) para toda la operación.
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
            Rutinas = CrearRepositorio<Rutina>();
            RutinaEjercicios = CrearRepositorio<RutinaEjercicio>();
            Ejercicios = CrearRepositorio<Ejercicio>();
            EjercicioImagenes = CrearRepositorio<EjercicioImagen>();
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
        public IRepositorio<Rutina> Rutinas { get; private set; }
        public IRepositorio<RutinaEjercicio> RutinaEjercicios { get; private set; }
        public IRepositorio<Ejercicio> Ejercicios { get; private set; }
        public IRepositorio<EjercicioImagen> EjercicioImagenes { get; private set; }

        /* Persiste los cambios y conserva la excepción original al informar errores de validación o actualización. */
        public int GuardarCambios()
        {
            try
            {
                return contexto.SaveChanges();  // Acá EF genera y ejecuta los INSERT, UPDATE y DELETE pendientes.
            }
            catch (DbEntityValidationException ex)  // Un dato no cumple [Required], [StringLength], etc.
            {
                var detalles = ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors).Select(e => e.PropertyName + ": " + e.ErrorMessage);
                throw new InvalidOperationException("No se pudieron guardar los datos: " + string.Join("; ", detalles), ex);
            }
            catch (DbUpdateException ex)  // SQL rechazó el cambio (FK, UNIQUE, CHECK...).
            {
                throw new InvalidOperationException("No se pudieron guardar los datos: " + ex.GetBaseException().Message, ex);
            }
        }

        /* Abre una transacción compartida por los repositorios de la unidad de trabajo. */
        public ITransaccion IniciarTransaccion()
        {
            return new Transaccion(contexto.Database.BeginTransaction());  // BEGIN TRANSACTION: "todo o nada".
        }

        /* Libera el contexto y sus recursos al finalizar la unidad de trabajo. */
        public void Dispose()
        {
            contexto.Dispose();  // Cierra la conexión con SQL Server.
        }

        /* Construye un repositorio usando el contexto compartido de la unidad de trabajo. */
        private IRepositorio<T> CrearRepositorio<T>()
            where T : class
        {
            return new Repositorio<T>(contexto);
        }

        /* Implementa las operaciones comunes de las entidades mediante el contexto compartido. */
        // Clase privada: solo la unidad de trabajo puede crear repositorios. sealed = no se puede heredar (final en Java).
        private sealed class Repositorio<T> : IRepositorio<T> where T : class
        {
            private readonly ContextoGimnasio contexto;
            /* Recibe el contexto compartido para consultar y guardar la entidad. */
            public Repositorio(ContextoGimnasio contexto)
            {
                this.contexto = contexto;
            }

            /* Prepara una consulta con seguimiento e incluye las relaciones solicitadas explícitamente. */
            public IQueryable<T> Consultar(params string[] relaciones)
            {
                IQueryable<T> consulta = contexto.Set<T>();  // Set<T>() = la tabla de esa entidad. Todavía no se ejecuta nada.
                if (relaciones == null)
                {
                    return consulta;
                }

                foreach (var relacion in relaciones)
                {
                    if (!string.IsNullOrWhiteSpace(relacion))
                    {
                        consulta = consulta.Include(relacion);  // Include = JOIN para traer también la entidad relacionada.
                    }
                }

                return consulta;
            }

            /* Busca una entidad por su clave y la mantiene asociada a la unidad de trabajo para modificarla. */
            public T Buscar(params object[] claves)
            {
                return contexto.Set<T>().Find(claves);  // Busca por clave primaria (PK); EF lo sigue para detectar cambios.
            }

            /* Prepara una consulta sin seguimiento con las relaciones solicitadas, evitando cargas implícitas. */
            public IQueryable<T> ConsultarSoloLectura(params string[] relaciones)
            {
                return Consultar(relaciones).AsNoTracking();  // AsNoTracking: solo lectura, EF no vigila cambios (más rápido para listados).
            }

            /* Obtiene el único registro que cumple la condición o null si no existe. */
            public T Primero(Expression<Func<T, bool>> condicion)
            {
                return Consultar().SingleOrDefault(condicion);  // Uno o null; si hay más de uno, lanza excepción.
            }

            /* Comprueba si hay algún registro que cumpla la condición sin cargar toda la lista. */
            public bool Existe(Expression<Func<T, bool>> condicion)
            {
                return Consultar().Any(condicion);  // SQL: SELECT CASE WHEN EXISTS(...).
            }

            /* Registra una entidad nueva para insertarla cuando se confirmen los cambios. */
            public void Agregar(T entidad)
            {
                contexto.Set<T>().Add(entidad);  // Queda pendiente; el INSERT ocurre en GuardarCambios().
            }

            /* Marca la entidad para borrarla (DELETE) al guardar; casi no se usa porque las bajas son lógicas (Estado = false). */
            public void Eliminar(T entidad)
            {
                contexto.Set<T>().Remove(entidad);
            }

            // ElementType, Expression y Provider son lo mínimo que pide IQueryable<T>: le pasan todo al DbSet para que LINQ funcione.
            public Type ElementType
            {
                /* Expone el tipo de entidad para componer consultas LINQ sobre el repositorio. */
                get
                {
                    return ((IQueryable<T>)contexto.Set<T>()).ElementType;
                }
            }

            public Expression Expression
            {
                /* Expone la expresión de consulta que Entity Framework traducirá a SQL. */
                get
                {
                    return ((IQueryable<T>)contexto.Set<T>()).Expression;
                }
            }

            public IQueryProvider Provider
            {
                /* Expone el proveedor de consultas de Entity Framework para ejecutar LINQ. */
                get
                {
                    return ((IQueryable<T>)contexto.Set<T>()).Provider;
                }
            }

            /* Permite recorrer los resultados de la consulta del repositorio. */
            public System.Collections.Generic.IEnumerator<T> GetEnumerator()
            {
                return ((IQueryable<T>)contexto.Set<T>()).GetEnumerator();
            }

            /* Permite recorrer los resultados de la consulta del repositorio. */
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /* Controla la confirmación o reversión de la transacción de Entity Framework. */
        private sealed class Transaccion : ITransaccion
        {
            private readonly DbContextTransaction transaccion;
            private bool confirmada;
            /* Recibe la transacción de Entity Framework que se confirmará o revertirá al finalizar. */
            public Transaccion(DbContextTransaction transaccion)
            {
                this.transaccion = transaccion;
            }

            /* Confirma la transacción para conservar todos los cambios de la operación. */
            public void Confirmar()
            {
                transaccion.Commit();  // COMMIT: se graba todo.
                confirmada = true;
            }

            /* Revierte la transacción si no fue confirmada y libera sus recursos. */
            public void Dispose()
            {
                if (!confirmada)
                {
                    transaccion.Rollback();  // ROLLBACK: si no se confirmó (hubo error), se deshace todo.
                }

                transaccion.Dispose();
            }
        }
    }
}
