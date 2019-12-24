using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for StudentRegistration.xaml
    /// </summary>
    public partial class StudentRegistration : Window
    {
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();

        public StudentRegistration()
        {
            InitializeComponent();

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
                Mouse.OverrideCursor = Cursors.Wait;

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

                // Determines the type of gear box according to what the student chose
                BE.Gearbox_type _gear = BE.Gearbox_type.Manual;
                try
                {
                    switch (((ComboBoxItem)gear.SelectedItem).Content)
                    {
                        case "ידני":
                            _gear = BE.Gearbox_type.Manual;
                            break;
                        case "אוטומטי":
                            _gear = BE.Gearbox_type.Automatic;
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
                    b_numer = Int32.Parse(building_number.Text);
                    c_number = Int32.Parse(number_of_class.Text);
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
                BE.Trainee trainee = new BE.Trainee(id_number.Text, last_name.Text, first_name.Text, (DateTime)birth_date.SelectedDate, _gender, phone_number.Text,
                                                   address, false ,_type, _gear, school_name.Text, teacher_name.Text, c_number, iBL_Imp.Encrypte(password1.Password), email.Text);

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender1, DoWorkEventArgs args) =>
                {
                    try
                    {
                        // Moves the rest of the treatment to the BL layer
                        iBL_Imp.Add_traniee(trainee);
                    }
                    catch (Exception ex)
                    {
                        args.Cancel = true;
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    try
                    {
                        // Send email to confirm the email adrress and registration
                        iBL_Imp.Email_Registration(trainee, password1.Password);
                    }
                    catch
                    {
                        MessageBox.Show("המערכת לא הצליחה לשלוח לך אימייל עם אישור ההרשמה שלך. ייתכן שכתובת האימייל שהזנת שגויה או שאין חיבור לאינטרנט\nבאפשרותך לתקן את כתובת האימייל בכל עת במסך עדכון הפרטים אחרי התחברותך למערכת");
                    }
                };
                worker.RunWorkerCompleted += (object sender1, RunWorkerCompletedEventArgs args) =>
                {
                    if (!args.Cancelled)
                    {
                        // Displays a confirmation message, and closes the window
                        MessageBox.Show("ברוכים הבאים " + first_name.Text + "! אתה רשום במערכת כעת");
                        this.Close();
                    }
                    Mouse.OverrideCursor = null;
                };
                worker.RunWorkerAsync();
            }

            // Displays the contents of the problem, if any
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(ex.Message);
            }
}
    }
}
