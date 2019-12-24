using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ChartComponent.xaml
    /// </summary>
    public partial class ChartComponent : UserControl
    {
        public ChartComponent(double Height, string HexColor, string LabelName, string LabelValue)
        {
            InitializeComponent();

            Storyboard storyboard = this.FindResource("start_height") as Storyboard;
            (storyboard.Children[0] as DoubleAnimation).To = Height;

            this.Resources["color"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(HexColor));

            txb_label.Text = LabelName;
            txb_value.Text = LabelValue;

            BeginStoryboard(storyboard);
        }
    }
}
