using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class RutinasEntrenadorFormulario
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private Panel barraAcciones; private Label lblEstado; private Panel panelContenido; private Panel panelFormulario; private Panel contenedorFormulario; private Label lblNombre; private Label lblDescripcionRutina; private Label lblEjercicio; private Label lblSeries; private Label lblRepeticiones; private Label lblPeso; private Label lblDescanso; private Label lblOrden; private Label lblMembresia; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colRutina; private DataGridViewTextBoxColumn colCreador; private DataGridViewTextBoxColumn colAsignados; private DataGridViewTextBoxColumn colCreacion;
        private ComboBox membresia; private ComboBox ejercicio; private TextBox nombre; private TextBox descripcion; private TextBox series; private TextBox repeticiones; private TextBox peso; private TextBox descanso; private TextBox orden; private Button nuevaRutina; private Button guardarRutina; private Button actualizar; private Button agregarEjercicio; private Button asignar; private Button darDeBaja;
        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new Panel(); nuevaRutina = new Button(); guardarRutina = new Button(); actualizar = new Button(); agregarEjercicio = new Button(); asignar = new Button(); darDeBaja = new Button(); lblEstado = new Label(); panelContenido = new Panel(); panelFormulario = new Panel(); contenedorFormulario = new Panel(); lblNombre = new Label(); nombre = new TextBox(); lblDescripcionRutina = new Label(); descripcion = new TextBox(); lblEjercicio = new Label(); ejercicio = new ComboBox(); lblSeries = new Label(); series = new TextBox(); lblRepeticiones = new Label(); repeticiones = new TextBox(); lblPeso = new Label(); peso = new TextBox(); lblDescanso = new Label(); descanso = new TextBox(); lblOrden = new Label(); orden = new TextBox(); lblMembresia = new Label(); membresia = new ComboBox(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colRutina = new DataGridViewTextBoxColumn(); colCreador = new DataGridViewTextBoxColumn(); colAsignados = new DataGridViewTextBoxColumn(); colCreacion = new DataGridViewTextBoxColumn(); panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); panelFormulario.SuspendLayout(); contenedorFormulario.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(14, 116, 144);   panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);   lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Catalogo de rutinas";  lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Crea plantillas y asigna ejercicios a tus socios";  btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(14, 116, 144);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            barraAcciones.BackColor = Color.White;   barraAcciones.Padding = new Padding(16, 8, 16, 8);  barraAcciones.Controls.Add(nuevaRutina); barraAcciones.Controls.Add(guardarRutina); barraAcciones.Controls.Add(actualizar); barraAcciones.Controls.Add(agregarEjercicio); barraAcciones.Controls.Add(asignar); barraAcciones.Controls.Add(darDeBaja);
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo"; panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla); panelContenido.Controls.Add(panelFormulario); panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle;   panelFormulario.Padding = new Padding(12); panelFormulario.Controls.Add(contenedorFormulario);
            contenedorFormulario.Controls.Add(lblNombre); contenedorFormulario.Controls.Add(nombre); contenedorFormulario.Controls.Add(lblDescripcionRutina); contenedorFormulario.Controls.Add(descripcion); contenedorFormulario.Controls.Add(lblEjercicio); contenedorFormulario.Controls.Add(ejercicio); contenedorFormulario.Controls.Add(lblSeries); contenedorFormulario.Controls.Add(series); contenedorFormulario.Controls.Add(lblRepeticiones); contenedorFormulario.Controls.Add(repeticiones); contenedorFormulario.Controls.Add(lblPeso); contenedorFormulario.Controls.Add(peso); contenedorFormulario.Controls.Add(lblDescanso); contenedorFormulario.Controls.Add(descanso); contenedorFormulario.Controls.Add(lblOrden); contenedorFormulario.Controls.Add(orden); contenedorFormulario.Controls.Add(lblMembresia); contenedorFormulario.Controls.Add(membresia);    ejercicio.DropDownStyle = ComboBoxStyle.DropDownList;       membresia.DropDownStyle = ComboBoxStyle.DropDownList;

                tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None;       tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colRutina, colCreador, colAsignados, colCreacion }); colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colRutina.HeaderText = "Rutina"; colRutina.Name = "colRutina"; colCreador.HeaderText = "Entrenador"; colCreador.Name = "colCreador"; colAsignados.HeaderText = "Socios asignados"; colAsignados.Name = "colAsignados"; colCreacion.HeaderText = "Creacion"; colCreacion.Name = "colCreacion";
             panelEncabezado.Name = "panelEncabezado";  panelEncabezado.TabIndex = 0;
            lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0; lblDescripcion.Name = "lblDescripcion";  lblDescripcion.TabIndex = 1; btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1;
             nuevaRutina.BackColor = Color.FromArgb(226, 232, 240); nuevaRutina.FlatAppearance.BorderSize = 0; nuevaRutina.FlatStyle = FlatStyle.Flat; nuevaRutina.ForeColor = Color.FromArgb(30, 41, 59);  nuevaRutina.Margin = new Padding(4, 0, 4, 0); nuevaRutina.Name = "nuevaRutina"; nuevaRutina.Padding = new Padding(10, 0, 10, 0); nuevaRutina.Text = "+ Nueva rutina"; nuevaRutina.UseVisualStyleBackColor = false;
             guardarRutina.BackColor = Color.FromArgb(14, 116, 144); guardarRutina.FlatAppearance.BorderSize = 0; guardarRutina.FlatStyle = FlatStyle.Flat; guardarRutina.ForeColor = Color.White;  guardarRutina.Margin = new Padding(4, 0, 4, 0); guardarRutina.Name = "guardarRutina"; guardarRutina.Padding = new Padding(10, 0, 10, 0); guardarRutina.Text = "Guardar"; guardarRutina.UseVisualStyleBackColor = false;
             actualizar.BackColor = Color.FromArgb(226, 232, 240); actualizar.FlatAppearance.BorderSize = 0; actualizar.FlatStyle = FlatStyle.Flat; actualizar.ForeColor = Color.FromArgb(30, 41, 59);  actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Name = "actualizar"; actualizar.Padding = new Padding(10, 0, 10, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false;
             agregarEjercicio.BackColor = Color.FromArgb(14, 116, 144); agregarEjercicio.FlatAppearance.BorderSize = 0; agregarEjercicio.FlatStyle = FlatStyle.Flat; agregarEjercicio.ForeColor = Color.White;  agregarEjercicio.Margin = new Padding(4, 0, 4, 0); agregarEjercicio.Name = "agregarEjercicio"; agregarEjercicio.Padding = new Padding(10, 0, 10, 0); agregarEjercicio.Text = "Agregar ejercicio"; agregarEjercicio.UseVisualStyleBackColor = false;
             asignar.BackColor = Color.FromArgb(14, 116, 144); asignar.FlatAppearance.BorderSize = 0; asignar.FlatStyle = FlatStyle.Flat; asignar.ForeColor = Color.White;  asignar.Margin = new Padding(4, 0, 4, 0); asignar.Name = "asignar"; asignar.Padding = new Padding(10, 0, 10, 0); asignar.Text = "Asignar a socio"; asignar.UseVisualStyleBackColor = false;
             darDeBaja.BackColor = Color.FromArgb(254, 242, 242); darDeBaja.FlatAppearance.BorderSize = 0; darDeBaja.FlatStyle = FlatStyle.Flat; darDeBaja.ForeColor = Color.FromArgb(185, 28, 28);  darDeBaja.Margin = new Padding(4, 0, 4, 0); darDeBaja.Name = "darDeBaja"; darDeBaja.Padding = new Padding(10, 0, 10, 0); darDeBaja.Text = "Dar de baja"; darDeBaja.UseVisualStyleBackColor = false;
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;  panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;  panelFormulario.Name = "panelFormulario";  panelFormulario.TabIndex = 0;  contenedorFormulario.Name = "contenedorFormulario";  contenedorFormulario.TabIndex = 0;
              lblNombre.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblNombre.ForeColor = Color.FromArgb(30, 41, 59); lblNombre.Name = "lblNombre"; lblNombre.Text = "Nombre:";
              lblDescripcionRutina.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblDescripcionRutina.ForeColor = Color.FromArgb(30, 41, 59); lblDescripcionRutina.Name = "lblDescripcionRutina"; lblDescripcionRutina.Text = "Descripcion:";
              lblEjercicio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblEjercicio.ForeColor = Color.FromArgb(30, 41, 59); lblEjercicio.Name = "lblEjercicio"; lblEjercicio.Text = "Ejercicio:";
              lblSeries.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblSeries.ForeColor = Color.FromArgb(30, 41, 59); lblSeries.Name = "lblSeries"; lblSeries.Text = "Series:";
              lblRepeticiones.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblRepeticiones.ForeColor = Color.FromArgb(30, 41, 59); lblRepeticiones.Name = "lblRepeticiones"; lblRepeticiones.Text = "Repeticiones:";
              lblPeso.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblPeso.ForeColor = Color.FromArgb(30, 41, 59); lblPeso.Name = "lblPeso"; lblPeso.Text = "Peso:";
              lblDescanso.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblDescanso.ForeColor = Color.FromArgb(30, 41, 59); lblDescanso.Name = "lblDescanso"; lblDescanso.Text = "Descanso:";
              lblOrden.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblOrden.ForeColor = Color.FromArgb(30, 41, 59); lblOrden.Name = "lblOrden"; lblOrden.Text = "Orden:";
              lblMembresia.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblMembresia.ForeColor = Color.FromArgb(30, 41, 59); lblMembresia.Name = "lblMembresia"; lblMembresia.Text = "Membresia:";
            nombre.BorderStyle = BorderStyle.FixedSingle; nombre.Margin = new Padding(0, 4, 8, 4); nombre.Name = "nombre"; descripcion.BorderStyle = BorderStyle.FixedSingle; descripcion.Margin = new Padding(0, 4, 8, 4); descripcion.Name = "descripcion"; ejercicio.Margin = new Padding(0, 4, 8, 4); ejercicio.Name = "ejercicio"; series.BorderStyle = BorderStyle.FixedSingle; series.Margin = new Padding(0, 4, 8, 4); series.Name = "series"; repeticiones.BorderStyle = BorderStyle.FixedSingle; repeticiones.Margin = new Padding(0, 4, 8, 4); repeticiones.Name = "repeticiones"; peso.BorderStyle = BorderStyle.FixedSingle; peso.Margin = new Padding(0, 4, 8, 4); peso.Name = "peso"; descanso.BorderStyle = BorderStyle.FixedSingle; descanso.Margin = new Padding(0, 4, 8, 4); descanso.Name = "descanso"; orden.BorderStyle = BorderStyle.FixedSingle; orden.Margin = new Padding(0, 4, 8, 4); orden.Name = "orden"; membresia.Margin = new Padding(0, 4, 8, 4); membresia.Name = "membresia";
             tabla.Name = "tabla";  tabla.TabIndex = 1; colId.FillWeight = 60; colRutina.FillWeight = 300; colRutina.MinimumWidth = 90; colCreador.FillWeight = 260; colCreador.MinimumWidth = 90; colAsignados.FillWeight = 220; colAsignados.MinimumWidth = 90; colCreacion.FillWeight = 220; colCreacion.MinimumWidth = 90;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font;  BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); this.Name = "RutinasEntrenadorFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Catalogo de rutinas";

            // Encabezado: titulo y descripcion a la izquierda, accion de regreso a la derecha.

            this.panelEncabezado.Padding = new Padding(22, 8, 22, 8);

            this.lblTitulo.Margin = new Padding(0);
            this.lblTitulo.TextAlign = ContentAlignment.BottomLeft;

            this.lblDescripcion.Margin = new Padding(0);
            this.lblDescripcion.TextAlign = ContentAlignment.TopLeft;

            this.btnVolver.Margin = new Padding(16, 0, 0, 0);
            // Barra de acciones: los controles se reacomodan cuando el ancho disminuye.

            this.barraAcciones.Padding = new Padding(16, 8, 16, 8);

            this.nuevaRutina.MinimumSize = new Size(120, 34);
            this.nuevaRutina.Margin = new Padding(0, 0, 8, 0);
            this.nuevaRutina.Padding = new Padding(12, 0, 12, 0);

            this.guardarRutina.MinimumSize = new Size(120, 34);
            this.guardarRutina.Margin = new Padding(0, 0, 8, 0);
            this.guardarRutina.Padding = new Padding(12, 0, 12, 0);

            this.actualizar.MinimumSize = new Size(120, 34);
            this.actualizar.Margin = new Padding(0, 0, 8, 0);
            this.actualizar.Padding = new Padding(12, 0, 12, 0);

            this.agregarEjercicio.MinimumSize = new Size(120, 34);
            this.agregarEjercicio.Margin = new Padding(0, 0, 8, 0);
            this.agregarEjercicio.Padding = new Padding(12, 0, 12, 0);

            this.asignar.MinimumSize = new Size(120, 34);
            this.asignar.Margin = new Padding(0, 0, 8, 0);
            this.asignar.Padding = new Padding(12, 0, 12, 0);

            this.darDeBaja.MinimumSize = new Size(120, 34);
            this.darDeBaja.Margin = new Padding(0, 0, 8, 0);
            this.darDeBaja.Padding = new Padding(12, 0, 12, 0);
            // Barra de estado inferior.

            this.lblEstado.Padding = new Padding(18, 0, 12, 0);
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            // Area de trabajo: la grilla ocupa el alto disponible y el formulario queda debajo.

            this.panelContenido.Padding = new Padding(16);

            this.tabla.Margin = new Padding(0, 0, 0, 16);
            this.tabla.RowTemplate.Height = 30;

            this.panelFormulario.Margin = new Padding(0);
            this.panelFormulario.Padding = new Padding(16, 14, 16, 14);

            this.lblNombre.Margin = new Padding(0, 0, 8, 8);
            this.lblNombre.TextAlign = ContentAlignment.MiddleLeft;

            this.nombre.Margin = new Padding(0, 3, 16, 8);

            this.lblDescripcionRutina.Margin = new Padding(0, 0, 8, 8);
            this.lblDescripcionRutina.TextAlign = ContentAlignment.MiddleLeft;

            this.descripcion.Margin = new Padding(0, 3, 16, 8);

            this.lblEjercicio.Margin = new Padding(0, 0, 8, 8);
            this.lblEjercicio.TextAlign = ContentAlignment.MiddleLeft;

            this.ejercicio.Margin = new Padding(0, 3, 16, 8);

            this.lblSeries.Margin = new Padding(0, 0, 8, 8);
            this.lblSeries.TextAlign = ContentAlignment.MiddleLeft;

            this.series.Margin = new Padding(0, 3, 16, 8);

            this.lblRepeticiones.Margin = new Padding(0, 0, 8, 8);
            this.lblRepeticiones.TextAlign = ContentAlignment.MiddleLeft;

            this.repeticiones.Margin = new Padding(0, 3, 16, 8);

            this.lblPeso.Margin = new Padding(0, 0, 8, 8);
            this.lblPeso.TextAlign = ContentAlignment.MiddleLeft;

            this.peso.Margin = new Padding(0, 3, 16, 8);

            this.lblDescanso.Margin = new Padding(0, 0, 8, 8);
            this.lblDescanso.TextAlign = ContentAlignment.MiddleLeft;

            this.descanso.Margin = new Padding(0, 3, 16, 8);

            this.lblOrden.Margin = new Padding(0, 0, 8, 8);
            this.lblOrden.TextAlign = ContentAlignment.MiddleLeft;

            this.orden.Margin = new Padding(0, 3, 16, 8);

            this.lblMembresia.Margin = new Padding(0, 0, 8, 8);
            this.lblMembresia.TextAlign = ContentAlignment.MiddleLeft;

            this.membresia.Margin = new Padding(0, 3, 16, 8);

            this.AutoScroll = false;
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 84);
            this.panelEncabezado.AutoScroll = false;
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Size = new System.Drawing.Size(890, 36);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
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
            this.barraAcciones.Size = new System.Drawing.Size(1100, 52);
            this.barraAcciones.AutoScroll = false;
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Size = new System.Drawing.Size(1100, 30);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(0, 136);
            this.panelContenido.Size = new System.Drawing.Size(1100, 514);
            this.panelContenido.AutoScroll = false;
            this.panelFormulario.AutoSize = false;
            this.panelFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.panelFormulario.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelFormulario.Location = new System.Drawing.Point(16, 354);
            this.panelFormulario.Size = new System.Drawing.Size(1068, 144);
            this.panelFormulario.AutoScroll = false;
            this.contenedorFormulario.AutoSize = false;
            this.contenedorFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorFormulario.Location = new System.Drawing.Point(16, 14);
            this.contenedorFormulario.Size = new System.Drawing.Size(1034, 114);
            this.contenedorFormulario.AutoScroll = false;
            this.lblNombre.AutoSize = false;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.None;
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.Location = new System.Drawing.Point(0, 0);
            this.lblNombre.Size = new System.Drawing.Size(102, 30);
            this.lblDescripcionRutina.AutoSize = false;
            this.lblDescripcionRutina.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcionRutina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcionRutina.Location = new System.Drawing.Point(344, 0);
            this.lblDescripcionRutina.Size = new System.Drawing.Size(102, 30);
            this.lblEjercicio.AutoSize = false;
            this.lblEjercicio.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEjercicio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEjercicio.Location = new System.Drawing.Point(688, 0);
            this.lblEjercicio.Size = new System.Drawing.Size(102, 30);
            this.lblSeries.AutoSize = false;
            this.lblSeries.Dock = System.Windows.Forms.DockStyle.None;
            this.lblSeries.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblSeries.Location = new System.Drawing.Point(0, 38);
            this.lblSeries.Size = new System.Drawing.Size(102, 30);
            this.lblRepeticiones.AutoSize = false;
            this.lblRepeticiones.Dock = System.Windows.Forms.DockStyle.None;
            this.lblRepeticiones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblRepeticiones.Location = new System.Drawing.Point(344, 38);
            this.lblRepeticiones.Size = new System.Drawing.Size(102, 30);
            this.lblPeso.AutoSize = false;
            this.lblPeso.Dock = System.Windows.Forms.DockStyle.None;
            this.lblPeso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblPeso.Location = new System.Drawing.Point(688, 38);
            this.lblPeso.Size = new System.Drawing.Size(102, 30);
            this.lblDescanso.AutoSize = false;
            this.lblDescanso.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescanso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescanso.Location = new System.Drawing.Point(0, 76);
            this.lblDescanso.Size = new System.Drawing.Size(102, 30);
            this.lblOrden.AutoSize = false;
            this.lblOrden.Dock = System.Windows.Forms.DockStyle.None;
            this.lblOrden.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblOrden.Location = new System.Drawing.Point(344, 76);
            this.lblOrden.Size = new System.Drawing.Size(102, 30);
            this.lblMembresia.AutoSize = false;
            this.lblMembresia.Dock = System.Windows.Forms.DockStyle.None;
            this.lblMembresia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMembresia.Location = new System.Drawing.Point(688, 76);
            this.lblMembresia.Size = new System.Drawing.Size(102, 30);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabla.Location = new System.Drawing.Point(16, 16);
            this.tabla.Size = new System.Drawing.Size(1068, 322);
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.ReadOnly = true;
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.RowHeadersVisible = false;
            this.tabla.MultiSelect = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.ColumnHeadersHeight = 46;
            this.membresia.AutoSize = false;
            this.membresia.Dock = System.Windows.Forms.DockStyle.None;
            this.membresia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.membresia.Location = new System.Drawing.Point(798, 80);
            this.membresia.Size = new System.Drawing.Size(222, 26);
            this.ejercicio.AutoSize = false;
            this.ejercicio.Dock = System.Windows.Forms.DockStyle.None;
            this.ejercicio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.ejercicio.Location = new System.Drawing.Point(798, 4);
            this.ejercicio.Size = new System.Drawing.Size(222, 26);
            this.nombre.AutoSize = false;
            this.nombre.Dock = System.Windows.Forms.DockStyle.None;
            this.nombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nombre.Location = new System.Drawing.Point(110, 4);
            this.nombre.Size = new System.Drawing.Size(222, 26);
            this.descripcion.AutoSize = false;
            this.descripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.descripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.descripcion.Location = new System.Drawing.Point(454, 4);
            this.descripcion.Size = new System.Drawing.Size(222, 26);
            this.series.AutoSize = false;
            this.series.Dock = System.Windows.Forms.DockStyle.None;
            this.series.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.series.Location = new System.Drawing.Point(110, 42);
            this.series.Size = new System.Drawing.Size(222, 26);
            this.repeticiones.AutoSize = false;
            this.repeticiones.Dock = System.Windows.Forms.DockStyle.None;
            this.repeticiones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.repeticiones.Location = new System.Drawing.Point(454, 42);
            this.repeticiones.Size = new System.Drawing.Size(222, 26);
            this.peso.AutoSize = false;
            this.peso.Dock = System.Windows.Forms.DockStyle.None;
            this.peso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.peso.Location = new System.Drawing.Point(798, 42);
            this.peso.Size = new System.Drawing.Size(222, 26);
            this.descanso.AutoSize = false;
            this.descanso.Dock = System.Windows.Forms.DockStyle.None;
            this.descanso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.descanso.Location = new System.Drawing.Point(110, 80);
            this.descanso.Size = new System.Drawing.Size(222, 26);
            this.orden.AutoSize = false;
            this.orden.Dock = System.Windows.Forms.DockStyle.None;
            this.orden.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.orden.Location = new System.Drawing.Point(454, 80);
            this.orden.Size = new System.Drawing.Size(222, 26);
            this.nuevaRutina.AutoSize = false;
            this.nuevaRutina.Dock = System.Windows.Forms.DockStyle.None;
            this.nuevaRutina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nuevaRutina.Location = new System.Drawing.Point(16, 8);
            this.nuevaRutina.Size = new System.Drawing.Size(125, 34);
            this.guardarRutina.AutoSize = false;
            this.guardarRutina.Dock = System.Windows.Forms.DockStyle.None;
            this.guardarRutina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.guardarRutina.Location = new System.Drawing.Point(149, 8);
            this.guardarRutina.Size = new System.Drawing.Size(120, 34);
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(277, 8);
            this.actualizar.Size = new System.Drawing.Size(120, 34);
            this.agregarEjercicio.AutoSize = false;
            this.agregarEjercicio.Dock = System.Windows.Forms.DockStyle.None;
            this.agregarEjercicio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.agregarEjercicio.Location = new System.Drawing.Point(405, 8);
            this.agregarEjercicio.Size = new System.Drawing.Size(136, 34);
            this.asignar.AutoSize = false;
            this.asignar.Dock = System.Windows.Forms.DockStyle.None;
            this.asignar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.asignar.Location = new System.Drawing.Point(549, 8);
            this.asignar.Size = new System.Drawing.Size(126, 34);
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.Dock = System.Windows.Forms.DockStyle.None;
            this.darDeBaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.darDeBaja.Location = new System.Drawing.Point(683, 8);
            this.darDeBaja.Size = new System.Drawing.Size(120, 34);
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); panelFormulario.ResumeLayout(false); contenedorFormulario.ResumeLayout(false); contenedorFormulario.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.RutinasEntrenadorFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.membresia.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.membresia_Format);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.nuevaRutina.Click += new System.EventHandler(this.nuevaRutina_Click);
            this.guardarRutina.Click += new System.EventHandler(this.guardarRutina_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.agregarEjercicio.Click += new System.EventHandler(this.agregarEjercicio_Click);
            this.asignar.Click += new System.EventHandler(this.asignar_Click);
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
                }

    }
}
