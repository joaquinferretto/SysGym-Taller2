using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionSociosFormulario
    {
        private System.Windows.Forms.PictureBox fotoSocio;
        private System.Windows.Forms.Button btnSeleccionarFoto;
        private System.Windows.Forms.Button btnQuitarFoto;
        private System.Windows.Forms.ComboBox sexo;
        private System.Windows.Forms.Label lblSexo;

        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstado;
        private TextBox nombre;
        private TextBox apellido;
        private TextBox dni;
        private DateTimePicker fechaNacimiento;
        private TextBox peso;
        private TextBox altura;
        private Button nuevo;
        private Button guardar;
        private Button actualizar;
        private Button darDeBaja;
        private Button reactivar;
        private Button calcularImc;
        private Button verRutina;
        private ComboBox filtroEstado;
        private TextBox buscador;
        private Panel panelContenido;
        private Panel contenedorContenido;
        private Panel panelListado;
        private Label lblListado;
        private Label lblAyuda;
        private Panel panelFiltro;
        private Label lblFiltro;
        private Panel panelDetalle;
        private Panel contenedorDetalle;
        private Panel contenedorCampos;
        private Panel panelAcciones;
        private Label lblFormulario;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private Label lblFechaNacimiento;
        private Label lblPeso;
        private Label lblAltura;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDni;
        private DataGridViewTextBoxColumn colNacimiento;
        private DataGridViewTextBoxColumn colEstado;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.fotoSocio = new System.Windows.Forms.PictureBox();
            this.btnSeleccionarFoto = new System.Windows.Forms.Button();
            this.btnQuitarFoto = new System.Windows.Forms.Button();
            this.sexo = new System.Windows.Forms.ComboBox();
            this.lblSexo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).BeginInit();
            components = new Container();
            panelEncabezado = new Panel();
            lblTitulo = new Label();
            lblDescripcion = new Label();
            btnVolver = new Button();
            barraAcciones = new Panel();
            lblEstado = new Label();
            panelContenido = new Panel();
            contenedorContenido = new Panel();
            panelListado = new Panel();
            lblListado = new Label();
            lblAyuda = new Label();
            panelFiltro = new Panel();
            lblFiltro = new Label();
            buscador = new TextBox();
            filtroEstado = new ComboBox();
            tabla = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colDni = new DataGridViewTextBoxColumn();
            colNacimiento = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            panelDetalle = new Panel();
            contenedorDetalle = new Panel();
            lblFormulario = new Label();
            contenedorCampos = new Panel();
            lblNombre = new Label();
            nombre = new TextBox();
            lblApellido = new Label();
            apellido = new TextBox();
            lblDni = new Label();
            dni = new TextBox();
            lblFechaNacimiento = new Label();
            fechaNacimiento = new DateTimePicker();
            lblPeso = new Label();
            peso = new TextBox();
            lblAltura = new Label();
            altura = new TextBox();
            panelAcciones = new Panel();
            nuevo = new Button();
            guardar = new Button();
            actualizar = new Button();
            darDeBaja = new Button();
            reactivar = new Button();
            calcularImc = new Button();
            verRutina = new Button();
            panelEncabezado.SuspendLayout();
            panelContenido.SuspendLayout();
            contenedorContenido.SuspendLayout();
            panelListado.SuspendLayout();
            panelFiltro.SuspendLayout();
            ((ISupportInitialize)(tabla)).BeginInit();
            panelDetalle.SuspendLayout();
            contenedorDetalle.SuspendLayout();
            contenedorCampos.SuspendLayout();
            panelAcciones.SuspendLayout();
            SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);

            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Controls.Add(lblDescripcion);
            panelEncabezado.Controls.Add(btnVolver);

            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;

            lblTitulo.Text = "Socios";

            lblDescripcion.Font = new Font("Segoe UI", 9.5F);
            lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);

            lblDescripcion.Text = "Alta, actualizacion, baja logica y consulta de IMC";

            btnVolver.BackColor = Color.White;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            btnVolver.ForeColor = Color.FromArgb(79, 70, 229);

            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;

            barraAcciones.BackColor = Color.White;

            barraAcciones.Padding = new Padding(16, 8, 16, 8);
            barraAcciones.Visible = true;
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo";
            panelContenido.BackColor = Color.FromArgb(248, 250, 252);

            panelContenido.Padding = new Padding(12);

            contenedorContenido.Controls.Add(panelListado);
            contenedorContenido.Controls.Add(panelDetalle);
            panelContenido.Controls.Add(contenedorContenido);

            panelListado.BackColor = Color.White;
            panelListado.BorderStyle = BorderStyle.FixedSingle;

            panelListado.Padding = new Padding(16);
            panelListado.Controls.Add(lblListado);
            panelListado.Controls.Add(lblAyuda);
            panelListado.Controls.Add(panelFiltro);
            panelListado.Controls.Add(tabla);

            lblListado.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblListado.ForeColor = Color.FromArgb(30, 41, 59);

            lblListado.Text = "Listado de socios";

            lblAyuda.ForeColor = Color.FromArgb(100, 116, 139);

            lblAyuda.Text = "Busca por nombre o DNI";

            panelFiltro.BackColor = Color.White;

            panelFiltro.Controls.Add(buscador);
            panelFiltro.Controls.Add(lblFiltro);
            panelFiltro.Controls.Add(filtroEstado);

            lblFiltro.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblFiltro.ForeColor = Color.FromArgb(71, 85, 105);

            lblFiltro.Text = "Estado";

            buscador.BorderStyle = BorderStyle.FixedSingle;

            filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            filtroEstado.SelectedIndex = 0;

            tabla.BackgroundColor = Color.White;
            tabla.BorderStyle = BorderStyle.None;

            tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colDni, colNacimiento, colEstado });
            colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false;
            colNombre.HeaderText = "Nombre"; colNombre.Name = "colNombre"; colNombre.FillWeight = 150;
            colDni.HeaderText = "DNI"; colDni.Name = "colDni"; colDni.FillWeight = 95;
            colNacimiento.HeaderText = "Nacimiento"; colNacimiento.Name = "colNacimiento"; colNacimiento.FillWeight = 95;
            colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado"; colEstado.FillWeight = 70;

            panelDetalle.BackColor = Color.White;
            panelDetalle.BorderStyle = BorderStyle.FixedSingle;

            panelDetalle.Padding = new Padding(16);

            contenedorDetalle.Controls.Add(lblFormulario);
            contenedorDetalle.Controls.Add(contenedorCampos);
            contenedorDetalle.Controls.Add(panelAcciones);
            panelDetalle.Controls.Add(contenedorDetalle);

            lblFormulario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblFormulario.ForeColor = Color.FromArgb(30, 41, 59);
            lblFormulario.Text = "Nuevo socio";

            contenedorCampos.Controls.Add(lblNombre); contenedorCampos.Controls.Add(nombre);
            contenedorCampos.Controls.Add(lblApellido); contenedorCampos.Controls.Add(apellido);
            contenedorCampos.Controls.Add(lblDni); contenedorCampos.Controls.Add(dni);
            contenedorCampos.Controls.Add(lblFechaNacimiento); contenedorCampos.Controls.Add(fechaNacimiento);
            contenedorCampos.Controls.Add(lblPeso); contenedorCampos.Controls.Add(peso);
            contenedorCampos.Controls.Add(lblAltura); contenedorCampos.Controls.Add(altura);
            fechaNacimiento.Format = DateTimePickerFormat.Short;
            fechaNacimiento.ShowCheckBox = true;

            panelAcciones.Controls.Add(nuevo);
            panelAcciones.Controls.Add(guardar);
            panelAcciones.Controls.Add(actualizar);
            panelAcciones.Controls.Add(darDeBaja);
            panelAcciones.Controls.Add(reactivar);
            panelAcciones.Controls.Add(calcularImc);
             panelEncabezado.Name = "panelEncabezado";  panelEncabezado.TabIndex = 0; lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0; lblDescripcion.Name = "lblDescripcion";  lblDescripcion.TabIndex = 1; btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1;  lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;  panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;  contenedorContenido.Name = "contenedorContenido";  contenedorContenido.TabIndex = 0;
            panelListado.Name = "panelListado"; panelListado.TabIndex = 0; lblListado.Name = "lblListado";  lblListado.TabIndex = 0; lblAyuda.Name = "lblAyuda";  lblAyuda.TabIndex = 1; panelFiltro.Name = "panelFiltro"; panelFiltro.TabIndex = 2; lblFiltro.Name = "lblFiltro"; lblFiltro.TabIndex = 1; buscador.Name = "buscador"; buscador.TabIndex = 0; filtroEstado.Name = "filtroEstado"; filtroEstado.TabIndex = 2;
               tabla.Name = "tabla";  tabla.TabIndex = 3; colNombre.MinimumWidth = 180; colDni.MinimumWidth = 110; colNacimiento.MinimumWidth = 120; colEstado.MinimumWidth = 90;
            panelDetalle.Name = "panelDetalle"; panelDetalle.TabIndex = 1; contenedorDetalle.Name = "contenedorDetalle"; contenedorDetalle.TabIndex = 0; lblFormulario.Name = "lblFormulario";  lblFormulario.TabIndex = 0; contenedorCampos.Name = "contenedorCampos";  contenedorCampos.TabIndex = 1; panelAcciones.Name = "panelAcciones"; panelAcciones.TabIndex = 2;
              lblNombre.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblNombre.ForeColor = Color.FromArgb(51, 65, 85); lblNombre.Name = "lblNombre"; lblNombre.Text = "Nombre:";   lblApellido.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblApellido.ForeColor = Color.FromArgb(51, 65, 85); lblApellido.Name = "lblApellido"; lblApellido.Text = "Apellido:";   lblDni.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblDni.ForeColor = Color.FromArgb(51, 65, 85); lblDni.Name = "lblDni"; lblDni.Text = "DNI:";   lblFechaNacimiento.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblFechaNacimiento.ForeColor = Color.FromArgb(51, 65, 85); lblFechaNacimiento.Name = "lblFechaNacimiento"; lblFechaNacimiento.Text = "Nacimiento:";   lblPeso.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblPeso.ForeColor = Color.FromArgb(51, 65, 85); lblPeso.Name = "lblPeso"; lblPeso.Text = "Peso (kg):";   lblAltura.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblAltura.ForeColor = Color.FromArgb(51, 65, 85); lblAltura.Name = "lblAltura"; lblAltura.Text = "Altura (m):";
            nombre.BorderStyle = BorderStyle.FixedSingle;  nombre.Name = "nombre"; apellido.BorderStyle = BorderStyle.FixedSingle;  apellido.Name = "apellido"; dni.BorderStyle = BorderStyle.FixedSingle;  dni.Name = "dni"; fechaNacimiento.Name = "fechaNacimiento"; peso.BorderStyle = BorderStyle.FixedSingle;  peso.Name = "peso"; altura.BorderStyle = BorderStyle.FixedSingle;  altura.Name = "altura";
            nuevo.BackColor = Color.FromArgb(79, 70, 229); nuevo.FlatAppearance.BorderSize = 0; nuevo.FlatStyle = FlatStyle.Flat; nuevo.ForeColor = Color.White;  nuevo.Name = "nuevo";  nuevo.Text = "+ Nuevo socio"; nuevo.UseVisualStyleBackColor = false; guardar.BackColor = Color.FromArgb(79, 70, 229); guardar.FlatAppearance.BorderSize = 0; guardar.FlatStyle = FlatStyle.Flat; guardar.ForeColor = Color.White;  guardar.Name = "guardar";  guardar.Text = "Guardar"; guardar.UseVisualStyleBackColor = false; actualizar.BackColor = Color.FromArgb(226, 232, 240); actualizar.FlatAppearance.BorderSize = 0; actualizar.FlatStyle = FlatStyle.Flat; actualizar.ForeColor = Color.FromArgb(30, 41, 59);  actualizar.Name = "actualizar";  actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false; darDeBaja.BackColor = Color.FromArgb(254, 242, 242); darDeBaja.FlatAppearance.BorderSize = 0; darDeBaja.FlatStyle = FlatStyle.Flat; darDeBaja.ForeColor = Color.FromArgb(185, 28, 28);  darDeBaja.Name = "darDeBaja";  darDeBaja.Text = "Dar de baja"; darDeBaja.UseVisualStyleBackColor = false; reactivar.BackColor = Color.FromArgb(226, 232, 240); reactivar.FlatAppearance.BorderSize = 0; reactivar.FlatStyle = FlatStyle.Flat; reactivar.ForeColor = Color.FromArgb(30, 41, 59);  reactivar.Name = "reactivar";  reactivar.Text = "Reactivar"; reactivar.UseVisualStyleBackColor = false; calcularImc.BackColor = Color.FromArgb(226, 232, 240); calcularImc.FlatAppearance.BorderSize = 0; calcularImc.FlatStyle = FlatStyle.Flat; calcularImc.ForeColor = Color.FromArgb(30, 41, 59);  calcularImc.Name = "calcularImc";  calcularImc.Text = "Calcular IMC"; calcularImc.UseVisualStyleBackColor = false; verRutina.BackColor = Color.FromArgb(226, 232, 240); verRutina.FlatAppearance.BorderSize = 0; verRutina.FlatStyle = FlatStyle.Flat; verRutina.ForeColor = Color.FromArgb(30, 41, 59);  verRutina.Name = "verRutina";  verRutina.Text = "Ver rutina semanal"; verRutina.UseVisualStyleBackColor = false;
            Controls.Add(panelContenido);
            Controls.Add(lblEstado);
            Controls.Add(barraAcciones);
            Controls.Add(panelEncabezado);
            AutoScaleMode = AutoScaleMode.Font;

            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1100, 680);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(900, 560);
            this.Name = "GestionSociosFormulario";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SysGym | Socios";

            // Encabezado superior en dos filas con el boton de regreso a la derecha.

            panelEncabezado.Padding = new Padding(22, 8, 22, 8);

            lblTitulo.Margin = new Padding(0);
            lblTitulo.TextAlign = ContentAlignment.BottomLeft;

            lblDescripcion.Margin = new Padding(0);
            lblDescripcion.TextAlign = ContentAlignment.TopLeft;

            btnVolver.Margin = new Padding(16, 0, 0, 0);
            // Separador fino entre el encabezado y el area de trabajo.

            barraAcciones.Padding = new Padding(0);
            barraAcciones.BackColor = Color.FromArgb(203, 213, 225);
            // Barra de estado inferior.

            lblEstado.Padding = new Padding(18, 0, 12, 0);
            lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            // El area de trabajo ocupa todo el espacio restante de la ventana.

            panelContenido.Padding = new Padding(16);

            // Listado: titulo, ayuda, filtros y grilla que crece con el contenedor.

            panelListado.Margin = new Padding(0, 0, 16, 0);
            panelListado.Padding = new Padding(16);

            lblListado.Margin = new Padding(0);
            lblListado.TextAlign = ContentAlignment.MiddleLeft;

            lblAyuda.Margin = new Padding(0, 0, 0, 6);
            lblAyuda.TextAlign = ContentAlignment.MiddleLeft;

            panelFiltro.Margin = new Padding(0, 0, 0, 10);

            buscador.Margin = new Padding(0, 8, 12, 8);

            lblFiltro.Margin = new Padding(0, 0, 8, 0);
            lblFiltro.TextAlign = ContentAlignment.MiddleRight;

            filtroEstado.Margin = new Padding(0, 8, 0, 8);

            tabla.Margin = new Padding(0);
            tabla.RowTemplate.Height = 30;
            // Detalle: se desplaza solo cuando la altura disponible no alcanza.

            panelDetalle.Margin = new Padding(0);
            panelDetalle.Padding = new Padding(16);

            lblFormulario.Margin = new Padding(0, 0, 0, 12);
            lblFormulario.TextAlign = ContentAlignment.MiddleLeft;

            contenedorCampos.Margin = new Padding(0, 0, 0, 16);

            lblNombre.Margin = new Padding(0, 0, 8, 8);
            lblNombre.TextAlign = ContentAlignment.MiddleLeft;

            nombre.Margin = new Padding(0, 3, 0, 8);

            lblApellido.Margin = new Padding(0, 0, 8, 8);
            lblApellido.TextAlign = ContentAlignment.MiddleLeft;

            apellido.Margin = new Padding(0, 3, 0, 8);

            lblDni.Margin = new Padding(0, 0, 8, 8);
            lblDni.TextAlign = ContentAlignment.MiddleLeft;

            dni.Margin = new Padding(0, 3, 0, 8);

            lblFechaNacimiento.Margin = new Padding(0, 0, 8, 8);
            lblFechaNacimiento.TextAlign = ContentAlignment.MiddleLeft;

            fechaNacimiento.Margin = new Padding(0, 3, 0, 8);

            lblPeso.Margin = new Padding(0, 0, 8, 8);
            lblPeso.TextAlign = ContentAlignment.MiddleLeft;

            peso.Margin = new Padding(0, 3, 0, 8);

            lblAltura.Margin = new Padding(0, 0, 8, 8);
            lblAltura.TextAlign = ContentAlignment.MiddleLeft;

            altura.Margin = new Padding(0, 3, 0, 8);

            panelAcciones.Margin = new Padding(0);

            nuevo.Margin = new Padding(0, 0, 8, 8);

            guardar.Margin = new Padding(0, 0, 8, 8);

            actualizar.Margin = new Padding(0, 0, 8, 8);

            darDeBaja.Margin = new Padding(0, 0, 8, 8);

            reactivar.Margin = new Padding(0, 0, 8, 8);

            calcularImc.Margin = new Padding(0, 0, 8, 8);

            this.AutoScroll = false;
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 84);
            this.panelEncabezado.AutoScroll = false;
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Size = new System.Drawing.Size(890, 36);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblDescripcion.Location = new System.Drawing.Point(24, 48);
            this.lblDescripcion.Size = new System.Drawing.Size(890, 26);
            this.btnVolver.AutoSize = false;
            this.btnVolver.Dock = System.Windows.Forms.DockStyle.None;
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.Location = new System.Drawing.Point(974, 24);
            this.btnVolver.Size = new System.Drawing.Size(104, 38);
            this.barraAcciones.AutoSize = false;
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.barraAcciones.Location = new System.Drawing.Point(0, 84);
            this.barraAcciones.Size = new System.Drawing.Size(1100, 1);
            this.barraAcciones.AutoScroll = false;
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Size = new System.Drawing.Size(1100, 30);
            this.nombre.AutoSize = false;
            this.nombre.Dock = System.Windows.Forms.DockStyle.None;
            this.nombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.nombre.Location = new System.Drawing.Point(124, 4);
            this.nombre.Size = new System.Drawing.Size(222, 24);
            this.apellido.AutoSize = false;
            this.apellido.Dock = System.Windows.Forms.DockStyle.None;
            this.apellido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.apellido.Location = new System.Drawing.Point(124, 42);
            this.apellido.Size = new System.Drawing.Size(222, 24);
            this.dni.AutoSize = false;
            this.dni.Dock = System.Windows.Forms.DockStyle.None;
            this.dni.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dni.Location = new System.Drawing.Point(124, 80);
            this.dni.Size = new System.Drawing.Size(222, 24);
            this.fechaNacimiento.AutoSize = false;
            this.fechaNacimiento.Dock = System.Windows.Forms.DockStyle.None;
            this.fechaNacimiento.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.fechaNacimiento.Location = new System.Drawing.Point(124, 118);
            this.fechaNacimiento.Size = new System.Drawing.Size(222, 24);
            this.peso.AutoSize = false;
            this.peso.Dock = System.Windows.Forms.DockStyle.None;
            this.peso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.peso.Location = new System.Drawing.Point(124, 156);
            this.peso.Size = new System.Drawing.Size(222, 24);
            this.altura.AutoSize = false;
            this.altura.Dock = System.Windows.Forms.DockStyle.None;
            this.altura.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.altura.Location = new System.Drawing.Point(124, 194);
            this.altura.Size = new System.Drawing.Size(222, 24);
            this.nuevo.AutoSize = false;
            this.nuevo.Dock = System.Windows.Forms.DockStyle.None;
            this.nuevo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nuevo.Location = new System.Drawing.Point(0, 0);
            this.nuevo.Size = new System.Drawing.Size(112, 38);
            this.guardar.AutoSize = false;
            this.guardar.Dock = System.Windows.Forms.DockStyle.None;
            this.guardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.guardar.Location = new System.Drawing.Point(120, 0);
            this.guardar.Size = new System.Drawing.Size(112, 38);
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(240, 0);
            this.actualizar.Size = new System.Drawing.Size(112, 38);
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.Dock = System.Windows.Forms.DockStyle.None;
            this.darDeBaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.darDeBaja.Location = new System.Drawing.Point(0, 46);
            this.darDeBaja.Size = new System.Drawing.Size(112, 38);
            this.reactivar.AutoSize = false;
            this.reactivar.Dock = System.Windows.Forms.DockStyle.None;
            this.reactivar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.reactivar.Location = new System.Drawing.Point(120, 46);
            this.reactivar.Size = new System.Drawing.Size(112, 38);
            this.calcularImc.AutoSize = false;
            this.calcularImc.Dock = System.Windows.Forms.DockStyle.None;
            this.calcularImc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.calcularImc.Location = new System.Drawing.Point(240, 46);
            this.calcularImc.Size = new System.Drawing.Size(112, 38);
            this.filtroEstado.AutoSize = false;
            this.filtroEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.filtroEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.filtroEstado.Location = new System.Drawing.Point(474, 8);
            this.filtroEstado.Size = new System.Drawing.Size(148, 26);
            this.buscador.AutoSize = false;
            this.buscador.Dock = System.Windows.Forms.DockStyle.None;
            this.buscador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.buscador.Location = new System.Drawing.Point(0, 9);
            this.buscador.Size = new System.Drawing.Size(412, 26);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(0, 85);
            this.panelContenido.Size = new System.Drawing.Size(1100, 565);
            this.panelContenido.AutoScroll = false;
            this.contenedorContenido.AutoSize = false;
            this.contenedorContenido.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorContenido.Location = new System.Drawing.Point(16, 16);
            this.contenedorContenido.Size = new System.Drawing.Size(1068, 533);
            this.contenedorContenido.AutoScroll = false;
            this.panelListado.AutoSize = false;
            this.panelListado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelListado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelListado.Location = new System.Drawing.Point(0, 0);
            this.panelListado.Size = new System.Drawing.Size(656, 533);
            this.panelListado.AutoScroll = false;
            this.lblListado.AutoSize = false;
            this.lblListado.Dock = System.Windows.Forms.DockStyle.None;
            this.lblListado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblListado.Location = new System.Drawing.Point(16, 16);
            this.lblListado.Size = new System.Drawing.Size(622, 28);
            this.lblAyuda.AutoSize = false;
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.None;
            this.lblAyuda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblAyuda.Location = new System.Drawing.Point(16, 44);
            this.lblAyuda.Size = new System.Drawing.Size(622, 24);
            this.panelFiltro.AutoSize = false;
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.panelFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelFiltro.Location = new System.Drawing.Point(16, 72);
            this.panelFiltro.Size = new System.Drawing.Size(622, 42);
            this.panelFiltro.AutoScroll = false;
            this.lblFiltro.AutoSize = false;
            this.lblFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblFiltro.Location = new System.Drawing.Point(424, 10);
            this.lblFiltro.Size = new System.Drawing.Size(46, 24);
            this.panelDetalle.AutoSize = false;
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.None;
            this.panelDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.panelDetalle.Location = new System.Drawing.Point(672, 0);
            this.panelDetalle.Size = new System.Drawing.Size(396, 533);
            this.panelDetalle.AutoScroll = true;
            this.contenedorDetalle.AutoSize = false;
            this.contenedorDetalle.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorDetalle.Location = new System.Drawing.Point(16, 16);
            this.contenedorDetalle.Size = new System.Drawing.Size(362, 438);
            this.contenedorDetalle.AutoScroll = false;
            this.contenedorCampos.AutoSize = false;
            this.contenedorCampos.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorCampos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorCampos.Location = new System.Drawing.Point(0, 42);
            this.contenedorCampos.Size = new System.Drawing.Size(362, 272);
            this.contenedorCampos.AutoScroll = false;
            this.panelAcciones.AutoSize = false;
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.None;
            this.panelAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelAcciones.Location = new System.Drawing.Point(0, 330);
            this.panelAcciones.Size = new System.Drawing.Size(362, 100);
            this.panelAcciones.AutoScroll = false;
            this.lblFormulario.AutoSize = false;
            this.lblFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblFormulario.Location = new System.Drawing.Point(0, 0);
            this.lblFormulario.Size = new System.Drawing.Size(362, 32);
            this.lblNombre.AutoSize = false;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.None;
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.Location = new System.Drawing.Point(0, 0);
            this.lblNombre.Size = new System.Drawing.Size(116, 30);
            this.lblApellido.AutoSize = false;
            this.lblApellido.Dock = System.Windows.Forms.DockStyle.None;
            this.lblApellido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblApellido.Location = new System.Drawing.Point(0, 38);
            this.lblApellido.Size = new System.Drawing.Size(116, 30);
            this.lblDni.AutoSize = false;
            this.lblDni.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDni.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDni.Location = new System.Drawing.Point(0, 76);
            this.lblDni.Size = new System.Drawing.Size(116, 30);
            this.lblFechaNacimiento.AutoSize = false;
            this.lblFechaNacimiento.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFechaNacimiento.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblFechaNacimiento.Location = new System.Drawing.Point(0, 114);
            this.lblFechaNacimiento.Size = new System.Drawing.Size(116, 30);
            this.lblPeso.AutoSize = false;
            this.lblPeso.Dock = System.Windows.Forms.DockStyle.None;
            this.lblPeso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblPeso.Location = new System.Drawing.Point(0, 152);
            this.lblPeso.Size = new System.Drawing.Size(116, 30);
            this.lblAltura.AutoSize = false;
            this.lblAltura.Dock = System.Windows.Forms.DockStyle.None;
            this.lblAltura.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblAltura.Location = new System.Drawing.Point(0, 190);
            this.lblAltura.Size = new System.Drawing.Size(116, 30);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabla.Location = new System.Drawing.Point(16, 124);
            this.tabla.Size = new System.Drawing.Size(622, 391);
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.ReadOnly = true;
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.RowHeadersVisible = false;
            this.tabla.MultiSelect = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.ColumnHeadersHeight = 46;

            this.contenedorCampos.Size = new System.Drawing.Size(362, 272);
            this.panelAcciones.Location = new System.Drawing.Point(0, 330);
            this.contenedorDetalle.Size = new System.Drawing.Size(362, 438);
            this.panelDetalle.Controls.Add(this.fotoSocio);
            this.panelDetalle.Controls.Add(this.btnSeleccionarFoto);
            this.panelDetalle.Controls.Add(this.btnQuitarFoto);
            this.panelDetalle.Controls.Add(this.verRutina);
            this.contenedorCampos.Controls.Add(this.lblSexo);
            this.contenedorCampos.Controls.Add(this.sexo);
            this.fotoSocio.Name = "fotoSocio";
            this.fotoSocio.Location = new System.Drawing.Point(16, 454);
            this.fotoSocio.Size = new System.Drawing.Size(120, 120);
            this.fotoSocio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fotoSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoSocio.BackColor = System.Drawing.Color.LightGray;
            this.fotoSocio.TabStop = false;
            this.fotoSocio.Disposed += new System.EventHandler(this.fotoSocio_Disposed);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Text = "Seleccionar foto";
            this.btnSeleccionarFoto.Location = new System.Drawing.Point(152, 454);
            this.btnSeleccionarFoto.Size = new System.Drawing.Size(180, 36);
            this.btnSeleccionarFoto.Click += new System.EventHandler(this.btnSeleccionarFoto_Click);
            this.btnQuitarFoto.Name = "btnQuitarFoto";
            this.btnQuitarFoto.Text = "Quitar foto";
            this.btnQuitarFoto.Location = new System.Drawing.Point(152, 498);
            this.btnQuitarFoto.Size = new System.Drawing.Size(180, 36);
            this.verRutina.AutoSize = false;
            this.verRutina.Dock = System.Windows.Forms.DockStyle.None;
            this.verRutina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.verRutina.Location = new System.Drawing.Point(16, 590);
            this.verRutina.Size = new System.Drawing.Size(316, 38);
            this.btnQuitarFoto.Click += new System.EventHandler(this.btnQuitarFoto_Click);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Text = "Sexo:";
            this.lblSexo.Location = new System.Drawing.Point(0, 234);
            this.lblSexo.Size = new System.Drawing.Size(116, 28);
            this.sexo.Name = "sexo";
            this.sexo.Location = new System.Drawing.Point(124, 234);
            this.sexo.Size = new System.Drawing.Size(222, 28);
            this.sexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sexo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.sexo.Items.AddRange(new object[] { "Masculino", "Femenino" });
            this.sexo.SelectedIndex = -1;
            this.sexo.TabIndex = 6;
            this.sexo.SelectedIndexChanged += new System.EventHandler(this.sexo_SelectedIndexChanged);
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).EndInit();

            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout();
            panelContenido.ResumeLayout(false); contenedorContenido.ResumeLayout(false); panelListado.ResumeLayout(false); panelListado.PerformLayout();
            panelFiltro.ResumeLayout(false); panelFiltro.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit();
            panelDetalle.ResumeLayout(false); contenedorDetalle.ResumeLayout(false); contenedorDetalle.PerformLayout(); contenedorCampos.ResumeLayout(false); contenedorCampos.PerformLayout();
            panelAcciones.ResumeLayout(false); ResumeLayout(false);

            this.Load += new System.EventHandler(this.GestionSociosFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            this.calcularImc.Click += new System.EventHandler(this.calcularImc_Click);
            this.verRutina.Click += new System.EventHandler(this.verRutina_Click);
                    this.peso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.peso_KeyPress);
            this.altura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.altura_KeyPress);
        }

    }
}
