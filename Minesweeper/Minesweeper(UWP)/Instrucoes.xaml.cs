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
    public sealed partial class Instrucoes : Page
    {
        public Instrucoes()
        {
            this.InitializeComponent();
            var appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            appView.Title = "Instruções";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
                ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 1000 });
                this.Frame.Navigate(typeof(Menu), null);
            
        }
    }
}
