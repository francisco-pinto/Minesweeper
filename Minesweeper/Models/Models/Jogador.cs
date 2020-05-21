using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Minesweeper
{
    public class Jogador
    {
        private string nome;
        private int pontuacao;
        
        public Jogador() { }
        public string Nome { get => nome; set => nome = value; }
        public int Pontuacao { get => pontuacao; set => pontuacao = value; }

       

        ////Serializaçao

        ////escrever
        //FileStream fs = new FileStream("jogador.xml", FileMode.Create);

        //XmlSerializer xs = new XmlSerializer(typeof(Jogador));
        //xs.Serialize(FileStream, jogador);
        
        //fs.Close();

        ////ler
        //FileStream fs = new FileStream("jogador.xml", FileMode.Open);

        //XmlSerializer xs = new XmlSerializer(typeof(Jogador));
        //Jogador jogador = (Jogador)xs.Deserialize(fs);

        //fs.close();


        ////Linq

        //var jogadores = new Jogador[]
        //{
        //    new Jogador { Nome = "aaaaaa", Pontuacao = 999}
        //};

        //var jogadorQuery =
        //    from jogador in jogadores
        //    select new { jogador.Nome, jogador.Pontuacao };

        //foreach (var jogador in jogadorQuery)
        //    {
        //        Console.WriteLine("{0}", jogador.Nome);
        //        Console.WriteLine("{0}", jogador.Pontuacao);
        //    }


    }
}
