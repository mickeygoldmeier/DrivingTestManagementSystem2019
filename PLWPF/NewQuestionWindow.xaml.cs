using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for NewQuestionWindow.xaml
    /// </summary>
    public partial class NewQuestionWindow : Window
    {
        private string path = null;
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();

        public NewQuestionWindow()
        {
            InitializeComponent();
        }

        private void Add_Image_Click(object sender, RoutedEventArgs e)
        {
            if (((ToggleButton)sender).IsChecked == true)
            {
                OpenFileDialog ofdPicture = new OpenFileDialog();
                ofdPicture.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
                ofdPicture.FilterIndex = 1;

                if (ofdPicture.ShowDialog() == true)
                {
                    image_display.Source = new BitmapImage(new Uri(ofdPicture.FileName));
                    path = ofdPicture.FileName;
                }
            }
            else
            {
                path = null;
                image_display.Source = null;
            }
        }

        private void Save_Question_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                iBL_Imp.Add_theory_q(new BE.TheoryQuestion(txb_question.Text, txb_answer.Text,
                    new string[] { txb_wrong_answer1.Text, txb_wrong_answer2.Text, txb_wrong_answer3.Text }, iBL_Imp.SourceToString(path)));
                this.Close();
                throw new Exception("השאלה נשמרה בהצלחה");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
