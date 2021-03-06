using ControlLib.RandomlyPlacedControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSimpleControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<RandomlyPlacedControlBase> randomlyPlacedControlBases = new ObservableCollection<RandomlyPlacedControlBase>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            randomlyPlacedControlBases.Add(new RPCTextBlock() { PanelZIndex = 0 });
            randomlyPlacedControlBases.Add(new RPCImage() { ImagePath = @"D:\\1.png", PanelZIndex = 1 });
            rpc.ItemsSources = randomlyPlacedControlBases;

            for(int i =0; i < 100; i++)
            {
                rtb.AppendText(string.Format("{0}\r\n", i));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
