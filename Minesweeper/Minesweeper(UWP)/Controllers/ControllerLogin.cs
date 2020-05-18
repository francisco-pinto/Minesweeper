using Minesweeper_UWP_;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.View_Controller
{
    public class ControllerLogin
    {
        private App Program;
        public ControllerLogin()
        {
            Program = App.Current as App;
            Program.V_Login.FazerLogin += V_Login_FazerLogin;
        }

        private void V_Login_FazerLogin()
        {
            Program.M_Login.FazerLogin();
        }
    }
}
