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
    /// ���������� ��������
    /// </summary>
    public class AddQuestionViewModel : ReactiveObject
    {
        /// <summary>
        /// ����������� ��� ������������� �������
        /// </summary>
        public AddQuestionViewModel()
        {
            InitQuestionCollection();
        }

        /// <summary>
        /// ������������� ������ ��������
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

        #region ���� ������������ �������
        private string _nameQuestion = "";

        public string NameQuestion
        {
            get => _nameQuestion;
            set => this.RaiseAndSetIfChanged(ref _nameQuestion, value);
        }
        #endregion

        #region ���� ������������ ������
        private string _nameAnswer = "";

        public string NameAnswer
        {
            get => _nameAnswer;
            set => this.RaiseAndSetIfChanged(ref _nameAnswer, value);
        }
        #endregion

        #region ���� ������ ��������
        private ObservableCollection<Questions> _questionCollection = new ObservableCollection<Questions>();

        public ObservableCollection<Questions> QuestionCollection
        {
            get => _questionCollection;
            set => this.RaiseAndSetIfChanged(ref _questionCollection, value);
        }
        #endregion

        #region ���� ������ �������
        private ObservableCollection<Answers> _answerCollection = new ObservableCollection<Answers>();


        public ObservableCollection<Answers> AnswerCollection
        {
            get => _answerCollection;
            set => this.RaiseAndSetIfChanged(ref _answerCollection, value);
        }        
        #endregion
    }
}