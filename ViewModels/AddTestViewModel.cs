using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData.Binding;
using ReactiveUI;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.ViewModels
{
    /// <summary>
    /// Добавление тестов
    /// </summary>
	public class AddTestViewModel : ReactiveObject
    {
        private string _name = "";
        private string _description = "";

        private ObservableCollection<Question> _questionSelectedCollection = new ObservableCollection<Question>();


        /// <summary>
        /// Конструктор для инициализации списков
        /// </summary>
        public AddTestViewModel()
        {
            AddQuestionViewModel AddQ = new AddQuestionViewModel();

            foreach (var item in AddQ.QuestionCollection)
            {
                QuestionSelectedCollection.Add(item);
            }
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public ObservableCollection<Question> QuestionSelectedCollection
        {
            get => _questionSelectedCollection;
            set => this.RaiseAndSetIfChanged(ref _questionSelectedCollection, value);
        }
    }
}