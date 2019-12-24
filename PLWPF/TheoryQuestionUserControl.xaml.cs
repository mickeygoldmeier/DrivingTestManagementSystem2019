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
    /// Interaction logic for TheoryQuestionUserControl.xaml
    /// </summary>
    public partial class TheoryQuestionUserControl : UserControl
    {
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();
        private RadioButton[] options;
        private BE.TheoryQuestion theory_question;
        private CheckBox check_box;

        public TheoryQuestionUserControl(BE.TheoryQuestion tq, CheckBox cb, int index, Random rand)
        {
            InitializeComponent();

            theory_question = tq;
            check_box = cb;
            options = new RadioButton[] { option1, option2, option3, option4 };

            questin.Text = "שאלה " + (index + 1) + ": " + theory_question.Question;
            string[] QandA = iBL_Imp.GetOptions(theory_question, rand);

            for (int i = 0; i < options.Length; i++)
            {
                options[i].Content = QandA[i];
                options[i].Checked += Option_Checked;
            }

            if (theory_question.Image_code != null)
            {
                question_image.Source = iBL_Imp.StringToBitmap(theory_question.Image_code);
                question_image.Visibility = Visibility.Visible;
            }
        }

        private void Option_Checked(object sender, RoutedEventArgs e)
        {
            theory_question.Student_answer = ((RadioButton)sender).Content.ToString();

            if (check_box.IsChecked == true)
                return;
            check_box.IsChecked = true;
        }
    }
}
