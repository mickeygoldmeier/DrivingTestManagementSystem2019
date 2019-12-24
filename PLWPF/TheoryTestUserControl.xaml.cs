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
using System.Windows.Threading;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for TheoryTestUserControl.xaml
    /// </summary>
    public partial class TheoryTestUserControl : UserControl
    {
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();
        private BE.Trainee Trainee;
        private TextBlock sub_title;
        private StackPanel left_tab;
        private StackPanel center_tab;
        private StackPanel right_tab;
        private StackPanel theory_tab;
        private Rectangle center_tab_rec;
        MaterialDesignThemes.Wpf.Card new_test_view;
        MaterialDesignThemes.Wpf.Card theory_test;
        private Button exit;
        private BE.TheoryQuestion[] test;
        private TheoryQuestionUserControl[] questionsUC;
        private CheckBox[] question_check_boxes;
        private int current_question = -1;

        private int time;
        private DispatcherTimer Timer;

        public TheoryTestUserControl(BE.Trainee trainee, TextBlock st, StackPanel lt, StackPanel ct, StackPanel rt, StackPanel tt, Rectangle rec,
                                      MaterialDesignThemes.Wpf.Card card, MaterialDesignThemes.Wpf.Card theory, Button b)
        {
            InitializeComponent();

            Trainee = trainee;

            if (iBL_Imp.GetTheoryStatus(Trainee) == BE.Thery_status.Hold)
            {
                text_hold.Text = "אין באפשרותך לעשות מבחן כרגע, תוכל לעשות את המבחן הבא\nבתאריך " +
                                  Trainee.Last_theory.AddDays(1).ToShortDateString() + " בשעה " + Trainee.Last_theory.AddDays(1).ToShortTimeString();
                text_hold.Visibility = Visibility.Visible;
                return;
            }

            sub_title = st;
            left_tab = lt;
            center_tab = ct;
            right_tab = rt;
            theory_tab = tt;
            center_tab_rec = rec;
            new_test_view = card;
            theory_test = theory;
            exit = b;

            txtblk_rules.Text = "במבחן התאוריה יוצגו בפניך " + BE.Configuration.Amount_questions
                                + " שאלות שמתוכם תצטרך לענות נכונה לפחות על " + (BE.Configuration.Amount_questions - BE.Configuration.Fail_theory + 1)
                                + "\n למילוי המבחן יש הגבלת זמן של " + BE.Configuration.Timer_theory + " דקות, כמובן שלמסיימים מוקדם יותר יש אפשרות להגיש קודם"
                                + "\n אם לא עברת מבחן זה בהצלחה - תאלץ לחכות 24 שעות עד שתוכל לעשות מבחן חדש";


            // initialize timer
            time = BE.Configuration.Timer_theory * 60;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;

            before_test.Visibility = Visibility.Visible;
        }

        private void Start_theory_test_Click(object sender, RoutedEventArgs e)
        {
            before_test.Visibility = Visibility.Collapsed;
            test_view.Visibility = Visibility.Visible;

            left_tab.IsEnabled = false;
            left_tab.Opacity = 0.25;
            exit.IsEnabled = false;

            Trainee.Last_theory = DateTime.Now; // update that trainee takes theory test now
            iBL_Imp.Update_traniee(Trainee);

            test = iBL_Imp.GetTheoryTest();

            Random rand = new Random();

            for (int i = 0; i < BE.Configuration.Amount_questions / 3; i++) // add rows for amount of questions
                check_box_grid.RowDefinitions.Add(new RowDefinition());

            question_check_boxes = new CheckBox[BE.Configuration.Amount_questions];
            questionsUC = new TheoryQuestionUserControl[BE.Configuration.Amount_questions];

            for (int i = 0; i < BE.Configuration.Amount_questions; i++) // add check boxes and TheoryQuestionUserControl
            {
                question_check_boxes[i] = new CheckBox();
                check_box_grid.Children.Add(question_check_boxes[i]);
                question_check_boxes[i].Content = i + 1;
                question_check_boxes[i].HorizontalAlignment = HorizontalAlignment.Center;
                question_check_boxes[i].VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(question_check_boxes[i], i / 3);
                Grid.SetColumn(question_check_boxes[i], i % 3);
                question_check_boxes[i].Click += QuestionCheckBox_Click;

                questionsUC[i] = new TheoryQuestionUserControl(test[i], question_check_boxes[i], i, rand);
                test_view.Children.Add(questionsUC[i]);
                questionsUC[i].Visibility = Visibility.Collapsed;
                Grid.SetRow(questionsUC[i], 0);
                Grid.SetColumn(questionsUC[i], 1);
            }

            // start from first question
            current_question = 0;
            questionsUC[current_question].Visibility = Visibility.Visible;

            previous_question_button.IsEnabled = false;

            // start timer
            timerTextBlock.Text = string.Format("{0}:{1}", (time / 60).ToString().PadLeft(2, '0'), (time % 60).ToString().PadLeft(2, '0'));
            Timer.Start();
        }

        private void QuestionCheckBox_Click(object sender, RoutedEventArgs e)
        {
            // go to the chosen question

            ((CheckBox)sender).IsChecked = !((CheckBox)sender).IsChecked;

            questionsUC[current_question].Visibility = Visibility.Collapsed;
            current_question = int.Parse(((CheckBox)sender).Content.ToString()) - 1;
            questionsUC[current_question].Visibility = Visibility.Visible;

            previous_question_button.IsEnabled = !(current_question == 0);
            next_question_button.IsEnabled = !(current_question + 1 == BE.Configuration.Amount_questions);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                timerTextBlock.Text = string.Format("{0}:{1}", (time / 60).ToString().PadLeft(2, '0'), (time % 60).ToString().PadLeft(2, '0'));
            }
            else
            {
                Timer.Stop();
                EndTest();
            }
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox item in question_check_boxes)
            {
                if (item.IsChecked == false)
                {
                    if (MessageBox.Show("לא ענית על כל השאלות,\nהאם אתה בטוח שברצונך לסיים את המבחן?", "סיום מבחן", MessageBoxButton.YesNo)
                        == MessageBoxResult.Yes)
                    {
                        Timer.Stop();
                        EndTest();
                    }
                    return;
                }
            }

            if (MessageBox.Show("?האם אתה בטוח שברצונך לסיים את המבחן", "סיום מבחן", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Timer.Stop();
                EndTest();
            }
        }

        private void EndTest()
        {
            List<BE.TheoryQuestion> wrong_answers = new List<BE.TheoryQuestion>();

            int amount_wrong;

            test_view.Visibility = Visibility.Collapsed;
            end_test_view.Visibility = Visibility.Visible;

            if (iBL_Imp.TestResult(test, wrong_answers, out amount_wrong))
            {
                text_result.Text = "עברת בהצלחה!";
                refresh_button.Visibility = Visibility.Visible;
            }
            else
            {
                text_result.Text = "נכשלת...";

                text_postponement.Visibility = Visibility.Visible;

                left_tab.IsEnabled = true;
                left_tab.Opacity = 1;
                exit.IsEnabled = true;
            }

            text_points.Text = "ענית נכון על " + (BE.Configuration.Amount_questions - amount_wrong) + " מתוך " + BE.Configuration.Amount_questions;

            if (amount_wrong == 0)
                Wrong_list_view.Visibility = Visibility.Collapsed;

            Wrong_list_view.Items.Clear();
            Wrong_list_view.ItemsSource = wrong_answers;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Trainee.Pass_theory = true;
            iBL_Imp.Update_traniee(Trainee);


            sub_title.Text = "במסך זה באפשרותך להוסיף טסטים, למחוק טסטים עתידיים, לעדכן את פרטיך ועוד";
            left_tab.IsEnabled = true;
            left_tab.Opacity = 1;
            center_tab.Visibility = Visibility.Visible;
            center_tab_rec.Visibility = Visibility.Visible;
            right_tab.Visibility = Visibility.Visible;
            theory_tab.Visibility = Visibility.Collapsed;

            theory_test.Visibility = Visibility.Collapsed;
            new_test_view.Visibility = Visibility.Visible;

            exit.IsEnabled = true;
        }

        private void Next_Question_Click(object sender, RoutedEventArgs e)
        {
            questionsUC[current_question].Visibility = Visibility.Collapsed;
            current_question++;
            questionsUC[current_question].Visibility = Visibility.Visible;

            if (current_question + 1 == BE.Configuration.Amount_questions)
                next_question_button.IsEnabled = false;

            if (!previous_question_button.IsEnabled)
                previous_question_button.IsEnabled = true;
        }

        private void Previous_Question_Click(object sender, RoutedEventArgs e)
        {
            questionsUC[current_question].Visibility = Visibility.Collapsed;
            current_question--;
            questionsUC[current_question].Visibility = Visibility.Visible;

            if (current_question == 0)
                previous_question_button.IsEnabled = false;

            if (!next_question_button.IsEnabled)
                next_question_button.IsEnabled = true;
        }
    }
}
