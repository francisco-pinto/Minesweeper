using Minesweeper_UWP_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.View_Controller
{
    public class ControllerMenu
    {
        private App Program;
        public ControllerMenu()
        {
            Program = App.Current as App;
            Program.V_Menu.play += V_Menu_play;
            Program.V_MainPage.play += V_Menu_play;
        }
        private void V_Menu_play(int numLinhas, int numColunas, int numMinas)
        {
            CreateMapa(numMinas, numLinhas, numColunas);
            ResizeForm(30 + 40 * numColunas, 90 + 40 * numLinhas);
            ShowForm();
        }
        private void CriarButtons(int numLinhas, int numColunas, int numMinas)
        {
            int ButtonX = 0, ButtonY = 40;
            CONTEUDO[,] conteudoQuadrado = new CONTEUDO[numLinhas, numColunas];
            int[,] distanciaBomba = new int[numLinhas, numColunas];

            preencherQuadrado( numLinhas,  numColunas,  numMinas, conteudoQuadrado);
            preencherDistancias(numLinhas, numColunas, conteudoQuadrado, distanciaBomba);

            for (int linha = 0; linha < numLinhas; linha++)
            {
                ButtonX = 5;
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    //Nome irá identificar a posição dos botões
                    string nome = linha.ToString() + "-" + coluna.ToString();
                    
                    //Possível Evento de set do quadrado
                    Program.M_mapa.setQuadrado(distanciaBomba[linha, coluna], conteudoQuadrado[linha, coluna], coluna, linha, ButtonX, ButtonY);  
                    //quadrado[linha, coluna] = new Quadrado(distanciaBomba[linha, coluna], conteudoQuadrado[linha, coluna], coluna, linha, ButtonX, ButtonY, nome);
                    
                    
                    Program.V_MainPage.CreateButton(linha, coluna, conteudoQuadrado[linha, coluna], ButtonX, ButtonY, nome);
                    ButtonX += 40;
                }
                ButtonY += 40;
            }
        }
        private void preencherDistancias(int numLinhas, int numColunas, CONTEUDO[,] conteudoQuadrado, int[,] distanciaBomba)
        {
            
            int distancia = -1;

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (conteudoQuadrado[linha, coluna] == CONTEUDO.BOMBA)
                    {
                        distanciaBomba[linha, coluna] = 0;
                    }
                    else if ((distancia = BombasAVolta(conteudoQuadrado, linha, coluna, numLinhas, numColunas)) != -1)
                    {
                        distanciaBomba[linha, coluna] = distancia;
                        distancia = -1;
                    }
                    else
                    {
                        distanciaBomba[linha, coluna] = distancia;
                    }
                }
            }
        }
        private int BombasAVolta(CONTEUDO[,] conteudoQuadrado, int linha, int coluna, int numLinhas, int numColunas)
        {
            int counter = 0;
            bool algumaBomba = false;

            if (conteudoQuadrado[linha, coluna] != CONTEUDO.BOMBA)
            {
                //Em baixo
                if ((linha + 1 < numLinhas) && (conteudoQuadrado[linha + 1, coluna] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em baixo direita
                if (((linha + 1 < numLinhas) && (coluna + 1 < numColunas)) && (conteudoQuadrado[linha + 1, coluna + 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em baixo esquerda
                if (((linha + 1 < numLinhas) && (coluna - 1 >= 0)) && (conteudoQuadrado[linha + 1, coluna - 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //À direita
                if ((coluna + 1 < numColunas) && (conteudoQuadrado[linha, coluna + 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em cima direita
                if (((linha - 1 >= 0) && (coluna + 1 < numColunas)) && (conteudoQuadrado[linha - 1, coluna + 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em cima
                if ((linha - 1 >= 0) && (conteudoQuadrado[linha - 1, coluna] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //Em cima esquerda
                if (((linha - 1 >= 0) && (coluna - 1 >= 0)) && (conteudoQuadrado[linha - 1, coluna - 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }

                //À esquerda
                if ((coluna - 1 >= 0) && (conteudoQuadrado[linha, coluna - 1] == CONTEUDO.BOMBA))
                {
                    counter++;
                    algumaBomba = true;
                }
            }

            if (algumaBomba)
            {
                return counter;
            }
            else
            {
                return -1;
            }

        }
        public void preencherQuadrado(int numLinhas, int numColunas, int numMinas, CONTEUDO[,] localizacaoBombas)
        {
            int contador = 0;
            var rand = new Random();

            do
            {
                int linha = rand.Next(0, numLinhas);
                int coluna = rand.Next(0, numColunas);

                if (localizacaoBombas[linha, coluna] != CONTEUDO.BOMBA)
                {
                    contador++;
                    localizacaoBombas[linha, coluna] = CONTEUDO.BOMBA;
                }

            } while (contador != numMinas);
        }
        private void ShowForm()
        {
            //Erro ao inicializar o mapa 2x
            if (!Program.V_MainPage.IsVisible())
            {
                Program.V_MainPage.InicializarVariaveis();
            }
            Program.V_MainPage.showPage();
        }
        private void CreateMapa(int numMinas, int nLinhas, int nColunas)
        {
            Program.V_MainPage.CreateMapa(numMinas, nLinhas, nColunas);
            Program.M_mapa.CreateMapa(numMinas, nLinhas, nColunas);
            CriarButtons(nLinhas, nColunas, numMinas);
            //Program.V_MainPage.InserirBotoes(); 
        }
        private void ResizeForm(int altura, int largura)
        {
            Program.V_MainPage.Resize(altura, largura);
        }
    }
}
