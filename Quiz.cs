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
            bool wannaPlay = true;

            while (wannaPlay)
            {
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
                    Console.WriteLine(q.Subject.ToString());
                    Console.WriteLine();
                    Console.WriteLine($"a. {q.options[0]}");
                    Console.WriteLine($"b. {q.options[1]}");
                    Console.WriteLine($"c. {q.options[2]}");
                    Console.WriteLine($"d. {q.options[3]}");

                    isKeyCorrect = false;

                    while (!isKeyCorrect)
                    {
                        cki = Console.ReadKey();
                        if (CheckAnswerKey(cki)) isKeyCorrect = true;
                    }

                    if (q.CheckAnswer(q.GetAnswerIndex(cki)))
                    {
                        Console.WriteLine("Poprawna odpowiedź!");
                        points++;
                    }
                    else Console.WriteLine("Niestety, błędna odpowiedź.");

                    Console.WriteLine();
                    Console.WriteLine();
                }

                Console.Clear();
                Console.WriteLine("Zakończyłeś quiz.");
                Console.WriteLine($"Twój wynik wynosi {CalculateResult()}%");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Chcesz zagrać ponownie? t/n");

                while(cki.Key != ConsoleKey.T || cki.Key != ConsoleKey.N)
                {
                    Console.ReadKey();
                    if (cki.Key == ConsoleKey.N) wannaPlay = false;
                }
                points = 0;
            }
        }

        public double CalculateResult()
        {
            return (points / questions.Count) * 100;
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
                newQuestion.options.Add(question.options[i]);

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
                quiz.Add(this.questions[i]);
                quiz[i] = ShuffleVariants(quiz[i]);
            }

            return quiz;            
        }
    }
}
