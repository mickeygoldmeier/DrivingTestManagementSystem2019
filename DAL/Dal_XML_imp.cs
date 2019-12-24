using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    class Dal_XML_imp : IDAL
    {
        // Set up the XElement and the path for the Testers
        private XElement TesterRoot;
        private string TesterPath = @"TesterPath.xml";
        private void CreateTestersFile()
        {
            TesterRoot = new XElement("Testers");
            TesterRoot.Save(TesterPath);
        }

        // Set up the XElement and the path for the Trainees
        private XElement TraineeRoot;
        private string TraineePath = @"TraineePath.xml";
        private void CreateTraineesFile()
        {
            TraineeRoot = new XElement("Trainees");
            TraineeRoot.Save(TraineePath);
        }

        // Set up the XElement and the path for the Tests
        private XElement TestRoot;
        private string TestPath = @"TestPath.xml";
        private void CreateTestFile()
        {
            TestRoot = new XElement("Tests");
            TestRoot.Save(TestPath);
        }

        private XElement TheoryRoot;
        private string TheoryPath = @"TheoryPath.xml";
        private void CreateTheoryFile()
        {
            TheoryRoot = new XElement("TheoryQuestions");
            TheoryRoot.Save(TheoryPath);
        }

        // Set up the XElement and the path for the Configuration
        private XElement ConfigurationRoot;
        private string ConfigurationPath = @"ConfigurationPath.xml";
        public void UpdateConfigurationFile()
        {

            XElement Timer_theory = new XElement("Timer_theory", BE.Configuration.Timer_theory);
            XElement Amount_questions = new XElement("Amount_questions", BE.Configuration.Amount_questions);
            XElement Fail_theory = new XElement("Fail_theory", BE.Configuration.Fail_theory);
            XElement Minimun_lessons = new XElement("Minimun_lessons", BE.Configuration.Minimun_lessons);
            XElement Maximum_tester_age = new XElement("Maximum_tester_age", BE.Configuration.Maximum_tester_age);
            XElement Minimum_tester_age = new XElement("Minimum_tester_age", BE.Configuration.Minimum_tester_age);
            XElement Minimum_Trainee_age = new XElement("Minimum_Trainee_age", BE.Configuration.Minimum_Trainee_age);
            XElement Time_between_tests = new XElement("Time_between_tests", BE.Configuration.Time_between_tests);
            XElement Test_serial_number = new XElement("Test_serial_number", BE.Configuration.Test_serial_number);
            XElement Pass_test = new XElement("Pass_test", BE.Configuration.Pass_test);
            XElement Defualt_distance = new XElement("Defualt_distance", BE.Configuration.Defualt_distance);
            XElement Worker_password = new XElement("Worker_password", BE.Configuration.Worker_password);

            ConfigurationRoot = new XElement("Configuration", Timer_theory, Amount_questions, Fail_theory, Minimun_lessons, Maximum_tester_age, Minimum_tester_age, Minimum_Trainee_age, Time_between_tests, 
                                                              Test_serial_number, Pass_test, Defualt_distance, Worker_password);
            ConfigurationRoot.Save(ConfigurationPath);
        }
        private void LoadConfigurationFile()
        {
            ConfigurationRoot = XElement.Load(ConfigurationPath);

            BE.Configuration.Timer_theory = int.Parse(ConfigurationRoot.Element("Timer_theory").Value);
            BE.Configuration.Amount_questions = int.Parse(ConfigurationRoot.Element("Amount_questions").Value);
            BE.Configuration.Fail_theory = int.Parse(ConfigurationRoot.Element("Fail_theory").Value);
            BE.Configuration.Minimun_lessons = int.Parse(ConfigurationRoot.Element("Minimun_lessons").Value);
            BE.Configuration.Maximum_tester_age = int.Parse(ConfigurationRoot.Element("Maximum_tester_age").Value);
            BE.Configuration.Minimum_tester_age = int.Parse(ConfigurationRoot.Element("Minimum_tester_age").Value);
            BE.Configuration.Minimum_Trainee_age = int.Parse(ConfigurationRoot.Element("Minimum_Trainee_age").Value);
            BE.Configuration.Time_between_tests = int.Parse(ConfigurationRoot.Element("Time_between_tests").Value);
            BE.Configuration.Test_serial_number = int.Parse(ConfigurationRoot.Element("Test_serial_number").Value);
            BE.Configuration.Pass_test = int.Parse(ConfigurationRoot.Element("Pass_test").Value);
            BE.Configuration.Defualt_distance = double.Parse(ConfigurationRoot.Element("Defualt_distance").Value);
            BE.Configuration.Worker_password = ConfigurationRoot.Element("Worker_password").Value;
        }

        public Dal_XML_imp()
        {
            Predicate<string> notExist = path => !File.Exists(path);

            if (notExist(TesterPath))
                CreateTestersFile();

            if (notExist(TraineePath))
                CreateTraineesFile();

            if (notExist(TestPath))
                CreateTestFile();

            if (notExist(TheoryPath))
                CreateTheoryFile();

            if (notExist(ConfigurationPath))
                UpdateConfigurationFile();

            LoadConfigurationFile();
        }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Tester functions
        /// </summary>
        public void Add_tester(BE.Tester tester)
        {
            try
            {
                TesterRoot = XElement.Load(TesterPath);

                if ((from item in TesterRoot.Elements()
                     where item.Element("ID").Value == tester.ID
                     select item).ToList().Count != 0)
                    throw new Exception("שגיאה בהכנסת בוחן: קיים כבר בוחן בעל מספר זהות זהה");

                XElement ID = new XElement("ID", tester.ID);
                XElement Last_name = new XElement("Last_name", tester.Last_name);
                XElement First_name = new XElement("First_name", tester.First_name);

                XElement Day = new XElement("Day", tester.Birth_date.Day);
                XElement Month = new XElement("Month", tester.Birth_date.Month);
                XElement Year = new XElement("Year", tester.Birth_date.Year);
                XElement Birth_date = new XElement("Birth_date", Day, Month, Year);

                XElement Gender = new XElement("Gender", (int)tester.Gender);
                XElement Phone = new XElement("Phone", tester.Phone);

                XElement City = new XElement("City", tester.Address.City);
                XElement Building_number = new XElement("Building_number", tester.Address.Building_number);
                XElement Street = new XElement("Street", tester.Address.Street);
                XElement Address = new XElement("Address", City, Building_number, Street);

                XElement Years_of_experience = new XElement("Years_of_experience", tester.Years_of_experience);
                XElement Specialization = new XElement("Specialization", (int)tester.Specialization);
                XElement Schedule = ScheduleToString(tester.Schedule);
                XElement Maximum_tests = new XElement("Maximum_tests", tester.Maximum_tests);
                XElement Maximum_distance = new XElement("Maximum_distance", tester.Maximum_distance);
                XElement Password = new XElement("Password", tester.Password);

                TesterRoot.Add(new XElement("Tester", ID, Last_name, First_name, Birth_date, Gender, Phone, Address, Years_of_experience, Specialization, Schedule, Maximum_distance, Maximum_tests, Password));
                TesterRoot.Save(TesterPath);
            }
            catch(Exception ex)
            {
                if (ex.Message == "שגיאה בהכנסת בוחן: קיים כבר בוחן בעל מספר זהות זהה")
                    throw new Exception(ex.Message);

                throw new Exception("שגיאה בשמירת בוחן: קרתה תקלה בגישה אל קובץ הנתונים, אנא נסה שוב");
            }
        }
        public void Remove_tester(BE.Tester tester)
        {
            try
            {
                TesterRoot = XElement.Load(TesterPath);
                XElement TesterElement = (from item in TesterRoot.Elements()
                                          where item.Element("ID").Value == tester.ID
                                          select item).FirstOrDefault();
                TesterElement.Remove();
                TesterRoot.Save(TesterPath);
            }
            catch
            {
                throw new Exception("שגיאה במחיקת בוחן: לא ניתן למצוא את הבוחן");
            }
        }
        public void Update_tester(BE.Tester tester)
        {
            try
            {
                Remove_tester(tester);
                Add_tester(tester);
            }
            catch
            {
                throw new Exception("שגיאה בעדכון בוחן: קרתה תקלה בגישה אל קובץ הנתונים, אנא נסה שוב");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Traniee functions
        /// </summary>
        public void Add_traniee(BE.Trainee trainee)
        {
            try
            {
                TraineeRoot = XElement.Load(TraineePath);

                if ((from item in TraineeRoot.Elements()
                     where item.Element("ID").Value == trainee.ID
                     select item).ToList().Count != 0)
                    throw new Exception("שגיאה בהכנסת תלמיד: קיים כבר תלמיד בעל מספר זהות זהה");

                XElement ID = new XElement("ID", trainee.ID);
                XElement Last_name = new XElement("Last_name", trainee.Last_name);
                XElement First_name = new XElement("First_name", trainee.First_name);

                XElement Day = new XElement("Day", trainee.Birth_date.Day);
                XElement Month = new XElement("Month", trainee.Birth_date.Month);
                XElement Year = new XElement("Year", trainee.Birth_date.Year);
                XElement Birth_date = new XElement("Birth_date", Day, Month, Year);

                XElement Gender = new XElement("Gender", (int)trainee.Gender);
                XElement Phone = new XElement("Phone", trainee.Phone);

                XElement City = new XElement("City", trainee.Address.City);
                XElement Building_number = new XElement("Building_number", trainee.Address.Building_number);
                XElement Street = new XElement("Street", trainee.Address.Street);
                XElement Address = new XElement("Address", City, Building_number, Street);

                XElement Pass_theory = new XElement("Pass_theory", trainee.Pass_theory.ToString());
                XElement Last_theory = new XElement("Last_theory", trainee.Last_theory.ToString("s"));

                XElement Gear_used = new XElement("Gear_used", (int)trainee.Gear_used);
                XElement Learned = new XElement("Learned", (int)trainee.Learned);
                XElement Driving_school = new XElement("Driving_school", trainee.Driving_school);
                XElement Teachers_name = new XElement("Teachers_name", trainee.Teachers_name);
                XElement Num_of_classes = new XElement("Num_of_classes", trainee.Num_of_classes);
                XElement Password = new XElement("Password", trainee.Password);
                XElement Email = new XElement("Email", trainee.Email);

                TraineeRoot.Add(new XElement("Traniee", ID, Last_name, First_name, Birth_date, Gender, Phone, Address, Pass_theory, Last_theory, Gear_used, Learned, Driving_school, Teachers_name, Num_of_classes, Password, Email));
                TraineeRoot.Save(TraineePath);
            }
            catch (Exception ex)
            {
                if (ex.Message == "שגיאה בהכנסת תלמיד: קיים כבר תלמיד בעל מספר זהות זהה")
                    throw new Exception(ex.Message);

                throw new Exception("שגיאה בשמירת תלמיד: קרתה תקלה בגישה אל קובץ הנתונים, אנא נסה שוב");
            }
        }
        public void Remove_traniee(BE.Trainee trainee)
        {
            try
            {
                TraineeRoot = XElement.Load(TraineePath);
                XElement TraineeElement = (from item in TraineeRoot.Elements()
                                           where item.Element("ID").Value == trainee.ID
                                           select item).FirstOrDefault();
                TraineeElement.Remove();
                TraineeRoot.Save(TraineePath);
            }
            catch
            {
                throw new Exception("שגיאה במחיקת תלמיד: לא ניתן למצוא את התלמיד");
            }
        }
        public void Update_traniee(BE.Trainee trainee)
        {
            try
            {
                Remove_traniee(trainee);
                Add_traniee(trainee);
            }
            catch
            {
                throw new Exception("שגיאה בעדכון פרטי התלמיד בקובץ הנתונים");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Test functions
        /// </summary>
        public void Add_test(BE.Test test)
        {
            try
            {
                TestRoot = XElement.Load(TestPath);

                //ConfigurationRoot = XElement.Load(ConfigurationPath);
                //BE.Configuration.Test_serial_number = int.Parse(ConfigurationRoot.Element("Test_serial_number").Value) + 1;
                //ConfigurationRoot.Element("Test_serial_number").Value = (int.Parse(ConfigurationRoot.Element("Test_serial_number").Value) + 1).ToString();
                //ConfigurationRoot.Save(ConfigurationPath);
                BE.Configuration.Test_serial_number += 1;
                UpdateConfigurationFile();

                XElement Test_number = new XElement("Test_number", BE.Configuration.Test_serial_number.ToString("X8"));
                XElement Tester_id = new XElement("Tester_id", test.Tester_id);
                XElement Traniee_id = new XElement("Traniee_id", test.Traniee_id);
                XElement Test_time = DateToXElement(test.Test_time);

                XElement City = new XElement("City", test.Address.City);
                XElement Building_number = new XElement("Building_number", test.Address.Building_number);
                XElement Street = new XElement("Street", test.Address.Street);
                XElement Address = new XElement("Address", City, Building_number, Street);
                
                XElement Criteria_list = new XElement("Criteria_list");
                foreach (BE.Criterion item in test.Criteria_list)
                {
                    XElement Score = new XElement("Score", (int)item.Score);
                    XElement Description = new XElement("Description", item.Description);
                    XElement Criterion = new XElement("Criterion", Score, Description);
                    Criteria_list.Add(Criterion);
                }

                XElement Grade = new XElement("Grade", test.Grade);
                XElement Tester_comment = new XElement("Tester_comment", test.Tester_comment);
                XElement Student_car_Type = new XElement("Student_car_Type", (int)test.Student_car_Type);

                TestRoot.Add(new XElement("Test", Test_number, Tester_id, Traniee_id, Test_time, Address, Criteria_list, Grade, Tester_comment, Student_car_Type));
                TestRoot.Save(TestPath);
            }
            catch
            {
                throw new Exception("שגיאה בשמירת מבחן: קרתה תקלה בגישה אל קובץ הנתונים, אנא נסה שוב");
            }
        }
        public void Update_test(BE.Test test)
        {
            try
            {
                //Remove_test(test);
                //Add_test(test);

                TestRoot = XElement.Load(TestPath);
                XElement TestElement = (from item in TestRoot.Elements()
                                          where item.Element("Test_number").Value == test.Test_number
                                          select item).FirstOrDefault();
                TestElement.Element("Grade").Value = test.Grade.ToString();
                TestElement.Element("Tester_comment").Value = test.Tester_comment;
                
                foreach (BE.Criterion item in test.Criteria_list)
                {
                    XElement Score = new XElement("Score", (int)item.Score);
                    XElement Description = new XElement("Description", item.Description);
                    XElement Criterion = new XElement("Criterion", Score, Description);
                    TestElement.Element("Criteria_list").Add(Criterion);
                }
                TestRoot.Save(TestPath);
            }
            catch
            {
                throw new Exception("שגיאה בעדכון מבחן: קרתה תקלה בגישה אל קובץ הנתונים, אנא נסה שוב");
            }
        }
        public void Remove_test(BE.Test test)
        {
            try
            {
                TestRoot = XElement.Load(TestPath);
                XElement TestElement = (from item in TestRoot.Elements()
                                        where item.Element("Test_number").Value == test.Test_number
                                        select item).FirstOrDefault();
                TestElement.Remove();
                TestRoot.Save(TestPath);
            }
            catch
            {
                throw new Exception("שגיאה במחיקת מבחן: לא ניתן למצוא את המבחן");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// theory functions
        /// </summary>
        /// <param name="theoryQuestion"></param>
        public void Add_theory_q(BE.TheoryQuestion theoryQuestion)
        {
            try
            {
                TheoryRoot = XElement.Load(TheoryPath);

                XElement Question = new XElement("Question", theoryQuestion.Question);
                XElement Answer = new XElement("Answer", theoryQuestion.Answer);
                XElement Wrong = WrongToSXElement(theoryQuestion.Wrong);
                XElement Image_code = new XElement("Image_code", theoryQuestion.Image_code);

                TheoryRoot.Add(new XElement("TheoryQuestion", Question, Answer, Wrong, Image_code));
                TheoryRoot.Save(TheoryPath);
            }
            catch
            {
                throw new Exception("שגיאה בשמירת מבחן: קרתה תקלה בגישה אל קובץ הנתונים, אנא נסה שוב");
            }
        }

        public void Remove_theory_q(BE.TheoryQuestion theoryQuestion)
        {
            try
            {
                TheoryRoot = XElement.Load(TheoryPath);
                XElement TheoryElement = (from item in TheoryRoot.Elements()
                                        where item.Element("Question").Value == theoryQuestion.Question
                                        select item).FirstOrDefault();
                TheoryElement.Remove();
                TheoryRoot.Save(TheoryPath);
            }
            catch
            {
                throw new Exception("שגיאה במחיקת השאלה: לא ניתן למצוא את השאלה");
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// List getters
        /// </summary>
        public List<BE.Tester> GetTesters()
        {
            // Load the data from the XML doucment
            TesterRoot = XElement.Load(TesterPath);

            List<BE.Tester> list = (from item in TesterRoot.Elements()
                                    select new BE.Tester(
                                        item.Element("ID").Value,
                                         item.Element("Last_name").Value,
                                         item.Element("First_name").Value,
                                         new DateTime(int.Parse(item.Element("Birth_date").Element("Year").Value),
                                                      int.Parse(item.Element("Birth_date").Element("Month").Value),
                                                      int.Parse(item.Element("Birth_date").Element("Day").Value)),
                                         (BE.Gender)int.Parse(item.Element("Gender").Value),
                                         item.Element("Phone").Value,
                                         new BE.Address(item.Element("Address").Element("Street").Value,
                                                  int.Parse(item.Element("Address").Element("Building_number").Value),
                                                  item.Element("Address").Element("City").Value),
                                         int.Parse(item.Element("Years_of_experience").Value),
                                         int.Parse(item.Element("Maximum_tests").Value),
                                         (BE.Car_type)int.Parse(item.Element("Specialization").Value),
                                         StringToSchedule(item.Element("Schedule").Value),
                                         int.Parse(item.Element("Maximum_distance").Value),
                                         item.Element("Password").Value)
                                    ).ToList();
            return list;
        }

        public List<BE.Trainee> GetTrainees()
        {

            // Load the data from the XML doucment
            TraineeRoot = XElement.Load(TraineePath);

            List<BE.Trainee> list = (from item in TraineeRoot.Elements()
                                     select new BE.Trainee(
                                         item.Element("ID").Value,
                                         item.Element("Last_name").Value,
                                         item.Element("First_name").Value,
                                         new DateTime(int.Parse(item.Element("Birth_date").Element("Year").Value),
                                                      int.Parse(item.Element("Birth_date").Element("Month").Value),
                                                      int.Parse(item.Element("Birth_date").Element("Day").Value)),
                                         (BE.Gender)int.Parse(item.Element("Gender").Value),
                                         item.Element("Phone").Value,
                                         new BE.Address(item.Element("Address").Element("Street").Value,
                                                  int.Parse(item.Element("Address").Element("Building_number").Value),
                                                  item.Element("Address").Element("City").Value),
                                         bool.Parse(item.Element("Pass_theory").Value),
                                         (BE.Car_type)int.Parse(item.Element("Learned").Value),
                                         (BE.Gearbox_type)int.Parse(item.Element("Gear_used").Value),
                                         item.Element("Driving_school").Value,
                                         item.Element("Teachers_name").Value,
                                         int.Parse(item.Element("Num_of_classes").Value),
                                         item.Element("Password").Value,
                                         item.Element("Email").Value
                                         )
                                     { Last_theory = DateTime.Parse(item.Element("Last_theory").Value) }).ToList();
            return list;

        }

        public List<BE.Test> GetTests()
        {
            // Load the data from the XML doucment
            TestRoot = XElement.Load(TestPath);

            List<BE.Test> list = (from item in TestRoot.Elements()
                                  select new BE.Test(
                                      item.Element("Test_number").Value,
                                      item.Element("Tester_id").Value,
                                      item.Element("Traniee_id").Value,
                                      XElementToDate(item.Element("Test_time")),
                                      new BE.Address(item.Element("Address").Element("Street").Value,
                                                     int.Parse(item.Element("Address").Element("Building_number").Value),
                                                     item.Element("Address").Element("City").Value),
                                      item.Element("Tester_comment").Value,
                                      bool.Parse(item.Element("Grade").Value),
                                      (BE.Car_type)int.Parse(item.Element("Student_car_Type").Value))
                                  {
                                      Criteria_list = (from cr in item.Element("Criteria_list").Elements()
                                                       select new BE.Criterion((BE.Score)int.Parse(cr.Element("Score").Value),
                                                                               cr.Element("Description").Value)).ToList()
                                  }
                                      ).ToList();

            return list;
        }

        public List<BE.TheoryQuestion> GetTQ()
        {
            // Load the data from the XML doucment
            TheoryRoot = XElement.Load(TheoryPath);

            List<BE.TheoryQuestion> list = (from item in TheoryRoot.Elements()
                                  select new BE.TheoryQuestion(
                                      item.Element("Question").Value,
                                      item.Element("Answer").Value,
                                      StringToWrong(item.Element("Wrong").Value),
                                      item.Element("Image_code").Value
                                      )).ToList();

            return list;
        }

        private XElement ScheduleToString(bool[,] matrix)
        {
            string str = "";
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 5; j++)
                    switch (matrix[i, j])
                    {
                        case true:
                            str += "1";
                            break;
                        case false:
                            str += "0";
                            break;
                    }

            return new XElement("Schedule", str);
        }

        private bool[,] StringToSchedule(string str)
        {
            bool[,] matrix = new bool[6, 5];
            int index = 0;
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 5; j++)
                    switch (str[index++])
                    {
                        case '1':
                            matrix[i, j] = true;
                            break;
                        case '0':
                            matrix[i, j] = false;
                            break;
                    }

            return matrix;
        }

        private XElement DateToXElement(DateTime date)
        {
            XElement Year = new XElement("Year", date.Year);
            XElement Month = new XElement("Month", date.Month);
            XElement Day = new XElement("Day", date.Day);
            XElement Hour = new XElement("Hour", date.Hour);

            return new XElement("Test_time", Year, Month, Day, Hour);
        }

        private DateTime XElementToDate(XElement element)
        {
            int Year = int.Parse(element.Element("Year").Value);
            int Month = int.Parse(element.Element("Month").Value);
            int Day = int.Parse(element.Element("Day").Value);
            int Hour = int.Parse(element.Element("Hour").Value);

            return new DateTime(Year, Month, Day, Hour, 0, 0);
        }

        private XElement WrongToSXElement(string[] arr)
        {
            string str = arr[0];
            for (int i = 1; i < 3; i++)
                str += "@" + arr[i];

            return new XElement("Wrong", str);
        }

        private string[] StringToWrong(string str)
        {
            return str.Split('@');
        }
    }
}
