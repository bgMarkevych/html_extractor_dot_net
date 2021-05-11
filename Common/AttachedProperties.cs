using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace html_exctractor.Common
{
    public class ListBoxAssist
    {
        public static readonly DependencyProperty ListBoxItemClickCommandProperty = DependencyProperty.RegisterAttached("ListBoxItemClickCommand", typeof(RelayCommand), typeof(ListBoxAssist), new PropertyMetadata(default(RelayCommand), new PropertyChangedCallback(OnCommandChanged)));

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = d as ListBoxItem;
            item.PreviewMouseDoubleClick -= Item_PreviewMouseDoubleClick;
            item.PreviewMouseDoubleClick += Item_PreviewMouseDoubleClick;
        }

        private static void Item_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (GetListBoxItemClickCommand(sender as DependencyObject) as RelayCommand).Execute((sender as FrameworkElement).DataContext);
        }

        public static void SetListBoxItemClickCommand(DependencyObject d, object value)
        {
            d.SetValue(ListBoxItemClickCommandProperty, value);
        }

        public static object GetListBoxItemClickCommand(DependencyObject d)
        {
            return d.GetValue(ListBoxItemClickCommandProperty);
        }
    }

    public static class ContextMenuLeftClickBehavior
    {
        public static bool GetIsLeftClickEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLeftClickEnabledProperty);
        }

        public static void SetIsLeftClickEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLeftClickEnabledProperty, value);
        }

        public static readonly DependencyProperty IsLeftClickEnabledProperty = DependencyProperty.RegisterAttached(
            "IsLeftClickEnabled",
            typeof(bool),
            typeof(ContextMenuLeftClickBehavior),
            new UIPropertyMetadata(false, OnIsLeftClickEnabledChanged));

        private static void OnIsLeftClickEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = sender as UIElement;

            if (uiElement != null)
            {
                bool IsEnabled = e.NewValue is bool && (bool)e.NewValue;

                if (IsEnabled)
                {
                    if (uiElement is ButtonBase)
                        ((ButtonBase)uiElement).Click += OnMouseLeftButtonUp;
                    else
                        uiElement.MouseLeftButtonUp += OnMouseLeftButtonUp;
                }
                else
                {
                    if (uiElement is ButtonBase)
                        ((ButtonBase)uiElement).Click -= OnMouseLeftButtonUp;
                    else
                        uiElement.MouseLeftButtonUp -= OnMouseLeftButtonUp;
                }
            }
        }

        private static void OnMouseLeftButtonUp(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            if (fe != null)
            {
                fe.ContextMenu.IsOpen = true;
            }
        }

    }

}