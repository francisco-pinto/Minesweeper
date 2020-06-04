using Minesweeper;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Size = Windows.Foundation.Size;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Minesweeper_UWP_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class Menu : Page
    {
        public event startGame play;
        private App Program = App.Current as App;
        public Menu()
        {
            this.InitializeComponent();
            
        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public async Task ShowFotoJogadorAsync()
        {
            //Condição para impedir 
            if (Program.M_jogador.Nome != null)
            {
                //Prepara o pedido ao servidor com o URL adequado
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/perfil/" + Program.M_jogador.Nome);

                // Com o acesso usa HTTPS e o servidor usar cerificados autoassinados, tempos de configurar o cliente para aceitar sempre o certificado.
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

                request.Method = "GET"; // método usado para enviar o pedido

                HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // faz o envio do pedido

                Stream receiveStream = response.GetResponseStream(); // obtem o stream associado à resposta.
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8); // Canaliza o stream para um leitor de stream de nível superior com o
                                                                                          //formato de codificação necessário.
                string resultado = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                // converte para objeto XML para facilitar a extração da informação e ...
                XDocument xmlResposta = XDocument.Parse(resultado);

                string base64Imagem = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("fotografia").Value;
                string base64 = base64Imagem.Split(',')[1]; // retira a parte da string correspondente aos bytes da imagem..
                byte[] bytes = Convert.FromBase64String(base64); //...converte para array de bytes...
                BitmapImage image = new BitmapImage();//... e, por fim, para Image

                var stream = new InMemoryRandomAccessStream();
                await stream.WriteAsync(bytes.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
                // pode mostrar a imagem num qualquer componente...como por exemplo:

                //BitmapImage offline = new BitmapImage(new Uri("ms-appx:///Assets/Offline.png"));
                ImageJogador.Source = image;
            }
            
        }
        private void ButtonInstrucoes_Click(object sender, RoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 480, Width = 535 });
            this.Frame.Navigate(typeof(Instrucoes), null);
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    Program.C_Intrucoes = new ControllerInstrucoes();
        //    Program.C_jogador = new ControllerJogador();
        //    Program.C_Login = new ControllerLogin();
        //    Program.C_mapa = new ControllerMapa();
        //    Program.C_menu = new ControllerMenu();
        //}

        private void TextBoxNumBombas_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void TextBoxNumLinhas_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void TextBoxNumColunas_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        private void ButtonJogar_Click(object sender, RoutedEventArgs e)
        {  
            if (RadioButtonFacil.IsChecked == true)
            {
                TextBoxNumBombas.Visibility = Visibility.Collapsed;
                TextBoxNumLinhas.Visibility = Visibility.Collapsed;
                TextBoxNumColunas.Visibility = Visibility.Collapsed;


                ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 110 + 32 * 9, Width = 32 * 9 });
                Program.C_menu.V_Menu_play(9, 9, 10);
                this.Frame.Navigate(typeof(MainPage));
                
                /*Adicionar ao eventpo de modo a chamar
                 a pág já inicializada no app.xaml.cs
                 usando o Program*/
                //this.Frame.Navigate(typeof(MainPage));
            }
            else if(RadioButtonMedio.IsChecked == true)
            {
                TextBoxNumBombas.Visibility = Visibility.Collapsed;
                TextBoxNumLinhas.Visibility = Visibility.Collapsed;
                TextBoxNumColunas.Visibility = Visibility.Collapsed;

                ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 110 + 32 * 16, Width = 32 * 16 });
                Program.C_menu.V_Menu_play(16, 16, 40);
                this.Frame.Navigate(typeof(MainPage));
            }else if(RadioButtonCustom.IsChecked == true)
            {
                int numLinhas = 0; Int32.TryParse(TextBoxNumLinhas.Text, out numLinhas);
                int numColunas = 0; Int32.TryParse(TextBoxNumColunas.Text, out numColunas);
                int numBombas = 0; Int32.TryParse(TextBoxNumBombas.Text, out numBombas);

                if (numLinhas < 9)
                {
                    numLinhas = 9;
                }
                if (numColunas < 9)
                {
                    numColunas = 9;
                }
                if (numLinhas > 15)
                {
                    numLinhas = 15;
                }
                if (numColunas > 22)
                {
                    numColunas = 22;
                }

                int numMaxBombas = (numLinhas * numColunas) - (numLinhas + numColunas) + 1;
                int numMinBombas = 10;

                if (numBombas > numMaxBombas)
                {
                    ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 32 * numLinhas, Width = 32 * numColunas });
                    Program.C_menu.V_Menu_play(numLinhas, numColunas, numMaxBombas);
                    this.Frame.Navigate(typeof(MainPage));
                }
                else if (numBombas < numMinBombas)
                {

                    ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 32 * numLinhas, Width = 32 * numColunas });
                    Program.C_menu.V_Menu_play(numLinhas, numColunas, numMinBombas);
                    this.Frame.Navigate(typeof(MainPage));
                }
                else
                {
                    ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 32 * numLinhas, Width = 32 * numColunas });
                    Program.C_menu.V_Menu_play(numLinhas, numColunas, numBombas);
                    this.Frame.Navigate(typeof(MainPage));
                }
            }
        }
        private void RadioButtonCustom_Checked(object sender, RoutedEventArgs e)
        {            
            TextBoxNumBombas.Visibility = Visibility.Visible;
            TextBoxNumLinhas.Visibility = Visibility.Visible;
            TextBoxNumColunas.Visibility = Visibility.Visible;

            TextBlockNumLinhas.Visibility = Visibility.Visible;
            TextBlockNumColunas.Visibility = Visibility.Visible;
            TextBlockNumBombas.Visibility = Visibility.Visible;
        }
        private void TextBoxNumBombas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxNumBombas_GotFocus;
        }
        private void TextBoxNumColunas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxNumColunas_GotFocus;
        }
        private void TextBoxNumLinhas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxNumLinhas_GotFocus;
        }
        private void RadioButtonMedio_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxNumBombas.Visibility = Visibility.Collapsed;
            TextBoxNumLinhas.Visibility = Visibility.Collapsed;
            TextBoxNumColunas.Visibility = Visibility.Collapsed;

            TextBlockNumLinhas.Visibility = Visibility.Collapsed;
            TextBlockNumColunas.Visibility = Visibility.Collapsed;
            TextBlockNumBombas.Visibility = Visibility.Collapsed;

            TextBoxNumLinhas.Text = "";
            TextBoxNumColunas.Text = "";
            TextBoxNumBombas.Text = "";
        }
        private void RadioButtonFacil_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxNumBombas.Visibility = Visibility.Collapsed;
            TextBoxNumLinhas.Visibility = Visibility.Collapsed;
            TextBoxNumColunas.Visibility = Visibility.Collapsed;

            TextBlockNumLinhas.Visibility = Visibility.Collapsed;
            TextBlockNumColunas.Visibility = Visibility.Collapsed;
            TextBlockNumBombas.Visibility = Visibility.Collapsed;

            TextBoxNumLinhas.Text = "";
            TextBoxNumColunas.Text = "";
            TextBoxNumBombas.Text = "";
        }
        private void ButtonConsulta_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ConsultaPerfil));
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ListViewFacil.Items.Clear();
            ListViewMedio.Items.Clear();

            TextBoxNumBombas.Visibility = Visibility.Collapsed;
            TextBoxNumLinhas.Visibility = Visibility.Collapsed;
            TextBoxNumColunas.Visibility = Visibility.Collapsed;

            TextBoxNumLinhas.Text = "";
            TextBoxNumColunas.Text = "";
            TextBoxNumBombas.Text = "";

            TextBlockNumLinhas.Visibility = Visibility.Collapsed;
            TextBlockNumColunas.Visibility = Visibility.Collapsed;
            TextBlockNumBombas.Visibility = Visibility.Collapsed;


            if (Program.M_menu.online)
            {
                RadioButtonMedio.Margin = new Thickness(RadioButtonMedio.Margin.Left + 100, RadioButtonMedio.Margin.Top, RadioButtonMedio.Margin.Right, RadioButtonMedio.Margin.Bottom);
                RadioButtonCustom.Visibility = Visibility.Collapsed;
                ImageJogador.Visibility = Visibility.Visible;
                ButtonConsulta.Visibility = Visibility.Visible;
                ImageJogador.Visibility = Visibility.Visible;
                BitmapImage online = new BitmapImage(new Uri("ms-appx:///Assets/online.png"));
                ImageOnOff.Source = online;
                await ShowFotoJogadorAsync();
                await ShowTop10Async();
            }
            else
            {
                ImageJogador.Visibility = Visibility.Collapsed;
                ButtonConsulta.Visibility = Visibility.Collapsed;
                ImageJogador.Visibility = Visibility.Collapsed;
                BitmapImage offline = new BitmapImage(new Uri("ms-appx:///Assets/Offline.png"));
                ImageOnOff.Source = offline;
                ShowRecordeAsync();
            }
        }
        private async void ShowRecordeAsync()
        {
            try
            {
                StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
                StorageFile file = await folder.GetFileAsync("pontuacao.xml");
                XDocument document;

                using (Stream fileStream = await file.OpenStreamForWriteAsync())
                {
                    document = XDocument.Load(fileStream);
                }

                await MessageBoxAsync(document.Element("pontuacoes").Element("Facil").Element("Nome").Value + "  " + document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);

                //listBoxFacil.Items.Add(document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);
                ListViewFacil.Items.Add(document.Element("pontuacoes").Element("Facil").Element("Nome").Value + "  " + document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);
                ListViewMedio.Items.Add(document.Element("pontuacoes").Element("Medio").Element("Nome").Value + "  " + document.Element("pontuacoes").Element("Medio").Element("Tempo").Value);
            }
            catch
            {
                ListViewFacil.Items.Clear();
                ListViewMedio.Items.Clear();
                await CriarDocumentoXMLAsync();
            }
        }
        private async Task ShowTop10Async()
        {
            //Prepara o pedido ao servidor com o URL adequado
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/top10");

            // Com o acesso usa HTTPS e o servidor usar cerificados autoassinados, tempos de configurar o cliente para aceitar sempre o certificado.
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            request.Method = "GET"; // método usado para enviar o pedido

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // faz o envio do pedido

            Stream receiveStream = response.GetResponseStream(); // obtem o stream associado à resposta.
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8); // Canaliza o stream para um leitor de stream de nível superior com o
                                                                                      //formato de codificação necessário.
            string resultado = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            // converte para objeto XML para facilitar a extração da informação e ...
            XDocument xmlResposta = XDocument.Parse(resultado);
            // ...interpretar o resultado de acordo com a lógica da aplicação (exemplificativo)
            if (xmlResposta.Element("resultado").Element("status").Value == "ERRO")
            {
                // apresenta mensagem de erro usando o texto (contexto) da resposta
                await MessageBoxAsync(xmlResposta.Element("resultado").Element("contexto").Value);
                //MessageBox.Show(
                //    xmlResposta.Element("resultado").Element("contexto").Value,
                //     "Erro",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Error
             
            }
            else
            {
                StringBuilder dificuldade = new StringBuilder();
                StringBuilder username = new StringBuilder();
                StringBuilder tempo = new StringBuilder();
                StringBuilder quando = new StringBuilder();
                foreach (XElement level1Element in xmlResposta.Element("resultado").Element("objeto").Element("top").Elements("nivel"))
                {
                    dificuldade.AppendLine(level1Element.FirstAttribute.Value);

                    foreach (XElement level2Element in level1Element.Elements("jogador"))
                    {
                        username.AppendLine(level2Element.Attribute("username").Value);
                        tempo.AppendLine(level2Element.Attribute("tempo").Value);
                        quando.AppendLine(level2Element.Attribute("quando").Value);

                        //MessageBox.Show(dificuldade.ToString());
                        string nova = dificuldade.ToString().Remove(5, 2);
                        if (nova == "Facil")
                        {
                            ListViewFacil.Items.Add(username.ToString() + tempo.ToString());
                            //ListViewFacil.Items.Add(quando);

                        }
                        else
                        {
                            ListViewMedio.Items.Add(username.ToString() + tempo.ToString());
                            //ListViewMedio.Items.Add(quando);

                        }

                        username.Clear();
                        tempo.Clear();
                        quando.Clear();
                    }
                }
            }
        }
        private async Task CriarDocumentoXMLAsync()
        {
            StorageFolder folder=null;
            StorageFile file = null;
            IStorageItem item = null;

            if ((item = await ApplicationData.Current.LocalFolder.TryGetItemAsync("Save")) == null)
            {
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("Save");
                folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
                file = await folder.CreateFileAsync("pontuacao.xml");
                //stream = await folder.CreateFileAsync("pontuacao.xml");
                //Directory.CreateDirectory(Environment.CurrentDirectory + @"\Save");
            }
            else
            {
                folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
                file = await folder.GetFileAsync("pontuacao.xml");
            }

            XDocument doc;

            {
                doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                                new XComment("Recorde em facil e medio"),
                                new XElement("pontuacoes",
                                    new XElement("Facil",
                                        new XElement("Nome"),
                                        new XElement("Tempo")
                                    ),
                                    new XElement("Medio",
                                        new XElement("Nome"),
                                        new XElement("Tempo")
                                        )
                                )
                            );
            }

            //var randomAccessStream = await file.OpenStreamForWriteAsync();
            //Stream stream = randomAccessStream.AsInputStream();

            using (Stream fileStream = await file.OpenStreamForWriteAsync())
            {
                doc.Save(fileStream);
            }

            //doc.Save();
        }
        private async Task MessageBoxAsync(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}
