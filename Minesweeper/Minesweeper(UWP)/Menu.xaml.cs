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
    public delegate void startGame(int numLinhas, int numColunas, int numBombas);
    public sealed partial class Menu : Page
    {
        public event startGame play;
        public Menu()
        {
            //Set tamanho min
           
            ApplicationView.PreferredLaunchViewSize = new Size { Height = 800, Width = 1000 };
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Height = 800, Width = 1000 });



            this.InitializeComponent();
        }
        private void ButtonInstrucoes_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Instrucoes), null);
        }
        private void ButtonJogar_Click(object sender, RoutedEventArgs e)
        {


            if (RadioButtonFacil.IsChecked == true)
            {
                //play(9, 9, 10);
                this.Frame.Navigate(typeof(MainPage));
            }
            else if(RadioButtonMedio.IsChecked == true)
            {
                //play(16, 16, 40);
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
