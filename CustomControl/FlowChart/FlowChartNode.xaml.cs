using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace CustomControl
{
    public class FlowChartNode : Thumb
    {
        static FlowChartNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlowChartNode), new FrameworkPropertyMetadata(typeof(FlowChartNode)));
            FocusableProperty.OverrideMetadata(typeof(FlowChartNode), new FrameworkPropertyMetadata(true));
        }

        #region Property
        #region CornerRadius
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlowChartNode), new PropertyMetadata(new CornerRadius(0)));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region FIcon
        public static readonly DependencyProperty FIconProperty =
            DependencyProperty.Register("FIcon", typeof(string), typeof(FlowChartNode), new PropertyMetadata(""));

        public string FIcon
        {
            get { return (string)GetValue(FIconProperty); }
            set { SetValue(FIconProperty, value); }
        }
        #endregion

        #region Text
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FlowChartNode), new PropertyMetadata(""));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion
        #endregion
    }
}
