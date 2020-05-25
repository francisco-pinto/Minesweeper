using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public class Mapa
    {
        private int numLinhas = 0;
        private int numColunas = 0;
        private int nMinasTotais = 0;
        private int numMinasPorEncontrar = 0;
        private Quadrado[,] quadrado;   //Alocados dinamicamente perante a dificuldade escolhida

        public Mapa()
        {

        }
        public void CreateMapa(int nMinas, int nLinhas, int nColunas)
        {
            NMinasTotais = nMinas;
            numMinasPorEncontrar = nMinas;
            NumLinhas = nLinhas;
            NumColunas = nColunas;

            //Criar matriz de quadrados perante a dimensão
            quadrado = new Quadrado[nLinhas, nColunas];
        }  
        //public int NMinasTotais { get => NMinasTotais; set => NMinasTotais = value; }
        public int NumMinasPorEncontrar { get => numMinasPorEncontrar; set => numMinasPorEncontrar = value; }
        public int NumLinhas { get => numLinhas; set => numLinhas = value; }
        public int NumColunas { get => numColunas; set => numColunas = value; }
        public int NMinasTotais { get => nMinasTotais; set => nMinasTotais = value; }

        public bool CheckQuadradoSelecionado(int linha, int coluna)
        {
            if(!quadrado[linha, coluna].Selecionado)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        public string getQuadradoPath(int linha, int coluna)
        {
            return quadrado[linha, coluna].AtualizaSimbolo();
        } 
        public Quadrado GetQuadrado(int linha, int coluna)
        {
            return quadrado[linha, coluna];
        }
        public string getImagePath(Quadrado quadrado)
        {
            string path = @"\Botoes\";
            switch (quadrado.DistanciaBomba)
            {
                /*COLOCAR O TAMANHO DAS IMAGENS MAIS PEQUENO*/

                /*Colocar imagem em .png ou retirar bordas dos quadrados*/
                case -1:
                    return path + "btnVazio.png";
                case 0:
                    return (path + "btnBomba.png");
                default:
                    return (path + "btn" + quadrado.DistanciaBomba + ".png");
            }
        }
        public void AtualizaMinas(string name)
        {
            string[] pos = name.Split('-');
            int linha = Convert.ToInt32(pos[0]);
            int coluna = Convert.ToInt32(pos[1]);

            if (quadrado[linha, coluna].SimboloQuadrado == SIMBOLO.BANDEIRA)
            {
                numMinasPorEncontrar = numMinasPorEncontrar - 1;
            }
            else if (quadrado[linha, coluna].SimboloQuadrado == SIMBOLO.QUESTION)
            {
                numMinasPorEncontrar = numMinasPorEncontrar + 1;
            }    
        }
        public string[] GetBandeirasErradas()
        {
            string[] PosErradas;
            int aux = 0;
            
            for (int linha = 0; linha < NumLinhas; linha++)
            {
                for(int coluna = 0; coluna < NumColunas; coluna++)
                {
                    if(((quadrado[linha, coluna].ConteudoQuadrado == CONTEUDO.NUM) || (quadrado[linha, coluna].ConteudoQuadrado == CONTEUDO.VAZIO)) && (quadrado[linha, coluna].SimboloQuadrado == SIMBOLO.BANDEIRA))
                    {
                        aux++;

                    }
                }
            }

            PosErradas = new string[aux];
            aux = 0;

            for (int linha = 0; linha < NumLinhas; linha++)
            {
                for (int coluna = 0; coluna < NumColunas; coluna++)
                {
                    if (((quadrado[linha, coluna].ConteudoQuadrado == CONTEUDO.NUM) || (quadrado[linha, coluna].ConteudoQuadrado == CONTEUDO.VAZIO)) && (quadrado[linha, coluna].SimboloQuadrado == SIMBOLO.BANDEIRA))
                    {
                        PosErradas[aux] = linha.ToString() + "-" + coluna.ToString();
                        aux++;
                    }
                }
            }

            return PosErradas;
        }
        public void setQuadrado(int distanciaBomba, CONTEUDO conteudoQuadrado, int coluna, int linha, int ButtonX, int ButtonY)
        {
            quadrado[linha, coluna] = new Quadrado(distanciaBomba, conteudoQuadrado, coluna, linha, ButtonX, ButtonY);
        }
    }
}
