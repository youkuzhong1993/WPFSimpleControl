using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomControl
{
    public class Singleton<T> where T : new()
    {
        private static T item;
        public static T GetInstance()
        {
            if (item == null)
            {
                item = new T();
            }

            return item;
        }
    }

    public sealed class XConverter
    {
        public static FilePathToBitmapImage FilePathToBitmapImage
        {
            get { return Singleton<FilePathToBitmapImage>.GetInstance(); }
        }

        public static IsFirstItemInItemsControl IsFirstItemInItemsControl
        {
            get { return Singleton<IsFirstItemInItemsControl>.GetInstance(); }
        }

        public static StringIsNullOrEmptyConverter StringIsNullOrEmptyConverter
        {
            get { return Singleton<StringIsNullOrEmptyConverter>.GetInstance(); }
        }        
    }
}
