using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class SocioLogica
    {
        public Socio Crear(Socio socio)
        {
            ValidarDatos(socio);
            using (var context = new GymUnidadDeTrabajo())
            {
                if (context.Socios.Any(s => s.DNI == socio.DNI))
                {
                    throw new InvalidOperationException("El DNI ya está registrado.");
                }

                socio.Estado = true;
                context.Socios.Add(socio);
                context.GuardarCambios();
                return socio;
            }
        }

        public Socio Modificar(Socio socio)
        {
            ValidarDatos(socio);
            using (var context = new GymUnidadDeTrabajo())
            {
                var existente = context.Socios.Find(socio.IdSocio);
                if (existente == null)
                {
                    throw new InvalidOperationException("El socio no existe.");
                }

                if (context.Socios.Any(s => s.DNI == socio.DNI && s.IdSocio != socio.IdSocio))
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
                context.GuardarCambios();
                return existente;
            }
        }

        public Socio ObtenerPorId(int idSocio)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Socios.Consultar("Membresias")
                    .SingleOrDefault(s => s.IdSocio == idSocio);
            }
        }

        public Socio ObtenerPorDni(string dni)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Socios.SingleOrDefault(s => s.DNI == dni);
            }
        }

        public List<Socio> ListarActivos()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Socios.Where(s => s.Estado)
                    .OrderBy(s => s.Apellido).ThenBy(s => s.Nombre).ToList();
            }
        }

        public List<Socio> ListarParaGestion()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Socios
                    .OrderByDescending(s => s.Estado)
                    .ThenBy(s => s.Apellido).ThenBy(s => s.Nombre).ToList();
            }
        }

        public void DarDeBaja(int idSocio)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var socio = context.Socios.Find(idSocio);
                if (socio == null)
                {
                    throw new InvalidOperationException("El socio no existe.");
                }

                socio.Estado = false;
                context.GuardarCambios();
            }
        }

        public void Reactivar(int idSocio)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var socio = context.Socios.Find(idSocio);
                if (socio == null)
                {
                    throw new InvalidOperationException("El socio no existe.");
                }

                socio.Estado = true;
                context.GuardarCambios();
            }
        }

        public decimal CalcularIMC(Socio socio)
        {
            if (socio == null || !socio.Peso.HasValue || !socio.Altura.HasValue
                || socio.Peso.Value <= 0 || socio.Altura.Value <= 0)
            {
                throw new InvalidOperationException("Peso y altura positivos son necesarios para calcular el IMC.");
            }

            ValidarAlturaFraccionaria(socio.Altura);

            return Math.Round(socio.Peso.Value / (socio.Altura.Value * socio.Altura.Value), 2);
        }

        public decimal CalcularIMC(int idSocio)
        {
            var socio = ObtenerPorId(idSocio);
            return CalcularIMC(socio);
        }

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
        }

        private static void ValidarAlturaFraccionaria(decimal? altura)
        {
            if (altura.HasValue && decimal.Truncate(altura.Value) == altura.Value)
            {
                throw new InvalidOperationException(
                    "La altura debe incluir decimales, por ejemplo 1,80 m.");
            }
        }
    }
}
