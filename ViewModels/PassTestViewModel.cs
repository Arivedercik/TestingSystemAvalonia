using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text.Json;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.ViewModels
{
    /// <summary>
    /// ����������� �����
    /// </summary>
    public class PassTestViewModel : ReactiveObject
    {
        WorkWithFileViewModel wwf = new WorkWithFileViewModel();


        /// <summary>
        /// ����������� ��� �������������
        /// </summary>
        public PassTestViewModel()
        {
            InitTest();
        }

        /// <summary>
        /// ������������� ������ ������
        /// </summary>
        public void InitTest()
        {
            TestCollection = wwf.SearchTest();
        }

        #region ���� ������ �����
        int _allpoint;


        public int AllPoint
        {
            get => _allpoint;
            set => this.RaiseAndSetIfChanged(ref _allpoint, value);
        }
        #endregion

        #region ���� ��������� ������������� ������
        int _userPoint;


        public int UserPoint
        {
            get => _userPoint;
            set => this.RaiseAndSetIfChanged(ref _userPoint, value);
        }
        #endregion

        #region ���� ������ ���������� �����
        string _result;


        public string Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref this._result, value);
        }
        #endregion

        #region ���� ��������� ������ ������
        string _btnText = "��������� ���������";


        public string BtnText
        {
            get => _btnText;
            set => this.RaiseAndSetIfChanged(ref _btnText, value);
        }
        #endregion

        #region ���� ���������
        bool _isVisibleTest = true;


        public bool IsVisibleTest
        {
            get => _isVisibleTest;
            set => this.RaiseAndSetIfChanged(ref _isVisibleTest, value);
        }
        #endregion

        #region ��������� ����
        Tests _currentTest;


        public Tests CurrentTest
        {
            get => _currentTest;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentTest, value);
                if (CurrentTest != null)
                {
                    QuestionTestCollection.Clear();

                    try
                    {
                        using (var file = File.OpenText(wwf.pathCollection[0]))
                        {
                            string sJson;
                            while ((sJson = file.ReadLine()) != null)
                            {
                                Tests test = JsonSerializer.Deserialize<Tests>(sJson);

                                if (test.Id == CurrentTest.Id)
                                {
                                    foreach (var item in CurrentTest.QuestionCollection)
                                    {
                                        QuestionTestCollection.Add(item);
                                    }

                                    break;
                                }
                            }
                        }
                    }
                    catch
                    {
                        //
                    }
                }
            }
        }
        #endregion

        #region ��������� �����
        Answers _selectedItem;


        public Answers SelectedItem
        {
            get => _selectedItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItem, value);

                ChoiceUser(AnswerTestCollection);
            }
        }
        #endregion

        #region ��������� ������
        Questions _currentQuest;


        public Questions CurrentQuestion
        {
            get => _currentQuest;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentQuest, value);

                if (CurrentQuestion != null)
                {
                    answerrrrr();
                }
                else
                {
                    AnswerTestCollection.Clear();
                }
            }
        }
        #endregion

        #region ������ ������
        ObservableCollection<Tests> _testCollection = new ObservableCollection<Tests>();


        public ObservableCollection<Tests> TestCollection
        {
            get => _testCollection;
            set => this.RaiseAndSetIfChanged(ref this._testCollection, value);
        }
        #endregion

        #region ������ ��������
        ObservableCollection<Questions> _questionTestCollection = new ObservableCollection<Questions>();


        public ObservableCollection<Questions> QuestionTestCollection
        {
            get => _questionTestCollection;
            set => this.RaiseAndSetIfChanged(ref this._questionTestCollection, value);
        }
        #endregion

        #region ������ �������
        ObservableCollection<Answers> _answerTestCollection = new ObservableCollection<Answers>();


        public ObservableCollection<Answers> AnswerTestCollection
        {
            get => _answerTestCollection;
            set => this.RaiseAndSetIfChanged(ref _answerTestCollection, value);
        }
        #endregion

        /// <summary>
        /// ��������� ������ ������� ������������
        /// </summary>
        /// <param name="AnswerTestCollection">������ ���� ������� �� ������</param>
        public void ChoiceUser(ObservableCollection<Answers> AnswerCollection)
        {
            foreach (var item in AnswerCollection)
            {
                if (item.IdQuestion == CurrentQuestion.Id && item.Id == SelectedItem.Id && !ResultTestUser.AnswerUser.Contains(item))
                {
                    ResultTestUser.AnswerUser.Add(item);

                    break;
                }
            }
        }

        bool _isSelect=false;
        public bool IsSelect
        {
            get => _isSelect;
            set => this.RaiseAndSetIfChanged(ref _isSelect, value);
        }

        public void answerrrrr()
        {
            try
            {
                AnswerTestCollection.Clear();

                using (var file = File.OpenText(wwf.pathCollection[2]))
                {
                    string sJson;

                    while ((sJson = file.ReadLine()) != null)
                    {
                        Answers answer = JsonSerializer.Deserialize<Answers>(sJson);

                        if (answer.IdQuestion == CurrentQuestion.Id)
                        {
                            if (ResultTestUser.AnswerUser.Contains(answer))
                            {
                                IsSelect = true;
                                answer.IsTrue = true;
                            }
                            else
                            {
                                IsSelect = false;
                                answer.IsTrue = false;
                            }
                            AnswerTestCollection.Add(answer);
                        }
                    }
                }
            }
            catch
            (Exception ex)
            {

            }
        }

        /*
        /// <summary>
        /// ��������� ������� �� ������
        /// </summary>
        public void EditChoiceUser()
        {
            try
            {

                if (AnswerTestCollection.Count > 0)
                {
                    ChoiceUser(AnswerTestCollection);
                }

                AnswerTestCollection.Clear();

                using (var file = File.OpenText(wwf.pathCollection[2]))
                {
                    string sJson;

                    while ((sJson = file.ReadLine()) != null)
                    {
                        Answers answer = JsonSerializer.Deserialize<Answers>(sJson);

                        if (answer.IdQuestion == CurrentQuestion.Id)
                        {

                            if (AnswerUser.Contains(answer) == false)
                            {
                                answer.IsTrue = false;
                            }
                            else if (AnswerUser.Contains(answer) && AnswerUser[AnswerUser.IndexOf(answer)].IsTrue == false)
                            {
                                answer.IsTrue = false;
                            }

                            AnswerTestCollection.Add(answer);
                        }
                    }
                }
            }
            catch
            {
                //
            }
        }
        */

        /// <summary>
        /// ������� ���������� ������ � �����
        /// </summary>
        /// <param name="IdTest">������������� �����</param>
        /// <returns></returns>
        public int CountPointInTest(int IdTest)
        {
            int countPoint = 0;

            Tests test = wwf.SearchTest().FirstOrDefault(x => x.Id == CurrentTest.Id);
            ObservableCollection<Questions> questions = test.QuestionCollection;
            ObservableCollection<Answers> answer = wwf.SearchAnswer();

            foreach (var item in questions)
            {
                countPoint += answer.Where(x => x.IdQuestion == item.Id && x.IsTrue == true).Count();
            }

            return countPoint;
        }

        /// <summary>
        /// ������� ���������� ������ ������������
        /// </summary>
        /// <param name="AnswerUser">������ ������� ������������</param>
        /// <returns></returns>
        public int CountPointInUser(ObservableCollection<Answers> AnswerUser)
        {
            int countPoint = 0;
            countPoint += AnswerUser.Where(x => x.IsTrue == true).Count();

            return countPoint;
        }

        /// <summary>
        /// �������� ����������
        /// </summary>
        public void ShowResult()
        {
            AllPoint = CountPointInTest(CurrentTest.Id);
            UserPoint = CountPointInUser(ResultTestUser.AnswerUser);
            IsVisibleTest = false;
            BtnText = "������ ������";
            Result = "������� " + UserPoint + " ������ ������ �� " + AllPoint;

            ResultTestUser.AnswerUser.Clear();
        }

        /// <summary>
        /// ��������� ����������� ������
        /// </summary>
        public void StartOver()
        {
            InitTest();
            CurrentTest = wwf.SearchTest().FirstOrDefault(x => x.Id == 1);
            UserPoint = 0;
            IsVisibleTest = true;
            BtnText = "��������� ���������";
            Result = "";
        }
    }
}