using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BL
{
    /// <summary>
    /// delegate for test terms
    /// </summary>
    /// <param name="test"></param>
    /// <returns></returns>
    public delegate bool conditionDelegate(BE.Test test);
    public delegate bool conditionDelegateTester(List<BE.Tester> list);

    public interface IBL
    {

        /// <summary>
        /// Tester functions
        /// </summary>
        void Add_tester(BE.Tester tester);
        void Remove_tester(BE.Tester tester);
        void Update_tester(BE.Tester tester);

        /// <summary>
        /// Traniee functions
        /// </summary>
        void Add_traniee(BE.Trainee trainee);
        void Remove_traniee(BE.Trainee trainee);
        void Update_traniee(BE.Trainee trainee);

        /// <summary>
        /// Test functions
        /// </summary>
        void Add_test(BE.Test test);
        void Update_test(BE.Test test);
        void Remove_test(BE.Test test);

        /// <summary>
        /// Configuration update
        /// </summary>
        void UpdateConfigurationFile();

        /// <summary>
        /// theory functions
        /// </summary>
        /// <param name="theoryQuestion"></param>
        void Add_theory_q(BE.TheoryQuestion theoryQuestion);
        void Remove_theory_q(BE.TheoryQuestion theoryQuestion);
        BE.TheoryQuestion[] GetTheoryTest();
        string[] GetOptions(BE.TheoryQuestion question, Random rand);
        BE.Thery_status GetTheoryStatus(BE.Trainee trainee);
        bool TestResult(BE.TheoryQuestion[] test, List<BE.TheoryQuestion> wrong_answers, out int amount_wrong);

        /// <summary>
        /// List getters
        /// </summary>
        List<BE.Tester> GetTesters();
        List<BE.Trainee> GetTrainees();
        List<BE.Test> GetTests();
        List<BE.TheoryQuestion> GetTQ();

        /// <summary>
        /// search lists
        /// </summary>
        List<BE.Test> SearchTest(string value, bool By_tester_id = true, bool By_trainee_id = true, bool By_test_number = true);
        List<BE.Trainee> SearchTrainee(string value, bool By_id = true, bool By_first_name = true, bool By_last_name = true);
        List<BE.Tester> SearchTester(string value, bool By_id = true, bool By_first_name = true, bool By_last_name = true);
        

        /// <summary>
        /// groupings
        /// </summary>
        /// <param name="oredered"></param>
        /// <returns></returns>
        List<List<BE.Tester>> GroupBySpecialization(bool oredered = false);
        List<List<BE.Trainee>> GroupBySchool(bool oredered = false);
        List<List<BE.Trainee>> GroupByTeacher(bool oredered = false);
        List<List<BE.Trainee>> GroupByNumberOfTests(bool oredered = false);

        /// <summary>
        /// funcs
        /// </summary>
        /// <returns></returns>
        bool Pass_Test(List<BE.Criterion> criteria);
        List<BE.Tester> Distance(BE.Address address);
        void Send_Email(BE.Test test);
        void Add_To_Calender(BE.Test test);
        int Count_tests(BE.Trainee trainee);
        List<BE.Tester> Testers_Free_at_Time(DateTime dateTime, List<BE.Tester> testers = null);
        List<BE.Test> Tests_by_terms(conditionDelegate condition);
        List<BE.Test> Tests_by_Date(DateTime dateTime);
        BE.Test[,] Tester_sceduale(BE.Tester tester, DateTime dateTime);
        List<BE.Test>[] groupByTraineePass(BE.Trainee trainee);
        List<BE.Test>[] groupByTesterFilled(BE.Tester tester);
        BE.Tester OldestTester();
        void Email_Registration(BE.Trainee trainee, string decrypted_password);
        string Encrypte(string password);

        string SourceToString(string source);
        BitmapImage StringToBitmap(string str);
    }
}
