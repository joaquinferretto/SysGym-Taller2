using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace exxen2._0.capaLogica
{
    public sealed class PronosticoDia
    {
        public DateTime Fecha { get; set; }
        public double TemperaturaMaxima { get; set; }
        public double TemperaturaMinima { get; set; }
        public int ProbabilidadLluvia { get; set; }
        public int CodigoClima { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
    }

    public class ClimaLogica
    {
        private const string UrlPronostico =
            "https://api.open-meteo.com/v1/forecast?latitude=-34.6037&longitude=-58.3816"
            + "&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max"
            + "&timezone=America%2FArgentina%2FBuenos_Aires&forecast_days=7";

        private static readonly HttpClient Cliente = CrearCliente();

        public string Ciudad
        {
            get { return "Buenos Aires"; }
        }

        public async Task<List<PronosticoDia>> ObtenerPronosticoSemanalAsync()
        {
            try
            {
                var json = await Cliente.GetStringAsync(UrlPronostico).ConfigureAwait(false);
                var respuesta = new JavaScriptSerializer().Deserialize<RespuestaClima>(json);
                ValidarRespuesta(respuesta);

                var dias = new List<PronosticoDia>();
                for (var indice = 0; indice < 7; indice++)
                {
                    var codigo = respuesta.daily.weather_code[indice];
                    dias.Add(new PronosticoDia
                    {
                        Fecha = DateTime.ParseExact(respuesta.daily.time[indice], "yyyy-MM-dd",
                            CultureInfo.InvariantCulture),
                        TemperaturaMaxima = respuesta.daily.temperature_2m_max[indice],
                        TemperaturaMinima = respuesta.daily.temperature_2m_min[indice],
                        ProbabilidadLluvia = respuesta.daily.precipitation_probability_max[indice],
                        CodigoClima = codigo,
                        Descripcion = DescribirClima(codigo),
                        Icono = ObtenerIcono(codigo)
                    });
                }

                return dias;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se pudo obtener el pronóstico semanal.", ex);
            }
        }

        private static HttpClient CrearCliente()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            var proxy = WebRequest.DefaultWebProxy;
            if (proxy != null)
            {
                proxy.Credentials = CredentialCache.DefaultCredentials;
            }

            var manejador = new HttpClientHandler
            {
                Proxy = proxy,
                UseProxy = proxy != null
            };
            var cliente = new HttpClient(manejador) { Timeout = TimeSpan.FromSeconds(12) };
            cliente.DefaultRequestHeaders.UserAgent.ParseAdd("SysGym-WinForms/1.0");
            return cliente;
        }

        private static void ValidarRespuesta(RespuestaClima respuesta)
        {
            if (respuesta == null || respuesta.daily == null
                || respuesta.daily.time == null || respuesta.daily.time.Length < 7
                || respuesta.daily.weather_code == null || respuesta.daily.weather_code.Length < 7
                || respuesta.daily.temperature_2m_max == null || respuesta.daily.temperature_2m_max.Length < 7
                || respuesta.daily.temperature_2m_min == null || respuesta.daily.temperature_2m_min.Length < 7
                || respuesta.daily.precipitation_probability_max == null
                || respuesta.daily.precipitation_probability_max.Length < 7)
            {
                throw new InvalidOperationException("El servicio de clima devolvió datos incompletos.");
            }
        }

        private static string DescribirClima(int codigo)
        {
            if (codigo == 0) return "Despejado";
            if (codigo == 1 || codigo == 2) return "Parcial nublado";
            if (codigo == 3) return "Nublado";
            if (codigo == 45 || codigo == 48) return "Niebla";
            if (codigo >= 51 && codigo <= 57) return "Llovizna";
            if (codigo >= 61 && codigo <= 67) return "Lluvia";
            if (codigo >= 71 && codigo <= 77) return "Nieve";
            if (codigo >= 80 && codigo <= 82) return "Chaparrones";
            if (codigo >= 85 && codigo <= 86) return "Nieve intensa";
            if (codigo >= 95) return "Tormenta";
            return "Variable";
        }

        private static string ObtenerIcono(int codigo)
        {
            if (codigo == 0) return "☀";
            if (codigo <= 2) return "⛅";
            if (codigo == 3) return "☁";
            if (codigo == 45 || codigo == 48) return "≋";
            if (codigo >= 71 && codigo <= 77) return "❄";
            if (codigo >= 85 && codigo <= 86) return "❄";
            if (codigo >= 95) return "⚡";
            return "☂";
        }

        private sealed class RespuestaClima
        {
            public DatosDiarios daily { get; set; }
        }

        private sealed class DatosDiarios
        {
            public string[] time { get; set; }
            public int[] weather_code { get; set; }
            public double[] temperature_2m_max { get; set; }
            public double[] temperature_2m_min { get; set; }
            public int[] precipitation_probability_max { get; set; }
        }
    }
}
