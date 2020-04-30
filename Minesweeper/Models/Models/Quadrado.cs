using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum SIMBOLO
{
    BANDEIRA = -1,
    VAZIO = 0,
    QUESTION = 1,
    BOMBA = 2       //Apenas mostrar no fim quando perde ou ganha
}
public enum CONTEUDO
{
    VAZIO = 0,
    BOMBA = 1,
    NUM = 2
}

namespace Minesweeper.Models
{
    public class Quadrado
    {
        private SIMBOLO simboloQuadrado;        //-1 -> Bandeira, 0 -> Vazio, 1 -> ?
        private CONTEUDO conteudoQuadrado;      //0 -> Num         1 -> Bomba
        private int contadorCliques;
        private int coluna;
        private int linha;
        private int distanciaBomba;             //-1 Sem bombas,  0 -> Bomba, Outros -> distância
        private bool selecionado;               
        public Quadrado(){
            
        }
        //Construtor por parâmetros para permitir inicializar cada atributo do quadrado 
        public Quadrado(int distancia, CONTEUDO conteudo, int col, int lin, int ButtonX, int ButtonY)
        {
            contadorCliques = 0;
            selecionado = false;
            DistanciaBomba = distancia;

            if (distancia != -1 && distancia != 0)
            {
                conteudoQuadrado = CONTEUDO.NUM;
            }
            else
            {
                conteudoQuadrado = conteudo;
            }    
            //InserirImagemConteudo();

            Coluna = col;
            Linha = lin;
        }
        public bool Selecionado { get => selecionado; set => selecionado = value; }
        public int Coluna { get => coluna; set => coluna = value; }
        public int Linha { get => linha; set => linha = value; }
        public CONTEUDO ConteudoQuadrado { get => conteudoQuadrado; set => conteudoQuadrado = value; }
        public SIMBOLO SimboloQuadrado { get => simboloQuadrado; set => simboloQuadrado = value; }
        public int DistanciaBomba { get => distanciaBomba; set => distanciaBomba = value; }

        //Atualiza simbolos, tal como a bandeira
        public string AtualizaSimbolo()
        {
            string path = Environment.CurrentDirectory + @"\btns\";

            contadorCliques++;
            if (contadorCliques == 1)                           //Ser banderia
            {
                SimboloQuadrado = SIMBOLO.BANDEIRA;
                //Colocar eventos para mudar de cor no form
                return (path + "btnFlag.png");
                //button.BackColor = Color.Red;
            }
            else if (contadorCliques == 2)                      //Ser ?
            {
                SimboloQuadrado = SIMBOLO.QUESTION;
                return (path + "btnQuestion.png");
                //button.BackColor = Color.BlueViolet;
            }
            else if(contadorCliques == 3)                       //Ser bomba
            {
                contadorCliques = 0;
                SimboloQuadrado = SIMBOLO.VAZIO;
                //button.BackColor = Color.Black;
            }

            return null;
            //AtualizaDesign do botão
        }
    }
}
