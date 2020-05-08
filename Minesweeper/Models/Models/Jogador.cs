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

        Jogador jogador = new Jogador();

        //var jogadorQuery =
        //    from jogador in Jogador
        //    select new { Jogador.nome, jogador.pontuacao };

        //var jogadorQuery =
        //{
        //    new Jogador { nome="aaaaa", pontuacao=999}
        //};
        
        
    }
}
