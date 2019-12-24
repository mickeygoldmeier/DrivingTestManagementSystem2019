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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TesterWindow.xaml
    /// </summary>
    public partial class TesterWindow : CustomWindowWithoutX
    {
        private BE.Tester Tester;
        private DateTime Date;
        private bool All_changes_saved = true;
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();

        public TesterWindow(BE.Tester tester)
        {
            InitializeComponent();

            Tester = tester;
            Date = DateTime.Now;
            this.DataContext = Tester;

            Fill_Schedule();
            Fill_Tests_List();
        }

        /// <summary>
        /// A function that fill the test lists
        /// </summary>
        private void Fill_Tests_List()
        {
            List<BE.Test>[] tests = iBL_Imp.groupByTesterFilled(Tester);

            exp_futre_tests.Children.Clear();
            exp_past_tests.Children.Clear();
            exp_futre_test_tests.Children.Clear();

            foreach (BE.Test item in tests[0])
            {
                TestUserControl test = new TestUserControl(item, true);
                test.FlowDirection = FlowDirection.LeftToRight;
                test.Height = 250;
                test.Width = 250;
                test.VerticalAlignment = VerticalAlignment.Top;
                exp_past_tests.Children.Add(test);
            }

            foreach (BE.Test item in tests[1])
            {
                TestUserControl test = new TestUserControl(item, true);
                test.FlowDirection = FlowDirection.LeftToRight;
                test.Height = 250;
                test.Width = 250;
                test.VerticalAlignment = VerticalAlignment.Top;
                exp_futre_tests.Children.Add(test);
            }

            foreach (BE.Test item in tests[2])
            {
                TestUserControl test = new TestUserControl(item, false);
                test.FlowDirection = FlowDirection.LeftToRight;
                test.Height = 250;
                test.Width = 250;
                test.VerticalAlignment = VerticalAlignment.Top;
                exp_futre_test_tests.Children.Add(test);
            }
        }

        private void Fill_Schedule()
        {
            BE.Test[,] tests = iBL_Imp.Tester_sceduale(Tester, Date);

            List<UIElement> list = new List<UIElement>();

            for (int i = 0; i < grd_schedule.Children.Count; i++)
                if (!(Grid.GetRow(grd_schedule.Children[i]) == 0) && !(Grid.GetColumn(grd_schedule.Children[i]) == 0))
                    list.Add(grd_schedule.Children[i]);

            foreach (UIElement item in list)
                grd_schedule.Children.Remove(item);


            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 5; j++)
                {
                    ScheduleComponent tb = new ScheduleComponent(tests[i, j]);
                    tb.Margin = new Thickness(5);
                    grd_schedule.Children.Add(tb);
                    Grid.SetRow(tb, i + 1);
                    Grid.SetColumn(tb, j + 1);
                }

            DateTime day1 = Date.AddDays(-(int)Date.DayOfWeek);
            string sunday = day1.Day + "." + day1.Month + "." + day1.Year;
            day1 = day1.AddDays(4);
            string thursday = day1.Day + "." + day1.Month + "." + day1.Year;
            txb_week_view.Text = sunday + " - " + thursday;
        }

        private void Sign_out(object sender, RoutedEventArgs e)
        {
            if (All_changes_saved)
            {
                this.Close();
                return;
            }

            MessageBoxResult result = MessageBox.Show("?ביצעת שינויים במידע ולא שמרת אותם. האם אתה בטוח שאתה רוצה להתנתק", "התנתקות מהמערכת", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                default:
                    break;
            }
        }


        private void Left_tab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            update_student_data.Visibility = Visibility.Visible;
            left_tab_rec.Visibility = Visibility.Visible;

            new_test_view.Visibility = Visibility.Collapsed;
            center_tab_rec.Visibility = Visibility.Collapsed;

            test_list_view.Visibility = Visibility.Collapsed;
            right_tab_rec.Visibility = Visibility.Collapsed;

            city1.DataContext = Tester.Address;
            building_number1.DataContext = Tester.Address;
            street1.DataContext = Tester.Address;

            foreach (var item in schedule.Children)
                if((item is TextBlock) && ((item as TextBlock).Text == "עובד" || (item as TextBlock).Text == "לא עובד"))
                {
                    if (Tester.Schedule[Grid.GetRow(item as TextBlock) - 1, Grid.GetColumn(item as TextBlock)])
                    {

                        (item as TextBlock).Text = "עובד";
                        (item as TextBlock).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F8FFBA"));
                    }
                    else
                    {
                        (item as TextBlock).Text = "לא עובד";
                        (item as TextBlock).Background = Brushes.LightGray;
                    }
                }
            
        }

        private void Center_tab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            update_student_data.Visibility = Visibility.Collapsed;
            left_tab_rec.Visibility = Visibility.Collapsed;

            new_test_view.Visibility = Visibility.Visible;
            center_tab_rec.Visibility = Visibility.Visible;

            test_list_view.Visibility = Visibility.Collapsed;
            right_tab_rec.Visibility = Visibility.Collapsed;
        }

        private void Right_tab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            update_student_data.Visibility = Visibility.Collapsed;
            left_tab_rec.Visibility = Visibility.Collapsed;

            new_test_view.Visibility = Visibility.Collapsed;
            center_tab_rec.Visibility = Visibility.Collapsed;

            test_list_view.Visibility = Visibility.Visible;
            right_tab_rec.Visibility = Visibility.Visible;

            //FillTestsList();
        }

        /// <summary>
        /// collapse other parts while one is opend
        /// </summary>
        private void when_open_pass(object sender, RoutedEventArgs e)
        {
            exp_past.IsExpanded = false;
            exp_future_test.IsExpanded = false;
        }

        private void when_open_future(object sender, RoutedEventArgs e)
        {
            exp_future.IsExpanded = false;
            exp_future_test.IsExpanded = false;
        }

        private void when_open_future_test(object sender, RoutedEventArgs e)
        {
            exp_future.IsExpanded = false;
            exp_past.IsExpanded = false;
        }

        private void Update_tester(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make sure all fields that must contain numbers contain numbers and not letters
                try
                {
                    Int32.Parse(building_number1.Text);
                    Int32.Parse(years_of_experience.Text);
                    Int32.Parse(maximum_tests.Text);
                    Double.Parse(maximum_distance.Text);
                    Int32.Parse(phone_number.Text);
                }
                catch
                {
                    throw new Exception("אנא וודא שלא הכנסת אותיות או תווים בשדות בהם עליך להכניס מספר");
                }

                if (password.Password.Length != 0)
                    Tester.Password = iBL_Imp.Encrypte(password.Password);
                // Moves the rest of the treatment to the BL layer, displays a confirmation message, and closes the window
                iBL_Imp.Update_tester(Tester);
                All_changes_saved = true;
                MessageBox.Show("הנתונים שהכנסת התעדכנו בהצלחה");
            }
            // Displays the contents of the problem, if any
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_student(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק את עצמך מהמערכת\nזוהי בלתי הפיכה. בפעולה זו תאבד את הגישה שלך למערכת ולא תוכל לצפות, להירשם לקבל מידע על טסטים", "מחיקה עצמית מהמערכת", MessageBoxButton.YesNo, MessageBoxImage.Hand);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        iBL_Imp.Remove_tester(Tester);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    MessageBox.Show("נמחקת לחלוטין מהמערכת");
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void Changes(object sender, KeyEventArgs e)
        {
            All_changes_saved = false;
        }

        private void Birth_date_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Changes(sender, null);
        }

        private void Combo_box_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Changes(sender, null);
        }

        private void Add_to_date(object sender, RoutedEventArgs e)
        {
            Date = Date.AddDays(7);
            Fill_Schedule();
        }

        private void Sub_from_date(object sender, RoutedEventArgs e)
        {
            Date = Date.AddDays(-7);
            Fill_Schedule();
        }

        private void Return_to_this_week(object sender, RoutedEventArgs e)
        {
            Date = DateTime.Now;
            Fill_Schedule();
        }

        /// <summary>
        /// Click on the schedule function
        /// </summary>
        private void ChangeState(object sender, MouseButtonEventArgs e)
        {
            bool condition = true;

            // Check whether "Working" or "Not Working" has been flagged and updated the button color accordingly
            if (((TextBlock)sender).Text == "עובד")
            {
                ((TextBlock)sender).Text = "לא עובד";
                ((TextBlock)sender).Background = Brushes.LightGray;
                condition = false;
            }
            else
            {
                ((TextBlock)sender).Text = "עובד";
                ((TextBlock)sender).Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F8FFBA"));
                condition = true;
            }

            // Update the change in the overall representation matrix of the hours
            int i = Grid.GetColumn(sender as TextBlock);
            int j = Grid.GetRow(sender as TextBlock) - 1;
            Tester.Schedule[j, i] = condition;

            All_changes_saved = false;
        }

        /// <summary>
        /// Show or hide the hand cursor
        /// </summary>
        private void Show_hand(object sender, MouseEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Hand)
                Mouse.OverrideCursor = null;
            else
                Mouse.OverrideCursor = Cursors.Hand;
        }


    }
}
