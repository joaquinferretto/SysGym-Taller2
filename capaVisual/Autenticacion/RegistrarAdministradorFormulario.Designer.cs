using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Autenticacion
{
    partial class RegistrarAdministradorFormulario
    {
        private IContainer components;
        private Label titulo;
        private Label ayuda;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private Label lblSexo;
        private Label lblUsuario;
        private Label lblClave;
        private Label lblConfirmarClave;
        private TextBox nombre;
        private TextBox apellido;
        private TextBox dni;
        private ComboBox sexo;
        private TextBox nombreUsuario;
        private TextBox clave;
        private TextBox confirmarClave;
        private Label lblEstado;
        private Button btnRegistrar;
        private Panel barraSuperior;
        private ErrorProvider indicadorErrores;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            titulo = new Label(); ayuda = new Label(); lblNombre = new Label(); lblApellido = new Label();
            lblDni = new Label(); lblSexo = new Label(); lblUsuario = new Label(); lblClave = new Label(); lblConfirmarClave = new Label();
            nombre = new TextBox(); apellido = new TextBox(); dni = new TextBox(); sexo = new ComboBox(); nombreUsuario = new TextBox();
            clave = new TextBox(); confirmarClave = new TextBox(); lblEstado = new Label(); btnRegistrar = new Button();
            barraSuperior = new Panel(); indicadorErrores = new ErrorProvider(components);
            ((ISupportInitialize)indicadorErrores).BeginInit();
            SuspendLayout();

            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(560, 620);
            ControlBox = true;
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = global::exxen2._0.Properties.Resources.SysGym;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegistrarAdministradorFormulario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SysGym";

            barraSuperior.BackColor = Color.FromArgb(20, 184, 166);
            barraSuperior.Dock = DockStyle.Top;
            barraSuperior.Name = "barraSuperior";
            barraSuperior.Size = new Size(560, 7);

            titulo.AutoSize = false;
            titulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(31, 41, 55);
            titulo.Location = new Point(38, 27);
            titulo.Name = "titulo";
            titulo.Size = new Size(484, 40);
            titulo.Text = "Registrar administrador";
            titulo.TextAlign = ContentAlignment.MiddleLeft;

            ayuda.AutoSize = false;
            ayuda.Font = new Font("Segoe UI", 9.5F);
            ayuda.ForeColor = Color.FromArgb(107, 114, 128);
            ayuda.Location = new Point(40, 72);
            ayuda.Name = "ayuda";
            ayuda.Size = new Size(480, 38);
            ayuda.Text = "Completá los datos para habilitar el acceso inicial al sistema.";

            PrepararEtiqueta(lblNombre, "Nombre", 40, 118);
            PrepararEtiqueta(lblApellido, "Apellido", 40, 176);
            PrepararEtiqueta(lblDni, "DNI", 40, 234);
            PrepararEtiqueta(lblSexo, "Sexo", 40, 292);
            PrepararEtiqueta(lblUsuario, "Usuario", 40, 350);
            PrepararEtiqueta(lblClave, "Contraseña", 40, 408);
            PrepararEtiqueta(lblConfirmarClave, "Confirmar contraseña", 40, 466);
            PrepararCampo(nombre, 40, 144, 480, 30, 0);
            PrepararCampo(apellido, 40, 202, 480, 30, 1);
            PrepararCampo(dni, 40, 260, 480, 30, 2);
            sexo.DropDownStyle = ComboBoxStyle.DropDownList;
            sexo.Items.AddRange(new object[] { "Masculino", "Femenino" });
            sexo.BackColor = Color.White;
            sexo.Font = new Font("Segoe UI", 10.5F);
            sexo.ForeColor = Color.FromArgb(15, 23, 42);
            sexo.SelectedIndex = -1;
            sexo.Location = new Point(40, 318);
            sexo.Name = "sexo";
            sexo.Size = new Size(480, 30);
            sexo.TabIndex = 3;
            PrepararCampo(nombreUsuario, 40, 376, 480, 30, 4);
            PrepararCampo(clave, 40, 434, 480, 30, 5);
            PrepararCampo(confirmarClave, 40, 492, 480, 30, 6);
            clave.UseSystemPasswordChar = true;
            confirmarClave.UseSystemPasswordChar = true;

            lblEstado.AutoSize = false;
            lblEstado.Font = new Font("Segoe UI", 9F);
            lblEstado.ForeColor = Color.FromArgb(107, 114, 128);
            lblEstado.Location = new Point(40, 530);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(480, 34);

            btnRegistrar.BackColor = Color.FromArgb(46, 125, 50);
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.FlatAppearance.MouseDownBackColor = Color.FromArgb(27, 75, 31);
            btnRegistrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(37, 101, 41);
            btnRegistrar.FlatStyle = FlatStyle.Flat;
            btnRegistrar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnRegistrar.ForeColor = Color.White;
            btnRegistrar.Location = new Point(40, 570);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(480, 40);
            btnRegistrar.Text = "Registrar administrador";
            btnRegistrar.UseVisualStyleBackColor = false;

            indicadorErrores.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            indicadorErrores.ContainerControl = this;
            Controls.Add(titulo); Controls.Add(ayuda); Controls.Add(lblNombre); Controls.Add(nombre);
            Controls.Add(lblApellido); Controls.Add(apellido); Controls.Add(lblDni); Controls.Add(dni);
            Controls.Add(lblSexo); Controls.Add(sexo);
            Controls.Add(lblUsuario); Controls.Add(nombreUsuario); Controls.Add(lblClave); Controls.Add(clave);
            Controls.Add(lblConfirmarClave); Controls.Add(confirmarClave); Controls.Add(lblEstado); Controls.Add(btnRegistrar);
            Controls.Add(barraSuperior);

            nombre.KeyPress += nombre_KeyPress;
            apellido.KeyPress += nombre_KeyPress;
            dni.KeyPress += dni_KeyPress;
            nombreUsuario.KeyPress += nombreUsuario_KeyPress;
            nombre.Validating += nombre_Validating;
            apellido.Validating += nombre_Validating;
            dni.Validating += dni_Validating;
            sexo.SelectedIndexChanged += campo_TextChanged;
            nombreUsuario.Validating += nombreUsuario_Validating;
            nombre.TextChanged += campo_TextChanged; apellido.TextChanged += campo_TextChanged;
            dni.TextChanged += campo_TextChanged; nombreUsuario.TextChanged += campo_TextChanged;
            clave.TextChanged += campo_TextChanged; confirmarClave.TextChanged += campo_TextChanged;
            btnRegistrar.Click += btnRegistrar_Click;

            ((ISupportInitialize)indicadorErrores).EndInit();
            ResumeLayout(false);
        }

        private void PrepararEtiqueta(Label etiqueta, string texto, int x, int y)
        {
            etiqueta.AutoSize = true;
            etiqueta.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            etiqueta.ForeColor = Color.FromArgb(55, 65, 81);
            etiqueta.Location = new Point(x, y);
            etiqueta.Text = texto;
        }

        private void PrepararCampo(TextBox campo, int x, int y, int ancho, int alto, int tabIndex)
        {
            campo.BackColor = Color.White;
            campo.BorderStyle = BorderStyle.FixedSingle;
            campo.Font = new Font("Segoe UI", 10.5F);
            campo.ForeColor = Color.FromArgb(15, 23, 42);
            campo.Location = new Point(x, y);
            campo.Size = new Size(ancho, alto);
            campo.TabIndex = tabIndex;
        }
    }
}
