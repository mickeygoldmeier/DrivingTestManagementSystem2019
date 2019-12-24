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
using System.Windows.Controls.Primitives;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();
        public MainWindow()
        {
   
            InitializeComponent();
        }

        private void LogInStudent(object sender, RoutedEventArgs e)
        {
            try
            {
                // search for trainee with the wanted id
                List<BE.Trainee> resualt = iBL_Imp.SearchTrainee(trainee_id.Text, true, false, false);

                // if there is no resualt
                if (resualt.Count == 0)
                {
                    student_error_message.Visibility = Visibility.Visible;
                    return;
                }

                // if the password is not correct
                if (resualt[0].Password != iBL_Imp.Encrypte(student_password.Password))
                {
                    student_error_message.Visibility = Visibility.Visible;
                    return;
                }

                ///////////////////////////////////////////////////////////
                student_error_message.Visibility = Visibility.Collapsed;
                student_password.Password = "";
                trainee_id.Text = "";

                StudentWindow studentWindow = new StudentWindow(resualt[0]);
                this.Visibility = Visibility.Hidden;
                studentWindow.ShowDialog();
                this.Visibility = Visibility.Visible;
        }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

}

        private void StartStudentRegistration(object sender, RoutedEventArgs e)
        {
            StudentRegistration registration = new StudentRegistration();
            BeginStoryboard(this.FindResource("fade_out") as Storyboard);
            registration.ShowDialog();
            BeginStoryboard(this.FindResource("fade_in") as Storyboard);
        }

        private void LogInTester(object sender, RoutedEventArgs e)
        {
            try
            {
                // search for trainee with the wanted id
                List<BE.Tester> resualt = iBL_Imp.SearchTester(tester_id.Text, true, false, false);

                // if there is no resualt
                if (resualt.Count == 0)
                {
                    tester_error_message.Visibility = Visibility.Visible;
                    return;
                }

                // if the password is not correct
                if (resualt[0].Password != iBL_Imp.Encrypte(tester_password.Password))
                {
                    tester_error_message.Visibility = Visibility.Visible;
                    return;
                }

                ////////////////////////////////////////////////////////////
                tester_error_message.Visibility = Visibility.Collapsed;
                tester_password.Password = "";
                tester_id.Text = "";

                TesterWindow testerWindow = new TesterWindow(resualt[0]);
                this.Visibility = Visibility.Hidden;
                testerWindow.ShowDialog();
                this.Visibility = Visibility.Visible;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void StartTesterRegistration(object sender, RoutedEventArgs e)
        {
            TesterRegistration registration = new TesterRegistration();
            BeginStoryboard(this.FindResource("fade_out") as Storyboard);
            registration.ShowDialog();
            BeginStoryboard(this.FindResource("fade_in") as Storyboard);
        }

        private void Sign_in_student_enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                LogInStudent(log_in_student, null);
        }

        private void Sign_in_tester_enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                LogInTester(log_in_tester, null);
        }

        private void Visible_password(object sender, RoutedEventArgs e)
        {
            ToggleButton tgb = sender as ToggleButton;
            PasswordBox pas = student_password; // gave him a starting value so he won't say it's empty
            TextBox txt = txt_student_password; // gave him a starting value so he won't say it's empty

            if (tgb == null)
                return;

            switch(tgb.Name)
            {
                case "tgb_student":
                    pas = student_password;
                    txt = txt_student_password;
                    break;
                case "tgb_tester":
                    pas = tester_password;
                    txt = txt_tester_password;
                    break;
            }

            if (((ToggleButton)sender).IsChecked == true)
            {
                pas.Visibility = Visibility.Collapsed;
                txt.Visibility = Visibility.Visible;
                txt.Text = pas.Password;
            }
            else
            {
                txt.Visibility = Visibility.Collapsed;
                pas.Visibility = Visibility.Visible;
                pas.Password = txt.Text;
            }
        }

        private void Log_in_worker_Click(object sender, RoutedEventArgs e)
        {
            if(worker_password.Password == BE.Configuration.Worker_password)
            {
                worker_password.Password = "";
                WorkerWindow workerWindow = new WorkerWindow();
                this.Visibility = Visibility.Hidden;
                workerWindow.ShowDialog();
                this.Visibility = Visibility.Visible;
            }
        }
    }
}
