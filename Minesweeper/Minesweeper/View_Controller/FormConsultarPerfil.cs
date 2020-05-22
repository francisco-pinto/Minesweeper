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
    public partial class FormConsultarPerfil : Form
    {
        public FormConsultarPerfil()
        {
            InitializeComponent();

            
        }

        OpenFileDialog ofd = new OpenFileDialog();
        
        private void buttonVoltarMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
