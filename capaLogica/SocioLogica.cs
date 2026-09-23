using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de socios. */
    // Reglas de los socios: DNI único, datos válidos, foto guardada como archivo e IMC.
    // La usan GestionSociosFormulario y ReportesFormulario.
    public class SocioLogica
    {
        /* Valida y registra socios mediante la unidad de trabajo, conservando sus reglas de alta. */
        public Socio Crear(Socio socio)
        {
            return Crear(socio, null);
        }

        /* Valida, copia la foto seleccionada a Datos y registra solo su ruta relativa. */
        public Socio Crear(Socio socio, byte[] fotoContenido)
        {
            ValidarDatos(socio);
            var rutaNueva = (string)null;
            try
            {
                using (var datos = new UnidadDeTrabajoGimnasio())  // Abre la conexión; al salir del bloque se cierra sola, aunque haya error (try-with-resources).
                {
                    if (datos.Socios.Any(s => s.DNI == socio.DNI))  // ¿Existe al menos uno? (no trae filas).
                        throw new InvalidOperationException("El DNI ya está registrado.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.

                    if (fotoContenido != null)
                        rutaNueva = AlmacenamientoImagenes.GuardarSocio(fotoContenido);
                    socio.FotoRuta = rutaNueva ?? NormalizarRuta(socio.FotoRuta);  // ??: si lo de la izquierda es null, usa lo de la derecha.
                    socio.Estado = true;
                    datos.Socios.Agregar(socio);  // Deja el objeto listo para INSERT (se ejecuta en GuardarCambios).
                    datos.GuardarCambios();  // EF envía a SQL los INSERT/UPDATE pendientes.
                    return socio;
                }
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(rutaNueva))
                    AlmacenamientoImagenes.Eliminar(rutaNueva);
                throw;
            }
        }

        /* Valida y guarda los cambios de socios sobre el registro existente. */
        public Socio Modificar(Socio socio)
        {
            return Modificar(socio, null);
        }

        /* Actualiza el socio y reemplaza o quita su foto sin guardar binarios en SQL. */
        public Socio Modificar(Socio socio, byte[] fotoContenido)
        {
            ValidarDatos(socio);
            var rutaNueva = (string)null;
            var guardado = false;
            try
            {
                using (var datos = new UnidadDeTrabajoGimnasio())
                {
                    var existente = datos.Socios.Buscar(socio.IdSocio);  // Busca por clave primaria; si no existe devuelve null.
                    if (existente == null)
                        throw new InvalidOperationException("El socio no existe.");

                    if (datos.Socios.Any(s => s.DNI == socio.DNI && s.IdSocio != socio.IdSocio))
                        throw new InvalidOperationException("El DNI ya está registrado.");

                    var rutaAnterior = existente.FotoRuta;
                    if (fotoContenido != null)
                        rutaNueva = AlmacenamientoImagenes.GuardarSocio(fotoContenido);

                    existente.DNI = socio.DNI;
                    existente.Nombre = socio.Nombre;
                    existente.Apellido = socio.Apellido;
                    existente.FechaNacimiento = socio.FechaNacimiento;
                    existente.Peso = socio.Peso;
                    existente.Altura = socio.Altura;
                    existente.FotoRuta = rutaNueva ?? NormalizarRuta(socio.FotoRuta);
                    existente.Sexo = socio.Sexo;
                    datos.GuardarCambios();
                    guardado = true;

                    if (!string.IsNullOrWhiteSpace(rutaAnterior) && rutaAnterior != existente.FotoRuta && !datos.Socios.Existe(s => s.FotoRuta == rutaAnterior && s.IdSocio != socio.IdSocio))
                        AlmacenamientoImagenes.Eliminar(rutaAnterior);
                    return existente;
                }
            }
            catch
            {
                if (!guardado && !string.IsNullOrWhiteSpace(rutaNueva))
                    AlmacenamientoImagenes.Eliminar(rutaNueva);
                throw;
            }
        }

        /* Busca el registro de socios por identificador y devuelve los datos disponibles. */
        public Socio ObtenerPorId(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                return datos.Socios.ConsultarSoloLectura("Membresias").SingleOrDefault(s => s.IdSocio == idSocio);  // Solo lectura: EF no vigila cambios (más liviano para listar).
            }
        }

        /* Busca el registro de socios por DNI y devuelve los datos disponibles. */
        public Socio ObtenerPorDni(string dni)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                return datos.Socios.ConsultarSoloLectura().SingleOrDefault(s => s.DNI == dni);  // Devuelve el único que cumple o null.
            }
        }

        /* Consulta socios activos para devolver los datos a la capa visual. */
        public List<Socio> ListarActivos()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                return ListarSinFotos(datos.Socios.ConsultarSoloLectura().Where(s => s.Estado).OrderBy(s => s.Apellido).ThenBy(s => s.Nombre));  // Where = filtro (como filter de Streams / WHERE de SQL). "x => ..." es una lambda.
            }
        }

        /* Consulta socios activos e inactivos para su gestión para devolver los datos a la capa visual. */
        public List<Socio> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                return ListarSinFotos(datos.Socios.ConsultarSoloLectura().OrderByDescending(s => s.Estado).ThenBy(s => s.Apellido).ThenBy(s => s.Nombre));  // Ordena (ORDER BY).
            }
        }

        /* Proyecta solo datos del listado en SQL, sin transferir los binarios de las fotos. */
        private static List<Socio> ListarSinFotos(IQueryable<Socio> consulta)
        {
            return consulta.Select(s => new  // Select = transforma cada elemento (como map de Streams).
            {
                s.IdSocio, s.DNI, s.Nombre, s.Apellido, s.FechaNacimiento,
                s.Peso, s.Altura, s.Estado, s.Sexo
            }).ToList().Select(s => new Socio  // Acá se ejecuta la consulta en SQL y se trae la lista.
            {
                IdSocio = s.IdSocio, DNI = s.DNI, Nombre = s.Nombre, Apellido = s.Apellido,
                FechaNacimiento = s.FechaNacimiento, Peso = s.Peso, Altura = s.Altura,
                Estado = s.Estado, Sexo = s.Sexo
            }).ToList();
        }

        /* Baja lógica del socio: queda inactivo junto con su membresía activa; no se borran cuotas, pagos ni rutinas. */
        public void DarDeBaja(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var socio = datos.Socios.Buscar(idSocio);
                if (socio == null)
                    throw new InvalidOperationException("El socio no existe.");
                if (!socio.Estado)
                    throw new InvalidOperationException("El socio ya está dado de baja.");

                socio.Estado = false;
                // Un socio inactivo no puede conservar una membresía activa.
                foreach (var membresia in datos.Membresias.Where(m => m.IdSocio == idSocio && m.Estado).ToList())
                    membresia.Estado = false;
                datos.GuardarCambios();
                transaccion.Confirmar();
            }
        }

        /* Reactiva al socio; su membresía se reactiva aparte desde Membresías, donde se controla la deuda. */
        public void Reactivar(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var socio = datos.Socios.Buscar(idSocio);
                if (socio == null)
                    throw new InvalidOperationException("El socio no existe.");
                if (socio.Estado)
                    throw new InvalidOperationException("El socio ya está activo.");

                socio.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Calcula el índice a partir del peso y la altura registrados, validando los datos requeridos. */
        public decimal CalcularIMC(Socio socio)
        {
            if (socio == null || !socio.Peso.HasValue || !socio.Altura.HasValue || socio.Peso.Value <= 0 || socio.Altura.Value <= 0)  // HasValue: ¿el valor opcional (int?, DateTime?) tiene dato?
            {
                throw new InvalidOperationException("Peso y altura positivos son necesarios para calcular el IMC.");
            }

            ValidarAlturaFraccionaria(socio.Altura);
            return Math.Round(socio.Peso.Value / (socio.Altura.Value * socio.Altura.Value), 2);  // IMC = peso (kg) / altura (m) al cuadrado, redondeado a 2 decimales.
        }

        /* Calcula el índice a partir del peso y la altura registrados, validando los datos requeridos. */
        public decimal CalcularIMC(int idSocio)
        {
            var socio = ObtenerPorId(idSocio);
            return CalcularIMC(socio);
        }

        /* Comprueba los campos y rangos obligatorios de socios antes de persistirlos. */
        private static void ValidarDatos(Socio socio)
        {
            if (socio == null)
            {
                throw new ArgumentNullException("socio");  // Se recibió null donde no corresponde.
            }

            ValidacionesGimnasio.ValidarDni(socio.DNI);
            ValidacionesGimnasio.ValidarNombre(socio.Nombre, "nombre");
            ValidacionesGimnasio.ValidarNombre(socio.Apellido, "apellido");
            ValidacionesGimnasio.ValidarEdadMinima(socio.FechaNacimiento, 13, "El socio debe tener al menos 13 años.");

            if (socio.Peso.HasValue && socio.Peso.Value <= 0)
            {
                throw new InvalidOperationException("El peso debe ser mayor que cero.");
            }

            if (socio.Altura.HasValue && socio.Altura.Value <= 0)
            {
                throw new InvalidOperationException("La altura debe ser mayor que cero.");
            }

            ValidarAlturaFraccionaria(socio.Altura);
            ValidacionesGimnasio.ValidarFotoYSexo(null, socio.Sexo);
            AlmacenamientoImagenes.ValidarRutaRelativa(socio.FotoRuta);
        }

        /* Convierte rutas vacías provenientes de formularios o integraciones en NULL de base. */
        private static string NormalizarRuta(string ruta)
        {
            return string.IsNullOrWhiteSpace(ruta) ? null : ruta.Trim();
        }

        /* Exige que la altura en metros tenga parte decimal según la regla del proyecto. */
        private static void ValidarAlturaFraccionaria(decimal? altura)
        {
            if (altura.HasValue && decimal.Truncate(altura.Value) == altura.Value)
            {
                throw new InvalidOperationException("La altura debe incluir decimales, por ejemplo 1,80 m.");
            }
        }
    }
}
