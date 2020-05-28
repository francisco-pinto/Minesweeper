using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Minesweeper.View_Controller
{
    public partial class FormConsultarPerfil : Form
    {
        public void AcessoPerfil()
        {
            //Prepara o pedido ao servidor com o URL adequado
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/perfil/"+"xcoelho");

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
                MessageBox.Show(
                    xmlResposta.Element("resultado").Element("contexto").Value,
                     "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
             );
            }
            else
            {
                // obtem todos os elementos do perfil do jogador...
                // ...como, por exemplo, a fotografia:

                string base64NomeAbreviado = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("nomeabreviado").Value;
                textBoxNomeAbreviado.Text = base64NomeAbreviado;
                string base64Email = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("email").Value;
                textBoxEmail.Text = base64Email;
                string base64Pais = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("pais").Value;
                textBoxPais.Text = base64Pais;
                string base64Imagem = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("fotografia").Value;
                string base64 = base64Imagem.Split(',')[1]; // retira a parte da string correspondente aos bytes da imagem..
                byte[] bytes = Convert.FromBase64String(base64); //...converte para array de bytes...
                Image image = Image.FromStream(new MemoryStream(bytes));//... e, por fim, para Image

                // pode mostrar a imagem num qualquer componente...como por exemplo:
                pictureBoxFoto.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBoxFoto.BackgroundImage = image;
            }
        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

        public FormConsultarPerfil()
        {
            InitializeComponent();
            


        }

        OpenFileDialog ofd = new OpenFileDialog();
        
        private void buttonVoltarMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FormConsultarPerfil_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.V_Menu.Show();
        }
    }
}
