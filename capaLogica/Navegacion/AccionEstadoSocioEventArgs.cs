using System;

namespace exxen2._0.capaLogica.Navegacion
{
    /// <summary>Transporta el socio y la acción solicitada sin depender de controles WinForms.</summary>
    public sealed class AccionEstadoSocioEventArgs : EventArgs
    {
        public AccionEstadoSocioEventArgs(int idSocio, AccionEstadoSocio accion) { IdSocio = idSocio; Accion = accion; }
        public int IdSocio { get; private set; }
        public AccionEstadoSocio Accion { get; private set; }
    }
}
