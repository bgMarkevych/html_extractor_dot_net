using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;

namespace html_exctractor.Common
{
    public class AppSettingsHandler
    {

        static AppSettingsHandler()
        {
            languages.Clear();
            languages.Add(new CultureInfo("en-US"));
            languages.Add(new CultureInfo("ua-UA"));
            languages.Add(new CultureInfo("ru-RU"));
        }

        private static List<CultureInfo> languages = new List<CultureInfo>();

        public static event EventHandler LanguageChanged;
        private static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri(String.Format("Resources/Localizations/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    case "ua-UA":
                        dict.Source = new Uri(String.Format("Resources/Localizations/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/Localizations/lang.xaml", UriKind.Relative);
                        break;
                }

                SwapResourceDictionaries(dict, "Resources/Localizations/lang.");
                if (LanguageChanged == null) return;
                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        public static void ChangeLocalization(int index)
        {
            Language = languages[index];
        }

        public static void ChangeTheme(bool isDarkTheme)
        {
            ResourceDictionary dict = new ResourceDictionary();
            var themeLightUri = new Uri("Resources/Themes/ColorsLightTheme.xaml", UriKind.Relative);
            var themeDarkUri = new Uri("Resources/Themes/ColorsDarkTheme.xaml", UriKind.Relative);
            string oldRes;
            if (isDarkTheme)
            {
                dict.Source = themeDarkUri;
                oldRes = themeLightUri.ToString().Remove(themeLightUri.ToString().IndexOf(".") + 1);
            }
            else
            {
                dict.Source = themeLightUri;
                oldRes = themeDarkUri.ToString().Remove(themeDarkUri.ToString().IndexOf(".") + 1);
            }
            SwapResourceDictionaries(dict, oldRes);
        }

        private static void SwapResourceDictionaries(ResourceDictionary newRes, string oldResPath)
        {
            ResourceDictionary oldDict;
            try
            {
                oldDict = Application.Current.Resources.MergedDictionaries.First((source) => source.Source.OriginalString.StartsWith(oldResPath));
            }
            catch { return; }

            if (oldDict != null)
            {
                int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                Application.Current.Resources.MergedDictionaries.Insert(ind, newRes);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newRes);
            }
        }

    }
}
