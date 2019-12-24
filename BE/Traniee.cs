using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee
    {
        // Fields
        private string _id;
        /// <summary>
        /// ID Property. If you try to place an identity number that does not contain 9 digits, an exception will be thrown
        /// </summary>
        public string ID
        {
            get { return _id; }
            set
            {
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

        private DateTime _birth_date;
        /// <summary>
        /// Birth Date Property. If the traniee age is smaller  than the range, an exception will be thrown
        /// </summary>
        public DateTime Birth_date
        {
            get { return _birth_date; }
            set
            {
                // The Traniee is too small
                if ((DateTime.Today - value) < new TimeSpan(BE.Configuration.Minimum_Trainee_age * 365, 0, 0, 0))
                    throw new Exception("תאריך לא חוקי: הנבחן לא יכול להיות בן פחות מ" + Configuration.Minimum_Trainee_age);

                _birth_date = value;
            }
        }

        private Car_type _learned;
        /// <summary>
        /// The type of vehicle in which the traniee learned on
        /// </summary>
        public Car_type Learned
        {
            get { return _learned; }
            set { _learned = value; }
        }

        private Gearbox_type _gear_used;
        /// <summary>
        /// The type of gear in which the traniee learned on
        /// </summary>
        public Gearbox_type Gear_used
        {
            get { return _gear_used; }
            set { _gear_used = value; }
        }

        private string _driving_school;
        /// <summary>
        /// driving school Property. If the name of the school is null, an exception will be thrown
        /// </summary>
        public string Driving_school
        {
            get { return _driving_school; }
            set
            {
                if (value == "" || value == null)
                    throw new Exception("שם בית הספר לנהיגה לא תקין: השם לא יכול להיות ריק");
                _driving_school = value;
            }
        }

        private string _teachers_name;
        /// <summary>
        /// driving school Property. If the name of the school is null, an exception will be thrown
        /// </summary>
        public string Teachers_name
        {
            get { return _teachers_name; }
            set
            {
                if (value == "" || value == null)
                    throw new Exception("שם המורה לנהיגה לא תקין: השם לא יכול להיות ריק");
                _teachers_name = value;
            }
        }

        private int _num_of_classes;
        /// <summary>
        /// number of classes Property. If the number of the classes is less then 0, an exception will be thrown
        /// </summary>
        public int Num_of_classes
        {
            get { return _num_of_classes; }
            set
            {
                if (value < 0)
                    throw new Exception("מספר השיעורים לא תקין: המספר לא יכול להיות קטן מ-0");
                _num_of_classes = value;
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
                if(value.Length < 5)
                    throw new Exception("סיסמה לא תקינה: הסיסמה חייבת להכיל 5 תווים לפחות");
                _password = value;
            }
        }

        private string _email;
        /// <summary>
        /// Email Property. If the email dont contains @ and ., an exception will be thrown
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                if(!(value.Contains("@") && value.Contains(".")))
                    throw new Exception("כתובת אימייל לא תקינה: כתובת האימייל חייבת להיות כתובה כתובה בצורה תיקנית");
                _email = value;
            }
        }

        private bool _pass_theory;
        /// <summary>
        /// pass theory Property. Has the student passed his theory test
        /// </summary>
        public bool Pass_theory
        {
            get { return _pass_theory; }
            set { _pass_theory = value; }
        }

        private DateTime _last_theory;
        /// <summary>
        /// last theory Property. The last time the student took a theory test
        /// </summary>
        public DateTime Last_theory
        {
            get { return _last_theory; }
            set { _last_theory = value; }
        }

        //------------------------------------------------------------------------------------------------------------------------

        // Function
        ///<summary>
        /// Simple constractor
        /// </summary>

        public Trainee(string id, string last_name, string first_name, DateTime birth_date, Gender gender, string phone, Address address, bool pass_theory,
                       Car_type learned,Gearbox_type gear_used,string school_name,string teachers_name,int num_of_classes, string password, string email)
        {
            ID = id;
            Last_name = last_name;
            First_name = first_name;
            Birth_date = birth_date;
            Gender = gender;
            Phone = phone;
            Address = address;
            Pass_theory = pass_theory;
            Last_theory = birth_date; // give him a starting value
            Learned = learned;
            Gear_used = gear_used;
            Driving_school = school_name;
            Teachers_name = teachers_name;
            Num_of_classes = num_of_classes;
            Password = password;
            Email = email;
        }

        ///<summary>
        /// Copy constructor
        /// </summary>
        public Trainee(Trainee other)
        {
            ID = other.ID;
            Last_name = other.Last_name;
            First_name = other.First_name;
            Birth_date = other.Birth_date;
            Gender = other.Gender;
            Phone = other.Phone;
            Address = other.Address;
            Pass_theory = other.Pass_theory;
            Last_theory = other.Last_theory;
            Learned = other.Learned;
            Gear_used = other.Gear_used;
            Driving_school = other.Driving_school;
            Teachers_name = other.Teachers_name;
            Num_of_classes = other.Num_of_classes;
            Password = other.Password;
            Email = other.Email;
        }

        /// <summary>
        /// To String Function
        /// </summary>
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
            str += "\n" + "בית ספר לנהיגה: " + Driving_school;
            str += "\n" + "שם המורה: " + Teachers_name;
            str += "\n" + "מספר שיעורים: " + Num_of_classes;

            // Add the specialization of the tester
            str += "\n" + "למד על: ";
            switch (Learned)
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
            str += "\n" + "תיבת הילוכים: ";
            if (Gear_used == Gearbox_type.Manual)
                str += "ידנית";
            else
                str += "אוטומטית";



            // Return the final string
            return str;
        }

    }
}
