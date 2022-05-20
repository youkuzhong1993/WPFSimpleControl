using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomControl
{
    public class IsFirstItemInItemsControl : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null
                || !(value is DependencyObject))
            {
                return false;
            }

            DependencyObject d = value as DependencyObject;
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(d);
            if(itemsControl == null)
            {
                return false;
            }

            return itemsControl.Items.IndexOf(d.GetValue(Control.DataContextProperty)) == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
