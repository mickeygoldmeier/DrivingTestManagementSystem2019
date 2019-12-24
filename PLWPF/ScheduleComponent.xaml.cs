using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ScheduleComponent.xaml
    /// </summary>
    public partial class ScheduleComponent : UserControl
    {
        private BL.IBL iBL_Imp = BL.FactoryBL.getBL();

        public ScheduleComponent(BE.Test test)
        {
            InitializeComponent();

            // If no test is available, display a blank element
            if (!(test is BE.Test))
            {
                sp.Children.Clear();
                return;
            }

            // Fill in the address of the test and set the background
            card.DataContext = test;
            card.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E5EE91"));

            // Determine the student's name and gear type
            try
            {
                BE.Trainee trainee = iBL_Imp.SearchTrainee(test.Traniee_id)[0];
                txb_name.Text = trainee.First_name + " " + trainee.Last_name;
                txb_gear_type.DataContext = trainee;
            }
            catch
            {
                sp.Children.Clear();
                sp.Children.Add(new MaterialDesignThemes.Wpf.PackIcon() { Kind = MaterialDesignThemes.Wpf.PackIconKind.AlertDecagram, Height = 20, Width = 20, HorizontalAlignment = HorizontalAlignment.Center });
                sp.Children.Add(new TextBlock() { Text = "התלמיד נמחק מהמערכת", FontSize = 10, HorizontalAlignment = HorizontalAlignment.Center });
                sp.Children.Add(new TextBlock() { Text = "ראה מבחן זה כמבוטל", FontSize = 8, HorizontalAlignment = HorizontalAlignment.Center });
                sp.VerticalAlignment = VerticalAlignment.Center;
                card.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF9F9F"));
            }
        }
    }
}
