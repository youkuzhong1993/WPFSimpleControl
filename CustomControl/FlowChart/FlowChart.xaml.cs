using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CustomControl
{
    public class FlowChart : Control
    {
        static FlowChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlowChart), new FrameworkPropertyMetadata(typeof(FlowChart)));
            FocusableProperty.OverrideMetadata(typeof(FlowChart), new FrameworkPropertyMetadata(true));
        }

        #region Property
        #region NodeWidth
        public static readonly DependencyProperty NodeWidthProperty =
            DependencyProperty.Register("NodeWidth", typeof(double), typeof(FlowChart), new PropertyMetadata(60d));

        public double NodeWidth
        {
            get { return (double)GetValue(NodeWidthProperty); }
            set { SetValue(NodeWidthProperty, value); }
        }
        #endregion

        #region NodeHeight
        public static readonly DependencyProperty NodeHeightProperty =
            DependencyProperty.Register("NodeHeight", typeof(double), typeof(FlowChart), new PropertyMetadata(24d));

        public double NodeHeight
        {
            get { return (double)GetValue(NodeHeightProperty); }
            set { SetValue(NodeHeightProperty, value); }
        }
        #endregion

        #region StartNode
        public static readonly DependencyProperty StartNodeProperty =
            DependencyProperty.Register("StartNode", typeof(object), typeof(FlowChart), new PropertyMetadata(null, OnStartNodeChanged));

        public object StartNode
        {
            get { return GetValue(StartNodeProperty); }
            set { SetValue(StartNodeProperty, value); }
        }

        private static void OnStartNodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d == null)
            {
                return;
            }

            (d as FlowChart).Refresh();
        }
        #endregion
        #endregion

        #region Refresh
        DispatcherOperation refreshDispatcherOpreation = null;
        private void Refresh()
        {
            refreshDispatcherOpreation?.Abort();
            refreshDispatcherOpreation = Dispatcher.BeginInvoke(new Action(() =>
            {

            }), DispatcherPriority.ApplicationIdle);
        }
        #endregion
    }
}
