namespace Minesweeper.View_Controller
{
    partial class PedirNome
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAdicionarFacil = new System.Windows.Forms.Button();
            this.textBoxNomeJogadorFacil = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(79, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parabéns! Fez o melhor tempo!\r\nIntroduza o seu nome:\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAdicionarFacil
            // 
            this.buttonAdicionarFacil.Location = new System.Drawing.Point(132, 181);
            this.buttonAdicionarFacil.Name = "buttonAdicionarFacil";
            this.buttonAdicionarFacil.Size = new System.Drawing.Size(75, 23);
            this.buttonAdicionarFacil.TabIndex = 1;
            this.buttonAdicionarFacil.Text = "Adicionar";
            this.buttonAdicionarFacil.UseVisualStyleBackColor = true;
            // 
            // textBoxNomeJogadorFacil
            // 
            this.textBoxNomeJogadorFacil.Location = new System.Drawing.Point(103, 134);
            this.textBoxNomeJogadorFacil.Name = "textBoxNomeJogadorFacil";
            this.textBoxNomeJogadorFacil.Size = new System.Drawing.Size(133, 20);
            this.textBoxNomeJogadorFacil.TabIndex = 2;
            // 
            // PedirNomeFacil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 287);
            this.Controls.Add(this.textBoxNomeJogadorFacil);
            this.Controls.Add(this.buttonAdicionarFacil);
            this.Controls.Add(this.label1);
            this.Name = "PedirNomeFacil";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAdicionarFacil;
        private System.Windows.Forms.TextBox textBoxNomeJogadorFacil;
    }
}