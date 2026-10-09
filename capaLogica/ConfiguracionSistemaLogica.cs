using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;
using exxen2._0.capaLogica.Auditoria;

namespace exxen2._0.capaLogica
{
    public sealed class ConfiguracionSistemaLogica
    {
        private readonly AuditoriaLogica auditoria;
        public ConfiguracionSistemaLogica() : this(0) { }
        public ConfiguracionSistemaLogica(int idUsuarioAutenticado)
        {
            auditoria = new AuditoriaLogica(idUsuarioAutenticado);
        }

        public void ValidarAcceso() { auditoria.ValidarAcceso(); }

        public ConfiguracionSistema Obtener()
        {
            using (var datos = new UnidadDeTrabajoGimnasio()) return ObtenerEnContexto(datos);
        }

        // Sin caché de sesión: cada caso de uso lee los valores persistidos actuales.
        internal static ConfiguracionSistema ObtenerEnContexto(IUnidadDeTrabajo datos)
        {
            var configuracion = datos.ConfiguracionesSistema.ConsultarSoloLectura()
                .SingleOrDefault(c => c.IdConfiguracion == 1);
            if (configuracion == null)
                throw new InvalidOperationException("No se encontró la configuración del sistema. Contactá al administrador.");
            return configuracion;
        }

        public bool Guardar(int maxVencidas, int maxAnticipacion, int diasAviso)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                auditoria.ObtenerUsuario(datos, true);
                if (maxVencidas < 1 || maxVencidas > 120 || maxAnticipacion < 0 || maxAnticipacion > 120 || diasAviso < 0 || diasAviso > 365)
                    throw new ArgumentException("Revisá los valores: las cuotas vencidas deben estar entre 1 y 120, la anticipación entre 0 y 120 meses y los días de aviso entre 0 y 365.");
                var configuracion = datos.ConfiguracionesSistema.Buscar(1);
                if (configuracion == null) throw new InvalidOperationException("No se encontró la configuración del sistema. Contactá al administrador.");
                var cambios = new List<string>();
                if (configuracion.MaxCuotasVencidasPermitidas != maxVencidas)
                    cambios.Add("Límite de cuotas vencidas cambiado de " + configuracion.MaxCuotasVencidasPermitidas + " a " + maxVencidas);
                if (configuracion.MaxMesesAnticipacionCuotas != maxAnticipacion)
                    cambios.Add("Meses máximos de anticipación cambiado de " + configuracion.MaxMesesAnticipacionCuotas + " a " + maxAnticipacion);
                if (configuracion.DiasAvisoVencimiento != diasAviso)
                    cambios.Add("Días de aviso de vencimiento cambiado de " + configuracion.DiasAvisoVencimiento + " a " + diasAviso);
                if (cambios.Count == 0) return false;
                configuracion.MaxCuotasVencidasPermitidas = maxVencidas;
                configuracion.MaxMesesAnticipacionCuotas = maxAnticipacion;
                configuracion.DiasAvisoVencimiento = diasAviso;
                auditoria.RegistrarOperacion(datos, AuditoriaLogica.ModificarConfiguracion, "ConfiguracionSistema", 1, string.Join("; ", cambios));
                datos.GuardarCambios();
                transaccion.Confirmar();
                return true;
            }
        }

        // Período mensual actual y N posteriores, conservando la secuencia real incluso en meses cortos.
        public static DateTime CalcularHorizonteCuotas(DateTime primeraFechaDesde, DateTime hoy, int mesesAnticipacion)
        {
            if (mesesAnticipacion < 0 || mesesAnticipacion > 120) throw new ArgumentOutOfRangeException("mesesAnticipacion");
            var desde = primeraFechaDesde.Date;
            while (CuotaMembresiaLogica.CalcularPeriodoHasta(desde) < hoy.Date)
                desde = CuotaMembresiaLogica.CalcularPeriodoHasta(desde).AddDays(1);
            for (var i = 0; i < mesesAnticipacion; i++)
                desde = CuotaMembresiaLogica.CalcularPeriodoHasta(desde).AddDays(1);
            return CuotaMembresiaLogica.CalcularPeriodoHasta(desde);
        }
    }
}
