using System;

namespace exxen2._0.capaLogica.Navegacion
{
    /// <summary>Identifica al socio cuyo estado de cuenta debe abrir el coordinador de navegación.</summary>
    public sealed class SocioEstadoCuentaEventArgs : EventArgs
    {
        public SocioEstadoCuentaEventArgs(int idSocio)
        {
            IdSocio = idSocio;
        }

        public int IdSocio { get; private set; }
    }
}
