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
    public partial class FormOnOff : Form
    {
        
        public FormOnOff()
        {
            InitializeComponent();
        }

        private void buttonRede_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.V_Login.Show();
        }

        private void buttonStand_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Program.V_Menu.Show();
            
        }
    }
}
