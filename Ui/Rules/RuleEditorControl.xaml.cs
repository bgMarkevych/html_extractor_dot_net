using html_exctractor.Common;
using html_exctractor.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace html_exctractor.Ui.Rules
{
    /// <summary>
    /// Логика взаимодействия для RuleEditorControl.xaml
    /// </summary>
    public partial class RuleEditorControl : UserControl
    {

        public int InnerSectionWidth
        {
            get => (int)GetValue(InnerSectionWidthProperty);
            set => SetValue(InnerSectionWidthProperty, value);
        }

        public static readonly DependencyProperty InnerSectionWidthProperty = DependencyProperty.Register(nameof(InnerSectionWidth), typeof(int), typeof(RuleEditorControl), new PropertyMetadata(default(int)));

        public Visibility GlobalVisibility
        {
            get => (Visibility)GetValue(GlobalVisibilityProperty);
            set => SetValue(GlobalVisibilityProperty, value);
        }

        public static readonly DependencyProperty GlobalVisibilityProperty = DependencyProperty.Register(nameof(GlobalVisibility), typeof(Visibility), typeof(RuleEditorControl), new PropertyMetadata(default(Visibility)));

        public string BuilderTitle
        {
            get => (string)GetValue(BuilderTitleProperty);
            set => SetValue(BuilderTitleProperty, value);
        }

        public static readonly DependencyProperty BuilderTitleProperty = DependencyProperty.Register(nameof(BuilderTitle), typeof(string), typeof(RuleEditorControl), new PropertyMetadata(Utils.GetStringResource("rule_builder_title_hint")));

        public ObservableCollection<Field> Fields
        {
            get => (ObservableCollection<Field>)GetValue(FieldsProperty);
            set => SetValue(FieldsProperty, value);
        }

        public static readonly DependencyProperty FieldsProperty = DependencyProperty.Register(nameof(Fields), typeof(ObservableCollection<Field>), typeof(RuleEditorControl), new PropertyMetadata(default(ObservableCollection<Field>)));

        public string RuleName
        {
            get => (string)GetValue(RuleNameProperty);
            set => SetValue(RuleNameProperty, value);
        }

        public static readonly DependencyProperty RuleNameProperty = DependencyProperty.Register(nameof(RuleName), typeof(string), typeof(RuleEditorControl), new PropertyMetadata(string.Empty));

        public string RuleUrl
        {
            get => (string)GetValue(RuleUrlProperty);
            set => SetValue(RuleUrlProperty, value);
        }

        public static readonly DependencyProperty RuleUrlProperty = DependencyProperty.Register(nameof(RuleUrl), typeof(string), typeof(RuleEditorControl), new PropertyMetadata(string.Empty));
        public string RulePaginationSuffix
        {
            get => (string)GetValue(RulePaginationSuffixProperty);
            set => SetValue(RulePaginationSuffixProperty, value);
        }

        public static readonly DependencyProperty RulePaginationSuffixProperty = DependencyProperty.Register(nameof(RulePaginationSuffix), typeof(string), typeof(RuleEditorControl), new PropertyMetadata(string.Empty));
        public string RulePagesCountClass
        {
            get => (string)GetValue(RulePagesCountClassProperty);
            set => SetValue(RulePagesCountClassProperty, value);
        }

        public static readonly DependencyProperty RulePagesCountClassProperty = DependencyProperty.Register(nameof(RulePagesCountClass), typeof(string), typeof(RuleEditorControl), new PropertyMetadata(string.Empty));
        public ICommand RuleFieldsTextChangedCommand
        {
            get => (ICommand)GetValue(RuleFieldsTextChangedCommandProperty);
            set => SetValue(RuleFieldsTextChangedCommandProperty, value);
        }

        public static readonly DependencyProperty RuleFieldsTextChangedCommandProperty = DependencyProperty.Register(nameof(RuleFieldsTextChangedCommand), typeof(ICommand), typeof(RuleEditorControl), new PropertyMetadata(default(ICommand)));
        public ICommand CssFieldsTextChangedCommand
        {
            get => (ICommand)GetValue(CssFieldsTextChangedCommandProperty);
            set => SetValue(CssFieldsTextChangedCommandProperty, value);
        }

        public static readonly DependencyProperty CssFieldsTextChangedCommandProperty = DependencyProperty.Register(nameof(CssFieldsTextChangedCommand), typeof(ICommand), typeof(RuleEditorControl), new PropertyMetadata(default(ICommand)));
        public ICommand SaveRuleCommand
        {
            get => (ICommand)GetValue(SaveRuleCommandProperty);
            set => SetValue(SaveRuleCommandProperty, value);
        }

        public static readonly DependencyProperty SaveRuleCommandProperty = DependencyProperty.Register(nameof(SaveRuleCommand), typeof(ICommand), typeof(RuleEditorControl), new PropertyMetadata(default(ICommand)));
        public bool SaveButtonEnabledState
        {
            get => (bool)GetValue(SaveButtonEnabledStateProperty);
            set => SetValue(SaveButtonEnabledStateProperty, value);
        }

        public static readonly DependencyProperty SaveButtonEnabledStateProperty = DependencyProperty.Register(nameof(SaveButtonEnabledState), typeof(bool), typeof(RuleEditorControl), new PropertyMetadata(default(bool)));
        public bool SaveButtonLoadingState
        {
            get => (bool)GetValue(SaveButtonLoadingStateProperty);
            set => SetValue(SaveButtonLoadingStateProperty, value);
        }

        public static readonly DependencyProperty SaveButtonLoadingStateProperty = DependencyProperty.Register(nameof(SaveButtonLoadingState), typeof(bool), typeof(RuleEditorControl), new PropertyMetadata(default(bool)));

        public bool ClearFieldsAfterSave
        {
            get => (bool)GetValue(ClearFieldsAfterSaveProperty);
            set => SetValue(ClearFieldsAfterSaveProperty, value);
        }

        public static readonly DependencyProperty ClearFieldsAfterSaveProperty = DependencyProperty.Register(nameof(ClearFieldsAfterSave), typeof(bool), typeof(RuleEditorControl), new PropertyMetadata(default(bool)));

        public RuleEditorControl()
        {
            InitializeComponent();
        }

        private void AddFieldButtonClick(object sender, RoutedEventArgs e)
        {
            Fields.Add(new Field(Fields.Count.ToString()));
        }

        private void DeleteClassFieldClick(object sender, RoutedEventArgs e)
        {
            var index = int.Parse((sender as Button).Tag as string);
            Fields.RemoveAt(index);
            foreach (Field f in Fields)
            {
                f.Position = Fields.IndexOf(f).ToString();
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            SaveRuleCommand.Execute(null);

            if (!ClearFieldsAfterSave) return;

            RuleNameTextBox.Text = null;
            RuleUrlTextBox.Text = null;
            RulePaginationSuffixTextBox.Text = null;
            RulePagesCountClassTextBox.Text = null;
        }
    }
}
