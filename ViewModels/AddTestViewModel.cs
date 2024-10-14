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
        /// <summary>
        /// Конструктор для инициализации списков
        /// </summary>
        public AddTestViewModel()
        {
            AddQuestionViewModel AddQ = new AddQuestionViewModel();

            foreach(var item in AddQ.QuestionCollection)
            {
                QuestionSelectedCollection.Add(item);
            }
        }

        #region Поле наименования теста
        private string _name = "";

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        #endregion

        #region Поле описания теста
        private string _description = "";

        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }
        #endregion

        #region Список вопросов
        private ObservableCollection<Questions> _questionSelectedCollection = new ObservableCollection<Questions>();

        public ObservableCollection<Questions> QuestionSelectedCollection
        {
            get => _questionSelectedCollection;
            set => this.RaiseAndSetIfChanged(ref _questionSelectedCollection, value);
        }
        #endregion
    }
}