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
    /// Interaction logic for TesterRegistration.xaml
    /// </summary>
    public partial class TesterRegistration : Window
    {
        // A general matrix for the representation of the tester's working hours
        private bool[,] matrix = new bool[6, 5];
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();

        public TesterRegistration()
        {
            InitializeComponent();


            // Boot the table to be all true
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 5; j++)
                    matrix[i, j] = true;
        }

        /// <summary>
        /// Check whether the password and password authentication are correlated
        /// </summary>
        private void CheckPassword(object sender, RoutedEventArgs e)
        {
            if (password1.Password != password2.Password)
                password2.Foreground = Brushes.Red;
            else
                password2.Foreground = Brushes.Black;
        }

        /// <summary>
        /// Registration of a new student
        /// Enabled when you press a button
        /// </summary>
        private void Registration(object sender, RoutedEventArgs e)
        {
            try
            {
                // Determines the student's gender according to what the student chooses
                BE.Gender _gender = BE.Gender.Female;
                try
                {
                    switch (((ComboBoxItem)gender.SelectedItem).Content)
                    {
                        case "בת":
                            _gender = BE.Gender.Female;
                            break;
                        case "בן":
                            _gender = BE.Gender.Male;
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
                BE.Car_type _type = BE.Car_type.Private_car;
                try
                {
                    switch (((ComboBoxItem)car_type.SelectedItem).Content)
                    {
                        case "רכב פרטי":
                            _type = BE.Car_type.Private_car;
                            break;
                        case "רכב דו-גלגלי":
                            _type = BE.Car_type.Two_wheeled_vehicle;
                            break;
                        case "משאית בינונית":
                            _type = BE.Car_type.Medium_truck;
                            break;
                        case "משאית כבדה":
                            _type = BE.Car_type.Heavy_truck;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    throw new Exception("אנא בחר סוג רכב");
                }

                // Make sure all fields that must contain numbers contain numbers and not letters
                int b_numer, e_number, m_number, d_number;
                try
                {
                    b_numer = Int32.Parse(building_number.Text);
                    e_number = Int32.Parse(years_of_experience.Text);
                    m_number = Int32.Parse(maximum_tests.Text);
                    d_number = Int32.Parse(maximum_distance.Text);
                    Int32.Parse(phone_number.Text);
                }
                catch
                {
                    throw new Exception("אנא וודא שלא הכנסת אותיות או תווים בשדות בהם עליך להכניס מספר");
                }

                // Builds a new address according to the data
                BE.Address address = new BE.Address(street.Text, b_numer, city.Text);

                // Check whether the password and password authentication are correlated
                if (password1.Password != password2.Password)
                    throw new Exception("הסיסמה שהזנת לא תואמת לאימות הסיסמה שהזנת");

                // Builds a new instance of a trainee
                BE.Tester tester = new BE.Tester(id_number.Text, last_name.Text, first_name.Text, (DateTime)birth_date.SelectedDate, _gender,
                                                 phone_number.Text, address, e_number, m_number, _type, matrix, d_number, iBL_Imp.Encrypte(password1.Password));

                // Moves the rest of the treatment to the BL layer, displays a confirmation message, and closes the window
                iBL_Imp.Add_tester(tester);
                MessageBox.Show("ברוכים הבאים " + first_name.Text + "! אתה רשום במערכת כעת");
                this.Close();
            }

            // Displays the contents of the problem, if any
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            matrix[j, i] = condition;
        }
    }
}
