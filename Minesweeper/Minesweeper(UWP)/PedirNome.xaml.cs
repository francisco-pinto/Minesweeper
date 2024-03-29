﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        string nomeRecordFacilAnterior = "";
        string nomeRecordMedioAnterior = "";
        int recordeFacilAnterior = 0;
        int recordeMedioAnterior = 0;
        public PedirNome()
        {
            this.InitializeComponent();
            var appView = ApplicationView.GetForCurrentView();
            appView.Title = "Rede";
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
        private async Task BuscarPontuacaoAsync(int pontuacao, string nome)
        {

            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
            StorageFile file = await folder.GetFileAsync("pontuacao.xml");
            int recordeAnterior = 999;
            XDocument document;

            using (Stream fileStream = await file.OpenStreamForReadAsync())
            {
                document = XDocument.Load(fileStream);
            }

            //XDocument document = XDocument.Load(ApplicationData.Current.LocalFolder;

            if (dificuldade == "facil")
            {
                try
                {
                    await GetOtherRecordsAsync("Facil");
                    recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);
                }
                catch
                {
                    //Documento vazio
                    await EscritaFicheiroXMLAsync(nome, pontuacao);
                }

                if (pontuacao < recordeAnterior)
                {
                    //Novo recorde
                    await EscritaFicheiroXMLAsync(nome, pontuacao);
                }

            }
            else
            {
                try
                {
                    await GetOtherRecordsAsync("Medio");
                    recordeAnterior = Int32.Parse(document.Element("pontuacoes").Element("Medio").Element("Tempo").Value); 
                }
                catch
                {
                    //Documento vazio
                    await EscritaFicheiroXMLAsync(nome, pontuacao);
                }
                if (pontuacao < recordeAnterior)
                {
                    //Novo recorde
                    await EscritaFicheiroXMLAsync(nome, pontuacao);
                }
            
                //using (Stream fileStream = await file.OpenStreamForWriteAsync())
                //{
                //    document.Save(fileStream);
                //}
            }
    
        }
        private async Task GetOtherRecordsAsync(string dificuldade)
        {
            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
            StorageFile file = await folder.GetFileAsync("pontuacao.xml");
            XDocument document;

            using (Stream fileStream = await file.OpenStreamForReadAsync())
            {
                document = XDocument.Load(fileStream);
            }

            if (dificuldade == "Medio")
            {
                recordeFacilAnterior = Int32.Parse(document.Element("pontuacoes").Element("Facil").Element("Tempo").Value);
                nomeRecordFacilAnterior = document.Element("pontuacoes").Element("Facil").Element("Nome").Value;
            }
            else
            {
                recordeMedioAnterior = Int32.Parse(document.Element("pontuacoes").Element("Medio").Element("Tempo").Value);
                nomeRecordMedioAnterior = document.Element("pontuacoes").Element("Medio").Element("Nome").Value;
            }
        }
        private async Task EsvaziarDocumento()
        {
            //Elimina documento
            StorageFolder folder = await ApplicationData.Current.LocalFolder.GetFolderAsync("Save");
            StorageFile file = await folder.GetFileAsync("pontuacao.xml");

            await file.DeleteAsync();


            //Cria Documento
            await folder.CreateFileAsync("pontuacao.xml");
        }
        public async Task EscritaFicheiroXMLAsync(string nome, int pontuacao)
        {
            await EsvaziarDocumento();

            XDocument doc;

            //Criado documento XML em memória com a declaração XML e a estrutura (comentário, elemento Alunos, subelementos Inscritos e NaoInscritos

            if (dificuldade == "facil")
            {
                if(recordeMedioAnterior != 0)
                {
                    doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                                new XComment("Recorde em facil e medio"),
                                new XElement("pontuacoes",
                                    new XElement("Facil",
                                        new XElement("Nome", nome),
                                        new XElement("Tempo", pontuacao)
                                    ),
                                    new XElement("Medio",
                                        new XElement("Nome", nomeRecordMedioAnterior),
                                        new XElement("Tempo", recordeMedioAnterior)
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
            }
            else
            {
                if(recordeFacilAnterior != 0)
                {
                    doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                                new XComment("Recorde em facil e medio"),
                                new XElement("pontuacoes",
                                    new XElement("Facil",
                                        new XElement("Nome", nomeRecordFacilAnterior),
                                        new XElement("Tempo", recordeFacilAnterior)
                                    ),
                                    new XElement("Medio",
                                        new XElement("Nome", nome),
                                        new XElement("Tempo", pontuacao)
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
