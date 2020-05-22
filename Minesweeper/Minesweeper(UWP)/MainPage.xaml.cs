using Minesweeper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core.Preview;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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

        private App Program = App.Current as App;

        //Timer
        private DispatcherTimer timer1;

        public event startGame play;
        //public event CreateButton CreateButtonModel;
        public event MostraBandeirasTodas MostraBandeirasTodas;
        public event MostraBombasTodas MostraBombasTodas;
        //public event GetMinas getMinas;
        public event AtualizarMinas AtualizarMinas;
        public event MostraConteudoQuadrado MostraConteudoQuadrado;
        public event AdicionaFlag AdicionaFlag;

        public MainPage()
        {   
            this.InitializeComponent();
            timer1 = new DispatcherTimer();
            timer1.Tick += Timer1_Tick; 


            //Colocar numa função de load
            //Muda tamanho da janela
            this.TextBlockMinas.Text = numMinas.ToString();
                 
        }

        private void Timer1_Tick(object sender, object e)
        {
            //string path = Environment.CurrentDirectory + @"/Clock.wav";
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            //player.Play();

            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Clock.wav"));
            mediaPlayer.Play();

            segundos += 1;

            if (segundos < 10)
            {
                TextBlockTimer.Text = "00" + segundos.ToString();
            }
            else if (segundos < 100)
            {
                TextBlockTimer.Text = "0" + segundos.ToString();
            }
            else if (segundos < 1000)
            {
                TextBlockTimer.Text = segundos.ToString();
            }
            else
            {
                TextBlockTimer.Text = "999";
            }
        }
        public void CreateMapa(int nLinhas, int nColunas, int nMinas)
        {
            numMinas = Program.M_mapa.NumMinasPorEncontrar;
            numLinhas = Program.M_mapa.NumLinhas;
            numColunas = Program.M_mapa.NumColunas;

            button = new Button[numLinhas, numColunas];
            //int ButtonX = 5;
            //int ButtonY = 10;
            //string nome;



            CreateGridProprieties(numLinhas, numColunas);

            //for (int linha = 0; linha < numLinhas; linha++)
            //{
            //    for (int coluna = 0; coluna < numColunas; coluna++)
            //    {
            //        nome = linha + "-" + coluna;
            //        CreateButton(linha, coluna, ButtonX, ButtonY, nome);
            //    }
            //}
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
        public void CreateButton(int linha, int coluna, CONTEUDO conteudoQuadrado, int ButtonX, int ButtonY, string nome)
        {
            button[linha, coluna] = new Button();
            button[linha, coluna].Name = nome;
            button[linha, coluna].Height = 40;
            button[linha, coluna].Width = 40;
            button[linha, coluna].Content = nome;
            button[linha, coluna].Click += B_Click;
            button[linha, coluna].PointerPressed += MainPage_PointerPressed; ;
            button[linha, coluna].PointerReleased += MainPage_PointerReleased; ;
            /*Colocar inicialmente imagem de fundo normal*/
            //    button[linha, coluna].Location = new Point(ButtonX, ButtonY);

            //    //Só para testar
            if (conteudoQuadrado == CONTEUDO.BOMBA)
            {
                button[linha, coluna].Background = new SolidColorBrush(Colors.Red);
            }

            MapaGrid.Children.Add(button[linha, coluna]);
            Grid.SetColumn(button[linha, coluna], coluna);
            Grid.SetRow(button[linha, coluna], linha);

        }
        private void MainPage_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ButtonCara.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/smile.png")), Stretch = Stretch.None };
        }
        private void MainPage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ButtonCara.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/boca.png")), Stretch = Stretch.None };
        }
        private void B_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            PointerRoutedEventArgs me = e.OriginalSource as PointerRoutedEventArgs;  //Evento do rato

            //Verifica se trata de rato
            if (me.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
            {

                var mouse = me.GetCurrentPoint(this).Properties;

                if (mouse.IsRightButtonPressed)
                {
                    AdicionaFlag(b);
                    AtualizarMinas(b);
                    TextBlockMinas.Text = numMinas.ToString();

                }
                if (mouse.IsLeftButtonPressed)
                {
                    MostraConteudoQuadrado(b);
                    //MessageBox.Show(b.Name);
                }
            }
        }
        public void MostraTodasBandeiras()
        {
            MostraBandeirasTodas(button, numLinhas, numColunas);
        }
        public void LimparForm()
        {
            //this.Hide();
            //Tornar feliz quando reinicia
            //string path = Environment.CurrentDirectory + @"/btns/smile.png";
            ButtonCara.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/smile.png")), Stretch = Stretch.None };
          
            timer1.Stop();

            TextBlockMinas.Text = "";
            TextBlockTimer.Text = "";
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
            //string path = Environment.CurrentDirectory + @"/btns/sad.png";
            ButtonCara.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/sad.png")), Stretch = Stretch.None };
        }
        public void GanharHappy()
        {
            //string path = Environment.CurrentDirectory + @"/btns/smile glasses.png";
            ButtonCara.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/smile glasses.png")), Stretch = Stretch.None };
        }



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
        public void AtualizaSimboloBotao(int linha, int coluna, string path)
        {
            if (path != null)
            {
                button[linha, coluna].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/" + path)), Stretch = Stretch.None };
            }
            else
            {
                button[linha, coluna].Background = null;
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
                        
                        button[linha, coluna].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/" + path)), Stretch = Stretch.None };
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
            string path = "btnBombaErrada.png";
            for (int i = 0; i < PosErradas.Length; i++)
            {
                string[] pos = PosErradas[i].Split('-');

                button[Int32.Parse(pos[0]), Int32.Parse(pos[1])].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/" + path)), Stretch = Stretch.None };
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
            timer1.Start();

            //Nº minas
            TextBlockMinas.Text = numMinas.ToString();
            TextBlockTimer.Text = "000";
        }
        public void setVariaveisFinais(string nMinas, bool timerAtualization)
        {
            if (nMinas != "-1")
            {
                TextBlockMinas.Text = nMinas;
            }

            if(timerAtualization== true)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }
        private void ButtonCara_Click(object sender, RoutedEventArgs e)
        {
            if(numMinas != 0)
            {
                LimparForm();
                Program.C_menu.V_Menu_play(numColunas, numLinhas, numMinas);
            }
          
            CreateMapa(numMinas, numLinhas, numColunas);
            CreateButtonModel(numLinhas, numColunas, numMinas);
            Resize(30 + 40 * numColunas, 90 + 40 * numLinhas);
            TextBlockMinas.Text = numMinas.ToString();
        }
        public void CreateButtonModel(int numLinhas, int numColunas, int numBombas)
        {
            int ButtonX = 0, ButtonY = 40;
            CONTEUDO[,] conteudoQuadrado = new CONTEUDO[numLinhas, numColunas];
            int[,] distanciaBomba = new int[numLinhas, numColunas];

            preencherQuadrado(numLinhas, numColunas, numBombas, conteudoQuadrado);
            preencherDistancias(numLinhas, numColunas, conteudoQuadrado, distanciaBomba);

            for (int linha = 0; linha < numLinhas; linha++)
            {
                ButtonX = 5;
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    //Nome irá identificar a posição dos botões
                    string nome = linha.ToString() + "-" + coluna.ToString();

                    //Possível Evento de set do quadrado
                    Program.M_mapa.setQuadrado(distanciaBomba[linha, coluna], conteudoQuadrado[linha, coluna], coluna, linha, ButtonX, ButtonY);
                    //quadrado[linha, coluna] = new Quadrado(distanciaBomba[linha, coluna], conteudoQuadrado[linha, coluna], coluna, linha, ButtonX, ButtonY, nome);


                    CreateButton(linha, coluna, conteudoQuadrado[linha, coluna], ButtonX, ButtonY, nome);
                    ButtonX += 40;
                }
                ButtonY += 40;
            }
        }
        public void preencherQuadrado(int numLinhas, int numColunas, int numMinas, CONTEUDO[,] localizacaoBombas)
        {
            int contador = 0;
            var rand = new Random();

            do
            {
                int linha = rand.Next(0, numLinhas);
                int coluna = rand.Next(0, numColunas);

                if (localizacaoBombas[linha, coluna] != CONTEUDO.BOMBA)
                {
                    contador++;
                    localizacaoBombas[linha, coluna] = CONTEUDO.BOMBA;
                }

            } while (contador != numMinas);
        }
        private int BombasAVolta(CONTEUDO[,] conteudoQuadrado, int linha, int coluna, int numLinhas, int numColunas)
        {
            int counter = 0;
            bool algumaBomba = false;

            if (conteudoQuadrado[linha, coluna] != CONTEUDO.BOMBA)
            {
                //Em baixo
                if ((linha + 1 < numLinhas) && (conteudoQuadrado[linha + 1, coluna] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em baixo direita
                if (((linha + 1 < numLinhas) && (coluna + 1 < numColunas)) && (conteudoQuadrado[linha + 1, coluna + 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em baixo esquerda
                if (((linha + 1 < numLinhas) && (coluna - 1 >= 0)) && (conteudoQuadrado[linha + 1, coluna - 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //À direita
                if ((coluna + 1 < numColunas) && (conteudoQuadrado[linha, coluna + 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em cima direita
                if (((linha - 1 >= 0) && (coluna + 1 < numColunas)) && (conteudoQuadrado[linha - 1, coluna + 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em cima
                if ((linha - 1 >= 0) && (conteudoQuadrado[linha - 1, coluna] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em cima esquerda
                if (((linha - 1 >= 0) && (coluna - 1 >= 0)) && (conteudoQuadrado[linha - 1, coluna - 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //À esquerda
                if ((coluna - 1 >= 0) && (conteudoQuadrado[linha, coluna - 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }
            }

            if (algumaBomba)
            {
                return counter;
            }
            else
            {
                return -1;
            }

        }
        private void preencherDistancias(int numLinhas, int numColunas, CONTEUDO[,] conteudoQuadrado, int[,] distanciaBomba)
        {

            int distancia = -1;

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (conteudoQuadrado[linha, coluna] == CONTEUDO.BOMBA)
                    {
                        distanciaBomba[linha, coluna] = 0;
                    }
                    else if ((distancia = BombasAVolta(conteudoQuadrado, linha, coluna, numLinhas, numColunas)) != -1)
                    {
                        distanciaBomba[linha, coluna] = distancia;
                        distancia = -1;
                    }
                    else
                    {
                        distanciaBomba[linha, coluna] = distancia;
                    }
                }
            }
        }
        public void showPage()
        {
            Frame.Navigate(typeof(MainPage));
        }
        public bool IsVisible()
        {
            if(this.Frame != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Resize(int altura, int largura)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = altura, Width = largura });
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
            ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 800, Width = 1000 });
            this.Frame.Navigate(typeof(Menu), null);
        }
    } 
}
