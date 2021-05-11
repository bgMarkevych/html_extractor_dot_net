using html_exctractor.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace html_exctractor.Model
{
    public abstract class FirebaseKeyObject : NotifyPropertyChangedImpl
    {
        [JsonIgnore]
        public string Key { get; set; }
    }
}
