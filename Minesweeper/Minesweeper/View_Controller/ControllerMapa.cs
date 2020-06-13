using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Minesweeper.View_Controller
{
    class ControllerMapa
    {
        string dificuldade;
        public ControllerMapa()
        {
            Program.V_Mapa.MostraBombasTodas += V_Mapa_MostraBombasTodas;
            Program.V_Mapa.MostraBandeirasTodas += V_Mapa_MostraBandeirasTodas;
            Program.V_Mapa.AdicionaFlag += V_Mapa_AdicionaFlag; 
            Program.V_Mapa.MostraConteudoQuadrado += V_Mapa_MostraConteudoQuadrado;
            Program.V_Mapa.getMinas += V_Mapa_getMinas;
            Program.V_Mapa.AtualizarMinas += V_Mapa_AtualizarMinas;
            Program.V_PedirNome.AtribuirNome += V_PedirNome_AtribuirNome;
            Program.V_Mapa.RestartOnlineGame += V_Mapa_RestartOnlineGame;
            Program.V_Mapa.VerificarBandeiras += V_Mapa_VerificarBandeiras;
        }

        private void V_Mapa_VerificarBandeiras(int numLinhas, int numColunas, int numMinas)
        {
            int count = 0;

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA && Program.M_mapa.GetQuadrado(linha, coluna).SimboloQuadrado == SIMBOLO.BANDEIRA)
                    {
                        count++;
                    }
                }
            }

            if(count == numMinas)
            {
                GanharJogo();
            }
        }

        private void V_Mapa_RestartOnlineGame()
        {
            Program.V_Menu.RestartOnlineGame();
        }
        public bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public void EscritaFicheiroXML(string nome, int pontuacao)
        {
            XDocument doc;

            //Criado documento XML em memória com a declaração XML e a estrutura (comentário, elemento Alunos, subelementos Inscritos e NaoInscritos
            
            if(dificuldade == "facil")
            {
                doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                                new XComment("Recorde em facil e medio"),
                                new XElement("pontuacoes",
                                    new XElement("Facil",
                                        new XElement("Nome", nome),
                                        new XElement("Tempo", pontuacao)
                                    ),
                                    new XElement("Medio",
                                        new XElement("Nome"),
                                        new XElement("Tempo")
                                        )
                                )
                            );
            }
            else
            {
                doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                                new XComment("Recorde em facil e medio"),
                                new XElement("pontuacoes",
                                    new XElement("Facil",
                                        new XElement("Nome"),
                                        new XElement("Tempo")
                                    ),
                                    new XElement("Medio",
                                        new XElement("Nome", nome),
                                        new XElement("Tempo", pontuacao)
                                        )
                                )
                            );
            }
                doc.Save(Environment.CurrentDirectory + @"\Save\pontuacao.xml");    
        }
        private void V_PedirNome_AtribuirNome(string nome)
        {

            if (Program.M_mapa.NumColunas == 9)
            {
                dificuldade = "facil";
            }
            else
            {
                dificuldade = "medio";
            }

            Program.M_jogador.Nome = nome;
            Program.M_jogador.Pontuacao = Program.V_Mapa.segundos;

            BuscarPontuacao(Program.M_jogador.Pontuacao, nome);
        }
        private bool CheckRecorde(int pontuacao)
        {
            try
            {
                XDocument document = XDocument.Load(Environment.CurrentDirectory + @"\Save\pontuacao.xml");
                
                if (Program.M_mapa.NumColunas == 9)
                {
                    int recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);

                if (pontuacao < recordeAnterior)
                {
                    return true;
                }

                }
                else if(Program.M_mapa.NumColunas == 16)
                {
                    int recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Medio").Element("Tempo").Value);

                    if (pontuacao < recordeAnterior)
                    {
                        return true;
                    }
                }

                    return false;

            }
            catch
            {
                return true;
            } 
        }
        private void BuscarPontuacao(int pontuacao, string nome)
        {

            int recordeAnterior = 999;

            XDocument document = XDocument.Load(Environment.CurrentDirectory + @"\Save\pontuacao.xml");
            
            if(dificuldade=="facil")
            {
                try
                {
                    recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);
                }
                catch
                {
                    EscritaFicheiroXML(nome, pontuacao);
                }
                
                if(pontuacao < recordeAnterior)
                {
                    document.Element("pontuacoes").Element("Facil").Element("Tempo").Value = pontuacao.ToString();
                    document.Element("pontuacoes").Element("Facil").Element("Nome").Value = nome;
                }

            }else
            {
                try
                {
                    recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Medio").Element("Tempo").Value);
                }
                catch
                {
                    EscritaFicheiroXML(nome, pontuacao);
                }
                if (pontuacao < recordeAnterior)
                {
                    document.Element("pontuacoes").Element("Medio").Element("Tempo").Value = pontuacao.ToString();
                    document.Element("pontuacoes").Element("Medio").Element("Nome").Value = nome;
                }
            }

            document.Save(Environment.CurrentDirectory + @"\Save\pontuacao.xml");
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
        public void EnviarDadosFimJogo(bool vitoria)
        {
            if(Program.M_menu.online)
            {
                int tempo = Program.V_Mapa.segundos;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://prateleira.utad.priv:1234/LPDSW/2019-2020/resultado/" + Program.M_jogador.Id);

                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

                XDocument xmlPedido = XDocument.Parse("<resultado_jogo><nivel></nivel><tempo></tempo><vitoria></vitoria></resultado_jogo>");

                //if (dificuldade == "facil")
                if(Program.M_mapa.NumLinhas == 9)
                {
                    xmlPedido.Element("resultado_jogo").Element("nivel").Value = "facil";
                }
                else
                {
                    xmlPedido.Element("resultado_jogo").Element("nivel").Value = "medio";
                }

                xmlPedido.Element("resultado_jogo").Element("tempo").Value = tempo.ToString();

                xmlPedido.Element("resultado_jogo").Element("vitoria").Value = vitoria.ToString();

                string mensagem = xmlPedido.Root.ToString();

                byte[] data = Encoding.Default.GetBytes(mensagem);
                request.Method = "POST";
                request.ContentType = "application/xml";
                request.ContentLength = data.Length;

                Stream newStream = request.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string resultado = readStream.ReadToEnd();
                response.Close();
                readStream.Close();

                XDocument xmlResposta = XDocument.Parse(resultado);

                if (xmlResposta.Element("resultado").Element("status").Value == "ERRO")
                {
                    MessageBox.Show(
                        xmlResposta.Element("resultado").Element("contexto").Value,
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
            }
        }

        //public void VerificarBandeiras(int numLinhas, int numColunas)
        //{
        //    for (int linha = 0; linha < numLinhas; linha++)
        //    {
        //        for (int coluna = 0; coluna < numColunas; coluna++)
        //        {
        //            if (Program.M_mapa.GetQuadrado(linha, coluna).ConteudoQuadrado == CONTEUDO.BOMBA && Program.M_mapa.GetQuadrado(linha, coluna).SimboloQuadrado == SIMBOLO.BANDEIRA)
        //            {
        //                GanharJogo();
        //            }
        //        }
        //    }
        //}

        private void GanharJogo()
        {
            //Som de vitória
            string path = Environment.CurrentDirectory + @"\Music\Winning.wav";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            player.Play();
            
            //this.Hide();
            
            
            //verificar online offline
            Program.V_Mapa.MostraTodasBandeiras();
            Program.V_Mapa.setVariaveisFinais("00", false);
            Program.V_Mapa.ChangeButtonToHappy();
            MessageBox.Show("Ganhou o jogo!");

            
            Program.V_Mapa.Hide();
            Program.V_Mapa.LimparForm();

            if (Program.M_menu.online)
            {
                Program.V_Menu.ShowTop10();
                EnviarDadosFimJogo(true);
                Program.V_Menu.Show();
            }
            else
            {
                if (CheckRecorde(Program.V_Mapa.segundos) == true)
                {
                    Program.V_PedirNome.Show();
                }
                else
                {
                    Program.V_Menu.Show();
                }
            }
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
            
            string[] posErradas = Program.M_mapa.GetBandeirasErradas();

            //Som de derrota
            string path = Environment.CurrentDirectory + @"\Music\Explosion.wav";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            player.Play();

            Program.V_Mapa.setVariaveisFinais("-1", false);          
            Program.V_Mapa.MostraTodasBombas();
            Program.V_Mapa.MostraBandeirasErradas(posErradas);
            Program.V_Mapa.ChangeButtonToSad();

            MessageBox.Show("Perdeu o jogo!");

            Program.V_Mapa.Hide();
            Program.V_Mapa.LimparForm();
            
            if(Program.M_menu.online)
            {
                Program.V_Menu.ShowTop10();
                EnviarDadosFimJogo(false);
            }

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
            int quadradoPorVer = numColunas * numLinhas;
            
            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    if (!Program.M_mapa.CheckQuadradoSelecionado(linha, coluna))
                    {
                        quadradoPorVer = quadradoPorVer - 1;
                        //MessageBox.Show("YAAAAAAAAAAA");
                    }
                }
            }

            if (quadradoPorVer == numMinas)
            {
                GanharJogo();
            }

            

            //Função de checkar se as bandeiras estão todas selecionadas
        }
        private void MostrarQuadrado(int linha, int coluna, Quadrado quadrado)
        {
            quadrado.Selecionado = true;
            string nome = quadrado.Linha.ToString() + '-' + quadrado.Coluna.ToString();
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