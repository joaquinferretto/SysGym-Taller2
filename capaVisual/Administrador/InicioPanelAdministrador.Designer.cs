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
        private Label lblFecha;
        private Panel tarjetaClima;
        private Panel cabeceraClima;
        private Label tituloClima;
        private Label estadoClima;
        private Panel listaClima;
        private Panel tarjetaSuscripcion;
        private Label lblSuscripcion;
        private Panel tarjetaCuotas;
        private Panel cabeceraCuotas;
        private Label resumenCuotas;
        private DataGridView tablaCuotas;
        private ToolTip ayudaClima;
        private Label lblTituloCuotas;
        private Panel tarjetaClimaEjemplo;
        private Label lblDiaEjemplo;
        private Label lblIconoEjemplo;
        private Label lblDescripcionClimaEjemplo;
        private Label lblTemperaturaEjemplo;
        private Label lblLluviaEjemplo;
        private DataGridViewTextBoxColumn colIdMembresia;
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
            this.components = new System.ComponentModel.Container();
            this.principal = new System.Windows.Forms.Panel();
            this.panelCabecera = new System.Windows.Forms.Panel();
            this.lblResumen = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.tarjetaClima = new System.Windows.Forms.Panel();
            this.listaClima = new System.Windows.Forms.Panel();
            this.tarjetaClimaEjemplo = new System.Windows.Forms.Panel();
            this.lblLluviaEjemplo = new System.Windows.Forms.Label();
            this.lblTemperaturaEjemplo = new System.Windows.Forms.Label();
            this.lblDescripcionClimaEjemplo = new System.Windows.Forms.Label();
            this.lblIconoEjemplo = new System.Windows.Forms.Label();
            this.lblDiaEjemplo = new System.Windows.Forms.Label();
            this.cabeceraClima = new System.Windows.Forms.Panel();
            this.tituloClima = new System.Windows.Forms.Label();
            this.estadoClima = new System.Windows.Forms.Label();
            this.tarjetaSuscripcion = new System.Windows.Forms.Panel();
            this.lblSuscripcion = new System.Windows.Forms.Label();
            this.tarjetaCuotas = new System.Windows.Forms.Panel();
            this.tablaCuotas = new System.Windows.Forms.DataGridView();
            this.colIdMembresia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPeriodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoCuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSituacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cabeceraCuotas = new System.Windows.Forms.Panel();
            this.lblTituloCuotas = new System.Windows.Forms.Label();
            this.resumenCuotas = new System.Windows.Forms.Label();
            this.ayudaClima = new System.Windows.Forms.ToolTip(this.components);
            this.principal.SuspendLayout();
            this.panelCabecera.SuspendLayout();
            this.tarjetaClima.SuspendLayout();
            this.listaClima.SuspendLayout();
            this.tarjetaClimaEjemplo.SuspendLayout();
            this.cabeceraClima.SuspendLayout();
            this.tarjetaSuscripcion.SuspendLayout();
            this.tarjetaCuotas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaCuotas)).BeginInit();
            this.cabeceraCuotas.SuspendLayout();
            this.SuspendLayout();
            //
            // principal
            //
            this.principal.BackColor = System.Drawing.Color.Transparent;

            this.principal.Controls.Add(this.panelCabecera);
            this.principal.Controls.Add(this.tarjetaClima);
            this.principal.Controls.Add(this.tarjetaSuscripcion);
            this.principal.Controls.Add(this.tarjetaCuotas);

            this.principal.Name = "principal";
            this.principal.Padding = new System.Windows.Forms.Padding(20);

            this.principal.TabIndex = 0;
            //
            // panelCabecera
            //
            this.panelCabecera.BackColor = System.Drawing.Color.Transparent;
            this.panelCabecera.Controls.Add(this.lblResumen);
            this.panelCabecera.Controls.Add(this.lblFecha);

            this.panelCabecera.Name = "panelCabecera";

            this.panelCabecera.TabIndex = 0;
            //
            // lblResumen
            //

            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));

            this.lblResumen.Name = "lblResumen";

            this.lblResumen.TabIndex = 0;
            this.lblResumen.Text = "Resumen general";
            //
            // lblFecha
            //

            this.lblFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));

            this.lblFecha.Name = "lblFecha";

            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Resumen actualizado al iniciar";
            //
            // tarjetaClima
            //
            this.tarjetaClima.BackColor = System.Drawing.Color.White;
            this.tarjetaClima.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tarjetaClima.Controls.Add(this.listaClima);
            this.tarjetaClima.Controls.Add(this.cabeceraClima);

            this.tarjetaClima.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.tarjetaClima.Name = "tarjetaClima";

            this.tarjetaClima.TabIndex = 1;
            //
            // listaClima
            //

            this.listaClima.BackColor = System.Drawing.Color.White;
            this.listaClima.Controls.Add(this.tarjetaClimaEjemplo);

            this.listaClima.Name = "listaClima";
            this.listaClima.Padding = new System.Windows.Forms.Padding(10, 4, 10, 8);

            this.listaClima.TabIndex = 1;

            //
            // tarjetaClimaEjemplo
            //
            this.tarjetaClimaEjemplo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tarjetaClimaEjemplo.Controls.Add(this.lblLluviaEjemplo);
            this.tarjetaClimaEjemplo.Controls.Add(this.lblTemperaturaEjemplo);
            this.tarjetaClimaEjemplo.Controls.Add(this.lblDescripcionClimaEjemplo);
            this.tarjetaClimaEjemplo.Controls.Add(this.lblIconoEjemplo);
            this.tarjetaClimaEjemplo.Controls.Add(this.lblDiaEjemplo);

            this.tarjetaClimaEjemplo.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.tarjetaClimaEjemplo.Name = "tarjetaClimaEjemplo";

            this.tarjetaClimaEjemplo.TabIndex = 0;
            //
            // lblLluviaEjemplo
            //
            this.lblLluviaEjemplo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblLluviaEjemplo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));

            this.lblLluviaEjemplo.Name = "lblLluviaEjemplo";

            this.lblLluviaEjemplo.TabIndex = 0;
            this.lblLluviaEjemplo.Text = "Lluvia: 10%";
            this.lblLluviaEjemplo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblTemperaturaEjemplo
            //
            this.lblTemperaturaEjemplo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTemperaturaEjemplo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));

            this.lblTemperaturaEjemplo.Name = "lblTemperaturaEjemplo";

            this.lblTemperaturaEjemplo.TabIndex = 1;
            this.lblTemperaturaEjemplo.Text = "12° / 24°";
            this.lblTemperaturaEjemplo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblDescripcionClimaEjemplo
            //
            this.lblDescripcionClimaEjemplo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));

            this.lblDescripcionClimaEjemplo.Name = "lblDescripcionClimaEjemplo";

            this.lblDescripcionClimaEjemplo.TabIndex = 2;
            this.lblDescripcionClimaEjemplo.Text = "Despejado";
            this.lblDescripcionClimaEjemplo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblIconoEjemplo
            //
            this.lblIconoEjemplo.Font = new System.Drawing.Font("Segoe UI Symbol", 20F);
            this.lblIconoEjemplo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));

            this.lblIconoEjemplo.Name = "lblIconoEjemplo";

            this.lblIconoEjemplo.TabIndex = 3;
            this.lblIconoEjemplo.Text = "☀";
            this.lblIconoEjemplo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblDiaEjemplo
            //

            this.lblDiaEjemplo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiaEjemplo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblDiaEjemplo.Name = "lblDiaEjemplo";

            this.lblDiaEjemplo.TabIndex = 4;
            this.lblDiaEjemplo.Text = "HOY";
            this.lblDiaEjemplo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // cabeceraClima
            //
            this.cabeceraClima.BackColor = System.Drawing.Color.White;
            this.cabeceraClima.Controls.Add(this.tituloClima);
            this.cabeceraClima.Controls.Add(this.estadoClima);

            this.cabeceraClima.Name = "cabeceraClima";

            this.cabeceraClima.TabIndex = 0;
            //
            // tituloClima
            //

            this.tituloClima.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.tituloClima.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));

            this.tituloClima.Name = "tituloClima";

            this.tituloClima.TabIndex = 0;
            this.tituloClima.Text = "Pronostico semanal - Buenos Aires";
            //
            // estadoClima
            //

            this.estadoClima.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));

            this.estadoClima.Name = "estadoClima";

            this.estadoClima.TabIndex = 1;
            this.estadoClima.Text = "Tarjeta de ejemplo";
            //
            // tarjetaSuscripcion
            //
            this.tarjetaSuscripcion.BackColor = System.Drawing.Color.White;
            this.tarjetaSuscripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tarjetaSuscripcion.Controls.Add(this.lblSuscripcion);

            this.tarjetaSuscripcion.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.tarjetaSuscripcion.Name = "tarjetaSuscripcion";

            this.tarjetaSuscripcion.TabIndex = 2;
            //
            // lblSuscripcion
            //

            this.lblSuscripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSuscripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblSuscripcion.Name = "lblSuscripcion";
            this.lblSuscripcion.Padding = new System.Windows.Forms.Padding(18, 12, 18, 8);

            this.lblSuscripcion.TabIndex = 0;
            this.lblSuscripcion.Text = "CUOTAS MENSUALES\r\nUna membresia funciona como una suscripcion: cada periodo mensu" +
    "al debe tener una cuota. El socio esta al dia cuando no posee cuotas pendientes " +
    "y el periodo vigente ya fue generado.";
            //
            // tarjetaCuotas
            //
            this.tarjetaCuotas.BackColor = System.Drawing.Color.White;
            this.tarjetaCuotas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tarjetaCuotas.Controls.Add(this.tablaCuotas);
            this.tarjetaCuotas.Controls.Add(this.cabeceraCuotas);

            this.tarjetaCuotas.Margin = new System.Windows.Forms.Padding(0);
            this.tarjetaCuotas.Name = "tarjetaCuotas";

            this.tarjetaCuotas.TabIndex = 3;
            //
            // tablaCuotas
            //

            this.tablaCuotas.BackgroundColor = System.Drawing.Color.White;
            this.tablaCuotas.BorderStyle = System.Windows.Forms.BorderStyle.None;

            this.tablaCuotas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdMembresia,
            this.colSocio,
            this.colDni,
            this.colPlan,
            this.colPeriodo,
            this.colEstadoCuota,
            this.colSaldo,
            this.colSituacion});

            this.tablaCuotas.Name = "tablaCuotas";

            this.tablaCuotas.RowTemplate.Height = 32;

            this.tablaCuotas.TabIndex = 1;
            //
            // colIdMembresia
            //
            this.colIdMembresia.HeaderText = "N.";
            this.colIdMembresia.Name = "colIdMembresia";
            this.colIdMembresia.ReadOnly = true;
            //
            // colSocio
            //
            this.colSocio.HeaderText = "Socio";
            this.colSocio.Name = "colSocio";
            this.colSocio.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.HeaderText = "DNI";
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            //
            // colPlan
            //
            this.colPlan.HeaderText = "Plan";
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            //
            // colPeriodo
            //
            this.colPeriodo.HeaderText = "Ultimo periodo";
            this.colPeriodo.Name = "colPeriodo";
            this.colPeriodo.ReadOnly = true;
            //
            // colEstadoCuota
            //
            this.colEstadoCuota.HeaderText = "Ultima cuota";
            this.colEstadoCuota.Name = "colEstadoCuota";
            this.colEstadoCuota.ReadOnly = true;
            //
            // colSaldo
            //
            this.colSaldo.HeaderText = "Saldo pendiente";
            this.colSaldo.Name = "colSaldo";
            this.colSaldo.ReadOnly = true;
            //
            // colSituacion
            //
            this.colSituacion.HeaderText = "Situacion";
            this.colSituacion.Name = "colSituacion";
            this.colSituacion.ReadOnly = true;
            //
            // cabeceraCuotas
            //
            this.cabeceraCuotas.BackColor = System.Drawing.Color.White;
            this.cabeceraCuotas.Controls.Add(this.lblTituloCuotas);
            this.cabeceraCuotas.Controls.Add(this.resumenCuotas);

            this.cabeceraCuotas.Name = "cabeceraCuotas";

            this.cabeceraCuotas.TabIndex = 0;
            //
            // lblTituloCuotas
            //

            this.lblTituloCuotas.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloCuotas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));

            this.lblTituloCuotas.Name = "lblTituloCuotas";

            this.lblTituloCuotas.TabIndex = 0;
            this.lblTituloCuotas.Text = "Estado de cuenta de socios";
            //
            // resumenCuotas
            //

            this.resumenCuotas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));

            this.resumenCuotas.Name = "resumenCuotas";

            this.resumenCuotas.TabIndex = 1;
            this.resumenCuotas.Text = "Sin datos cargados";
            //
            // DashboardInicioAdministrador
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.principal);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "InicioPanelAdministrador";
            this.MinimumSize = new System.Drawing.Size(640, 460);
            this.Size = new System.Drawing.Size(1100, 640);

            // Columna unica: titulo, pronostico, aviso de cuotas y grilla que ocupa el resto del alto.

            this.principal.Padding = new System.Windows.Forms.Padding(20);

            this.panelCabecera.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);

            this.lblResumen.Margin = new System.Windows.Forms.Padding(0);
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblFecha.Margin = new System.Windows.Forms.Padding(0);
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // Tarjeta de pronostico: cabecera fija y lista de dias con desplazamiento horizontal si no entran.

            this.tarjetaClima.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);

            this.cabeceraClima.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);

            this.tituloClima.Margin = new System.Windows.Forms.Padding(0);
            this.tituloClima.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.estadoClima.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.estadoClima.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.listaClima.Padding = new System.Windows.Forms.Padding(10, 4, 10, 8);
            this.tarjetaClimaEjemplo.Location = new System.Drawing.Point(16, 6);
            this.tarjetaClimaEjemplo.Size = new System.Drawing.Size(148, 108);
            this.lblDiaEjemplo.AutoSize = false;
            this.lblDiaEjemplo.Location = new System.Drawing.Point(0, 4);
            this.lblDiaEjemplo.Size = new System.Drawing.Size(148, 22);
            this.lblIconoEjemplo.AutoSize = false;
            this.lblIconoEjemplo.Location = new System.Drawing.Point(8, 24);
            this.lblIconoEjemplo.Size = new System.Drawing.Size(132, 34);
            this.lblDescripcionClimaEjemplo.AutoSize = false;
            this.lblDescripcionClimaEjemplo.Location = new System.Drawing.Point(5, 58);
            this.lblDescripcionClimaEjemplo.Size = new System.Drawing.Size(138, 18);
            this.lblTemperaturaEjemplo.AutoSize = false;
            this.lblTemperaturaEjemplo.Location = new System.Drawing.Point(5, 76);
            this.lblTemperaturaEjemplo.Size = new System.Drawing.Size(138, 18);
            this.lblLluviaEjemplo.AutoSize = false;
            this.lblLluviaEjemplo.Location = new System.Drawing.Point(5, 92);
            this.lblLluviaEjemplo.Size = new System.Drawing.Size(138, 14);
            // Aviso de cuotas.

            this.tarjetaSuscripcion.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);

            this.lblSuscripcion.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // Estado de cuenta: la grilla ocupa el alto disponible.

            this.tarjetaCuotas.Margin = new System.Windows.Forms.Padding(0);

            this.cabeceraCuotas.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);

            this.lblTituloCuotas.Margin = new System.Windows.Forms.Padding(0);
            this.lblTituloCuotas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.resumenCuotas.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.resumenCuotas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.AutoScroll = false;
            this.principal.AutoSize = false;
            this.principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.principal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.principal.Location = new System.Drawing.Point(0, 0);
            this.principal.Size = new System.Drawing.Size(1100, 640);
            this.principal.AutoScroll = false;
            this.panelCabecera.AutoSize = false;
            this.panelCabecera.Dock = System.Windows.Forms.DockStyle.None;
            this.panelCabecera.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelCabecera.Location = new System.Drawing.Point(20, 20);
            this.panelCabecera.Size = new System.Drawing.Size(1060, 60);
            this.panelCabecera.AutoScroll = false;
            this.lblResumen.AutoSize = false;
            this.lblResumen.Dock = System.Windows.Forms.DockStyle.None;
            this.lblResumen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblResumen.Location = new System.Drawing.Point(0, 0);
            this.lblResumen.Size = new System.Drawing.Size(1060, 37);
            this.lblFecha.AutoSize = false;
            this.lblFecha.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblFecha.Location = new System.Drawing.Point(0, 37);
            this.lblFecha.Size = new System.Drawing.Size(1060, 23);
            this.tarjetaClima.AutoSize = false;
            this.tarjetaClima.Dock = System.Windows.Forms.DockStyle.None;
            this.tarjetaClima.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tarjetaClima.Location = new System.Drawing.Point(20, 88);
            this.tarjetaClima.Size = new System.Drawing.Size(1060, 174);
            this.tarjetaClima.AutoScroll = false;
            this.cabeceraClima.AutoSize = false;
            this.cabeceraClima.Dock = System.Windows.Forms.DockStyle.None;
            this.cabeceraClima.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cabeceraClima.Location = new System.Drawing.Point(0, 0);
            this.cabeceraClima.Size = new System.Drawing.Size(1058, 44);
            this.cabeceraClima.AutoScroll = false;
            this.tituloClima.AutoSize = false;
            this.tituloClima.Dock = System.Windows.Forms.DockStyle.None;
            this.tituloClima.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.tituloClima.Location = new System.Drawing.Point(16, 0);
            this.tituloClima.Size = new System.Drawing.Size(480, 44);
            this.estadoClima.AutoSize = false;
            this.estadoClima.Dock = System.Windows.Forms.DockStyle.None;
            this.estadoClima.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.estadoClima.Location = new System.Drawing.Point(624, 11);
            this.estadoClima.Size = new System.Drawing.Size(420, 22);
            this.listaClima.AutoSize = false;
            this.listaClima.Dock = System.Windows.Forms.DockStyle.None;
            this.listaClima.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.listaClima.Location = new System.Drawing.Point(0, 44);
            this.listaClima.Size = new System.Drawing.Size(1058, 128);
            this.listaClima.AutoScroll = true;
            this.tarjetaSuscripcion.AutoSize = false;
            this.tarjetaSuscripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.tarjetaSuscripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tarjetaSuscripcion.Location = new System.Drawing.Point(20, 274);
            this.tarjetaSuscripcion.Size = new System.Drawing.Size(1060, 92);
            this.tarjetaSuscripcion.AutoScroll = false;
            this.lblSuscripcion.AutoSize = false;
            this.lblSuscripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblSuscripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblSuscripcion.Location = new System.Drawing.Point(0, 0);
            this.lblSuscripcion.Size = new System.Drawing.Size(1058, 90);
            this.tarjetaCuotas.AutoSize = false;
            this.tarjetaCuotas.Dock = System.Windows.Forms.DockStyle.None;
            this.tarjetaCuotas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tarjetaCuotas.Location = new System.Drawing.Point(20, 378);
            this.tarjetaCuotas.Size = new System.Drawing.Size(1060, 242);
            this.tarjetaCuotas.AutoScroll = false;
            this.cabeceraCuotas.AutoSize = false;
            this.cabeceraCuotas.Dock = System.Windows.Forms.DockStyle.None;
            this.cabeceraCuotas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cabeceraCuotas.Location = new System.Drawing.Point(0, 0);
            this.cabeceraCuotas.Size = new System.Drawing.Size(1058, 44);
            this.cabeceraCuotas.AutoScroll = false;
            this.resumenCuotas.AutoSize = false;
            this.resumenCuotas.Dock = System.Windows.Forms.DockStyle.None;
            this.resumenCuotas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.resumenCuotas.Location = new System.Drawing.Point(494, 11);
            this.resumenCuotas.Size = new System.Drawing.Size(550, 22);
            this.tablaCuotas.AutoSize = false;
            this.tablaCuotas.Dock = System.Windows.Forms.DockStyle.None;
            this.tablaCuotas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tablaCuotas.Location = new System.Drawing.Point(0, 44);
            this.tablaCuotas.Size = new System.Drawing.Size(1058, 196);
            this.tablaCuotas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaCuotas.ReadOnly = true;
            this.tablaCuotas.AllowUserToAddRows = false;
            this.tablaCuotas.AllowUserToDeleteRows = false;
            this.tablaCuotas.AllowUserToResizeRows = false;
            this.tablaCuotas.RowHeadersVisible = false;
            this.tablaCuotas.MultiSelect = false;
            this.tablaCuotas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaCuotas.ColumnHeadersHeight = 46;
            this.lblTituloCuotas.AutoSize = false;
            this.lblTituloCuotas.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTituloCuotas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTituloCuotas.Location = new System.Drawing.Point(16, 0);
            this.lblTituloCuotas.Size = new System.Drawing.Size(360, 44);
            this.principal.ResumeLayout(false);
            this.panelCabecera.ResumeLayout(false);
            this.panelCabecera.PerformLayout();
            this.tarjetaClima.ResumeLayout(false);
            this.listaClima.ResumeLayout(false);
            this.tarjetaClimaEjemplo.ResumeLayout(false);
            this.cabeceraClima.ResumeLayout(false);
            this.cabeceraClima.PerformLayout();
            this.tarjetaSuscripcion.ResumeLayout(false);
            this.tarjetaCuotas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaCuotas)).EndInit();
            this.cabeceraCuotas.ResumeLayout(false);
            this.cabeceraCuotas.PerformLayout();
            this.ResumeLayout(false);

            this.Load += new System.EventHandler(this.InicioPanelAdministrador_Load);
                }

    }
}
