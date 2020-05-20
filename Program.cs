using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quiz
{
    class Program
    {
        static void Main(string[] args)
        {
            string FilePath = @"..\..\..\questions.csv";
            FileInfo fi = new FileInfo(FilePath);
            List<Question> questions = new List<Question>();

            try
            {
                if (fi.Extension != ".csv") throw new Exception("Nieprawidłowy format pliku!");

                using(StreamReader sr = new StreamReader(FilePath))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        List<string> fields = line.Split(';').ToList<string>();
                        List<string> questionsList = new List<string>
                            {
                            fields[1],
                            fields[2],
                            fields[3],
                            fields[4]
                            };
                        
                        questions.Add(new Question() { Subject = fields[0].ToString(), options = questionsList, CorrectAnswer = fields[5].ToString() });
                    }
                }

                Quiz quiz = new Quiz(questions, 5);
                quiz.Play();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
