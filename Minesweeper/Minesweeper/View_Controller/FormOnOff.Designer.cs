namespace Minesweeper.View_Controller
{
    partial class FormOnOff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOnOff));
            this.labelOnOff = new System.Windows.Forms.Label();
            this.buttonRede = new System.Windows.Forms.Button();
            this.buttonStand = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelOnOff
            // 
            this.labelOnOff.AutoSize = true;
            this.labelOnOff.Font = new System.Drawing.Font("Microsoft Tai Le", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOnOff.Location = new System.Drawing.Point(158, 58);
            this.labelOnOff.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOnOff.Name = "labelOnOff";
            this.labelOnOff.Size = new System.Drawing.Size(211, 29);
            this.labelOnOff.TabIndex = 0;
            this.labelOnOff.Text = "Como deseja jogar:";
            // 
            // buttonRede
            // 
            this.buttonRede.Location = new System.Drawing.Point(94, 141);
            this.buttonRede.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonRede.Name = "buttonRede";
            this.buttonRede.Size = new System.Drawing.Size(104, 60);
            this.buttonRede.TabIndex = 1;
            this.buttonRede.Text = "Rede";
            this.buttonRede.UseVisualStyleBackColor = true;
            this.buttonRede.Click += new System.EventHandler(this.buttonRede_Click);
            // 
            // buttonStand
            // 
            this.buttonStand.Location = new System.Drawing.Point(306, 141);
            this.buttonStand.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonStand.Name = "buttonStand";
            this.buttonStand.Size = new System.Drawing.Size(106, 60);
            this.buttonStand.TabIndex = 2;
            this.buttonStand.Text = "Standalone";
            this.buttonStand.UseVisualStyleBackColor = true;
            this.buttonStand.Click += new System.EventHandler(this.buttonStand_Click);
            // 
            // FormOnOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 265);
            this.Controls.Add(this.buttonStand);
            this.Controls.Add(this.buttonRede);
            this.Controls.Add(this.labelOnOff);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormOnOff";
            this.Text = "FormOnOff";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelOnOff;
        private System.Windows.Forms.Button buttonRede;
        private System.Windows.Forms.Button buttonStand;
    }
}