using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core.Preview;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Minesweeper_UWP_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Button[,] button;
        private int nLinhas = 9;       
        private int nColunas = 9;
        private int nMinas = 10;

        /*Não conseguimos usar 
         controllers para mudar 
         isto através da escolha*/

        public int NMinas { get => nMinas; set => nMinas = value; }
        public int NColunas { get => nColunas; set => nColunas = value; }
        public int NLinhas { get => nLinhas; set => nLinhas = value; }

        public MainPage()
        {   
            this.InitializeComponent();
            //Muda tamanho da janela
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 110 + 42 * NLinhas, Width = 42 * NColunas });

            //Tamanho min da janela
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(110 + 42 * NLinhas - 106, 42 * NColunas + 112));

            this.TextBlockMinas.Text = NMinas.ToString();
            CreateMapa(NLinhas, NColunas);      
        }
        public void CreateMapa(int numLinhas, int numColunas)
        {
            int ButtonX = 5;
            int ButtonY = 10;
            string nome;

            button = new Button[numLinhas, numColunas];

            CreateGridProprieties(numLinhas, numColunas);

            for (int linha = 0; linha < numLinhas; linha++)
            {
                for (int coluna = 0; coluna < numColunas; coluna++)
                {
                    nome = linha + "-" + coluna;
                    CreateButton(linha, coluna, ButtonX, ButtonY, nome);
                }
            }
        }
        public void CreateGridProprieties(int numLinhas, int numColunas)
        {
            //Fazer a grelha
            for (int linha = 0; linha < numLinhas; linha++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(42); 
                MapaGrid.RowDefinitions.Add(gridRow);
            }

            for(int coluna = 0; coluna < numColunas; coluna++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridCol.Width = new GridLength(42);
                MapaGrid.ColumnDefinitions.Add(gridCol);
            }
        }
        public void CreateButton(int linha, int coluna, int ButtonX, int ButtonY, string nome)
        {
            button[linha, coluna] = new Button();
            button[linha, coluna].Name = nome;
            button[linha, coluna].Height = 40;
            button[linha, coluna].Width = 40;
            button[linha, coluna].Content = nome;
            button[linha, coluna].Click += B_Click;

            MapaGrid.Children.Add(button[linha, coluna]);
            Grid.SetColumn(button[linha, coluna], coluna);
            Grid.SetRow(button[linha, coluna], linha);

        }
        private void B_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            b.Content = "Click";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 800, Width = 1000 });
            this.Frame.Navigate(typeof(Menu), null);
        }
        
        
    }
}
