using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
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
        /// theory functions
        /// </summary>
        /// <param name="theoryQuestion"></param>
        void Add_theory_q(BE.TheoryQuestion theoryQuestion);
        void Remove_theory_q(BE.TheoryQuestion theoryQuestion);

        /// <summary>
        /// List getters
        /// </summary>
        List<BE.Tester> GetTesters();
        List<BE.Trainee> GetTrainees();
        List<BE.Test> GetTests();
        List<BE.TheoryQuestion> GetTQ();

        /// <summary>
        /// Configuration update
        /// </summary>
        void UpdateConfigurationFile();
    }
}
