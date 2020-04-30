using Minesweeper.Models;
using Minesweeper.View_Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Minesweeper.Models.Menu;

namespace Minesweeper
{
    static class Program
    {
        public static Mapa M_mapa { get; private set; }
        public static Menu M_menu { get; private set; }
        public static Jogador M_jogador { get; private set; }
        public static Login M_Login { get; private set; }
        public static FormMenu V_Menu { get; private set; }
        public static FormMineSweeper V_Mapa { get; private set; }
        public static FormLogin V_Login { get; private set; }
        public static Instrucoes V_Instrucoes { get; private set; }
        public static ControllerJogador C_jogador { get; private set; }
        public static ControllerMapa C_mapa { get; private set; }
        public static ControllerMenu C_menu { get; private set; }
        public static ControllerInstrucoes C_Intrucoes { get; private set; }
        public static ControllerLogin C_Login { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMineSweeper());

            //Models
            M_jogador = new Jogador();
            M_menu = new Menu();
            M_mapa = new Mapa();
            M_Login = new Login();
                                        
            //Views
            V_Menu = new FormMenu();
            V_Mapa = new FormMineSweeper();
            V_Instrucoes = new Instrucoes();
            V_Login = new FormLogin();

            //Controllers
            C_jogador = new ControllerJogador();
            C_menu = new ControllerMenu();
            C_mapa = new ControllerMapa();
            C_Intrucoes = new ControllerInstrucoes();
            C_Login = new ControllerLogin();

            Application.Run(V_Login);
        }
    }
}
