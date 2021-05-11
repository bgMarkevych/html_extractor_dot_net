using html_exctractor.Common;
using html_exctractor.Model;
using html_exctractor.Service;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace html_exctractor.Ui.Rules
{
    public class RulesPageViewModel : ViewModelBase, JobObserver
    {

        private RuleJobWorkerWrapper ruleJobWorkerWrapper = App.RuleJobWorkerWrapper;

        public ObservableCollection<Rule> Rules { get; set; }
        public bool IsRulesEmpty
        {
            get
            {
                if (Rules == null)
                {
                    return false;
                }
                return Rules.Count == 0;
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

        public bool IsEditingSectionVisible { get => SelectedRule != null; }
        private int selectedRuleIndex = -1;
        public Rule SelectedRule { get; set; }

        public ICommand SaveRuleCommand { get; }
        public ICommand DeleteRuleCommand { get; }
        public ICommand EditRuleCommand { get; }
        public ICommand StartRuleCommand { get; }
        public ICommand CloseEditSectionCommand { get; }
        public ICommand ClassFieldsTextChangedCommand { get; }
        public ICommand RuleFieldsTextChangedCommand { get; }

        protected override bool ShowGlobalLoader => true;

        public RulesPageViewModel()
        {
            SaveRuleCommand = new SimpleCommand(saveRule);
            DeleteRuleCommand = new RelayCommand(deleteRule);
            StartRuleCommand = new RelayCommand(startRule);
            CloseEditSectionCommand = new SimpleCommand(closeEditSection);
            EditRuleCommand = new RelayCommand(editRule);
            ClassFieldsTextChangedCommand = new RelayCommand(classFieldTextChangedEvent);
            RuleFieldsTextChangedCommand = new RelayCommand(ruleFieldTextChangedEvent);

            ruleJobWorkerWrapper.JobObserver = this;
        }

        public async void PageLoaded()
        {
            Loading = true;
            try
            {
                Rules = new ObservableCollection<Rule>(await FirebaseManager.GetRules());
            }
            catch (Exception e)
            {
                OnError(e);
            }

            NotifyProperty(nameof(Rules));
            NotifyProperty(nameof(IsRulesEmpty));
            Loading = false;
        }

        private void startRule(object obj)
        {
            ruleJobWorkerWrapper.Execute(obj as Rule);
        }

        private void editRule(object obj)
        {
            var temp = obj as Rule;
            selectedRuleIndex = Rules.IndexOf(temp);
            SelectedRule = new Rule(temp);
            NotifyProperty(nameof(SelectedRule));
            NotifyProperty(nameof(IsEditingSectionVisible));
        }

        private async void saveRule()
        {
            Loading = true;
            try
            {
                var newCopy = new Rule(SelectedRule);
                await FirebaseManager.UpdateRule(newCopy);
                Rules.RemoveAt(selectedRuleIndex);
                Rules.Insert(selectedRuleIndex, newCopy);
                closeEditSection();
            }
            catch (Exception e)
            {
                OnError(e);
            }
            Loading = false;
        }

        private async void deleteRule(object value)
        {
            var rule = value as Rule;
            Loading = true;
            try
            {
                await FirebaseManager.DeleteRule(rule);
                Rules.Remove(rule);
            }
            catch (Exception e)
            {
                OnError(e);
            }

            NotifyProperty(nameof(Rules));
            NotifyProperty(nameof(IsRulesEmpty));
            Loading = false;
        }


        private void closeEditSection()
        {
            SelectedRule = null;
            NotifyProperty(nameof(SelectedRule));
            NotifyProperty(nameof(IsEditingSectionVisible));
        }

        private void classFieldTextChangedEvent(object obj)
        {
            var textBox = obj as TextBox;

            if (textBox.Tag != null)
            {
                var index = int.Parse(textBox.Tag as string);
                SelectedRule.Fields[index].Value = textBox.Text;
            }
            OnPropertyUpdated(null);
        }

        private void ruleFieldTextChangedEvent(object obj)
        {
            if (SelectedRule == null) return;

            var textBox = obj as TextBox;
            var textBoxName = textBox.Name;
            if (textBoxName.ToLower() == "RuleNameTextBox".ToLower())
                SelectedRule.Name = textBox.Text;
            if (textBoxName.ToLower() == "RuleUrlTextBox".ToLower())
                SelectedRule.Url = textBox.Text;
            if (textBoxName.ToLower() == "RulePaginationSuffixTextBox".ToLower())
                SelectedRule.PaginationSuffics = textBox.Text;
            if (textBoxName.ToLower() == "RulePagesCountClassTextBox".ToLower())
                SelectedRule.RulePagesCountClass = textBox.Text;
            OnPropertyUpdated("mock");
        }

        protected override void OnPropertyUpdated(string property)
        {
            if ((!string.IsNullOrWhiteSpace(property) && property.ToLower() == nameof(IsValuesCorrect).ToLower()) || SelectedRule == null)
            {
                return;
            }
            Uri uriResult;
            bool isHttpUrl = Uri.TryCreate(SelectedRule.Url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp);

            IsValuesCorrect = !string.IsNullOrWhiteSpace(SelectedRule.Name) && !string.IsNullOrWhiteSpace(SelectedRule.Url) && SelectedRule.IsFieldsNotEmpty && isHttpUrl;
        }

        public void OnRuleJobRunning(string key)
        {
            Loading = true;
        }

        public void OnRuleJobCompleted(string key)
        {
            Loading = false;
        }


        public void OnRuleJobError(string error)
        {
            Navigation.SnackMessage(error);
        }
    }
}
