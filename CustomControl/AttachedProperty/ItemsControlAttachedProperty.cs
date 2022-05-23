using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControl
{
    public class ItemsControlAttachedProperty
    {
        #region ItemSelectedBrush
        public static readonly DependencyProperty ItemSelectedBrushProperty =
            DependencyProperty.RegisterAttached("ItemSelectedBrush", typeof(Brush), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetItemSelectedBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(ItemSelectedBrushProperty);
        }

        public static void SetItemSelectedBrush(DependencyObject d, Brush value)
        {
            d.SetValue(ItemSelectedBrushProperty, value);
        }
        #endregion

        #region FirstItemMargin
        public static readonly DependencyProperty FirstItemMarginProperty =
            DependencyProperty.RegisterAttached("FirstItemMargin", typeof(Thickness), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(new Thickness(0)));

        public static Thickness GetFirstItemMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(FirstItemMarginProperty);
        }

        public static void SetFirstItemMargin(DependencyObject d, Thickness value)
        {
            d.SetValue(FirstItemMarginProperty, value);
        }
        #endregion

        #region ItemMargin
        public static readonly DependencyProperty ItemMarginProperty =
            DependencyProperty.RegisterAttached("ItemMargin", typeof(Thickness), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(new Thickness(0)));

        public static Thickness GetItemMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(ItemMarginProperty);
        }

        public static void SetItemMargin(DependencyObject d, Thickness value)
        {
            d.SetValue(ItemMarginProperty, value);
        }
        #endregion

        #region ItemHeight
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.RegisterAttached("ItemHeight", typeof(double), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(double.NaN));

        public static double GetItemHeight(DependencyObject d)
        {
            return (double)d.GetValue(ItemHeightProperty);
        }

        public static void SetItemHeight(DependencyObject d, double value)
        {
            d.SetValue(ItemHeightProperty, value);
        }
        #endregion

        #region ItemWidth
        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.RegisterAttached("ItemWidth", typeof(double), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(double.NaN));

        public static double GetItemWidth(DependencyObject d)
        {
            return (double)d.GetValue(ItemWidthProperty);
        }

        public static void SetItemWidth(DependencyObject d, double value)
        {
            d.SetValue(ItemWidthProperty, value);
        }
        #endregion

        #region ItemBorderThickness
        public static readonly DependencyProperty ItemBorderThicknessProperty =
            DependencyProperty.RegisterAttached("ItemBorderThickness", typeof(Thickness), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(new Thickness(0)));

        public static Thickness GetItemBorderThickness(DependencyObject d)
        {
            return (Thickness)d.GetValue(ItemBorderThicknessProperty);
        }

        public static void SetItemBorderThickness(DependencyObject d, Thickness value)
        {
            d.SetValue(ItemBorderThicknessProperty, value);
        }
        #endregion

        #region ItemBorderBrush
        public static readonly DependencyProperty ItemBorderBrushProperty =
            DependencyProperty.RegisterAttached("ItemBorderBrush", typeof(Brush), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(Brushes.Transparent));

        public static Brush GetItemBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(ItemBorderBrushProperty);
        }

        public static void SetItemBorderBrush(DependencyObject d, Brush value)
        {
            d.SetValue(ItemBorderBrushProperty, value);
        }
        #endregion

        #region ItemBorderCornerRadius
        public static readonly DependencyProperty ItemBorderCornerRadiusProperty =
            DependencyProperty.RegisterAttached("ItemBorderCornerRadius", typeof(CornerRadius), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(new CornerRadius(0)));

        public static CornerRadius GetItemBorderCornerRadius(DependencyObject d)
        {
            return (CornerRadius)d.GetValue(ItemBorderCornerRadiusProperty);
        }

        public static void SetItemBorderCornerRadius(DependencyObject d, CornerRadius value)
        {
            d.SetValue(ItemBorderCornerRadiusProperty, value);
        }
        #endregion

        #region IsItemClickToSelected(当被点击即选)
        public static readonly DependencyProperty IsItemClickToSelectedProperty =
            DependencyProperty.RegisterAttached("IsItemClickToSelected", typeof(bool), typeof(ItemsControlAttachedProperty), new FrameworkPropertyMetadata(false, OnIsItemClickToSelectedChanged));

        public static bool GetIsItemClickToSelected(DependencyObject d)
        {
            return (bool)d.GetValue(IsItemClickToSelectedProperty);
        }

        public static void SetIsItemClickToSelected(DependencyObject d, bool value)
        {
            d.SetValue(IsItemClickToSelectedProperty, value);
        }

        private static void OnIsItemClickToSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d == null)
            {
                return;
            }

            if ((bool)e.OldValue)
            {
                (d as Control).PreviewMouseLeftButtonDown -= ItemsControlHelp_PreviewMouseLeftButtonDown;
            }

            if ((bool)e.NewValue)
            {
                (d as Control).PreviewMouseLeftButtonDown += ItemsControlHelp_PreviewMouseLeftButtonDown;
            }
        }
        #endregion

        #region GetItem
        private static DependencyObject GetItem(DependencyObject d)
        {
            if(d == null)
            {
                return null;
            }

            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(d); 
            if(itemsControl != null)
            {
                return d;
            }

            return GetItem(VisualTreeHelper.GetParent(d));
        }
        #endregion

        private static void ItemsControlHelp_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DependencyObject d = GetItem(e.OriginalSource as DependencyObject);
            if(d == null)
            {
                return;
            }

            if(d is ListBoxItem)
            {
                (d as ListBoxItem).IsSelected = true;
            }
            else if(d is DataGridRow)
            {
                (d as DataGridRow).IsSelected = true;
            }
        }
    }
}
