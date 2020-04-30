namespace Minesweeper
{
    partial class FormMineSweeper
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMineSweeper));
            this.labelTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelMinas = new System.Windows.Forms.Label();
            this.buttonReiniciar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(394, 9);
            this.labelTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(51, 26);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "000";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelMinas
            // 
            this.labelMinas.AutoSize = true;
            this.labelMinas.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinas.Location = new System.Drawing.Point(11, 9);
            this.labelMinas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMinas.Name = "labelMinas";
            this.labelMinas.Size = new System.Drawing.Size(38, 26);
            this.labelMinas.TabIndex = 2;
            this.labelMinas.Text = "00";
            // 
            // buttonReiniciar
            // 
            this.buttonReiniciar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonReiniciar.Image = ((System.Drawing.Image)(resources.GetObject("buttonReiniciar.Image")));
            this.buttonReiniciar.Location = new System.Drawing.Point(210, 6);
            this.buttonReiniciar.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReiniciar.Name = "buttonReiniciar";
            this.buttonReiniciar.Size = new System.Drawing.Size(30, 32);
            this.buttonReiniciar.TabIndex = 4;
            this.buttonReiniciar.UseVisualStyleBackColor = true;
            this.buttonReiniciar.Click += new System.EventHandler(this.buttonReiniciar_Click);
            // 
            // FormMineSweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 325);
            this.Controls.Add(this.buttonReiniciar);
            this.Controls.Add(this.labelMinas);
            this.Controls.Add(this.labelTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMineSweeper";
            this.Text = "MineSweeper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMineSweeper_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelMinas;
        private System.Windows.Forms.Button buttonReiniciar;
    }
}

