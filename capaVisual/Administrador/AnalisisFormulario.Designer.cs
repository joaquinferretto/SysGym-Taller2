namespace exxen2._0.capaVisual.Administrador
{
    partial class AnalisisFormulario
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel disposicion;
        private System.Windows.Forms.Panel encabezado;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker desde;
        private System.Windows.Forms.DateTimePicker hasta;
        private System.Windows.Forms.Button aplicar;
        private System.Windows.Forms.Button exportarReporte;
        private System.Windows.Forms.TableLayoutPanel indicadores;
        private System.Windows.Forms.Label lblIngresosTitulo;
        private System.Windows.Forms.Label lblPagosTitulo;
        private System.Windows.Forms.Label lblActivosTitulo;
        private System.Windows.Forms.Label lblDeudaTitulo;
        private System.Windows.Forms.Label lblIngresos;
        private System.Windows.Forms.Label lblPagos;
        private System.Windows.Forms.Label lblActivos;
        private System.Windows.Forms.Label lblDeuda;
        private System.Windows.Forms.Label lblComparacion;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblPorcentajeActivos;
        private System.Windows.Forms.TabControl categorias;
        private System.Windows.Forms.TabPage tabResumen;
        private System.Windows.Forms.TabPage tabFinanzas;
        private System.Windows.Forms.TabPage tabSocios;
        private System.Windows.Forms.TabPage tabPlanes;
        private System.Windows.Forms.TabPage tabEntrenadores;
        private System.Windows.Forms.TableLayoutPanel disposicionSocios;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartIngresos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMetodos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAltas;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartActivos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDeuda;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPlanes;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEntrenadores;
        private System.Windows.Forms.TableLayoutPanel disposicionPlanes;
        private System.Windows.Forms.TabPage tabRutinas;
        private System.Windows.Forms.TabPage tabEjercicios;
        private System.Windows.Forms.TabPage tabComparar;
        private System.Windows.Forms.TabPage tabSocio;
        private System.Windows.Forms.Label lblSinRutina;
        private System.Windows.Forms.TableLayoutPanel tablaEjercicios;
        private System.Windows.Forms.Label lblEjerciciosSinUso;
        private System.Windows.Forms.DataGridView gridEjerciciosSinUso;
        private System.Windows.Forms.TableLayoutPanel tablaComparar;
        private System.Windows.Forms.Panel filtrosComparar;
        private System.Windows.Forms.Label lblPeriodoA;
        private System.Windows.Forms.Label lblPeriodoB;
        private System.Windows.Forms.Label lblHastaA;
        private System.Windows.Forms.Label lblHastaB;
        private System.Windows.Forms.DateTimePicker desdeA;
        private System.Windows.Forms.DateTimePicker hastaA;
        private System.Windows.Forms.DateTimePicker desdeB;
        private System.Windows.Forms.DateTimePicker hastaB;
        private System.Windows.Forms.Button comparar;
        private System.Windows.Forms.TableLayoutPanel tablaChartsComparar;
        private System.Windows.Forms.DataGridView gridComparacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndicador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorB;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVariacion;
        private System.Windows.Forms.Label lblCompararEstado;
        private System.Windows.Forms.TableLayoutPanel tablaSocio;
        private System.Windows.Forms.Panel barraSocio;
        private System.Windows.Forms.Label lblBuscarSocio;
        private System.Windows.Forms.TextBox buscarSocio;
        private System.Windows.Forms.Button btnBuscarSocio;
        private System.Windows.Forms.ComboBox selectorSocio;
        private System.Windows.Forms.Label lblBusquedaEstado;
        private System.Windows.Forms.TableLayoutPanel tablaDetalleSocio;
        private System.Windows.Forms.DataGridView gridSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDatoSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorSocio;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartIngresosPlan;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRutinas;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEjercicios;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCompararIngresos;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCompararCantidades;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSocio;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title9 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title10 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea11 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title11 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea12 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title12 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea13 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title13 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.disposicion = new System.Windows.Forms.TableLayoutPanel();
            this.encabezado = new System.Windows.Forms.Panel();
            this.titulo = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.desde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.hasta = new System.Windows.Forms.DateTimePicker();
            this.aplicar = new System.Windows.Forms.Button();
            this.exportarReporte = new System.Windows.Forms.Button();
            this.indicadores = new System.Windows.Forms.TableLayoutPanel();
            this.lblIngresosTitulo = new System.Windows.Forms.Label();
            this.lblPagosTitulo = new System.Windows.Forms.Label();
            this.lblActivosTitulo = new System.Windows.Forms.Label();
            this.lblDeudaTitulo = new System.Windows.Forms.Label();
            this.lblIngresos = new System.Windows.Forms.Label();
            this.lblPagos = new System.Windows.Forms.Label();
            this.lblActivos = new System.Windows.Forms.Label();
            this.lblDeuda = new System.Windows.Forms.Label();
            this.lblComparacion = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.categorias = new System.Windows.Forms.TabControl();
            this.tabResumen = new System.Windows.Forms.TabPage();
            this.chartIngresos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabFinanzas = new System.Windows.Forms.TabPage();
            this.chartMetodos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabSocios = new System.Windows.Forms.TabPage();
            this.disposicionSocios = new System.Windows.Forms.TableLayoutPanel();
            this.lblPorcentajeActivos = new System.Windows.Forms.Label();
            this.chartAltas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartActivos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartDeuda = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPlanes = new System.Windows.Forms.TabPage();
            this.disposicionPlanes = new System.Windows.Forms.TableLayoutPanel();
            this.chartPlanes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartIngresosPlan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabEntrenadores = new System.Windows.Forms.TabPage();
            this.chartEntrenadores = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabRutinas = new System.Windows.Forms.TabPage();
            this.chartRutinas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblSinRutina = new System.Windows.Forms.Label();
            this.tabEjercicios = new System.Windows.Forms.TabPage();
            this.tablaEjercicios = new System.Windows.Forms.TableLayoutPanel();
            this.chartEjercicios = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblEjerciciosSinUso = new System.Windows.Forms.Label();
            this.gridEjerciciosSinUso = new System.Windows.Forms.DataGridView();
            this.tabComparar = new System.Windows.Forms.TabPage();
            this.tablaComparar = new System.Windows.Forms.TableLayoutPanel();
            this.filtrosComparar = new System.Windows.Forms.Panel();
            this.lblPeriodoA = new System.Windows.Forms.Label();
            this.lblPeriodoB = new System.Windows.Forms.Label();
            this.lblHastaA = new System.Windows.Forms.Label();
            this.lblHastaB = new System.Windows.Forms.Label();
            this.desdeA = new System.Windows.Forms.DateTimePicker();
            this.hastaA = new System.Windows.Forms.DateTimePicker();
            this.desdeB = new System.Windows.Forms.DateTimePicker();
            this.hastaB = new System.Windows.Forms.DateTimePicker();
            this.comparar = new System.Windows.Forms.Button();
            this.tablaChartsComparar = new System.Windows.Forms.TableLayoutPanel();
            this.chartCompararIngresos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCompararCantidades = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gridComparacion = new System.Windows.Forms.DataGridView();
            this.colIndicador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVariacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCompararEstado = new System.Windows.Forms.Label();
            this.tabSocio = new System.Windows.Forms.TabPage();
            this.tablaSocio = new System.Windows.Forms.TableLayoutPanel();
            this.barraSocio = new System.Windows.Forms.Panel();
            this.lblBuscarSocio = new System.Windows.Forms.Label();
            this.buscarSocio = new System.Windows.Forms.TextBox();
            this.btnBuscarSocio = new System.Windows.Forms.Button();
            this.selectorSocio = new System.Windows.Forms.ComboBox();
            this.lblBusquedaEstado = new System.Windows.Forms.Label();
            this.tablaDetalleSocio = new System.Windows.Forms.TableLayoutPanel();
            this.gridSocio = new System.Windows.Forms.DataGridView();
            this.colDatoSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartSocio = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.disposicion.SuspendLayout();
            this.encabezado.SuspendLayout();
            this.indicadores.SuspendLayout();
            this.categorias.SuspendLayout();
            this.tabResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartIngresos)).BeginInit();
            this.tabFinanzas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMetodos)).BeginInit();
            this.tabSocios.SuspendLayout();
            this.disposicionSocios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAltas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartActivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDeuda)).BeginInit();
            this.tabPlanes.SuspendLayout();
            this.disposicionPlanes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPlanes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartIngresosPlan)).BeginInit();
            this.tabEntrenadores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEntrenadores)).BeginInit();
            this.tabRutinas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRutinas)).BeginInit();
            this.tabEjercicios.SuspendLayout();
            this.tablaEjercicios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEjercicios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEjerciciosSinUso)).BeginInit();
            this.tabComparar.SuspendLayout();
            this.tablaComparar.SuspendLayout();
            this.filtrosComparar.SuspendLayout();
            this.tablaChartsComparar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompararIngresos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompararCantidades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridComparacion)).BeginInit();
            this.tabSocio.SuspendLayout();
            this.tablaSocio.SuspendLayout();
            this.barraSocio.SuspendLayout();
            this.tablaDetalleSocio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSocio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSocio)).BeginInit();
            this.SuspendLayout();
            //
            // disposicion
            //
            this.disposicion.ColumnCount = 1;
            this.disposicion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicion.Controls.Add(this.encabezado, 0, 0);
            this.disposicion.Controls.Add(this.indicadores, 0, 1);
            this.disposicion.Controls.Add(this.lblComparacion, 0, 2);
            this.disposicion.Controls.Add(this.lblEstado, 0, 4);
            this.disposicion.Controls.Add(this.categorias, 0, 3);
            this.disposicion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disposicion.Location = new System.Drawing.Point(0, 0);
            this.disposicion.Name = "disposicion";
            this.disposicion.RowCount = 5;
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.disposicion.Size = new System.Drawing.Size(1080, 700);
            this.disposicion.TabIndex = 0;
            this.disposicion.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            //
            // encabezado
            //
            this.encabezado.BackColor = System.Drawing.Color.White;
            this.encabezado.Controls.Add(this.titulo);
            this.encabezado.Controls.Add(this.lblDesde);
            this.encabezado.Controls.Add(this.desde);
            this.encabezado.Controls.Add(this.lblHasta);
            this.encabezado.Controls.Add(this.hasta);
            this.encabezado.Controls.Add(this.aplicar);
            this.encabezado.Controls.Add(this.exportarReporte);
            this.encabezado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encabezado.Location = new System.Drawing.Point(3, 3);
            this.encabezado.Name = "encabezado";
            this.encabezado.Size = new System.Drawing.Size(1074, 98);
            this.encabezado.TabIndex = 0;
            //
            // titulo
            //
            this.titulo.AutoSize = true;
            this.titulo.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.titulo.Location = new System.Drawing.Point(18, 8);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(257, 35);
            this.titulo.TabIndex = 0;
            this.titulo.Text = "Análisis del gimnasio";
            //
            // lblDesde
            //
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(20, 62);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(56, 21);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde:";
            //
            // desde
            //
            this.desde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.desde.Location = new System.Drawing.Point(82, 59);
            this.desde.Name = "desde";
            this.desde.Size = new System.Drawing.Size(130, 29);
            this.desde.TabIndex = 2;
            this.desde.ValueChanged += new System.EventHandler(this.periodoReporte_Cambiado);
            //
            // lblHasta
            //
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(226, 62);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(52, 21);
            this.lblHasta.TabIndex = 3;
            this.lblHasta.Text = "Hasta:";
            //
            // hasta
            //
            this.hasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hasta.Location = new System.Drawing.Point(282, 59);
            this.hasta.Name = "hasta";
            this.hasta.Size = new System.Drawing.Size(130, 29);
            this.hasta.TabIndex = 4;
            this.hasta.ValueChanged += new System.EventHandler(this.periodoReporte_Cambiado);
            //
            // aplicar
            //
            this.aplicar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.aplicar.FlatAppearance.BorderSize = 0;
            this.aplicar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.aplicar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.aplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aplicar.ForeColor = System.Drawing.Color.White;
            this.aplicar.Location = new System.Drawing.Point(430, 56);
            this.aplicar.Name = "aplicar";
            this.aplicar.Size = new System.Drawing.Size(96, 30);
            this.aplicar.TabIndex = 5;
            this.aplicar.Text = "Aplicar";
            this.aplicar.UseVisualStyleBackColor = false;
            this.aplicar.Click += new System.EventHandler(this.aplicar_Click);
            //
            // exportarReporte
            //
            this.exportarReporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.exportarReporte.Enabled = false;
            this.exportarReporte.FlatAppearance.BorderSize = 0;
            this.exportarReporte.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.exportarReporte.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.exportarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportarReporte.ForeColor = System.Drawing.Color.White;
            this.exportarReporte.Location = new System.Drawing.Point(542, 56);
            this.exportarReporte.Name = "exportarReporte";
            this.exportarReporte.Size = new System.Drawing.Size(168, 30);
            this.exportarReporte.TabIndex = 3;
            this.exportarReporte.Text = "Exportar reporte PDF";
            this.exportarReporte.UseVisualStyleBackColor = false;
            this.exportarReporte.Click += new System.EventHandler(this.exportarReporte_Click);
            //
            // indicadores
            //
            this.indicadores.BackColor = System.Drawing.Color.White;
            this.indicadores.ColumnCount = 4;
            this.indicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.indicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.indicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.indicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.indicadores.Controls.Add(this.lblIngresosTitulo, 0, 0);
            this.indicadores.Controls.Add(this.lblPagosTitulo, 1, 0);
            this.indicadores.Controls.Add(this.lblActivosTitulo, 2, 0);
            this.indicadores.Controls.Add(this.lblDeudaTitulo, 3, 0);
            this.indicadores.Controls.Add(this.lblIngresos, 0, 1);
            this.indicadores.Controls.Add(this.lblPagos, 1, 1);
            this.indicadores.Controls.Add(this.lblActivos, 2, 1);
            this.indicadores.Controls.Add(this.lblDeuda, 3, 1);
            this.indicadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indicadores.Location = new System.Drawing.Point(3, 107);
            this.indicadores.Name = "indicadores";
            this.indicadores.RowCount = 2;
            this.indicadores.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.indicadores.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.indicadores.Size = new System.Drawing.Size(1074, 72);
            this.indicadores.TabIndex = 1;
            //
            // lblIngresosTitulo
            //
            this.lblIngresosTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIngresosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblIngresosTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblIngresosTitulo.Name = "lblIngresosTitulo";
            this.lblIngresosTitulo.Padding = new System.Windows.Forms.Padding(12, 8, 0, 0);
            this.lblIngresosTitulo.Size = new System.Drawing.Size(262, 32);
            this.lblIngresosTitulo.TabIndex = 0;
            this.lblIngresosTitulo.Text = "Ingresos del período";
            this.lblIngresosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblPagosTitulo
            //
            this.lblPagosTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPagosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPagosTitulo.Location = new System.Drawing.Point(271, 0);
            this.lblPagosTitulo.Name = "lblPagosTitulo";
            this.lblPagosTitulo.Padding = new System.Windows.Forms.Padding(12, 8, 0, 0);
            this.lblPagosTitulo.Size = new System.Drawing.Size(257, 32);
            this.lblPagosTitulo.TabIndex = 1;
            this.lblPagosTitulo.Text = "Pagos aprobados";
            this.lblPagosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblActivosTitulo
            //
            this.lblActivosTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActivosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblActivosTitulo.Location = new System.Drawing.Point(534, 0);
            this.lblActivosTitulo.Name = "lblActivosTitulo";
            this.lblActivosTitulo.Padding = new System.Windows.Forms.Padding(12, 8, 0, 0);
            this.lblActivosTitulo.Size = new System.Drawing.Size(267, 32);
            this.lblActivosTitulo.TabIndex = 2;
            this.lblActivosTitulo.Text = "Socios activos";
            this.lblActivosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblDeudaTitulo
            //
            this.lblDeudaTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDeudaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDeudaTitulo.Location = new System.Drawing.Point(807, 0);
            this.lblDeudaTitulo.Name = "lblDeudaTitulo";
            this.lblDeudaTitulo.Padding = new System.Windows.Forms.Padding(12, 8, 0, 0);
            this.lblDeudaTitulo.Size = new System.Drawing.Size(264, 32);
            this.lblDeudaTitulo.TabIndex = 3;
            this.lblDeudaTitulo.Text = "Socios con deuda";
            this.lblDeudaTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblIngresos
            //
            this.lblIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIngresos.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblIngresos.Location = new System.Drawing.Point(3, 32);
            this.lblIngresos.Name = "lblIngresos";
            this.lblIngresos.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblIngresos.Size = new System.Drawing.Size(262, 40);
            this.lblIngresos.TabIndex = 4;
            this.lblIngresos.Text = "—";
            this.lblIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblPagos
            //
            this.lblPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPagos.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblPagos.Location = new System.Drawing.Point(271, 32);
            this.lblPagos.Name = "lblPagos";
            this.lblPagos.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblPagos.Size = new System.Drawing.Size(257, 40);
            this.lblPagos.TabIndex = 5;
            this.lblPagos.Text = "—";
            this.lblPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblActivos
            //
            this.lblActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActivos.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblActivos.Location = new System.Drawing.Point(534, 32);
            this.lblActivos.Name = "lblActivos";
            this.lblActivos.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblActivos.Size = new System.Drawing.Size(267, 40);
            this.lblActivos.TabIndex = 6;
            this.lblActivos.Text = "—";
            this.lblActivos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblDeuda
            //
            this.lblDeuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDeuda.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblDeuda.Location = new System.Drawing.Point(807, 32);
            this.lblDeuda.Name = "lblDeuda";
            this.lblDeuda.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblDeuda.Size = new System.Drawing.Size(264, 40);
            this.lblDeuda.TabIndex = 7;
            this.lblDeuda.Text = "—";
            this.lblDeuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblComparacion
            //
            this.lblComparacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComparacion.Location = new System.Drawing.Point(3, 182);
            this.lblComparacion.Name = "lblComparacion";
            this.lblComparacion.Padding = new System.Windows.Forms.Padding(15, 3, 0, 0);
            this.lblComparacion.Size = new System.Drawing.Size(1074, 30);
            this.lblComparacion.TabIndex = 2;
            this.lblComparacion.Text = "Período anterior: —   |   Variación: —";
            //
            // lblEstado
            //
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Location = new System.Drawing.Point(3, 666);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblEstado.Size = new System.Drawing.Size(1074, 34);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Seleccione el período y pulse Aplicar.";
            //
            // categorias
            //
            this.categorias.Controls.Add(this.tabResumen);
            this.categorias.Controls.Add(this.tabFinanzas);
            this.categorias.Controls.Add(this.tabSocios);
            this.categorias.Controls.Add(this.tabPlanes);
            this.categorias.Controls.Add(this.tabEntrenadores);
            this.categorias.Controls.Add(this.tabRutinas);
            this.categorias.Controls.Add(this.tabEjercicios);
            this.categorias.Controls.Add(this.tabComparar);
            this.categorias.Controls.Add(this.tabSocio);
            this.categorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categorias.Location = new System.Drawing.Point(3, 215);
            this.categorias.Multiline = true;
            this.categorias.Name = "categorias";
            this.categorias.SelectedIndex = 0;
            this.categorias.Size = new System.Drawing.Size(1074, 448);
            this.categorias.TabIndex = 3;
            //
            // tabResumen
            //
            this.tabResumen.Controls.Add(this.chartIngresos);
            this.tabResumen.Location = new System.Drawing.Point(4, 30);
            this.tabResumen.Name = "tabResumen";
            this.tabResumen.Size = new System.Drawing.Size(1066, 414);
            this.tabResumen.TabIndex = 0;
            this.tabResumen.Text = "Resumen";
            this.tabResumen.UseVisualStyleBackColor = true;
            //
            // chartIngresos
            //
            chartArea1.Name = "Principal";
            this.chartIngresos.ChartAreas.Add(chartArea1);
            this.chartIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartIngresos.Location = new System.Drawing.Point(0, 0);
            this.chartIngresos.Name = "chartIngresos";
            this.chartIngresos.Size = new System.Drawing.Size(1066, 414);
            this.chartIngresos.TabIndex = 0;
            title1.Name = "Title1";
            title1.Text = "Ingresos por mes";
            this.chartIngresos.Titles.Add(title1);
            //
            // tabFinanzas
            //
            this.tabFinanzas.Controls.Add(this.chartMetodos);
            this.tabFinanzas.Location = new System.Drawing.Point(4, 30);
            this.tabFinanzas.Name = "tabFinanzas";
            this.tabFinanzas.Size = new System.Drawing.Size(1066, 414);
            this.tabFinanzas.TabIndex = 1;
            this.tabFinanzas.Text = "Finanzas";
            this.tabFinanzas.UseVisualStyleBackColor = true;
            //
            // chartMetodos
            //
            chartArea2.Name = "Principal";
            this.chartMetodos.ChartAreas.Add(chartArea2);
            this.chartMetodos.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Categorias";
            this.chartMetodos.Legends.Add(legend1);
            this.chartMetodos.Location = new System.Drawing.Point(0, 0);
            this.chartMetodos.Name = "chartMetodos";
            this.chartMetodos.Size = new System.Drawing.Size(1066, 414);
            this.chartMetodos.TabIndex = 0;
            title2.Name = "Title1";
            title2.Text = "Métodos de pago";
            this.chartMetodos.Titles.Add(title2);
            //
            // tabSocios
            //
            this.tabSocios.Controls.Add(this.disposicionSocios);
            this.tabSocios.Location = new System.Drawing.Point(4, 30);
            this.tabSocios.Name = "tabSocios";
            this.tabSocios.Size = new System.Drawing.Size(1066, 414);
            this.tabSocios.TabIndex = 2;
            this.tabSocios.Text = "Socios";
            this.tabSocios.UseVisualStyleBackColor = true;
            //
            // disposicionSocios
            //
            this.disposicionSocios.ColumnCount = 3;
            this.disposicionSocios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.disposicionSocios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.disposicionSocios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.disposicionSocios.Controls.Add(this.lblPorcentajeActivos, 0, 0);
            this.disposicionSocios.Controls.Add(this.chartAltas, 0, 1);
            this.disposicionSocios.Controls.Add(this.chartActivos, 1, 1);
            this.disposicionSocios.Controls.Add(this.chartDeuda, 2, 1);
            this.disposicionSocios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disposicionSocios.Location = new System.Drawing.Point(0, 0);
            this.disposicionSocios.Name = "disposicionSocios";
            this.disposicionSocios.RowCount = 2;
            this.disposicionSocios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.disposicionSocios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicionSocios.Size = new System.Drawing.Size(1066, 414);
            this.disposicionSocios.TabIndex = 0;
            //
            // lblPorcentajeActivos
            //
            this.disposicionSocios.SetColumnSpan(this.lblPorcentajeActivos, 3);
            this.lblPorcentajeActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPorcentajeActivos.Location = new System.Drawing.Point(3, 0);
            this.lblPorcentajeActivos.Name = "lblPorcentajeActivos";
            this.lblPorcentajeActivos.Size = new System.Drawing.Size(1060, 50);
            this.lblPorcentajeActivos.TabIndex = 0;
            this.lblPorcentajeActivos.Text = "Total socios: —";
            //
            // chartAltas
            //
            chartArea3.Name = "Principal";
            this.chartAltas.ChartAreas.Add(chartArea3);
            this.chartAltas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartAltas.Location = new System.Drawing.Point(3, 53);
            this.chartAltas.Name = "chartAltas";
            this.chartAltas.Size = new System.Drawing.Size(527, 176);
            this.chartAltas.TabIndex = 1;
            title3.Name = "Title1";
            title3.Text = "Altas de socios por mes";
            this.chartAltas.Titles.Add(title3);
            //
            // chartActivos
            //
            chartArea4.Name = "Principal";
            this.chartActivos.ChartAreas.Add(chartArea4);
            this.chartActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Categorias";
            this.chartActivos.Legends.Add(legend2);
            this.chartActivos.Location = new System.Drawing.Point(536, 53);
            this.chartActivos.Name = "chartActivos";
            this.chartActivos.Size = new System.Drawing.Size(527, 176);
            this.chartActivos.TabIndex = 2;
            title4.Name = "Title1";
            title4.Text = "Socios activos e inactivos";
            this.chartActivos.Titles.Add(title4);
            //
            // chartDeuda
            //
            chartArea5.Name = "Principal";
            this.chartDeuda.ChartAreas.Add(chartArea5);
            this.disposicionSocios.SetColumnSpan(this.chartDeuda, 1);
            this.chartDeuda.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Categorias";
            this.chartDeuda.Legends.Add(legend3);
            this.chartDeuda.Location = new System.Drawing.Point(3, 235);
            this.chartDeuda.Name = "chartDeuda";
            this.chartDeuda.Size = new System.Drawing.Size(1060, 176);
            this.chartDeuda.TabIndex = 3;
            title5.Name = "Title1";
            title5.Text = "Estado de deuda";
            this.chartDeuda.Titles.Add(title5);
            //
            // tabPlanes
            //
            this.tabPlanes.Controls.Add(this.disposicionPlanes);
            this.tabPlanes.Location = new System.Drawing.Point(4, 30);
            this.tabPlanes.Name = "tabPlanes";
            this.tabPlanes.Size = new System.Drawing.Size(1066, 414);
            this.tabPlanes.TabIndex = 3;
            this.tabPlanes.Text = "Planes";
            this.tabPlanes.UseVisualStyleBackColor = true;
            //
            // disposicionPlanes
            //
            this.disposicionPlanes.ColumnCount = 2;
            this.disposicionPlanes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.disposicionPlanes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.disposicionPlanes.Controls.Add(this.chartPlanes, 0, 0);
            this.disposicionPlanes.Controls.Add(this.chartIngresosPlan, 1, 0);
            this.disposicionPlanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disposicionPlanes.Location = new System.Drawing.Point(0, 0);
            this.disposicionPlanes.Name = "disposicionPlanes";
            this.disposicionPlanes.RowCount = 1;
            this.disposicionPlanes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicionPlanes.Size = new System.Drawing.Size(1066, 414);
            this.disposicionPlanes.TabIndex = 0;
            //
            // chartPlanes
            //
            chartArea6.Name = "Principal";
            this.chartPlanes.ChartAreas.Add(chartArea6);
            this.chartPlanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPlanes.Location = new System.Drawing.Point(3, 3);
            this.chartPlanes.Name = "chartPlanes";
            this.chartPlanes.Size = new System.Drawing.Size(527, 408);
            this.chartPlanes.TabIndex = 0;
            title6.Name = "Title1";
            title6.Text = "Planes más contratados";
            this.chartPlanes.Titles.Add(title6);
            //
            // chartIngresosPlan
            //
            chartArea7.Name = "Principal";
            this.chartIngresosPlan.ChartAreas.Add(chartArea7);
            this.chartIngresosPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartIngresosPlan.Location = new System.Drawing.Point(536, 3);
            this.chartIngresosPlan.Name = "chartIngresosPlan";
            this.chartIngresosPlan.Size = new System.Drawing.Size(527, 408);
            this.chartIngresosPlan.TabIndex = 1;
            title7.Name = "Title1";
            title7.Text = "Ingresos reales por plan";
            this.chartIngresosPlan.Titles.Add(title7);
            //
            // tabEntrenadores
            //
            this.tabEntrenadores.Controls.Add(this.chartEntrenadores);
            this.tabEntrenadores.Location = new System.Drawing.Point(4, 30);
            this.tabEntrenadores.Name = "tabEntrenadores";
            this.tabEntrenadores.Size = new System.Drawing.Size(1066, 414);
            this.tabEntrenadores.TabIndex = 4;
            this.tabEntrenadores.Text = "Entrenadores";
            this.tabEntrenadores.UseVisualStyleBackColor = true;
            //
            // chartEntrenadores
            //
            chartArea8.Name = "Principal";
            this.chartEntrenadores.ChartAreas.Add(chartArea8);
            this.chartEntrenadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEntrenadores.Location = new System.Drawing.Point(0, 0);
            this.chartEntrenadores.Name = "chartEntrenadores";
            this.chartEntrenadores.Size = new System.Drawing.Size(1066, 414);
            this.chartEntrenadores.TabIndex = 0;
            title8.Name = "Title1";
            title8.Text = "Socios por entrenador";
            this.chartEntrenadores.Titles.Add(title8);
            //
            // tabRutinas
            //
            this.tabRutinas.Controls.Add(this.chartRutinas);
            this.tabRutinas.Controls.Add(this.lblSinRutina);
            this.tabRutinas.Location = new System.Drawing.Point(4, 30);
            this.tabRutinas.Name = "tabRutinas";
            this.tabRutinas.Size = new System.Drawing.Size(1066, 414);
            this.tabRutinas.TabIndex = 5;
            this.tabRutinas.Text = "Rutinas";
            this.tabRutinas.UseVisualStyleBackColor = true;
            //
            // chartRutinas
            //
            chartArea9.Name = "Principal";
            this.chartRutinas.ChartAreas.Add(chartArea9);
            this.chartRutinas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartRutinas.Location = new System.Drawing.Point(0, 0);
            this.chartRutinas.Name = "chartRutinas";
            this.chartRutinas.Size = new System.Drawing.Size(1066, 384);
            this.chartRutinas.TabIndex = 0;
            title9.Name = "Title1";
            title9.Text = "Rutinas más utilizadas";
            this.chartRutinas.Titles.Add(title9);
            //
            // lblSinRutina
            //
            this.lblSinRutina.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSinRutina.Location = new System.Drawing.Point(0, 384);
            this.lblSinRutina.Name = "lblSinRutina";
            this.lblSinRutina.Size = new System.Drawing.Size(1066, 30);
            this.lblSinRutina.TabIndex = 1;
            this.lblSinRutina.Text = "Socios activos sin rutina activa: —";
            //
            // tabEjercicios
            //
            this.tabEjercicios.Controls.Add(this.tablaEjercicios);
            this.tabEjercicios.Location = new System.Drawing.Point(4, 30);
            this.tabEjercicios.Name = "tabEjercicios";
            this.tabEjercicios.Size = new System.Drawing.Size(1066, 414);
            this.tabEjercicios.TabIndex = 6;
            this.tabEjercicios.Text = "Ejercicios";
            this.tabEjercicios.UseVisualStyleBackColor = true;
            //
            // tablaEjercicios
            //
            this.tablaEjercicios.ColumnCount = 2;
            this.tablaEjercicios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tablaEjercicios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tablaEjercicios.Controls.Add(this.chartEjercicios, 0, 0);
            this.tablaEjercicios.Controls.Add(this.lblEjerciciosSinUso, 1, 0);
            this.tablaEjercicios.Controls.Add(this.gridEjerciciosSinUso, 1, 1);
            this.tablaEjercicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaEjercicios.Location = new System.Drawing.Point(0, 0);
            this.tablaEjercicios.Name = "tablaEjercicios";
            this.tablaEjercicios.RowCount = 2;
            this.tablaEjercicios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tablaEjercicios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaEjercicios.Size = new System.Drawing.Size(1066, 414);
            this.tablaEjercicios.TabIndex = 0;
            //
            // chartEjercicios
            //
            chartArea10.Name = "Principal";
            this.chartEjercicios.ChartAreas.Add(chartArea10);
            this.chartEjercicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEjercicios.Location = new System.Drawing.Point(3, 3);
            this.chartEjercicios.Name = "chartEjercicios";
            this.tablaEjercicios.SetRowSpan(this.chartEjercicios, 2);
            this.chartEjercicios.Size = new System.Drawing.Size(633, 408);
            this.chartEjercicios.TabIndex = 0;
            title10.Name = "Title1";
            title10.Text = "Top 10 ejercicios: rutinas distintas";
            this.chartEjercicios.Titles.Add(title10);
            //
            // lblEjerciciosSinUso
            //
            this.lblEjerciciosSinUso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEjerciciosSinUso.Location = new System.Drawing.Point(642, 0);
            this.lblEjerciciosSinUso.Name = "lblEjerciciosSinUso";
            this.lblEjerciciosSinUso.Size = new System.Drawing.Size(421, 32);
            this.lblEjerciciosSinUso.TabIndex = 1;
            this.lblEjerciciosSinUso.Text = "Ejercicios activos sin uso: —";
            //
            // gridEjerciciosSinUso
            //
            this.gridEjerciciosSinUso.AllowUserToAddRows = false;
            this.gridEjerciciosSinUso.AllowUserToDeleteRows = false;
            this.gridEjerciciosSinUso.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridEjerciciosSinUso.BackgroundColor = System.Drawing.Color.White;
            this.gridEjerciciosSinUso.ColumnHeadersHeight = 29;
            this.gridEjerciciosSinUso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEjerciciosSinUso.Location = new System.Drawing.Point(642, 35);
            this.gridEjerciciosSinUso.Name = "gridEjerciciosSinUso";
            this.gridEjerciciosSinUso.ReadOnly = true;
            this.gridEjerciciosSinUso.RowHeadersVisible = false;
            this.gridEjerciciosSinUso.RowHeadersWidth = 51;
            this.gridEjerciciosSinUso.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridEjerciciosSinUso.Size = new System.Drawing.Size(421, 376);
            this.gridEjerciciosSinUso.TabIndex = 2;
            //
            // tabComparar
            //
            this.tabComparar.Controls.Add(this.tablaComparar);
            this.tabComparar.Location = new System.Drawing.Point(4, 30);
            this.tabComparar.Name = "tabComparar";
            this.tabComparar.Size = new System.Drawing.Size(1066, 414);
            this.tabComparar.TabIndex = 7;
            this.tabComparar.Text = "Comparar";
            this.tabComparar.UseVisualStyleBackColor = true;
            //
            // tablaComparar
            //
            this.tablaComparar.ColumnCount = 2;
            this.tablaComparar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaComparar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tablaComparar.Controls.Add(this.filtrosComparar, 0, 0);
            this.tablaComparar.Controls.Add(this.tablaChartsComparar, 0, 1);
            this.tablaComparar.Controls.Add(this.gridComparacion, 1, 1);
            this.tablaComparar.Controls.Add(this.lblCompararEstado, 0, 2);
            this.tablaComparar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaComparar.Location = new System.Drawing.Point(0, 0);
            this.tablaComparar.Name = "tablaComparar";
            this.tablaComparar.RowCount = 3;
            this.tablaComparar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tablaComparar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaComparar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tablaComparar.Size = new System.Drawing.Size(1066, 414);
            this.tablaComparar.TabIndex = 0;
            //
            // filtrosComparar
            //
            this.tablaComparar.SetColumnSpan(this.filtrosComparar, 2);
            this.filtrosComparar.Controls.Add(this.lblPeriodoA);
            this.filtrosComparar.Controls.Add(this.lblPeriodoB);
            this.filtrosComparar.Controls.Add(this.lblHastaA);
            this.filtrosComparar.Controls.Add(this.lblHastaB);
            this.filtrosComparar.Controls.Add(this.desdeA);
            this.filtrosComparar.Controls.Add(this.hastaA);
            this.filtrosComparar.Controls.Add(this.desdeB);
            this.filtrosComparar.Controls.Add(this.hastaB);
            this.filtrosComparar.Controls.Add(this.comparar);
            this.filtrosComparar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtrosComparar.Location = new System.Drawing.Point(3, 3);
            this.filtrosComparar.Name = "filtrosComparar";
            this.filtrosComparar.Size = new System.Drawing.Size(1060, 82);
            this.filtrosComparar.TabIndex = 0;
            //
            // lblPeriodoA
            //
            this.lblPeriodoA.Location = new System.Drawing.Point(12, 13);
            this.lblPeriodoA.Name = "lblPeriodoA";
            this.lblPeriodoA.Size = new System.Drawing.Size(96, 25);
            this.lblPeriodoA.TabIndex = 0;
            this.lblPeriodoA.Text = "Período A";
            this.lblPeriodoA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblPeriodoB
            //
            this.lblPeriodoB.Location = new System.Drawing.Point(12, 51);
            this.lblPeriodoB.Name = "lblPeriodoB";
            this.lblPeriodoB.Size = new System.Drawing.Size(96, 25);
            this.lblPeriodoB.TabIndex = 1;
            this.lblPeriodoB.Text = "Período B";
            this.lblPeriodoB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblHastaA
            //
            this.lblHastaA.Location = new System.Drawing.Point(284, 13);
            this.lblHastaA.Name = "lblHastaA";
            this.lblHastaA.Size = new System.Drawing.Size(52, 25);
            this.lblHastaA.TabIndex = 2;
            this.lblHastaA.Text = "Hasta";
            this.lblHastaA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblHastaB
            //
            this.lblHastaB.Location = new System.Drawing.Point(284, 51);
            this.lblHastaB.Name = "lblHastaB";
            this.lblHastaB.Size = new System.Drawing.Size(52, 25);
            this.lblHastaB.TabIndex = 3;
            this.lblHastaB.Text = "Hasta";
            this.lblHastaB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // desdeA
            //
            this.desdeA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.desdeA.Location = new System.Drawing.Point(116, 13);
            this.desdeA.Name = "desdeA";
            this.desdeA.Size = new System.Drawing.Size(138, 29);
            this.desdeA.TabIndex = 4;
            this.desdeA.ValueChanged += new System.EventHandler(this.periodoComparacion_Cambiado);
            //
            // hastaA
            //
            this.hastaA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hastaA.Location = new System.Drawing.Point(338, 13);
            this.hastaA.Name = "hastaA";
            this.hastaA.Size = new System.Drawing.Size(138, 29);
            this.hastaA.TabIndex = 5;
            this.hastaA.ValueChanged += new System.EventHandler(this.periodoComparacion_Cambiado);
            //
            // desdeB
            //
            this.desdeB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.desdeB.Location = new System.Drawing.Point(116, 51);
            this.desdeB.Name = "desdeB";
            this.desdeB.Size = new System.Drawing.Size(138, 29);
            this.desdeB.TabIndex = 6;
            this.desdeB.ValueChanged += new System.EventHandler(this.periodoComparacion_Cambiado);
            //
            // hastaB
            //
            this.hastaB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hastaB.Location = new System.Drawing.Point(338, 51);
            this.hastaB.Name = "hastaB";
            this.hastaB.Size = new System.Drawing.Size(138, 29);
            this.hastaB.TabIndex = 7;
            this.hastaB.ValueChanged += new System.EventHandler(this.periodoComparacion_Cambiado);
            //
            // comparar
            //
            this.comparar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comparar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.comparar.FlatAppearance.BorderSize = 0;
            this.comparar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.comparar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.comparar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comparar.ForeColor = System.Drawing.Color.White;
            this.comparar.Location = new System.Drawing.Point(900, 25);
            this.comparar.Name = "comparar";
            this.comparar.Size = new System.Drawing.Size(148, 32);
            this.comparar.TabIndex = 8;
            this.comparar.Text = "Comparar períodos";
            this.comparar.UseVisualStyleBackColor = false;
            this.comparar.Click += new System.EventHandler(this.comparar_Click);
            //
            // tablaChartsComparar
            //
            this.tablaChartsComparar.ColumnCount = 2;
            this.tablaChartsComparar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablaChartsComparar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablaChartsComparar.Controls.Add(this.chartCompararIngresos, 0, 0);
            this.tablaChartsComparar.Controls.Add(this.chartCompararCantidades, 1, 0);
            this.tablaChartsComparar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaChartsComparar.Location = new System.Drawing.Point(3, 91);
            this.tablaChartsComparar.Name = "tablaChartsComparar";
            this.tablaChartsComparar.RowCount = 1;
            this.tablaChartsComparar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaChartsComparar.Size = new System.Drawing.Size(718, 282);
            this.tablaChartsComparar.TabIndex = 1;
            //
            // chartCompararIngresos
            //
            chartArea11.Name = "Principal";
            this.chartCompararIngresos.ChartAreas.Add(chartArea11);
            this.chartCompararIngresos.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Periodos";
            this.chartCompararIngresos.Legends.Add(legend4);
            this.chartCompararIngresos.Location = new System.Drawing.Point(3, 3);
            this.chartCompararIngresos.Name = "chartCompararIngresos";
            this.chartCompararIngresos.Size = new System.Drawing.Size(353, 276);
            this.chartCompararIngresos.TabIndex = 0;
            title11.Name = "Title1";
            title11.Text = "Ingresos: A vs B";
            this.chartCompararIngresos.Titles.Add(title11);
            //
            // chartCompararCantidades
            //
            chartArea12.Name = "Principal";
            this.chartCompararCantidades.ChartAreas.Add(chartArea12);
            this.chartCompararCantidades.Dock = System.Windows.Forms.DockStyle.Fill;
            legend5.Name = "Periodos";
            this.chartCompararCantidades.Legends.Add(legend5);
            this.chartCompararCantidades.Location = new System.Drawing.Point(362, 3);
            this.chartCompararCantidades.Name = "chartCompararCantidades";
            this.chartCompararCantidades.Size = new System.Drawing.Size(353, 276);
            this.chartCompararCantidades.TabIndex = 1;
            title12.Name = "Title1";
            title12.Text = "Pagos y altas: A vs B";
            this.chartCompararCantidades.Titles.Add(title12);
            //
            // gridComparacion
            //
            this.gridComparacion.AllowUserToAddRows = false;
            this.gridComparacion.AllowUserToDeleteRows = false;
            this.gridComparacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridComparacion.BackgroundColor = System.Drawing.Color.White;
            this.gridComparacion.ColumnHeadersHeight = 29;
            this.gridComparacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndicador,
            this.colValorA,
            this.colValorB,
            this.colVariacion});
            this.gridComparacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridComparacion.Location = new System.Drawing.Point(727, 91);
            this.gridComparacion.Name = "gridComparacion";
            this.gridComparacion.ReadOnly = true;
            this.gridComparacion.RowHeadersVisible = false;
            this.gridComparacion.RowHeadersWidth = 51;
            this.gridComparacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridComparacion.Size = new System.Drawing.Size(336, 282);
            this.gridComparacion.TabIndex = 2;
            //
            // colIndicador
            //
            this.colIndicador.FillWeight = 90F;
            this.colIndicador.HeaderText = "Indicador";
            this.colIndicador.MinimumWidth = 6;
            this.colIndicador.Name = "colIndicador";
            this.colIndicador.ReadOnly = true;
            this.colIndicador.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colValorA
            //
            this.colValorA.FillWeight = 120F;
            this.colValorA.HeaderText = "Valor A";
            this.colValorA.MinimumWidth = 6;
            this.colValorA.Name = "colValorA";
            this.colValorA.ReadOnly = true;
            this.colValorA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colValorB
            //
            this.colValorB.FillWeight = 120F;
            this.colValorB.HeaderText = "Valor B";
            this.colValorB.MinimumWidth = 6;
            this.colValorB.Name = "colValorB";
            this.colValorB.ReadOnly = true;
            this.colValorB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colVariacion
            //
            this.colVariacion.FillWeight = 90F;
            this.colVariacion.HeaderText = "Variación";
            this.colVariacion.MinimumWidth = 6;
            this.colVariacion.Name = "colVariacion";
            this.colVariacion.ReadOnly = true;
            this.colVariacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // lblCompararEstado
            //
            this.tablaComparar.SetColumnSpan(this.lblCompararEstado, 2);
            this.lblCompararEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCompararEstado.Location = new System.Drawing.Point(3, 376);
            this.lblCompararEstado.Name = "lblCompararEstado";
            this.lblCompararEstado.Size = new System.Drawing.Size(1060, 38);
            this.lblCompararEstado.TabIndex = 3;
            this.lblCompararEstado.Text = "Seleccione A y B y pulse Comparar períodos. No hay historial suficiente de deuda." +
    "";
            //
            // tabSocio
            //
            this.tabSocio.Controls.Add(this.tablaSocio);
            this.tabSocio.Location = new System.Drawing.Point(4, 30);
            this.tabSocio.Name = "tabSocio";
            this.tabSocio.Size = new System.Drawing.Size(1066, 414);
            this.tabSocio.TabIndex = 8;
            this.tabSocio.Text = "Socio";
            this.tabSocio.UseVisualStyleBackColor = true;
            //
            // tablaSocio
            //
            this.tablaSocio.ColumnCount = 1;
            this.tablaSocio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaSocio.Controls.Add(this.barraSocio, 0, 0);
            this.tablaSocio.Controls.Add(this.tablaDetalleSocio, 0, 1);
            this.tablaSocio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaSocio.Location = new System.Drawing.Point(0, 0);
            this.tablaSocio.Name = "tablaSocio";
            this.tablaSocio.RowCount = 2;
            this.tablaSocio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tablaSocio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaSocio.Size = new System.Drawing.Size(1066, 414);
            this.tablaSocio.TabIndex = 0;
            //
            // barraSocio
            //
            this.barraSocio.Controls.Add(this.lblBuscarSocio);
            this.barraSocio.Controls.Add(this.buscarSocio);
            this.barraSocio.Controls.Add(this.btnBuscarSocio);
            this.barraSocio.Controls.Add(this.selectorSocio);
            this.barraSocio.Controls.Add(this.lblBusquedaEstado);
            this.barraSocio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barraSocio.Location = new System.Drawing.Point(3, 3);
            this.barraSocio.Name = "barraSocio";
            this.barraSocio.Size = new System.Drawing.Size(1060, 100);
            this.barraSocio.TabIndex = 0;
            //
            // lblBuscarSocio
            //
            this.lblBuscarSocio.Location = new System.Drawing.Point(12, 12);
            this.lblBuscarSocio.Name = "lblBuscarSocio";
            this.lblBuscarSocio.Size = new System.Drawing.Size(110, 24);
            this.lblBuscarSocio.TabIndex = 0;
            this.lblBuscarSocio.Text = "Nombre o DNI:";
            //
            // buscarSocio
            //
            this.buscarSocio.Location = new System.Drawing.Point(126, 10);
            this.buscarSocio.Name = "buscarSocio";
            this.buscarSocio.Size = new System.Drawing.Size(254, 29);
            this.buscarSocio.TabIndex = 1;
            //
            // btnBuscarSocio
            //
            this.btnBuscarSocio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnBuscarSocio.FlatAppearance.BorderSize = 0;
            this.btnBuscarSocio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnBuscarSocio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnBuscarSocio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarSocio.ForeColor = System.Drawing.Color.White;
            this.btnBuscarSocio.Location = new System.Drawing.Point(392, 8);
            this.btnBuscarSocio.Name = "btnBuscarSocio";
            this.btnBuscarSocio.Size = new System.Drawing.Size(96, 30);
            this.btnBuscarSocio.TabIndex = 2;
            this.btnBuscarSocio.Text = "Buscar";
            this.btnBuscarSocio.UseVisualStyleBackColor = false;
            this.btnBuscarSocio.Click += new System.EventHandler(this.buscarSocio_Click);
            //
            // selectorSocio
            //
            this.selectorSocio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectorSocio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectorSocio.Location = new System.Drawing.Point(12, 44);
            this.selectorSocio.Name = "selectorSocio";
            this.selectorSocio.Size = new System.Drawing.Size(1032, 29);
            this.selectorSocio.TabIndex = 3;
            this.selectorSocio.SelectedIndexChanged += new System.EventHandler(this.selectorSocio_SelectedIndexChanged);
            //
            // lblBusquedaEstado
            //
            this.lblBusquedaEstado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBusquedaEstado.Location = new System.Drawing.Point(12, 74);
            this.lblBusquedaEstado.Name = "lblBusquedaEstado";
            this.lblBusquedaEstado.Size = new System.Drawing.Size(1032, 26);
            this.lblBusquedaEstado.TabIndex = 4;
            this.lblBusquedaEstado.Text = "Busque y seleccione un socio; consulta sin edición.";
            //
            // tablaDetalleSocio
            //
            this.tablaDetalleSocio.ColumnCount = 2;
            this.tablaDetalleSocio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tablaDetalleSocio.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this.tablaDetalleSocio.Controls.Add(this.gridSocio, 0, 0);
            this.tablaDetalleSocio.Controls.Add(this.chartSocio, 1, 0);
            this.tablaDetalleSocio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaDetalleSocio.Location = new System.Drawing.Point(3, 109);
            this.tablaDetalleSocio.Name = "tablaDetalleSocio";
            this.tablaDetalleSocio.RowCount = 1;
            this.tablaDetalleSocio.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaDetalleSocio.Size = new System.Drawing.Size(1060, 302);
            this.tablaDetalleSocio.TabIndex = 1;
            //
            // gridSocio
            //
            this.gridSocio.AllowUserToAddRows = false;
            this.gridSocio.AllowUserToDeleteRows = false;
            this.gridSocio.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSocio.BackgroundColor = System.Drawing.Color.White;
            this.gridSocio.ColumnHeadersHeight = 29;
            this.gridSocio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDatoSocio,
            this.colValorSocio});
            this.gridSocio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSocio.Location = new System.Drawing.Point(3, 3);
            this.gridSocio.Name = "gridSocio";
            this.gridSocio.ReadOnly = true;
            this.gridSocio.RowHeadersVisible = false;
            this.gridSocio.RowHeadersWidth = 51;
            this.gridSocio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSocio.Size = new System.Drawing.Size(481, 296);
            this.gridSocio.TabIndex = 0;
            //
            // colDatoSocio
            //
            this.colDatoSocio.HeaderText = "Dato";
            this.colDatoSocio.MinimumWidth = 6;
            this.colDatoSocio.Name = "colDatoSocio";
            this.colDatoSocio.ReadOnly = true;
            this.colDatoSocio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colValorSocio
            //
            this.colValorSocio.HeaderText = "Valor";
            this.colValorSocio.MinimumWidth = 6;
            this.colValorSocio.Name = "colValorSocio";
            this.colValorSocio.ReadOnly = true;
            this.colValorSocio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // chartSocio
            //
            chartArea13.Name = "Principal";
            this.chartSocio.ChartAreas.Add(chartArea13);
            this.chartSocio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartSocio.Location = new System.Drawing.Point(490, 3);
            this.chartSocio.Name = "chartSocio";
            this.chartSocio.Size = new System.Drawing.Size(567, 296);
            this.chartSocio.TabIndex = 1;
            title13.Name = "Title1";
            title13.Text = "Pagos históricos del socio";
            this.chartSocio.Titles.Add(title13);
            //
            // AnalisisFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1080, 700);
            this.Controls.Add(this.disposicion);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "AnalisisFormulario";
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.AnalisisFormulario_Load);
            this.disposicion.ResumeLayout(false);
            this.encabezado.ResumeLayout(false);
            this.encabezado.PerformLayout();
            this.indicadores.ResumeLayout(false);
            this.categorias.ResumeLayout(false);
            this.tabResumen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartIngresos)).EndInit();
            this.tabFinanzas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMetodos)).EndInit();
            this.tabSocios.ResumeLayout(false);
            this.disposicionSocios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartAltas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartActivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDeuda)).EndInit();
            this.tabPlanes.ResumeLayout(false);
            this.disposicionPlanes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPlanes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartIngresosPlan)).EndInit();
            this.tabEntrenadores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEntrenadores)).EndInit();
            this.tabRutinas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRutinas)).EndInit();
            this.tabEjercicios.ResumeLayout(false);
            this.tablaEjercicios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEjercicios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEjerciciosSinUso)).EndInit();
            this.tabComparar.ResumeLayout(false);
            this.tablaComparar.ResumeLayout(false);
            this.filtrosComparar.ResumeLayout(false);
            this.tablaChartsComparar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartCompararIngresos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompararCantidades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridComparacion)).EndInit();
            this.tabSocio.ResumeLayout(false);
            this.tablaSocio.ResumeLayout(false);
            this.barraSocio.ResumeLayout(false);
            this.barraSocio.PerformLayout();
            this.tablaDetalleSocio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSocio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSocio)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
