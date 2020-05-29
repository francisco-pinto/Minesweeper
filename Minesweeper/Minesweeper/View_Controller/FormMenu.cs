using System;
using Minesweeper.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Minesweeper.Models.Menu; //diferenciar o menu classe do form.menu
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace Minesweeper.View_Controller
{
    public delegate void VerPerfil();
    public partial class FormMenu : Form
    {
        public event startGame play;
        public event VerPerfil ConsultarPerfil;

        public bool online = false;

        private TextBox TBnumLinhas = new TextBox();
        private TextBox TBnumColunas = new TextBox();
        private TextBox TBnumBombas = new TextBox();
        public FormMenu()
        {
            InitializeComponent();
            ConfigRadioButtons();

        }

        private void buttonJogar_Click(object sender, EventArgs e)
        {
            int nLinhasCustom, nColunasCustom, nBombasCustom;


            if (TBnumBombas.Visible)
            {
                if (!((Int32.TryParse(TBnumLinhas.Text, out nLinhasCustom)) && (Int32.TryParse(TBnumColunas.Text, out nColunasCustom) && (Int32.TryParse(TBnumBombas.Text, out nBombasCustom)))))
                {
                    MessageBox.Show("Insira valores corretos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {

                    if (nLinhasCustom < 9)
                    {
                        nLinhasCustom = 9;
                    }
                    if (nColunasCustom < 9)
                    {
                        nColunasCustom = 9;
                    }
                    if (nLinhasCustom > 20)
                    {
                        nLinhasCustom = 20;
                    }
                    if (nColunasCustom > 22)
                    {
                        nColunasCustom = 22;
                    }

                    int numMaxBombas = (nLinhasCustom * nColunasCustom) - (nLinhasCustom + nColunasCustom) + 1;
                    int numMinBombas = 10;


                    if (nBombasCustom > numMaxBombas)
                    {
                        this.Hide();
                        play(nLinhasCustom, nColunasCustom, numMaxBombas);
                    }
                    else if (nBombasCustom < numMinBombas)
                    {
                        this.Hide();
                        play(nLinhasCustom, nColunasCustom, numMinBombas);
                    }
                    else
                    {
                        this.Hide();
                        play(nLinhasCustom, nColunasCustom, nBombasCustom);
                    }
                }
            }
            else if (radioButtonFacil.Checked)
            {
                this.Hide();
                if (play != null)
                {
                    play(9, 9, 10);
                }
            }
            else if (radioButtonMedia.Checked)
            {
                this.Hide();
                play(16, 16, 40);
            }
            else
            {
                MessageBox.Show("Escolha uma das dificuldades", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonInstrucoes_Click(object sender, EventArgs e)
        {
            Program.V_Instrucoes.ShowDialog();
        }
        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
        private void radioButtonCustom_Click(object sender, EventArgs e)
        {
            TBnumColunas.Visible = true;
            TBnumLinhas.Visible = true;
            TBnumBombas.Visible = true;
        }
        private void radioButtonMedia_Click(object sender, EventArgs e)
        {
            TBnumColunas.Visible = false;
            TBnumLinhas.Visible = false;
            TBnumBombas.Visible = false;
            TBnumLinhas.Text = "NumLinhas";
            TBnumColunas.Text = "NumColunas";
            TBnumBombas.Text = "NumBombas";
        }
        private void radioButtonFacil_Click(object sender, EventArgs e)
        {
            TBnumColunas.Visible = false;
            TBnumLinhas.Visible = false;
            TBnumBombas.Visible = false;

            TBnumLinhas.Text = "NumLinhas";
            TBnumColunas.Text = "NumColunas";
            TBnumBombas.Text = "NumBombas";
        }
        private void ConfigRadioButtons()
        {
            TBnumLinhas.Size = new System.Drawing.Size(70, 20);
            TBnumLinhas.Text = "NumLinhas";
            TBnumLinhas.Location = new System.Drawing.Point(351, 220);
            TBnumLinhas.Click += TB_Click;
            this.Controls.Add(TBnumLinhas);

            TBnumColunas.Size = new System.Drawing.Size(70, 20);
            TBnumColunas.Text = "NumColunas";
            TBnumColunas.Location = new System.Drawing.Point(351, 250);
            TBnumColunas.Click += TB_Click;
            this.Controls.Add(TBnumColunas);

            TBnumBombas.Size = new System.Drawing.Size(70, 20);
            TBnumBombas.Text = "NumBombas";
            TBnumBombas.Location = new System.Drawing.Point(351, 280);
            TBnumBombas.Click += TB_Click;
            this.Controls.Add(TBnumBombas);
        }
        private void TB_Click(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
        }

        private void buttonConsultarPerfil_Click(object sender, EventArgs e)
        {

            ConsultarPerfil();
            this.Hide();
            Program.V_ConsultarPerfil.Show();

        }

        public void AlteraImagem()
        {
            pictureBoxOnline.Image = Image.FromFile(Environment.CurrentDirectory + @"/Botoes/online.png");


        }
        public void ShowConsultaPerfil()
        {
            buttonConsultarPerfil.Visible = true;
        }
        public void ShowFoto()
        {
            //Prepara o pedido ao servidor com o URL adequado
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/perfil/" + "xcoelho");

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
            Image image = Image.FromStream(new MemoryStream(bytes));//... e, por fim, para Image

            // pode mostrar a imagem num qualquer componente...como por exemplo:
            pictureBoxFotoMenu.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxFotoMenu.BackgroundImage = image;
        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            if (online)
            {
                ShowTop10();
            }
        }

        private void ShowTop10()
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
                MessageBox.Show(
                    xmlResposta.Element("resultado").Element("contexto").Value,
                     "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
             );
            }
            else
            {
                StringBuilder result = new StringBuilder();
                foreach (XElement level1Element in xmlResposta.Element("resultado").Element("objeto").Element("top").Elements("nivel"))
                    {
                    result.AppendLine(level1Element.Attribute("dificudade").Value);
                    
                    foreach (XElement level2Element in level1Element.Elements("jogador"))
                    {
                        result.AppendLine(level2Element.Attribute("username").Value);
                        result.AppendLine(level2Element.Attribute("tempo").Value);
                        result.AppendLine(level2Element.Attribute("quando").Value);
                    }

                    //if dificuldade facil
                    //    listBoxFacil.Items.Add();
                    //else
                    //    listBoxMedio.Items.Add();
                }
            }
        }
    }
}
