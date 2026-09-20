using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public sealed class EntrenadorConsultaItem
    {
        public int IdEntrenador { get; set; }
        public string NombreCompleto { get; set; }
        public string DNI { get; set; }
        public bool Estado { get; set; }
        public int CantidadSocios { get; set; }
    }

    public sealed class SocioAsignadoConsultaItem
    {
        public string NombreCompleto { get; set; }
        public string DNI { get; set; }
        public string Plan { get; set; }
        public DateTime Vencimiento { get; set; }
        public string Rutina { get; set; }
        public bool MembresiaActiva { get; set; }
    }

    /* Proporciona la consulta de entrenadores y sus socios sin modificar estados. */
    public sealed class ConsultaEntrenadoresLogica
    {
        public List<EntrenadorConsultaItem> ListarEntrenadores()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.UsuariosSistema
                    .ConsultarSoloLectura("Rol", "MembresiasComoEntrenador.Membresia.Socio")
                    .Where(u => u.Rol.Descripcion == "Entrenador")
                    .OrderBy(u => u.Apellido).ThenBy(u => u.Nombre)
                    .ToList()
                    .Select(u => new EntrenadorConsultaItem
                    {
                        IdEntrenador = u.IdUsuarioSistema,
                        NombreCompleto = u.Apellido + ", " + u.Nombre,
                        DNI = u.DNI,
                        Estado = u.Estado,
                        CantidadSocios = u.MembresiasComoEntrenador.Count(a => a.Estado &&
                            a.Membresia != null && a.Membresia.Socio != null)
                    }).ToList();
            }
        }

        public List<SocioAsignadoConsultaItem> ListarSocios(int idEntrenador)
        {
            if (idEntrenador <= 0)
                throw new InvalidOperationException("Seleccione un entrenador.");

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.MembresiasEntrenadores
                    .ConsultarSoloLectura("Membresia.Socio", "Membresia.Plan", "Membresia.Rutina")
                    .Where(a => a.IdEntrenador == idEntrenador && a.Estado && a.Membresia.Socio != null)
                    .OrderBy(a => a.Membresia.Socio.Apellido).ThenBy(a => a.Membresia.Socio.Nombre)
                    .ToList()
                    .Select(a => new SocioAsignadoConsultaItem
                    {
                        NombreCompleto = a.Membresia.Socio.Apellido + ", " + a.Membresia.Socio.Nombre,
                        DNI = a.Membresia.Socio.DNI,
                        Plan = a.Membresia.Plan == null ? "-" : a.Membresia.Plan.Nombre,
                        Vencimiento = a.Membresia.FechaVencimiento,
                        Rutina = a.Membresia.Rutina == null ? "Sin rutina" : a.Membresia.Rutina.Nombre,
                        MembresiaActiva = a.Membresia.Estado
                    }).ToList();
            }
        }
    }
}
