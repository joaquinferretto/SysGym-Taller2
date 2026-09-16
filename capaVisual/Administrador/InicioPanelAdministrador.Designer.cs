using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class InicioPanelAdministrador
    {
        private IContainer components;
        private Panel principal;
        private Panel panelCabecera;
        private Label lblResumen;
        private Panel tarjetaClima;
        private Panel cabeceraClima;
        private Label tituloClima;
        private Label estadoClima;
        private FlowLayoutPanel listaClima;
        private Panel tarjetaCuotas;
        private Panel cabeceraCuotas;
        private Label lblTituloCuotas;
        private Label resumenCuotas;
        private DataGridView tablaCuotas;
        private ToolTip ayudaClima;
        private DataGridViewTextBoxColumn colIdMembresia;
        private DataGridViewTextBoxColumn colIdSocio;
        private DataGridViewTextBoxColumn colSocio;
        private DataGridViewTextBoxColumn colDni;
        private DataGridViewTextBoxColumn colPlan;
        private DataGridViewTextBoxColumn colPeriodo;
        private DataGridViewTextBoxColumn colEstadoCuota;
        private DataGridViewTextBoxColumn colSaldo;
        private DataGridViewTextBoxColumn colSituacion;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            components = new Container();
            principal = new Panel(); panelCabecera = new Panel(); lblResumen = new Label();
            tarjetaClima = new Panel(); cabeceraClima = new Panel(); tituloClima = new Label(); estadoClima = new Label(); listaClima = new FlowLayoutPanel();
            tarjetaCuotas = new Panel(); cabeceraCuotas = new Panel(); lblTituloCuotas = new Label(); resumenCuotas = new Label(); tablaCuotas = new DataGridView(); ayudaClima = new ToolTip(components);
            colIdMembresia = new DataGridViewTextBoxColumn(); colIdSocio = new DataGridViewTextBoxColumn(); colSocio = new DataGridViewTextBoxColumn(); colDni = new DataGridViewTextBoxColumn(); colPlan = new DataGridViewTextBoxColumn(); colPeriodo = new DataGridViewTextBoxColumn(); colEstadoCuota = new DataGridViewTextBoxColumn(); colSaldo = new DataGridViewTextBoxColumn(); colSituacion = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)(tablaCuotas)).BeginInit(); principal.SuspendLayout(); panelCabecera.SuspendLayout(); tarjetaClima.SuspendLayout(); cabeceraClima.SuspendLayout(); listaClima.SuspendLayout(); tarjetaCuotas.SuspendLayout(); cabeceraCuotas.SuspendLayout(); SuspendLayout();

            principal.BackColor = Color.Transparent; principal.Dock = DockStyle.Fill; principal.Padding = new Padding(20); principal.Controls.Add(tarjetaCuotas); principal.Controls.Add(tarjetaClima); principal.Controls.Add(panelCabecera);
            panelCabecera.BackColor = Color.Transparent; panelCabecera.Dock = DockStyle.Top; panelCabecera.Height = 48; panelCabecera.Margin = new Padding(0, 0, 0, 8); panelCabecera.Controls.Add(lblResumen);
            lblResumen.AutoSize = false; lblResumen.Dock = DockStyle.Fill; lblResumen.Font = new Font("Segoe UI", 20F, FontStyle.Bold); lblResumen.ForeColor = Color.FromArgb(30, 41, 59); lblResumen.Text = "Resumen general"; lblResumen.TextAlign = ContentAlignment.MiddleLeft;

            tarjetaClima.BackColor = Color.White; tarjetaClima.BorderStyle = BorderStyle.FixedSingle; tarjetaClima.Dock = DockStyle.Top; tarjetaClima.Height = 174; tarjetaClima.Margin = new Padding(0, 0, 0, 12); tarjetaClima.Controls.Add(listaClima); tarjetaClima.Controls.Add(cabeceraClima);
            cabeceraClima.BackColor = Color.White; cabeceraClima.Dock = DockStyle.Top; cabeceraClima.Height = 44; cabeceraClima.Padding = new Padding(16, 0, 16, 0); cabeceraClima.Controls.Add(tituloClima); cabeceraClima.Controls.Add(estadoClima);
            tituloClima.AutoSize = false; tituloClima.Dock = DockStyle.Fill; tituloClima.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold); tituloClima.ForeColor = Color.FromArgb(30, 41, 59); tituloClima.Text = "Pronóstico semanal - Corrientes Capital"; tituloClima.TextAlign = ContentAlignment.MiddleLeft;
            estadoClima.AutoSize = false; estadoClima.Dock = DockStyle.Right; estadoClima.Width = 240; estadoClima.ForeColor = Color.FromArgb(100, 116, 139); estadoClima.Text = ""; estadoClima.TextAlign = ContentAlignment.MiddleRight;
            listaClima.BackColor = Color.White; listaClima.Dock = DockStyle.Fill; listaClima.AutoScroll = true; listaClima.FlowDirection = FlowDirection.LeftToRight; listaClima.WrapContents = false; listaClima.Padding = new Padding(10, 4, 10, 8);

            tarjetaCuotas.BackColor = Color.White; tarjetaCuotas.BorderStyle = BorderStyle.FixedSingle; tarjetaCuotas.Dock = DockStyle.Fill; tarjetaCuotas.Margin = new Padding(0); tarjetaCuotas.Controls.Add(tablaCuotas); tarjetaCuotas.Controls.Add(cabeceraCuotas);
            cabeceraCuotas.BackColor = Color.White; cabeceraCuotas.Dock = DockStyle.Top; cabeceraCuotas.Height = 44; cabeceraCuotas.Padding = new Padding(16, 0, 16, 0); cabeceraCuotas.Controls.Add(lblTituloCuotas); cabeceraCuotas.Controls.Add(resumenCuotas);
            lblTituloCuotas.AutoSize = false; lblTituloCuotas.Dock = DockStyle.Fill; lblTituloCuotas.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold); lblTituloCuotas.ForeColor = Color.FromArgb(30, 41, 59); lblTituloCuotas.Text = "Estado de cuenta de socios"; lblTituloCuotas.TextAlign = ContentAlignment.MiddleLeft;
            resumenCuotas.AutoSize = false; resumenCuotas.Dock = DockStyle.Right; resumenCuotas.Width = 520; resumenCuotas.ForeColor = Color.FromArgb(71, 85, 105); resumenCuotas.Text = ""; resumenCuotas.TextAlign = ContentAlignment.MiddleRight;
            tablaCuotas.Dock = DockStyle.Fill; tablaCuotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tablaCuotas.BackgroundColor = Color.White; tablaCuotas.BorderStyle = BorderStyle.None; tablaCuotas.AllowUserToAddRows = false; tablaCuotas.AllowUserToDeleteRows = false; tablaCuotas.AllowUserToResizeRows = false; tablaCuotas.ReadOnly = true; tablaCuotas.MultiSelect = false; tablaCuotas.RowHeadersVisible = false; tablaCuotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tablaCuotas.RowTemplate.Height = 32; tablaCuotas.ColumnHeadersHeight = 38; tablaCuotas.Columns.AddRange(new DataGridViewColumn[] { colIdMembresia, colIdSocio, colSocio, colDni, colPlan, colPeriodo, colEstadoCuota, colSaldo, colSituacion }); tablaCuotas.CellDoubleClick += new DataGridViewCellEventHandler(tablaCuotas_CellDoubleClick);
            colIdMembresia.HeaderText = "N."; colIdMembresia.Name = "colIdMembresia"; colIdMembresia.ReadOnly = true; colIdMembresia.FillWeight = 8; colIdMembresia.MinimumWidth = 42;
            colIdSocio.HeaderText = "Id socio"; colIdSocio.Name = "colIdSocio"; colIdSocio.ReadOnly = true; colIdSocio.Visible = false;
            colSocio.HeaderText = "Socio"; colSocio.Name = "colSocio"; colSocio.ReadOnly = true; colSocio.FillWeight = 22; colSocio.MinimumWidth = 100;
            colDni.HeaderText = "DNI"; colDni.Name = "colDni"; colDni.ReadOnly = true; colDni.FillWeight = 12; colDni.MinimumWidth = 65;
            colPlan.HeaderText = "Plan"; colPlan.Name = "colPlan"; colPlan.ReadOnly = true; colPlan.FillWeight = 16; colPlan.MinimumWidth = 75;
            colPeriodo.HeaderText = "Ultimo periodo"; colPeriodo.Name = "colPeriodo"; colPeriodo.ReadOnly = true; colPeriodo.FillWeight = 20; colPeriodo.MinimumWidth = 110;
            colEstadoCuota.HeaderText = "Ultima cuota"; colEstadoCuota.Name = "colEstadoCuota"; colEstadoCuota.ReadOnly = true; colEstadoCuota.FillWeight = 16; colEstadoCuota.MinimumWidth = 90;
            colSaldo.HeaderText = "Saldo pendiente"; colSaldo.Name = "colSaldo"; colSaldo.ReadOnly = true; colSaldo.FillWeight = 15; colSaldo.MinimumWidth = 90;
            colSituacion.HeaderText = "Situacion"; colSituacion.Name = "colSituacion"; colSituacion.ReadOnly = true; colSituacion.FillWeight = 16; colSituacion.MinimumWidth = 90;

            Controls.Add(principal); AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(640, 460); Size = new Size(936, 670); Name = "InicioPanelAdministrador";
            cabeceraCuotas.ResumeLayout(false); tarjetaCuotas.ResumeLayout(false); listaClima.ResumeLayout(false); cabeceraClima.ResumeLayout(false); tarjetaClima.ResumeLayout(false); panelCabecera.ResumeLayout(false); principal.ResumeLayout(false); ((ISupportInitialize)(tablaCuotas)).EndInit(); ResumeLayout(false);
            Load += new System.EventHandler(InicioPanelAdministrador_Load);
        }
    }
}
