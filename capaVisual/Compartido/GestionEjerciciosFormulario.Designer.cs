using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionEjerciciosFormulario
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblBuscar;
        private TextBox buscador;
        private Label lblEstadoFiltro;
        private ComboBox filtroEstado;
        private Button nuevo;
        private Button actualizar;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private Panel panelListado;
        private Label lblListadoTitulo;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colEstado;
        private Panel panelDetalle;
        private GroupBox grupoFicha;
        private Label lblDetalleTitulo;
        private TableLayoutPanel tablaFicha;
        private Label lblNombre;
        private TextBox nombre;
        private Label lblDescripcionEjercicio;
        private TextBox descripcion;
        private Label lblEstadoCampo;
        private Label lblEstadoValor;
        private FlowLayoutPanel accionesFicha;
        private GroupBox grupoImagenes;
        private FlowLayoutPanel galeriaImagenes;
        private Panel accionesImagenes;
        private Button agregarImagen;
        private Button quitarImagen;
        private Button guardar;
        private Button cancelar;
        private Button darDeBaja;
        private Button reactivar;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            components = new Container();
            panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button();
            barraAcciones = new Panel(); lblBuscar = new Label(); buscador = new TextBox(); lblEstadoFiltro = new Label(); filtroEstado = new ComboBox(); nuevo = new Button(); actualizar = new Button();
            lblEstado = new Label(); splitContenido = new SplitContainer(); panelListado = new Panel(); lblListadoTitulo = new Label(); tabla = new DataGridView();
            colId = new DataGridViewTextBoxColumn(); colNombre = new DataGridViewTextBoxColumn(); colDescripcion = new DataGridViewTextBoxColumn(); colEstado = new DataGridViewTextBoxColumn();
            panelDetalle = new Panel(); grupoFicha = new GroupBox(); lblDetalleTitulo = new Label(); tablaFicha = new TableLayoutPanel(); lblNombre = new Label(); nombre = new TextBox(); lblDescripcionEjercicio = new Label(); descripcion = new TextBox(); lblEstadoCampo = new Label(); lblEstadoValor = new Label(); grupoImagenes = new GroupBox(); galeriaImagenes = new FlowLayoutPanel(); accionesImagenes = new Panel(); agregarImagen = new Button(); quitarImagen = new Button(); accionesFicha = new FlowLayoutPanel(); guardar = new Button(); cancelar = new Button(); darDeBaja = new Button(); reactivar = new Button();
            ((ISupportInitialize)(splitContenido)).BeginInit(); splitContenido.Panel1.SuspendLayout(); splitContenido.Panel2.SuspendLayout(); splitContenido.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229); panelEncabezado.Dock = DockStyle.Top; panelEncabezado.Height = 70; panelEncabezado.Padding = new Padding(20, 8, 20, 8);
            lblTitulo.AutoSize = false; lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(20, 8); lblTitulo.Size = new Size(700, 30); lblTitulo.Text = "Catálogo de ejercicios";
            lblDescripcion.AutoSize = false; lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Location = new Point(22, 39); lblDescripcion.Size = new Size(700, 22); lblDescripcion.Text = "Busca, selecciona y edita ejercicios disponibles para las rutinas";
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.Location = new Point(980, 17); btnVolver.Size = new Size(100, 36); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false; btnVolver.Click += btnVolver_Click;
            panelEncabezado.Controls.Add(btnVolver); panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Height = 56;
            lblTitulo.Size = new Size(700, 40); lblTitulo.Text = "Ejercicios | Catálogo de ejercicios"; lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblDescripcion.Visible = false;
            panelEncabezado.Visible = false;
            btnVolver.Location = new Point(980, 10);

            barraAcciones.BackColor = Color.White; barraAcciones.Dock = DockStyle.Top; barraAcciones.Height = 54; barraAcciones.Padding = new Padding(16, 9, 16, 9);
            lblBuscar.AutoSize = false; lblBuscar.Location = new Point(16, 10); lblBuscar.Size = new Size(52, 28); lblBuscar.Text = "Buscar:"; lblBuscar.TextAlign = ContentAlignment.MiddleLeft;
            buscador.Anchor = AnchorStyles.Top | AnchorStyles.Left; buscador.Location = new Point(72, 10); buscador.Size = new Size(240, 27); buscador.BorderStyle = BorderStyle.FixedSingle; buscador.TabIndex = 0; buscador.TextChanged += buscador_TextChanged;
            lblEstadoFiltro.AutoSize = false; lblEstadoFiltro.Location = new Point(330, 10); lblEstadoFiltro.Size = new Size(48, 28); lblEstadoFiltro.Text = "Estado:"; lblEstadoFiltro.TextAlign = ContentAlignment.MiddleLeft;
            filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList; filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" }); filtroEstado.SelectedIndex = 0; filtroEstado.Location = new Point(382, 10); filtroEstado.Size = new Size(120, 28); filtroEstado.TabIndex = 1; filtroEstado.SelectedIndexChanged += filtroEstado_SelectedIndexChanged;
            actualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right; actualizar.BackColor = Color.FromArgb(226, 232, 240); actualizar.FlatAppearance.BorderSize = 0; actualizar.FlatStyle = FlatStyle.Flat; actualizar.Location = new Point(744, 9); actualizar.Size = new Size(150, 34); actualizar.TabIndex = 2; actualizar.Text = "Actualizar listado"; actualizar.UseVisualStyleBackColor = false; actualizar.Click += actualizar_Click;
            nuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right; nuevo.BackColor = Color.FromArgb(79, 70, 229); nuevo.FlatAppearance.BorderSize = 0; nuevo.FlatStyle = FlatStyle.Flat; nuevo.ForeColor = Color.White; nuevo.Location = new Point(902, 9); nuevo.Size = new Size(178, 34); nuevo.TabIndex = 3; nuevo.Text = "+ Nuevo ejercicio"; nuevo.UseVisualStyleBackColor = false; nuevo.Click += nuevo_Click;
            barraAcciones.Controls.Add(nuevo); barraAcciones.Controls.Add(actualizar); barraAcciones.Controls.Add(filtroEstado); barraAcciones.Controls.Add(lblEstadoFiltro); barraAcciones.Controls.Add(buscador); barraAcciones.Controls.Add(lblBuscar);

            lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.Height = 28; lblEstado.Padding = new Padding(16, 0, 8, 0); lblEstado.Text = "Listo"; lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            splitContenido.Dock = DockStyle.Fill; splitContenido.Size = new Size(1068, 480); splitContenido.FixedPanel = FixedPanel.None; splitContenido.IsSplitterFixed = false; splitContenido.MinimumSize = new Size(900, 420); splitContenido.SplitterWidth = 6; splitContenido.Panel1MinSize = 420; splitContenido.Panel2MinSize = 340;
            splitContenido.Panel1.BackColor = Color.FromArgb(248, 250, 252); splitContenido.Panel1.Padding = new Padding(16); splitContenido.Panel2.BackColor = Color.FromArgb(248, 250, 252); splitContenido.Panel2.Padding = new Padding(0, 16, 16, 16);
            lblListadoTitulo.Dock = DockStyle.Top; lblListadoTitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); lblListadoTitulo.ForeColor = Color.FromArgb(30, 41, 59); lblListadoTitulo.Height = 34; lblListadoTitulo.Text = "Ejercicios del catálogo"; lblListadoTitulo.TextAlign = ContentAlignment.MiddleLeft;
            tabla.Dock = DockStyle.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.FixedSingle; tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.ColumnHeadersHeight = 34; tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tabla.RowTemplate.Height = 30; tabla.TabIndex = 4; tabla.SelectionChanged += tabla_SelectionChanged;
            colId.Name = "colId"; colId.Visible = false; colNombre.HeaderText = "Nombre"; colNombre.Name = "colNombre"; colNombre.FillWeight = 30; colNombre.MinimumWidth = 100; colDescripcion.HeaderText = "Descripción"; colDescripcion.Name = "colDescripcion"; colDescripcion.FillWeight = 52; colDescripcion.MinimumWidth = 120; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado"; colEstado.FillWeight = 18; colEstado.MinimumWidth = 80; tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colDescripcion, colEstado });
            panelListado.Dock = DockStyle.Fill; panelListado.Controls.Add(tabla); panelListado.Controls.Add(lblListadoTitulo); splitContenido.Panel1.Controls.Add(panelListado);

            grupoFicha.Dock = DockStyle.Fill; grupoFicha.BackColor = Color.White; grupoFicha.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); grupoFicha.ForeColor = Color.FromArgb(30, 41, 59); grupoFicha.Padding = new Padding(16); grupoFicha.Text = "Ficha / Edición del ejercicio";
            lblDetalleTitulo.Dock = DockStyle.Top; lblDetalleTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold); lblDetalleTitulo.ForeColor = Color.FromArgb(79, 70, 229); lblDetalleTitulo.Height = 32; lblDetalleTitulo.Text = "Nuevo ejercicio";
            tablaFicha.ColumnCount = 2; tablaFicha.Dock = DockStyle.Fill; tablaFicha.Padding = new Padding(0, 8, 0, 0); tablaFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F)); tablaFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); tablaFicha.RowCount = 4; tablaFicha.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F)); tablaFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); tablaFicha.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F)); tablaFicha.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            lblNombre.Dock = DockStyle.Fill; lblNombre.Text = "Nombre"; lblNombre.TextAlign = ContentAlignment.MiddleLeft; nombre.Dock = DockStyle.Fill; nombre.BorderStyle = BorderStyle.FixedSingle; nombre.Margin = new Padding(0, 3, 0, 3); nombre.TabIndex = 0;
            lblDescripcionEjercicio.Dock = DockStyle.Fill; lblDescripcionEjercicio.Text = "Descripción"; lblDescripcionEjercicio.TextAlign = ContentAlignment.TopLeft; descripcion.Dock = DockStyle.Fill; descripcion.BorderStyle = BorderStyle.FixedSingle; descripcion.Multiline = true; descripcion.ScrollBars = ScrollBars.Vertical; descripcion.Margin = new Padding(0, 3, 0, 3); descripcion.TabIndex = 1;
            lblEstadoCampo.Dock = DockStyle.Fill; lblEstadoCampo.Text = "Estado"; lblEstadoCampo.TextAlign = ContentAlignment.MiddleLeft; lblEstadoValor.Dock = DockStyle.Fill; lblEstadoValor.ForeColor = Color.FromArgb(22, 101, 52); lblEstadoValor.Text = "Activo"; lblEstadoValor.TextAlign = ContentAlignment.MiddleLeft;
            grupoImagenes.Dock = DockStyle.Fill; grupoImagenes.Text = "Imágenes del ejercicio"; grupoImagenes.Padding = new Padding(8); grupoImagenes.ForeColor = Color.FromArgb(30, 41, 59);
            galeriaImagenes.Dock = DockStyle.Fill; galeriaImagenes.AutoScroll = true; galeriaImagenes.WrapContents = true; galeriaImagenes.FlowDirection = FlowDirection.LeftToRight; galeriaImagenes.Padding = new Padding(4); galeriaImagenes.BackColor = Color.FromArgb(248, 250, 252);
            accionesImagenes.Dock = DockStyle.Bottom; accionesImagenes.Height = 34; accionesImagenes.Padding = new Padding(0, 2, 0, 0);
            agregarImagen.BackColor = Color.FromArgb(226, 232, 240); agregarImagen.FlatAppearance.BorderSize = 0; agregarImagen.FlatStyle = FlatStyle.Flat; agregarImagen.Size = new Size(116, 30); agregarImagen.Text = "Agregar imagen"; agregarImagen.Click += agregarImagen_Click;
            quitarImagen.BackColor = Color.FromArgb(254, 242, 242); quitarImagen.FlatAppearance.BorderSize = 0; quitarImagen.FlatStyle = FlatStyle.Flat; quitarImagen.ForeColor = Color.FromArgb(185, 28, 28); quitarImagen.Size = new Size(108, 30); quitarImagen.Text = "Quitar imagen"; quitarImagen.Click += quitarImagen_Click;
            accionesImagenes.Controls.Add(agregarImagen); accionesImagenes.Controls.Add(quitarImagen); grupoImagenes.Controls.Add(galeriaImagenes); grupoImagenes.Controls.Add(accionesImagenes); tablaFicha.Controls.Add(grupoImagenes, 0, 3); tablaFicha.SetColumnSpan(grupoImagenes, 2);
            tablaFicha.Controls.Add(lblNombre, 0, 0); tablaFicha.Controls.Add(nombre, 1, 0); tablaFicha.Controls.Add(lblDescripcionEjercicio, 0, 1); tablaFicha.Controls.Add(descripcion, 1, 1); tablaFicha.Controls.Add(lblEstadoCampo, 0, 2); tablaFicha.Controls.Add(lblEstadoValor, 1, 2);
            accionesFicha.Dock = DockStyle.Bottom; accionesFicha.FlowDirection = FlowDirection.LeftToRight; accionesFicha.Height = 48; accionesFicha.Padding = new Padding(0, 8, 0, 0); accionesFicha.WrapContents = false;
            guardar.BackColor = Color.FromArgb(79, 70, 229); guardar.FlatAppearance.BorderSize = 0; guardar.FlatStyle = FlatStyle.Flat; guardar.ForeColor = Color.White; guardar.Size = new Size(112, 34); guardar.TabIndex = 7; guardar.Text = "Guardar"; guardar.UseVisualStyleBackColor = false; guardar.Click += guardar_Click;
            cancelar.BackColor = Color.FromArgb(226, 232, 240); cancelar.FlatAppearance.BorderSize = 0; cancelar.FlatStyle = FlatStyle.Flat; cancelar.Size = new Size(100, 34); cancelar.TabIndex = 8; cancelar.Text = "Cancelar"; cancelar.UseVisualStyleBackColor = false; cancelar.Click += cancelar_Click;
            darDeBaja.BackColor = Color.FromArgb(254, 242, 242); darDeBaja.FlatAppearance.BorderSize = 0; darDeBaja.FlatStyle = FlatStyle.Flat; darDeBaja.ForeColor = Color.FromArgb(185, 28, 28); darDeBaja.Size = new Size(120, 34); darDeBaja.TabIndex = 9; darDeBaja.Text = "Dar de baja"; darDeBaja.UseVisualStyleBackColor = false; darDeBaja.Visible = false; darDeBaja.Click += darDeBaja_Click;
            reactivar.BackColor = Color.FromArgb(220, 252, 231); reactivar.FlatAppearance.BorderSize = 0; reactivar.FlatStyle = FlatStyle.Flat; reactivar.ForeColor = Color.FromArgb(22, 101, 52); reactivar.Size = new Size(100, 34); reactivar.TabIndex = 10; reactivar.Text = "Reactivar"; reactivar.UseVisualStyleBackColor = false; reactivar.Visible = false; reactivar.Click += reactivar_Click;
            accionesFicha.Controls.Add(guardar); accionesFicha.Controls.Add(cancelar); accionesFicha.Controls.Add(darDeBaja); accionesFicha.Controls.Add(reactivar);
            grupoFicha.Controls.Add(tablaFicha); grupoFicha.Controls.Add(accionesFicha); grupoFicha.Controls.Add(lblDetalleTitulo); panelDetalle.Dock = DockStyle.Fill; panelDetalle.Controls.Add(grupoFicha); splitContenido.Panel2.Controls.Add(panelDetalle);
            Controls.Add(splitContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado);
            AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); Name = "GestionEjerciciosFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Ejercicios"; Load += GestionEjerciciosFormulario_Load;
            splitContenido.Panel2.ResumeLayout(false); splitContenido.Panel1.ResumeLayout(false); ((ISupportInitialize)(splitContenido)).EndInit(); splitContenido.ResumeLayout(false); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false); PerformLayout(); splitContenido.SplitterDistance = 620;
        }
    }
}
