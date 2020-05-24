using Minesweeper;
using Minesweeper.View_Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    /// 
    public sealed partial class Menu : Page
    {
        public event startGame play;
        private App Program = App.Current as App;
        public Menu()
        {
            this.InitializeComponent();
            TextBoxNumBombas.Visibility = Visibility.Collapsed;
            TextBoxNumLinhas.Visibility = Visibility.Collapsed;
            TextBoxNumColunas.Visibility = Visibility.Collapsed;
        }
        private void ButtonInstrucoes_Click(object sender, RoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 480, Width = 535 });
            this.Frame.Navigate(typeof(Instrucoes), null);
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    Program.C_Intrucoes = new ControllerInstrucoes();
        //    Program.C_jogador = new ControllerJogador();
        //    Program.C_Login = new ControllerLogin();
        //    Program.C_mapa = new ControllerMapa();
        //    Program.C_menu = new ControllerMenu();
        //}

        private void ButtonJogar_Click(object sender, RoutedEventArgs e)
        {


            if (RadioButtonFacil.IsChecked == true)
            {
                TextBoxNumBombas.Visibility = Visibility.Collapsed;
                TextBoxNumLinhas.Visibility = Visibility.Collapsed;
                TextBoxNumColunas.Visibility = Visibility.Collapsed;


                ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 110 + 42 * 9, Width = 42 * 9 });
                Program.C_menu.V_Menu_play(9, 9, 10);
                this.Frame.Navigate(typeof(MainPage));
                /*Adicionar ao eventpo de modo a chamar
                 a pág já inicializada no app.xaml.cs
                 usando o Program*/
                //this.Frame.Navigate(typeof(MainPage));
            }
            else if(RadioButtonMedio.IsChecked == true)
            {
                TextBoxNumBombas.Visibility = Visibility.Collapsed;
                TextBoxNumLinhas.Visibility = Visibility.Collapsed;
                TextBoxNumColunas.Visibility = Visibility.Collapsed;

                ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 110 + 42 * 16, Width = 42 * 16 });
                Program.C_menu.V_Menu_play(16, 16, 40);
                this.Frame.Navigate(typeof(MainPage));
            }else if(RadioButtonCustom.IsChecked == true)
            {
                int numLinhas = 0; Int32.TryParse(TextBoxNumLinhas.Text, out numLinhas);
                int numColunas = 0; Int32.TryParse(TextBoxNumColunas.Text, out numColunas);
                int numBombas = 0; Int32.TryParse(TextBoxNumBombas.Text, out numBombas);


                ApplicationView.GetForCurrentView().TryResizeView(new Windows.Foundation.Size { Height = 110 + 42 * numLinhas, Width = 42 * numColunas });
                Program.C_menu.V_Menu_play(numLinhas, numColunas, numBombas);
                this.Frame.Navigate(typeof(MainPage));
            }
        }
        private void RadioButtonCustom_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxNumBombas.Text = "Num Bombas";
            TextBoxNumLinhas.Text = "Num Linhas";
            TextBoxNumColunas.Text = "Num Colunas";
            
            TextBoxNumBombas.Visibility = Visibility.Visible;
            TextBoxNumLinhas.Visibility = Visibility.Visible;
            TextBoxNumColunas.Visibility = Visibility.Visible;
        }
        private void TextBoxNumBombas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxNumBombas_GotFocus;
        }
        private void TextBoxNumColunas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxNumColunas_GotFocus;
        }
        private void TextBoxNumLinhas_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBoxNumLinhas_GotFocus;
        }
        private void RadioButtonMedio_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxNumBombas.Visibility = Visibility.Collapsed;
            TextBoxNumLinhas.Visibility = Visibility.Collapsed;
            TextBoxNumColunas.Visibility = Visibility.Collapsed;
        }
        private void RadioButtonFacil_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxNumBombas.Visibility = Visibility.Collapsed;
            TextBoxNumLinhas.Visibility = Visibility.Collapsed;
            TextBoxNumColunas.Visibility = Visibility.Collapsed;
        }
    }
}
