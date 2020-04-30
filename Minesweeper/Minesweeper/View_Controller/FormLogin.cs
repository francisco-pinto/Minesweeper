using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper.View_Controller
{
    public delegate void  fazlogin();
    public partial class FormLogin : Form
    {
        public event fazlogin FazerLogin;
        public FormLogin()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            FazerLogin();
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void buttonRegistar_Click(object sender, EventArgs e)
        {
            Program.V_Login.Size = new System.Drawing.Size(500, 400);
            labelemail.Visible = true;
            labelfoto.Visible = true;
            labelNomeAbreviado.Visible = true;
            labelPais.Visible = true;
            labelpassword.Visible = true;
            labelusername.Visible = true;
            textBoxEmail.Visible = true;
            textBoxNomeAbreviado.Visible = true;
            textBoxPais.Visible = true;
            textBoxPassword.Visible = true;
            textBoxUsername.Visible = true;

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            Program.V_Login.Size = new System.Drawing.Size(500, 100);
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            ofd.Filter = "PNG|*.png";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxFileName.Text = ofd.SafeFileName;
                textBoxSAvedFileName.Text = ofd.FileName;
            }
        }
    }
}
