using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz
{
    class Quiz
    {
        public List<Question> questions = new List<Question>();
        public int points = 0;

        public Quiz (List<Question> questions, int questionsNumber)
        {
            this.questions = questions;
            this.questions = ChooseQuestions(questionsNumber);
        }

        public void Play()
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            
            Console.Clear();
            Console.WriteLine("Witaj w quizie!");
            Console.WriteLine("Zostanie zaraz wylosowanych specjalnie dla Ciebie 5 pytań z dziedziny chemii.");
            Console.WriteLine("Pod każdym pytaniem będą 4 warianty odpowiedzi, z czego tylko jedna jest prawidłowa.");
            Console.WriteLine("W celu udzielenia odpowiedzi, naciśnij klawisz na klawiaturze, odpowiadający literze przy wybranej odpowiedzi.");
            Console.WriteLine("Aby zacząć, naciśnij klawisz ENTER. Powodzenia!");

            bool isKeyCorrect = false;
            while (!isKeyCorrect)
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Enter) isKeyCorrect = true;
            }

            Console.Clear();

            foreach (Question q in questions)
            {
                Console.WriteLine(q.Subject);
                Console.WriteLine();
                Console.WriteLine($"A. {q.options[0]}");
                Console.WriteLine($"B. {q.options[1]}");
                Console.WriteLine($"C. {q.options[2]}");
                Console.WriteLine($"D. {q.options[3]}");

                isKeyCorrect = false;

                while (!isKeyCorrect)
                {
                    cki = Console.ReadKey();
                    if (CheckAnswerKey(cki)) isKeyCorrect = true;
                    else Console.WriteLine("Podaj prawidłową odpowiedź!");
                }

                if (q.CheckAnswer(q.GetAnswerIndex(cki)))
                {
                    Console.WriteLine("\nPoprawna odpowiedź!");
                    points++;
                }
                else Console.WriteLine("\nNiestety, błędna odpowiedź.");

                Console.WriteLine();
                Console.WriteLine();
            }
            double percent = CalculateResult();
            Console.Clear();
            Console.WriteLine("Zakończyłeś quiz.");
            Console.WriteLine($"Twój wynik wynosi {percent}%");                         
        }

        public double CalculateResult()
        {
            return ((double)points / questions.Count) * 100;
        }

        private bool CheckAnswerKey(ConsoleKeyInfo cki)
        {
            if (cki.Key == ConsoleKey.A
                || cki.Key == ConsoleKey.B
                || cki.Key == ConsoleKey.C
                || cki.Key == ConsoleKey.D)
                return true;
            else return false;
        }

        public Question ShuffleVariants(Question question)
        {
            Question newQuestion = new Question();
            int[] indexTab = RandomizeWithoutRepetition(question.options.Count);

            for (int i = 0; i < indexTab.Length; i++)
                newQuestion.options.Add(question.options[indexTab[i]]);
            newQuestion.Subject = question.Subject;
            newQuestion.CorrectAnswer = question.CorrectAnswer;

            return newQuestion;
        }

        private int[] RandomizeWithoutRepetition(int number)
        {
            Random random = new Random();
            int[] tab = new int[number];
            int[] finalTab = new int[number];

            for (int i = 0; i < number; i++)
                tab[i] = i;

            for(int i = number - 1; i >= 0; i--)
            {
                int index = random.Next(0, i);
                finalTab[number - i - 1] = tab[index];
                tab[index] = tab[i];
            }

            return finalTab;
        }

        private List<Question> ChooseQuestions(int number)
        {
            List<Question> quiz = new List<Question>();
            int[] indexTab = RandomizeWithoutRepetition(this.questions.Count);

            for (int i = 0; i < number; i++)
            {
                quiz.Add(this.questions[indexTab[i]]);
                quiz[i] = ShuffleVariants(quiz[i]);
            }

            return quiz;            
        }
    }
}
