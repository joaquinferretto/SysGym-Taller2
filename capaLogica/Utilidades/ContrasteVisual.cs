using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace exxen2._0.capaLogica.Utilidades
{
    /* Mantiene legible el texto de los botones: fondo oscuro → letras blancas, fondo claro → letras oscuras. */
    // Se aplica sobre el panel principal y sobre cada diálogo: recorre sus controles y también los que se agreguen
    // después (módulos MDI, inicio, tarjetas creadas por código). Solo corrige el color cuando no se lee bien, así que
    // respeta los colores de la paleta que ya tienen buen contraste (por ejemplo, rojo sobre rosa en "Dar de baja").
    internal static class ContrasteVisual
    {
        private const double ContrasteMinimo = 4.5;  // Mínimo recomendado para texto normal (WCAG AA).
        private static readonly Color TextoClaro = Color.White;
        private static readonly Color TextoOscuro = Color.FromArgb(15, 23, 42);
        private static readonly Color TextoInactivoOscuro = Color.FromArgb(71, 85, 105);
        private static readonly object Marca = new object();
        // Recuerda qué controles ya se prepararon sin impedir que se liberen (tabla débil).
        private static readonly ConditionalWeakTable<Control, object> preparados = new ConditionalWeakTable<Control, object>();

        /* Prepara el control, todos sus hijos y los controles que se le agreguen más adelante. */
        internal static void Aplicar(Control raiz)
        {
            if (raiz == null)
                return;
            object marca;
            if (!preparados.TryGetValue(raiz, out marca))
            {
                preparados.Add(raiz, Marca);
                raiz.ControlAdded += (origen, e) => Aplicar(e.Control);
                var boton = raiz as Button;
                if (boton != null && !boton.UseVisualStyleBackColor)
                    PrepararBoton(boton);
            }

            foreach (Control hijo in raiz.Controls)
                Aplicar(hijo);
        }

        /* Indica si sobre ese fondo se lee mejor el texto blanco que el oscuro. */
        internal static bool EsOscuro(Color fondo)
        {
            return Contraste(TextoClaro, fondo) >= Contraste(TextoOscuro, fondo);
        }

        /* Devuelve blanco para fondos oscuros y casi negro para fondos claros. */
        internal static Color TextoLegible(Color fondo)
        {
            return EsOscuro(fondo) ? TextoClaro : TextoOscuro;
        }

        /* Relación de contraste entre dos colores: 1 = iguales, 21 = negro sobre blanco. */
        internal static double Contraste(Color a, Color b)
        {
            var la = Luminancia(a);
            var lb = Luminancia(b);
            return (Math.Max(la, lb) + 0.05) / (Math.Min(la, lb) + 0.05);
        }

        /* Corrige el texto del botón ahora y cada vez que cambie su fondo, y dibuja su estado deshabilitado. */
        private static void PrepararBoton(Button boton)
        {
            AjustarTexto(boton);
            boton.BackColorChanged += (origen, e) => AjustarTexto(boton);
            boton.Paint += boton_Paint;
        }

        /* Cambia el color de letra solo si el actual no alcanza el contraste mínimo con el fondo. */
        private static void AjustarTexto(Button boton)
        {
            if (Contraste(boton.ForeColor, boton.BackColor) < ContrasteMinimo)
                boton.ForeColor = TextoLegible(boton.BackColor);
        }

        /* Windows ignora ForeColor en un botón deshabilitado y lo pinta gris, ilegible sobre fondos oscuros.
           Se redibuja siguiendo la misma regla: un fondo fuerte (verde, morado) se aclara apenas y conserva la letra
           blanca; un fondo claro se aclara un poco más y lleva letra oscura atenuada. */
        private static void boton_Paint(object origen, PaintEventArgs e)
        {
            var boton = (Button)origen;
            if (boton.Enabled || boton.Image != null || boton.BackgroundImage != null)
                return;
            var oscuro = EsOscuro(boton.BackColor);
            var fondo = Mezclar(boton.BackColor, Color.White, oscuro ? 0.15 : 0.7);
            var texto = oscuro ? TextoClaro : TextoInactivoOscuro;
            using (var pincel = new SolidBrush(fondo))
                e.Graphics.FillRectangle(pincel, boton.ClientRectangle);
            var borde = boton.FlatAppearance.BorderSize;
            if (boton.FlatStyle == FlatStyle.Flat && borde > 0 && !boton.FlatAppearance.BorderColor.IsEmpty)
            {
                using (var lapiz = new Pen(Mezclar(boton.FlatAppearance.BorderColor, Color.White, 0.7), borde))
                {
                    lapiz.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    e.Graphics.DrawRectangle(lapiz, 0, 0, boton.Width - 1, boton.Height - 1);
                }
            }

            var area = new Rectangle(boton.Padding.Left, boton.Padding.Top,
                boton.ClientSize.Width - boton.Padding.Horizontal, boton.ClientSize.Height - boton.Padding.Vertical);
            TextRenderer.DrawText(e.Graphics, boton.Text, boton.Font, area, texto,
                Alineacion(boton.TextAlign) | TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
        }

        /* Traduce la alineación del botón al formato que usa TextRenderer. */
        private static TextFormatFlags Alineacion(ContentAlignment alineacion)
        {
            var formato = TextFormatFlags.Default;
            switch (alineacion)
            {
                case ContentAlignment.TopLeft: case ContentAlignment.TopCenter: case ContentAlignment.TopRight:
                    formato |= TextFormatFlags.Top; break;
                case ContentAlignment.BottomLeft: case ContentAlignment.BottomCenter: case ContentAlignment.BottomRight:
                    formato |= TextFormatFlags.Bottom; break;
                default:
                    formato |= TextFormatFlags.VerticalCenter; break;
            }

            switch (alineacion)
            {
                case ContentAlignment.TopLeft: case ContentAlignment.MiddleLeft: case ContentAlignment.BottomLeft:
                    formato |= TextFormatFlags.Left; break;
                case ContentAlignment.TopRight: case ContentAlignment.MiddleRight: case ContentAlignment.BottomRight:
                    formato |= TextFormatFlags.Right; break;
                default:
                    formato |= TextFormatFlags.HorizontalCenter; break;
            }

            return formato;
        }

        /* Mezcla dos colores; proporcion = 0 devuelve el primero y 1 el segundo. */
        private static Color Mezclar(Color a, Color b, double proporcion)
        {
            return Color.FromArgb(
                (int)Math.Round(a.R + (b.R - a.R) * proporcion),
                (int)Math.Round(a.G + (b.G - a.G) * proporcion),
                (int)Math.Round(a.B + (b.B - a.B) * proporcion));
        }

        /* Brillo percibido de un color según la fórmula de WCAG (0 = negro, 1 = blanco). */
        private static double Luminancia(Color color)
        {
            return 0.2126 * Canal(color.R) + 0.7152 * Canal(color.G) + 0.0722 * Canal(color.B);
        }

        private static double Canal(int valor)
        {
            var v = valor / 255.0;
            return v <= 0.03928 ? v / 12.92 : Math.Pow((v + 0.055) / 1.055, 2.4);
        }
    }
}
