using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class InicioPanelAdministrador
    {
        private IContainer components;
        private TableLayoutPanel contenidoLayout;
        private Label lblResumen;
        private Label tituloClima;
        private Label estadoClima;
        private TableLayoutPanel listaClima;
        private Label lblTituloCuotas;
        private Label resumenCuotas;
        private DataGridView tablaCuotas;
        private exxen2._0.capaVisual.Compartido.Controles.EstadoSociosControl estadoSocios;
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
            contenidoLayout = new TableLayoutPanel();
            lblResumen = new Label();
            tituloClima = new Label();
            estadoClima = new Label();
            listaClima = new TableLayoutPanel();
            lblTituloCuotas = new Label();
            resumenCuotas = new Label();
            tablaCuotas = new DataGridView();
            estadoSocios = new exxen2._0.capaVisual.Compartido.Controles.EstadoSociosControl();
            ayudaClima = new ToolTip(components);
            colIdMembresia = new DataGridViewTextBoxColumn();
            colIdSocio = new DataGridViewTextBoxColumn();
            colSocio = new DataGridViewTextBoxColumn();
            colDni = new DataGridViewTextBoxColumn();
            colPlan = new DataGridViewTextBoxColumn();
            colPeriodo = new DataGridViewTextBoxColumn();
            colEstadoCuota = new DataGridViewTextBoxColumn();
            colSaldo = new DataGridViewTextBoxColumn();
            colSituacion = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)(tablaCuotas)).BeginInit();
            listaClima.SuspendLayout();
            SuspendLayout();



            lblResumen.AutoSize = false;
            lblResumen.Dock = DockStyle.None;
            lblResumen.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblResumen.ForeColor = Color.FromArgb(30, 41, 59);
            lblResumen.Text = "Resumen general";
            lblResumen.TextAlign = ContentAlignment.MiddleLeft;



            tituloClima.AutoSize = false;
            tituloClima.Dock = DockStyle.None;
            tituloClima.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            tituloClima.ForeColor = Color.FromArgb(30, 41, 59);
            tituloClima.Text = "Pronóstico semanal - Corrientes Capital";
            tituloClima.TextAlign = ContentAlignment.MiddleLeft;
            estadoClima.AutoSize = false;
            estadoClima.Dock = DockStyle.None;
            estadoClima.Width = 240;
            estadoClima.ForeColor = Color.FromArgb(71, 85, 105);
            estadoClima.Text = "";
            estadoClima.TextAlign = ContentAlignment.MiddleRight;
            listaClima.BackColor = Color.White;
            listaClima.Dock = DockStyle.None;
            listaClima.AutoScroll = false;
            listaClima.AutoSize = false;
            listaClima.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            listaClima.ColumnCount = 7;
            listaClima.RowCount = 1;
            listaClima.Padding = new Padding(8, 4, 8, 8);
            listaClima.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            listaClima.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            listaClima.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            listaClima.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            listaClima.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            listaClima.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            listaClima.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            listaClima.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1F));
            listaClima.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));



            lblTituloCuotas.AutoSize = false;
            lblTituloCuotas.Dock = DockStyle.None;
            lblTituloCuotas.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTituloCuotas.ForeColor = Color.FromArgb(30, 41, 59);
            lblTituloCuotas.Text = "Estado de cuenta de socios";
            lblTituloCuotas.TextAlign = ContentAlignment.MiddleLeft;
            resumenCuotas.AutoSize = false;
            resumenCuotas.Dock = DockStyle.None;
            resumenCuotas.Width = 520;
            resumenCuotas.ForeColor = Color.FromArgb(71, 85, 105);
            resumenCuotas.Text = "";
            resumenCuotas.TextAlign = ContentAlignment.MiddleRight;
            tablaCuotas.Dock = DockStyle.None;
            tablaCuotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tablaCuotas.BackgroundColor = Color.White;
            tablaCuotas.BorderStyle = BorderStyle.None;
            tablaCuotas.AllowUserToAddRows = false;
            tablaCuotas.AllowUserToDeleteRows = false;
            tablaCuotas.AllowUserToResizeRows = false;
            tablaCuotas.ReadOnly = true;
            tablaCuotas.MultiSelect = false;
            tablaCuotas.RowHeadersVisible = false;
            tablaCuotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tablaCuotas.RowTemplate.Height = 32;
            tablaCuotas.ColumnHeadersHeight = 38;
            tablaCuotas.Columns.AddRange(new DataGridViewColumn[] { colIdMembresia, colIdSocio, colSocio, colDni, colPlan, colPeriodo, colEstadoCuota, colSaldo, colSituacion });
            tablaCuotas.CellDoubleClick += new DataGridViewCellEventHandler(tablaCuotas_CellDoubleClick);
            colIdMembresia.HeaderText = "N.";
            colIdMembresia.Name = "colIdMembresia";
            colIdMembresia.ReadOnly = true;
            colIdMembresia.MinimumWidth = 44;
            colIdSocio.HeaderText = "Id socio";
            colIdSocio.Name = "colIdSocio";
            colIdSocio.ReadOnly = true;
            colIdSocio.Visible = false;
            colSocio.HeaderText = "Socio";
            colSocio.Name = "colSocio";
            colSocio.ReadOnly = true;
            colSocio.MinimumWidth = 110;
            colDni.HeaderText = "DNI";
            colDni.Name = "colDni";
            colDni.ReadOnly = true;
            colDni.MinimumWidth = 70;
            colPlan.HeaderText = "Plan";
            colPlan.Name = "colPlan";
            colPlan.ReadOnly = true;
            colPlan.MinimumWidth = 80;
            colPeriodo.HeaderText = "Ultimo periodo";
            colPeriodo.Name = "colPeriodo";
            colPeriodo.ReadOnly = true;
            colPeriodo.MinimumWidth = 125;
            colEstadoCuota.HeaderText = "Ultima cuota";
            colEstadoCuota.Name = "colEstadoCuota";
            colEstadoCuota.ReadOnly = true;
            colEstadoCuota.MinimumWidth = 92;
            colSaldo.HeaderText = "Saldo pendiente";
            colSaldo.Name = "colSaldo";
            colSaldo.ReadOnly = true;
            colSaldo.MinimumWidth = 124;
            colSituacion.HeaderText = "Situacion";
            colSituacion.Name = "colSituacion";
            colSituacion.ReadOnly = true;
            colSituacion.MinimumWidth = 92;


            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(640, 460);
            Size = new Size(936, 670);
            Name = "InicioPanelAdministrador";
            listaClima.ResumeLayout(false);
            ((ISupportInitialize)(tablaCuotas)).EndInit();
            ResumeLayout(false);
            PerformLayout();
            colIdMembresia.FillWeight = 7;
            colSocio.FillWeight = 19;
            colDni.FillWeight = 9;
            colPlan.FillWeight = 12;
            colPeriodo.FillWeight = 17;
            colEstadoCuota.FillWeight = 13;
            colSaldo.FillWeight = 13;
            colSituacion.FillWeight = 10;
            lblResumen.Name = "lblResumen";
            lblResumen.Location = new Point(20, 20);
            lblResumen.Size = new Size(896, 48);
            lblResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            tituloClima.Name = "tituloClima";
            tituloClima.Location = new Point(36, 68);
            tituloClima.Size = new Size(590, 44);
            tituloClima.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            estadoClima.Name = "estadoClima";
            estadoClima.Location = new Point(626, 68);
            estadoClima.Size = new Size(274, 44);
            estadoClima.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            listaClima.Name = "listaClima";
            listaClima.Location = new Point(20, 112);
            listaClima.Size = new Size(896, 130);
            listaClima.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            lblTituloCuotas.Name = "lblTituloCuotas";
            lblTituloCuotas.Location = new Point(36, 242);
            lblTituloCuotas.Size = new Size(350, 44);
            lblTituloCuotas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            resumenCuotas.Name = "resumenCuotas";
            resumenCuotas.Location = new Point(386, 242);
            resumenCuotas.Size = new Size(514, 44);
            resumenCuotas.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            tablaCuotas.Name = "tablaCuotas";
            tablaCuotas.Location = new Point(20, 286);
            tablaCuotas.Size = new Size(896, 364);
            tablaCuotas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            estadoSocios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            estadoSocios.Location = new Point(20, 242);
            estadoSocios.Name = "estadoSocios";
            estadoSocios.Size = new Size(896, 420);

            contenidoLayout.Name = "contenidoLayout";
            contenidoLayout.Dock = DockStyle.Fill;
            contenidoLayout.Padding = new Padding(12);
            contenidoLayout.ColumnCount = 2;
            contenidoLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            contenidoLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            contenidoLayout.RowCount = 4;
            contenidoLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            contenidoLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            contenidoLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            contenidoLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            lblResumen.Dock = DockStyle.Fill;
            tituloClima.Dock = DockStyle.Fill;
            estadoClima.Dock = DockStyle.Fill;
            listaClima.Dock = DockStyle.Fill;
            estadoSocios.Dock = DockStyle.Fill;
            estadoSocios.MinimumSize = Size.Empty;
            contenidoLayout.Controls.Add(lblResumen, 0, 0);
            contenidoLayout.SetColumnSpan(lblResumen, 2);
            contenidoLayout.Controls.Add(tituloClima, 0, 1);
            contenidoLayout.Controls.Add(estadoClima, 1, 1);
            contenidoLayout.Controls.Add(listaClima, 0, 2);
            contenidoLayout.SetColumnSpan(listaClima, 2);
            contenidoLayout.Controls.Add(estadoSocios, 0, 3);
            contenidoLayout.SetColumnSpan(estadoSocios, 2);
            Controls.Add(contenidoLayout);
            Load += new System.EventHandler(InicioPanelAdministrador_Load);
        }
    }
}
