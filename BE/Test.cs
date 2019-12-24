using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test
    {
        private string _test_number;
        /// <summary>
        /// test_number Property
        /// </summary>
        public string Test_number
        {
            get { return _test_number; }
            set { _test_number = value; }
        }

        private string _tester_id;
        /// <summary>
        /// Tester_ID Property. If you try to place an identity number that does not contain 9 digits, an exception will be thrown
        /// </summary>
        public string Tester_id
        {
            get { return _tester_id; }
            set
            {
                if (value.Length != 9)
                    throw new Exception("מספר זהות של הבוחן לא תקין: מספר הזהות חייב להכיל 9 ספרות בדיוק");
                _tester_id = value;
            }
        }

        private string _traniee_id;
        /// <summary>
        /// Traniee_id Property. If you try to place an identity number that does not contain 9 digits, an exception will be thrown
        /// </summary>
        public string Traniee_id
        {
            get { return _traniee_id; }
            set
            {
                if (value.Length != 9)
                    throw new Exception("מספר זהות של התלמיד לא תקין: מספר הזהות חייב להכיל 9 ספרות בדיוק");
                _traniee_id = value;
            }
        }

        private DateTime _test_time;
        /// <summary>
        /// test_time Property. if the hour is not in working hours it throws in exception
        /// </summary>
        public DateTime Test_time
        {
            set {
                if (value.Hour < 9 || value.Hour >= 15)
                    throw new Exception("שעה לא תקינה: השעה לא בטווח שעות העבודה המוגדרות של הבוחן");

                if(value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Friday)
                    throw new Exception("יום לא תקין: שישי ושבת הם לא ימי עבודה");
                _test_time = value;

            }
            get { return _test_time; }
        }

        private Address _address;
        /// <summary>
        /// Address Property
        /// </summary>
        public Address Address
        {
            get { return _address; }
            set { _address = value; }
        }


        private List<Criterion> _criteria_list = new List<Criterion>();
        /// <summary>
        /// The list of criteria on which the traniee received a test result. If the list if empty, an exception will be thrown
        /// </summary>
        public List<Criterion> Criteria_list
        {
            get { return _criteria_list; }
            set
            {
                //if (value.Count == 0)
                //    throw new Exception("רשימת קריטריונים לא תקינה: לא ניתן לשמור טסט בלי קריטריונים");

                _criteria_list.Clear();
                foreach (Criterion item in value)
                    _criteria_list.Add(item);
            }
        }

        private bool _grade;
        /// <summary>
        /// grade Property 
        /// </summary>
        public bool Grade
        {
            set { _grade = value; }
            get { return _grade; }
        }

        private string _tester_comment;
        /// <summary>
        /// tester_comment Property
        /// </summary>
        public string Tester_comment
        {
            set { _tester_comment = value; }
            get { return _tester_comment; }
        }

        private Car_type _car_type;
        /// <summary>
        /// car_type Property
        /// </summary>
        public Car_type Student_car_Type
        {
            set { _car_type = value; }
            get { return _car_type; }
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// ctr Test
        /// </summary>
        /// <param name="tester_id"></param>
        /// <param name="traniee_id"></param>
        /// <param name="test_time"></param>
        /// <param name="address"></param>
        /// <param name="criteria_list"></param>
        /// <param name="teachers_comment"></param>
        /// <param name="grade"></param>
        public Test(string num, string tester_id, string traniee_id, DateTime test_time, Address address,
                       string tester_comment, bool grade, Car_type car_type)
        {
            Test_number = num;
            Tester_id = tester_id;
            Traniee_id = traniee_id;
            Test_time = test_time;
            Address = address;
            List<BE.Criterion> criteria = new List<BE.Criterion>()
        {
            new BE.Criterion(BE.Score.Bad, "כניסה חזיתית ברכב לפניך"),
            new BE.Criterion(BE.Score.OK, "היצמדות לימין"),
            new BE.Criterion(BE.Score.Good, "איתות"),
        };
            Criteria_list = criteria;
            Grade = grade;
            Tester_comment = tester_comment;
            Student_car_Type = car_type;
        }

        /// <summary>
        /// ctr test
        /// </summary>
        /// <param name="traniee_id"></param>
        /// <param name="test_time"></param>
        /// <param name="address"></param>
        public Test(string traniee_id, DateTime test_time, Address address, Car_type car_type)
        {
            Traniee_id = traniee_id;
            Test_time = test_time;
            Address = address;
            Student_car_Type = car_type;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Test(Test other)
        {
            Test_number = other.Test_number;
            Tester_id = other.Tester_id;
            Traniee_id = other.Traniee_id;
            Test_time = other.Test_time;
            Address = other.Address;
            Criteria_list = other.Criteria_list;
            Grade = other.Grade;
            Tester_comment = other.Tester_comment;
            Student_car_Type = other.Student_car_Type;
        }

        /// <summary>
        /// To String Function
        /// </summary>
        /// <returns>Returns all tests data  within one string</returns>
        public override string ToString()
        {
            string str = "מספר מבחן: " + Test_number +"/n";
            str += "מספר זהות של הבוחן: " + Tester_id + "/n";
            str += "מספר זהות של הנבחן: " + Traniee_id + "/n";
            str += "שעה ותאריך של המבחן: " + Test_time.ToString() + "/n";
            str += "כתובת של המבחן: " + Address.ToString() + "/n";

            foreach (Criterion item in Criteria_list)
                str += item.ToString() + "/n";
            if (Grade)
                str += "ציון: עובר" + "/n";
            else
                str += "ציון: נכשל" + "/n";
            str += "הערת הבוחן: " + Tester_comment + "/n";

            return str;
        }
    }
}
