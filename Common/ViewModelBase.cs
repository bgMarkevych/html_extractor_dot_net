using Firebase.Auth;
using Firebase.Database;
using html_exctractor.Exceptions;
using html_exctractor.Model;
using html_exctractor.Repository;
using html_exctractor.Respository;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace html_exctractor.Common
{
    public abstract class ViewModelBase : NotifyPropertyChangedImpl
    {

        private bool loading;
        public bool Loading
        {
            get => loading;
            set
            {
                loading = value;
                NotifyProperty(nameof(Loading));
                if (ShowGlobalLoader)
                {
                    Navigation.ProvideLoader(value);
                }
            }
        }

        protected virtual bool ShowGlobalLoader => false;

        protected FirebaseManager FirebaseManager = App.FirebaseManager;
        protected SettingsManager<AppSettings> SettingsManager = App.SettingsManager;
        protected Navigation Navigation = App.Navigation;

        protected void OnError(Exception e)
        {
            var message = e.Message;
            if (e is FirebaseAuthException)
            {
                message = JsonConvert.DeserializeObject<FirebaseError>((e as FirebaseAuthException).ResponseData).Message;
            }
            if (e is FirebaseException)
            {
                message = JsonConvert.DeserializeObject<FirebaseError>((e as FirebaseException).ResponseData).Message;
            }
            Navigation.SnackMessage(message);
        }
    }
}
