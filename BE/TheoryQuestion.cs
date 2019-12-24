using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BE
{
    /// <summary>
    /// class that represent a single Theory Question
    /// </summary>
    public class TheoryQuestion : DependencyObject
    {
        private string _question;
        /// <summary>
        /// question Property
        /// </summary>
        public string Question
        {
            get { return _question; }
            private set { _question = value; }
        }

        private string _answer;
        /// <summary>
        /// right answer Property
        /// </summary>
        public string Answer
        {
            get { return _answer; }
            private set { _answer = value; }
        }

        private string[] _wrong;
        /// <summary>
        /// array of the wrong answers Property
        /// </summary>
        public string[] Wrong
        {
            get { return _wrong; }
            private set { _wrong = value; }
        }


        /// <summary>
        /// right answer DependencyProperty
        /// </summary>
        public string Student_answer
        {
            get { return (string)GetValue(Student_answerProperty); }
            set { SetValue(Student_answerProperty, value); }
        }

        public static readonly DependencyProperty Student_answerProperty =
            DependencyProperty.Register("Student_answer", typeof(string), typeof(TheoryQuestion), new UIPropertyMetadata(""));

        string _image;
        /// <summary>
        /// right answer Property
        /// </summary>
        public string Image_code
        {
            get { return _image; }
            private set { _image = value; }
        }

        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="question">the question</param>
        /// <param name="answer">right answer</param>
        /// <param name="wrong">array of the wrong answers</param>
        /// <param name="code">string code of an image</param>
        public TheoryQuestion(string question, string answer, string[] wrong, string code = null)
        {
            Question = question;
            Answer = answer;
            Wrong = wrong;
            Image_code = code;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        public TheoryQuestion(TheoryQuestion other)
        {
            Question = other.Question;
            Answer = other.Answer;
            Wrong = other.Wrong;
            Student_answer = other.Student_answer;
            Image_code = other.Image_code;
        }
    }
}
