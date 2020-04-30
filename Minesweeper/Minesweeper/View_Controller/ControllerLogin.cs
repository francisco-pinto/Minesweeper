using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.View_Controller
{
    class ControllerLogin
    {
        public ControllerLogin()
        {

            Program.V_Login.FazerLogin += V_Login_FazerLogin;
        }

        private void V_Login_FazerLogin()
        {
            Program.M_Login.FazerLogin();
        }
    }
}
