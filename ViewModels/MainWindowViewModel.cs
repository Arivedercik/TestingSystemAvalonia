using Avalonia.Controls;
using DynamicData;
using DynamicData.Kernel;
using ReactiveUI;
using System;
using System.IO;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Главный UserControl
        UserControl _us = new AddItems();


        public UserControl US
        {
            get => _us;
            set => this.RaiseAndSetIfChanged(ref _us, value);
        }
        #endregion

        #region Представление модели Добавления вопроса
        AddQuestionViewModel _addQVM = new AddQuestionViewModel();


        public AddQuestionViewModel AddQVM
        {
            get => _addQVM;
            set => _addQVM = value;
        }
        #endregion

        #region Представление модели Добаления теста
        AddTestViewModel _addTVM = new AddTestViewModel();


        public AddTestViewModel AddTVM
        {
            get => _addTVM;
            set => _addTVM = value;
        }
        #endregion

        #region Представление модели Работа с фалйоми
        WorkWithFileViewModel _workWithFile = new WorkWithFileViewModel();


        public WorkWithFileViewModel WorkWithFile
        {
            get => _workWithFile;
            set => _workWithFile = value;
        }
        #endregion

        #region Представление модели Прохождение теста
        PassTestViewModel _passTest = new PassTestViewModel();
        public PassTestViewModel PassTest
        {
            get => _passTest;
            set => _passTest = value;
        }
        #endregion
        
        /// <summary>
        /// Сохранение теста 
        /// </summary>
        public void ToSaveTest()
        {
            if (!String.IsNullOrWhiteSpace(AddTVM.Name) && !String.IsNullOrWhiteSpace(AddTVM.Description))
            {
                WorkWithFile.AddTest(AddTVM.Name, AddTVM.Description, AddTVM.QuestionSelectedCollection);
                AddTVM.Name = "";
                AddTVM.Description = "";
                PassTest.InitTest();

                foreach (var item in AddQVM.AnswerCollection)
                {
                    item.IsTrue = false;
                }
            }
        }

        /// <summary>
        /// Сохранение вопроса
        /// </summary>
        public void ToSaveQuestion()
        {
            if (!String.IsNullOrWhiteSpace(AddQVM.NameQuestion))
            {
                WorkWithFile.AddAnswer(AddQVM.AnswerCollection);
                WorkWithFile.AddQuestion(AddQVM.NameQuestion);
                AddQVM.InitQuestionCollection();
                AddQVM.NameQuestion = "";
            }
        }

        /// <summary>
        /// Добавление ответов в список
        /// </summary>
        public void AddNewAnswer()
        {
            if (!String.IsNullOrWhiteSpace(AddQVM.NameQuestion) && !String.IsNullOrWhiteSpace(AddQVM.NameAnswer))
            {
                Answers newAnswer = new Answers()
                {
                    Name = AddQVM.NameAnswer
                };

                AddQVM.AnswerCollection.Add(newAnswer);
                AddQVM.NameAnswer = "";
            }
        }

        /// <summary>
        /// Очистка полей ответов
        /// </summary>
        public void ClearAnswerCollection()
        {
            AddQVM.AnswerCollection.Clear();
        }
       
        /// <summary>
        /// Сохранение результата тестирования 
        /// </summary>
        public void SaveChoceUser()
        {
            if (PassTest.IsVisibleTest)
            {
                PassTest.ShowResult();
            }
            else
            {
                PassTest.StartOver();
            }                
        }
    }
}





