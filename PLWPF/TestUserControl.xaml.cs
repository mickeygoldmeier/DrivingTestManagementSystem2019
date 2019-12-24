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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TestUserControl.xaml
    /// </summary>
    public partial class TestUserControl : UserControl
    {
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();
        private BE.Test Test;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="test">The test itself you want to present</param>
        /// <param name="Enabl_editing">Do you want to allow editing of the test?</param>
        public TestUserControl(BE.Test test, bool Enabl_editing, bool Enable_printing = false)
        {
            InitializeComponent();

            Test = test;

            Fill_data();

            // Enable / Disable the edit button
            if (Enabl_editing)
                edit_button.Visibility = Visibility.Visible;

            if (Enable_printing)
                fee_buton.Visibility = Visibility.Visible;
        }

        private void Fill_data()
        {
            // Fill in the default values according to the trainee's data
            card.DataContext = Test;
            hour.Text = Test.Test_time.ToShortTimeString();
            date.Text = Test.Test_time.ToLongDateString();
            address.Text = Test.Address.ToString();
            switch (Test.Student_car_Type)
            {
                case BE.Car_type.Private_car:
                    car_type.Text = "רכב פרטי";
                    break;
                case BE.Car_type.Two_wheeled_vehicle:
                    car_type.Text = "רכב דו-גלגלי";
                    break;
                case BE.Car_type.Medium_truck:
                    car_type.Text = "משאית בינונית";
                    break;
                case BE.Car_type.Heavy_truck:
                    car_type.Text = "משאית כבדה";
                    break;
                default:
                    car_type.Text = "שגיאה לא הגיונית";
                    break;
            }

            // Fill in the required details if the test has already been completed
            if (Test.Criteria_list.Count != 0)
            {
                details.Visibility = Visibility.Visible;
                delete_button.Visibility = Visibility.Hidden;

                if (Test.Grade)
                {
                    test_state.Text = "עבר";
                    state_color.Background = Brushes.DarkCyan;
                    edit_button.Background = Brushes.DarkCyan;
                    edit_button.BorderBrush = Brushes.DarkCyan;
                }
                else
                {
                    test_state.Text = "נכשל";
                    state_color.Background = Brushes.DarkRed;
                    edit_button.Background = Brushes.DarkRed;
                    edit_button.BorderBrush = Brushes.DarkRed;
                }

                remarks.Selection.Text = Test.Tester_comment;

                foreach (BE.Criterion item in Test.Criteria_list)
                    criterion.AppendText(item.ToString() + "\r");
            }
        }

        private void ShowOptions(object sender, MouseEventArgs e)
        {
            OptionPanel.Visibility = Visibility.Visible;
        }

        private void HideOptions(object sender, MouseEventArgs e)
        {
            OptionPanel.Visibility = Visibility.Hidden;
        }

        private void DeleteTest(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק את המבחן הזה\nפעולה זו היא פעולה בלתי הפיכה", "מחיקת מבחן מספר " + Test.Test_number, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    iBL_Imp.Remove_test(Test);
                    this.Visibility = Visibility.Collapsed;
                    MessageBox.Show("המבחן נמחק");
                    break;
                default:
                    break;
            }
        }

        private void EditTest(object sender, RoutedEventArgs e)
        {
            UpdateTestWindow window = new UpdateTestWindow(Test);
            window.ShowDialog();

            Test = iBL_Imp.SearchTest(Test.Test_number)[0];
            Fill_data();
        }

        private void PrintFee(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the print dialog object and set options
                PrintDialog pDialog = new PrintDialog();
                pDialog.PageRangeSelection = PageRangeSelection.AllPages;
                pDialog.UserPageRangeEnabled = true;

                // Display the dialog. This returns true if the user presses the Print button.
                Nullable<Boolean> print = pDialog.ShowDialog();
                if (print == true)
                {
                    LicenseFee fee = new LicenseFee(Test) { Height = 640 };
                    pDialog.PrintVisual(fee, "הדפסת אגרת נהיגה");
                }
            }
            catch
            {
                MessageBox.Show("לא ניתן להדפיס כרגע. נסה שוב במועד אחר");
            }
        }
    }
}
