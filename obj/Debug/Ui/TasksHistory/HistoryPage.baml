   M S B A M L   `   `   `  ����K  Ghtml_extractor_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:html_exctractor.Ui.TasksHistoryhtml_exctractor.Ui.TasksHistory  5      =#clr-namespace:html_exctractor.Modelhtml_exctractor.Model  5      1    +html_exctractor.Ui.TasksHistory.HistoryPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:html_exctractor.Ui.TasksHistory     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      /model#clr-namespace:html_exctractor.Model  5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5	         main_first_color  N� 
Background#  �   5       N� Title$ HistoryPage��5      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
      	EventName$ Loaded��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      4  .Microsoft.Xaml.Behaviors.Core.CallMethodAction     
MethodName$ 
PageLoaded��5   !      TargetObject 5   !   �� 5   !   5       � DataContext 5      6   0html_exctractor.Ui.TasksHistory.HistoryViewModel  5   
   .��5      � .+�5   
   �� $��1��6   $��Right=�5        views_border_color#���  5      $
��0 20q�5      �� 5    
    �� Orientation$ Vertical=�5!      $��Center=�5"      $��Center=�5#      y�5       �� *IsHistoriesEmpty+   BooleanToVisibilityConverter	 �� 	Converter#	 [ .+�5$      �� 
 �� CornerRadius$
 20��6   $	��180��5%      $	��180��5&       $  empty_rules_container_background#���  5'      ._�5(      �� $	��100��6   $	��100��5)      $)��#/Resources/Images/empty_history.png��5*      5+      �� 5,      $
��0 20q�6   $��16(�5-        text_main_color#���  5.        MaterialDesignFont#��[ 5/        empty_history_message#���  50      51      �� 52   
   $��Disabled=�6     HistoryListBoxItemStyle#��[ 53        GridTemplateContainer#��[ 54      
������ 
������ ��55      �� *	Histories+57      �� 58   
    &	 "MaterialDesignFloatingActionButton#��[	 59       
 main_second_color#��� 
 5:      
������ $��24q�5=      $��Bottom=�5>      $��Right=�5?       � ToolTip$ Clear History��5@       � Cursor$
 Hand��5A      ��58      0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ ClearBox=�   Size$ 20X���5;      �� *ClearHistoryCommand+5A      5B      5C      