using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaVisual.Dashboards
{
    public class DashboardEntrenador : DashboardBase
    {
        public DashboardEntrenador(UsuarioSistema usuario)
            : base(usuario, "SysGym - Entrenador")
        {
            AgregarOpcion("Mis socios", delegate { MostrarModulo("Mis socios asignados"); });
            AgregarOpcion("Rutinas", delegate { MostrarModulo("Rutinas"); });
            AgregarOpcion("Ejercicios", delegate { MostrarModulo("Ejercicios"); });
            AgregarOpcion("Asistencias", delegate { MostrarModulo("Asistencias"); });
        }
    }
}
