﻿using System;
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
using System.Net.Security;

namespace Minesweeper.View_Controller
{
    public delegate void VerPerfil();
    
    public partial class FormMenu : Form
    {
        public event startGame play;
        public event startGameOnline playOnline;
        public event VerPerfil ConsultarPerfil;
        public event GetNome getNomeJogador;
        public event ConsultarTop10 verPerfilTop10;

        public bool online = false;

        private TextBox TBnumLinhas = new TextBox();
        private TextBox TBnumColunas = new TextBox();
        private TextBox TBnumBombas = new TextBox();
        public FormMenu()
        {
            InitializeComponent();
            ConfigRadioButtons();
        }
        public void RestartOnlineGame()
        {
            buttonJogar_Click(null, null);
        }
        private void buttonJogar_Click(object sender, EventArgs e)
        {
            int nLinhasCustom = 0;
            int nColunasCustom = 0;
            int nBombasCustom = 0;

            if (online)
            {
                HttpWebRequest request = null;

                //Verificar qual o nível de jogo
                if (radioButtonFacil.Checked)
                {
                    nLinhasCustom = 9;
                    nColunasCustom = 9;
                    nBombasCustom = 10;

                    request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/novo/Facil/" + Program.M_jogador.Id); // ou outro qualquer username
                }
                else if (radioButtonMedia.Checked)
                {
                    nLinhasCustom = 16;
                    nColunasCustom = 16;
                    nBombasCustom = 40;

                    request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/novo/Medio/" + Program.M_jogador.Id); // ou outro qualquer username
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);

                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse(); 

                Stream receiveStream = response.GetResponseStream(); 
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string resultado = readStream.ReadToEnd();
                response.Close();
                readStream.Close();

                XDocument xmlResposta = XDocument.Parse(resultado);
                
                if (xmlResposta.Element("resultado").Element("status").Value == "ERRO")
                {
                    MessageBox.Show(
                    xmlResposta.Element("resultado").Element("contexto").Value,
                     "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                }
                else
                {
                    if (playOnline != null)
                    {
                        playOnline(xmlResposta, nLinhasCustom, nColunasCustom, nBombasCustom);
                    }
                }
            }
            else
            {
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
                            TBnumColunas.Text = "Num Colunas";
                            TBnumColunas.Visible = false;
                            TBnumLinhas.Text = "Num Linhas";
                            TBnumLinhas.Visible = false;
                            TBnumBombas.Text = "Num Bombas";
                            TBnumBombas.Visible = false;
                            play(nLinhasCustom, nColunasCustom, numMaxBombas);
                        }
                        else if (nBombasCustom < numMinBombas)
                        {
                            this.Hide();
                            TBnumColunas.Text = "Num Colunas";
                            TBnumColunas.Visible = false;
                            TBnumLinhas.Text = "Num Linhas";
                            TBnumLinhas.Visible = false;
                            TBnumBombas.Text = "Num Bombas";
                            TBnumBombas.Visible = false;
                            play(nLinhasCustom, nColunasCustom, numMinBombas);
                        }
                        else
                        {
                            this.Hide();
                            TBnumColunas.Text = "Num Colunas";
                            TBnumColunas.Visible = false;
                            TBnumLinhas.Text = "Num Linhas";
                            TBnumLinhas.Visible = false;
                            TBnumBombas.Text = "Num Bombas";
                            TBnumBombas.Visible = false;
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
            TBnumLinhas.Size = new Size(70, 20);
            TBnumLinhas.Text = "NumLinhas";
            TBnumLinhas.Location = new Point(351, 220);
            TBnumLinhas.Click += TB_Click;
            this.Controls.Add(TBnumLinhas);

            TBnumColunas.Size = new Size(70, 20);
            TBnumColunas.Text = "NumColunas";
            TBnumColunas.Location = new Point(351, 250);
            TBnumColunas.Click += TB_Click;
            this.Controls.Add(TBnumColunas);

            TBnumBombas.Size = new Size(70, 20);
            TBnumBombas.Text = "NumBombas";
            TBnumBombas.Location = new Point(351, 280);
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
            string nome = getNomeJogador();
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/perfil/" + nome);

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); 
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8); 
            
            string resultado = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            XDocument xmlResposta = XDocument.Parse(resultado);

            string base64Imagem;
            string base64;
            try
            {
                base64Imagem = xmlResposta.Element("resultado").Element("objeto").Element("perfil").Element("fotografia").Value;
                base64 = base64Imagem.Split(',')[1]; // retira a parte da string correspondente aos bytes da imagem..
                byte[] bytes = Convert.FromBase64String(base64); //...converte para array de bytes...
                Image image = Image.FromStream(new MemoryStream(bytes));//... e, por fim, para Image

                pictureBoxFotoMenu.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBoxFotoMenu.BackgroundImage = image;
            }
            catch
            {
                //Erro imagem não existe
            }
            
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
                radioButtonCustom.Visible = false;
                radioButtonMedia.Location = new Point(253, 19);
                
            } else
            {
                ShowRecorde();
            }

            
        }
        public void AtualizaValoresRecorde()
        {
            listBoxFacil.Items.Clear();
            listBoxMedio.Items.Clear();
            if (online)
            {
                ShowTop10();
            }
            else
            {
                ShowRecorde();
            }
        }
        private void ShowRecorde()
        {
            try
            {
                XDocument document = XDocument.Load(Environment.CurrentDirectory + @"\Save\pontuacao.xml");

                //listBoxFacil.Items.Add(document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);
                listBoxFacil.Items.Add(document.Element("pontuacoes").Element("Facil").Element("Nome").Value + "  " + document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);
                listBoxMedio.Items.Add(document.Element("pontuacoes").Element("Medio").Element("Nome").Value + "  " + document.Element("pontuacoes").Element("Medio").Element("Tempo").Value);
            }
            catch
            {
                listBoxFacil.Items.Clear();
                listBoxMedio.Items.Clear();
                CriarDocumentoXML();
            }
            

        }
        private void CriarDocumentoXML()
        {

            if (!Directory.Exists(Environment.CurrentDirectory + @"\Save"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Save");
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
            doc.Save(Environment.CurrentDirectory + @"\Save\pontuacao.xml");
        }
        public void ShowTop10()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/top10");

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            request.Method = "GET"; 

            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); 

            Stream receiveStream = response.GetResponseStream(); 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8); 
            
