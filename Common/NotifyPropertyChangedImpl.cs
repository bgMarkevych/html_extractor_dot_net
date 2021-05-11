using System.ComponentModel;

namespace html_exctractor.Common
{
    public class NotifyPropertyChangedImpl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyProperty(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
                OnPropertyUpdated(property);
            }
        }

        protected void NotifyProperties()
        {
            if (PropertyChanged != null)
            {
                var properties = GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    var propertyName = properties[i].Name;
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                    OnPropertyUpdated(propertyName);
                }
            }
        }

        protected virtual void OnPropertyUpdated(string property) { }
    }
}
