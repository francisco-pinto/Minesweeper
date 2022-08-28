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
            this.buttonVoltarAoMenu.Location = new System.Drawing.Point(229, 357);
            this.buttonVoltarAoMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonVoltarAoMenu.Name = "buttonVoltarAoMenu";
            this.buttonVoltarAoMenu.Size = new System.Drawing.Size(152, 28);
            this.buttonVoltarAoMenu.TabIndex = 0;
            this.buttonVoltarAoMenu.Text = "Voltar ao menu";
            this.buttonVoltarAoMenu.UseVisualStyleBackColor = true;
            this.buttonVoltarAoMenu.Click += new System.EventHandler(this.buttonVoltarAoMenu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2012, 139);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "j";
            // 
            // labelInstrucoes
            // 
            this.labelInstrucoes.AutoSize = true;
            this.labelInstrucoes.Location = new System.Drawing.Point(52, 39);
            this.labelInstrucoes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInstrucoes.Name = "labelInstrucoes";
            this.labelInstrucoes.Size = new System.Drawing.Size(509, 289);
            this.labelInstrucoes.TabIndex = 2;
            this.labelInstrucoes.Text = resources.GetString("labelInstrucoes.Text");
            // 
            // Instrucoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 418);
            this.Controls.Add(this.labelInstrucoes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonVoltarAoMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Instrucoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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