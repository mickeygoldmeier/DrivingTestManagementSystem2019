using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DataSource
    {
        static bool[,] array = { { true, true, true, true, true},
                                 { true, true, true, true, true},
                                 { true, true, true, true, true},
                                 { true, true, true, true, true},
                                 { true, true, true, true, true},
                                 { true, true, true, true, true} };



        /// <summary>
        /// A static list that contains all the testers
        /// </summary>
        public static List<BE.Tester> Testers_list = new List<BE.Tester>()
        { new BE.Tester("123456789", "בוזגלו", "עומר", new DateTime(1970, 5, 3), BE.Gender.Male, "0506412874", new BE.Address("שזר", 15, "תל אביב"), 200, 5, BE.Car_type.Private_car, array, 1000, "12345"),
          new BE.Tester("987654321", "יצחקי", "עדנה", new DateTime(1972, 5, 3), BE.Gender.Female, "0506456874", new BE.Address("נחל נועם", 5, "בית שמש"), 21, 5, BE.Car_type.Two_wheeled_vehicle, array, 1000, "54321"),
          new BE.Tester("222222222", "דוד", "חיים", new DateTime(1957, 5, 3), BE.Gender.Male, "0506456874", new BE.Address("עין נטפים", 10, "אילת"), 21, 5, BE.Car_type.Heavy_truck, array, 1000, "22222"),
          new BE.Tester("111111111", "כהן", "רווית", new DateTime(1963, 5, 3), BE.Gender.Female, "0506456874", new BE.Address("דוד פינסקי", 29, "תל אביב"), 21, 5, BE.Car_type.Medium_truck, array, 990, "11111")};

        /// <summary>
        /// A static list that contains all the trainees
        /// </summary>
        public static List<BE.Trainee> Trainees_list = new List<BE.Trainee>()
        { new BE.Trainee("123456789", "כהן", "מקס", new DateTime(1999, 5, 6), BE.Gender.Male, "0504587563", new BE.Address("הועד הלאומי", 21, "ירושלים"),true ,BE.Car_type.Private_car, BE.Gearbox_type.Manual, "משה ובניו", "ד\"ר אהרון ניימן",  25, "12345", "dab13568@hotmail.com"),
          new BE.Trainee("987654321", "כהן", "יצחק", new DateTime(1999, 5, 6), BE.Gender.Male, "0504585563", new BE.Address("הועד הלאומי", 21, "ירושלים"),true ,BE.Car_type.Private_car, BE.Gearbox_type.Manual, "גלגלים", "ד\"ר אהרון ניימן",  25, "54321", "natanmanor@gmail.com"),
          new BE.Trainee("111111111", "בן חיים", "יצחק", new DateTime(1999, 5, 6), BE.Gender.Male, "0504585563", new BE.Address("הועד הלאומי", 21, "ירושלים"),true ,BE.Car_type.Two_wheeled_vehicle, BE.Gearbox_type.Manual, "יצחק ובניו", "ד\"ר אהרון ניימן",  25, "54321", "natanmanor@gmail.com"),
          new BE.Trainee("222222222", "בן עמי", "יצחק", new DateTime(1999, 5, 6), BE.Gender.Male, "0504585563", new BE.Address("הועד הלאומי", 21, "ירושלים"),true ,BE.Car_type.Private_car, BE.Gearbox_type.Manual, "מנשה ובניו", "ד\"ר אהרון ניימן",  25, "54321", "natanmanor@gmail.com") };


        /// <summary>
        /// A static list that contains all the tests
        /// </summary>
        public static List<BE.Test> Tests_list = new List<BE.Test>()
        { 
          new BE.Test("FB240C8F", "987654321", "123456789", new DateTime(2018, 1, 24, 14, 0, 0), new BE.Address("הבנאי", 5, "נהריה"),  "נוהג טוב בסך הכל, לא עפתי מה שנקרא. יכול להשתפר קצת יותר בלהתייחס לתמוררים", false, BE.Car_type.Medium_truck),
          new BE.Test("FG5400DF", "123456789", "123456789", new DateTime(2018, 1, 24, 14, 0, 0), new BE.Address("הבנאי", 5, "נהריה"),  "תלמיד מצטיין", true, BE.Car_type.Heavy_truck),
          new BE.Test("FB940C8F", "987654321", "123456789", new DateTime(2018, 1, 24, 14, 0, 0), new BE.Address("הבנאי", 5, "נהריה"),  "נוהג טוב בסך הכל, לא עפתי מה שנקרא. יכול להשתפר קצת יותר בלהתייחס לתמוררים", true, BE.Car_type.Two_wheeled_vehicle),
          new BE.Test("5B940C8F", "987654321", "123456789", new DateTime(2018, 1, 24, 14, 0, 0), new BE.Address("הבנאי", 5, "נהריה"),  "נוהג טוב בסך הכל, לא עפתי מה שנקרא. יכול להשתפר קצת יותר בלהתייחס לתמוררים", true, BE.Car_type.Heavy_truck),
          new BE.Test("548FAB5B","123456789","987654321", new DateTime(2018, 12, 26, 12, 0, 0), new BE.Address("הועד הלאומי", 21, "ירושלים"), " ילד תחת לך קיבינמט על אופניים לא הייתי נותן לך רשיון בבימבה אתה היית עושה תאונה לא הייתי נותן לך להכנס לרכב שלי כנוסע", false,BE.Car_type.Private_car)
        };

        /// <summary>
        /// A static list that contains all the thery questions
        /// </summary>
        public static List<BE.TheoryQuestion> TQ = new List<BE.TheoryQuestion>()
        {new BE.TheoryQuestion("1+1=?","2",new string[]{"1","3","4"},@"pack://application:,,,/DS;component/questin_images/two.jpg"),
         new BE.TheoryQuestion("1+2=?","3",new string[]{"2","5","4"},@"pack://application:,,,/DS;component/questin_images/two.jpg"),
         new BE.TheoryQuestion("1+3=?","4",new string[]{"3","7","5"}),
         new BE.TheoryQuestion("1+4=?","5",new string[]{"1","3","6"}),
         new BE.TheoryQuestion("1+5=?","6",new string[]{"2","3","4"})
        };
    }
}
