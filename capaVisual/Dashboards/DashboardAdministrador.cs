using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaVisual.Dashboards
{
    public class DashboardAdministrador : DashboardBase
    {
        public DashboardAdministrador(UsuarioSistema usuario)
            : base(usuario, "SysGym - Administrador")
        {
            AgregarOpcion("Usuarios y roles", delegate { MostrarModulo("Usuarios y roles"); });
            AgregarOpcion("Socios", delegate { MostrarModulo("Socios"); });
            AgregarOpcion("Planes y membresías", delegate { MostrarModulo("Planes y membresías"); });
            AgregarOpcion("Pagos", delegate { MostrarModulo("Pagos"); });
            AgregarOpcion("Rutinas y ejercicios", delegate { MostrarModulo("Rutinas y ejercicios"); });
            AgregarOpcion("Reportes", delegate { MostrarModulo("Reportes"); });
        }
    }
}
