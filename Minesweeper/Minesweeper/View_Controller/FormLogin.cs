﻿using System;
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
   public partial class FormLogin : Form
    {
        public event dadosUtilizador EnviarDados;

        string imagem;
        public FormLogin()
        {
            InitializeComponent();
        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            this.Hide();

            //Autenticar
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/Autentica");

            // Com o acesso usa HTTPS e o servidor usar cerificados autoassinados, temos de configurar o cliente para aceitar sempre o certificado.
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);


            // prepara os dados do pedido a partir de uma string só com a estrutura do XML (sem dados)
            XDocument xmlPedido = XDocument.Parse("<credenciais><username></username><password></password></credenciais>");
            //preenche os dados no XML
            xmlPedido.Element("credenciais").Element("username").Value = textBoxNome.Text; // colocar aqui o username do utilizador
            xmlPedido.Element("credenciais").Element("password").Value = textBoxPasse.Text; // colocar aqui a palavra passe do utilizador

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
                MessageBox.Show(xmlResposta.Element("resultado").Element("contexto").Value, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxNome.Text = null;
                textBoxPasse.Text = null;
                this.Hide();
                Program.V_OnOff.Show();

            } else
            {
                // assume a autenticação e obtem o ID do resultado...para ser usado noutros pedidos
                string ID = xmlResposta.Element("resultado").Element("objeto").Element("ID").Value;
                MessageBox.Show("Entrou!");
                EnviarDados(ID, textBoxNome.Text);
                Program.V_Menu.Show();
            
            }

            //Erro no Login segue para o offline
            

        }
        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
        private void buttonRegistar_Click(object sender, EventArgs e)
        {
            Program.V_Login.Text = "Registo";
            Program.V_Login.Size = new Size(500, 400);
            labelemail.Visible = true;
            labelfoto.Visible = true;
            labelNomeAbreviado.Visible = true;
            labelPais.Visible = true;
            labelpassword.Visible = true;
            labelusername.Visible = true;
            textBoxEmail.Visible = true;
            textBoxNomeAbreviado.Visible = true;
            textBoxPais.Visible = true;
            textBoxPassword.Visible = true;
            textBoxUsername.Visible = true;

        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            Program.V_Login.Size = new Size(500, 100);

            // Set to no text.
            //textBoxPasse.Text = "";

            // The password character is an asterisk.
            textBoxPasse.PasswordChar = '*';

            // The control will allow no more than 14 characters.
            //textBoxPasse.MaxLength = 14;
        }
        private void buttonOpenFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "PNG|*.png; *.jpg; *.jpeg";
            ofd.CheckFileExists = true;


            if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxFileName.Text = ofd.SafeFileName;
                textBoxSAvedFileName.Text = ofd.FileName;
                //pictureBoxFoto.Image = new Bitmap(ofd.FileName);
               
               
                    Bitmap image = new Bitmap(ofd.FileName);
                    pictureBoxFoto.Image = (Image)image;

                    byte[] imageArray = System.IO.File.ReadAllBytes(ofd.FileName);
                    string base64Text = Convert.ToBase64String(imageArray); //base64Text must be global but I'll use  richtext
                    string fileEXT = ofd.FileName;
                    imagem = "data:" + fileEXT + ";base64," + base64Text;
                
            }
        }
        private void buttonSubmeter_Click(object sender, EventArgs e)
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
            if (textBoxNomeAbreviado.Text == null) 
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }
            else
            {
                xmlPedido.Element("registo").Element("nomeabreviado").Value = textBoxNomeAbreviado.Text;
            }

            //Username
            if (textBoxUsername.Text == null)
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }
            else
            {
                 xmlPedido.Element("registo").Element("username").Value = textBoxUsername.Text; // colocar aqui o username do utilizador
            }


            //Password
            if (textBoxPassword.Text == null)
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }
            else
            {
                xmlPedido.Element("registo").Element("password").Value = textBoxPassword.Text;
            }


            if (textBoxEmail.Text == null)
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }
            else
            {
                xmlPedido.Element("registo").Element("email").Value = textBoxEmail.Text;// colocar aqui a palavra passe do utilizador
            }

            //Imagem
            if (imagem == null)
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }
            else
            {
               xmlPedido.Element("registo").Element("fotografia").Value = imagem;
            }

            if (textBoxPais.Text == null)
            {
                MessageBox.Show("Preencha todos os campos");
                return;
            }
            else
            {
                xmlPedido.Element("registo").Element("pais").Value = textBoxPais.Text;
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
                MessageBox.Show(
                xmlResposta.Element("resultado").Element("contexto").Value,
                "Erro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                 );
            }
            else
            {
                
                MessageBox.Show( "Registo submetido com sucesso");
                Program.V_Login.Size = new Size(500, 100);


                textBoxEmail.Text = null;
                textBoxNomeAbreviado.Text = null;
                textBoxPais.Text = null;
                textBoxPassword.Text = null;
                textBoxUsername.Text = null;
                pictureBoxFoto.Image = null;

                // assume a autenticação e obtem o ID do resultado...para ser usado noutros pedidos
                // xmlResposta.Element("resultado").Element("objeto").Element("ID").Value }
            }
        }
    }
}
