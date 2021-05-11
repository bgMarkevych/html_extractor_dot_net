using html_exctractor.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace html_exctractor.Model
{
    public class History : FirebaseKeyObject
    {

        public string RuleName { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
        public string LocalPath { get; set; }

        [JsonIgnore]
        public bool LoocalFileExists { get => File.Exists(LocalPath); }

        [JsonIgnore]
        public string DocumentName { get => Path.GetFileName(LocalPath); }

        public History(string ruleName, string date, string url, string localPath)
        {
            RuleName = ruleName;
            Date = date;
            Url = url;
            LocalPath = localPath;
        }

        public void NotifyChangeFileStructure()
        {
            NotifyProperty(nameof(LoocalFileExists));
        }
    }
}
