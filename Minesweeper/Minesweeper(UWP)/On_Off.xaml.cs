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
    public sealed partial class On_Off : Page
    {
        public On_Off()
        {
            this.InitializeComponent();
            var appView = ApplicationView.GetForCurrentView();
            appView.Title = "Rede vs Standalone";
        }

        private void ButtonRede_Click(object sender, RoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 1000 });
            this.Frame.Navigate(typeof(Login), null);
        }
        private void ButtonStandalone_Click(object sender, RoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 700, Width = 1000 });
            this.Frame.Navigate(typeof(Menu), null);
        }
    }
}
