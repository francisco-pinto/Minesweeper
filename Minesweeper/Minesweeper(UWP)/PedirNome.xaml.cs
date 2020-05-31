using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Minesweeper_UWP_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PedirNome : Page
    {
        private App Program = App.Current as App;
        string dificuldade;
        public PedirNome()
        {
            this.InitializeComponent();
        }

        private async void ButtonAdicionar_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (TextBoxPedirNome.Text == string.Empty)
            {
                MessageBoxAsync("Introduza o seu nome");
            }
            else
            {
                Program.M_jogador.Nome = TextBoxPedirNome.Text;

                if (Program.M_mapa.NumColunas == 9)
                {
                    dificuldade = "facil";
                }
                else
                {
                    dificuldade = "medio";
                }

                await BuscarPontuacaoAsync(Program.M_jogador.Pontuacao, Program.M_jogador.Nome);
                TextBoxPedirNome.Text = "";
            }

            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 900 });
            this.Frame.Navigate(typeof(Menu));
        }
        private async System.Threading.Tasks.Task BuscarPontuacaoAsync(int pontuacao, string nome)
        {

            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
            StorageFile file = await folder.GetFileAsync("pontuacao.xml");
            int recordeAnterior = 999;
            XDocument document;

            using (Stream fileStream = await file.OpenStreamForWriteAsync())
            {
                document = XDocument.Load(fileStream);
            }

            //XDocument document = XDocument.Load(ApplicationData.Current.LocalFolder;

            if (dificuldade == "facil")
            {
                try
                {
                    recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);
                }
                catch
                {
                    EscritaFicheiroXMLAsync(nome, pontuacao);
                }

                if (pontuacao < recordeAnterior)
                {
                    document.Element("pontuacoes").Element("Facil").Element("Tempo").Value = pontuacao.ToString();
                    document.Element("pontuacoes").Element("Facil").Element("Nome").Value = nome;
                }

            }
            else
            {
                try
                {
                    recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Medio").Element("Tempo").Value);
                }
                catch
                {
                    EscritaFicheiroXMLAsync(nome, pontuacao);
                }
                if (pontuacao < recordeAnterior)
                {
                    document.Element("pontuacoes").Element("Medio").Element("Tempo").Value = pontuacao.ToString();
                    document.Element("pontuacoes").Element("Medio").Element("Nome").Value = nome;
                }
            
            using (Stream fileStream = await file.OpenStreamForWriteAsync())
            {
                document.Save(fileStream);
            }
        }
    
        }
        public async void EscritaFicheiroXMLAsync(string nome, int pontuacao)
        {
            XDocument doc;

            //Criado documento XML em memória com a declaração XML e a estrutura (comentário, elemento Alunos, subelementos Inscritos e NaoInscritos

            if (dificuldade == "facil")
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

            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
            StorageFile file = await folder.GetFileAsync("pontuacao.xml");
            using (Stream fileStream = await file.OpenStreamForWriteAsync())
            {
                doc.Save(fileStream);
            }
        }
        private async void MessageBoxAsync(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}
