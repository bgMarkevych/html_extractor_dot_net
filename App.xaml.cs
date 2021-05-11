using html_exctractor.Common;
using html_exctractor.Model;
using html_exctractor.Repository;
using html_exctractor.Respository;
using html_exctractor.Service;
using Notification.Wpf;
using SimpleInjector;
using System.Windows;

namespace html_exctractor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static Container container;
        public static FirebaseManager FirebaseManager { get => container.GetInstance<FirebaseManager>(); }
        public static Navigation Navigation { get => container.GetInstance<Navigation>(); }
        public static SettingsManager<AppSettings> SettingsManager { get => container.GetInstance<SettingsManager<AppSettings>>(); }
        public static RuleJobWorkerWrapper RuleJobWorkerWrapper { get => container.GetInstance<RuleJobWorkerWrapper>(); }
        public static NotificationManager NotificationManager { get => container.GetInstance<NotificationManager>(); }

        public App()
        {
            container = new Container();
            container.Register<FirebaseManager, FirebaseManager>(Lifestyle.Singleton);
            container.Register(() => new SettingsManager<AppSettings>("settings"), Lifestyle.Singleton);
            container.Register<Navigation, Navigation>(Lifestyle.Singleton);
            container.Register(() => new RuleJobWorkerWrapper(FirebaseManager), Lifestyle.Singleton);
            container.Register(() => new NotificationManager(System.Windows.Threading.Dispatcher.CurrentDispatcher), Lifestyle.Singleton);
            container.Verify();

        }

        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            AppSettingsHandler.ChangeLocalization(SettingsManager.LoadSettings().LocalizationIndex);
            AppSettingsHandler.ChangeTheme(SettingsManager.LoadSettings().DarkTheme);
        }
    }
}
