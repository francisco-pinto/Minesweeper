using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Minesweeper_UWP_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public delegate void startGame(int numLinhas, int numColunas, int numBombas);
    public sealed partial class Menu : Page
    {
        public event startGame play;
        public Menu()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size { Height = 800, Width = 1000 };
            TextBoxNumBombas.Visibility = Visibility.Collapsed;
            TextBoxNumLinhas.Visibility = Visibility.Collapsed;
            TextBoxNumColunas.Visibility = Visibility.Collapsed;
        }
        private void ButtonInstrucoes_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Instrucoes), null);
        }
        private void ButtonJogar_Click(object sender, RoutedEventArgs e)
        {


            if (RadioButtonFacil.IsChecked == true)
            {
                TextBoxNumBombas.Visibility = Visibility.Collapsed;
                TextBoxNumLinhas.Visibility = Visibility.Collapsed;
                TextBoxNumColunas.Visibility = Visibility.Collapsed;
             
                
                //play(9, 9, 10);
                /*Adicionar ao eventpo de modo a chamar
                 a pág já inicializada no app.xaml.cs
                 usando o Program*/
                this.Frame.Navigate(typeof(MainPage));
            }
            else if(RadioButtonMedio.IsChecked == true)
            {
                TextBoxNumBombas.Visibility = Visibility.Collapsed;
                TextBoxNumLinhas.Visibility = Visibility.Collapsed;
                TextBoxNumColunas.Visibility = Visibility.Collapsed;


                //play(16, 16, 40);
                this.Frame.Navigate(typeof(MainPage));
            }else if(RadioButtonCustom.IsChecked == true)
            {
                int numLinhas = 0; Int32.TryParse(TextBoxNumLinhas.Text, out numLinhas);
                int numColunas = 0; Int32.TryParse(TextBoxNumColunas.Text, out numColunas);
                int numBombas = 0; Int32.TryParse(TextBoxNumBombas.Text, out numBombas);

                //play(numLinhas, numColunas, numBombas);
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void RadioButtonCustom_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxNumBombas.Text = "Num Bombas";
            TextBoxNumLinhas.Text = "Num Linhas";
            TextBoxNumColunas.Text = "Num Colunas";
            
            //Evento textbox click

            TextBoxNumBombas.Visibility = Visibility.Visible;
            TextBoxNumLinhas.Visibility = Visibility.Visible;
            TextBoxNumColunas.Visibility = Visibility.Visible;
        }
    }
}
