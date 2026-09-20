using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Imazen.WebP;

namespace exxen2._0.capaLogica
{
    /* Resultado inmutable de decodificar y normalizar una imagen externa. */
    public sealed class ImagenNormalizada
    {
        internal ImagenNormalizada(byte[] contenido, string extension, string formatoOrigen, int anchoOriginal, int altoOriginal)
        {
            Contenido = contenido;
            Extension = extension;
            FormatoOrigen = formatoOrigen;
            AnchoOriginal = anchoOriginal;
            AltoOriginal = altoOriginal;
        }

        public byte[] Contenido { get; private set; }
        public string Extension { get; private set; }
        public string FormatoOrigen { get; private set; }
        public int AnchoOriginal { get; private set; }
        public int AltoOriginal { get; private set; }
    }

    /* Aplica una única política segura a toda imagen externa usada por SysGym. */
    public static class ProcesadorImagenes
    {
        public const int AnchoNormalizado = 800;
        public const int AltoNormalizado = 800;
        public const int TamanoMaximoMb = 10;
        public const int TamanoMaximoBytes = TamanoMaximoMb * 1024 * 1024;
        public const int MaxAnchoOriginal = 12000;
        public const int MaxAltoOriginal = 12000;
        public const long MaxPixeles = 40000000L;
        public const int MaxImagenesPorEjercicio = 4;
        public const long CalidadJpeg = 90L;

        private const int IdOrientacionExif = 0x0112;

