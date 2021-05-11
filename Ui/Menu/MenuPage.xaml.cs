using System.Windows.Controls;

namespace html_exctractor.Ui.Menu
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            App.Navigation.MenuService = Container.NavigationService;
            App.Navigation.LoaderHost = loader_host;
        }

    }
}
