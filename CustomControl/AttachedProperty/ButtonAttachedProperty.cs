using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CustomControl
{
    public class ButtonAttachedProperty
    {
        #region PressedBackground
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.RegisterAttached("PressedBackground", typeof(Brush), typeof(ButtonAttachedProperty), new PropertyMetadata(Brushes.Transparent));

        public static Brush GetPressedBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(PressedBackgroundProperty);
        }

        public static void SetPressedBackground(DependencyObject d, Brush value)
        {
            d.SetValue(PressedBackgroundProperty, value);
        }
        #endregion

        #region PressedBorderBrush
        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.RegisterAttached("PressedBorderBrush", typeof(Brush), typeof(ButtonAttachedProperty), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetPressedBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(PressedBorderBrushProperty);
        }

        public static void SetPressedBorderBrush(DependencyObject d, Brush value)
        {
            d.SetValue(PressedBorderBrushProperty, value);
        }
        #endregion

        #region MouseOverBackground
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(ButtonAttachedProperty), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetMouseOverBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(MouseOverBackgroundProperty);
        }

        public static void SetMouseOverBackground(DependencyObject d, Brush value)
        {
            d.SetValue(MouseOverBackgroundProperty, value);
        }
        #endregion

        #region MouseOverBorderBrush
        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(ButtonAttachedProperty), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetMouseOverBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(MouseOverBorderBrushProperty);
        }

        public static void SetMouseOverBorderBrush(DependencyObject d, Brush value)
        {
            d.SetValue(MouseOverBorderBrushProperty, value);
        }
        #endregion
    }
}
