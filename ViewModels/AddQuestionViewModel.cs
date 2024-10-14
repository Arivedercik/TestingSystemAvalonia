using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.ViewModels
{
    public class AddQuestionViewModel : ReactiveObject
    {
        public AddQuestionViewModel()
        {
            InitQuestionCollection();
        }

        public void InitQuestionCollection()
        {
            QuestionCollection.Clear();           

            WorkWithFileViewModel wwf = new WorkWithFileViewModel();
            using (var file = File.OpenText(wwf.pathCollection[1]))
            {

                string sJson;
                while ((sJson = file.ReadLine()) != null)
                {
                    QuestionCollection.Add(JsonSerializer.Deserialize<Questions>(sJson));
                }
            }

            /*
            using (var file = File.OpenText(wwf.pathCollection[2]))
            {

                string sJson;
                while ((sJson = file.ReadLine()) != null)
                {
                    AnswerCollection.Add(JsonSerializer.Deserialize<Answer>(sJson));
                }
            }
            */
        }

        private string _nameQuestion = "";
        private string _nameAnswer = "";
        private ObservableCollection<Questions> _questionCollection = new ObservableCollection<Questions>();
        private ObservableCollection<Answer> _answerCollection = new ObservableCollection<Answer>();
        

        public string NameQuestion
        {
            get => _nameQuestion;
            set => this.RaiseAndSetIfChanged(ref _nameQuestion, value);
        }

        public string NameAnswer
        {
            get => _nameAnswer;
            set => this.RaiseAndSetIfChanged(ref _nameAnswer, value);
        }

        public ObservableCollection<Questions> QuestionCollection
        {
            get => _questionCollection;
            set => this.RaiseAndSetIfChanged(ref _questionCollection, value);
        }

        public ObservableCollection<Answer> AnswerCollection
        {
            get => _answerCollection;
            set => this.RaiseAndSetIfChanged(ref _answerCollection, value);
        }
        
    }
}