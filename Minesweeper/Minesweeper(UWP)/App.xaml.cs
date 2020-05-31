using Minesweeper;
using Minesweeper.Models;
using Minesweeper.View_Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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

namespace Minesweeper_UWP_
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public Mapa M_mapa { get; set; }
        public Minesweeper.Models.Menu M_menu { get; set; }
        public Jogador M_jogador { get; set; }
        public Minesweeper.Models.Login M_Login { get; set; }
        public ConsultaPerfil V_ConsultaPerfil { get; set; }
        public MainPage V_MainPage { get; set; }
        public Instrucoes V_Instrucoes { get; set; }
        public PedirNome V_PedirNome { get; set; }
        public On_Off V_On_Off { get; set; }
        public Login V_Login { get; set; }
        public Menu V_Menu { get; set; }
        public ControllerJogador C_jogador { get; set; }
        public ControllerMapa C_mapa { get; set; }
        public ControllerMenu C_menu { get; set; }
        public ControllerInstrucoes C_Intrucoes { get; set; }
        public ControllerLogin C_Login { get; set; }
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            M_mapa = new Mapa();
            M_menu = new Minesweeper.Models.Menu();
            M_jogador = new Jogador();
            M_Login = new Minesweeper.Models.Login();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(100, 100));

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();    
                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(On_Off), e.Arguments);
                }


                //Altera propriedades da janela
                WindowProprieties();
                
                Window.Current.Activate();
            }

            //V_Menu = rootFrame.Content as Menu;
            //V_Login = rootFrame.Content as Login;
            //V_Instrucoes = rootFrame.Content as Instrucoes;
            //V_MainPage = rootFrame.Content as MainPage;
            //V_PedirNome = rootFrame.Content as PedirNome;
            V_On_Off = rootFrame.Content as On_Off;
            V_ConsultaPerfil = new ConsultaPerfil();
            V_Menu = new Menu();
            V_Login = new Login();
            V_Instrucoes = new Instrucoes();
            V_MainPage = new MainPage();
            V_PedirNome = new PedirNome();
            V_On_Off = new On_Off();

            C_Intrucoes = new ControllerInstrucoes();
            C_jogador = new ControllerJogador();
            C_Login = new ControllerLogin();
            C_mapa = new ControllerMapa();
            C_menu = new ControllerMenu();
        }
        void WindowProprieties(){
            //Altera cores de forma a esconder os botões
            ChangeTitleBarColors();
        }
        void ChangeTitleBarColors()
        {
            //Esconder botões Maximizar e minimizar
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            // Set active window colors
            titleBar.ForegroundColor = Windows.UI.Colors.Black;
            titleBar.BackgroundColor = Windows.UI.Colors.White;
            titleBar.ButtonForegroundColor = Windows.UI.Colors.Transparent;
            titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
            titleBar.ButtonHoverForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonHoverBackgroundColor = Windows.UI.Colors.White;
            titleBar.ButtonPressedForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonPressedBackgroundColor = Windows.UI.Colors.White;

            // Set inactive window colors
            titleBar.InactiveForegroundColor = Windows.UI.Colors.White;
            titleBar.InactiveBackgroundColor = Windows.UI.Colors.White;
            titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.White;
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
