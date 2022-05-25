using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomControl
{
    public class TimePicker : Control
    {
        static TimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
            FocusableProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(true));
        }

        #region Property
        #region Value
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime), typeof(TimePicker), new PropertyMetadata(DateTime.Now, OnValueChanged));

        public DateTime Value
        {
            get { return (DateTime)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            TimePicker picker = (TimePicker)d;
            picker.Hour = picker.Value.Hour;
            picker.Minute = picker.Value.Minute;
            picker.Second = picker.Value.Second;
        }
        #endregion

        #region Hour
        public static readonly DependencyProperty HourProperty =
            DependencyProperty.Register("Hour", typeof(decimal), typeof(TimePicker), new PropertyMetadata(Convert.ToDecimal(DateTime.Now.Hour), OnHourChanged));

        public decimal Hour
        {
            get { return (decimal)GetValue(HourProperty); }
            set { SetValue(HourProperty, value); }
        }

        private static void OnHourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            TimePicker picker = (TimePicker)d;
            DateTime now = DateTime.Now;
            picker.Value = new DateTime(now.Year, now.Month, now.Day, (int)picker.Hour, (int)picker.Minute, (int)picker.Second);
        }
        #endregion

        #region Minute 
        public static readonly DependencyProperty MinuteProperty =
            DependencyProperty.Register("Minute", typeof(decimal), typeof(TimePicker), new PropertyMetadata(Convert.ToDecimal(DateTime.Now.Minute), OnMinuteChanged));

        public decimal Minute
        {
            get { return (decimal)GetValue(MinuteProperty); }
            set { SetValue(MinuteProperty, value); }
        }

        private static void OnMinuteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            TimePicker picker = (TimePicker)d;
            DateTime now = DateTime.Now;
            picker.Value = new DateTime(now.Year, now.Month, now.Day, (int)picker.Hour, (int)picker.Minute, (int)picker.Second);
        }
        #endregion

        #region Second
        public static readonly DependencyProperty SecondProperty =
            DependencyProperty.Register("Second", typeof(decimal), typeof(TimePicker), new PropertyMetadata(Convert.ToDecimal(DateTime.Now.Second), OnSecondChanged));

        public decimal Second
        {
            get { return (decimal)GetValue(SecondProperty); }
            set { SetValue(SecondProperty, value); }
        }

        private static void OnSecondChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
            {
                return;
            }

            TimePicker picker = (TimePicker)d;
            DateTime now = DateTime.Now;
            picker.Value = new DateTime(now.Year, now.Month, now.Day, (int)picker.Hour, (int)picker.Minute, (int)picker.Second);
        }
        #endregion
        #endregion
    }
}
