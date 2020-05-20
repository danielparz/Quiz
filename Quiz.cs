using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    class Quiz
    {
        public List<Question> questions = new List<Question>();
        public int points = 0;

        public Quiz (List<Question> questions)
        {
            this.questions = questions;
        }

        public List<Question> ShuffleVariants(int counter)
        {
            List<Question> newQuestionsList = new List<Question>();
            int[] indexTab = RandomizeWithoutRepetition(counter);

            for(int i = 0; i < counter; i++)            
                newQuestionsList.Add(questions[indexTab[i]]);

            return newQuestionsList;
        }

        private int[] RandomizeWithoutRepetition(int counter)
        {
            Random random = new Random();
            int[] tab = new int[counter];
            int[] finalTab = new int[counter];

            for (int i = 0; i < counter; i++)
                tab[i] = i;

            for(int i = counter - 1; i >= 0; i--)
            {
                int index = random.Next(0, i);
                finalTab[counter - i] = tab[index];
                tab[index] = tab[i];
            }

            return finalTab;
        }

        public bool CheckAnswer(Question question, string answer)
        {
            if (question.CorrectAnswer == answer)
            {
                points++;
                return true;
            }
            else return false;
        }
    }
}
