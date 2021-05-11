using html_exctractor.Ui.Login;
using html_exctractor.Ui.Menu;
using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace html_exctractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Navigation.GlobalService = main_frame.NavigationService;
            snack_bar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            App.Navigation.SnackBarQueue = snack_bar.MessageQueue;
            Page startPage;
            if (string.IsNullOrWhiteSpace(App.SettingsManager.LoadSettings().Email))
            {
                startPage = new LoginPage();
            }
            else
            {
                startPage = new MenuPage();
            }
            main_frame.NavigationService.Navigate(startPage);
        }
    }
}
