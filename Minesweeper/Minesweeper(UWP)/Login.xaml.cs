using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class Login : Page
    {
        FileOpenPicker picker = new FileOpenPicker();
        
        public Login()
        {
            this.InitializeComponent();

            PropriedadesRegistar();
            picker = new FileOpenPicker();
            //mainGrid.Height = 412;
            //mainGrid.Width = 1000;
            
            ApplicationView.PreferredLaunchViewSize = new Size { Height = 520, Width = 1020 };
          
        }

        private void PropriedadesRegistar()
        {
            TBLogin_Email.Visibility = Visibility.Collapsed;
            TBLogin_Fotografia.Visibility = Visibility.Collapsed;
            TBLogin_Name.Visibility = Visibility.Collapsed;
            TBLogin_Pais.Visibility = Visibility.Collapsed;
            TBLogin_Password.Visibility = Visibility.Collapsed;
            TBLogin_Username.Visibility = Visibility.Collapsed;

            Login_Email.Visibility = Visibility.Collapsed;
            Login_Fotografia.Visibility = Visibility.Collapsed;
            Login_Name.Visibility = Visibility.Collapsed;
            Login_Pais.Visibility = Visibility.Collapsed;
            Login_Password.Visibility = Visibility.Collapsed;
            Login_Username.Visibility = Visibility.Collapsed;

            ButtonRegistar.Visibility = Visibility.Collapsed;
        }

        private void ButtonRegistar_Click(object sender, RoutedEventArgs e)
        {
            //ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 790, Width = 1020 });
            //mainGrid.Height = 684;
            //mainGrid.Width = 1000;

            TBLogin_Email.Visibility = Visibility.Visible;
            TBLogin_Fotografia.Visibility = Visibility.Visible;
            TBLogin_Name.Visibility = Visibility.Visible;
            TBLogin_Pais.Visibility = Visibility.Visible;
            TBLogin_Password.Visibility = Visibility.Visible;
            TBLogin_Username.Visibility = Visibility.Visible;

            Login_Email.Visibility = Visibility.Visible;
            Login_Fotografia.Visibility = Visibility.Visible;
            Login_Name.Visibility = Visibility.Visible;
            Login_Pais.Visibility = Visibility.Visible;
            Login_Password.Visibility = Visibility.Visible;
            Login_Username.Visibility = Visibility.Visible;

            ButtonRegistar.Visibility = Visibility.Visible;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 800, Width = 1000 });
            this.Frame.Navigate(typeof(Menu), null);
        }

        private void ButtonInserirfoto_ClickAsync(object sender, RoutedEventArgs e)
        {
            PhotoPickerAsync();
        }

        private async System.Threading.Tasks.Task PhotoPickerAsync()
        {
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {

                //Adicionar Picture box que mostra a foto
                TBLogin_Fotografia.Text = file.Path.ToString();


            }
            else
            {
                TBLogin_Fotografia.Text = "Erro";
            }
        }
    }
}
