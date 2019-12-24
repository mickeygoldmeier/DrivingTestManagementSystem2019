using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// A structure to describe the result of a criterion in the driving test
    /// </summary>
    public struct Criterion
    {
        public Score Score;
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_score"></param>
        /// <param name="_description"></param>
        public Criterion(Score _score, string _description)
        {
            Score = _score;
            Description = _description;
        }

        /// <summary>
        /// To String function 
        /// </summary>
        /// <returns>Return criterion and the score in this criterion</returns>
        public override string ToString() { return "בקריטריון " + Description + " קיבל התלמיד ציון של " + ((int)Score + 1) + "/3"; }
    }
}
