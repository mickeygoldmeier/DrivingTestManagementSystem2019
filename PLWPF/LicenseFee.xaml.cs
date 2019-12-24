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
    /// Interaction logic for LicenseFee.xaml
    /// </summary>
    public partial class LicenseFee : UserControl
    {
        BL.IBL iBL_Imp = BL.FactoryBL.getBL();
        public LicenseFee(BE.Test test)
        {
            InitializeComponent();

            txb_Date.Text = test.Test_time.AddMonths(2).ToString("dd.MM.yyyy");
            txb_Name.Text = iBL_Imp.SearchTrainee(test.Traniee_id)[0].Last_name + " " + iBL_Imp.SearchTrainee(test.Traniee_id)[0].First_name;
            txb_ID.Text = test.Traniee_id;
            txb_Car_Type.Text = new CarTypeConvertorToString().Convert(test.Student_car_Type, null, null, null) as string + " - " + 
                                new GearboxTypeConvertorToString().Convert(iBL_Imp.SearchTrainee(test.Traniee_id)[0].Gear_used, null, null, null);
        }
    }
}
