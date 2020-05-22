using Windows.UI.Xaml.Controls;

namespace Minesweeper
{
    public delegate void startGame(int numLinhas, int numColunas, int numBombas);
    public delegate void CreateButton(int numLinhas, int numColunas, int numBombas);
    public delegate void MostraBandeirasTodas(Button[,] b, int numLinhas, int numColunas);
    public delegate void MostraBombasTodas(Button[,] b, int numLinhas, int numColunas);
    public delegate void MostraConteudoQuadrado(Button b);
    public delegate void AdicionaFlag(Button b);
    public delegate void AtualizarMinas(Button b);
    public delegate string GetMinas();
}
