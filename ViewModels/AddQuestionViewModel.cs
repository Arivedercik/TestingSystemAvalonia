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
    /// <summary>
    /// Добавление вопросов
    /// </summary>
    public class AddQuestionViewModel : ReactiveObject
    {
        /// <summary>
        /// Конструктор для инициализации списков
        /// </summary>
        public AddQuestionViewModel()
        {
            InitQuestionCollection();
        }

        /// <summary>
        /// Инициализация списка вопросов
        /// </summary>
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
        }

        #region Поле наименования вопроса
        private string _nameQuestion = "";

        public string NameQuestion
        {
            get => _nameQuestion;
            set => this.RaiseAndSetIfChanged(ref _nameQuestion, value);
        }
        #endregion

        #region Поле наименование ответа
        private string _nameAnswer = "";

        public string NameAnswer
        {
            get => _nameAnswer;
            set => this.RaiseAndSetIfChanged(ref _nameAnswer, value);
        }
        #endregion

        #region Поле списка вопросов
        private ObservableCollection<Questions> _questionCollection = new ObservableCollection<Questions>();

        public ObservableCollection<Questions> QuestionCollection
        {
            get => _questionCollection;
            set => this.RaiseAndSetIfChanged(ref _questionCollection, value);
        }
        #endregion

        #region Поле списка ответов
        private ObservableCollection<Answers> _answerCollection = new ObservableCollection<Answers>();


        public ObservableCollection<Answers> AnswerCollection
        {
            get => _answerCollection;
            set => this.RaiseAndSetIfChanged(ref _answerCollection, value);
        }        
        #endregion
    }
}