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
    /// Interaction logic for UpdateTestWindow.xaml
    /// </summary>
    public partial class UpdateTestWindow : Window
    {
        // General variables that are initialized in the constructor
        private BE.Test Test;
        private List<BE.Criterion> Criteria_list = new List<BE.Criterion>();
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();

        public UpdateTestWindow(BE.Test test)
        {
            InitializeComponent();


            //Comparison of the general test that is represented in the test window that the constructor received
            Test = test;
            // Start the Binding 
            this.DataContext = Test;
            hour.Text = Test.Test_time.ToShortTimeString();
            date.Text = Test.Test_time.ToLongDateString();
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

            // Fill out the list of criteria according to the criteria written in the XML code
            foreach (var item in grd_criteria.Children)
            {
                if(item is StackPanel)
                    foreach (var item1 in (item as StackPanel).Children)
                        if (item1 is TextBlock)
                        {
                            BE.Criterion criterion = new BE.Criterion(BE.Score.Bad, (item1 as TextBlock).Text);
                            Criteria_list.Add(criterion);
                        }
            }
        }

        /// <summary>
        /// A function that asks the user if he is sure that he wants to exit without saving the changes
        /// </summary>
        private void Close_Window(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("האם אתה בטוח שאתה רוצה לצאת\nהשינויים שביצעת לא יישמרו?", "סגירה", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// A function that changes the color of the button that represents the test result
        /// </summary>
        private void Result_Change(object sender, RoutedEventArgs e)
        {
            if (tgb_result.IsChecked == true)
                BeginStoryboard(this.FindResource("to_green") as Storyboard);
            else
                BeginStoryboard(this.FindResource("to_red") as Storyboard);
        }

        /// <summary>
        /// A function that is triggered when a criterion is changed in the criterion and also changes within the list
        /// </summary>
        private void Update_criterion(object sender, RoutedEventArgs e)
        {
            // Get the criterion from the list
            BE.Criterion criterion = Criteria_list[Grid.GetRow(sender as ListBox)];

            // Update the criterion according to what was entered by the user
            switch ((sender as ListBox).SelectedIndex)
            {
                case 2:
                    criterion.Score = BE.Score.Bad;
                    break;
                case 1:
                    criterion.Score = BE.Score.OK;
                    break;
                case 0:
                    criterion.Score = BE.Score.Good;
                    break;
                default:
                    break;
            }

            // Enter the criteria back to the list
            Criteria_list[Grid.GetRow(sender as ListBox)] = criterion;

            // Update the test result according to the new criterion
            tgb_result.IsChecked = iBL_Imp.Pass_Test(Criteria_list);
        }

        private void Save_changes(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in grd_criteria.Children)
                    if (item is ListBox)
                        if ((item as ListBox).SelectedItems.Count == 0)
                            throw new Exception("שגיאה בהזנת מרכיבי מבחן: עליך להזין ציון בכל מרכיב לפני שתוכל לשמור את המידע");

                if (Test.Tester_comment == "")
                    throw new Exception("שגיאה בהזנת הערת בוחן: שדה הערת הבוחן הוא שדה חובה. אנא הכנס הערה");

                Test.Grade = (bool)tgb_result.IsChecked;
                Test.Criteria_list = Criteria_list;

                iBL_Imp.Update_test(Test);
                MessageBox.Show("הטסט עודכן בהצלחה");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
