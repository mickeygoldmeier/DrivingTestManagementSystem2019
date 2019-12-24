using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : CustomWindowWithoutX
    {
        private BE.Trainee Trainee;
        private bool All_changes_saved = true;
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();

        public StudentWindow(BE.Trainee trainee)
        {
            InitializeComponent();

            Trainee = trainee;
            thisWindow.DataContext = Trainee;
            city1.DataContext = Trainee.Address;
            building_number1.DataContext = Trainee.Address;
            street1.DataContext = Trainee.Address;
            All_changes_saved = true;

            if (iBL_Imp.GetTheoryStatus(Trainee) == BE.Thery_status.Passed)
            {
                sub_title.Text = "במסך זה באפשרותך להוסיף טסטים, למחוק טסטים עתידיים, לעדכן את פרטיך ועוד";

                theory_tab.Visibility = Visibility.Collapsed;
                center_tab.Visibility = Visibility.Visible;
                right_tab.Visibility = Visibility.Visible;
                theory_test.Visibility = Visibility.Collapsed;


                // Restart the test display
                Right_tab_MouseDown(null, null);
            }
            else
            {
                TheoryTestUserControl UC = new TheoryTestUserControl(Trainee, sub_title, left_tab, center_tab, right_tab, theory_tab, center_tab_rec, new_test_view, theory_test, exit_button);
                UC.FlowDirection = FlowDirection.LeftToRight;
                UC.VerticalAlignment = VerticalAlignment.Top;
                theory_test_view.Children.Add(UC);
            }
        }

        /// <summary>
        /// A function that fill the test lists
        /// </summary>
        private void FillTestsList()
        {
            List<BE.Test>[] tests = iBL_Imp.groupByTraineePass(Trainee);

            exp_pass_tests.Children.Clear();
            exp_didnt_pass_tests.Children.Clear();
            exp_futre_tests.Children.Clear();


            foreach (BE.Test item in tests[0])
            {
                TestUserControl test = new TestUserControl(item, false, true);
                test.FlowDirection = FlowDirection.LeftToRight;
                test.Height = 250;
                test.Width = 250;
                test.VerticalAlignment = VerticalAlignment.Top;
                exp_pass_tests.Children.Add(test);
            }


            foreach (BE.Test item in tests[1])
            {
                TestUserControl test = new TestUserControl(item, false);
                test.FlowDirection = FlowDirection.LeftToRight;
                test.Height = 250;
                test.Width = 250;
                test.VerticalAlignment = VerticalAlignment.Top;
                exp_didnt_pass_tests.Children.Add(test);
            }

            foreach (BE.Test item in tests[2])
            {
                TestUserControl test = new TestUserControl(item, false);
                test.FlowDirection = FlowDirection.LeftToRight;
                test.Height = 250;
                test.Width = 250;
                test.VerticalAlignment = VerticalAlignment.Top;
                exp_futre_tests.Children.Add(test);
            }
        }

        private void Home_Address(object sender, RoutedEventArgs e)
        {
            if (home_address.IsChecked == true)
            {
                address.DataContext = Trainee.Address;

                city.IsEnabled = false;
                street.IsEnabled = false;
                building_number.IsEnabled = false;
            }
            else
            {
                address.DataContext = null;

                city.Text = "";
                street.Text = "";
                building_number.Text = "";

                city.IsEnabled = true;
                street.IsEnabled = true;
                building_number.IsEnabled = true;
            }
        }

        private void Save_test(object sender, RoutedEventArgs e)
        {
            try
            {
                pb_save.Visibility = Visibility.Visible;
                pi_save.Visibility = Visibility.Collapsed;

                // Make sure all fields that must contain numbers contain numbers and not letters
                int b_numer;
                try
                {
                    b_numer = Int32.Parse(building_number.Text);
                }
                catch
                {
                    throw new Exception("אנא וודא שלא הכנסת אותיות או תווים בשדות בהם עליך להכניס מספר");
                }

                // Builds a new address according to the data
                BE.Address address = new BE.Address(street.Text, b_numer, city.Text);

                int hour = Int32.Parse(((ListViewItem)test_hour.SelectedItem).Content.ToString().Replace(":00", ""));
                DateTime _date = (DateTime)test_date.SelectedDate;
                DateTime date = new DateTime(_date.Year, _date.Month, _date.Day, hour, 0, 0);

                BE.Test test = new BE.Test(Trainee.ID, date, address, Trainee.Learned);

                if (txt_credit_card_number.Text.Length < 18 || txt_credit_card_date.Text.Length != 5 || txt_credit_card_CVV.Text.Length != 3)
                    throw new Exception("שגיאה בפרטי האשראי: פרטי כרטיס האשראי שהכנסת לא נכונים, או שלא מילאת את כל השדות הדרושים");

                BackgroundWorker worker = new BackgroundWorker() { WorkerSupportsCancellation = true };
                worker.DoWork += (object sender1, DoWorkEventArgs args) =>
                {
                    try
                    {
                        iBL_Imp.Add_test(test);
                        //iBL_Imp.Send_Email(test);
                        new Thread(() => { iBL_Imp.Send_Email(test); }).Start();
                        args.Cancel = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        args.Cancel = true;
                    }
                };
                worker.RunWorkerCompleted += (object sender1, RunWorkerCompletedEventArgs args) =>
                {
                    if (!args.Cancelled)
                    {
                        home_address.IsChecked = false;
                        Home_Address(home_address, null);
                        test_date.SelectedDate = null;
                        test_hour.SelectedItem = null;
                        txt_credit_card_date.Text = "";
                        txt_credit_card_CVV.Text = "";
                        txt_credit_card_number.Text = "";

                        string test_data = "נרשמת בהצלחה לטסט ב" + test.Test_time.ToLongDateString() + " בשעה " + test.Test_time.ToShortTimeString() + "\nהטסט מתחיל ב" + test.Address.ToString() + "\nהאם ברצונך להוסיף את הטסט ללוח השנה שלך?";
                        MessageBoxResult result = MessageBox.Show(test_data, "הטסט נשמר בהצלחה", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                iBL_Imp.Add_To_Calender(test);
                                break;
                            default:
                                break;
                        }
                    }

                    pb_save.Visibility = Visibility.Collapsed;
                    pi_save.Visibility = Visibility.Visible;
                };

                worker.RunWorkerAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

                pb_save.Visibility = Visibility.Collapsed;
                pi_save.Visibility = Visibility.Visible;
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

            theory_test.Visibility = Visibility.Collapsed;
            theory_tab_rec.Visibility = Visibility.Collapsed;
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

            FillTestsList();
        }

        private void Theory_tab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!All_changes_saved)
            {
                MessageBox.Show("עליך לשמור את השינויים שביצעת לפני שתוכל לעשות מבחן תאורטי", "עידכון פרטים", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            update_student_data.Visibility = Visibility.Collapsed;
            left_tab_rec.Visibility = Visibility.Collapsed;

            theory_test.Visibility = Visibility.Visible;
            theory_tab_rec.Visibility = Visibility.Visible;
        }

        private void Delete_student(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק את עצמך מהמערכת\nזוהי בלתי הפיכה. בפעולה זו תאבד את הגישה שלך למערכת ולא תוכל לצפות, להירשם לקבל מידע על טסטים", "מחיקה עצמית מהמערכת", MessageBoxButton.YesNo, MessageBoxImage.Hand);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        iBL_Imp.Remove_traniee(Trainee);
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

        private void Update_student(object sender, RoutedEventArgs e)
        {
            try
            {
                // Determines the student's gender according to what the student chooses
                try
                {
                    switch (((ComboBoxItem)gender.SelectedItem).Content)
                    {
                        case "בת":
                            Trainee.Gender = BE.Gender.Female;
                            break;
                        case "בן":
                            Trainee.Gender = BE.Gender.Male;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    throw new Exception("אנא בחר מין");
                }

                // Determines the type of vehicle according to what the student chose
                try
                {
                    switch (((ComboBoxItem)car_type.SelectedItem).Content)
                    {
                        case "רכב פרטי":
                            Trainee.Learned = BE.Car_type.Private_car;
                            break;
                        case "רכב דו-גלגלי":
                            Trainee.Learned = BE.Car_type.Two_wheeled_vehicle;
                            break;
                        case "משאית בינונית":
                            Trainee.Learned = BE.Car_type.Medium_truck;
                            break;
                        case "משאית כבדה":
                            Trainee.Learned = BE.Car_type.Heavy_truck;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    throw new Exception("אנא בחר סוג רכב");
                }

                // Determines the type of gear box according to what the student chose
                try
                {
                    switch (((ComboBoxItem)gear.SelectedItem).Content)
                    {
                        case "ידני":
                            Trainee.Gear_used = BE.Gearbox_type.Manual;
                            break;
                        case "אוטומטי":
                            Trainee.Gear_used = BE.Gearbox_type.Automatic;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    throw new Exception("אנא בחר סוג גיר");
                }

                // Make sure all fields that must contain numbers contain numbers and not letters
                int b_numer, c_number;
                try
                {
                    b_numer = Int32.Parse(building_number1.Text);
                    c_number = Int32.Parse(number_of_class.Text);
                    Int32.Parse(phone_number.Text);
                }
                catch
                {
                    throw new Exception("אנא וודא שלא הכנסת אותיות או תווים בשדות בהם עליך להכניס מספר");
                }

                // Builds a new address according to the data
                Trainee.Address = new BE.Address(street1.Text, b_numer, city1.Text);

                if (password.Password.Length != 0)
                    Trainee.Password = iBL_Imp.Encrypte(password.Password);

                // Moves the rest of the treatment to the BL layer, displays a confirmation message, and closes the window
                iBL_Imp.Update_traniee(Trainee);
                All_changes_saved = true;
                MessageBox.Show("הנתונים שהכנסת התעדכנו בהצלחה");
            }

            // Displays the contents of the problem, if any
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void Txt_credit_card_date_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 2)
            {
                (sender as TextBox).Text += "/";
                (sender as TextBox).CaretIndex = (sender as TextBox).Text.Length;
            }
        }

        private void Txt_credit_card_number_KeyUp(object sender, KeyEventArgs e)
        {
            int l = (sender as TextBox).Text.Length;
            if (l == 4 || l == 9 || l == 14)
            {
                (sender as TextBox).Text += "- ";
                (sender as TextBox).CaretIndex = (sender as TextBox).Text.Length;
            }
        }

        /// <summary>
        /// collapse other parts while one is opend
        /// </summary>
        private void when_open_pass(object sender, RoutedEventArgs e)
        {
            exp_didnt_pass.IsExpanded = false;
            exp_future.IsExpanded = false;
        }

        private void when_open_didnt_pass(object sender, RoutedEventArgs e)
        {
            exp_pass.IsExpanded = false;
            exp_future.IsExpanded = false;
        }

        private void when_open_future(object sender, RoutedEventArgs e)
        {
            exp_pass.IsExpanded = false;
            exp_didnt_pass.IsExpanded = false;
        }

        private void Changes(object sender, KeyEventArgs e)
        {
            All_changes_saved = false;
        }

        private void Combo_box_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Changes(sender, null);
        }

        private void Birth_date_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Changes(sender, null);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_plus_minus_Click(object sender, RoutedEventArgs e)
        {
            int num = 1;
            if ((string)(sender as Button).Content == "-")
                num = -1;
            try
            {
                number_of_class.Text = "" + (int.Parse(number_of_class.Text) + num);
                All_changes_saved = false;
            }
            catch 
            {
            }
            
        }
    }
}
