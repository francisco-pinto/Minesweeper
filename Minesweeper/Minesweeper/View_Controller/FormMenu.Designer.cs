namespace Minesweeper.View_Controller
{
    partial class FormMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.radioButtonFacil = new System.Windows.Forms.RadioButton();
            this.radioButtonMedia = new System.Windows.Forms.RadioButton();
            this.groupBoxInserirDificuldade = new System.Windows.Forms.GroupBox();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.buttonJogar = new System.Windows.Forms.Button();
            this.buttonInstrucoes = new System.Windows.Forms.Button();
            this.listBoxMedio = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxFacil = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConsultarPerfil = new System.Windows.Forms.Button();
            this.pictureBoxOnline = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.groupBoxInserirDificuldade.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOnline)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(160, 15);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(384, 158);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // radioButtonFacil
            // 
            this.radioButtonFacil.AutoSize = true;
            this.radioButtonFacil.Location = new System.Drawing.Point(20, 23);
            this.radioButtonFacil.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFacil.Name = "radioButtonFacil";
            this.radioButtonFacil.Size = new System.Drawing.Size(58, 21);
            this.radioButtonFacil.TabIndex = 2;
            this.radioButtonFacil.TabStop = true;
            this.radioButtonFacil.Text = "Fácil";
            this.radioButtonFacil.UseVisualStyleBackColor = true;
            this.radioButtonFacil.Click += new System.EventHandler(this.radioButtonFacil_Click);
            // 
            // radioButtonMedia
            // 
            this.radioButtonMedia.AutoSize = true;
            this.radioButtonMedia.Location = new System.Drawing.Point(184, 23);
            this.radioButtonMedia.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonMedia.Name = "radioButtonMedia";
            this.radioButtonMedia.Size = new System.Drawing.Size(67, 21);
            this.radioButtonMedia.TabIndex = 3;
            this.radioButtonMedia.TabStop = true;
            this.radioButtonMedia.Text = "Média";
            this.radioButtonMedia.UseVisualStyleBackColor = true;
            this.radioButtonMedia.Click += new System.EventHandler(this.radioButtonMedia_Click);
            // 
            // groupBoxInserirDificuldade
            // 
            this.groupBoxInserirDificuldade.Controls.Add(this.radioButtonCustom);
            this.groupBoxInserirDificuldade.Controls.Add(this.radioButtonFacil);
            this.groupBoxInserirDificuldade.Controls.Add(this.radioButtonMedia);
            this.groupBoxInserirDificuldade.Location = new System.Drawing.Point(131, 208);
            this.groupBoxInserirDificuldade.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxInserirDificuldade.Name = "groupBoxInserirDificuldade";
            this.groupBoxInserirDificuldade.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxInserirDificuldade.Size = new System.Drawing.Size(433, 52);
            this.groupBoxInserirDificuldade.TabIndex = 4;
            this.groupBoxInserirDificuldade.TabStop = false;
            this.groupBoxInserirDificuldade.Text = "Insira a dificuldade";
            // 
            // radioButtonCustom
            // 
            this.radioButtonCustom.AutoSize = true;
            this.radioButtonCustom.Location = new System.Drawing.Point(337, 23);
            this.radioButtonCustom.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.Size = new System.Drawing.Size(76, 21);
            this.radioButtonCustom.TabIndex = 4;
            this.radioButtonCustom.TabStop = true;
            this.radioButtonCustom.Text = "Custom";
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            this.radioButtonCustom.Click += new System.EventHandler(this.radioButtonCustom_Click);
            // 
            // buttonJogar
            // 
            this.buttonJogar.Location = new System.Drawing.Point(288, 267);
            this.buttonJogar.Margin = new System.Windows.Forms.Padding(4);
            this.buttonJogar.Name = "buttonJogar";
            this.buttonJogar.Size = new System.Drawing.Size(100, 28);
            this.buttonJogar.TabIndex = 5;
            this.buttonJogar.Text = "Jogar";
            this.buttonJogar.UseVisualStyleBackColor = true;
            this.buttonJogar.Click += new System.EventHandler(this.buttonJogar_Click);
            // 
            // buttonInstrucoes
            // 
            this.buttonInstrucoes.Location = new System.Drawing.Point(288, 320);
            this.buttonInstrucoes.Margin = new System.Windows.Forms.Padding(4);
            this.buttonInstrucoes.Name = "buttonInstrucoes";
            this.buttonInstrucoes.Size = new System.Drawing.Size(100, 28);
            this.buttonInstrucoes.TabIndex = 7;
            this.buttonInstrucoes.Text = "Instruções";
            this.buttonInstrucoes.UseVisualStyleBackColor = true;
            this.buttonInstrucoes.Click += new System.EventHandler(this.buttonInstrucoes_Click);
            // 
            // listBoxMedio
            // 
            this.listBoxMedio.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.listBoxMedio.FormattingEnabled = true;
            this.listBoxMedio.ItemHeight = 16;
            this.listBoxMedio.Location = new System.Drawing.Point(419, 448);
            this.listBoxMedio.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxMedio.Name = "listBoxMedio";
            this.listBoxMedio.Size = new System.Drawing.Size(144, 116);
            this.listBoxMedio.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(476, 410);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Média";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 410);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Fácil";
            // 
            // listBoxFacil
            // 
            this.listBoxFacil.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.listBoxFacil.FormattingEnabled = true;
            this.listBoxFacil.ItemHeight = 16;
            this.listBoxFacil.Location = new System.Drawing.Point(131, 448);
            this.listBoxFacil.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxFacil.Name = "listBoxFacil";
            this.listBoxFacil.Size = new System.Drawing.Size(144, 116);
            this.listBoxFacil.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(269, 373);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Melhores Tempos";
            // 
            // buttonConsultarPerfil
            // 
            this.buttonConsultarPerfil.Location = new System.Drawing.Point(31, 133);
            this.buttonConsultarPerfil.Name = "buttonConsultarPerfil";
            this.buttonConsultarPerfil.Size = new System.Drawing.Size(89, 49);
            this.buttonConsultarPerfil.TabIndex = 17;
            this.buttonConsultarPerfil.Text = "Consultar Perfil";
            this.buttonConsultarPerfil.UseVisualStyleBackColor = true;
            this.buttonConsultarPerfil.Click += new System.EventHandler(this.buttonConsultarPerfil_Click);
            // 
            // pictureBoxOnline
            // 
            this.pictureBoxOnline.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxOnline.Image")));
            this.pictureBoxOnline.Location = new System.Drawing.Point(12, 15);
            this.pictureBoxOnline.Name = "pictureBoxOnline";
            this.pictureBoxOnline.Size = new System.Drawing.Size(40, 40);
            this.pictureBoxOnline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOnline.TabIndex = 18;
            this.pictureBoxOnline.TabStop = false;
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 593);
            this.Controls.Add(this.pictureBoxOnline);
            this.Controls.Add(this.buttonConsultarPerfil);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxMedio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxFacil);
            this.Controls.Add(this.buttonInstrucoes);
            this.Controls.Add(this.buttonJogar);
            this.Controls.Add(this.groupBoxInserirDificuldade);
            this.Controls.Add(this.pictureBoxLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMenu_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.groupBoxInserirDificuldade.ResumeLayout(false);
            this.groupBoxInserirDificuldade.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOnline)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.RadioButton radioButtonFacil;
        private System.Windows.Forms.RadioButton radioButtonMedia;
        private System.Windows.Forms.GroupBox groupBoxInserirDificuldade;
        private System.Windows.Forms.Button buttonJogar;
        private System.Windows.Forms.Button buttonInstrucoes;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.ListBox listBoxMedio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxFacil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonConsultarPerfil;
        private System.Windows.Forms.PictureBox pictureBoxOnline;
    }
}