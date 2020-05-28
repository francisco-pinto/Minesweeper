namespace Minesweeper.View_Controller
{
    partial class FormConsultarPerfil
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConsultarPerfil));
            this.labelPais = new System.Windows.Forms.Label();
            this.labelfoto = new System.Windows.Forms.Label();
            this.labelemail = new System.Windows.Forms.Label();
            this.textBoxPais = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxNomeAbreviado = new System.Windows.Forms.TextBox();
            this.labelNomeAbreviado = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxFoto = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPais
            // 
            this.labelPais.AutoSize = true;
            this.labelPais.Location = new System.Drawing.Point(112, 208);
            this.labelPais.Name = "labelPais";
            this.labelPais.Size = new System.Drawing.Size(39, 17);
            this.labelPais.TabIndex = 43;
            this.labelPais.Text = "País:";
            // 
            // labelfoto
            // 
            this.labelfoto.AutoSize = true;
            this.labelfoto.Location = new System.Drawing.Point(109, 277);
            this.labelfoto.Name = "labelfoto";
            this.labelfoto.Size = new System.Drawing.Size(76, 17);
            this.labelfoto.TabIndex = 42;
            this.labelfoto.Text = "Fotografia:";
            // 
            // labelemail
            // 
            this.labelemail.AutoSize = true;
            this.labelemail.Location = new System.Drawing.Point(109, 144);
            this.labelemail.Name = "labelemail";
            this.labelemail.Size = new System.Drawing.Size(42, 17);
            this.labelemail.TabIndex = 41;
            this.labelemail.Text = "Email";
            // 
            // textBoxPais
            // 
            this.textBoxPais.Location = new System.Drawing.Point(300, 208);
            this.textBoxPais.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPais.Name = "textBoxPais";
            this.textBoxPais.Size = new System.Drawing.Size(255, 22);
            this.textBoxPais.TabIndex = 40;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(300, 144);
            this.textBoxEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(255, 22);
            this.textBoxEmail.TabIndex = 39;
            // 
            // textBoxNomeAbreviado
            // 
            this.textBoxNomeAbreviado.Location = new System.Drawing.Point(300, 85);
            this.textBoxNomeAbreviado.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxNomeAbreviado.Name = "textBoxNomeAbreviado";
            this.textBoxNomeAbreviado.Size = new System.Drawing.Size(255, 22);
            this.textBoxNomeAbreviado.TabIndex = 36;
            // 
            // labelNomeAbreviado
            // 
            this.labelNomeAbreviado.AutoSize = true;
            this.labelNomeAbreviado.Location = new System.Drawing.Point(109, 88);
            this.labelNomeAbreviado.Name = "labelNomeAbreviado";
            this.labelNomeAbreviado.Size = new System.Drawing.Size(116, 17);
            this.labelNomeAbreviado.TabIndex = 33;
            this.labelNomeAbreviado.Text = "Nome abreviado:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(357, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 29);
            this.label1.TabIndex = 47;
            this.label1.Text = "Perfil";
            // 
            // pictureBoxFoto
            // 
            this.pictureBoxFoto.Location = new System.Drawing.Point(353, 285);
            this.pictureBoxFoto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxFoto.Name = "pictureBoxFoto";
            this.pictureBoxFoto.Size = new System.Drawing.Size(91, 94);
            this.pictureBoxFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFoto.TabIndex = 48;
            this.pictureBoxFoto.TabStop = false;
            // 
            // FormConsultarPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 450);
            this.Controls.Add(this.pictureBoxFoto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPais);
            this.Controls.Add(this.labelfoto);
            this.Controls.Add(this.labelemail);
            this.Controls.Add(this.textBoxPais);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxNomeAbreviado);
            this.Controls.Add(this.labelNomeAbreviado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormConsultarPerfil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minesweeper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConsultarPerfil_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelPais;
        private System.Windows.Forms.Label labelfoto;
        private System.Windows.Forms.Label labelemail;
        private System.Windows.Forms.TextBox textBoxPais;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxNomeAbreviado;
        private System.Windows.Forms.Label labelNomeAbreviado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxFoto;
    }
}