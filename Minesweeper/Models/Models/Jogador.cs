using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Jogador
    {
        private string nome;
        private int pontuacao;

        public Jogador() { }
        public string Nome { get => nome; set => nome = value; }
        public int Pontuacao { get => pontuacao; set => pontuacao = value; }
    }
}
