using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControl
{
    public class ControlAttachedProperty
    {
        #region CornerRadius
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ControlAttachedProperty), new PropertyMetadata(new CornerRadius()));

        public static CornerRadius GetCornerRadius(DependencyObject d)
        {
            return (CornerRadius)d.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject d, CornerRadius value)
        {
            d.SetValue(CornerRadiusProperty, value);
        }
        #endregion

        #region AppendContent
        public static readonly DependencyProperty AppendContentProperty =
            DependencyProperty.RegisterAttached("AppendContent", typeof(object), typeof(ControlAttachedProperty), new FrameworkPropertyMetadata(null));

        public static object GetAppendContent(DependencyObject d)
        {
            return d.GetValue(AppendContentProperty);
        }

        public static void SetAppendContent(DependencyObject d, object value)
        {
            d.SetValue(AppendContentProperty, value);
        }
        #endregion

        #region OpenFileDialogEnabled
        public static readonly DependencyProperty OpenFileDialogEnabledProperty =
            DependencyProperty.RegisterAttached("OpenFileDialogEnabled", typeof(bool), typeof(ControlAttachedProperty), new PropertyMetadata(false, OnOpenFileDialogEnabledChanged));

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetOpenFileDialogEnabled(DependencyObject d)
        {
            return (bool)d.GetValue(OpenFileDialogEnabledProperty);
        }

        public static void SetOpenFileDialogEnabled(DependencyObject d, bool value)
        {
            d.SetValue(OpenFileDialogEnabledProperty, value);
        }

        private static void OnOpenFileDialogEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null
                || !(d is Button))
            {
                return;
            }

            Button button = d as Button;
            if ((bool)e.OldValue
                && button.CommandBindings.Contains(openFileCommandBinding))
            {
                button.CommandBindings.Remove(openFileCommandBinding);
            }

            if ((bool)e.NewValue)
            {
                button.CommandBindings.Add(openFileCommandBinding);
            }
        }
        #endregion

        #region OpenFileCommand
        public static RoutedUICommand openFileCommand { get; set; }
        private static readonly CommandBinding openFileCommandBinding;
        private static void OpentFileExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(e.Parameter is TextBox))
            {
                return;
            }

            var textBox = e.Parameter as TextBox;
            string filter = textBox.Tag == null ? "所有文件(*.*)|*.*" : textBox.Tag.ToString();
            if (!filter.Contains("所有文件(*.*)|*.*"))
            {
                filter = string.Format("{0}{1}", filter, "|所有文件(*.*)|*.*");
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择文件";
            openFileDialog.Filter = filter;
            openFileDialog.FileName = textBox.Text.Trim();
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = openFileDialog.FileName;
            }

            textBox.Focus();
            textBox.Select(textBox.Text.Length, 0);
        }
        #endregion

        #region FIcon
        public static readonly DependencyProperty FIconProperty =
            DependencyProperty.RegisterAttached("FIcon", typeof(string), typeof(ControlAttachedProperty), new FrameworkPropertyMetadata(""));

        public static string GetFIcon(DependencyObject d)
        {
            return (string)d.GetValue(FIconProperty);
        }

        public static void SetFIcon(DependencyObject d, string value)
        {
            d.SetValue(FIconProperty, value);
        }
        #endregion

        #region CheckedFIcon
        public static readonly DependencyProperty CheckedFIconProperty =
            DependencyProperty.RegisterAttached("CheckedFIcon", typeof(string), typeof(ControlAttachedProperty), new FrameworkPropertyMetadata(""));

        public static string GetCheckedFIcon(DependencyObject d)
        {
            return (string)d.GetValue(CheckedFIconProperty);
        }

        public static void SetCheckedFIcon(DependencyObject d, string value)
        {
            d.SetValue(CheckedFIconProperty, value);
        }
        #endregion

        #region CheckedForeground
        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.RegisterAttached("CheckedForeground", typeof(Brush), typeof(ControlAttachedProperty), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetCheckedForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(CheckedForegroundProperty);
        }

        public static void SetCheckedForeground(DependencyObject d, Brush value)
        {
            d.SetValue(CheckedForegroundProperty, value);
        }
        #endregion

        static ControlAttachedProperty()
        {
            openFileCommand = new RoutedUICommand();
            openFileCommandBinding = new CommandBinding(openFileCommand, OpentFileExecute);
        }
    }
}
