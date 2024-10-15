using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using TestingSystemAvalonia.Models;
using TestingSystemAvalonia.Services;

namespace TestingSystemAvalonia.ViewModels
{
    /// <summary>
    /// Добавление вопросов
    /// </summary>
    public class AddQuestionViewModel : ReactiveObject
    {
        private string _nameQuestion = "";
        private string _nameAnswer = "";

        private ObservableCollection<Question> _questionCollection = new ObservableCollection<Question>();

        private ObservableCollection<Answer> _answerCollection = new ObservableCollection<Answer>();


        /// <summary>
        /// Конструктор для инициализации списков
        /// </summary>
        public AddQuestionViewModel()
        {
            InitQuestionCollection();
        }

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

        public ObservableCollection<Question> QuestionCollection
        {
            get => _questionCollection;
            set => this.RaiseAndSetIfChanged(ref _questionCollection, value);
        }

        public ObservableCollection<Answer> AnswerCollection
        {
            get => _answerCollection;
            set => this.RaiseAndSetIfChanged(ref _answerCollection, value);
        }

        /// <summary>
        /// Инициализация списка вопросов
        /// </summary>
        public void InitQuestionCollection()
        {
            QuestionCollection.Clear();
            QuestionCollection = new ObservableCollection<Question>(new QuestionRepo().GetAllQuestion());
        }
    }
}