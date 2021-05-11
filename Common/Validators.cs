using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace html_exctractor.Common
{

    public class EmailPatternValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var email = value as string;
            if (email == null || email.Length == 0)
            {
                return new ValidationResult(false, null);
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email)
                {
                    return new ValidationResult(true, null);
                }
            }
            catch { }
            return new ValidationResult(false, Utils.GetStringResource("email_validation_error_text"));
        }
    }

    public class EmptyValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            if (str != null && str.Length == 0)
            {
                return new ValidationResult(false, Utils.GetStringResource("empty_field_validation_error_text"));
            }
            return new ValidationResult(true, null);
        }
    }

}
