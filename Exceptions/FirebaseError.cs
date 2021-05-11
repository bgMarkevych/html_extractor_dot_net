using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace html_exctractor.Exceptions
{
    public class FirebaseError
    {
        public ParentError error;

        public class ParentError
        {
            public int code;
            public string message;
        }

        public string Message
        {
            get
            {
                var parts = error.message.ToLower().Split('_');
                var normalizedMessage = "";
                for (int i = 0; i < parts.Length; i++)
                {
                    var p = parts[i];
                    if (i == 0)
                    {
                        p = p.First().ToString().ToUpper() + p.Substring(1);
                    }
                    normalizedMessage += p + " ";
                }
                return normalizedMessage;
            }
        }
    }
}
