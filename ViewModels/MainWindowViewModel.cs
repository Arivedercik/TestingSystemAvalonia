using Avalonia.Controls;
using ReactiveUI;
using System;
using TestingSystemAvalonia.Models;
using TestingSystemAvalonia.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestingSystemAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserControl _us = new AddItems();

        private AddQuestionViewModel _addQVM = new AddQuestionViewModel();

        private AddTestViewModel _addTVM = new AddTestViewModel();

        private PassTestViewModel _passTest = new PassTestViewModel();


        public UserControl US
        {
            get => _us;
            set => this.RaiseAndSetIfChanged(ref _us, value);
        }

        public AddQuestionViewModel AddQVM
        {
            get => _addQVM;
            set => _addQVM = value;
        }

        public AddTestViewModel AddTVM
        {
            get => _addTVM;
            set => _addTVM = value;
        }

        public PassTestViewModel PassTest
        {
            get => _passTest;
            set => _passTest = value;
        }

        /// <summary>
        /// Сохранение теста в файл по кнопке
        /// </summary>
        public async void ToSaveTest()
        {
            if (String.IsNullOrWhiteSpace(AddTVM.Name))
            {
                var mesBox = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Тест не был сохранен: Введите наименование теста", ButtonEnum.OkCancel);
                var resultMesBox = await mesBox.ShowAsync();

                return;
            }

            try
            {
                Test test = new Test()
                {
                    Name = AddTVM.Name,
                    Description = AddTVM.Description,
                    QuestionCollection = AddTVM.QuestionSelectedCollection
                };

                new TestRepo().AddTest(test);
                AddTVM.Name = "";
                AddTVM.Description = "";
                PassTest.InitTest();
            }
            catch
            {
                throw new Exception("Файл для тестов не был создан");
            }
        }

        /// <summary>
        /// Сохранение вопроса в файл по кнопке
        /// </summary>
        public async void ToSaveQuestion()
        {
            if (String.IsNullOrWhiteSpace(AddQVM.NameQuestion))
            {
                var mesBox = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Вопрос не был сохранен: Введите вопрос", ButtonEnum.OkCancel);
                var resultMesBox = await mesBox.ShowAsync();

                return;
            }

            try
            {
                Question question = new Question()
                {
                    Name = AddQVM.NameQuestion
                };

                QuestionRepo qRepo = new QuestionRepo();
                qRepo.AddQuestion(question);
                int idQuestion = new ObservableCollection<Question>(qRepo.GetAllQuestion()).Count;
                new AnswerRepo().AddAnswer(AddQVM.AnswerCollection.ToList(), idQuestion);
                AddQVM.InitQuestionCollection();
                AddQVM.NameQuestion = "";
            }
            catch
            {
                throw new Exception("Файл для вопросов не был создан");
            }
        }

        /// <summary>
        /// Добавление ответов в список
        /// </summary>
        public async void AddNewAnswer()
        {
            if (String.IsNullOrWhiteSpace(AddQVM.NameAnswer))
            {
                var mesBox = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Ответ не был сохранен: Введите тело ответа", ButtonEnum.OkCancel);
                var resultMesBox = await mesBox.ShowAsync();

                return;
            }

            Answer newAnswer = new Answer()
            {
                Name = AddQVM.NameAnswer
            };

            AddQVM.AnswerCollection.Add(newAnswer);
            AddQVM.NameAnswer = "";
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
        public void SaveChoiceUser()
        {
            if (PassTest.IsVisibleTest)
            {
                PassTest.ShowResult();
                AddQVM.QuestionCollection.Clear();
            }
            else
            {
                PassTest.StartOver();
            }
        }
    }
}