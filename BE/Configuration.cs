using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        /// <summary>
        /// Time in minutes for the theory test
        /// </summary>
        public static int Timer_theory = 30;

        /// <summary>
        /// Amount of questions in the theory test
        /// </summary>
        public static int Amount_questions = 4;

        /// <summary>
        /// Amount of questions to fail in theory test
        /// </summary>
        public static int Fail_theory = 1;

        /// <summary>
        /// Minimum number of lessons to access the test
        /// </summary>
        public static int Minimun_lessons = 20;

        /// <summary>
        /// The maximum age at which an tester can serve as an tester
        /// </summary>
        public static int Maximum_tester_age = 75;

        /// <summary>
        /// The minimum age at which an tester can serve as an tester
        /// </summary>
        public static int Minimum_tester_age = 40;

        /// <summary>
        /// The minimum age at which a student can access the test
        /// </summary>
        public static int Minimum_Trainee_age = 18;

        /// <summary>
        /// The minimum time range between the two tests of that student (days)
        /// </summary>
        public static int Time_between_tests = 7;

        /// <summary>
        /// The Test number
        /// </summary>
        public static int Test_serial_number = 0;

        /// <summary>
        /// 
        /// </summary>
        public static int Pass_test = 1;

        /// <summary>
        /// The defualt distance to return if the network not working (for the MapQuest)
        /// </summary>
        public static double Defualt_distance = 5;

        /// <summary>
        /// The defualt password for all the workers
        /// </summary>
        public static string Worker_password = "worker";
    }
}
