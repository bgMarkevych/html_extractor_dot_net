   M S B A M L   `   `   `  ���� K  Ghtml_extractor_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:html_exctractor.Ui.Ruleshtml_exctractor.Ui.Rules  0    *html_exctractor.Ui.Rules.RuleEditorControl   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:html_exctractor.Ui.Rules     b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  -   $
��root��   main_first_color#���   y��� *GlobalVisibility+  �� ElementName$
  root��.��� .+��� $��0��$��Hidden=�.����  �� Orientation$ Vertical=�$
��0 90q����� *InnerSectionWidth+$
  root��.+��� $��30(�$
��Wrap=�  text_main_color#���    MaterialDesignFont#���  $��Medium"����� *BuilderTitle+$
  root���� -   $��RuleNameTextBox��$	��0 4q�  rule_builder_name_hint)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �    RuleBuilderTextBox#��[  � Cursor$ IBeam������ *
RuleName+$
  root��.��*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ TextChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter ��  �� RelativeSource �� 	 �� AncestorType$	 TextBoxL�
   Command
 �� *RuleFieldsTextChangedCommand+$
  root��.���� -   $��RuleUrlTextBox��$	��0 4q�  rule_builder_url_hint# �  #��[ $ IBeam������ *	RuleUrl+$
  root��.��   $ TextChanged��.    ��  �� $	 TextBoxL�
 �� *RuleFieldsTextChangedCommand+$
  root��.���� -   $��paginationCheckBox��  MaterialDesignCheckBox#��[ $��0 16 0 0q�  main_second_color#���  #���  #���     rule_builder_pagination_hint#���  �� -   $!��RulePaginationSuffixTextBox��$	��0 4q� (	 $rule_builder_pagination_suffics_hint# � 	 #��[ ���� *RulePaginationSuffix+$
  root��{��� $  paginationCheckBox�� �� Path$ 	IsChecked�.��   $ TextChanged��.    ��  �� $	 TextBoxL�
 �� *RuleFieldsTextChangedCommand+$
  root��.���� -   $ ��RulePagesCountClassTextBox��$	��0 4q� '
 #rule_builder_pages_count_field_hint# � 
 #��[ $ IBeam������ *RulePagesCountClass+$
  root��.��   $ TextChanged��.    ��  �� $	 TextBoxL�
 �� *RuleFieldsTextChangedCommand+$
  root��.���� -    & "rule_builder_add_field_button_text#���  $
��0 10q�$��25��$��10(�$��Center=�$��Right=�  chip_background_color#���  +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 5���� -   $��fields��$��Disabled=�$��Disabled=� �� SelectionMode$ Single=�
������ 
������ |�.  ���� *Fields+���� .R��� $ 
Horizontal=�.+��� -	   $��0 0 10 0q� & "MaterialDesignFloatingActionButton#��[ $��16��$��16��#���  $��Center=�
 � Tag �� *
Position+��0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ Close=�   Size$ 10X��� $	��0 4q�$	��200��$��Right=� " rule_builder_class_field_title# �     
IsFloating .  #��[ $ IBeam�� �� *
Position+���� *Value+.��   $ TextChanged��.    ��  �� $	 TextBoxL�
 �� *CssFieldsTextChangedCommand+$
  root��.������  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R��� �� -
     save#���    save_button_text_color#���  3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�$
��0 30q�$��40��   save_button_background_color#���  $ 15��$
 Hand��{��� *SaveButtonEnabledState+$
  root��   IsIndicatorVisible �� *SaveButtonLoadingState+$
  root��   IsIndeterminate �� *SaveButtonLoadingState+$
  root��