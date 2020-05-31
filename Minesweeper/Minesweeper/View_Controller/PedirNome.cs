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
    public partial class PedirNome : Form
    {
        public event AtribuirNome AtribuirNome;
        public PedirNome()
        {
            InitializeComponent();
        }
        private void buttonAdicionar_Click(object sender, EventArgs e)
        {
            if(textBoxNomeJogador.Text == string.Empty)
            {
                MessageBox.Show("Introduza o seu nome");
            }
            else
            {
                AtribuirNome(textBoxNomeJogador.Text);
                textBoxNomeJogador.Clear();
                this.Hide();
                Program.V_Menu.AtualizaValoresRecorde();
                Program.V_Menu.Show();
            }
        }
    }
}
