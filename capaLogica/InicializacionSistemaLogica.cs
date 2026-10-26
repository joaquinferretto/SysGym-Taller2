using System;
using System.Linq;
using exxen2._0.capaDatos.Servicios;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina la comprobación del servidor y el primer inicio de SysGym. */
    public sealed class InicializacionSistemaLogica
    {
        public string NombreBaseConfigurada
        {
            get { return AdministracionBaseDatos.NombreBaseConfigurada; }
        }

        public void ValidarConexionServidor()
        {
            AdministracionBaseDatos.ValidarConexionServidor();
        }

        public bool ExisteBaseConfigurada()
        {
            return AdministracionBaseDatos.ExisteBaseConfigurada();
        }

        public void CrearBaseDesdeScript(string rutaScript)
        {
            AdministracionBaseDatos.CrearBaseDesdeScript(rutaScript);
        }

        public bool HayAdministradorActivo()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.UsuariosSistema.ConsultarSoloLectura("Rol")
                    .Any(u => u.Estado && u.Rol != null && u.Rol.Estado && u.Rol.Descripcion == "Administrador");
            }
        }
    }
}
