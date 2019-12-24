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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : CustomWindowWithoutX
    {
        private bool All_changes_saved = true;
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();

        public WorkerWindow()
        {
            InitializeComponent();

            

            Student_list_view.Items.Clear();
            Question_list_view.Items.Clear();
            Student_list_view.ItemsSource = iBL_Imp.GetTrainees();
            Testers_list_view.ItemsSource = iBL_Imp.GetTesters();
            Question_list_view.ItemsSource = iBL_Imp.GetTQ();
        }

        private void Left_tab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Query_Card.Visibility = Visibility.Visible;
            left_tab_rec.Visibility = Visibility.Visible;

            Testers_list_card.Visibility = Visibility.Collapsed;
            center_tab_rec.Visibility = Visibility.Collapsed;

            Student_list_card.Visibility = Visibility.Collapsed;
            right_tab_rec.Visibility = Visibility.Collapsed;

            Settings_list_card.Visibility = Visibility.Collapsed;
            settings_tab_rec.Visibility = Visibility.Collapsed;

            Fill_Querys();
        }

        private void Center_tab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Query_Card.Visibility = Visibility.Collapsed;
            left_tab_rec.Visibility = Visibility.Collapsed;

            Testers_list_card.Visibility = Visibility.Visible;
            center_tab_rec.Visibility = Visibility.Visible;

            Student_list_card.Visibility = Visibility.Collapsed;
            right_tab_rec.Visibility = Visibility.Collapsed;

            Settings_list_card.Visibility = Visibility.Collapsed;
            settings_tab_rec.Visibility = Visibility.Collapsed;
        }

        private void Right_tab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Query_Card.Visibility = Visibility.Collapsed;
            left_tab_rec.Visibility = Visibility.Collapsed;

            Testers_list_card.Visibility = Visibility.Collapsed;
            center_tab_rec.Visibility = Visibility.Collapsed;

            Student_list_card.Visibility = Visibility.Visible;
            right_tab_rec.Visibility = Visibility.Visible;

            Settings_list_card.Visibility = Visibility.Collapsed;
            settings_tab_rec.Visibility = Visibility.Collapsed;
        }

        private void Settings_tab_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Query_Card.Visibility = Visibility.Collapsed;
            left_tab_rec.Visibility = Visibility.Collapsed;

            Testers_list_card.Visibility = Visibility.Collapsed;
            center_tab_rec.Visibility = Visibility.Collapsed;

            Student_list_card.Visibility = Visibility.Collapsed;
            right_tab_rec.Visibility = Visibility.Collapsed;

            Settings_list_card.Visibility = Visibility.Visible;
            settings_tab_rec.Visibility = Visibility.Visible;
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
        /// Log in as one of the trainee in the list
        /// </summary>
        private void Student_list_view_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BE.Trainee trainee = Student_list_view.SelectedItem as BE.Trainee;
                MessageBoxResult result = MessageBox.Show("האם ברצונך להתחבר למערכת בתור " + trainee.First_name + " " + trainee.Last_name + "?", "התחברות כתלמיד", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    StudentWindow studentWindow = new StudentWindow(trainee);
                    this.Close();
                    studentWindow.ShowDialog();
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// When there is a change in the text in the search bar, search the students
        /// </summary>
        private void Search_Trainee(object sender, TextChangedEventArgs e)
        {
            // If the search bar is empty
            if ((sender as TextBox).Text == "")
            {
                Student_list_view.ItemsSource = iBL_Imp.GetTrainees();
                icon_search.Visibility = Visibility.Visible;
                btn_clear_text.Visibility = Visibility.Collapsed;
                return;
            }

            // // If the search bar is not empty
            Student_list_view.ItemsSource = iBL_Imp.SearchTrainee((sender as TextBox).Text);
            icon_search.Visibility = Visibility.Collapsed;
            btn_clear_text.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Clear the text in the search bar
        /// </summary>
        private void Btn_clear_text_Click(object sender, RoutedEventArgs e)
        {
            tbx_search_bar.Text = "";
        }

        /// <summary>
        /// When there is a change in the text in the search bar, search the testers
        /// </summary>
        private void Search_Tester(object sender, TextChangedEventArgs e)
        {
            // If the search bar is empty
            if ((sender as TextBox).Text == "")
            {
                Testers_list_view.ItemsSource = iBL_Imp.GetTesters();
                icon_search2.Visibility = Visibility.Visible;
                btn_clear_text2.Visibility = Visibility.Collapsed;
                return;
            }

            // // If the search bar is not empty
            Testers_list_view.ItemsSource = iBL_Imp.SearchTester((sender as TextBox).Text);
            icon_search2.Visibility = Visibility.Collapsed;
            btn_clear_text2.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Clear the text in the search bar
        /// </summary>
        private void Btn_clear_text_Click2(object sender, RoutedEventArgs e)
        {
            tbx_search_bar2.Text = "";
        }

        /// <summary>
        /// Log in as one of the tester in the list
        /// </summary>
        private void Tester_list_view_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BE.Tester tester = Testers_list_view.SelectedItem as BE.Tester;
                MessageBoxResult result = MessageBox.Show("האם ברצונך להתחבר למערכת בתור " + tester.First_name + " " + tester.Last_name + "?", "התחברות כבוחן", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    TesterWindow testerWindow = new TesterWindow(tester);
                    this.Close();
                    testerWindow.ShowDialog();
                }
            }
            catch (Exception) { }
        }

        private void Log_out(object sender, RoutedEventArgs e)
        {
            if (All_changes_saved)
            {
                this.Close();
                return;
            }

            if(MessageBox.Show("?ביצעת שינויים במידע ולא שמרת אותם. האם אתה בטוח שאתה רוצה להתנתק", "התנתקות מהמערכת", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
                return;
            }
        }

        private void Fill_Querys()
        {
            string[] colors = { "#ffbd11", "#01ac4c", "#308ccd", "#f02c22", "#f86321" };
            stp_schools.Children.Clear();
            trv_schools.Items.Clear();
            List<List<BE.Trainee>> list = iBL_Imp.GroupBySchool();
            int i = -1;
            foreach (List<BE.Trainee> item in list)
            {
                i++;
                double value = ((double)item.Count / (double)iBL_Imp.GetTrainees().Count) * 250.0;
                stp_schools.Children.Add(new ChartComponent(value, colors[i % colors.Length], item[0].Driving_school, item.Count.ToString()));
                TreeViewItem tree = new TreeViewItem();
                tree.Header = item[0].Driving_school;

                foreach (BE.Trainee trainee in item)
                {
                    TreeViewItem sub_tree = new TreeViewItem();
                    sub_tree.Header = trainee.First_name + " " + trainee.Last_name;
                    TreeViewItem sub_sub_tree = new TreeViewItem();
                    sub_sub_tree.Header = trainee.ToString();
                    sub_tree.Items.Add(sub_sub_tree);
                    tree.Items.Add(sub_tree);
                }

                trv_schools.Items.Add(tree);
            }

            stp_teachers.Children.Clear();
            trv_teachers.Items.Clear();
            List<List<BE.Trainee>> list1 = iBL_Imp.GroupByTeacher();
            int j = -1;
            foreach (List<BE.Trainee> item in list1)
            {
                j++;
                double value = ((double)item.Count / (double)iBL_Imp.GetTrainees().Count) * 250.0;
                stp_teachers.Children.Add(new ChartComponent(value, colors[j % colors.Length], item[0].Teachers_name, item.Count.ToString()));
                TreeViewItem tree = new TreeViewItem();
                tree.Header = item[0].Teachers_name;

                foreach (BE.Trainee trainee in item)
                {
                    TreeViewItem sub_tree = new TreeViewItem();
                    sub_tree.Header = trainee.First_name + " " + trainee.Last_name;
                    TreeViewItem sub_sub_tree = new TreeViewItem();
                    sub_sub_tree.Header = trainee.ToString();
                    sub_tree.Items.Add(sub_sub_tree);
                    tree.Items.Add(sub_tree);
                }

                trv_teachers.Items.Add(tree);
            }

            stp_Specialization.Children.Clear();
            trv_Specialization.Items.Clear();
            CarTypeConvertorToString convertor = new CarTypeConvertorToString();
            List<List<BE.Tester>> list2 = iBL_Imp.GroupBySpecialization();
            int k = -1;
            foreach (List<BE.Tester> item in list2)
            {
                k++;
                double value = ((double)item.Count / (double)iBL_Imp.GetTesters().Count) * 250.0;
                stp_Specialization.Children.Add(new ChartComponent(value, colors[k % colors.Length], convertor.Convert(item[0].Specialization, null, null, null).ToString(), item.Count.ToString()));
                TreeViewItem tree = new TreeViewItem();
                tree.Header = convertor.Convert(item[0].Specialization, null, null, null).ToString();

                foreach (BE.Tester tester in item)
                {
                    TreeViewItem sub_tree = new TreeViewItem();
                    sub_tree.Header = tester.First_name + " " + tester.Last_name;
                    TreeViewItem sub_sub_tree = new TreeViewItem();
                    sub_sub_tree.Header = tester.ToString();
                    sub_tree.Items.Add(sub_sub_tree);
                    tree.Items.Add(sub_tree);
                }

                trv_Specialization.Items.Add(tree);
            }

            stp_number.Children.Clear();
            trv_number.Items.Clear();
            List<List<BE.Trainee>> list3 = iBL_Imp.GroupByNumberOfTests();
            int t = -1;
            foreach (List<BE.Trainee> item in list3)
            {
                t++;
                double value = ((double)item.Count / (double)iBL_Imp.GetTrainees().Count) * 250.0;
                stp_number.Children.Add(new ChartComponent(value, colors[t % colors.Length], iBL_Imp.Count_tests(item[0]).ToString(), item.Count.ToString()));
                TreeViewItem tree = new TreeViewItem();
                tree.Header = iBL_Imp.Count_tests(item[0]);

                foreach (BE.Trainee trainee in item)
                {
                    TreeViewItem sub_tree = new TreeViewItem();
                    sub_tree.Header = trainee.First_name + " " + trainee.Last_name;
                    TreeViewItem sub_sub_tree = new TreeViewItem();
                    sub_sub_tree.Header = trainee.ToString();
                    sub_tree.Items.Add(sub_sub_tree);
                    tree.Items.Add(sub_tree);
                }

                trv_number.Items.Add(tree);
            }

        }

        private void Changes(object sender, KeyEventArgs e)
        {
            All_changes_saved = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    int.Parse(txb_timer_theory.Text);
                    int.Parse(txb_amount_questions.Text);
                    int.Parse(txb_fail_theory.Text);
                    int.Parse(txb_minimun_lessons.Text);
                    int.Parse(txb_maximum_tester_age.Text);
                    int.Parse(txb_minimum_tester_age.Text);
                    int.Parse(txb_minimum_Trainee_age.Text);
                    int.Parse(txb_time_between_tests.Text);
                    double.Parse(txb_defualt_distance.Text);
                }
                catch
                {
                    throw new Exception("שגיאה בעדכון נתונים: הזנת ערכים שאינם מספרים בשדות שבהם היית אמור להזין מספרים");
                }

                BE.Configuration.Timer_theory = int.Parse(txb_timer_theory.Text);
                BE.Configuration.Amount_questions = int.Parse(txb_amount_questions.Text);
                BE.Configuration.Fail_theory = int.Parse(txb_fail_theory.Text);
                BE.Configuration.Minimun_lessons = int.Parse(txb_minimun_lessons.Text);
                BE.Configuration.Maximum_tester_age = int.Parse(txb_maximum_tester_age.Text);
                BE.Configuration.Minimum_tester_age = int.Parse(txb_minimum_tester_age.Text);
                BE.Configuration.Minimum_Trainee_age = int.Parse(txb_minimum_Trainee_age.Text);
                BE.Configuration.Time_between_tests = int.Parse(txb_time_between_tests.Text);
                BE.Configuration.Defualt_distance = double.Parse(txb_defualt_distance.Text);
                BE.Configuration.Worker_password = txb_worker_password.Text;

                iBL_Imp.UpdateConfigurationFile();
                All_changes_saved = true;
                throw new Exception("כל הנתונים התעדכנו בהצלחה");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void New_question(object sender, RoutedEventArgs e)
        {
            NewQuestionWindow window = new NewQuestionWindow();
            //BeginStoryboard(this.FindResource("fadeO") as Storyboard);
            window.ShowDialog();
            //BeginStoryboard(this.FindResource("fadeI") as Storyboard);
            Question_list_view.ItemsSource = null;
            Question_list_view.Items.Clear();
            Question_list_view.ItemsSource = iBL_Imp.GetTQ();
        }

        private void Delete_questions(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("?האם אתה בתוך שאתה רוצה למחוק את השאלות הנבחרות", "מחיקת שאלות", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                List<BE.TheoryQuestion> list = new List<BE.TheoryQuestion>();

                foreach (BE.TheoryQuestion item in Question_list_view.SelectedItems)
                    list.Add(item);

                foreach (BE.TheoryQuestion item in list)
                    iBL_Imp.Remove_theory_q(item);

                Question_list_view.ItemsSource = iBL_Imp.GetTQ();
            }
        }
    }
}
