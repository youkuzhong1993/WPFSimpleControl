using ControlLib.RandomlyPlacedControl;
using System.Windows;
using System.Windows.Controls;

namespace CustomControl
{
    public class RandomlyPlacedControlContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBlockDataTemplate { get; set; }
        public DataTemplate ImageDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null
                || !(item is RandomlyPlacedControlBase))
            {
                return null;
            }

            if(item is RPCTextBlock)
            {
                return TextBlockDataTemplate;
            }
            else if(item is RPCImage)
            {
                return ImageDataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
