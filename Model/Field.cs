using html_exctractor.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace html_exctractor.Model
{
    public class Field : NotifyPropertyChangedImpl
    {
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                NotifyProperty(nameof(Value));
            }
        }
        private string id;
        public string Position
        {
            get => id;
            set
            {
                id = value;
                NotifyProperty(nameof(Position));
            }
        }

        public Field(string id)
        {
            Position = id;
        }
    }
}
