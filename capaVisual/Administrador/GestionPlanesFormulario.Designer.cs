using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class GestionPlanesFormulario
    {
        private IContainer components;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private Label lblListado;
        private Label lblBuscar;
        private TextBox buscador;
        private Label lblFiltroEstado;
        private ComboBox filtroEstado;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colEstado;
        private Label lblFormulario;
        private Label lblNombre;
        private Label lblDescripcionPlan;
        private Label lblPrecio;
        private TextBox nombre;
        private TextBox descripcion;
        private TextBox precio;
        private Button nuevo;
        private Button guardar;
        private Button actualizar;
        private Button darDeBaja;
        private Button reactivar;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null)
            {
                components.Dispose();
            }

            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblEstado = new System.Windows.Forms.Label();
            this.splitContenido = new System.Windows.Forms.SplitContainer();
            this.lblListado = new System.Windows.Forms.Label();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblFiltroEstado = new System.Windows.Forms.Label();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDescripcionPlan = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.precio = new System.Windows.Forms.TextBox();
            this.nuevo = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.reactivar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).BeginInit();
            this.splitContenido.Panel1.SuspendLayout();
            this.splitContenido.Panel2.SuspendLayout();
            this.splitContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.SuspendLayout();
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(16, 638);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(2, 0, 12, 0);
            this.lblEstado.Size = new System.Drawing.Size(1068, 26);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // splitContenido
            //
            this.splitContenido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenido.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContenido.Location = new System.Drawing.Point(16, 16);
            this.splitContenido.Name = "splitContenido";
            //
            // splitContenido.Panel1
            //
            this.splitContenido.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContenido.Panel1.Controls.Add(this.lblListado);
            this.splitContenido.Panel1.Controls.Add(this.lblBuscar);
            this.splitContenido.Panel1.Controls.Add(this.buscador);
            this.splitContenido.Panel1.Controls.Add(this.lblFiltroEstado);
            this.splitContenido.Panel1.Controls.Add(this.filtroEstado);
            this.splitContenido.Panel1.Controls.Add(this.tabla);
            this.splitContenido.Panel1.Padding = new System.Windows.Forms.Padding(16);
            this.splitContenido.Panel1MinSize = 400;
            //
            // splitContenido.Panel2
            //
            this.splitContenido.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContenido.Panel2.Controls.Add(this.lblFormulario);
            this.splitContenido.Panel2.Controls.Add(this.lblNombre);
            this.splitContenido.Panel2.Controls.Add(this.nombre);
            this.splitContenido.Panel2.Controls.Add(this.lblDescripcionPlan);
            this.splitContenido.Panel2.Controls.Add(this.descripcion);
            this.splitContenido.Panel2.Controls.Add(this.lblPrecio);
            this.splitContenido.Panel2.Controls.Add(this.precio);
            this.splitContenido.Panel2.Controls.Add(this.nuevo);
            this.splitContenido.Panel2.Controls.Add(this.guardar);
            this.splitContenido.Panel2.Controls.Add(this.actualizar);
            this.splitContenido.Panel2.Controls.Add(this.darDeBaja);
            this.splitContenido.Panel2.Controls.Add(this.reactivar);
            this.splitContenido.Panel2.Padding = new System.Windows.Forms.Padding(16);
            this.splitContenido.Panel2MinSize = 396;
            this.splitContenido.Size = new System.Drawing.Size(1068, 622);
            this.splitContenido.SplitterDistance = 656;
            this.splitContenido.SplitterWidth = 16;
            this.splitContenido.TabIndex = 0;
            //
            // lblListado
            //
            this.lblListado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(16, 16);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(622, 28);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Planes";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblBuscar
            //
            this.lblBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBuscar.Location = new System.Drawing.Point(16, 44);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(622, 20);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Busca por nombre o descripción";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(16, 70);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(408, 26);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblFiltroEstado
            //
            this.lblFiltroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroEstado.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltroEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltroEstado.Location = new System.Drawing.Point(430, 70);
            this.lblFiltroEstado.Name = "lblFiltroEstado";
            this.lblFiltroEstado.Size = new System.Drawing.Size(52, 26);
            this.lblFiltroEstado.TabIndex = 2;
            this.lblFiltroEstado.Text = "Estado:";
            this.lblFiltroEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // filtroEstado
            //
            this.filtroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});
            this.filtroEstado.Location = new System.Drawing.Point(490, 70);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(148, 29);
            this.filtroEstado.TabIndex = 1;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // tabla
            //
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabla.ColumnHeadersHeight = 46;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colPrecio,
            this.colEstado});
            this.tabla.Location = new System.Drawing.Point(16, 106);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(622, 498);
            this.tabla.TabIndex = 2;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colNombre
            //
            this.colNombre.FillWeight = 48F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 130;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            //
            // colPrecio
            //
            this.colPrecio.FillWeight = 25F;
            this.colPrecio.HeaderText = "Precio";
            this.colPrecio.MinimumWidth = 90;
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.FillWeight = 27F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 90;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // lblFormulario
            //
            this.lblFormulario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(16, 16);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(362, 32);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo plan";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblNombre
            //
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNombre.Location = new System.Drawing.Point(16, 58);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(116, 30);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nombre
            //
            this.nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Location = new System.Drawing.Point(140, 62);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(238, 26);
            this.nombre.TabIndex = 0;
            //
            // lblDescripcionPlan
            //
            this.lblDescripcionPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescripcionPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDescripcionPlan.Location = new System.Drawing.Point(16, 96);
            this.lblDescripcionPlan.Name = "lblDescripcionPlan";
            this.lblDescripcionPlan.Size = new System.Drawing.Size(116, 30);
            this.lblDescripcionPlan.TabIndex = 2;
            this.lblDescripcionPlan.Text = "Descripción:";
            this.lblDescripcionPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // descripcion
            //
            this.descripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descripcion.Location = new System.Drawing.Point(140, 100);
            this.descripcion.Name = "descripcion";
            this.descripcion.Size = new System.Drawing.Size(238, 26);
            this.descripcion.TabIndex = 1;
            //
            // lblPrecio
            //
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPrecio.Location = new System.Drawing.Point(16, 134);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(116, 30);
            this.lblPrecio.TabIndex = 3;
            this.lblPrecio.Text = "Precio:";
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // precio
            //
            this.precio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.precio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.precio.Location = new System.Drawing.Point(140, 138);
            this.precio.Name = "precio";
            this.precio.Size = new System.Drawing.Size(238, 26);
            this.precio.TabIndex = 2;
            this.precio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.precio_KeyPress);
            //
            // nuevo
            //
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(16, 188);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(112, 38);
            this.nuevo.TabIndex = 3;
            this.nuevo.Text = "+ Nuevo";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // guardar
            //
            this.guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.Location = new System.Drawing.Point(136, 188);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(112, 38);
            this.guardar.TabIndex = 4;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            //
            // actualizar
            //
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.actualizar.Location = new System.Drawing.Point(256, 188);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(112, 38);
            this.actualizar.TabIndex = 5;
            this.actualizar.Text = "Modificar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.darDeBaja.Location = new System.Drawing.Point(16, 234);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(112, 38);
            this.darDeBaja.TabIndex = 6;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // reactivar
            //
            this.reactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.reactivar.FlatAppearance.BorderSize = 0;
            this.reactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reactivar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.reactivar.Location = new System.Drawing.Point(136, 234);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new System.Drawing.Size(112, 38);
            this.reactivar.TabIndex = 7;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            //
            // GestionPlanesFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "GestionPlanesFormulario";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Planes";
            this.Load += new System.EventHandler(this.GestionPlanesFormulario_Load);
            this.splitContenido.Panel1.ResumeLayout(false);
            this.splitContenido.Panel1.PerformLayout();
            this.splitContenido.Panel2.ResumeLayout(false);
            this.splitContenido.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).EndInit();
            this.splitContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
