using Minesweeper.Models;
using Minesweeper_UWP_;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Minesweeper.View_Controller
{
    public class ControllerMapa
    {
        private App Program;
        public ControllerMapa()
        {
            Program = App.Current as App;
            //Program.V_MainPage.MostraBombasTodas += V_Mapa_MostraBombasTodas;
            //Program.V_MainPage.MostraBandeirasTodas += V_Mapa_MostraBandeirasTodas;
            //Program.V_MainPage.AdicionaFlag += V_Mapa_AdicionaFlag; 
            //Program.V_MainPage.MostraConteudoQuadrado += V_Mapa_MostraConteudoQuadrado;
            ////Program.V_MainPage.getMinas += V_Mapa_getMinas;
            //Program.V_MainPage.AtualizarMinas += V_Mapa_AtualizarMinas;
            //Program.V_MainPage.getMinas += V_MainPage_getMinas;
        }

        private int V_MainPage_getMinas()
        {
            return Program.M_mapa.NMinasTotais;
        }

        //private void V_Mapa_MostraBandeirasTodas(Button[,] b, int numLinhas, int numColunas)
        //{
        //    for (int linha = 0; linha < numLinhas; linha++)
        //    {
        //        for (int coluna = 0; coluna < numColunas; coluna++)
        //        {
        //            if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA)
        //            {
        //                string path = "btnFlag.png";
        //                AtualizaImagemConteudo(b[linha, coluna].Name, path);
        //            }
        //        }
        //    }
        //}
        private void GanharJogo()
        {
            //Program.V_MainPage.MostraTodasBandeiras();
            //Program.V_MainPage.setVariaveisFinais("00", false);
            //Program.V_MainPage.GanharHappy();

            ////Mensagem ganhou o jogo
            ////MessageBox.Show("Ganhou o jogo!");
            
            
            ////Program.V_MainPage.Hide();
            //Program.V_MainPage.LimparForm();
            //Program.V_MainPage.showPage();
        }
        //private void V_Mapa_MostraBombasTodas(Button[,] b, int numLinhas, int numColunas)
        //{  
        //    for (int linha = 0; linha < numLinhas; linha++)
        //    {
        //        for (int coluna = 0; coluna < numColunas; coluna++)
        //        {
        //            if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA)
        //            {
        //                string path = Program.M_mapa.getImagePath(Program.M_mapa.GetQuadrado(linha, coluna));
                        
        //                if(path == @"\Botoes\btnVazio.png")
        //                {
        //                    AtualizaImagemConteudo(b[linha, coluna].Name, "btnVazio.png");
        //                }else if(path == @"\Botoes\btnBomba.png")
        //                {
        //                    AtualizaImagemConteudo(b[linha, coluna].Name, "btnVazio.png");
        //                }
        //                else
        //                {
        //                    string newPath = path.Remove(0, 8);
        //                    AtualizaImagemConteudo(b[linha, coluna].Name, newPath);
        //                } 
        //            }
        //        }
        //    }
        //}
        //private void AtualizaImagemConteudo(string nome, string path)
        //{
        //    Program.V_MainPage.AtualizaImagemConteudo(nome, path);
        //}
        private void AlteraSimboloBotao(int linha, int coluna, string path)
        {
            Program.V_MainPage.AtualizaSimboloBotao(linha, coluna, path);
        }
        public void V_Mapa_AtualizarMinas(Button b)
        {
            Program.M_mapa.AtualizaMinas(b.Name);
        }
        private string V_Mapa_getMinas()
        {
            return Program.M_mapa.NumMinasPorEncontrar.ToString();
        }
        private void PerderJogo()
        {
            //Program.V_MainPage.setVariaveisFinais("-1", false);
            //string[] posErradas = Program.M_mapa.GetBandeirasErradas();
            //Program.V_MainPage.MostraTodasBombas();
            //Program.V_MainPage.MostraBandeirasErradas(posErradas);
            //Program.V_MainPage.PerderSad();

            
            //Perder o jogo Mensagem
            //MessageBox.Show("Perdeu o jogo!");
            

            //Program.V_MainPage.Hide();
            //Program.V_MainPage.LimparForm();
            
            //Program.V_MainPage.showPage();
            /*Pontuação*/
        }
        public int V_Mapa_MostraConteudoQuadrado(Button b)
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
        public string V_Mapa_AdicionaFlag(Button b)
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
    }
}
