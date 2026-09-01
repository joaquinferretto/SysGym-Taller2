using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaVisual.Dashboards
{
    public class DashboardRecepcionista : DashboardBase
    {
        public DashboardRecepcionista(UsuarioSistema usuario)
            : base(usuario, "SysGym - Recepcionista")
        {
            AgregarOpcion("Socios", delegate { MostrarModulo("Socios"); });
            AgregarOpcion("Membresías", delegate { MostrarModulo("Membresías"); });
            AgregarOpcion("Cuotas y pagos", delegate { MostrarModulo("Cuotas y pagos"); });
            AgregarOpcion("Asignar entrenador", delegate { MostrarModulo("Asignación de entrenador"); });
            AgregarOpcion("Consultar entrenador", delegate { MostrarModulo("Consulta de entrenador del socio"); });
            AgregarOpcion("Asistencias", delegate { MostrarModulo("Asistencias"); });
        }
    }
}
