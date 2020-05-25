﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core.Preview;
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
    public sealed partial class Instrucoes : Page
    {
        public Instrucoes()
        {
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                ApplicationView.GetForCurrentView().TryResizeView(new Size { Height = 800, Width = 1000 });
                this.Frame.Navigate(typeof(Menu), null);
            }
        }

        private async void App_CloseRequested(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {

            e.Handled = true;

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
