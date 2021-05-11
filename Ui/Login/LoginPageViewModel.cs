using html_exctractor.Common;
using html_exctractor.Model;
using html_exctractor.Ui.Menu;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;

namespace html_exctractor.Ui.Login
{
    public class LoginPageViewModel : ViewModelBase
    {
        private SignType type = new SignType.LoginType();

        public string TopButtonText { get => type.TopButtonText; }
        public string SmallTitleFirstPart { get => type.SmallTitleFirstPart; }
        public string SmallTitleSecondPart { get => type.SmallTitleSecondPart; }
        public string BigTitle { get => type.BigTitle; }
        public string OkayButtonText { get => type.OkayButtonText; }
        public string GoogleButtonText { get => type.GoogleButtonText; }
        public string BottomTextFirstPart { get => type.BottomTextFirstPart; }
        public string BottomTextSecondPart { get => type.BottomTextSecondPart; }
        public ICommand SwitchTypeCommand { get; set; }
        public ICommand OkayButtonCommand { get; set; }
        public ICommand GoogleButtonCommand { get; set; }
        public ICommand BottomTextCommand { get; set; }

        public string Email { get; set; }


        public LoginPageViewModel()
        {
            SwitchTypeCommand = new SimpleCommand(switchType);
            OkayButtonCommand = new RelayCommand(okayButton);
            GoogleButtonCommand = new SimpleCommand(googleButton);
            BottomTextCommand = new SimpleCommand(bottomText);
            AppSettingsHandler.LanguageChanged += AppSettingsHandler_LanguageChanged;
        }

        private void AppSettingsHandler_LanguageChanged(object sender, EventArgs e)
        {
            notifyTypeChanging();
        }

        private async void bottomText()
        {
            if (type is SignType.RegistrationType)
            {
                switchType();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    Navigation.SnackMessage(Utils.GetStringResource("enter_email_error"));
                    return;
                }

                var addr = new System.Net.Mail.MailAddress(Email);
                if (addr.Address != Email)
                {
                    Navigation.SnackMessage(Utils.GetStringResource("email_validation_error_text"));
                    return;
                }
                try
                {
                    await FirebaseManager.ForgotPassword(Email);
                    Navigation.SnackMessage(Utils.GetStringResource("check_email_message"));
                }
                catch (Exception e)
                {
                    OnError(e);
                }
            }
        }

        private void switchType()
        {
            if (type is SignType.LoginType)
            {
                type = new SignType.RegistrationType();
            }
            else
            {
                type = new SignType.LoginType();
            }
            notifyTypeChanging();
        }

        private void notifyTypeChanging()
        {
            var properties = new List<PropertyInfo>(GetType().GetProperties());
            for (int i = 0; i < properties.Count; i++)
            {
                NotifyProperty(properties[i].Name);
            }
        }

        private async void okayButton(object value)
        {
            var success = false;
            var email = "";
            var password = (value as PasswordBox).Password;

            if (type is SignType.LoginType)
            {
                Loading = true;
                try
                {
                    email = await FirebaseManager.Login(Email, password);
                    success = true;
                }
                catch (Exception e)
                {
                    OnError(e);
                }
                Loading = false;
            }

            if (type is SignType.RegistrationType)
            {
                Loading = true;
                try
                {
                    email = await FirebaseManager.Registration(Email, password);
                    success = true;
                }
                catch (Exception e)
                {
                    OnError(e);
                }
                Loading = false;
            }

            if (success)
            {
                AppSettings settings = SettingsManager.LoadSettings();
                settings.Email = email;
                SettingsManager.SaveSettings(settings);
                Navigation.NavigateGlobal(new MenuPage());
            }

        }

        private async void googleButton()
        {
            Loading = false;
            try
            {
                Loading = true;
                var email = await FirebaseManager.Google();
                AppSettings settings = SettingsManager.LoadSettings();
                settings.Email = email;
                SettingsManager.SaveSettings(settings);
                Navigation.NavigateGlobal(new MenuPage());
            }
            catch (Exception e)
            {
                OnError(e);
            }
            Loading = false;
        }


    }
}
