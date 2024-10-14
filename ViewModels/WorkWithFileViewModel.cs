using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using Avalonia;
using ReactiveUI;
using TestingSystemAvalonia.Models;
using Tmds.DBus.Protocol;

namespace TestingSystemAvalonia.ViewModels
{
    /// <summary>
    /// Работа с фалами Json
    /// </summary>
	public class WorkWithFileViewModel : ReactiveObject
    {
        const string PathJsonFileTest = "Tests.json";
        const string PathJsonFileQuestions = "Questions.json";
        const string PathJsonFileAnswers = "Answers.json";

        /// <summary>
        /// Список всех файлов
        /// </summary>
        public ObservableCollection<string> pathCollection = new ObservableCollection<string>()
        {
            PathJsonFileTest,
            PathJsonFileQuestions,
            PathJsonFileAnswers
        };

        /// <summary>
        /// Конструктор для создания файлов
        /// </summary>
        public WorkWithFileViewModel()
        {
            try
            {
                if (!File.Exists(PathJsonFileTest))
                {
                    File.Create(pathCollection[0]).Close();
                    File.Create(pathCollection[1]).Close();
                    File.Create(pathCollection[2]).Close();
                    File.Create(pathCollection[3]).Close();
                }
            }
            catch
            {
                //
            }
        }

        /// <summary>
        /// Добавление теста в файл
        /// </summary>
        /// <param name="Name">Наименование теста</param>
        /// <param name="Description">Описание теста</param>
        /// <param name="QuestionCollection">Список вопросов теста</param>
        public void AddTest(string Name, string Description, ObservableCollection<Questions> QuestionCollection)
        {
            int count = File.ReadAllLines(pathCollection[0]).Length == 0 ? 1 : File.ReadAllLines(pathCollection[0]).Length + 1;

            using (var file = File.AppendText(pathCollection[0]))
            {
                Tests newTest = new Tests()
                {
                    Id = count,
                    Name = Name,
                    Description = Description,
                    QuestionCollection = QuestionCollection
                };

                file.WriteLine(JsonSerializer.Serialize(newTest));
            }
        }

        /// <summary>
        /// Добавление вопроса в файл
        /// </summary>
        /// <param name="Name">Тело вопроса</param>
        public void AddQuestion(string Name)
        {
            int count = File.ReadAllLines(pathCollection[1]).Length == 0 ? 1 : File.ReadAllLines(pathCollection[1]).Length + 1;

            using (var file = File.AppendText(pathCollection[1]))
            {
                Questions newQ = new Questions()
                {
                    Id = count,
                    Name = Name
                };

                file.WriteLine(JsonSerializer.Serialize(newQ));
            }
        }

        /// <summary>
        /// Добавление ответов в файл
        /// </summary>
        /// <param name="AnswerCollection">Список ответов</param>
        public void AddAnswer(ObservableCollection<Answers> AnswerCollection)
        {
            int countAnswer = File.ReadAllLines(pathCollection[2]).Length == 0 ? 1 : File.ReadAllLines(pathCollection[2]).Length + 1;

            int countQuestionn = File.ReadAllLines(pathCollection[1]).Length == 0 ? 1 : File.ReadAllLines(pathCollection[1]).Length + 1;

            using (var file = File.AppendText(pathCollection[2]))
            {
                for (int i = 0; i < AnswerCollection.Count; i++)
                {
                    Answers newA = new Answers()
                    {
                        Id = countAnswer + i,
                        Name = AnswerCollection[i].Name,
                        IdQuestion = countQuestionn,
                        IsTrue = AnswerCollection[i].IsTrue
                    };

                    file.WriteLine(JsonSerializer.Serialize(newA));
                }
            }
        }

        /// <summary>
        /// Изъятие тестов из файла
        /// </summary>
        /// <returns>Список тестов</returns>
        public ObservableCollection<Tests> SearchTest()
        {
            ObservableCollection<Tests> testCollection = new ObservableCollection<Tests>();

            using (var file = File.OpenText(pathCollection[0]))
            {
                string sJson;

                while ((sJson = file.ReadLine()) != null)
                {
                    testCollection.Add(JsonSerializer.Deserialize<Tests>(sJson));
                }
            }

            return testCollection;
        }

        /// <summary>
        /// Изъятие ответов из файла
        /// </summary>
        /// <returns>Список ответов</returns>
        public ObservableCollection<Answers> SearchAnswer()
        {
            ObservableCollection<Answers> answerCollection = new ObservableCollection<Answers>();

            using (var file = File.OpenText(pathCollection[2]))
            {
                string sJson;

                while ((sJson = file.ReadLine()) != null)
                {
                    answerCollection.Add(JsonSerializer.Deserialize<Answers>(sJson));
                }
            }

            return answerCollection;
        }
    }
}