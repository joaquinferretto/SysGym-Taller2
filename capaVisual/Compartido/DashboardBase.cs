using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaVisual.Compartido
{
    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class DashboardBase : Form
    {
        private readonly FlowLayoutPanel panelOpciones;
        private readonly Panel panelContenido;
        private readonly Color colorPrimario;
        private FlowLayoutPanel seccionActiva;
        private Button botonSeccionActivo;
        private Button botonOpcionActivo;
        private Form formularioActual;
        private Control contenidoInicio;
        private Action actualizarContenidoInicio;

        protected UsuarioSistema UsuarioActual { get; private set; }
        public bool CambioCuentaSolicitado { get; private set; }

        protected DashboardBase(UsuarioSistema usuario, string titulo, Color colorPrimario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");

            UsuarioActual = usuario;
            this.colorPrimario = colorPrimario;
            Text = titulo;
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 10F);
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(900, 600);

            var encabezado = CrearEncabezado(usuario, titulo);
            panelOpciones = new FlowLayoutPanel
            {
                AutoScroll = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(14, 18, 14, 18),
                WrapContents = false
            };
            var panelMenu = CrearMenuLateral(panelOpciones);
            panelContenido = new Panel
            {
                BackColor = Color.FromArgb(226, 232, 240),
                Dock = DockStyle.Fill
            };

            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            Controls.Add(encabezado);
        }

        private Panel CrearMenuLateral(Control opciones)
        {
            var menu = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Left,
                Width = 260
            };
            var pie = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Bottom,
                Height = 70,
                Padding = new Padding(14, 10, 14, 14)
            };
            var salir = new Button
            {
                BackColor = Color.FromArgb(254, 242, 242),
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(185, 28, 28),
                Padding = new Padding(10, 0, 0, 0),
                Text = "Salir",
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false
            };
            salir.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            salir.Click += delegate
            {
                var respuesta = MessageBox.Show("¿Deseás salir de SysGym?", "Salir",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    CambioCuentaSolicitado = false;
                    Close();
                }
            };

            pie.Controls.Add(salir);
            menu.Controls.Add(opciones);
            menu.Controls.Add(pie);
            return menu;
        }

        private Panel CrearEncabezado(UsuarioSistema usuario, string titulo)
        {
            var encabezado = new Panel
            {
                BackColor = colorPrimario,
                Dock = DockStyle.Top,
                Height = 82
            };
            encabezado.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(24, 8),
                Text = "SYSGYM"
            });

            var rol = usuario.Rol == null || string.IsNullOrWhiteSpace(usuario.Rol.Descripcion)
                ? titulo.Replace("SysGym - ", string.Empty)
                : usuario.Rol.Descripcion;
            encabezado.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(224, 231, 255),
                Location = new Point(27, 48),
                Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + rol
            });

            var cambiarCuenta = new Button
            {
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = colorPrimario,
                Size = new Size(158, 34),
                Text = "Cambiar de cuenta",
                Top = 23,
                UseVisualStyleBackColor = false
            };
            cambiarCuenta.FlatAppearance.BorderSize = 0;
            cambiarCuenta.Click += delegate { CambioCuentaSolicitado = true; Close(); };
            encabezado.Controls.Add(cambiarCuenta);
            encabezado.Resize += delegate
            {
                cambiarCuenta.Left = Math.Max(12, encabezado.ClientSize.Width - cambiarCuenta.Width - 22);
            };
            return encabezado;
        }

        protected FlowLayoutPanel AgregarSeccion(string texto)
        {
            var contenedor = new Panel
            {
                BackColor = Color.White,
                Height = 44,
                Margin = new Padding(0, 0, 0, 8),
                Width = 230
            };
            var botonSeccion = new Button
            {
                BackColor = colorPrimario,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Height = 44,
                Tag = texto,
                Text = ">  " + texto,
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false
            };
            botonSeccion.FlatAppearance.BorderSize = 0;

            var opciones = new FlowLayoutPanel
            {
                AutoScroll = false,
                BackColor = Color.FromArgb(248, 250, 252),
                FlowDirection = FlowDirection.TopDown,
                Location = new Point(0, 44),
                Padding = new Padding(8, 7, 8, 2),
                Visible = false,
                Width = 230,
                WrapContents = false
            };
            botonSeccion.Click += delegate { AlternarSeccion(botonSeccion, opciones); };
            contenedor.Controls.Add(opciones);
            contenedor.Controls.Add(botonSeccion);
            panelOpciones.Controls.Add(contenedor);
            return opciones;
        }

        private void AlternarSeccion(Button boton, FlowLayoutPanel opciones)
        {
            var cerrar = seccionActiva == opciones && opciones.Visible;
            if (seccionActiva != null)
            {
                seccionActiva.Visible = false;
                seccionActiva.Parent.Height = 44;
            }
            if (botonSeccionActivo != null)
            {
                botonSeccionActivo.Text = ">  " + Convert.ToString(botonSeccionActivo.Tag);
            }

            if (cerrar)
            {
                seccionActiva = null;
                botonSeccionActivo = null;
                return;
            }

            opciones.Visible = true;
            opciones.Parent.Height = 44 + opciones.Height;
            boton.Text = "v  " + Convert.ToString(boton.Tag);
            seccionActiva = opciones;
            botonSeccionActivo = boton;
        }

        protected void AgregarOpcion(FlowLayoutPanel seccion, string texto, EventHandler accion)
        {
            var boton = new Button
            {
                BackColor = Color.FromArgb(248, 250, 252),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(51, 65, 85),
                Height = 40,
                Margin = new Padding(0, 0, 0, 5),
                Text = texto,
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false,
                Width = 214
            };
            boton.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            boton.Click += delegate(object sender, EventArgs e)
            {
                if (botonOpcionActivo != null)
                {
                    botonOpcionActivo.BackColor = Color.FromArgb(248, 250, 252);
                    botonOpcionActivo.ForeColor = Color.FromArgb(51, 65, 85);
                }
                boton.BackColor = Color.FromArgb(224, 231, 255);
                boton.ForeColor = colorPrimario;
                botonOpcionActivo = boton;
                accion(sender, e);
            };
            seccion.Controls.Add(boton);
            seccion.Height = seccion.Padding.Top + seccion.Padding.Bottom + seccion.Controls.Count * 45;
            if (seccion.Visible) seccion.Parent.Height = 44 + seccion.Height;
        }

        protected void AbrirFormulario(Form formulario)
        {
            if (formulario == null) return;

            if (formularioActual != null && !formularioActual.IsDisposed)
            {
                if (formularioActual.GetType() == formulario.GetType())
                {
                    formulario.Dispose();
                    formularioActual.BringToFront();
                    return;
                }
                var anterior = formularioActual;
                formularioActual = null;
                anterior.Close();
                anterior.Dispose();
            }

            formularioActual = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.MinimumSize = Size.Empty;
            formulario.FormClosed += delegate
            {
                if (ReferenceEquals(formularioActual, formulario))
                {
                    formularioActual = null;
                    MostrarContenidoInicio(true);
                }
            };
            if (contenidoInicio != null) contenidoInicio.Visible = false;
            panelContenido.Controls.Add(formulario);
            formulario.Show();
            formulario.BringToFront();
        }

        protected void EstablecerContenidoInicio(Control control, Action actualizar)
        {
            if (control == null) throw new ArgumentNullException("control");

            contenidoInicio = control;
            actualizarContenidoInicio = actualizar;
            contenidoInicio.Dock = DockStyle.Fill;
            panelContenido.Controls.Clear();
            panelContenido.Controls.Add(contenidoInicio);
            contenidoInicio.Visible = true;
        }

        private void MostrarContenidoInicio(bool actualizar)
        {
            if (contenidoInicio == null || contenidoInicio.IsDisposed) return;

            if (contenidoInicio.Parent != panelContenido)
            {
                panelContenido.Controls.Add(contenidoInicio);
            }
            contenidoInicio.Visible = true;
            contenidoInicio.BringToFront();
            if (actualizar && actualizarContenidoInicio != null)
            {
                actualizarContenidoInicio();
            }
        }

        protected void MostrarModulo(string modulo)
        {
            MessageBox.Show("El modulo " + modulo + " se conectara con su formulario correspondiente.",
                "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
