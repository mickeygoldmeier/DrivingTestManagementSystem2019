using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Threading;
using System.Windows.Media.Imaging;

namespace BL
{
    class IBL_imp : IBL
    {
        DAL.IDAL Dal = DAL.FactoryDal.getDal();

        /// <summary>
        /// Tester functions
        /// </summary>
        public void Add_tester(BE.Tester tester)
        {
            if ((DateTime.Today - tester.Birth_date) < new TimeSpan(BE.Configuration.Minimum_tester_age * 365, 0, 0, 0))
                throw new Exception("שגיאה בהכנסת בוחן: הבוחן לא בגיל המתאים");
            Dal.Add_tester(tester);
        }

        public void Remove_tester(BE.Tester tester)
        {
            if (groupByTesterFilled(tester)[1].Count != 0)
                throw new Exception("בעיה במחיקת משתמש: יש לך מבחנים שלא מילאת או שלא עשית במערכת");
            Dal.Remove_tester(tester);
        }

        public void Update_tester(BE.Tester tester)
        {
            if ((DateTime.Today - tester.Birth_date) < new TimeSpan(BE.Configuration.Minimum_tester_age * 365, 0, 0, 0))
                throw new Exception("שגיאה בעדכון בוחן: הבוחן לא בגיל המתאים");
            Dal.Update_tester(tester);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Traniee functions
        /// </summary>
        public void Add_traniee(BE.Trainee trainee)
        {
            if ((DateTime.Today - trainee.Birth_date) < new TimeSpan(BE.Configuration.Minimum_Trainee_age * 365, 0, 0, 0))
                throw new Exception("שגיאה בהכנסת תלמיד: התלמיד לא בגיל הנצרך");
            Dal.Add_traniee(trainee);
        }

        public void Remove_traniee(BE.Trainee trainee)
        {
            if (groupByTraineePass(trainee)[2].Count != 0)
            {
                throw new Exception("בעיה במחיקת משתמש: יש לך מבחנים עתידיים במערכת"); ;
            }
            Dal.Remove_traniee(trainee);

        }

        public void Update_traniee(BE.Trainee trainee)
        {
            if ((DateTime.Today - trainee.Birth_date) < new TimeSpan(BE.Configuration.Minimum_Trainee_age * 365, 0, 0, 0))
                throw new Exception("שגיאה בעדכון תלמיד: התלמיד לא בגיל הנצרך");
            Dal.Update_traniee(trainee);
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Test functions
        /// </summary>
        public void Add_test(BE.Test test)
        {
            List<BE.Trainee> trainees = SearchTrainee(test.Traniee_id, true, false, false);
            List<BE.Test> tests = SearchTest(test.Traniee_id, false, true, false);
            conditionDelegate condition = (x) => { return x.Student_car_Type == test.Student_car_Type; };

            //makes sure the student exists
            if (trainees.Count == 0)
                throw new Exception("שגיאה בקבלת נתונים: התלמיד לא קיים במערכת");

            //checks if the student took at least 20 lessons
            if (trainees[0].Num_of_classes < BE.Configuration.Minimun_lessons)
                throw new Exception("שגיאה: התלמיד לא עשה מספיק שיעורים");

            //checks if the date passed
            if (test.Test_time < DateTime.Now)
                throw new Exception("שגיאה בקביעת טסט: התאריך עבר כבר");

            //checks if the student as a test in the last 7 days 
            foreach (BE.Test item in tests)
                if ((test.Test_time - item.Test_time) < new TimeSpan(BE.Configuration.Time_between_tests, 0, 0, 0))
                    throw new Exception("שגיאה: אי אפשר לקבוע עוד מבחן תוך זמן כלכך קצר");

            //checks the test to see if the student past the test on the car type
            foreach (BE.Test item in tests)
            {
                if (condition(item) && item.Grade)
                    throw new Exception("שגיאה: עברת כבר מבחן על סוג רכב זה");
                if (condition(item) && item.Criteria_list.Count == 0)
                    throw new Exception("שגיאה: עשית כבר מבחן על סוג רכב זה, חכה לתשובה");
            }

            Func<BE.Test, List<BE.Tester>> find_testers = Find_testers;

            //finds the tester that can work that day
            List<BE.Tester> testers = find_testers(test);

            //if couldnt find any testers he searches for the next time that does have testers
            if (testers.Count == 0)
            {
                DateTime time_new = test.Test_time;
                while (testers.Count == 0)
                {
                    if (time_new.Hour + 1 > 14)
                        time_new = time_new.AddHours(19);
                    else
                        time_new = time_new.AddHours(1);
                    if (time_new.DayOfWeek == DayOfWeek.Friday)
                        time_new = time_new.AddDays(2);

                    test.Test_time = time_new;
                    testers = find_testers(test);
                }
                // offers a diffrent date for the test
                throw new Exception("שגיאה בקביעת מבחן: לא נמצא בוחן פנוי בזמן המבוקש\nמומלץ לנסות את התאריך - " + time_new.ToString());
            }

            //if found testers takes the first and adds the test to the list
            test.Tester_id = testers[0].ID;
            Dal.Add_test(test);
        }

        /// <summary>
        /// help func to find testers that match the criterion
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        private List<BE.Tester> Find_testers(BE.Test test)
        {
            //gets all testers in the distance
            List<BE.Tester> testers1 = Distance(test.Address);
            conditionDelegateTester condition = (y) => { return y.Count == 0; };

            //checks if list is empty
            if (condition(testers1))
                return testers1;

            //find all testers that specialize in the same car type
            testers1 = (from item in testers1
                        where item.Specialization == test.Student_car_Type
                        select new BE.Tester(item)).ToList();

            //checks if list is empty
            if (condition(testers1))
                throw new Exception("שגיאה: אין בוחן לסוג רכב זה באיזורך, נסה להתחיל מכתובת שונה.");



            //find the testers that are free at the time
            testers1 = Testers_Free_at_Time(test.Test_time, testers1);

            //checks if list is empty
            if (condition(testers1))
                return testers1;


            //find all testers that didnt pass the amount of tests a week and removes a tester that is busy at that day
            foreach (BE.Tester item in testers1)
            {
                //bigenning and end of the test week
                DateTime sunday = test.Test_time.AddDays(-(double)test.Test_time.DayOfWeek);
                DateTime saterday = sunday.AddDays(6);
                //list of all the testers tests
                List<BE.Test> tests_tester = SearchTest(item.ID, true, false, false);

                //find all the tests in the list that are in the same week
                var result1 = from item1 in tests_tester
                              where item1.Test_time <= saterday && item1.Test_time >= sunday
                              select item1;

                //counts the tests and checks if the specific time is busy
                int count = 0;
                foreach (var item1 in result1)
                    count++;

                if (count >= item.Maximum_tests)
                    testers1.Remove(item);
            }
            return testers1;
        }

        public void Update_test(BE.Test test)
        {
            List<BE.Test> tests = SearchTest(test.Test_number, false, false, true);
            if (tests.Count == 0)
                throw new Exception("שגיאת עדכון: המבחן לא קיים במערכת");
            if (test.Criteria_list.Count == 0 || test.Tester_comment == null)
                throw new Exception("שגיאת עדכון: לא מילאת את כל הפרטים");
            Dal.Update_test(test);
        }

        public void Remove_test(BE.Test test)
        {
            Dal.Remove_test(test);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// theory functions
        /// </summary>
        /// <param name="theoryQuestion"></param>
        public void Add_theory_q(BE.TheoryQuestion theoryQuestion)
        {
            if (theoryQuestion.Question == null)
                throw new Exception("שגיאה בהוספת שאלה: השאלה לא יכולה להיות ריקה. ");
            Dal.Add_theory_q(theoryQuestion);
        }
        public void Remove_theory_q(BE.TheoryQuestion theoryQuestion)
        {
            Dal.Remove_theory_q(theoryQuestion);
        }


        /// <summary>
        /// Configuration update
        /// </summary>
        public void UpdateConfigurationFile()
        {
            Dal.UpdateConfigurationFile();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// List getters
        /// </summary>
        public List<BE.Tester> GetTesters()
        {
            return Dal.GetTesters();
        }

        public List<BE.Trainee> GetTrainees()
        {
            return Dal.GetTrainees();
        }

        public List<BE.Test> GetTests()
        {
            return Dal.GetTests();
        }

        public List<BE.TheoryQuestion> GetTQ()
        {
            return Dal.GetTQ();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Search the list of testers by identity number and / or by first name and / or by last name
        /// </summary>
        /// <param name="value">The value you want to search</param>
        /// <param name="By_id">Do you want to search by ID?</param>
        /// <param name="By_first_name">Do you want to search by first name?</param>
        /// <param name="By_last_name">Do you want to search by last name?</param>
        /// <returns>A list that contains one varuble or a list of the members containing the resulting value</returns>
        public List<BE.Tester> SearchTester(string value, bool By_id = true, bool By_first_name = true, bool By_last_name = true)
        {
            // create the list to return
            List<BE.Tester> list = new List<BE.Tester>();
            List<BE.Tester> testers = Dal.GetTesters();

            list = (from item in testers
                    where (item.ID.Contains(value) && By_id) || (By_first_name && item.First_name.Contains(value)) || (By_last_name && item.Last_name.Contains(value))
                    select new BE.Tester(item)).ToList();

            return list;
        }

        /// <summary>
        /// Search the list of trainees by identity number and / or by first name and / or by last name
        /// </summary>
        /// <param name="value">The value you want to search</param>
        /// <param name="By_id">Do you want to search by ID?</param>
        /// <param name="By_first_name">Do you want to search by first name?</param>
        /// <param name="By_last_name">Do you want to search by last name?</param>
        /// <returns>A list that contains one varuble or a list of the members containing the resulting value</returns>
        public List<BE.Trainee> SearchTrainee(string value, bool By_id = true, bool By_first_name = true, bool By_last_name = true)
        {
            // create the list to return
            List<BE.Trainee> list = new List<BE.Trainee>();
            List<BE.Trainee> trainee = Dal.GetTrainees();

            list = (from item in trainee
                    where (item.ID.Contains(value) && By_id) || (By_first_name && item.First_name.Contains(value)) || (By_last_name && item.Last_name.Contains(value))
                    select new BE.Trainee(item)).ToList();

            // return the final list 
            return list;
        }

        /// <summary>
        /// Search the list of tests by the identity number of the tester and / or according to the identity number of the trainee and / or test number
        /// </summary>
        /// <param name="value">The value you want to search</param>
        /// <param name="By_tester_id">Do you want to search by tester ID?</param>
        /// <param name="By_trainee_id">Do you want to search by student ID?</param>
        /// <param name="By_test_number">Do you want to search by test number?</param>
        /// <returns></returns>
        public List<BE.Test> SearchTest(string value, bool By_tester_id = true, bool By_trainee_id = true, bool By_test_number = true)
        {
            List<BE.Test> list = new List<BE.Test>();
            List<BE.Test> tests = Dal.GetTests();

            list = (from item in tests
                    where (item.Tester_id.Contains(value) && By_tester_id) || (item.Traniee_id.Contains(value) && By_trainee_id) || (item.Test_number.Contains(value) && By_test_number)
                    select new BE.Test(item)).ToList();

            // return the final list 
            return list;
        }

        /// <summary>
        /// A grouping of all the testers by specialization
        /// </summary>
        /// <param name="oredered">Do you want the values within the groups to be sorted?</param>
        /// <returns>An array that each cell in the array is another group of specialization</returns>
        public List<List<BE.Tester>> GroupBySpecialization(bool oredered = false)
        {
            // get the list of the testers
            List<BE.Tester> testers = Dal.GetTesters();
            List<List<BE.Tester>> list = new List<List<BE.Tester>>();
            // if the user want the grouping to be orderd
            if (oredered)
            {
                // group the list
                var resualt = (from tester in testers
                               orderby tester.First_name
                               group tester by tester.Specialization into g
                               select new { Specialization = g.Key, testers = g });
                int i = 0;
                foreach (var item in resualt)
                {
                    List<BE.Tester> inner_list = new List<BE.Tester>();
                    list.Add(inner_list);
                    foreach (var tester in item.testers)
                        list[i].Add(tester);
                    i++;
                }

                // return the array
                return list;
            }

            // if the user dont want the grouping to be orderd
            else
            {
                // group the list
                var resualt = from tester in testers
                              group tester by tester.Specialization into g
                              select new { Specialization = g.Key, testers = g };


                // put the grouped list in the array
                int i = 0;
                foreach (var item in resualt)
                {
                    List<BE.Tester> inner_list = new List<BE.Tester>();
                    list.Add(inner_list);
                    foreach (var tester in item.testers)
                        list[i].Add(tester);
                    i++;
                }

                // return the array
                return list;
            }
        }

        /// <summary>
        /// A grouping of trainees by school
        /// </summary>
        /// <param name="oredered"></param>
        /// <returns></returns>
        public List<List<BE.Trainee>> GroupBySchool(bool oredered = false)
        {
            // get the list of the trainees
            List<BE.Trainee> trainees = Dal.GetTrainees();

            // if the user want the grouping to be orderd
            if (oredered)
            {
                // group the list
                var resualt = from trainee in trainees
                              orderby trainee
                              group trainee by trainee.Driving_school into g
                              select new { School = g.Key, trainees = g };

                // create new array. every group will be in diffrent cell
                List<List<BE.Trainee>> return_list = new List<List<BE.Trainee>>();

                // put the grouped list in the array
                int i = 0;
                foreach (var item in resualt)
                {
                    List<BE.Trainee> New_trainees = new List<BE.Trainee>();
                    return_list.Add(New_trainees);
                    foreach (var trainee in item.trainees)
                        return_list[i].Add(trainee);
                    i++;
                }
                // return the array
                return return_list;
            }

            // if the user dont want the grouping to be orderd
            else
            {
                // group the list
                var resualt = from trainee in trainees
                              group trainee by trainee.Driving_school into g
                              select new { school = g.Key, trainees = g };

                // create new array. every group will be in diffrent cell
                List<List<BE.Trainee>> return_list = new List<List<BE.Trainee>>();

                // put the grouped list in the array
                int i = 0;
                foreach (var item in resualt)
                {
                    List<BE.Trainee> New_trainees = new List<BE.Trainee>();
                    return_list.Add(New_trainees);
                    foreach (var trainee in item.trainees)
                        return_list[i].Add(trainee);
                    i++;
                }
                // return the array
                return return_list;
            }
        }

        /// <summary>
        /// A grouping of trainees by teacher
        /// </summary>
        /// <param name="oredered"></param>
        /// <returns></returns>
        public List<List<BE.Trainee>> GroupByTeacher(bool oredered = false)
        {
            //get the list of the trainees
            List<BE.Trainee> trainees = Dal.GetTrainees();

            //if the user want the grouping to be orderd
            if (oredered)
            {
                // group the list
                var resualt = from trainee in trainees
                              orderby trainee
                              group trainee by trainee.Teachers_name into g
                              select new { Teacher = g.Key, trainees = g };

                //create new list.every group will be in diffrent list
                List<List<BE.Trainee>> return_list = new List<List<BE.Trainee>>();

                // put the grouped list in the list
                int i = 0;
                foreach (var item in resualt)
                {
                    List<BE.Trainee> New_trainees = new List<BE.Trainee>();
                    return_list.Add(New_trainees);
                    foreach (var trainee in item.trainees)
                        return_list[i].Add(trainee);
                    i++;
                }
                //return the list
                return return_list;
            }

            //if the user dont want the grouping to be orderd
            else
            {
                // group the list
                var resualt = from trainee in trainees
                              group trainee by trainee.Teachers_name into g
                              select new { Teacher = g.Key, trainees = g };

                //create new list.every group will be in diffrent cell
                List<List<BE.Trainee>> return_list = new List<List<BE.Trainee>>();

                //put the grouped list in the list
                int i = 0;
                foreach (var item in resualt)
                {
                    List<BE.Trainee> New_trainees = new List<BE.Trainee>();
                    return_list.Add(New_trainees);
                    foreach (var trainee in item.trainees)
                        return_list[i].Add(trainee);
                    i++;
                }
                // return the list
                return return_list;
            }
        }

        /// <summary>
        /// A grouping of trainees by number of tests
        /// </summary>
        /// <param name="oredered"></param>
        /// <returns></returns>
        public List<List<BE.Trainee>> GroupByNumberOfTests(bool oredered = false)
        {
            //get the list of the trainees
            List<BE.Trainee> trainees = Dal.GetTrainees();

            //if the user want the grouping to be orderd
            if (oredered)
            {
                // group the list
                var resualt = from trainee in trainees
                              orderby trainee.ID
                              group trainee by Count_tests(trainee) into g
                              select new { Number = g.Key, trainees = g };

                //create new list.every group will be in diffrent list
                List<List<BE.Trainee>> return_list = new List<List<BE.Trainee>>();

                // put the grouped list in the list
                int i = 0;
                foreach (var item in resualt)
                {
                    List<BE.Trainee> New_trainees = new List<BE.Trainee>();
                    return_list.Add(New_trainees);
                    foreach (var trainee in item.trainees)
                        return_list[i].Add(trainee);
                    i++;
                }
                //return the list
                return return_list;
            }

            //if the user dont want the grouping to be orderd
            else
            {
                // group the list
                var resualt = from trainee in trainees
                              group trainee by Count_tests(trainee) into g
                              select new { Number = g.Key, trainees = g };

                //create new list.every group will be in diffrent cell
                List<List<BE.Trainee>> return_list = new List<List<BE.Trainee>>();

                //put the grouped list in the list
                int i = 0;
                foreach (var item in resualt)
                {
                    List<BE.Trainee> New_trainees = new List<BE.Trainee>();
                    return_list.Add(New_trainees);
                    foreach (var trainee in item.trainees)
                        return_list[i].Add(trainee);
                    i++;
                }
                // return the list
                return return_list;
            }
        }

        /// <summary>
        /// help tester dicide if trainee passed
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public bool Pass_Test(List<BE.Criterion> criteria)
        {
            int count = 0, sum = 0;
            foreach (BE.Criterion item in criteria)
            {
                if (item.Score == 0)
                    count++;
                sum += (int)item.Score;
            }
            if (count > 2)
                return false;
            return sum / criteria.Count > BE.Configuration.Pass_test;
        }

        /// <summary>
        /// find all testers in a cirten distance
        /// </summary>
        public List<BE.Tester> Distance(BE.Address address)
        {
            List<BE.Tester> testers = Dal.GetTesters();
            List<BE.Tester> return_testers = new List<BE.Tester>();

            Func<BE.Address, BE.Address, double> find_distance = Help_Distance;

            foreach (BE.Tester item in testers)
            {
                double distance = find_distance(address, item.Address);
                if (item.Maximum_distance >= distance)
                    return_testers.Add(item);
            }
            return return_testers;
        }

        /// <summary>
        /// find distance between to addresses
        /// </summary>
        private double Help_Distance(BE.Address first_address, BE.Address seconed_address)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    string origin = first_address.ToString(); //or "תקווה פתח 100 העם אחד "etc.
                    string destination = seconed_address.ToString();//or "גן רמת 10 בוטינסקי'ז "etc.
                    string KEY = @"5rRMSAOyU11mGgkbAlWk3C1y1y0nT2Gv";
                    string url = @"https://www.mapquestapi.com/directions/v2/route" +
                     @"?key=" + KEY +
                     @"&from=" + origin +
                     @"&to=" + destination +
                     @"&outFormat=xml" +
                     @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
                     @"&enhancedNarrative=false&avoidTimedConditions=false";
                    //request from MapQuest service the distance between the 2 addresses
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader sreader = new StreamReader(dataStream);
                    string responsereader = sreader.ReadToEnd();
                    response.Close();
                    //the response is given in an XML format
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(responsereader);
                    if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
                    //we have the expected answer
                    {
                        //display the returned distance
                        XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                        double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                        return distInMiles * 1.609344;
                    }
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }
            return BE.Configuration.Defualt_distance;
        }

        /// <summary>
        /// A function for sending an email with the student's test information
        /// </summary>
        /// <param name="test">The test you want to send by email</param>
        public void Send_Email(BE.Test test)
        {
            try
            {
                BE.Trainee trainee = SearchTrainee(test.Traniee_id, true, false, false)[0];

                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("miniproject.mmn@gmail.com");
                msg.To.Add(trainee.Email);
                msg.Subject = "נקבע לך טסט חדש בתאריך " + test.Test_time.ToLongDateString();
                msg.Body = "";

                string car_type;
                switch (test.Student_car_Type)
                {
                    case BE.Car_type.Private_car:
                        car_type = "רכב פרטי";
                        break;
                    case BE.Car_type.Two_wheeled_vehicle:
                        car_type = "רכב דו-גלגלי";
                        break;
                    case BE.Car_type.Medium_truck:
                        car_type = "משאית בינונית";
                        break;
                    case BE.Car_type.Heavy_truck:
                        car_type = "משאית כבדה";
                        break;
                    default:
                        car_type = "-- לא הוזן כלי רכב --";
                        break;
                }

                msg.IsBodyHtml = true;

                //create Alrternative HTML view
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(@"
              <html>
                <body>
                   <p dir=""rtl""><span style=""font - size: 17pt; font - family: tahoma, arial, helvetica, sans - serif; ""><strong>שלום " + trainee.First_name + " " + trainee.Last_name + @", נקבע עבורך טסט חדש!</strong></span></p>
                   <p dir = ""rtl""><span style = ""font-family: tahoma, arial, helvetica, sans-serif;""> מבחן הנהיגה שלך על " + car_type + @" יתקיים בתאריך " + test.Test_time.ToLongDateString() + @" ובשעה " + test.Test_time.ToShortTimeString() + @"</span></p>
                   <p dir = ""rtl""><span style = ""font-family: tahoma, arial, helvetica, sans-serif;""> כתובת תחילת המבחן היא " + test.Address.ToString() + @"</span></p>
                   <p dir = ""rtl""><span style = ""font-family: tahoma, arial, helvetica, sans-serif;""> ______________________________________________ </span></p>
                   <p dir = ""rtl""><span style = ""font-size: 9pt; font-family: tahoma, arial, helvetica, sans-serif;""> -נא להגיע לפחות חצי שעה לפני השעה הנקובה לכתובת ההתחלה </span></p>
                   <p dir = ""rtl""><span style = ""font-size: 9pt; font-family: tahoma, arial, helvetica, sans-serif;""> -יש להביא איתך את כל המסמכים הדרושים</ span ></ p >
                   <p dir = ""rtl""><span style = ""font-size: 9pt; font-family: tahoma, arial, helvetica, sans-serif;""> -במידה ויש בעיה (הטסטר לא מגיע בזמן או בעיות נוספות) התקשר ל02-9999999 </span></p>
                 </body>
              </html>
            ", null, "text/html");

                //Add view to the Email Message
                msg.AlternateViews.Add(htmlView);

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("miniproject.mmn@gmail.com", "miniproject_mmn123");
                client.Send(msg);
            }
            finally { }
        }

        /// <summary>
        /// adds the test to your calender
        /// </summary>
        /// <param name="test"></param>
        public void Add_To_Calender(BE.Test test)
        {
            string address = test.Address.ToString().Replace(' ', '+');
            string details = "הטסט+שלך+יהיה+ב" + test.Test_time.ToShortTimeString() + "+" + test.Test_time.ToLongDateString().Replace(' ', '+');
            details += (". נא להגיע לפחות חצי שעה לפני השעה הנקובה לכתובת ההתחלה").Replace(' ', '+');
            details += (". יש להביא איתך את כל המסמכים הדרושים").Replace(' ', '+');
            details += (". במידה ויש בעיה (הטסטר לא מגיע בזמן או בעיות נוספות) התקשר ל02-9999999.").Replace(' ', '+');
            // 20180327T090000/2010327T160000Z
            string date = test.Test_time.Year.ToString("0000") + test.Test_time.Month.ToString("00") + test.Test_time.Day.ToString("00") + "T" + test.Test_time.Hour.ToString("00") + "0000" + @"/" + test.Test_time.Year.ToString("0000") + test.Test_time.Month.ToString("00") + test.Test_time.Day.ToString("00") + "T" + (test.Test_time.Hour + 1).ToString("00") + "0000";

            string URL = @"https://www.google.com/calendar/render?action=TEMPLATE&text=יש+לך+טסט&dates=" + date + "&details=" + details + "&location=" + address + "&sf=true&output=xml";

            System.Diagnostics.Process.Start(URL);
        }

        /// <summary>
        /// returns the amount of test for a cirten trainee
        /// </summary>
        /// <param name="trainee"></param>
        /// <returns></returns>
        public int Count_tests(BE.Trainee trainee)
        {
            List<BE.Test> tests = SearchTest(trainee.ID, false, true, false);
            return tests.Count;
        }

        /// <summary>
        /// checks if trainee ever passed a test
        /// </summary>
        /// <param name="trainee"></param>
        /// <returns></returns>
        public bool Ever_passed_a_test(BE.Trainee trainee)
        {
            List<BE.Test> tests = SearchTest(trainee.ID, false, true, false);
            foreach (BE.Test item in tests)
                if (item.Grade)
                    return true;
            return false;
        }

        /// <summary>
        /// gets all the testers that are not busy at the time
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="testers"></param>
        /// <returns></returns>
        public List<BE.Tester> Testers_Free_at_Time(DateTime dateTime, List<BE.Tester> testers = null)
        {
            if (testers == null)
                testers = GetTesters();
            if (dateTime.Hour > 14 || dateTime.Hour < 9 || dateTime.DayOfWeek == DayOfWeek.Friday || dateTime.DayOfWeek == DayOfWeek.Saturday)
                throw new Exception("שגיאה בהזנת השעה של הטסט: השעות או היום אינם שעות או ימי עבודה");

            List<BE.Tester> return_testers = new List<BE.Tester>();

            // Convert the day of the date to int 
            int Day_index = 0;
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    Day_index = 4;
                    break;
                case DayOfWeek.Monday:
                    Day_index = 3;
                    break;
                case DayOfWeek.Tuesday:
                    Day_index = 2;
                    break;
                case DayOfWeek.Wednesday:
                    Day_index = 1;
                    break;
                case DayOfWeek.Thursday:
                    Day_index = 0;
                    break;
            }

            //for each tester that works at that time check if he has a test at that time if not add to a list
            foreach (BE.Tester item in testers)
            {
                bool flag = false;
                if (item.Schedule[dateTime.Hour - 9, Day_index] == true)
                {
                    flag = true;
                    List<BE.Test> tests = SearchTest(item.ID, true, false, false);
                    foreach (BE.Test test in tests)
                        if (test.Test_time == dateTime)
                            flag = false;
                }
                if (flag)
                    return_testers.Add(item);
            }
            return return_testers;
        }

        /// <summary>
        /// finds all test acoording to a cirten condition
        /// </summary>
        /// <returns></returns>
        public List<BE.Test> Tests_by_terms(conditionDelegate condition)
        {
            List<BE.Test> tests = GetTests();
            List<BE.Test> return_tests = new List<BE.Test>();

            foreach (BE.Test test in tests)
                if (condition(test))
                    return_tests.Add(test);
            return return_tests;
        }

        /// <summary>
        /// finds the sceduale of a cirten tester
        /// </summary>
        public BE.Test[,] Tester_sceduale(BE.Tester tester, DateTime dateTime)
        {
            List<BE.Test> tests = SearchTest(tester.ID, true, false, false);
            DateTime sunday = dateTime.AddDays(-(double)dateTime.DayOfWeek);
            DateTime saterday = sunday.AddDays(6);
            BE.Test[,] return_tests = new BE.Test[6, 5];

            var result = from item in tests
                         let time = item.Test_time
                         where time <= saterday && time >= sunday
                         select item;

            foreach (var item in result)
                return_tests[item.Test_time.Hour - 9, (int)item.Test_time.DayOfWeek] = item;

            return return_tests;
        }

        /// <summary>
        /// finds all tests in a cirten day
        /// </summary>
        public List<BE.Test> Tests_by_Date(DateTime dateTime)
        {
            List<BE.Test> tests = GetTests();
            List<BE.Test> return_tests = new List<BE.Test>();
            foreach (BE.Test item in tests)
                if (item.Test_time.Date == dateTime)
                    return_tests.Add(item);
            return return_tests;
        }

        /// <summary>
        /// group al students tests by grade
        /// </summary>
        public List<BE.Test>[] groupByTraineePass(BE.Trainee trainee)
        {
            List<BE.Test> tests = SearchTest(trainee.ID, false, true, false);
            // group the list
            var result = from test in tests
                         where test.Criteria_list.Count != 0
                         orderby test.Test_time
                         group test by test.Grade into g
                         orderby g.Key descending
                         select new { grade = g.Key, tests = g };

            // create new array. every group will be in diffrent cell
            List<BE.Test>[] return_list = new List<BE.Test>[3] { new List<BE.Test>(), new List<BE.Test>(), new List<BE.Test>() };

            // put the grouped list in the array

            foreach (var item in result)
            {
                foreach (var test in item.tests)
                {
                    if (test.Grade)
                        return_list[0].Add(test);

                    if (!test.Grade)
                        return_list[1].Add(test);
                }
            }
            List<BE.Test> tests1 = SearchTest(trainee.ID, false, true, false);
            var condition = from item1 in tests1
                            where item1.Criteria_list.Count == 0
                            orderby item1.Test_time
                            select item1;

            foreach (var item in condition)
                return_list[2].Add(item);

            return return_list;
        }

        /// <summary>
        /// group all tsters tests by criterion
        /// </summary>
        /// <param name="tester"></param>
        /// <returns></returns>
        public List<BE.Test>[] groupByTesterFilled(BE.Tester tester)
        {
            List<BE.Test> tests = SearchTest(tester.ID, true, false, false);
            // group the list
            var result = from test in tests
                         where test.Test_time < DateTime.Now
                         orderby test.Test_time
                         group test by test.Criteria_list.Count != 0 into g
                         orderby g.Key descending
                         select new { grade = g.Key, tests = g };

            // create new array. every group will be in diffrent cell
            List<BE.Test>[] return_list = new List<BE.Test>[3] { new List<BE.Test>(), new List<BE.Test>(), new List<BE.Test>() };

            // put the grouped list in the array
            foreach (var item in result)
            {
                foreach (var test in item.tests)
                {
                    if (test.Criteria_list.Count == 0)
                        return_list[1].Add(test);

                    if (test.Criteria_list.Count != 0)
                        return_list[0].Add(test);
                }
            }

            List<BE.Test> tests1 = SearchTest(tester.ID, true, false, false);
            var condition = from item1 in tests1
                            where item1.Test_time > DateTime.Now
                            orderby item1.Test_time
                            select item1;

            foreach (var item in condition)
                return_list[2].Add(item);
            return return_list;
        }

        public BE.Tester OldestTester()
        {
            List<BE.Tester> testers = GetTesters();
            testers = (from item in testers
                       orderby item.Birth_date
                       select item).ToList();
            return testers[0];
        }

        /// <summary>
        /// A function for sending an email with the registration information
        /// </summary>
        /// <param name="test">The test you want to send by email</param>
        public void Email_Registration(BE.Trainee trainee, string decrypted_password)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("miniproject.mmn@gmail.com");
            msg.To.Add(trainee.Email);
            msg.Subject = "ברוכים הבאים למערכת רישום לטסטים, " + trainee.First_name + "!";
            msg.Body = "";

            msg.IsBodyHtml = true;

            //create Alrternative HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(@"
              <html>
                <body>
                   <p dir=""rtl""><span style=""font - size: 17pt; font - family: tahoma, arial, helvetica, sans - serif; ""><strong>שלום " + trainee.First_name + " " + trainee.Last_name + @", נרשמת למערכת בהצלחה!</strong></span></p>
                   <p dir = ""rtl""><span style = ""font-family: tahoma, arial, helvetica, sans-serif;""> נרשמת בהצלחה למערכת הטסטים. כעת באפשרותך להירשם לטסטים, לצפות בטסטים הקודמים שלך ועוד. " + @"</span></p>
                   <p dir = ""rtl""><span style = ""font-family: tahoma, arial, helvetica, sans-serif;""> סיסמתך האישית היא " + decrypted_password + @". סיסמה זו היא סיסמה אישית, ואין להעבירה לגורמים אחרים." + @"</span></p>
                   <p dir = ""rtl""><span style = ""font-family: tahoma, arial, helvetica, sans-serif;""> ______________________________________________ </span></p>
                   <p dir = ""rtl""><span style = ""font-size: 9pt; font-family: tahoma, arial, helvetica, sans-serif;""> -במידה ויש בעיה (שכחת את הסיסמה, בעיות בהתחברות ועוד) התקשר ל02-9999999 </span></p>
                 </body>
              </html>
            ", null, "text/html");

            //Add view to the Email Message
            msg.AlternateViews.Add(htmlView);

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("miniproject.mmn@gmail.com", "miniproject_mmn123");
            client.Send(msg);
        }

        /// <summary>
        /// 
        /// </summary>
        public BE.Thery_status GetTheoryStatus(BE.Trainee trainee)
        {
            if (trainee.Pass_theory)
                return BE.Thery_status.Passed;

            if (trainee.Last_theory.AddDays(1) > DateTime.Now)
                return BE.Thery_status.Hold;

            return BE.Thery_status.NeedToDo;
        }

        /// <summary>
        /// 
        /// </summary>
        public BE.TheoryQuestion[] GetTheoryTest()
        {
            List<BE.TheoryQuestion> tq = Dal.GetTQ();
            BE.TheoryQuestion[] return_test = new BE.TheoryQuestion[BE.Configuration.Amount_questions];

            Random rand = new Random();

            for (int i = 0; i < return_test.Length; i++) // getting a random question without repeat
            {
                int index = rand.Next(0, tq.Count);
                return_test[i] = tq[index];
                return_test[i].Student_answer = "לא ענית תשובה";
                tq.RemoveAt(index);
            }

            return return_test;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="question"></param>
        /// <param name="rand"></param>
        public string[] GetOptions(BE.TheoryQuestion question, Random rand)
        {
            List<string> options = new List<string>(question.Wrong);
            options.Add(question.Answer);

            string[] random_options = new string[4];

            for (int i = 0; i < random_options.Length; i++) // getting a random options without repeat
            {
                int index = rand.Next(0, options.Count);
                random_options[i] = options[index];
                options.RemoveAt(index);
            }

            return random_options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <param name="wrong_answers"></param>
        /// <param name="amount_wrong"></param>
        public bool TestResult(BE.TheoryQuestion[] test, List<BE.TheoryQuestion> wrong_answers, out int amount_wrong)
        {
            amount_wrong = 0;

            for (int i = 0; i < test.Length; i++)
                if (test[i].Student_answer != test[i].Answer)
                {
                    amount_wrong++;
                    wrong_answers.Add(test[i]);
                }

            return amount_wrong < BE.Configuration.Fail_theory;
        }

        public string SourceToString(string source)
        {
            if (source == null)
                return "";

            byte[] imageBytes = File.ReadAllBytes(source);
            return Convert.ToBase64String(imageBytes);
        }

        public BitmapImage StringToBitmap(string str)
        {
            if (str == "")
                return null;

            string base64String = str;
            byte[] imgBytes = Convert.FromBase64String(base64String);

            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream ms = new MemoryStream(imgBytes);
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public string Encrypte(string password)
        {
            byte[] string_in_bytes = Encoding.Unicode.GetBytes(password);
            for (int i = 0, j= string_in_bytes.Length - 1; i < string_in_bytes.Length; i++,j--)
            {
                if (i < j)
                {
                    byte temp = string_in_bytes[i];
                    string_in_bytes[i] = string_in_bytes[j];
                    string_in_bytes[j] = temp;
                }
                string_in_bytes[i] *= 100;
                string_in_bytes[i] += 7;
            }
            return Convert.ToBase64String(string_in_bytes);
        }
    }
}