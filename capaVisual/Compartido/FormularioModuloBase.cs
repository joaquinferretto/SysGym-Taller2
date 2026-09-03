using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class FormularioModuloBase : Form
    {
        protected readonly Color ColorPrimario;
        protected readonly DataGridView Tabla;
        protected readonly FlowLayoutPanel BarraAcciones;
        protected readonly Label Estado;
        protected readonly Panel Contenido;

        protected bool EnModoDiseno
        {
            get
            {
                return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                    || DesignMode
                    || (Site != null && Site.DesignMode)
                    || GetType().Name.EndsWith("FormBase", StringComparison.Ordinal);
            }
        }

        protected FormularioModuloBase(string titulo, string descripcion, Color colorPrimario)
        {
            ColorPrimario = colorPrimario;
            Text = "SysGym | " + titulo;
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.5F);
            StartPosition = FormStartPosition.Manual;
            MinimumSize = new Size(760, 540);
            Size = new Size(1100, 680);

            var encabezado = new Panel
            {
                BackColor = colorPrimario,
                Dock = DockStyle.Top,
                Height = 80
            };

            var volver = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = colorPrimario,
                Size = new Size(92, 34),
                Text = "Volver",
                Top = 22,
                UseVisualStyleBackColor = false
            };
            volver.FlatAppearance.BorderSize = 0;
            volver.Click += delegate { Close(); };
            encabezado.Controls.Add(volver);
            encabezado.Resize += delegate
            {
                volver.Left = Math.Max(12, encabezado.ClientSize.Width - volver.Width - 20);
            };

            encabezado.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(22, 10),
                Text = titulo
            });
            encabezado.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(24, 47),
                Text = descripcion
            });

            BarraAcciones = new FlowLayoutPanel
            {
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Height = 52,
                Padding = new Padding(16, 8, 16, 8),
                WrapContents = false
            };

            Estado = new Label
            {
                AutoSize = false,
                BackColor = Color.FromArgb(226, 232, 240),
                Dock = DockStyle.Bottom,
                ForeColor = Color.FromArgb(51, 65, 85),
                Height = 32,
                Padding = new Padding(18, 8, 8, 0),
                Text = "Listo"
            };

            Contenido = new Panel
            {
                BackColor = Color.FromArgb(248, 250, 252),
                Dock = DockStyle.Fill,
                Padding = new Padding(12)
            };

            Tabla = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                ColumnHeadersHeight = 38,
                Dock = DockStyle.Fill,
                GridColor = Color.FromArgb(226, 232, 240),
                MultiSelect = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                RowTemplate = { Height = 36 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            Tabla.EnableHeadersVisualStyles = false;
            Tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            Tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            Tabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            Tabla.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            Tabla.DefaultCellStyle.BackColor = Color.White;
            Tabla.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            Tabla.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            Tabla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 231, 255);
            Tabla.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            Tabla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            Contenido.Controls.Add(Tabla);

            Controls.Add(Contenido);
            Controls.Add(Estado);
            Controls.Add(BarraAcciones);
            Controls.Add(encabezado);
        }

        protected void AlCargarEnEjecucion(EventHandler accion)
        {
            if (accion == null) return;

            Load += delegate(object sender, EventArgs e)
            {
                if (!EnModoDiseno)
                {
                    accion(sender, e);
                }
            };
        }

        protected VistaListadoDetalle CrearVistaListadoDetalle(string tituloListado,
            string ayudaBusqueda, string textoNuevo, EventHandler accionNuevo)
        {
            BarraAcciones.Visible = false;
            Contenido.Padding = new Padding(0);
            Contenido.Controls.Clear();
            var vista = new VistaListadoDetalle(Tabla, ColorPrimario, tituloListado,
                ayudaBusqueda, textoNuevo, accionNuevo);
            Contenido.Controls.Add(vista);
            return vista;
        }

        protected Button AgregarBoton(string texto, EventHandler accion, bool destacado = false)
        {
            var boton = new Button
            {
                AutoSize = true,
                BackColor = destacado ? ColorPrimario : Color.FromArgb(226, 232, 240),
                FlatStyle = FlatStyle.Flat,
                ForeColor = destacado ? Color.White : Color.FromArgb(30, 41, 59),
                Height = 36,
                Margin = new Padding(4, 0, 4, 0),
                Padding = new Padding(12, 0, 12, 0),
                Text = texto,
                UseVisualStyleBackColor = false
            };
            boton.FlatAppearance.BorderSize = 0;
            boton.Click += accion;
            BarraAcciones.Controls.Add(boton);
            return boton;
        }

        protected T AgregarFiltroListado<T>(string etiqueta, T control) where T : Control
        {
            var contenedor = new Panel
            {
                BackColor = Color.White,
                Height = 50,
                Margin = new Padding(4, 0, 12, 0),
                Width = 190
            };
            contenedor.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(0, 0),
                Text = etiqueta
            });
            control.Font = new Font("Segoe UI", 9.5F);
            control.Location = new Point(0, 20);
            control.Size = new Size(contenedor.ClientSize.Width, 28);
            control.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            contenedor.Controls.Add(control);
            BarraAcciones.Controls.Add(contenedor);
            return control;
        }

        protected void AgregarPanelFormulario(Control panel)
        {
            panel.Dock = DockStyle.Top;
            panel.Margin = new Padding(0, 0, 0, 12);
            Contenido.Controls.Add(panel);
            panel.BringToFront();
        }

        protected void MostrarError(Exception excepcion)
        {
            Estado.ForeColor = Color.FromArgb(185, 28, 28);
            Estado.Text = excepcion.Message;
            MessageBox.Show(excepcion.Message, "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        protected void MostrarExito(string mensaje)
        {
            Estado.ForeColor = Color.FromArgb(21, 128, 61);
            Estado.Text = mensaje;
        }

        protected static GroupBox CrearGrupo(string texto, int ancho, int alto, Point ubicacion)
        {
            return new GroupBox
            {
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Location = ubicacion,
                Padding = new Padding(12),
                Size = new Size(ancho, alto),
                Text = texto
            };
        }

        protected static TextBox Campo(GroupBox grupo, string etiqueta, int left, int top, int width = 180)
        {
            grupo.Controls.Add(new Label { AutoSize = true, Location = new Point(left, top + 4), Text = etiqueta });
            var campo = new TextBox { Location = new Point(left + 88, top), Size = new Size(width, 26) };
            grupo.Controls.Add(campo);
            return campo;
        }

        protected static ComboBox Selector(GroupBox grupo, string etiqueta, int left, int top, int width = 180)
        {
            grupo.Controls.Add(new Label { AutoSize = true, Location = new Point(left, top + 4), Text = etiqueta });
            var selector = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(left + 88, top),
                Size = new Size(width, 26)
            };
            grupo.Controls.Add(selector);
            return selector;
        }

        protected static int Entero(TextBox campo, string nombre)
        {
            int resultado;
            if (!int.TryParse(campo.Text.Trim(), out resultado) || resultado <= 0)
            {
                throw new InvalidOperationException("El campo " + nombre + " debe ser un entero positivo.");
            }
            return resultado;
        }

        protected static decimal DecimalPositivo(TextBox campo, string nombre, bool permitirCero = false)
        {
            decimal resultado;
            var texto = campo.Text.Trim();
            var esValido = decimal.TryParse(texto.Replace(',', '.'),
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out resultado);
            if (!esValido
                || (permitirCero ? resultado < 0 : resultado <= 0))
            {
                throw new InvalidOperationException("El campo " + nombre + " debe ser numérico.");
            }
            return resultado;
        }

        protected static void ConfigurarEntradaDecimal(TextBox campo)
        {
            campo.KeyPress += delegate(object sender, KeyPressEventArgs e)
            {
                if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;

                if (e.KeyChar == ',' || e.KeyChar == '.')
                {
                    if (campo.Text.Contains(",") || campo.Text.Contains("."))
                    {
                        e.Handled = true;
                        return;
                    }

                    e.KeyChar = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                    return;
                }

                e.Handled = true;
            };
        }
    }

    [DesignerCategory("Code")]
    [DesignTimeVisible(false)]
    public sealed class VistaListadoDetalle : UserControl
    {
        private readonly Color colorPrimario;
        private readonly List<Panel> campos = new List<Panel>();
        private readonly RowStyle filaFiltros;

        public TextBox Buscador { get; private set; }
        public FlowLayoutPanel Formulario { get; private set; }
        public FlowLayoutPanel Acciones { get; private set; }
        public Label TituloDetalle { get; private set; }
        public Button BotonNuevo { get; private set; }
        public FlowLayoutPanel FiltrosListado { get; private set; }

        public VistaListadoDetalle(DataGridView tabla, Color colorPrimario, string tituloListado,
            string ayudaBusqueda, string textoNuevo, EventHandler accionNuevo)
        {
            this.colorPrimario = colorPrimario;
            BackColor = Color.FromArgb(248, 250, 252);
            Dock = DockStyle.Fill;
            Padding = new Padding(20, 16, 20, 20);

            var principal = new TableLayoutPanel
            {
                BackColor = Color.Transparent,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                RowCount = 2
            };
            principal.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            principal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var barraSuperior = new Panel { BackColor = Color.Transparent, Dock = DockStyle.Fill };
            barraSuperior.Controls.Add(new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(0, 15),
                Text = "Selecciona un registro para editarlo o crea uno nuevo."
            });
            BotonNuevo = CrearBoton(textoNuevo, true, false);
            BotonNuevo.Size = new Size(174, 36);
            BotonNuevo.Top = 5;
            BotonNuevo.Click += accionNuevo;
            barraSuperior.Controls.Add(BotonNuevo);
            barraSuperior.Resize += delegate
            {
                BotonNuevo.Left = Math.Max(0, barraSuperior.ClientSize.Width - BotonNuevo.Width);
            };

            var columnas = new TableLayoutPanel
            {
                BackColor = Color.Transparent,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                RowCount = 1
            };
            columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46F));
            columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54F));

            var listado = CrearPanelTarjeta();
            listado.Margin = new Padding(0, 0, 8, 0);
            var layoutListado = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                RowCount = 5
            };
            layoutListado.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            layoutListado.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            layoutListado.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            filaFiltros = new RowStyle(SizeType.Absolute, 0F);
            layoutListado.RowStyles.Add(filaFiltros);
            layoutListado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutListado.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Text = tituloListado
            }, 0, 0);
            layoutListado.Controls.Add(new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 116, 139),
                Text = ayudaBusqueda
            }, 0, 1);
            Buscador = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 10F),
                Height = 30,
                Margin = new Padding(0, 2, 0, 8)
            };
            layoutListado.Controls.Add(Buscador, 0, 2);
            FiltrosListado = new FlowLayoutPanel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0),
                Padding = new Padding(0),
                WrapContents = false
            };
            layoutListado.Controls.Add(FiltrosListado, 0, 3);
            tabla.Margin = new Padding(0, 8, 0, 0);
            layoutListado.Controls.Add(tabla, 0, 4);
            listado.Controls.Add(layoutListado);

            var detalle = CrearPanelTarjeta();
            detalle.Margin = new Padding(8, 0, 0, 0);
            var layoutDetalle = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                RowCount = 3
            };
            layoutDetalle.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            layoutDetalle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutDetalle.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            TituloDetalle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Text = "Nuevo registro"
            };
            layoutDetalle.Controls.Add(TituloDetalle, 0, 0);
            Formulario = new FlowLayoutPanel
            {
                AutoScroll = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(0, 4, 0, 8),
                WrapContents = false
            };
            Formulario.ClientSizeChanged += delegate { AjustarCampos(); };
            layoutDetalle.Controls.Add(Formulario, 0, 1);
            Acciones = new FlowLayoutPanel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 10, 0, 0),
                WrapContents = false
            };
            Acciones.ClientSizeChanged += delegate { AjustarAcciones(); };
            layoutDetalle.Controls.Add(Acciones, 0, 2);
            detalle.Controls.Add(layoutDetalle);

            columnas.Controls.Add(listado, 0, 0);
            columnas.Controls.Add(detalle, 1, 0);
            principal.Controls.Add(barraSuperior, 0, 0);
            principal.Controls.Add(columnas, 0, 1);
            Controls.Add(principal);
        }

        public T AgregarCampo<T>(string etiqueta, T control) where T : Control
        {
            var panelCampo = new Panel
            {
                BackColor = Color.White,
                Height = 52,
                Margin = new Padding(0, 0, 0, 6),
                Width = 400
            };
            panelCampo.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                Location = new Point(0, 0),
                Text = etiqueta
            });
            control.Font = new Font("Segoe UI", 10F);
            control.Location = new Point(0, 20);
            control.Height = 26;
            panelCampo.Controls.Add(control);
            panelCampo.Resize += delegate
            {
                control.Width = panelCampo.ClientSize.Width;
            };
            campos.Add(panelCampo);
            Formulario.Controls.Add(panelCampo);
            AjustarCampos();
            return control;
        }

        public Button AgregarAccion(string texto, EventHandler accion, bool principal = false,
            bool destructiva = false)
        {
            var boton = CrearBoton(texto, principal, destructiva);
            boton.Click += accion;
            Acciones.Controls.Add(boton);
            AjustarAcciones();
            return boton;
        }

        public T AgregarFiltroListado<T>(string etiqueta, T control) where T : Control
        {
            filaFiltros.Height = 56F;
            var contenedor = new Panel
            {
                BackColor = Color.White,
                Height = 50,
                Margin = new Padding(0, 0, 12, 0),
                Width = 240
            };
            contenedor.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(0, 0),
                Text = etiqueta
            });
            control.Font = new Font("Segoe UI", 9.5F);
            control.Location = new Point(0, 20);
            control.Height = 28;
            control.Width = contenedor.ClientSize.Width;
            control.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            contenedor.Controls.Add(control);
            FiltrosListado.Controls.Add(contenedor);
            return control;
        }

        private Button CrearBoton(string texto, bool principal, bool destructiva)
        {
            var fondo = principal ? colorPrimario : Color.FromArgb(241, 245, 249);
            var frente = principal ? Color.White : Color.FromArgb(30, 41, 59);
            if (destructiva)
            {
                fondo = Color.FromArgb(254, 242, 242);
                frente = Color.FromArgb(185, 28, 28);
            }

            var boton = new Button
            {
                BackColor = fondo,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = frente,
                Height = 36,
                Margin = new Padding(0, 0, 8, 0),
                Padding = new Padding(12, 0, 12, 0),
                Text = texto,
                UseVisualStyleBackColor = false,
                Width = 112
            };
            boton.FlatAppearance.BorderSize = destructiva ? 1 : 0;
            boton.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            return boton;
        }

        private static Panel CrearPanelTarjeta()
        {
            return new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                Padding = new Padding(16)
            };
        }

        private void AjustarCampos()
        {
            var ancho = Math.Min(440, Math.Max(220, Formulario.ClientSize.Width - 24));
            foreach (var campo in campos)
            {
                campo.Width = ancho;
            }
        }

        private void AjustarAcciones()
        {
            if (Acciones == null || Acciones.Controls.Count == 0) return;
            var separacion = 8 * (Acciones.Controls.Count - 1);
            var disponible = Math.Max(80, Acciones.ClientSize.Width - separacion - 4);
            var ancho = Math.Min(140, Math.Max(88, disponible / Acciones.Controls.Count));
            foreach (Control control in Acciones.Controls)
            {
                control.Width = ancho;
            }
        }
    }
}
