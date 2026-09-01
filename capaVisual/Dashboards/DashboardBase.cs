using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaVisual.Dashboards
{
    public class DashboardBase : Form
    {
        private readonly FlowLayoutPanel panelOpciones;

        protected UsuarioSistema UsuarioActual { get; private set; }

        protected DashboardBase(UsuarioSistema usuario, string titulo)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("usuario");
            }

            UsuarioActual = usuario;
            Text = titulo;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(760, 480);
            Size = new Size(900, 560);

            var encabezado = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Location = new Point(24, 20),
                Text = "SYSGYM"
            };

            var usuarioLabel = new Label
            {
                AutoSize = true,
                Location = new Point(27, 62),
                Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido
            };

            panelOpciones = new FlowLayoutPanel
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                Location = new Point(24, 105),
                Size = new Size(820, 330),
                WrapContents = true
            };

            Controls.Add(encabezado);
            Controls.Add(usuarioLabel);
            Controls.Add(panelOpciones);
        }

        protected void AgregarOpcion(string texto, EventHandler accion)
        {
            var boton = new Button
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 10F),
                Height = 54,
                Margin = new Padding(8),
                Text = texto,
                Width = 180,
                UseVisualStyleBackColor = true
            };

            boton.Click += accion;
            panelOpciones.Controls.Add(boton);
        }

        protected void MostrarModulo(string modulo)
        {
            MessageBox.Show("El módulo " + modulo + " se conectará con su formulario correspondiente.",
                "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
