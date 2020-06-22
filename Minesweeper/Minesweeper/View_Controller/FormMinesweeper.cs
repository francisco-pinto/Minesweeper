using Minesweeper.Models;
using Minesweeper.View_Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class FormMineSweeper : Form
    {
        private int numMinas;
        private int numLinhas;
        private int numColunas;
        public int segundos = 0;
        private int aux = 0;
        private Button[,] button;

        public event RestartOnlineGame RestartOnlineGame;
        public event startGame play;
        public event MostraBandeirasTodas MostraBandeirasTodas;
        public event MostraBombasTodas MostraBombasTodas;
        public event GetMinas getMinas;
        public event AtualizarMinas AtualizarMinas;
        public event MostraConteudoQuadrado MostraConteudoQuadrado;
        public event AdicionaFlag AdicionaFlag;
        public event VerificarBandeiras VerificarBandeiras;

        public FormMineSweeper()
        {
            InitializeComponent();
        }
        public void CreateMapa(int nMinas, int nLinhas, int nColunas)
        {
            numMinas = nMinas;
            numLinhas = nLinhas;
            numColunas = nColunas;
            button = new Button[nLinhas, nColunas];
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            labelMinas.Text = getMinas();
            labelTime.Text = "000";
        }
        public void InserirBotoes()
        {
            //Criação dinâmica dos botões no form
            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    Button b = button[linha, coluna];//mapa.GetButton(linha, coluna);
                    this.Controls.Add(b);
                    b.MouseUp += b_click; //Cria evento de depois do clique em cada botão 
                }
            }
        }
        private void b_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            MouseEventArgs me = (MouseEventArgs)e;  //Evento do rato

            if (me.Button == MouseButtons.Right)
            {
                AdicionaFlag(b);
                AtualizarMinas(b);
                labelMinas.Text = getMinas();
                //VerificarBandeiras(numLinhas, numColunas, numMinas, labelMinas.Text);

            }
            if (me.Button == MouseButtons.Left)
            {
                MostraConteudoQuadrado(b);
                //MessageBox.Show(b.Name);
            }
        }                
        public void MostraTodasBandeiras()
        {
            MostraBandeirasTodas(button, numLinhas, numColunas);
        }
        public void LimparForm()
        {
            this.Hide();
            //Tornar feliz quando reinicia
            string path = Environment.CurrentDirectory + @"/Botoes/smile.png";
            buttonReiniciar.Image = Image.FromFile(path);
            timer1.Stop();
            aux = 0;
            

            labelMinas.Text = null;
            labelTime.Text = null;
            for(int linha = 0; linha < numLinhas; linha++)
            {
                for(int coluna = 0; coluna < numColunas; coluna++)
                {
                    button[linha, coluna].Visible = false;
                    button[linha, coluna] = null;
                }
            }
        }
        public void ChangeButtonToSad()
        {
            string path = Environment.CurrentDirectory + @"/Botoes/sad.png";
            buttonReiniciar.Image = Image.FromFile(path);
        }
        public void ChangeButtonToHappy()
        {
            string path = Environment.CurrentDirectory + @"/Botoes/smile glasses.png";
            buttonReiniciar.Image = Image.FromFile(path);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory + @"/Music/Clock.wav";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            player.Play();

            segundos += 1;

            if (segundos < 10)
            {
                labelTime.Text = "00" + segundos.ToString();
            }
            else if (segundos < 100)
            {
                labelTime.Text = "0" + segundos.ToString();
            }
            else if (segundos < 1000)
            {
                labelTime.Text = segundos.ToString();
            }
            else
            {
                labelTime.Text = "999";
            }
        }  
        public void CreateButton(int linha, int coluna, CONTEUDO conteudoQuadrado, int ButtonX, int ButtonY, string nome)
        {
            /*Colocar inicialmente imagem de fundo normal*/
            button[linha, coluna] = new Button();
            button[linha, coluna].Name = nome;
            button[linha, coluna].Size = new Size(40, 40);
            button[linha, coluna].Location = new Point(ButtonX, ButtonY);

            //Configurar os botões para permitir retirar margens a estes
            button[linha, coluna].FlatStyle = FlatStyle.Flat;
            button[linha, coluna].FlatAppearance.BorderColor = Color.Gray;
            button[linha, coluna].FlatAppearance.BorderSize = 1;

            button[linha, coluna].MouseDown += B_MouseDown;
            button[linha, coluna].MouseUp += B_MouseUp;

            //Só para testar
            if (conteudoQuadrado == CONTEUDO.BOMBA)
            {
                button[linha, coluna].BackColor = Color.Red;
            }
        }
        private void B_MouseUp(object sender, MouseEventArgs e)
        {
            string path = Environment.CurrentDirectory + @"/Botoes/smile.png";
            buttonReiniciar.Image = Image.FromFile(path);
        }
        private void B_MouseDown(object sender, MouseEventArgs e)
        {
            aux++;
            string path = Environment.CurrentDirectory + @"/Botoes/boca.png";
            buttonReiniciar.Image = Image.FromFile(path);

            if (aux == 1)
            {
                timer1.Start();
            }
            
        }
        public void AtualizaSimboloBotao(int linha, int coluna, string path)
        {
            path = Environment.CurrentDirectory + @"/Botoes" +path;
            //if(path != null)
            if(path != Environment.CurrentDirectory + @"/Botoes")
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
            for(int linha = 0; linha < numLinhas; linha++)
            {
                for(int coluna = 0; coluna < numColunas; coluna++)
                {
                    if(nome == button[linha, coluna].Name)
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
            string path = Environment.CurrentDirectory + @"\Botoes\btnBombaErrada.png";
            for(int i = 0; i < PosErradas.Length; i++)
            {
                string[] pos = PosErradas[i].Split('-');

                button[Int32.Parse(pos[0]), Int32.Parse(pos[1])].Image = Image.FromFile(path);
            }
        }
        public Button GetButton(int linha, int coluna)
        {
            return button[linha, coluna];
        }
        private void FormMineSweeper_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Esconder e não eliminar form
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            LimparForm();
            Program.V_Menu.Show();
        }
        public void InicializarVariaveis()
        {
            //Timer


            //Nº minas
            segundos = 0;
            labelMinas.Text = getMinas();
            labelTime.Text = "000";
        }
        public void setVariaveisFinais(string nMinas, bool timerAtualization)
        {
            if(nMinas != "-1")
            {
                labelMinas.Text = nMinas;
            }

            timer1.Enabled = timerAtualization;
        }
        private void buttonReiniciar_Click(object sender, EventArgs e)
        {       
            LimparForm();

            if (Program.M_menu.online)
            {
                RestartOnlineGame();
            }
            else
            {
                play(numColunas, numLinhas, numMinas);
            }
            
            labelMinas.Text = getMinas();
        }
        public void VerificarRecorde()
        {
            //if melhor tempo
            //abrir FormPedirNome
            //buscar funçao EstadoFicheiro
        }
    }
}