        /* Valida el contenido real y devuelve un JPEG nuevo de 800x800 . */
        public static ImagenNormalizada Procesar(byte[] contenido)
        {
            ValidarTamano(contenido);
            var formato = DetectarFormato(contenido);
            ValidarDimensionesDeclaradas(contenido, formato);

            try
            {
                using (var decodificada = Decodificar(contenido, formato))
                {
                    ValidarDimensiones(decodificada.Width, decodificada.Height);
                    var preservarTransparencia = TienePixelesTransparentes(decodificada);
                    using (var lienzo = CrearLienzo(decodificada, preservarTransparencia))
                    using (var memoria = new MemoryStream())
                    {
                        var extension = ".png";
                        if (preservarTransparencia)
                            lienzo.Save(memoria, ImageFormat.Png);
                        else
                        {
                            extension = ".jpg";
                            using (var parametros = new EncoderParameters(1))
                            {
                                parametros.Param[0] = new EncoderParameter(Encoder.Quality, CalidadJpeg);
                                lienzo.Save(memoria, ObtenerCodificadorJpeg(), parametros);
                            }
                        }
                        return new ImagenNormalizada(memoria.ToArray(), extension, NombreFormato(formato), decodificada.Width, decodificada.Height);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (ArgumentException excepcion)
            {
                throw ImagenInvalida(excepcion);
            }
            catch (OutOfMemoryException excepcion)
            {
                throw ImagenInvalida(excepcion);
            }
            catch (ExternalException excepcion)
            {
                throw ImagenInvalida(excepcion);
            }
        }

        /* Ejecuta exactamente el mismo proceso y entrega bytes seguros para la vista previa. */
        public static byte[] ProcesarParaVistaPrevia(byte[] contenido)
        {
            return Procesar(contenido).Contenido;
        }

        /* Valida completamente sin persistir ni conservar el archivo normalizado. */
        public static void Validar(byte[] contenido)
        {
            Procesar(contenido);
        }

        private static void ValidarTamano(byte[] contenido)
        {
            if (contenido == null || contenido.Length == 0)
                throw new InvalidOperationException("Seleccione un archivo de imagen válido.");
            if (contenido.Length > TamanoMaximoBytes)
                throw new InvalidOperationException("La imagen supera el tamaño máximo permitido de 10 MB.");
        }

        private static FormatoImagen DetectarFormato(byte[] contenido)
        {
            if (EmpiezaCon(contenido, 0xFF, 0xD8, 0xFF))
                return FormatoImagen.Jpeg;
            if (EmpiezaCon(contenido, 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A))
                return FormatoImagen.Png;
            if (EmpiezaCon(contenido, 0x47, 0x49, 0x46, 0x38) && contenido.Length >= 6 && (contenido[4] == 0x37 || contenido[4] == 0x39) && contenido[5] == 0x61)
                return FormatoImagen.Gif;
            if (EmpiezaCon(contenido, 0x42, 0x4D))
                return FormatoImagen.Bmp;
            if (EmpiezaCon(contenido, 0x49, 0x49, 0x2A, 0x00) || EmpiezaCon(contenido, 0x4D, 0x4D, 0x00, 0x2A))
                return FormatoImagen.Tiff;
            if (contenido.Length >= 12 && EmpiezaCon(contenido, 0x52, 0x49, 0x46, 0x46) && contenido[8] == 0x57 && contenido[9] == 0x45 && contenido[10] == 0x42 && contenido[11] == 0x50)
                return FormatoImagen.WebP;

            throw new InvalidOperationException("El formato de esta imagen no puede procesarse.");
        }

        private static bool EmpiezaCon(byte[] contenido, params byte[] firma)
        {
            if (contenido.Length < firma.Length)
                return false;
            for (var indice = 0; indice < firma.Length; indice++)
                if (contenido[indice] != firma[indice])
                    return false;
            return true;
        }

        private static Bitmap Decodificar(byte[] contenido, FormatoImagen formato)
        {
            if (formato == FormatoImagen.WebP)
            {
                int ancho;
                int alto;
                if (!WebPInfo.TryGetSize(contenido, out ancho, out alto))
                    throw new InvalidOperationException("La imagen está dañada o incompleta.");
                ValidarDimensiones(ancho, alto);
                var decodificador = new SimpleDecoder();
                var bitmapWebP = decodificador.DecodeFromBytes(contenido, contenido.LongLength);
                if (bitmapWebP == null)
                    throw new InvalidOperationException("La imagen está dañada o incompleta.");
                AplicarOrientacion(bitmapWebP, ObtenerOrientacionWebP(contenido));
                return bitmapWebP;
            }

            using (var memoria = new MemoryStream(contenido, false))
            using (var original = Image.FromStream(memoria, true, true))
            {
                SeleccionarPrimerCuadro(original, formato);
                CorregirOrientacion(original);
                ValidarDimensiones(original.Width, original.Height);
                return new Bitmap(original);
            }
        }

        private static void ValidarDimensionesDeclaradas(byte[] contenido, FormatoImagen formato)
        {
            int ancho;
            int alto;
            var encontradas = false;
            switch (formato)
            {
                case FormatoImagen.Png:
                    encontradas = contenido.Length >= 24;
                    ancho = encontradas ? LeerInt32BigEndian(contenido, 16) : 0;
                    alto = encontradas ? LeerInt32BigEndian(contenido, 20) : 0;
                    break;
                case FormatoImagen.Gif:
                    encontradas = contenido.Length >= 10;
                    ancho = encontradas ? LeerUInt16(contenido, 6, true) : 0;
                    alto = encontradas ? LeerUInt16(contenido, 8, true) : 0;
                    break;
                case FormatoImagen.Bmp:
                    encontradas = contenido.Length >= 26;
                    var anchoBmp = encontradas ? (long)BitConverter.ToInt32(contenido, 18) : 0;
                    var altoBmp = encontradas ? (long)BitConverter.ToInt32(contenido, 22) : 0;
                    ancho = anchoBmp == int.MinValue ? 0 : (int)Math.Abs(anchoBmp);
                    alto = altoBmp == int.MinValue ? 0 : (int)Math.Abs(altoBmp);
                    break;
                case FormatoImagen.Jpeg:
                    encontradas = IntentarLeerDimensionesJpeg(contenido, out ancho, out alto);
                    break;
                case FormatoImagen.Tiff:
                    encontradas = IntentarLeerDimensionesTiff(contenido, out ancho, out alto);
                    break;
                case FormatoImagen.WebP:
                    encontradas = WebPInfo.TryGetSize(contenido, out ancho, out alto);
                    break;
                default:
                    ancho = 0;
                    alto = 0;
                    break;
            }

            if (!encontradas)
                throw new InvalidOperationException("La imagen está dañada o incompleta.");
            ValidarDimensiones(ancho, alto);
        }

        private static bool IntentarLeerDimensionesJpeg(byte[] datos, out int ancho, out int alto)
        {
            ancho = 0;
            alto = 0;
            var posicion = 2;
            while (posicion + 3 < datos.Length)
            {
                if (datos[posicion] != 0xFF)
                {
                    posicion++;
                    continue;
                }
                while (posicion < datos.Length && datos[posicion] == 0xFF)
                    posicion++;
                if (posicion >= datos.Length)
                    return false;
                var marcador = datos[posicion++];
                if (marcador == 0xD8 || marcador == 0xD9 || (marcador >= 0xD0 && marcador <= 0xD7))
                    continue;
                if (posicion + 1 >= datos.Length)
                    return false;
                var longitud = datos[posicion] << 8 | datos[posicion + 1];
                if (longitud < 2 || posicion + longitud > datos.Length)
                    return false;
                if (EsMarcadorSof(marcador) && longitud >= 7)
                {
                    alto = datos[posicion + 3] << 8 | datos[posicion + 4];
                    ancho = datos[posicion + 5] << 8 | datos[posicion + 6];
                    return true;
                }
                posicion += longitud;
            }
            return false;
        }

        private static bool EsMarcadorSof(byte marcador)
        {
            return marcador >= 0xC0 && marcador <= 0xCF && marcador != 0xC4 && marcador != 0xC8 && marcador != 0xCC;
        }

        private static bool IntentarLeerDimensionesTiff(byte[] datos, out int ancho, out int alto)
        {
            ancho = 0;
            alto = 0;
            if (datos.Length < 8)
                return false;
            var littleEndian = datos[0] == 0x49 && datos[1] == 0x49;
            if (!littleEndian && !(datos[0] == 0x4D && datos[1] == 0x4D))
                return false;
            var ifd = (long)LeerUInt32(datos, 4, littleEndian);
            if (ifd < 0 || ifd + 2 > datos.Length)
                return false;
            var cantidad = LeerUInt16(datos, (int)ifd, littleEndian);
            for (var indice = 0; indice < cantidad; indice++)
            {
                var entrada = ifd + 2 + indice * 12L;
                if (entrada + 12 > datos.Length)
                    return false;
                var etiqueta = LeerUInt16(datos, (int)entrada, littleEndian);
                if (etiqueta != 256 && etiqueta != 257)
                    continue;
                var tipo = LeerUInt16(datos, (int)entrada + 2, littleEndian);
                var cantidadValores = LeerUInt32(datos, (int)entrada + 4, littleEndian);
                if (cantidadValores != 1 || (tipo != 3 && tipo != 4))
                    return false;
                var valor = tipo == 3 ? LeerUInt16(datos, (int)entrada + 8, littleEndian) : LeerUInt32(datos, (int)entrada + 8, littleEndian);
                if (valor > int.MaxValue)
                    return false;
                if (etiqueta == 256)
                    ancho = (int)valor;
                else
                    alto = (int)valor;
            }
            return ancho > 0 && alto > 0;
        }

        private static int LeerInt32BigEndian(byte[] datos, int posicion)
        {
            var valor = (uint)(datos[posicion] << 24 | datos[posicion + 1] << 16 | datos[posicion + 2] << 8 | datos[posicion + 3]);
            return valor > int.MaxValue ? 0 : (int)valor;
        }

        private static void SeleccionarPrimerCuadro(Image imagen, FormatoImagen formato)
        {
            if (formato != FormatoImagen.Gif && formato != FormatoImagen.Tiff)
                return;
            var dimension = formato == FormatoImagen.Gif ? FrameDimension.Time : FrameDimension.Page;
            if (imagen.GetFrameCount(dimension) > 0)
                imagen.SelectActiveFrame(dimension, 0);
        }

        private static void CorregirOrientacion(Image imagen)
        {
            if (!imagen.PropertyIdList.Contains(IdOrientacionExif))
                return;

            try
            {
                var propiedad = imagen.GetPropertyItem(IdOrientacionExif);
                if (propiedad == null || propiedad.Value == null || propiedad.Value.Length < 2)
                    return;
                AplicarOrientacion(imagen, BitConverter.ToUInt16(propiedad.Value, 0));
            }
            catch (ArgumentException)
            {
                // Una etiqueta EXIF defectuosa no invalida píxeles que sí pudieron decodificarse.
            }
        }

        private static void AplicarOrientacion(Image imagen, int orientacion)
        {
            var giro = RotateFlipType.RotateNoneFlipNone;
            switch (orientacion)
            {
                case 2: giro = RotateFlipType.RotateNoneFlipX; break;
                case 3: giro = RotateFlipType.Rotate180FlipNone; break;
                case 4: giro = RotateFlipType.Rotate180FlipX; break;
                case 5: giro = RotateFlipType.Rotate90FlipX; break;
                case 6: giro = RotateFlipType.Rotate90FlipNone; break;
                case 7: giro = RotateFlipType.Rotate270FlipX; break;
                case 8: giro = RotateFlipType.Rotate270FlipNone; break;
            }
            if (giro != RotateFlipType.RotateNoneFlipNone)
                imagen.RotateFlip(giro);
        }

        /* WebP puede incluir un bloque EXIF; se lee solo la orientación y se descarta el resto. */
        private static int ObtenerOrientacionWebP(byte[] contenido)
        {
            var posicion = 12;
            while (posicion + 8 <= contenido.Length)
            {
                var longitud = BitConverter.ToUInt32(contenido, posicion + 4);
                var inicio = posicion + 8;
                if (longitud > int.MaxValue || inicio + (long)longitud > contenido.Length)
                    return 1;
                if (contenido[posicion] == 0x45 && contenido[posicion + 1] == 0x58 && contenido[posicion + 2] == 0x49 && contenido[posicion + 3] == 0x46)
                    return LeerOrientacionTiff(contenido, inicio, (int)longitud);
                posicion = inicio + (int)longitud + ((longitud & 1) == 1 ? 1 : 0);
            }
            return 1;
        }

        private static int LeerOrientacionTiff(byte[] datos, int inicio, int longitud)
        {
            if (longitud >= 6 && datos[inicio] == 0x45 && datos[inicio + 1] == 0x78 && datos[inicio + 2] == 0x69 && datos[inicio + 3] == 0x66)
            {
                inicio += 6;
                longitud -= 6;
            }
            if (longitud < 8)
                return 1;
            var littleEndian = datos[inicio] == 0x49 && datos[inicio + 1] == 0x49;
            if (!littleEndian && !(datos[inicio] == 0x4D && datos[inicio + 1] == 0x4D))
                return 1;
            var desplazamientoIfd = LeerUInt32(datos, inicio + 4, littleEndian);
            var ifd = inicio + (long)desplazamientoIfd;
            var fin = inicio + (long)longitud;
            if (ifd < inicio || ifd + 2 > fin)
                return 1;
            var cantidad = LeerUInt16(datos, (int)ifd, littleEndian);
            for (var indice = 0; indice < cantidad; indice++)
            {
                var entrada = ifd + 2 + indice * 12L;
                if (entrada + 12 > fin)
                    return 1;
                if (LeerUInt16(datos, (int)entrada, littleEndian) == IdOrientacionExif)
                    return LeerUInt16(datos, (int)entrada + 8, littleEndian);
            }
            return 1;
        }

        private static ushort LeerUInt16(byte[] datos, int posicion, bool littleEndian)
        {
            return littleEndian
                ? (ushort)(datos[posicion] | datos[posicion + 1] << 8)
                : (ushort)(datos[posicion] << 8 | datos[posicion + 1]);
        }

        private static uint LeerUInt32(byte[] datos, int posicion, bool littleEndian)
        {
            if (littleEndian)
                return (uint)(datos[posicion] | datos[posicion + 1] << 8 | datos[posicion + 2] << 16 | datos[posicion + 3] << 24);
            return (uint)(datos[posicion] << 24 | datos[posicion + 1] << 16 | datos[posicion + 2] << 8 | datos[posicion + 3]);
        }

        private static void ValidarDimensiones(int ancho, int alto)
        {
            if (ancho <= 0 || alto <= 0)
                throw new InvalidOperationException("La imagen está dañada o incompleta.");
            if (ancho > MaxAnchoOriginal || alto > MaxAltoOriginal || (long)ancho * alto > MaxPixeles)
                throw new InvalidOperationException("La imagen supera la resolución máxima permitida de 40 megapíxeles.");
        }

        private static bool TienePixelesTransparentes(Image imagen)
        {
            if (!Image.IsAlphaPixelFormat(imagen.PixelFormat) && (imagen.Flags & (int)ImageFlags.HasAlpha) == 0 && (imagen.Flags & (int)ImageFlags.HasTranslucent) == 0)
                return false;

            int ancho;
            int alto;
            CalcularTamanoContenido(imagen, out ancho, out alto);
            using (var muestra = new Bitmap(ancho, alto, PixelFormat.Format32bppArgb))
            {
                using (var grafico = Graphics.FromImage(muestra))
                using (var atributos = new ImageAttributes())
                {
                    ConfigurarCalidad(grafico);
                    grafico.CompositingMode = CompositingMode.SourceCopy;
                    grafico.Clear(Color.Transparent);
                    atributos.SetWrapMode(WrapMode.TileFlipXY);
                    grafico.DrawImage(imagen, new Rectangle(0, 0, ancho, alto), 0, 0, imagen.Width, imagen.Height, GraphicsUnit.Pixel, atributos);
                }
                var area = new Rectangle(0, 0, ancho, alto);
                var datos = muestra.LockBits(area, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                try
                {
                    var fila = new byte[Math.Abs(datos.Stride)];
                    for (var y = 0; y < alto; y++)
                    {
                        Marshal.Copy(IntPtr.Add(datos.Scan0, y * datos.Stride), fila, 0, fila.Length);
                        for (var x = 3; x < ancho * 4; x += 4)
                            if (fila[x] < 255)
                                return true;
                    }
                }
                finally
                {
                    muestra.UnlockBits(datos);
                }
            }
            return false;
        }

        private static Bitmap CrearLienzo(Image imagen, bool transparente)
        {
            var formato = transparente ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb;
            var lienzo = new Bitmap(AnchoNormalizado, AltoNormalizado, formato);
            lienzo.SetResolution(96, 96);
            using (var grafico = Graphics.FromImage(lienzo))
            {
                ConfigurarCalidad(grafico);
                grafico.Clear(transparente ? Color.Transparent : Color.White);
                int ancho;
                int alto;
                CalcularTamanoContenido(imagen, out ancho, out alto);
                var x = (AnchoNormalizado - ancho) / 2;
                var y = (AltoNormalizado - alto) / 2;
                grafico.DrawImage(imagen, new Rectangle(x, y, ancho, alto), 0, 0, imagen.Width, imagen.Height, GraphicsUnit.Pixel);
            }
            return lienzo;
        }

        private static void CalcularTamanoContenido(Image imagen, out int ancho, out int alto)
        {
            var escala = Math.Min(1d, Math.Min((double)AnchoNormalizado / imagen.Width, (double)AltoNormalizado / imagen.Height));
            ancho = Math.Max(1, (int)Math.Round(imagen.Width * escala));
            alto = Math.Max(1, (int)Math.Round(imagen.Height * escala));
        }

        private static void ConfigurarCalidad(Graphics grafico)
        {
            grafico.CompositingMode = CompositingMode.SourceOver;
            grafico.CompositingQuality = CompositingQuality.HighQuality;
            grafico.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grafico.SmoothingMode = SmoothingMode.HighQuality;
            grafico.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        private static ImageCodecInfo ObtenerCodificadorJpeg()
        {
            var codificador = ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
            if (codificador == null)
                throw new InvalidOperationException("No se encontró el codificador JPEG del sistema.");
            return codificador;
        }

        private static InvalidOperationException ImagenInvalida(Exception excepcion)
        {
            return new InvalidOperationException("La imagen está dañada, incompleta o no puede decodificarse.", excepcion);
        }

        private static string NombreFormato(FormatoImagen formato)
        {
            return formato == FormatoImagen.WebP ? "WebP" : formato.ToString().ToUpperInvariant();
        }

        private enum FormatoImagen
        {
            Jpeg,
            Png,
            Bmp,
            Gif,
            Tiff,
            WebP
        }
    }
}
