using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace exxen2._0.capaLogica
{
    /* Transporta los datos de un día de pronóstico para presentarlos en el panel principal. */
    [DataContract]
    public sealed class PronosticoDia
    {
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public double TemperaturaMaxima { get; set; }
        [DataMember]
        public double TemperaturaMinima { get; set; }
        [DataMember]
        public int ProbabilidadLluvia { get; set; }
        [DataMember]
        public int CodigoClima { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Icono { get; set; }
    }

    /* Consulta y transforma el pronóstico semanal para la capa visual. */
    public class ClimaLogica
    {
        public const int DiasPronostico = 8;
        private const string UrlPronostico = "https://api.open-meteo.com/v1/forecast?latitude=-27.4692&longitude=-58.8306" + "&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max" + "&timezone=America%2FArgentina%2FBuenos_Aires&forecast_days=8";
        private static readonly string RutaCache = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SysGym", "pronostico-corrientes.json");
        private static readonly HttpClient Cliente = CrearCliente();
        public bool UltimaConsultaUsoCache { get; private set; }
        public string Ciudad
        {
            /* Indica la ciudad para la que se solicita el pronóstico configurado. */
            get
            {
                return "Corrientes, Corrientes";
            }
        }

        /* Consulta y transforma los siete días de pronóstico, conservando la causa si falla el servicio. */
        public async Task<List<PronosticoDia>> ObtenerPronosticoSemanalAsincrono()
        {
            try
            {
                var json = await Cliente.GetStringAsync(UrlPronostico).ConfigureAwait(false);
                RespuestaClima respuesta;
                using (var contenido = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var serializador = new DataContractJsonSerializer(typeof(RespuestaClima));
                    respuesta = (RespuestaClima)serializador.ReadObject(contenido);
                }
                ValidarRespuesta(respuesta);
                var dias = new List<PronosticoDia>();
                for (var indice = 0; indice < DiasPronostico; indice++)
                {
                    var codigo = respuesta.Dias.CodigosClima[indice];
                    dias.Add(new PronosticoDia { Fecha = DateTime.ParseExact(respuesta.Dias.Fechas[indice], "yyyy-MM-dd", CultureInfo.InvariantCulture), TemperaturaMaxima = respuesta.Dias.TemperaturasMaximas[indice], TemperaturaMinima = respuesta.Dias.TemperaturasMinimas[indice], ProbabilidadLluvia = respuesta.Dias.ProbabilidadesLluvia[indice], CodigoClima = codigo, Descripcion = DescribirClima(codigo), Icono = ObtenerIcono(codigo) });
                }

                GuardarCache(dias);
                UltimaConsultaUsoCache = false;
                return dias;
            }
            catch (Exception ex)
            {
                var respaldo = LeerCache();
                if (respaldo != null)
                {
                    UltimaConsultaUsoCache = true;
                    return respaldo;
                }
                UltimaConsultaUsoCache = false;
                throw new InvalidOperationException("No se pudo obtener el pronóstico semanal.", ex);
            }
        }

        /* Guarda el pronóstico descargado para poder mostrarlo sin conexión. */
        private static void GuardarCache(List<PronosticoDia> dias)
        {
            var directorio = Path.GetDirectoryName(RutaCache);
            var temporal = RutaCache + ".tmp";
            try
            {
                Directory.CreateDirectory(directorio);
                using (var archivo = File.Create(temporal))
                {
                    new DataContractJsonSerializer(typeof(List<PronosticoDia>)).WriteObject(archivo, dias);
                }
                File.Copy(temporal, RutaCache, true);
                File.Delete(temporal);
            }
            catch
            {
                try
                {
                    if (File.Exists(temporal))
                        File.Delete(temporal);
                }
                catch
                {
                }
            }
        }

        /* Lee el último pronóstico válido guardado en el equipo. */
        private static List<PronosticoDia> LeerCache()
        {
            try
            {
                if (!File.Exists(RutaCache))
                    return null;
                using (var archivo = File.OpenRead(RutaCache))
                {
                    var dias = new DataContractJsonSerializer(typeof(List<PronosticoDia>)).ReadObject(archivo) as List<PronosticoDia>;
                    return dias == null || dias.Count < DiasPronostico ? null : dias.GetRange(0, DiasPronostico);
                }
            }
            catch
            {
                return null;
            }
        }

        /* Prepara el cliente HTTP con proxy, TLS y tiempo de espera para consultar el clima. */
        private static HttpClient CrearCliente()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            var intermediario = WebRequest.DefaultWebProxy;
            if (intermediario != null)
            {
                intermediario.Credentials = CredentialCache.DefaultCredentials;
            }

            var manejador = new HttpClientHandler
            {
                Proxy = intermediario,
                UseProxy = intermediario != null
            };
            var cliente = new HttpClient(manejador)
            {
                Timeout = TimeSpan.FromSeconds(12)
            };
            cliente.DefaultRequestHeaders.UserAgent.ParseAdd("SysGym-WinForms/1.0");
            return cliente;
        }

        /* Rechaza respuestas del servicio que no contienen los siete días completos. */
        private static void ValidarRespuesta(RespuestaClima respuesta)
        {
            if (respuesta == null || respuesta.Dias == null || respuesta.Dias.Fechas == null || respuesta.Dias.Fechas.Length < DiasPronostico || respuesta.Dias.CodigosClima == null || respuesta.Dias.CodigosClima.Length < DiasPronostico || respuesta.Dias.TemperaturasMaximas == null || respuesta.Dias.TemperaturasMaximas.Length < DiasPronostico || respuesta.Dias.TemperaturasMinimas == null || respuesta.Dias.TemperaturasMinimas.Length < DiasPronostico || respuesta.Dias.ProbabilidadesLluvia == null || respuesta.Dias.ProbabilidadesLluvia.Length < DiasPronostico)
            {
                throw new InvalidOperationException("El servicio de clima devolvió datos incompletos.");
            }
        }

        /* Traduce el código meteorológico a la descripción usada por la aplicación. */
        private static string DescribirClima(int codigo)
        {
            if (codigo == 0)
                return "Despejado";
            if (codigo == 1 || codigo == 2)
                return "Parcial nublado";
            if (codigo == 3)
                return "Nublado";
            if (codigo == 45 || codigo == 48)
                return "Niebla";
            if (codigo >= 51 && codigo <= 57)
                return "Llovizna";
            if (codigo >= 61 && codigo <= 67)
                return "Lluvia";
            if (codigo >= 71 && codigo <= 77)
                return "Nieve";
            if (codigo >= 80 && codigo <= 82)
                return "Chaparrones";
            if (codigo >= 85 && codigo <= 86)
                return "Nieve intensa";
            if (codigo >= 95)
                return "Tormenta";
            return "Variable";
        }

        /* Selecciona el símbolo asociado al código del pronóstico. */
        private static string ObtenerIcono(int codigo)
        {
            if (codigo == 0)
                return "☀";
            if (codigo <= 2)
                return "⛅";
            if (codigo == 3)
                return "☁";
            if (codigo == 45 || codigo == 48)
                return "≋";
            if (codigo >= 71 && codigo <= 77)
                return "❄";
            if (codigo >= 85 && codigo <= 86)
                return "❄";
            if (codigo >= 95)
                return "⚡";
            return "☂";
        }

        /* Representa la respuesta JSON del servicio de pronóstico. */
        [DataContract]
        private sealed class RespuestaClima
        {
            [DataMember(Name = "daily")]
            public DatosDiarios Dias { get; set; }
        }

        /* Representa las series diarias recibidas del servicio meteorológico. */
        [DataContract]
        private sealed class DatosDiarios
        {
            [DataMember(Name = "time")]
            public string[] Fechas { get; set; }
            [DataMember(Name = "weather_code")]
            public int[] CodigosClima { get; set; }
            [DataMember(Name = "temperature_2m_max")]
            public double[] TemperaturasMaximas { get; set; }
            [DataMember(Name = "temperature_2m_min")]
            public double[] TemperaturasMinimas { get; set; }
            [DataMember(Name = "precipitation_probability_max")]
            public int[] ProbabilidadesLluvia { get; set; }
        }
    }
}
