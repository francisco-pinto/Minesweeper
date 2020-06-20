using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Minesweeper_UWP_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConsultaPerfil : Page
    {
        private App Program = App.Current as App;
        public ConsultaPerfil()
        {
            this.InitializeComponent();
            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "Consulta de Perfil";

        }
        public async Task AcessoPerfilTop10Async(string Nome)
        {
            string nome = Nome;

            //Prepara o pedido ao servidor com o URL adequado
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/perfil/" + nome);

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
                //   MessageBox.Show(
                //       xmlResposta.Element("resultado").Element("contexto").Value,
                //        "Erro",
                //       MessageBoxButtons.OK,
                //       MessageBoxIcon.Error
                //);
            }
            else
            {
                string base64jogosganhos;
                string base64jogosperdidos;
                string base64tempofacil;
                string base64tempomedio;

                // obtem todos os elementos do perfil do jogador...
                // ...como, por exemplo, a fotografia:
                //NOMEABREVIADO
                string base64NomeAbreviado = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("nomeabreviado").Value;
                TextBoxNome.Text = base64NomeAbreviado;
                //EMAIL
                string base64Email = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("email").Value;
                TextBoxEmail.Text = base64Email;
                //PAIS
                string base64Pais = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("pais").Value;
                TextBoxPais.Text = base64Pais;

                //JOGOS GANHOS
                if ((base64jogosganhos = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("jogos").Element("ganhos").Value) == null)
                {
                    TextBoxJogosGanhos.Text = "0";
                }
                else
                    TextBoxJogosGanhos.Text = base64jogosganhos;
                //JOGOS PERDIDOS
                if ((base64jogosperdidos = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("jogos").Element("perdidos").Value) == null)
                {
                    TextBoxJogosPerdidos.Text = "0";
                }
                else
                    TextBoxJogosPerdidos.Text = base64jogosperdidos;
                //TEMPOS FACIL

                try
                {
                    base64tempofacil = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("tempos").Element("facil").Value;

                    if (base64tempofacil != null)
                    {
                        TextBoxTempoFacil.Text = base64tempofacil;
                    }
                }
                catch { TextBoxTempoFacil.Text = "0"; }

                //TEMPOS MEDIO

                try
                {
                    base64tempomedio = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("tempos").Element("medio").Value;

                    if (base64tempomedio != null)
                    {
                        TextBoxTempoMedio.Text = base64tempomedio;
                    }
                }
                catch
                {
                    TextBoxTempoMedio.Text = "0";
                }

                try
                {
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
                catch
                {

                }
            }
        }
        public async Task AcessoPerfilAsync()
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
            // ...interpretar o resultado de acordo com a lógica da aplicação (exemplificativo)
            if (xmlResposta.Element("resultado").Element("status").Value == "ERRO")
            {
                // apresenta mensagem de erro usando o texto (contexto) da resposta
                //   MessageBox.Show(
                //       xmlResposta.Element("resultado").Element("contexto").Value,
                //        "Erro",
                //       MessageBoxButtons.OK,
                //       MessageBoxIcon.Error
                //);

                /*await */
                await MessageBoxAsync("Erro na consulta dos dados do jogdor");

            }
            else
            {
                string base64jogosganhos;
                string base64jogosperdidos;
                string base64tempofacil;
                string base64tempomedio;

                // obtem todos os elementos do perfil do jogador...
                // ...como, por exemplo, a fotografia:
                //NOMEABREVIADO
                string base64NomeAbreviado = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("nomeabreviado").Value;
                TextBoxNome.Text = base64NomeAbreviado;
                //EMAIL
                string base64Email = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("email").Value;
                TextBoxEmail.Text = base64Email;
                //PAIS
                string base64Pais = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("pais").Value;
                TextBoxPais.Text = base64Pais;

                //JOGOS GANHOS
                if ((base64jogosganhos = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("jogos").Element("ganhos").Value) == null)
                {
                    TextBoxJogosGanhos.Text = "0";
                }
                else
                    TextBoxJogosGanhos.Text = base64jogosganhos;
                //JOGOS PERDIDOS
                if ((base64jogosperdidos = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("jogos").Element("perdidos").Value) == null)
                {
                    TextBoxJogosPerdidos.Text = "0";
                }
                else
                    TextBoxJogosPerdidos.Text = base64jogosperdidos;
                //TEMPOS FACIL

                //Erro devido à não existência de elementos 
                try
                {
                    if ((base64tempofacil = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("tempos").Element("facil").Value) != null)
                        TextBoxTempoFacil.Text = base64tempofacil;
                }
                catch
                {
                    TextBoxTempoFacil.Text = "0";
                }
                //base64tempofacil;
                //TEMPOS MEDIO
                try
                {
                    if ((base64tempomedio = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("tempos").Element("medio").Value) != null)
                    {
                        TextBoxTempoMedio.Text = base64tempomedio;
                    }
                }
                catch
                {
                    TextBoxTempoMedio.Text = "0";
                }
                
                //FOTO
                try
                {
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
                catch
                {
                    //Não tem foto
                }
            }
        }
        private async Task MessageBoxAsync(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((string)e.Parameter != null)
            {
                await AcessoPerfilTop10Async((string)e.Parameter);
            }
            else
            {
                await AcessoPerfilAsync();
            }
        }
        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Menu));
        }

    }
}
