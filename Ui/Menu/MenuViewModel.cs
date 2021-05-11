using html_exctractor.Common;
using html_exctractor.Ui.Builder;
using html_exctractor.Ui.TasksHistory;
using html_exctractor.Ui.Rules;
using System;
using System.Windows.Input;
using html_exctractor.Ui.Login;

namespace html_exctractor.Ui.Menu
{
    public class MenuViewModel : ViewModelBase
    {

        private static int RULES_INDEX = 0;
        private static int HISTORY_INDEX = 1;
        private static int BUILDER_INDEX = 2;

        public string Email { get => SettingsManager.LoadSettings().Email; }
        private int selectedMenuIndex = -1;
        public int SelectedMenuIndex
        {
            get => selectedMenuIndex;
            set
            {
                selectedMenuIndex = value;
                navigate();
                NotifyProperty(nameof(SelectedMenuIndex));
            }
        }

        public bool DarkTheme
        {
            get => SettingsManager.LoadSettings().DarkTheme;
            set
            {
                var settings = SettingsManager.LoadSettings();
                settings.DarkTheme = value;
                SettingsManager.SaveSettings(settings);
                changeTheme(value);
            }
        }

        public int SelectedLocalizationIndex
        {
            get => SettingsManager.LoadSettings().LocalizationIndex;
            set
            {
                var settings = SettingsManager.LoadSettings();
                settings.LocalizationIndex = value;
                SettingsManager.SaveSettings(settings);
                changeLocalization();
            }
        }

        public ICommand LogoutCommand { get; }

        public MenuViewModel()
        {
            LogoutCommand = new SimpleCommand(logout);
        }

        public void PageLoaded()
        {
            if (selectedMenuIndex == -1)
                SelectedMenuIndex = 0;
        }

        private void changeTheme(bool isDark)
        {
            AppSettingsHandler.ChangeTheme(isDark);
        }

        private void changeLocalization()
        {
            AppSettingsHandler.ChangeLocalization(SelectedLocalizationIndex);
        }

        private void navigate()
        {
            if (selectedMenuIndex == RULES_INDEX)
            {
                Navigation.NavigateMenu(new RulesPage());
            }
            if (selectedMenuIndex == HISTORY_INDEX)
            {
                Navigation.NavigateMenu(new HistoryPage());
            }
            if (selectedMenuIndex == BUILDER_INDEX)
            {
                Navigation.NavigateMenu(new BuilderPage());
            }
        }

        private void logout()
        {
            var settings = SettingsManager.LoadSettings();
            settings.Email = null;
            SettingsManager.SaveSettings(settings);
            Navigation.NavigateGlobal(new LoginPage());
        }
    }
}
