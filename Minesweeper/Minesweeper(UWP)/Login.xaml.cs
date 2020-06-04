using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Minesweeper_UWP_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private App Program = App.Current as App;
        FileOpenPicker picker = new FileOpenPicker();
        private string imagem;
        public Login()
        {
            this.InitializeComponent();
            //TextboxPassword. = '*';
            PropriedadesRegistar();
            picker = new FileOpenPicker();
            //mainGrid.Height = 412;
            //mainGrid.Width = 1000;
            
            ApplicationView.PreferredLaunchViewSize = new Size { Height = 520, Width = 1020 };
        }
        private void PropriedadesRegistar()
        {
            TBLogin_Email.Visibility = Visibility.Collapsed;
            TBLogin_Fotografia.Visibility = Visibility.Collapsed;
            TBLogin_Name.Visibility = Visibility.Collapsed;
            TBLogin_Pais.Visibility = Visibility.Collapsed;
            TBLogin_Password.Visibility = Visibility.Collapsed;
            TBLogin_Username.Visibility = Visibility.Collapsed;

            Login_Email.Visibility = Visibility.Collapsed;
            Login_Fotografia.Visibility = Visibility.Collapsed;
            Login_Name.Visibility = Visibility.Collapsed;
            Login_Pais.Visibility = Visibility.Collapsed;
            Login_Password.Visibility = Visibility.Collapsed;
            Login_Username.Visibility = Visibility.Collapsed;

            ButtonRegistar.Visibility = Visibility.Collapsed;
            ButtonInserirfoto.Visibility = Visibility.Collapsed;
        }
        private void ButtonRegistar_Click(object sender, RoutedEventArgs e)
        {
            //ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 790, Width = 1020 });
            //mainGrid.Height = 684;
            //mainGrid.Width = 1000;

            TBLogin_Email.Visibility = Visibility.Visible;
            TBLogin_Fotografia.Visibility = Visibility.Visible;
            TBLogin_Name.Visibility = Visibility.Visible;
            TBLogin_Pais.Visibility = Visibility.Visible;
            TBLogin_Password.Visibility = Visibility.Visible;
            TBLogin_Username.Visibility = Visibility.Visible;

            Login_Email.Visibility = Visibility.Visible;
            Login_Fotografia.Visibility = Visibility.Visible;
            Login_Name.Visibility = Visibility.Visible;
            Login_Pais.Visibility = Visibility.Visible;
            Login_Password.Visibility = Visibility.Visible;
            Login_Username.Visibility = Visibility.Visible;

            ButtonRegistar.Visibility = Visibility.Visible;
            ButtonInserirfoto.Visibility = Visibility.Visible;
        }
        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
         return true;
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            //Autenticar
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/Autentica");
            // Com o acesso usa HTTPS e o servidor usar cerificados autoassinados, temos de configurar o cliente para aceitar sempre o certificado.

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);


            // prepara os dados do pedido a partir de uma string só com a estrutura do XML (sem dados)
            XDocument xmlPedido = XDocument.Parse("<credenciais><username></username><password></password></credenciais>");
            //preenche os dados no XML
            xmlPedido.Element("credenciais").Element("username").Value = TextboxName.Text; // colocar aqui o username do utilizador
            xmlPedido.Element("credenciais").Element("password").Value = PasswordBox1.Password; // colocar aqui a palavra passe do utilizador

            string mensagem = xmlPedido.Root.ToString();

            byte[] data = Encoding.Default.GetBytes(mensagem); // note: choose appropriate encoding
            request.Method = "POST";// método usado para enviar o pedido
            request.ContentType = "application/xml"; // tipo de dados que é enviado com o pedido
            request.ContentLength = data.Length; // comprimento dos dados enviado no pedido

            Stream newStream = request.GetRequestStream(); // obtem a referência do stream associado ao pedido...
            newStream.Write(data, 0, data.Length);// ... que permite escrever os dados a ser enviados ao servidor
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // faz o envio do pedido

            Stream receiveStream = response.GetResponseStream(); // obtem o stream associado à resposta.
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8); // Canaliza o stream para um leitor de stream de nível superior com o formato de codificação necessário.

            string resultado = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            // converte para objeto XML para facilitar a extração da informação e ...
            XDocument xmlResposta = XDocument.Parse(resultado);
            // ...interpretar o resultado de acordo com a lógica da aplicação (exemplificativo)
            if (xmlResposta.Element("resultado").Element("status").Value == "ERRO")
            {
                // apresenta mensagem de erro usando o texto (contexto) da resposta
                MessageBoxAsync(xmlResposta.Element("resultado").Element("contexto").Value);
                
                //MessageBox.Show(xmlResposta.Element("resultado").Element("contexto").Value, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBoxAsync("Entrou");
                Program.M_menu.online = true;
                Program.M_jogador.Nome = TextboxName.Text;
                Program.M_jogador.Id = xmlResposta.Element("resultado").Element("objeto").Element("ID").Value;


                // assume a autenticação e obtem o ID do resultado...para ser usado noutros pedidos
                // xmlResposta.Element("resultado").Element("objeto").Element("id").Value
            }
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 900 });
            this.Frame.Navigate(typeof(Menu), null);
        }
        private async Task MessageBoxAsync(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
        private async void ButtonInserirfoto_ClickAsync(object sender, RoutedEventArgs e)
        {
            await PhotoPickerAsync();
        }
        private async Task PhotoPickerAsync()
        {
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {

                //Adicionar Picture box que mostra a foto
                TBLogin_Fotografia.Text = file.Path.ToString();

                //byte[] imageArray = File.ReadAllBytes(file.Path);
                //string base64Text = Convert.ToBase64String(imageArray); //base64Text must be global but I'll use  richtext
                //string fileEXT = file.Path;
                //imagem = "data:" + fileEXT + ";base64," + base64Text;

                //Inserir imagem na imagem
                ImageBox.Source = new BitmapImage(new Uri(file.Path));

            }
            else
            {
                TBLogin_Fotografia.Text = "Erro";
            }
        }
        private void ButtonRegistar_Click_1(object sender, RoutedEventArgs e)
        {
            //EnviarServidor()
            //Voltatr


            //Prepara o pedido ao servidor com o URL adequado
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/Registo");

            // Com o acesso usa HTTPS e o servidor usar cerificados autoassinados, temos de configurar o cliente para aceitar sempre o certificado.
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            // prepara os dados do pedido a partir de uma string só com a estrutura do XML (sem dados)
            XDocument xmlPedido = XDocument.Parse("<registo><nomeabreviado></nomeabreviado><username></username><password></password><email></email><fotografia></fotografia><pais></pais></registo>");
            //preenche os dados no XML

            // Nome Abreviado
            if (TBLogin_Name.Text == null)
            {
                MessageBoxAsync("Preencha todos os campos");
            }
            else
            {
                xmlPedido.Element("registo").Element("nomeabreviado").Value = TBLogin_Name.Text;
            }

            //Username
            if (TBLogin_Username.Text == null)
            {

            }
            else
            {
                xmlPedido.Element("registo").Element("username").Value = TBLogin_Username.Text; // colocar aqui o username do utilizador
            }


            //Password
            if (TBLogin_Password.Text == null)
            {

            }
            else
            {
                xmlPedido.Element("registo").Element("password").Value = TBLogin_Password.Text;
            }


            if (TBLogin_Email.Text == null)
            {

            }
            else
            {
                xmlPedido.Element("registo").Element("email").Value = TBLogin_Email.Text;// colocar aqui a palavra passe do utilizador
            }

            //Imagem
            if (imagem == null)
            {

            }
            else
            {
                xmlPedido.Element("registo").Element("fotografia").Value = imagem;
            }

            if (TBLogin_Pais.Text == null)
            {

            }
            else
            {
                xmlPedido.Element("registo").Element("pais").Value = TBLogin_Pais.Text;
            }


            string mensagem = xmlPedido.Root.ToString();

            byte[] data = Encoding.Default.GetBytes(mensagem); // note: choose appropriate encoding
            request.Method = "POST";// método usado para enviar o pedido
            request.ContentType = "application/xml"; // tipo de dados que é enviado com o pedido
            request.ContentLength = data.Length; // comprimento dos dados enviado no pedido

            Stream newStream = request.GetRequestStream(); // obtem a referência do stream associado ao pedido...
            newStream.Write(data, 0, data.Length);// ... que permite escrever os dados a ser enviados ao servidor
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); // faz o envio do pedido

            Stream receiveStream = response.GetResponseStream(); // obtem o stream associado à resposta.
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8); // Canaliza o stream para um leitor de stream de nível superior com oformato de codificação necessário.

            string resultado = readStream.ReadToEnd();
            response.Close();
            readStream.Close();

            // converte para objeto XML para facilitar a extração da informação e ...
            XDocument xmlResposta = XDocument.Parse(resultado);
            // ...interpretar o resultado de acordo com a lógica da aplicação (exemplificativo)
            if (xmlResposta.Element("resultado").Element("status").Value == "ERRO")
            {
                // apresenta mensagem de erro usando o texto (contexto) da resposta
                //MessageBox.Show(
                //xmlResposta.Element("resultado").Element("contexto").Value,
                //"Erro",
                //MessageBoxButtons.OK,
                //MessageBoxIcon.Error
                // );
            }
            else
            {
                //Program.V_Login.Size = new System.Drawing.Size(500, 100);
                

                

                //Fazer Resize do Login e clea das TextBoxes



                
                MessageBoxAsync("submeteu o seu registo com sucesso");
                // assume a autenticação e obtem o ID do resultado...para ser usado noutros pedidos
                // xmlResposta.Element("resultado").Element("objeto").Element("ID").Value }


                // Colocar Online
            }
        }
    }
}
