using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace html_exctractor.Repository
{
    public class SettingsManager<T> where T : class
    {
        private readonly string _filePath;

        public SettingsManager(string fileName)
        {
            _filePath = GetLocalFilePath(fileName);
        }

        private string GetLocalFilePath(string fileName)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, fileName);
        }

        public T LoadSettings()
        {
            try
            {
                return File.Exists(_filePath) ?
            JsonConvert.DeserializeObject<T>(File.ReadAllText(_filePath)) :
            (T)Activator.CreateInstance(typeof(T));
            }
            catch { return (T)Activator.CreateInstance(typeof(T)); }
        }


        public void SaveSettings(T settings)
        {
            string json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_filePath, json);
        }
    }
}
