   M S B A M L   `   `   `  ����K  Ghtml_extractor_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:html_exctractor.Ui.Menuhtml_exctractor.Ui.Menu  5      R NLottieSharp, Version=1.1.3.0, Culture=neutral, PublicKeyToken=25f75b656488b9c2>.clr-namespace:LottieSharp;assembly=LottieSharpLottieSharp 5	      ?$clr-namespace:html_exctractor.Commonhtml_exctractor.Common  5
      &     html_exctractor.Ui.Menu.MenuPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      1local%clr-namespace:html_exctractor.Ui.Menu  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      ;lottie.clr-namespace:LottieSharp;assembly=LottieSharp  5	      1common$clr-namespace:html_exctractor.Common  5
         menu_color_background  N� 
Background#  �   5       N� Title$ MenuPage��5      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
      	EventName$ Loaded��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      4  .Microsoft.Xaml.Behaviors.Core.CallMethodAction     
MethodName$ 
PageLoaded��5   !      TargetObject 5   !   �� 5   !   5       � DataContext 5      +   %html_exctractor.Ui.Menu.MenuViewModel  5   
    � 	Resources 5      )   #html_exctractor.Common.BindingProxy 	 Proxy	   Data��%*   &	         5   
   	 5      �� 5      .��5!      � -   5        
 �Name$
 
LayoutRoot��5!       � ColumnDefinitions 5"   
   �� 5#      $	��100 �6   �� 5$      $��* �6    � RowDefinitions 5&   
   �� 5'      $��60 �6   �� 5(      $��* �6   �� 5)      $
��auto �6   .+�5,   
   �� $��Medium"�5-      $
��16 0q�5.      $��Center=�5/      $��1��50        text_main_color#���  51      $��Right=�52      ��5,      �� *Email+52      �� 53   
   $	��Top=�54       � IsSharedSizeScope . $��10q�56      $��1��57       �� SelectedIndex 53      �� *SelectedMenuIndex+.@�58      �� ��6   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$
 Home=��� 59      ��6     $ History=��� 5:      ��6     $ Build=�5;      �� 5<   
   $��2��6   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth0=�5=      
������ $��50��5?      $��50��5@      $��5 0 5 50q�5A        main_second_color+	  %MaterialDesignThemes.Wpf.RippleAssist 	  Feedback# �  5B       ) %menu_item_unselected_color_background#���  5C       + 'MaterialDesignFloatingActionLightButton#��[ 5D       # menu_icon_unselected_tint_color#���  5E      9
   3html_exctractor.Common.ContextMenuLeftClickBehavior 
  IsLeftClickEnabled .  � Cursor$
 Hand��5H      ��5F        $ Settings=�   Size$ 22X� � ContextMenu 5I      �� 5J        DefaultContextMenu#��[ 6   $��20��5K      ���#fff.@�5M      m�  m� Icon 6     $ ThemeLightDark=���5N      d� 5O        chip_background_color#���  5P   +    d� 	IsChecked 5O   +   ��  �� Source# [  �� Path$ Data.DarkTheme�5P   +   5R      m� 5S       6     $ Language=���5T      �� 5U      $��1��6'    5V   '   �� # [ $$ Data.SelectedLocalizationIndex�.@�5W   "   �� $	��USA��6/   �� 5X   "   $��UA��6/   �� 5Y   "   $��RU��6/   5Z      5\      m� 5]       m� Command 6   �� # [ $ Data.LogoutCommand� 6j     $ Logout=�5^      5`      )  #MaterialDesignThemes.Wpf.DialogHost  5b   
   -   5        $��loader_host��5b   $      OverlayBackground
 ���� $��1��5d   $   $��1��5e   $   $��2��5f   $      DialogContent 5g      %  LottieSharp.LottieAnimationView  5h      $	��100��6-   $	��100��5i   -      DefaultCacheStrategy$
 Weak=�5j   -   $��Center=�5k   -      Speed$	 1.5Q�5l   -   $��Center=�5m   -   
   Url$? 9https://assets2.lottiefiles.com/packages/lf20_uwR49r.json��5n   -       AutoPlay  . 5o   -   .��5q      � .+�5r       � -   5        $��	Container��5t       	 main_first_color#��� 	 5r      $��1��5s      5t      5u      5v      5x      5z      