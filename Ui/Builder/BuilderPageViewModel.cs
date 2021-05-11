using html_exctractor.Common;
using html_exctractor.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace html_exctractor.Ui.Builder
{
    public class BuilderPageViewModel : ViewModelBase
    {

        private Rule newRule = new Rule();
        public string RuleName
        {
            get => newRule.Name;
            set
            {
                newRule.Name = value;
                NotifyProperty(nameof(RuleName));
                NotifyProperty(nameof(RuleDocumentName));
            }
        }
        public string RuleUrl
        {
            get => newRule.GeneralUrl;
            set
            {
                newRule.Url = value;
                NotifyProperty(nameof(RuleUrl));
            }
        }

        public string RulePaginationSuffix
        {
            get => newRule.PaginationSuffics;
            set
            {
                newRule.PaginationSuffics = value;
                NotifyProperty(nameof(RuleUrl));
            }
        }
        public string RulePagesCountClass
        {
            get => newRule.RulePagesCountClass;
            set
            {
                newRule.RulePagesCountClass = value;
                NotifyProperty(nameof(RulePagesCountClass));
            }
        }

        public string RuleDocumentName
        {
            get => newRule.DocumentNameRegex;
        }

        public ObservableCollection<Field> Fields
        {
            get => newRule.Fields;
            set
            {
                newRule.Fields = value;
                NotifyProperty(nameof(Fields));
            }
        }

        private bool isValuesCorrect = false;
        public bool IsValuesCorrect
        {
            get => isValuesCorrect;
            set
            {
                isValuesCorrect = value;
                NotifyProperty(nameof(IsValuesCorrect));
            }
        }

        public ICommand ClassFieldsTextChangedCommand { get; }
        public ICommand RuleFieldsTextChangedCommand { get; }
        public ICommand SaveRuleCommand { get; }

        public BuilderPageViewModel()
        {
            ClassFieldsTextChangedCommand = new RelayCommand(classFieldTextChangedEvent);
            RuleFieldsTextChangedCommand = new RelayCommand(ruleFieldTextChangedEvent);
            SaveRuleCommand = new SimpleCommand(saveRule);
        }

        private async void saveRule()
        {
            isValuesCorrect = false;
            Loading = true;
            try
            {
                newRule.CreationDate = DateTime.Now.ToShortDateString();
                await FirebaseManager.CreateRule(newRule);
                newRule = new Rule();
                NotifyProperties();
                Navigation.SnackMessage(Utils.GetStringResource("rule_builder_successful_creation"));
            }
            catch (Exception e)
            {
                OnError(e);
            }
            Loading = false;
        }

        private void classFieldTextChangedEvent(object obj)
        {
            var textBox = obj as TextBox;

            if (textBox.Tag != null)
            {
                var index = int.Parse(textBox.Tag as string);
                newRule.Fields[index].Value = textBox.Text;
            }
            OnPropertyUpdated(null);
        }

        private void ruleFieldTextChangedEvent(object obj)
        {
            var textBox = obj as TextBox;
            var textBoxName = textBox.Name;
            if (textBoxName.ToLower() == "RuleNameTextBox".ToLower())
                RuleName = textBox.Text;
            if (textBoxName.ToLower() == "RuleUrlTextBox".ToLower())
                RuleUrl = textBox.Text;
            if (textBoxName.ToLower() == "RulePaginationSuffixTextBox".ToLower())
                RulePaginationSuffix = textBox.Text;
            if (textBoxName.ToLower() == "RulePagesCountClassTextBox".ToLower())
                RulePagesCountClass = textBox.Text;
        }

        protected override void OnPropertyUpdated(string property)
        {
            if (!string.IsNullOrWhiteSpace(property) && property.ToLower() == nameof(IsValuesCorrect).ToLower())
            {
                return;
            }

            Uri uriResult;
            bool isHttpUrl = Uri.TryCreate(RuleUrl, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            IsValuesCorrect = !string.IsNullOrWhiteSpace(RuleName) && !string.IsNullOrWhiteSpace(RuleUrl) && newRule.IsFieldsNotEmpty && isHttpUrl;
        }

    }
}
