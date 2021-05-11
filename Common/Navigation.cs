using MaterialDesignThemes.Wpf;
using System.Windows.Navigation;

namespace html_exctractor.Common
{
    public class Navigation
    {

        public NavigationService GlobalService { private get; set; }
        public NavigationService MenuService { private get; set; }
        public DialogHost LoaderHost { private get; set; }
        public SnackbarMessageQueue SnackBarQueue { private get; set; }

        public void NavigateGlobal(object page)
        {
            GlobalService.RemoveBackEntry();
            GlobalService.Navigate(page);
        }

        public void NavigateMenu(object page)
        {
            MenuService.RemoveBackEntry();
            MenuService.Navigate(page);
        }

        public void SnackMessage(string message)
        {
            SnackBarQueue.Enqueue(message);
        }

        public void ProvideLoader(bool visibility)
        {
            LoaderHost.Dispatcher.Invoke(() => LoaderHost.IsOpen = visibility);
        }

    }
}
