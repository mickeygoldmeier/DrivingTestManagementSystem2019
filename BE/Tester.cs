using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Tester 
    {
        // Fields
        private string _id;
        /// <summary>
        /// ID Property. If you try to place an identity number that does not contain 9 digits, an exception will be thrown
        /// </summary>
        public string ID
        {
            get { return _id; }
            set {
                if (value.Length != 9)
                    throw new Exception("מספר זהות לא תקין: מספר הזהות חייב להכיל 9 ספרות בדיוק");
                _id = value;
                }
        }

        private string _last_name;
        /// <summary>
        /// Last name Property. If you try to place an null string, an exception will be thrown
        /// </summary>
        public string Last_name
        {
            get { return _last_name; }
            set
            {
                if (value == "" || value == null)
                    throw new Exception("שם משפחה לא תקין: שם המשפחה לא יכול להיות ריק");
                _last_name = value;
            }
        }

        private string _first_name;
        /// <summary>
        /// First name Property. If you try to place an null string, an exception will be thrown
        /// </summary>
        public string First_name
        {
            get { return _first_name; }
            set
            {
                if (value == "" || value == null)
                    throw new Exception("שם פרטי לא תקין: השם הפרטי לא יכול להיות ריק");
                _first_name = value;
            }
        }

        private DateTime _birth_date;
        /// <summary>
        /// Birth Date Property. If the tester age is smaller or larger than the range, an exception will be thrown
        /// </summary>
        public DateTime Birth_date
        {
            get { return _birth_date; }
            set
            {
                // The tester is too small
                if (DateTime.Now.Year - value.Year < Configuration.Minimum_tester_age)
                    throw new Exception("תאריך לא חוקי: הבוחן לא יכול להיות בן פחות מ" + Configuration.Minimum_tester_age);

                // The tester is too big
                if (DateTime.Now.Year - value.Year > Configuration.Maximum_tester_age)
                    throw new Exception("תאריך לא חוקי: הבוחן לא יכול להיות בן יותר מ" + Configuration.Maximum_tester_age);

                _birth_date = value;
            }
        }

        private Gender _gender;
        /// <summary>
        /// Gender Property
        /// </summary>
        public Gender Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private string _phone;
        /// <summary>
        /// Phone Property. If the phone number is too short / too long, an exception will be thrown
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set
            {
                // The number is too short
                if (value.Length < 9)
                    throw new Exception("מספר טלפון לא תקין: מספר טלפון לא יכול להכיל פחות מ9 ספרות");

                // The number is too long
                if (value.Length > 10)
                    throw new Exception("מספר טלפון לא תקין: מספר טלפון לא יכול להכיל יותר מ10 ספרות");

                _phone = value;
            }
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

        private int _years_of_experience;
        /// <summary>
        /// Years of experience Property. If you try to place an negative number, an exception will be thrown
        /// </summary>
        public int Years_of_experience
        {
            get { return _years_of_experience; }
            set
            {
                if (value < 0)
                    throw new Exception("מספר שנות ניסיון לא תקין: מספר שנים לא יכול להיות שלילי");
                _years_of_experience = value;
            }
        }

        private int _maximum_tests;
        /// <summary>
        /// Maximum number of tests per week. If you try to place an negative number, an exception will be thrown
        /// </summary>
        public int Maximum_tests
        {
            get { return _maximum_tests; }
            set
            {
                if (value < 0)
                    throw new Exception("מספר מבחנים לא תקין: מספר מבחנים לא יכול להיות שלילי");
                _maximum_tests = value;
            }
        }

        private Car_type _specialization;
        /// <summary>
        /// The type of vehicle in which the examiner specializes
        /// </summary>
        public Car_type Specialization
        {
            get { return _specialization; }
            set { _specialization = value; }
        }

        private bool[,] _schedule;
        /// <summary>
        /// The tester's schedule. Receives a two-dimensional array [,] of hours and if the array is not 5X6, an exception will be thrown
        /// </summary>
        public bool[,] Schedule
        {
            get { return _schedule; }
            set
            {
                // Check the length of the "value" array
                int y = value.GetLength(0), x = value.GetLength(1);
                if (x != 5)
                    throw new Exception("מערכת שעות לא תקינה: מספר הימים במערכת חייב להיות 5");
                if (y != 6)
                    throw new Exception("מערכת שעות לא תקינה: מספר שעות העבודה במערכת חייב להיות 6 (בין 9:00 עד 15:00)");

                // Places all values by deep placement
                _schedule = new bool[6, 5];
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 5; j++)
                        _schedule[i, j] = value[i, j];                    
            }
        }

        private double _maximum_distance;
        /// <summary>
        /// Maximum distance from the tester's address. If you try to place an negative number, an exception will be thrown
        /// </summary>
        public double Maximum_distance
        {
            get { return _maximum_distance; }
            set
            {
                if (value < 0)
                    throw new Exception("מרחק מקסימלי מהבוחן לא תקין: מרחק לא יכול להיות שלילי");
                _maximum_distance = value;
            }
        }

        private string _password;
        /// <summary>
        /// Password Property. If the length of the password is less then 5, an exception will be thrown
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (value.Length < 5)
                    throw new Exception("סיסמה לא תקינה: הסיסמה חייבת להכיל 5 תווים לפחות");
                _password = value;
            }
        }



        //------------------------------------------------------------------------------------------------------------------------

        // Function

        /// <summary>
        /// Simple constractor
        /// </summary>
        /// <param name="id">String that indicate the tester's identity number</param>
        /// <param name="last_name">String that indicates the surname of the examiner</param>
        /// <param name="first_name">String that indicates the test's first name</param>
        /// <param name="birth_date">DateTime that indicates the tester's date of birth</param>
        /// <param name="gender">Gender (enum) that indicates the sex of the tester</param>
        /// <param name="phone">String that indicates the tester's phone</param>
        /// <param name="address">Address that indicates the tester's address</param>
        /// <param name="years_of_experience">Int that indicates the number of years of experience of the tester</param>
        /// <param name="maximum_tests">Int  that indicates the maximum possible number of tests per week</param>
        /// <param name="specialization">Car_type (enum) that indicates the type of vehicle in which the tester specializes</param>
        /// <param name="schedule">Bool[,]  that includes the schedule of the tester (only round hours, between Sunday to Thursday, 9:00-15:00)</param>
        /// <param name="maximum_distance">Double of maximum distance from its address, which a tester can examine</param>
        /// <param name="password"></param>
        public Tester(string id, string last_name, string first_name, DateTime birth_date, Gender gender, string phone, Address address,
                       int years_of_experience, int maximum_tests, Car_type specialization, bool[,] schedule, double maximum_distance, string password)
        {
            ID = id;
            Last_name = last_name;
            First_name = first_name;
            Birth_date = birth_date;
            Gender = gender;
            Phone = phone;
            Address = address;
            Years_of_experience = years_of_experience;
            Maximum_tests = maximum_tests;
            Specialization = specialization;
            Schedule = schedule;
            Maximum_distance = maximum_distance;
            Password = password;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public Tester(Tester other)
        {
            ID = other.ID;
            Last_name = other.Last_name;
            First_name = other.First_name;
            Birth_date = other.Birth_date;
            Gender = other.Gender;
            Phone = other.Phone;
            Address = other.Address;
            Years_of_experience = other.Years_of_experience;
            Maximum_tests = other.Maximum_tests;
            Specialization = other.Specialization;
            Schedule = other.Schedule;
            Maximum_distance = other.Maximum_distance;
            Password = other.Password;
        }

        /// <summary>
        /// To String Function
        /// </summary>
        /// <returns>Returns all tester data (except its schedule) within one string</returns>
        public override string ToString()
        {
            string str = First_name + " " + Last_name;
            str += "\n" + "מספר זהות: " + ID;

            // Add the gender of the tester
            str += "\n" + "מין: ";
            if (Gender == Gender.Female)
                str += "אישה";
            else
                str += "גבר";

            str += "\n" + "תאריך לידה: " + Birth_date.Day + "/" + Birth_date.Month + "/" + Birth_date.Year;
            str += "\n" + "מספר טלפון: " + Phone;
            str += "\n" + "כתובת מגורים: " + Address.ToString();
            str += "\n" + "מספר שנות ניסיון: " + Years_of_experience;
            str += "\n" + " מספר המבחנים השבועי המקסימלי: " + Maximum_tests;

            // Add the specialization of the tester
            str += "\n" + "התמחות: ";
            switch (Specialization)
            {
                case Car_type.Private_car:
                    str += "רכב פרטי";
                    break;
                case Car_type.Two_wheeled_vehicle:
                    str += "רכב דו-גלגלי";
                    break;
                case Car_type.Medium_truck:
                    str += "משאית במשקל בינוני";
                    break;
                case Car_type.Heavy_truck:
                    str += "משאית במשקל כבד";
                    break;
                default:
                    str += "-- שגיאה --";
                    break;
            }

            str += "\n" + "מרחק מקסימלי מבית הבוחן על מנת לקיים טסט: " + Maximum_distance;

            // Return the final string
            return str;
        }

    }
}
