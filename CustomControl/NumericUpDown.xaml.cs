using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControl
{
    public class NumericUpDown : Control
    {
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
            FocusableProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(true));
        }

        #region Property
        private bool IsTextFocuse = false;

        #region Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(decimal.Parse("0"), new PropertyChangedCallback(OnValueChanged)));

        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            NumericUpDown numericUpDown = (NumericUpDown)d;
            if (numericUpDown.ValueString != e.NewValue.ToString()
                && numericUpDown.ValueString != string.Format("{0}.", e.NewValue.ToString()))
            {
                numericUpDown.ValueString = e.NewValue.ToString();
            }
        }
        #endregion

        #region ValueString
        public static readonly DependencyProperty ValueStringProperty =
            DependencyProperty.Register("ValueString", typeof(string), typeof(NumericUpDown),
                new FrameworkPropertyMetadata("0", new PropertyChangedCallback(OnValueStringChanged), new CoerceValueCallback(CoerceValueString)));

        public string ValueString
        {
            get { return (string)GetValue(ValueStringProperty); }
            set { SetValue(ValueStringProperty, value); }
        }

        private static void OnValueStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            NumericUpDown numericUpDown = (NumericUpDown)d;
            decimal.TryParse(e.NewValue.ToString(), out decimal value);
            if (value != numericUpDown.Value)
            {
                numericUpDown.Value = value;
            }
        }

        private static object CoerceValueString(DependencyObject d, object value)
        {
            if (d == null)
            {
                return value;
            }

            NumericUpDown numericUpDown = (NumericUpDown)d;
            string strValue = value.ToString();
            decimal.TryParse(strValue, out decimal newValue);
            newValue = Math.Round(newValue, numericUpDown.DecimalPlaces);

            string ret = Math.Min(numericUpDown.Maximum, Math.Max(newValue, numericUpDown.Minimum)).ToString();
            if (numericUpDown.IsTextFocuse
                && strValue.IndexOf(".") > 0
                && numericUpDown.DecimalPlaces > 0
                && ret.IndexOf(".") < 0)
            {
                ret = string.Format("{0}.", ret);
            }

            return ret;
        }
        #endregion

        #region Maximum
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(decimal), typeof(NumericUpDown),
                new PropertyMetadata(decimal.MaxValue, new PropertyChangedCallback(OnMaximumChanged)));

        public decimal Maximum
        {
            get { return (decimal)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            NumericUpDown numericUpDown = (NumericUpDown)d;
            numericUpDown.CoerceValue(MinimumProperty);
            numericUpDown.CoerceValue(ValueStringProperty);
        }
        #endregion

        #region Minimum
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(decimal), typeof(NumericUpDown),
                new PropertyMetadata(decimal.MinValue, new PropertyChangedCallback(OnMinimumChanged)));

        public decimal Minimum
        {
            get { return (decimal)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            NumericUpDown numericUpDown = (NumericUpDown)d;
            numericUpDown.CoerceValue(ValueStringProperty);
        }
        #endregion

        #region DecimalPlaces
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(NumericUpDown),
                new PropertyMetadata((int)0, new PropertyChangedCallback(OnDecimalPlacesChanged), new CoerceValueCallback(CoerceDecimalPlaces)));

        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        private static void OnDecimalPlacesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            NumericUpDown numericUpDown = (NumericUpDown)d;
            numericUpDown.CoerceValue(ValueStringProperty);
        }

        private static object CoerceDecimalPlaces(DependencyObject d, object value)
        {
            if (d == null)
            {
                return value;
            }

            int nValue = (int)value;
            return Math.Max(nValue, 0);
        }
        #endregion

        #region ChangeValue
        public static readonly DependencyProperty ChangeValueProperty =
            DependencyProperty.Register("ChangeValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(decimal.Parse("1")));

        public decimal ChangeValue
        {
            get { return (decimal)GetValue(ChangeValueProperty); }
            set { SetValue(ChangeValueProperty, value); }
        }
        #endregion

        #region UpDownButtonVisibility
        public static readonly DependencyProperty UpDownButtonVisibilityProperty =
            DependencyProperty.Register("UpDownButtonVisibility", typeof(Visibility), typeof(NumericUpDown), new PropertyMetadata(Visibility.Visible));

        public Visibility UpDownButtonVisibility
        {
            get { return (Visibility)GetValue(UpDownButtonVisibilityProperty); }
            set { SetValue(UpDownButtonVisibilityProperty, value); }
        }
        #endregion

        #region UpDownButtonSize
        public static readonly DependencyProperty UpDownButtonSizeProperty =
            DependencyProperty.Register("UpDownButtonSize", typeof(double), typeof(NumericUpDown), new PropertyMetadata(8d));

        public double UpDownButtonSize
        {
            get { return (double)GetValue(UpDownButtonSizeProperty); }
            set { SetValue(UpDownButtonSizeProperty, value); }
        }
        #endregion

        #region UpDownButtonBrush
        public static readonly DependencyProperty UpDownButtonBrushProperty =
            DependencyProperty.Register("UpDownButtonBrush", typeof(Brush), typeof(NumericUpDown), new PropertyMetadata(Brushes.Black));

        public Brush UpDownButtonBrush
        {
            get { return (Brush)GetValue(UpDownButtonBrushProperty); }
            set { SetValue(UpDownButtonBrushProperty, value); }
        }
        #endregion
        #endregion

        #region Command
        public ICommand UpCommand { get; private set; }
        public ICommand DownCommand { get; private set; }
        #endregion

        public NumericUpDown()
        {
            UpCommand = new RoutedCommand();
            CommandBinding commandBinding = new CommandBinding(UpCommand, ExecutedUpRoutedEvent);
            this.CommandBindings.Add(commandBinding);

            DownCommand = new RoutedCommand();
            CommandBinding downCommandBinding = new CommandBinding(DownCommand, ExecutedDownRoutedEvent);
            this.CommandBindings.Add(downCommandBinding);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            TextBox text = this.Template.FindName("text", this) as TextBox;

            if (text != null)
            {
                text.GotFocus -= Text_GotFocus;
                text.GotFocus += Text_GotFocus;
                text.LostFocus -= Text_LostFocus;
                text.LostFocus += Text_LostFocus;
            }
        }

        #region  Text_GotFocus|Text_LostFocus
        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            IsTextFocuse = true;
        }

        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            IsTextFocuse = false;
        }
        #endregion

        #region Up
        private void ExecutedUpRoutedEvent(object sender, ExecutedRoutedEventArgs e)
        {
            this.Value += ChangeValue;
        }
        #endregion

        #region Down
        private void ExecutedDownRoutedEvent(object sender, ExecutedRoutedEventArgs e)
        {
            this.Value -= ChangeValue;
        }
        #endregion
    }
}
