using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class RutinaSemanalFormulario
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private Label lblEstado; private Panel panelContenido; private DataGridView tablaSemana;
        private DataGridViewTextBoxColumn colLunes; private DataGridViewTextBoxColumn colMartes; private DataGridViewTextBoxColumn colMiercoles; private DataGridViewTextBoxColumn colJueves; private DataGridViewTextBoxColumn colViernes;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); lblEstado = new Label(); panelContenido = new Panel(); tablaSemana = new DataGridView(); colLunes = new DataGridViewTextBoxColumn(); colMartes = new DataGridViewTextBoxColumn(); colMiercoles = new DataGridViewTextBoxColumn(); colJueves = new DataGridViewTextBoxColumn(); colViernes = new DataGridViewTextBoxColumn(); panelEncabezado.SuspendLayout(); panelContenido.SuspendLayout(); ((ISupportInitialize)(tablaSemana)).BeginInit(); SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229); panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Text = "Rutina semanal";
            lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Text = "Entrenamiento del socio de lunes a viernes";
            btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229); btnVolver.Text = "Cerrar"; btnVolver.UseVisualStyleBackColor = false;
            lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.ForeColor = Color.FromArgb(51, 65, 85); lblEstado.Text = "Listo";
            panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Padding = new Padding(16); panelContenido.Controls.Add(tablaSemana);
            tablaSemana.BackgroundColor = Color.White; tablaSemana.BorderStyle = BorderStyle.None;
            tablaSemana.Columns.AddRange(new DataGridViewColumn[] { colLunes, colMartes, colMiercoles, colJueves, colViernes });
            colLunes.HeaderText = "Lunes"; colLunes.Name = "colLunes"; colLunes.MinimumWidth = 120;
            colMartes.HeaderText = "Martes"; colMartes.Name = "colMartes"; colMartes.MinimumWidth = 120;
            colMiercoles.HeaderText = "Miercoles"; colMiercoles.Name = "colMiercoles"; colMiercoles.MinimumWidth = 120;
            colJueves.HeaderText = "Jueves"; colJueves.Name = "colJueves"; colJueves.MinimumWidth = 120;
            colViernes.HeaderText = "Viernes"; colViernes.Name = "colViernes"; colViernes.MinimumWidth = 120;
            panelEncabezado.Name = "panelEncabezado"; panelEncabezado.TabIndex = 0; lblTitulo.Name = "lblTitulo"; lblTitulo.TabIndex = 0; lblDescripcion.Name = "lblDescripcion"; lblDescripcion.TabIndex = 1; btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
            lblEstado.Name = "lblEstado"; lblEstado.TabIndex = 2; panelContenido.Name = "panelContenido"; panelContenido.TabIndex = 1; tablaSemana.Name = "tablaSemana"; tablaSemana.TabIndex = 0;

            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; AutoScroll = false; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1080, 620); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 520); this.Name = "RutinaSemanalFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Rutina semanal";

            // Armazon con Dock; los controles interiores conservan coordenadas propias y se adaptan con Anchor.
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1080, 84);
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Size = new System.Drawing.Size(870, 36);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcion.Location = new System.Drawing.Point(24, 48);
            this.lblDescripcion.Size = new System.Drawing.Size(870, 26);
            this.btnVolver.AutoSize = false;
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.Location = new System.Drawing.Point(954, 24);
            this.btnVolver.Size = new System.Drawing.Size(104, 38);
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Location = new System.Drawing.Point(0, 590);
            this.lblEstado.Size = new System.Drawing.Size(1080, 30);
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 0, 12, 0);
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 84);
            this.panelContenido.Size = new System.Drawing.Size(1080, 506);
            this.tablaSemana.AutoSize = false;
            this.tablaSemana.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tablaSemana.Location = new System.Drawing.Point(16, 16);
            this.tablaSemana.Size = new System.Drawing.Size(1048, 474);
            this.tablaSemana.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaSemana.ReadOnly = true;
            this.tablaSemana.AllowUserToAddRows = false;
            this.tablaSemana.AllowUserToDeleteRows = false;
            this.tablaSemana.AllowUserToResizeRows = false;
            this.tablaSemana.RowHeadersVisible = false;
            this.tablaSemana.MultiSelect = false;
            this.tablaSemana.ColumnHeadersHeight = 46;
            this.tablaSemana.RowTemplate.Height = 34;
            // Los nombres largos se muestran completos en varias lineas en lugar de recortarse.
            this.tablaSemana.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tablaSemana.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.tablaSemana.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;

            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); panelContenido.ResumeLayout(false); ((ISupportInitialize)(tablaSemana)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.RutinaSemanalFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
        }
    }
}
