using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper.View_Controller
{
    class ControllerMapa
    {
        public ControllerMapa()
        {
            Program.V_Mapa.MostraBombasTodas += V_Mapa_MostraBombasTodas;
            Program.V_Mapa.MostraBandeirasTodas += V_Mapa_MostraBandeirasTodas;
            Program.V_Mapa.AdicionaFlag += V_Mapa_AdicionaFlag; 
            Program.V_Mapa.MostraConteudoQuadrado += V_Mapa_MostraConteudoQuadrado;
            Program.V_Mapa.getMinas += V_Mapa_getMinas;
            Program.V_Mapa.AtualizarMinas += V_Mapa_AtualizarMinas;
        }
        private void V_Mapa_MostraBandeirasTodas(Button[,] b, int numLinhas, int numColunas)
        {
            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA)
                    {
                        string path = Environment.CurrentDirectory + @"\Botoes\btnFlag.png";
                        AtualizaImagemConteudo(b[linha, coluna].Name, path);
                    }
                }
            }
        }
        private void GanharJogo()
        {
            //Som de vitória
            string path = Environment.CurrentDirectory + @"\Music\Winning.wav";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            player.Play();

            Program.V_Mapa.MostraTodasBandeiras();
            Program.V_Mapa.setVariaveisFinais("00", false);
            Program.V_Mapa.GanharHappy();
            MessageBox.Show("Ganhou o jogo!");

            
            //Program.V_Mapa.VerificarRecorde();

            Program.V_Mapa.Hide();
            Program.V_Mapa.LimparForm();
            Program.V_Menu.Show();
        }
        private void V_Mapa_MostraBombasTodas(Button[,] b, int numLinhas, int numColunas)
        {  
            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA)
                    {
                        string path = Environment.CurrentDirectory + Program.M_mapa.getImagePath(Program.M_mapa.GetQuadrado(linha, coluna));
                        AtualizaImagemConteudo(b[linha, coluna].Name, path);
                    }
                }
            }
        }
        private void AtualizaImagemConteudo(string nome, string path)
        {
            Program.V_Mapa.AtualizaImagemConteudo(nome, path);
        }
        private void AlteraSimboloBotao(int linha, int coluna, string path)
        {
            Program.V_Mapa.AtualizaSimboloBotao(linha, coluna, path);
        }
        private void V_Mapa_AtualizarMinas(Button b)
        {
            Program.M_mapa.AtualizaMinas(b.Name);
        }
        private string V_Mapa_getMinas()
        {
            return Program.M_mapa.NumMinasPorEncontrar.ToString();
        }
        private void PerderJogo()
        {
            //Som de derrota
            string path = Environment.CurrentDirectory + @"\Music\Explosion.wav";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            player.Play();

            Program.V_Mapa.setVariaveisFinais("-1", false);
            string[] posErradas = Program.M_mapa.GetBandeirasErradas();
            Program.V_Mapa.MostraTodasBombas();
            Program.V_Mapa.MostraBandeirasErradas(posErradas);
            Program.V_Mapa.PerderSad();
            MessageBox.Show("Perdeu o jogo!");
            Program.V_Mapa.Hide();
            Program.V_Mapa.LimparForm();
            Program.V_Menu.Show();
            /*Pontuação*/
        }
        private void V_Mapa_MostraConteudoQuadrado(Button b)
        {
            /*Identifica o quadrado que possui aquele nome*/
            int numLinhas = Program.M_mapa.NumLinhas;
            int numColunas = Program.M_mapa.NumColunas;
            int numMinas = Program.M_mapa.NumMinasPorEncontrar;
            string[] pos = b.Name.Split('-');
            int linha = Convert.ToInt32(pos[0]);
            int coluna = Convert.ToInt32(pos[1]);

            Quadrado quadrado = Program.M_mapa.GetQuadrado(linha, coluna);

            if (Program.M_mapa.CheckQuadradoSelecionado(linha, coluna) && (quadrado.SimboloQuadrado == SIMBOLO.VAZIO))
            {
                //Botão vazio fazer abrir todos os vazios
                if (Program.M_mapa.getImagePath(quadrado).Contains("Vazio"))
                {
                    MostraQuadradosVaziosTodos(linha, coluna, quadrado, numLinhas, numColunas);
                }

                //Mostrar quadrado individual
                MostrarQuadrado(linha, coluna, quadrado);
            }

            //Verifica se perdeu o jogo
            if (quadrado.ConteudoQuadrado == CONTEUDO.BOMBA)
            {
                PerderJogo();
            }

            //Verifica se ganha o jogo
            //Talvez mudar nome da função
            //Colocar condição de entrada para não percorrer sempre que código é executado
            VerificarQuadradosExpostos(numLinhas, numColunas, numMinas);
        }
        public void VerificarQuadradosExpostos(int numLinhas, int numColunas, int numMinas)
        {
            int quadradoporver = numColunas * numLinhas;

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (!Program.M_mapa.CheckQuadradoSelecionado(linha, coluna))
                    {
                        quadradoporver = quadradoporver - 1;
                        //MessageBox.Show("YAAAAAAAAAAA");
                    }
                }
            }

            if (quadradoporver == numMinas)
            {
                GanharJogo();
            }
        }
        private void MostrarQuadrado(int linha, int coluna, Quadrado quadrado)
        {
            string nome = quadrado.Linha.ToString() + '-' + quadrado.Coluna.ToString();
            quadrado.Selecionado = true;
            string path = Environment.CurrentDirectory + Program.M_mapa.getImagePath(quadrado);
            AtualizaImagemConteudo(nome, path);   //Ir pelo nome
        }
        public void MostraQuadradosVaziosTodos(int linha, int coluna, Quadrado quadrado, int numLinhas, int numColunas)
        {
            
            if ((!quadrado.Selecionado) && (quadrado.ConteudoQuadrado != CONTEUDO.NUM) && (quadrado.SimboloQuadrado == SIMBOLO.VAZIO))
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
        private void V_Mapa_AdicionaFlag(Button b)
        {
            string nome = b.Name;
            
            string[] pos = nome.Split('-');
            int linha = Convert.ToInt32(pos[0]);
            int coluna = Convert.ToInt32(pos[1]);

            if (Program.M_mapa.CheckQuadradoSelecionado(linha, coluna))
            {
                //Função deveria estar no MAPA?
                string path = Program.M_mapa.getQuadradoPath(linha, coluna);

                /*Passar isso a imagens mais tarde*/
                AlteraSimboloBotao(linha, coluna, path);
            }
        }
    }
}
