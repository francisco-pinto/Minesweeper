using Minesweeper.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Size = Windows.Foundation.Size;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Minesweeper_UWP_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    ///

    //public delegate int GetMinas();
    public sealed partial class MainPage : Page
    {
        //public event GetMinas getMinas;


        private int numMinas;
        private int numLinhas;
        private int numColunas;
        private int segundos = 0;
        private Button[,] button;
        private int auxTimer = 0;
        private int auxBlockControls = 0;

        private App Program = App.Current as App;

        //Timer
        private DispatcherTimer timer1;

        public MainPage()
        {   
            this.InitializeComponent();
            //NavigationCacheMode = NavigationCacheMode.Required;
            timer1 = new DispatcherTimer();
            timer1.Interval = new TimeSpan(0, 0, 1);
            timer1.Tick += Timer1_Tick; 


            //Colocar numa função de load
            //Muda tamanho da janela
            this.TextBlockMinas.Text = numMinas.ToString();

            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "Minesweeper";
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
        public void InicializarValoresMapaView(int nLinhas, int nColunas, int nMinas)
        {
            numMinas = Program.M_mapa.NMinasTotais;
            numLinhas = Program.M_mapa.NumLinhas;
            numColunas = Program.M_mapa.NumColunas;

            TextBlockMinas.Text = numMinas.ToString();
            TextBlockTimer.Text = "000";
            timer1.Stop();
            auxTimer = 0;

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
                gridRow.Height = new GridLength(32); 
                MapaGrid.RowDefinitions.Add(gridRow);
            }

            for(int coluna = 0; coluna < numColunas; coluna++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridCol.Width = new GridLength(32);
                MapaGrid.ColumnDefinitions.Add(gridCol);
            }
        }
        public void CreateButton(int linha, int coluna, CONTEUDO conteudoQuadrado, int ButtonX, int ButtonY, string nome)
        {
            //Atributos
            button[linha, coluna] = new Button();
            button[linha, coluna].Name = nome;
            button[linha, coluna].Height = 30;
            button[linha, coluna].Width = 30;
            button[linha, coluna].Background = new SolidColorBrush(Colors.LightGray);
            //button[linha, coluna].Content = nome;

            //Eventos
            button[linha, coluna].Tapped += MainPage_TappedAsync;
            button[linha, coluna].RightTapped += MainPage_RightTappedAsync;
            button[linha, coluna].PointerPressed += MainPage_PointerPressed; 
            button[linha, coluna].PointerReleased += MainPage_PointerReleased;

            //Adicionar o PointerPressed ao botão esquerdo
            button[linha, coluna].AddHandler(PointerPressedEvent,
            new PointerEventHandler(MainPage_PointerPressed), true);
            //Adicionar o PointerReleased ao botão esquerdo
            button[linha, coluna].AddHandler(PointerReleasedEvent,
            new PointerEventHandler(MainPage_PointerReleased), true);


            //button[linha, coluna].

            /*Colocar inicialmente imagem de fundo normal*/
            //    button[linha, coluna].Location = new Point(ButtonX, ButtonY);

            //    //Só para testar
            //if (conteudoQuadrado == CONTEUDO.BOMBA)
            //{
            //    button[linha, coluna].Background = new SolidColorBrush(Colors.Red);
            //}

            MapaGrid.Children.Add(button[linha, coluna]);
            Grid.SetColumn(button[linha, coluna], coluna);
            Grid.SetRow(button[linha, coluna], linha);

        }
        private async void MainPage_TappedAsync(object sender, TappedRoutedEventArgs e)
        {
            Button b = (Button)sender;

            int aux = MostraConteudoQuadrado(b);

            if(aux == 0)            //Mostrar Quadrados Vazios
            {
                AtualizaImagemConteudo();
            }
            else if(aux == -1)       //Perder o jogo
            {
                await PerderJogoAsync();
                ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 900 });
                this.Frame.Navigate(typeof(Menu));
            }
            else if(aux == 1)       //Ganhar Jogo
            {
                await GanharJogoAsync();
                //ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 900 });
                //this.Frame.Navigate(typeof(Menu));
            }   
        }
        public int MostraConteudoQuadrado(Button b)
        {
            /*Identifica o quadrado que possui aquele nome*/
            int numLinhas = Program.M_mapa.NumLinhas;
            int numColunas = Program.M_mapa.NumColunas;
            int numMinas = Program.M_mapa.NumMinasPorEncontrar;
            string[] pos = b.Name.Split('-');
            int linha = Convert.ToInt32(pos[0]);
            int coluna = Convert.ToInt32(pos[1]);

            Quadrado quadrado = Program.M_mapa.GetQuadrado(linha, coluna);

            if (Program.M_mapa.CheckQuadradoSelecionado(linha, coluna) && ((quadrado.SimboloQuadrado == SIMBOLO.VAZIO) || (quadrado.SimboloQuadrado == SIMBOLO.QUESTION)))
            {
                //Botão vazio fazer abrir todos os vazios
                if (quadrado.DistanciaBomba == -1)
                {
                    MostraQuadradosVaziosTodos(linha, coluna, quadrado, numLinhas, numColunas);
                }

                //Mostrar quadrado individual
                MostrarQuadrado(linha, coluna, quadrado);
            }

            //Verifica se perdeu o jogo
            if (quadrado.ConteudoQuadrado == CONTEUDO.BOMBA && quadrado.SimboloQuadrado != SIMBOLO.BANDEIRA)
            {
                //PerderJogo();
                return -1;
            }

            //Verifica se ganha o jogo
            //Talvez mudar nome da função
            //Colocar condição de entrada para não percorrer sempre que código é executado
            return VerificarQuadradosExpostos(numLinhas, numColunas, numMinas);
        }
        public int VerificarQuadradosExpostos(int numLinhas, int numColunas, int numMinas)
        {
            int quadradoporver = numColunas * numLinhas;

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (!Program.M_mapa.CheckQuadradoSelecionado(linha, coluna) || (Program.M_mapa.CheckQuadradoSelecionado(linha, coluna) && Program.M_mapa.GetQuadrado(linha, coluna).SimboloQuadrado == SIMBOLO.BANDEIRA))
                    {
                        quadradoporver = quadradoporver - 1;
                        //MessageBox.Show("YAAAAAAAAAAA");
                    }
                }
            }

            if (quadradoporver == numMinas)
            {
                //GanharJogo();
                return 1;
            }
            return 0;
        }
        private void MostrarQuadrado(int linha, int coluna, Quadrado quadrado)
        {
            string nome = quadrado.Linha.ToString() + '-' + quadrado.Coluna.ToString();
            quadrado.Selecionado = true;
            //string path = Program.M_mapa.getImagePath(quadrado);
            //AtualizaImagemConteudo(nome, path);   //Ir pelo nome
        }
        public void MostraQuadradosVaziosTodos(int linha, int coluna, Quadrado quadrado, int numLinhas, int numColunas)
        {

            if ((!quadrado.Selecionado) && (quadrado.ConteudoQuadrado != CONTEUDO.NUM) && ((quadrado.SimboloQuadrado == SIMBOLO.VAZIO) || (quadrado.SimboloQuadrado == SIMBOLO.QUESTION)))
            {
                MostrarQuadrado(linha, coluna, quadrado);

                if (linha + 1 < numLinhas)
                {
                    MostraQuadradosVaziosTodos(linha + 1, coluna, Program.M_mapa.GetQuadrado(linha + 1, coluna), numLinhas, numColunas);

                    if (Program.M_mapa.GetQuadrado(linha + 1, coluna).ConteudoQuadrado == CONTEUDO.NUM)
                        MostrarQuadrado(linha + 1, coluna, Program.M_mapa.GetQuadrado(linha + 1, coluna));
                }

                if (linha - 1 >= 0)
                {
                    MostraQuadradosVaziosTodos(linha - 1, coluna, Program.M_mapa.GetQuadrado(linha - 1, coluna), numLinhas, numColunas);

                    if (Program.M_mapa.GetQuadrado(linha - 1, coluna).ConteudoQuadrado == CONTEUDO.NUM)
                        MostrarQuadrado(linha - 1, coluna, Program.M_mapa.GetQuadrado(linha - 1, coluna));
                }

                if ((linha + 1 < numLinhas) && (coluna + 1 < numColunas))
                {
                    MostraQuadradosVaziosTodos(linha + 1, coluna + 1, Program.M_mapa.GetQuadrado(linha + 1, coluna + 1), numLinhas, numColunas);

                    if (Program.M_mapa.GetQuadrado(linha + 1, coluna + 1).ConteudoQuadrado == CONTEUDO.NUM)
                        MostrarQuadrado(linha + 1, coluna + 1, Program.M_mapa.GetQuadrado(linha + 1, coluna + 1));
                }

                if ((linha - 1 >= 0) && (coluna + 1 < numColunas))
                {
                    MostraQuadradosVaziosTodos(linha - 1, coluna + 1, Program.M_mapa.GetQuadrado(linha - 1, coluna + 1), numLinhas, numColunas);

                    if (Program.M_mapa.GetQuadrado(linha - 1, coluna + 1).ConteudoQuadrado == CONTEUDO.NUM)
                        MostrarQuadrado(linha - 1, coluna + 1, Program.M_mapa.GetQuadrado(linha - 1, coluna + 1));
                }

                if ((linha + 1 < numLinhas) && (coluna - 1 >= 0))
                {
                    MostraQuadradosVaziosTodos(linha + 1, coluna - 1, Program.M_mapa.GetQuadrado(linha + 1, coluna - 1), numLinhas, numColunas);

                    if (Program.M_mapa.GetQuadrado(linha + 1, coluna - 1).ConteudoQuadrado == CONTEUDO.NUM)
                        MostrarQuadrado(linha + 1, coluna - 1, Program.M_mapa.GetQuadrado(linha + 1, coluna - 1));
                }

                if ((linha - 1 >= 0) && (coluna - 1 >= 0))
                {
                    MostraQuadradosVaziosTodos(linha - 1, coluna - 1, Program.M_mapa.GetQuadrado(linha - 1, coluna - 1), numLinhas, numColunas);

                    if (Program.M_mapa.GetQuadrado(linha - 1, coluna - 1).ConteudoQuadrado == CONTEUDO.NUM)
                        MostrarQuadrado(linha - 1, coluna - 1, Program.M_mapa.GetQuadrado(linha - 1, coluna - 1));
                }

                if (coluna + 1 < numColunas)
                {
                    MostraQuadradosVaziosTodos(linha, coluna + 1, Program.M_mapa.GetQuadrado(linha, coluna + 1), numLinhas, numColunas);

                    if (Program.M_mapa.GetQuadrado(linha, coluna + 1).ConteudoQuadrado == CONTEUDO.NUM)
                        MostrarQuadrado(linha, coluna + 1, Program.M_mapa.GetQuadrado(linha, coluna + 1));
                }

                if (coluna - 1 >= 0)
                {
                    MostraQuadradosVaziosTodos(linha, coluna - 1, Program.M_mapa.GetQuadrado(linha, coluna - 1), numLinhas, numColunas);

                    if (Program.M_mapa.GetQuadrado(linha, coluna - 1).ConteudoQuadrado == CONTEUDO.NUM)
                        MostrarQuadrado(linha, coluna - 1, Program.M_mapa.GetQuadrado(linha, coluna - 1));
                }
            }
        }
        public string AdicionaFlag(Button b)
        {
            string nome = b.Name;

            string[] pos = nome.Split('-');
            int linha = Convert.ToInt32(pos[0]);
            int coluna = Convert.ToInt32(pos[1]);

            if (Program.M_mapa.CheckQuadradoSelecionado(linha, coluna))
            {
                //Função deveria estar no MAPA?
                string path = Program.M_mapa.getQuadradoPath(linha, coluna);

                //string path = @"\btnFlag.png";

                /*Passar isso a imagens mais tarde*/

                return path;
                //AlteraSimboloBotao(linha, coluna, path);
            }

            return "Vazio";
        }
        private async Task PerderJogoAsync()
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Explosion.wav"));
            mediaPlayer.Play();
            auxTimer = 0;
            timer1.Stop();
            setVariaveisFinais("-1", false);
            MostraTodasBombas();
            string[] posErradas = Program.M_mapa.GetBandeirasErradas(); //Colcoar um get no controller
            MostraBandeirasErradas(posErradas);
            PerderSad();
            BlockControls();
            //LimparForm();
            Sleep(3);
            await MessageBoxAsync("Perdeu o Jogo!");
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 1000 });
            this.Frame.Navigate(typeof(Menu));
            //Task.Run(async () => await Task.Delay(TimeSpan.FromMilliseconds(10000)));
        }
        private async Task GanharJogoAsync()
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Winning.wav"));
            mediaPlayer.Play();

            Program.M_jogador.Pontuacao = segundos;
            auxTimer = 0;
            timer1.Stop();
            MostraBandeirasTodas();
            setVariaveisFinais("00", false);
            GanharHappy();
            BlockControls();
            await MessageBoxAsync("Ganhou o Jogo!");
            //LimparForm();

            if (Program.M_menu.online)
            {
                //Enviar os dados para o server
                await EnviarDadosFimJogoAsync(true);

                ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 1000 });
                this.Frame.Navigate(typeof(Menu), null);
            }
            else
            {
                await CheckRecorde(segundos);
            }
        }
        public async Task EnviarDadosFimJogoAsync(bool vitoria)
        {
            int tempo = segundos;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/resultado/" + Program.M_jogador.Id);

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            XDocument xmlPedido = XDocument.Parse("<resultado_jogo><nivel></nivel><tempo></tempo><vitoria></vitoria></resultado_jogo>");

            //if (dificuldade == "facil")
            if (Program.M_mapa.NumLinhas == 9)
            {
                xmlPedido.Element("resultado_jogo").Element("nivel").Value = "facil";
            }
            else
            {
                xmlPedido.Element("resultado_jogo").Element("nivel").Value = "medio";
            }

            xmlPedido.Element("resultado_jogo").Element("tempo").Value = tempo.ToString();

            xmlPedido.Element("resultado_jogo").Element("vitoria").Value = vitoria.ToString();

            string mensagem = xmlPedido.Root.ToString();

            byte[] data = Encoding.Default.GetBytes(mensagem);
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = data.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            string resultado = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            XDocument xmlResposta = XDocument.Parse(resultado);

            if (xmlResposta.Element("resultado").Element("status").Value == "ERRO")
            {

                await MessageBoxAsync(xmlResposta.Element("resultado").Element("contexto").Value.ToString());

            }
        }
        private async Task MessageBoxAsync(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
        private async Task<string> getFilePathAsync()
        {
            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
            StorageFile file = await folder.GetFileAsync("pontuacao.xml");

            return file.Path;
        }
        private async Task CheckRecorde(int pontuacao)
        {

            try
            {
                string filepath = await getFilePathAsync();
                XDocument document = XDocument.Load(filepath);

                if (numColunas == 9)
                {
                    int recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);

                    if (pontuacao < recordeAnterior)
                    {
                        ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 398, Width = 288 });
                        this.Frame.Navigate(typeof(PedirNome));
                        
                    }
                    else
                    {
                        Sleep(3);
                        ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 700, Width = 1000 });
                        this.Frame.Navigate(typeof(Menu));
                    }

                }
                else if (numColunas == 16)
                {
                    int recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Medio").Element("Tempo").Value);

                    if (pontuacao < recordeAnterior)
                    {
                        ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 398, Width = 288 });
                        this.Frame.Navigate(typeof(PedirNome));
                    }
                    else
                    {
                        Sleep(3);
                        ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 700, Width = 1000 });
                        this.Frame.Navigate(typeof(Menu));
                    }
                }
                else
                {
                    Sleep(3);
                    ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 700, Width = 1000 });
                    this.Frame.Navigate(typeof(Menu));
                }
            }
            catch
            {
                ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 398, Width = 288 });
                this.Frame.Navigate(typeof(PedirNome));
            }
        }
        private void BlockControls()
        {
            auxBlockControls = 1;
            ButtonCara.Click -= ButtonCara_Click;

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    button[linha, coluna].Tapped -= new TappedEventHandler(MainPage_TappedAsync);
                    button[linha, coluna].RightTapped -= new RightTappedEventHandler(MainPage_RightTappedAsync);
                    button[linha, coluna].PointerPressed -= new PointerEventHandler(MainPage_PointerPressed);
                    button[linha, coluna].PointerReleased -= new PointerEventHandler(MainPage_PointerReleased);
                }
            }
        }
        private void Sleep(int sec)
        {
            TimeSpan delay = TimeSpan.FromSeconds(5);
            bool completed = false;
            ThreadPoolTimer DelayTimer = ThreadPoolTimer.CreateTimer(
                (source) =>
                {
                        //
                        Dispatcher.RunAsync(
                                                CoreDispatcherPriority.High,
                                                () =>
                                        {
                                                //
                                                // UI components can be accessed within this scope.
                                                //

                                            });

                    completed = true;
                },
                delay,
                (source) =>
                {

                    Dispatcher.RunAsync(
                                        CoreDispatcherPriority.High,
                                        () =>
                                        {
                                                //

                                                if (completed)
                                            {
                                                ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 700, Width = 1000 });
                                                this.Frame.Navigate(typeof(Menu));
                                            }
                                            else
                                            {
                                                    // Timer cancelled.
                                                }

                                        });
                });
        }
        public void AtualizaImagemConteudo()
        {
            string path = @"";

            for(int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    path = Program.M_mapa.getImagePath(Program.M_mapa.GetQuadrado(linha, coluna));
                    path = path.Remove(0, 8);

                    if (!Program.M_mapa.CheckQuadradoSelecionado(linha, coluna))
                    {
                        button[linha, coluna].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/" + path)), Stretch = Stretch.UniformToFill };
                        //button[linha, coluna].Background = new SolidColorBrush(Colors.DarkGray);
                    }
                }
            }
        }
        private async void MainPage_RightTappedAsync(object sender, RightTappedRoutedEventArgs e)
        {
            Button b = (Button)sender;
            string nome = b.Name;

            if (auxBlockControls == 1)
            {
                Window.Current.Content.RemoveHandler(RightTappedEvent, b);
            }

            string[] pos = nome.Split('-');
            int linha = Convert.ToInt32(pos[0]);
            int coluna = Convert.ToInt32(pos[1]);

            string path = AdicionaFlag(b);
            AtualizaSimboloBotao(linha, coluna, path);
            Program.M_mapa.AtualizaMinas(b.Name);
            AtualizaNumMinasMapa();
            TextBlockMinas.Text = numMinas.ToString();
            //await VerificarBandeirasAsync();
        }
        private async Task VerificarBandeirasAsync()
        {
            int count = 0;

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA && Program.M_mapa.GetQuadrado(linha, coluna).SimboloQuadrado == SIMBOLO.BANDEIRA)
                    {
                        count++;
                    }
                }
            }

            if (count == numMinas)
            {
                await GanharJogoAsync();
            }
        }
        private void AtualizaNumMinasMapa()
        {
            numMinas = Program.M_mapa.NumMinasPorEncontrar;
        }
        private void MainPage_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ButtonCara.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/smile.png")), Stretch = Stretch.None };
        }
        private void MainPage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if(auxTimer == 0)
            {
                timer1.Start();
                auxTimer++;
            }
            ButtonCara.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/boca.png")), Stretch = Stretch.None };
        }
        public void LimparForm()
        {
            //this.Hide();
            //Tornar feliz quando reinicia
            //string path = Environment.CurrentDirectory + @"/btns/smile.png";
            ButtonCara.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/smile.png")), Stretch = Stretch.None };
          
            timer1.Stop();

            TextBlockMinas.Text = "";
            TextBlockTimer.Text = "000";
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
        public void AtualizaSimboloBotao(int linha, int coluna, string path)
        {
            if (path == "Vazio")
            {
                //Voltar ao estado original em que estava. Impedir que botão direito mude novamento os já selecionados
                AtualizaImagemConteudo();
                //button[linha, coluna].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/btnVazio.png")), Stretch = Stretch.UniformToFill };
            }
            else if(path == null)
            {
                button[linha, coluna].Background = new SolidColorBrush(Colors.LightGray);
            }
            else
            {
                button[linha, coluna].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/" + path)), Stretch = Stretch.UniformToFill };
            }

        }
        public void MostraTodasBombas()
        {
            string path = @"/btnBomba.png";
            //MostraBombasTodas(button, numLinhas, numColunas);
            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA)
                    {
                        button[linha, coluna].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/" + path)), Stretch = Stretch.UniformToFill };
                    }
                }
            }
        }
        public void MostraBandeirasErradas(string[] PosErradas)
        {
            string path = @"/btnBombaErrada.png";
            for (int i = 0; i < PosErradas.Length; i++)
            {
                string[] pos = PosErradas[i].Split('-');

                button[Int32.Parse(pos[0]), Int32.Parse(pos[1])].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/" + path)), Stretch = Stretch.UniformToFill };
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

            //Melhorar código destas funções








            //timer1.Stop();
            auxTimer = 0;
            TextBlockMinas.Text = "000";
            segundos = 0;

            //Reiniciar o Jogo novamente
            if (numMinas != 0)
            {
                LimparForm();
                Program.M_mapa.CreateMapa(Program.M_mapa.NMinasTotais, numColunas, numLinhas);
            }

            if (Program.M_menu.online)
            {
                InicializarValoresMapaView(Program.M_mapa.NumLinhas, Program.M_mapa.NumColunas, Program.M_mapa.NMinasTotais);
                ReceberDadosOnline();
            }
            else
            { 
                InicializarValoresMapaView(numLinhas, numColunas, numMinas);
                CreateButtonModel(numLinhas, numColunas, numMinas); 
            }

            
            Resize(110 + 32 * numColunas, 32 * numLinhas);
            TextBlockMinas.Text = numMinas.ToString();
        }
        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private void ReceberDadosOnline()
        {
            //Prepara o pedido ao servidor com o URL adequado
            HttpWebRequest request = null;
            //Verificar qual o nível de jogo
            if (Program.M_mapa.NumColunas == 9)
            {
                request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/novo/Facil/" + Program.M_jogador.Id); // ou outro qualquer username
            }
            else if(Program.M_mapa.NumColunas == 16)
            {
                request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/novo/Medio/" + Program.M_jogador.Id); // ou outro qualquer username
            }


            
            // Com o acesso usa HTTPS e o servidor usar cerificados autoassinados, tempos de configurar o cliente para aceitar sempre o certificado.
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            
            request.Method = "GET"; // método usado para enviar o pedido

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // faz o envio do pedido
            
            Stream receiveStream = response.GetResponseStream(); // obtem o stream associado à resposta.
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8); // Canaliza o stream para um leitor de stream de nível superior com o

            string resultado = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
           
            XDocument xmlResposta = XDocument.Parse(resultado);
             // ...interpretar o resultado de acordo com a lógica da aplicação (exemplificativo)
            if (xmlResposta.Element("resultado").Element("status").Value == "ERRO")
            {


                //Mensagem de erro do carregar campo




                 // apresenta mensagem de erro usando o texto (contexto) da resposta
                //MessageBox.Show(
                //xmlResposta.Element("resultado").Element("contexto").Value,
                // "Erro",
                //MessageBoxButtons.OK,
                //MessageBoxIcon.Error
                //);
            }
            else
            {
                CONTEUDO[,] conteudoQuadrado = new CONTEUDO[Program.M_mapa.NumLinhas, Program.M_mapa.NumColunas];
                conteudoQuadrado = getConteudoQuadradoOnline(conteudoQuadrado, xmlResposta);
                CreateButtonModelOnline(Program.M_mapa.NumLinhas, Program.M_mapa.NumColunas, Program.M_mapa.NMinasTotais, conteudoQuadrado);
            }
        }
        private CONTEUDO[,] getConteudoQuadradoOnline(CONTEUDO[,] conteudoQuadrado, XDocument xmlResposta)
        {
            XElement posicao = xmlResposta.Element("resultado").Element("objeto").Element("campo");
            foreach (XElement xe in posicao.Descendants("posicao"))
            {
                if(Convert.ToInt32(xe.Value) == -1)
                {
                    int linha = Convert.ToInt32(xe.Attribute("linha").Value);
                    int coluna = Convert.ToInt32(xe.Attribute("coluna").Value);

                    conteudoQuadrado[linha, coluna] = CONTEUDO.BOMBA;
                }
            }

            return conteudoQuadrado;
        }
        public void CreateButtonModelOnline(int numLinhas, int numColunas, int numBombas, CONTEUDO[,] conteudoQuadrado)
        {
            int ButtonX = 0, ButtonY = 40;
            int[,] distanciaBomba = new int[numLinhas, numColunas];

            //preencherQuadrado(numLinhas, numColunas, numBombas, conteudoQuadrado);
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
        private void MostraBandeirasTodas()
        {
            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA)
                    {
                        string path = "btnFlag.png";
                        button[linha, coluna].Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(this.BaseUri, "Assets/" + path)), Stretch = Stretch.None };
                    }
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 800, Width = 1000 });
            this.Frame.Navigate(typeof(Menu), null);
        }
    } 
}