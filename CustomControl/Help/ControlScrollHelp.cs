using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControl
{
    public static class ControlScrollHelp
    {
        private static ScrollViewer selectedScrol = null;
        private static bool IsMouseDown = false;
        private static Point MouseDownPoint = new Point(0, 0);

        #region LeftMouseButtonScrollEnabled（给样式里滚动条嵌内容的控件增加左键滑动滚动条,不想通过点击滚动条时使用）
        public static readonly DependencyProperty LeftMouseButtonScrollEnabledProperty =
            DependencyProperty.RegisterAttached("LeftMouseButtonScrollEnabled", typeof(bool), typeof(ControlScrollHelp),
                new FrameworkPropertyMetadata(false, OnLeftMouseButtonScrollEnabledChanged));

        public static bool GetLeftMouseButtonScrollEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(LeftMouseButtonScrollEnabledProperty);
        }

        public static void SetLeftMouseButtonScrollEnabled(DependencyObject d, bool value)
        {
            d.SetValue(LeftMouseButtonScrollEnabledProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(RichTextBox))]
        [AttachedPropertyBrowsableForType(typeof(ListView))]
        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        private static void OnLeftMouseButtonScrollEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            if ((bool)e.OldValue)
            {
                Control control = d as Control;
                control.PreviewMouseLeftButtonDown -= Control_PreviewMouseLeftButtonDown;
                control.PreviewMouseMove -= Control_PreviewMouseMove;
                control.PreviewMouseLeftButtonUp -= Control_PreviewMouseLeftButtonUp;
            }

            if ((bool)e.NewValue)
            {
                Control control = d as Control;
                control.PreviewMouseLeftButtonDown += Control_PreviewMouseLeftButtonDown;
                control.PreviewMouseMove += Control_PreviewMouseMove;
                control.PreviewMouseLeftButtonUp += Control_PreviewMouseLeftButtonUp;
            }
        }
        #endregion

        private static ScrollViewer GetChildScrollViewer(DependencyObject d)
        {
            if (d == null)
            {
                return null;
            }

            if (d is ScrollViewer)
            {
                return d as ScrollViewer;
            }

            return GetChildScrollViewer(VisualTreeHelper.GetChild(d, 0));
        }

        private static void Control_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ScrollViewer scrollViewer = GetChildScrollViewer(sender as DependencyObject);
            if (scrollViewer == null)
            {
                return;
            }

            selectedScrol = scrollViewer;
            MouseDownPoint = e.GetPosition(sender as Control);
            IsMouseDown = true;
        }

        private static void Control_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton != System.Windows.Input.MouseButtonState.Pressed)
            {
                IsMouseDown = false;
                selectedScrol = null;
                return;
            }

            ScrollViewer scrollViewer = GetChildScrollViewer(sender as DependencyObject);
            if (scrollViewer == null
                || selectedScrol != scrollViewer)
            {
                IsMouseDown = false;
                selectedScrol = null;
                return;
            }

            if (IsMouseDown)
            {
                Point point = e.GetPosition(sender as Control);
                double x = point.X - MouseDownPoint.X;
                double y = point.Y - MouseDownPoint.Y;
                selectedScrol.ScrollToHorizontalOffset(selectedScrol.HorizontalOffset - x);
                selectedScrol.ScrollToVerticalOffset(selectedScrol.VerticalOffset - y);

                MouseDownPoint = point;
            }
        }

        private static void Control_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsMouseDown = false;
            selectedScrol = null;
        }
    }
}
