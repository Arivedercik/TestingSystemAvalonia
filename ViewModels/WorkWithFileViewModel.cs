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
	public class WorkWithFileViewModel : ReactiveObject
	{
        const string PathJsonFileTest = "Tests.json";
        const string PathJsonFileQuestions = "Questions.json";
        const string PathJsonFileAnswers = "Answers.json";
        const string PathJsonFileTypeQuestion = "TypeQuestion.json";

        public List<string> pathCollection = new List<string>() 
        { 
            PathJsonFileTest, 
            PathJsonFileQuestions, 
            PathJsonFileAnswers, 
            PathJsonFileTypeQuestion 
        };
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
               
            }
        }

        public void AddTest(string Name, string Description, ObservableCollection<Questions> QuestionCollection)
        {
            int count = File.ReadAllLines(pathCollection[0]).Length == 0 ? 1 : File.ReadAllLines(pathCollection[0]).Length;
           
            using (var file = File.AppendText(pathCollection[0]))
            {
                Tests newTest = new Tests()
                {
                    Id = count,
                    Name = Name,
                    Description = Description,
                    questionCollection = QuestionCollection
                };

                file.WriteLine(JsonSerializer.Serialize(newTest));
            }
        }

        public void AddQuestion(string Name)
        {
            int count = File.ReadAllLines(pathCollection[1]).Length == 0 ? 1 : File.ReadAllLines(pathCollection[1]).Length;

            using (var file = File.AppendText(pathCollection[1]))
            {

                Questions newQ = new Questions()
                {
                    ID = count,
                    Name = Name
                };
                file.WriteLine(JsonSerializer.Serialize(newQ));
            }
        }

        public void AddAnswer(ObservableCollection<Answer> answerColllection)
        {
            int countAnswer = File.ReadAllLines(pathCollection[2]).Length == 0 ? 1 : File.ReadAllLines(pathCollection[2]).Length;

            int countQuestionn = File.ReadAllLines(pathCollection[1]).Length == 0 ? 1 : File.ReadAllLines(pathCollection[1]).Length;

            using (var file = File.AppendText(pathCollection[2]))
            {
                for (int i = 0; i < answerColllection.Count; i++)
                {
                    Answer newA = new Answer()
                    {
                        Id = countAnswer+i,
                        Name = answerColllection[i].Name,
                        IdQuestion = countQuestionn,
                        IsTrue = answerColllection[i].IsTrue
                    };
                    file.WriteLine(JsonSerializer.Serialize(newA));
                }
            }
        }

        public void EditAnswer(string Name, bool IsTrue)
        {
            ObservableCollection<Answer> answerCollection = new ObservableCollection<Answer>();

            using (var file = File.OpenText(pathCollection[2]))
            {
                string sJson;
                while ((sJson = file.ReadLine()) != null)
                {
                    answerCollection.Add(JsonSerializer.Deserialize<Answer>(sJson));

                    if(Name == answerCollection[answerCollection.Count-1].Name)
                    {
                        answerCollection[answerCollection.Count - 1].IsTrue = IsTrue;
                    }
                }
            }

            File.WriteAllText(pathCollection[2], "");

            using (var file = File.AppendText(pathCollection[2]))
            {
                foreach (var item in answerCollection)
                {
                    file.WriteLine(JsonSerializer.Serialize(item));
                }
            }
        }

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
    }
}