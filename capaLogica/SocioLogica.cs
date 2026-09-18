using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de socios. */
    public class SocioLogica
    {
        /* Valida y registra socios mediante la unidad de trabajo, conservando sus reglas de alta. */
        public Socio Crear(Socio socio)
        {
            ValidarDatos(socio);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                if (datos.Socios.Any(s => s.DNI == socio.DNI))
                {
                    throw new InvalidOperationException("El DNI ya está registrado.");
                }

                socio.Estado = true;
                datos.Socios.Agregar(socio);
                datos.GuardarCambios();
                return socio;
            }
        }

        /* Valida y guarda los cambios de socios sobre el registro existente. */
        public Socio Modificar(Socio socio)
        {
            ValidarDatos(socio);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.Socios.Buscar(socio.IdSocio);
                if (existente == null)
                {
                    throw new InvalidOperationException("El socio no existe.");
                }

                if (datos.Socios.Any(s => s.DNI == socio.DNI && s.IdSocio != socio.IdSocio))
                {
                    throw new InvalidOperationException("El DNI ya está registrado.");
                }

                existente.DNI = socio.DNI;
                existente.Nombre = socio.Nombre;
                existente.Apellido = socio.Apellido;
                existente.FechaNacimiento = socio.FechaNacimiento;
                existente.Peso = socio.Peso;
                existente.Altura = socio.Altura;
                existente.Estado = socio.Estado;
                existente.Foto = socio.Foto;
                existente.Sexo = socio.Sexo;
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca el registro de socios por identificador y devuelve los datos disponibles. */
        public Socio ObtenerPorId(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Socios.ConsultarSoloLectura("Membresias").SingleOrDefault(s => s.IdSocio == idSocio);
            }
        }

        /* Busca el registro de socios por DNI y devuelve los datos disponibles. */
        public Socio ObtenerPorDni(string dni)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Socios.ConsultarSoloLectura().SingleOrDefault(s => s.DNI == dni);
            }
        }

        /* Consulta socios activos para devolver los datos a la capa visual. */
        public List<Socio> ListarActivos()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return ListarSinFotos(datos.Socios.ConsultarSoloLectura().Where(s => s.Estado).OrderBy(s => s.Apellido).ThenBy(s => s.Nombre));
            }
        }

        /* Consulta socios activos e inactivos para su gestión para devolver los datos a la capa visual. */
        public List<Socio> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return ListarSinFotos(datos.Socios.ConsultarSoloLectura().OrderByDescending(s => s.Estado).ThenBy(s => s.Apellido).ThenBy(s => s.Nombre));
            }
        }

        /* Proyecta solo datos del listado en SQL, sin transferir los binarios de las fotos. */
        private static List<Socio> ListarSinFotos(IQueryable<Socio> consulta)
        {
            return consulta.Select(s => new
            {
                s.IdSocio, s.DNI, s.Nombre, s.Apellido, s.FechaNacimiento,
                s.Peso, s.Altura, s.Estado, s.Sexo
            }).ToList().Select(s => new Socio
            {
                IdSocio = s.IdSocio, DNI = s.DNI, Nombre = s.Nombre, Apellido = s.Apellido,
                FechaNacimiento = s.FechaNacimiento, Peso = s.Peso, Altura = s.Altura,
                Estado = s.Estado, Sexo = s.Sexo
            }).ToList();
        }

        /* Desactiva el registro de socios sin eliminar su historial. */
        public void DarDeBaja(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var socio = datos.Socios.Buscar(idSocio);
                if (socio == null)
                {
                    throw new InvalidOperationException("El socio no existe.");
                }

                socio.Estado = false;
                datos.GuardarCambios();
            }
        }

        /* Recupera el estado activo del registro de socios según las validaciones de la operación. */
        public void Reactivar(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var socio = datos.Socios.Buscar(idSocio);
                if (socio == null)
                {
                    throw new InvalidOperationException("El socio no existe.");
                }

                socio.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Calcula el índice a partir del peso y la altura registrados, validando los datos requeridos. */
        public decimal CalcularIMC(Socio socio)
        {
            if (socio == null || !socio.Peso.HasValue || !socio.Altura.HasValue || socio.Peso.Value <= 0 || socio.Altura.Value <= 0)
            {
                throw new InvalidOperationException("Peso y altura positivos son necesarios para calcular el IMC.");
            }

            ValidarAlturaFraccionaria(socio.Altura);
            return Math.Round(socio.Peso.Value / (socio.Altura.Value * socio.Altura.Value), 2);
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
                throw new ArgumentNullException("socio");
            }

            if (string.IsNullOrWhiteSpace(socio.DNI))
            {
                throw new InvalidOperationException("El DNI es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(socio.Nombre) || string.IsNullOrWhiteSpace(socio.Apellido))
            {
                throw new InvalidOperationException("Nombre y apellido son obligatorios.");
            }

            if (socio.Peso.HasValue && socio.Peso.Value <= 0)
            {
                throw new InvalidOperationException("El peso debe ser mayor que cero.");
            }

            if (socio.Altura.HasValue && socio.Altura.Value <= 0)
            {
                throw new InvalidOperationException("La altura debe ser mayor que cero.");
            }

            ValidarAlturaFraccionaria(socio.Altura);
            ValidacionesGimnasio.ValidarFotoYSexo(socio.Foto, socio.Sexo);
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
