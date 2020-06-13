using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Minesweeper
{
    public delegate void RestartOnlineGame();
    public delegate void startGameOnline(XDocument xmlresposta, int numLinhas, int numColunas, int numBombas);
    public delegate void startGame(int numLinhas, int numColunas, int numBombas);
    public delegate void MostraBandeirasTodas(Button[,] b, int numLinhas, int numColunas);
    public delegate void MostraBombasTodas(Button[,] b, int numLinhas, int numColunas);
    public delegate void MostraConteudoQuadrado(Button b);
    public delegate void AdicionaFlag(Button b);
    public delegate void AtualizarMinas(Button b);
    public delegate string GetMinas();
    public delegate void dadosUtilizador(string id, string Nome);
    public delegate void AtribuirNome(string nome);
    public delegate string GetNome();
}
