using Avalonia.Controls;
using Avalonia.Controls.Selection;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text.Json;
using TestingSystemAvalonia.Models;
using TestingSystemAvalonia.Services;

namespace TestingSystemAvalonia.ViewModels
{
    /// <summary>
    /// Прохождение теста
    /// </summary>
    public class PassTestViewModel : ReactiveObject
    {
        private int _allpoint;
        private int _userPoint;

        private string _result;
        private string _btnText = "Проверить результат";

        private bool _isVisibleTest = true;
        private bool _isChangingQuestion = false;

        private Test _currentTest;

        private Question _currentQuest;

        private ObservableCollection<Test> _testCollection = new ObservableCollection<Test>();

        private ObservableCollection<Question> _questionTestCollection = new ObservableCollection<Question>();

        private ObservableCollection<Answer> _answerTestCollection = new ObservableCollection<Answer>();
        private ObservableCollection<Answer> _selectedItems = new ObservableCollection<Answer>();


        /// <summary>
        /// Конструктор для инициализации
        /// </summary>
        public PassTestViewModel()
        {
            InitTest();
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        public int AllPoint
        {
            get => _allpoint;
            set => this.RaiseAndSetIfChanged(ref _allpoint, value);
        }

        public int UserPoint
        {
            get => _userPoint;
            set => this.RaiseAndSetIfChanged(ref _userPoint, value);
        }

        public string Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref this._result, value);
        }

        public string BtnText
        {
            get => _btnText;
            set => this.RaiseAndSetIfChanged(ref _btnText, value);
        }

        public bool IsVisibleTest
        {
            get => _isVisibleTest;
            set => this.RaiseAndSetIfChanged(ref _isVisibleTest, value);
        }

        public Test CurrentTest
        {
            get => _currentTest;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentTest, value);
                if (CurrentTest != null)
                {
                    QuestionTestCollection.Clear();

                    QuestionTestCollection.AddRange(value.QuestionCollection);
                }
            }
        }

        public Question CurrentQuestion
        {
            get => _currentQuest;
            set
            {
                _isChangingQuestion = true;
                this.RaiseAndSetIfChanged(ref _currentQuest, value);

                if (CurrentQuestion != null)
                {
                    RememberSelectedItemInChoiceUser();
                }
                else
                {
                    AnswerTestCollection.Clear();
                }
                _isChangingQuestion = false;
            }
        }

        public ObservableCollection<Test> TestCollection
        {
            get => _testCollection;
            set => this.RaiseAndSetIfChanged(ref this._testCollection, value);
        }

        public ObservableCollection<Question> QuestionTestCollection
        {
            get => _questionTestCollection;
            set => this.RaiseAndSetIfChanged(ref this._questionTestCollection, value);
        }

        public ObservableCollection<Answer> AnswerTestCollection
        {
            get => _answerTestCollection;
            set => this.RaiseAndSetIfChanged(ref _answerTestCollection, value);
        }

        public ObservableCollection<Answer> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItems, value);
                SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
            }
        }

        /// <summary>
        /// Событие запоминания выбранных ответов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectionAnswerUser();
        }


        /// <summary>
        /// Инициализация списка тестов
        /// </summary>
        public void InitTest()
        {
            TestCollection = new ObservableCollection<Test>(new TestRepo().GetAllTests());
        }

        /// <summary>
        /// Изменение выбора ответов пользователя
        /// </summary>
        /// <param name="AnswerTestCollection">Список всех ответов на вопрос</param>
        public void SelectionAnswerUser()
        {
            if (CurrentQuestion is null)
                return;

            ResultTestUser.AnswearsOnQuestions.TryAdd(CurrentQuestion, []);

            if (!_isChangingQuestion)
            {
                ResultTestUser.AnswearsOnQuestions[CurrentQuestion] = SelectedItems.ToList();
            }
        }

        /// <summary>
        /// Вывод ответов на вопрос с запомненными вариантами
        /// </summary>
        /// <exception cref="Exception">Ошибка запоминания</exception>
        public void RememberSelectedItemInChoiceUser()
        {
            try
            {
                AnswerTestCollection.Clear();

                Answer[] answers = new AnswerRepo().GetAllAnswersOnQuestion(CurrentQuestion.Id);

                foreach (var answer in answers)
                    AnswerTestCollection.Add(answer);

                if (ResultTestUser.AnswearsOnQuestions.TryGetValue(CurrentQuestion, out List<Answer>? value))
                {
                    foreach (var answer in value)
                    {
                        SelectedItems.Add(answer);
                    }
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Подсчет правильных баллов в тесте
        /// </summary>
        /// <param name="idTest">Идентификатор теста</param>
        /// <returns></returns>
        public int CountPointInTest(int idTest)
        {
            int countPoint = 0;

            ObservableCollection<Question> questions = new ObservableCollection<Question>(new TestRepo().GetTest(CurrentTest.Id).QuestionCollection);

            foreach (var item in questions)
            {
                countPoint += new AnswerRepo().GetAllAnswers().Where(x => x.IdQuestion == item.Id && x.IsTrue == true).Count();
            }

            return countPoint;
        }

        /// <summary>
        /// Подсчет правильных баллов пользователя
        /// </summary>
        /// <returns>Количество баллов пользователя</returns>
        public int CountPointInUser()
        {
            int countPoint = 0;

            foreach (var item in ResultTestUser.AnswearsOnQuestions)
            {
                countPoint += item.Value.Where(x => x.IsTrue).Count();
            }

            return countPoint;
        }

        /// <summary>
        /// Вывод результата теста
        /// </summary>
        public void ShowResult()
        {
            AllPoint = CountPointInTest(CurrentTest.Id);
            UserPoint = ResultTestUser.AnswearsOnQuestions.Count == 0 ? 0 : CountPointInUser();
            IsVisibleTest = false;
            BtnText = "Начать заново";
            Result = "Набрано " + UserPoint + " верных баллов из " + AllPoint;
            ResultTestUser.AnswearsOnQuestions.Clear();
        }

        /// <summary>
        /// Повторное прохождение тестов
        /// </summary>
        public void StartOver()
        {
            InitTest();
            CurrentTest = new TestRepo().GetTest(1);
            UserPoint = 0;
            IsVisibleTest = true;
            BtnText = "Проверить результат";
            Result = "";
        }
    }
}