using System;

namespace exxen2._0.capaLogica.Navegacion
{
    /// <summary>Notifica qué usuario cambió para coordinar la actualización de la sesión existente.</summary>
    public sealed class UsuarioActualizadoEventArgs : EventArgs
    {
        public UsuarioActualizadoEventArgs(int idUsuarioSistema)
        {
            IdUsuarioSistema = idUsuarioSistema;
        }

        public int IdUsuarioSistema { get; private set; }
    }
}
