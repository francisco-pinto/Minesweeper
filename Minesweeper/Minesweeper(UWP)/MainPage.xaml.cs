using Minesweeper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core.Preview;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Minesweeper_UWP_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int numMinas;
        private int numLinhas;
        private int numColunas;
        private int segundos = 0;
        private Button[,] button;

        public event startGame play;
        public event MostraBandeirasTodas MostraBandeirasTodas;
        public event MostraBombasTodas MostraBombasTodas;
        public event GetMinas getMinas;
        public event AtualizarMinas AtualizarMinas;
        public event MostraConteudoQuadrado MostraConteudoQuadrado;
        public event AdicionaFlag AdicionaFlag;

        public int NMinas { get => nMinas; set => nMinas = value; }
        public int NColunas { get => nColunas; set => nColunas = value; }
        public int NLinhas { get => nLinhas; set => nLinhas = value; }

        public MainPage()
        {   
            this.InitializeComponent();
            //Muda tamanho da janela
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 110 + 42 * NLinhas, Width = 42 * NColunas });
            this.TextBlockMinas.Text = NMinas.ToString();
                 
        }
        public void CreateMapa(int nLinhas, int nColunas, int nMinas)
        {
            numMinas = nMinas;
            numLinhas = nLinhas;
            numColunas = nColunas;

            
            int ButtonX = 5;
            int ButtonY = 10;
            string nome;

            button = new Button[numLinhas, numColunas];

            CreateGridProprieties(numLinhas, numColunas);

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    nome = linha + "-" + coluna;
                    CreateButton(linha, coluna, ButtonX, ButtonY, nome);
                }
            }
        }
        public void CreateGridProprieties(int numLinhas, int numColunas)
        {
            //Fazer a grelha
            for (int linha = 0; linha < numLinhas; linha++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(42); 
                MapaGrid.RowDefinitions.Add(gridRow);
            }

            for(int coluna = 0; coluna < numColunas; coluna++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridCol.Width = new GridLength(42);
                MapaGrid.ColumnDefinitions.Add(gridCol);
            }
        }
        public void CreateButton(int linha, int coluna, int ButtonX, int ButtonY, string nome)
        {
            button[linha, coluna] = new Button();
            button[linha, coluna].Name = nome;
            button[linha, coluna].Height = 40;
            button[linha, coluna].Width = 40;
            button[linha, coluna].Content = nome;
            button[linha, coluna].Click += B_Click;

            MapaGrid.Children.Add(button[linha, coluna]);
            Grid.SetColumn(button[linha, coluna], coluna);
            Grid.SetRow(button[linha, coluna], linha);

        }
        //private void B_Click(object sender, RoutedEventArgs e)
        //{
        //    Button b = (Button)sender;
        //    MouseEventArgs me = e as MouseEventArgs;  //Evento do rato

        //    if (me.Button == MouseButtons.Right)
        //    {
        //        AdicionaFlag(b);
        //        AtualizarMinas(b);
        //        TextBlockMinas.Text = getMinas();

        //    }
        //    if (me.Button == MouseButtons.Left)
        //    {

        //        //iF ME BUTTON DOWN UMA CARA


        //        //if me button up outra cara


        //        MostraConteudoQuadrado(b);
        //        //MessageBox.Show(b.Name);
        //    }

        //}


        public void MostraTodasBandeiras()
        {
            MostraBandeirasTodas(button, numLinhas, numColunas);
        }
        public void LimparForm()
        {
            //this.Hide();
            //Tornar feliz quando reinicia
            string path = Environment.CurrentDirectory + @"/btns/smile.png";
            //ButtonCara.Image = Image.FromFile(path);
            //timer1.Stop();

            TextBlockMinas.Text = null;
            textblockTimer.Text = null;
            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    button[linha, coluna].Visibility = Visibility.Collapsed;
                    button[linha, coluna] = null;
                }
            }
        }

        public void PerderSad()
        {
            string path = Environment.CurrentDirectory + @"/btns/sad.png";
            ButtonCara.Image = Image.FromFile(path);
        }
        public void GanharHappy()
        {
            string path = Environment.CurrentDirectory + @"/btns/smile glasses.png";
            ButtonCara.Image = Image.FromFile(path);
        }
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    string path = Environment.CurrentDirectory + @"/Clock.wav";
        //    System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
        //    //player.Play();

        //    segundos += 1;

        //    if (segundos < 10)
        //    {
        //        labelTime.Text = "00" + segundos.ToString();
        //    }
        //    else if (segundos < 100)
        //    {
        //        labelTime.Text = "0" + segundos.ToString();
        //    }
        //    else if (segundos < 1000)
        //    {
        //        labelTime.Text = segundos.ToString();
        //    }
        //    else
        //    {
        //        labelTime.Text = "999";
        //    }
        //}




        // Adaptar codigo ao criar botao em cima
        //public void CreateButton(int linha, int coluna, CONTEUDO conteudoQuadrado, int ButtonX, int ButtonY, string nome)
        //{
        //    /*Colocar inicialmente imagem de fundo normal*/
        //    button[linha, coluna] = new Button();
        //    button[linha, coluna].Name = nome;
        //    button[linha, coluna].Size = new Size(40, 40);
        //    button[linha, coluna].Location = new Point(ButtonX, ButtonY);

        //    //Configurar os botões para permitir retirar margens a estes
        //    button[linha, coluna].FlatStyle = FlatStyle.Flat;
        //    button[linha, coluna].FlatAppearance.BorderColor = Color.Gray;
        //    button[linha, coluna].FlatAppearance.BorderSize = 1;

        //    button[linha, coluna].MouseDown += B_MouseDown;
        //    button[linha, coluna].MouseUp += B_MouseUp;

        //    //Só para testar
        //    if (conteudoQuadrado == CONTEUDO.BOMBA)
        //    {
        //        button[linha, coluna].BackColor = Color.Red;
        //    }
        //}

        private void B_MouseUp(object sender, MouseEventArgs e)
        {
            string path = Environment.CurrentDirectory + @"/btns/smile.png";
            ButtonCara.Image = Image.FromFile(path);
        }
        private void B_MouseDown(object sender, MouseEventArgs e)
        {
            string path = Environment.CurrentDirectory + @"/btns/boca.png";
            ButtonCara.Image = Image.FromFile(path);
        }

        public void AtualizaSimboloBotao(int linha, int coluna, string path)
        {
            if (path != null)
            {
                button[linha, coluna].Image = Image.FromFile(path);
            }
            else
            {
                button[linha, coluna].Image = null;
            }

        }
        public void AtualizaImagemConteudo(string nome, string path)
        {
            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (nome == button[linha, coluna].Name)
                    {
                        button[linha, coluna].Image = Image.FromFile(path);
                        return;
                    }
                }
            }
        }

        public void MostraTodasBombas()
        {
            MostraBombasTodas(button, numLinhas, numColunas);

        }
        public void MostraBandeirasErradas(string[] PosErradas)
        {
            string path = Environment.CurrentDirectory + @"/btns/btnBombaErrada.png";
            for (int i = 0; i < PosErradas.Length; i++)
            {
                string[] pos = PosErradas[i].Split('-');

                button[Int32.Parse(pos[0]), Int32.Parse(pos[1])].Image = Image.FromFile(path);
            }
        }
        public Button GetButton(int linha, int coluna)
        {
            return button[linha, coluna];
        }
        public void InicializarVariaveis()
        {
            //Timer
            segundos = 0;
            timer1.Enabled = true;

            //Nº minas
            TextBlockMinas.Text = getMinas();
            textblockTimer.Text = "000";
        }
        public void setVariaveisFinais(string nMinas, bool timerAtualization)
        {
            if (nMinas != "-1")
            {
                TextBlockMinas.Text = nMinas;
            }

            timer1.Enabled = timerAtualization;
        }
        

        private void ButtonCara_Click(object sender, RoutedEventArgs e)
        {
            LimparForm();
            play(numColunas, numLinhas, numMinas);
            TextBlockMinas.Text = getMinas();
        }
    }



    //private void FormMineSweeper_FormClosing(object sender, FormClosingEventArgs e)
    //{
    //    //Esconder e não eliminar form
    //    if (e.CloseReason == CloseReason.UserClosing)
    //    {
    //        e.Cancel = true;
    //        Hide();
    //    }
    //    LimparForm();
    //    Program.V_Menu.Show();
    //}
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 800, Width = 1000 });
            this.Frame.Navigate(typeof(Menu), null);
        }
        
        
    }
}
