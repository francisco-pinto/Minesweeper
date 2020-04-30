namespace Minesweeper
{
    partial class Instrucoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Instrucoes));
            this.buttonVoltarAoMenu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelInstrucoes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonVoltarAoMenu
            // 
            this.buttonVoltarAoMenu.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonVoltarAoMenu.Location = new System.Drawing.Point(172, 290);
            this.buttonVoltarAoMenu.Name = "buttonVoltarAoMenu";
            this.buttonVoltarAoMenu.Size = new System.Drawing.Size(114, 23);
            this.buttonVoltarAoMenu.TabIndex = 0;
            this.buttonVoltarAoMenu.Text = "Voltar ao menu";
            this.buttonVoltarAoMenu.UseVisualStyleBackColor = true;
            this.buttonVoltarAoMenu.Click += new System.EventHandler(this.buttonVoltarAoMenu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1509, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(9, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "j";
            // 
            // labelInstrucoes
            // 
            this.labelInstrucoes.AutoSize = true;
            this.labelInstrucoes.Location = new System.Drawing.Point(39, 32);
            this.labelInstrucoes.Name = "labelInstrucoes";
            this.labelInstrucoes.Size = new System.Drawing.Size(379, 221);
            this.labelInstrucoes.TabIndex = 2;
            this.labelInstrucoes.Text = resources.GetString("labelInstrucoes.Text");
            // 
            // Instrucoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 340);
            this.Controls.Add(this.labelInstrucoes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonVoltarAoMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Instrucoes";
            this.Text = "Instruções";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonVoltarAoMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelInstrucoes;
    }
}