using html_exctractor.Common;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace html_exctractor.Model
{
    public class Rule : FirebaseKeyObject
    {
        public string Name { get; set; }
        public string Url { get; set; }
        [JsonIgnore]
        public string GeneralUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Url))
                {
                    return "";
                }
                var globalUrl = Url;
                if (!string.IsNullOrWhiteSpace(PaginationSuffics))
                {
                    globalUrl += PaginationSuffics + "={page}";
                }
                return globalUrl;
            }
        }
        public string PaginationSuffics { get; set; }
        public string RulePagesCountClass { get; set; }
        [JsonIgnore]
        public string DocumentNameRegex
        {
            get
            {
                var value = "dd_MM_yyyy_HH_mm_ss.xlsx";
                if (string.IsNullOrWhiteSpace(Name))
                {
                    return value;
                }
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    var regexNameType = "";
                    var parts = Name.Split(' ');
                    foreach (string part in parts)
                    {
                        regexNameType += part + "_";
                    }
                    value = regexNameType + value;
                }
                return value;
            }
        }
        public string CreationDate { get; set; }
        public ObservableCollection<Field> Fields { get; set; } = new ObservableCollection<Field>();
        [JsonIgnore]
        public bool IsFieldsNotEmpty
        {
            get
            {
                if (Fields.Count == 0)
                {
                    return false;
                }
                foreach (Field f in Fields)
                {
                    if (string.IsNullOrWhiteSpace(f.Value))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private bool isRunning;
        [JsonIgnore]
        public bool IsRunning
        {
            get => isRunning;
            set
            {
                isRunning = value;
                NotifyProperty(nameof(IsRunning));
            }
        }
        public Rule(string name, string url, string paginationSuffics, string rulePagesCountClass, string creationDate, ObservableCollection<Field> fields)
        {
            Name = name;
            Url = url;
            PaginationSuffics = paginationSuffics;
            RulePagesCountClass = rulePagesCountClass;
            CreationDate = creationDate;
            Fields = fields;
        }

        public Rule(Rule rule)
        {
            Key = rule.Key.ToString();
            Name = rule.Name.ToString();
            Url = rule.Url.ToString();
            PaginationSuffics = rule.PaginationSuffics.ToString();
            RulePagesCountClass = rule.RulePagesCountClass.ToString();
            CreationDate = rule.CreationDate.ToString();
            Fields = new ObservableCollection<Field>();
            foreach (Field f in rule.Fields)
            {
                Fields.Insert(int.Parse(f.Position), new Field(f.Position.ToString()) { Value = f.Value.ToString() });
            }
        }

        public Rule() { }

        public void NotifyPropertiesPublic()
        {
            NotifyProperties();
        }

        public string GetFinalDocumentName()
        {
            return DocumentNameRegex.Replace("dd_MM_yyyy_HH_mm_ss", DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"));
        }
    }
}
