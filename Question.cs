using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    class Question
    {
        public string Subject = "";
        public List<string> options = new List<string>();
        public string CorrectAnswer = "";

        public bool CheckAnswer(int i)
        {
            if (i >= options.Count) return false;
            if (CorrectAnswer.Equals(options[i]))
                return true;            
            else return false;
        }

        public int GetAnswerIndex(ConsoleKeyInfo cki)
        {
            switch (cki.Key)
            {
                case ConsoleKey.A:
                    return 0;
                case ConsoleKey.B:
                    return 1;
                case ConsoleKey.C:
                    return 2;
                case ConsoleKey.D:
                    return 3;
                default:
                    return 4;
            }
        }
    }
}