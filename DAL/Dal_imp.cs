using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Dal_imp: IDAL
    {
        // Tester functions:

        /// <summary>
        /// Add tester to the DS. If the tester alredy exist throw exeption
        /// </summary>
        /// <param name="tester">The tester to add</param>
        public void Add_tester(BE.Tester tester)
        {
            foreach (BE.Tester item in DS.DataSource.Testers_list)
                if (item.ID == tester.ID)
                    throw new Exception("שגיאה בהכנסת בוחן: קיים כבר בוחן בעל מספר זהות זהה");

            // If  the ID dosent exist, add the tester
            DS.DataSource.Testers_list.Add(tester);
        }

        /// <summary>
        /// Remove tester from the DS. If the tester not exist throw exeption
        /// </summary>
        /// <param name="tester">The tester to remove</param>
        public void Remove_tester(BE.Tester tester)
        {
            // try to delete the tester
            bool deleted = false;
            for (int i = 0; i < DS.DataSource.Testers_list.Count; i++)
                if (DS.DataSource.Testers_list[i].ID == tester.ID)
                {
                    DS.DataSource.Testers_list.RemoveAt(i);
                    deleted = true;
                    break;
                }

            // if the tester not found, throw exeption
            if (!deleted)
                throw new Exception("שגיאה במחיקת בוחן: הבוחן לא קיים במערכת");
        }

        /// <summary>
        /// Update tester in the DS. If the tester not exist throw exeption
        /// </summary>
        /// <param name="tester">The tester to update</param>
        public void Update_tester(BE.Tester tester)
        {
            // try to update the tester
            bool update = false;
            foreach (BE.Tester item in DS.DataSource.Testers_list)
                if (item.ID == tester.ID)
                {
                    int i = DS.DataSource.Testers_list.IndexOf(item);
                    DS.DataSource.Testers_list[i] = tester;
                    update = true;
                    break;
                }

            // if the tester not found, throw exeption
            if (!update)
                throw new Exception("שגיאה בעדכון בוחן: הבוחן לא קיים במערכת");
        }

        // ------------------------------------------------------------------------------------------
        // Traniee functions:

        /// <summary>
        /// Add trainee to the DS. If the trinee alredy exist throw exeption
        /// </summary>
        /// <param name="trainee">The trianee to add</param>
        public void Add_traniee(BE.Trainee trainee)
        {
            foreach (BE.Trainee item in DS.DataSource.Trainees_list)
                if (item.ID == trainee.ID)
                    throw new Exception("שגיאה בהכנסת תלמיד: קיים כבר תלמיד בעל מספר זהות זהה");

            // If  the ID dosent exist, add the tester
            DS.DataSource.Trainees_list.Add(trainee);
        }

        /// <summary>
        /// Remove trainee from the DS. If the trainee not exist throw exeption
        /// </summary>
        /// <param name="trainee">The trainee to remove</param>
        public void Remove_traniee(BE.Trainee trainee)
        {
            // try to delete the trainee
            bool deleted = false;
            for (int i = 0; i < DS.DataSource.Trainees_list.Count; i++)
                if (DS.DataSource.Trainees_list[i].ID == trainee.ID)
                {
                    DS.DataSource.Trainees_list.RemoveAt(i);
                    deleted = true;
                    break;
                }

            // if the trainee not found, throw exeption
            if (!deleted)
                throw new Exception("שגיאה במחיקת תלמיד: התלמיד לא קיים במערכת");
        }

        /// <summary>
        /// Update trainee in the DS. If the trainee not exist throw exeption
        /// </summary>
        /// <param name="trainee">The trainee to update</param>
        public void Update_traniee(BE.Trainee trainee)
        {
            // try to update the trainee
            bool update = false;
            foreach (BE.Trainee item in DS.DataSource.Trainees_list)
                if (item.ID == trainee.ID)
                {
                    int i = DS.DataSource.Trainees_list.IndexOf(item);
                    DS.DataSource.Trainees_list[i] = trainee;
                    update = true;
                    break;
                }

            // if the trainee not found, throw exeption
            if (!update)
                throw new Exception("שגיאה בעדכון תלמיד: התלמיד לא קיים במערכת");
        }

        // ------------------------------------------------------------------------------------------
        // Test functions:

        /// <summary>
        /// Add test to the DS
        /// </summary>
        /// <param name="test">Test show WITHOUT TEST NUMBER!</param>
        public void Add_test(BE.Test test)
        {
            BE.Configuration.Test_serial_number++;
            test.Test_number = BE.Configuration.Test_serial_number.ToString("X8");

            DS.DataSource.Tests_list.Add(test);
        }

        /// <summary>
        /// Update trainee in the DS. If the trainee not exist throw exeption
        /// </summary>
        /// <param name="test">The test to update</param>
        public void Update_test(BE.Test test)
        {
            // try to update the test
            bool update = false;
            foreach (BE.Test item in DS.DataSource.Tests_list)
                if (item.Test_number == test.Test_number)
                {
                    int i = DS.DataSource.Tests_list.IndexOf(item);
                    DS.DataSource.Tests_list[i] = test;
                    update = true;
                    break;
                }

            // if the tester not found, throw exeption
            if (!update)
                throw new Exception("שגיאה בעדכון מבחן: המבחן לא קיים במערכת");
        }

        /// <summary>
        /// Remove test from the DS. If the test not exist throw exeption
        /// </summary>
        /// <param name="test">The test to remove</param>
        public void Remove_test(BE.Test test)
        {
            // try to delete the test
            bool deleted = false;
            for (int i = 0; i < DS.DataSource.Tests_list.Count; i++)
                if (DS.DataSource.Tests_list[i].Test_number == test.Test_number)
                {
                    DS.DataSource.Tests_list.RemoveAt(i);
                    deleted = true;
                    break;
                }

            // if the trainee not found, throw exeption
            if (!deleted)
                throw new Exception("שגיאה במחיקת מבחן: המבחן לא קיים במערכת");
        }

        // ------------------------------------------------------------------------------------------
        // Get lists functions:

        /// <summary>
        /// A function that returns a copy of all testers registered with the DS
        /// </summary>
        public List<BE.Tester> GetTesters()
        {
            List<BE.Tester> Testers = new List<BE.Tester>();

            // copy the list
            foreach (BE.Tester item in DS.DataSource.Testers_list)
                Testers.Add(new BE.Tester(item));

            return Testers;
        }

        /// <summary>
        /// A function that returns a copy of all trainees registered with the DS
        /// </summary>
        public List<BE.Trainee> GetTrainees()
        {
            List<BE.Trainee> Trainees = new List<BE.Trainee>();

            // copy the list
            foreach (BE.Trainee item in DS.DataSource.Trainees_list)
                Trainees.Add(new BE.Trainee(item));

            return Trainees;
        }

        /// <summary>
        /// A function that returns a copy of all tests registered with the DS
        /// </summary>
        public List<BE.Test> GetTests()
        {
            List<BE.Test> Tests = new List<BE.Test>();

            // copy the list
            foreach (BE.Test item in DS.DataSource.Tests_list)
                Tests.Add(new BE.Test(item));

            return Tests;
        }

        /// <summary>
        /// A function that returns a copy of all the Theory Questions within the DS
        /// </summary>
        public List<BE.TheoryQuestion> GetTQ()
        {
            List<BE.TheoryQuestion> Questions = new List<BE.TheoryQuestion>();

            // copy the list
            foreach (BE.TheoryQuestion item in DS.DataSource.TQ)
                Questions.Add(new BE.TheoryQuestion(item));

            return Questions;
        }

        //--------------------------------------------------------------

        public void Add_theory_q(BE.TheoryQuestion theoryQuestion) { }
        public void Remove_theory_q(BE.TheoryQuestion theoryQuestion) { }
        public void UpdateConfigurationFile() { }
    }
}
