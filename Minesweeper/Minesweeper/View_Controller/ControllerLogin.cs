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
            Program.V_Login.EnviarDados += V_Login_EnviarDados;
        }
        private void V_Login_FazerLogin()
        {
            Program.M_Login.FazerLogin();
        }
        private void V_Login_EnviarDados()
        {
            Program.M_menu.online = true;
            Program.V_Menu.online = true;
            Program.V_Menu.AlteraImagem();
            Program.V_Menu.ShowConsultaPerfil();
            Program.V_Menu.ShowFoto();
        }
    }
}