            string resultado = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            
            XDocument xmlResposta = XDocument.Parse(resultado);
            
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
                listBoxFacil.Items.Clear();
                listBoxMedio.Items.Clear();
                StringBuilder dificuldade = new StringBuilder();
                StringBuilder username = new StringBuilder();
                StringBuilder tempo = new StringBuilder();
                StringBuilder quando = new StringBuilder();
                foreach (XElement level1Element in xmlResposta.Element("resultado").Element("objeto").Element("top").Elements("nivel"))
                    {
                    dificuldade.Clear();
                    dificuldade.AppendLine(level1Element.FirstAttribute.Value);
                    
                    foreach (XElement level2Element in level1Element.Elements("jogador"))
                    {
                        username.AppendLine(level2Element.Attribute("username").Value);
                        tempo.AppendLine(level2Element.Attribute("tempo").Value);
                        quando.AppendLine(level2Element.Attribute("quando").Value);

                        //MessageBox.Show(dificuldade.ToString());
                        string nova = dificuldade.ToString().Remove(5,2);
                        if (nova =="Facil")
                        {
                            listBoxFacil.Items.Add(username + "-" + tempo);
                           // listBoxFacil.Items.Add(tempo);
                           // listBoxFacil.Items.Add(quando);

                        } else
                        {
                            listBoxMedio.Items.Add(username + "-" + tempo);
                           // listBoxMedio.Items.Add(tempo);
                            //listBoxMedio.Items.Add(quando);

                        }

                        username.Clear();
                        tempo.Clear();
                        quando.Clear();
                    }
                }
            }
        }

        private void listBoxFacil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(online)
            {
                string [] nome = listBoxFacil.SelectedItem.ToString().Split('-');

                verPerfilTop10(nome[0]);

                this.Hide();
                Program.V_ConsultarPerfil.Show();
            }
            
        }

        private void listBoxMedio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(online)
            {
                string[] nome = listBoxMedio.SelectedItem.ToString().Split('-');

                verPerfilTop10(nome[0]);

                this.Hide();
                Program.V_ConsultarPerfil.Show();
            }
            
        }
    }
}
