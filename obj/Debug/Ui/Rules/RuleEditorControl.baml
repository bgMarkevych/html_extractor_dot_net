   M S B A M L   `   `   `  ����K  Ghtml_extractor_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:html_exctractor.Ui.Ruleshtml_exctractor.Ui.Rules  5      0    *html_exctractor.Ui.Rules.RuleEditorControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:html_exctractor.Ui.Rules     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      -   5        $
��root��5
         main_first_color#���   5	      y�5      �� *GlobalVisibility+  �� ElementName$
  root��.��5      � .+�5   
   �� $��0��6   $��Hidden=�5      .��5      ��  �� Orientation$ Vertical=�6   $
��0 90q�5      ��5      �� *InnerSectionWidth+$
  root��.+�5      �� $��30(�5      $
��Wrap=�5        text_main_color#���  5        MaterialDesignFont#���  5      $��Medium"�5      ��5      �� *BuilderTitle+$
  root��5      �� 5      -   5        $��RuleNameTextBox��5      $	��0 4q�5        rule_builder_name_hint)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  5        RuleBuilderTextBox#��[ 5        � Cursor$ IBeam��5!      ��5      �� *
RuleName+$
  root��.��5!   )   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5"      +  %Microsoft.Xaml.Behaviors.EventTrigger  5#         	EventName$ TextChanged��6)   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5$      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter 64   ��  �� RelativeSource �� 	 �� AncestorType$	 TextBoxL�
   Command
 5%   4   �� *RuleFieldsTextChangedCommand+$
  root��5&      .��5'   .   5(      �� 5)      -   5        $��RuleUrlTextBox��5*      $	��0 4q�5)        rule_builder_url_hint# �  5,      #��[ 5-      $ IBeam��5.      ��5+      �� *	RuleUrl+$
  root��.��5.   )    5/        50      $ TextChanged��6)   . 51         64   ��  �� $	 TextBoxL�
 52   4   �� *RuleFieldsTextChangedCommand+$
  root��53      .��54   .   55      �� 56      -   5        $��paginationCheckBox��56        MaterialDesignCheckBox#��[ 57      $��0 16 0 0q�58        main_second_color#���  59      #���  5:      #���  5;         rule_builder_pagination_hint#���  5<      �� 5=      -   5        $!��RulePaginationSuffixTextBox��5>      $	��0 4q�5=       (	 $rule_builder_pagination_suffics_hint# � 	 5A      #��[ 5B      ��5?      �� *RulePaginationSuffix+$
  root��{�5@      �� $  paginationCheckBox�� �� Path$ 	IsChecked�.��5B   F    5C        5D      $ TextChanged��6)   . 5E         64   ��  �� $	 TextBoxL�
 5F   4   �� *RuleFieldsTextChangedCommand+$
  root��5G      .��5H   .   5I      �� 5J      -   5        $ ��RulePagesCountClassTextBox��5K      $	��0 4q�5J       '
 #rule_builder_pages_count_field_hint# � 
 5M      #��[ 5N      $ IBeam��5O      ��5L      �� *RulePagesCountClass+$
  root��.��5O   )    5P        5Q      $ TextChanged��6)   . 5R         64   ��  �� $	 TextBoxL�
 5S   4   �� *RuleFieldsTextChangedCommand+$
  root��5T      .��5U   .   5V      �� 5W      -   5         & "rule_builder_add_field_button_text#���  5W      $
��0 10q�5X      $��25��5Y      $��10(�5Z      $��Center=�5\      $��Right=�5]        chip_background_color#���  5^      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 5��5_      �� 5`      -   5        $��fields��5`      $��Disabled=�5b      $��Disabled=�5c       �� SelectionMode$ Single=�5d      
������ 
������ |�.  ��5a      �� *Fields+��5h      �� 5i      .R�5j      �� $ 
Horizontal=�6)   .+�5k   "   �� -	   5        $��0 0 10 0q�5k   )    & "MaterialDesignFloatingActionButton#��[ 5l   )   $��16��5m   )   $��16��5n   )   #���  5p   )   $��Center=�5q   )   
 � Tag 5o   )   �� *
Position+��5s   )   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ Close=�   Size$ 10X��� 5t   "   $	��0 4q�6*   $	��200��5v   *   $��Right=�5w   *    " rule_builder_class_field_title# �  5y   *      
IsFloating .  #��[ 5{   *   $ IBeam��5|   *    5u   *   �� *
Position+��5x   *   �� *Value+.��5|   9    5}   &     5~   *   $ TextChanged��69   . 5   .      6D   ��  �� $	 TextBoxL�
 5�   D   �� *CssFieldsTextChangedCommand+$
  root��5�   +   .��5�   >   5�   #   5�      5�      ��5�      �� 5�       �� 
TargetType" ��6    .�5�      ��  �� Property � �� �� Value 5�   "   �� 5�   &    �� 
TargetType" ��66   .R�5�   *   �� 5�   '   5�      5�      5�      �� 5�      -
   5          save#���  5�        save_button_text_color#���  5�      3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�5�      $
��0 30q�5�      $��40��5�         save_button_background_color#���  5�      $ 15��5�      $
 Hand��5�      {�5�      �� *SaveButtonEnabledState+$
  root��   IsIndicatorVisible 5�      �� *SaveButtonLoadingState+$
  root��   IsIndeterminate 5�      �� *SaveButtonLoadingState+$
  root��5�      5�      5�      5�      5�      