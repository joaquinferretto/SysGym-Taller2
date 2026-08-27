namespace exxen2._0.capaVisual
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SysgymLogin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nombrelogin = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SysgymLogin
            // 
            this.SysgymLogin.AutoSize = true;
            this.SysgymLogin.BackColor = System.Drawing.Color.Transparent;
            this.SysgymLogin.Font = new System.Drawing.Font("MS UI Gothic", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SysgymLogin.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.SysgymLogin.Location = new System.Drawing.Point(417, 36);
            this.SysgymLogin.Name = "SysgymLogin";
            this.SysgymLogin.Size = new System.Drawing.Size(205, 47);
            this.SysgymLogin.TabIndex = 0;
            this.SysgymLogin.Text = "SYSGYM";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(138, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 47);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bienvenido a";
            // 
            // nombrelogin
            // 
            this.nombrelogin.AutoSize = true;
            this.nombrelogin.BackColor = System.Drawing.Color.Transparent;
            this.nombrelogin.Font = new System.Drawing.Font("MS UI Gothic", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombrelogin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.nombrelogin.Location = new System.Drawing.Point(12, 167);
            this.nombrelogin.Name = "nombrelogin";
            this.nombrelogin.Size = new System.Drawing.Size(423, 47);
            this.nombrelogin.TabIndex = 2;
            this.nombrelogin.Text = "Nombre de Usuario:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 28.2F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(425, 164);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(363, 54);
            this.textBox1.TabIndex = 3;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(818, 441);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.nombrelogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SysgymLogin);
            this.Name = "Login";
            this.Text = "Inicio de Sesion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SysgymLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label nombrelogin;
        private System.Windows.Forms.TextBox textBox1;
    }
}