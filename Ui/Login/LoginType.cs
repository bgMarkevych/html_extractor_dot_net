using html_exctractor.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace html_exctractor.Ui.Login
{
    interface SignType
    {

        public string TopButtonText { get; }
        public string SmallTitleFirstPart { get; }
        public string SmallTitleSecondPart { get; }
        public string BigTitle { get; }
        public string OkayButtonText { get; }
        public string GoogleButtonText { get; }
        public string BottomTextFirstPart { get; }
        public string BottomTextSecondPart { get; }


        public class LoginType : SignType
        {
            public string TopButtonText => Utils.GetStringResource("registration_button_text");

            public string SmallTitleFirstPart => Utils.GetStringResource("login_small_title_text_part_first");

            public string SmallTitleSecondPart => Utils.GetStringResource("login_small_title_text_part_second");

            public string BigTitle => Utils.GetStringResource("login_big_title_text");

            public string OkayButtonText => Utils.GetStringResource("login_button_text");

            public string GoogleButtonText => Utils.GetStringResource("login_google_button_text");

            public string BottomTextFirstPart => "";

            public string BottomTextSecondPart => Utils.GetStringResource("login_bottom_text");
        }

        public class RegistrationType : SignType
        {
            public string TopButtonText => Utils.GetStringResource("login_button_text");

            public string SmallTitleFirstPart => Utils.GetStringResource("registration_small_title_text");

            public string SmallTitleSecondPart => "";

            public string BigTitle => Utils.GetStringResource("registration_big_title_text");

            public string OkayButtonText => Utils.GetStringResource("registration_button_text");

            public string GoogleButtonText => Utils.GetStringResource("registration_google_button_text");

            public string BottomTextFirstPart => Utils.GetStringResource("registration_bottom_text");

            public string BottomTextSecondPart => Utils.GetStringResource("login_button_text");
        }


    }
}
