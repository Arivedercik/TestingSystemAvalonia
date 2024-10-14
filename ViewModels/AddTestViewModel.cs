using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData.Binding;
using ReactiveUI;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.ViewModels
{
    /// <summary>
    /// ���������� ������
    /// </summary>
	public class AddTestViewModel : ReactiveObject
	{
        /// <summary>
        /// ����������� ��� ������������� �������
        /// </summary>
        public AddTestViewModel()
        {
            AddQuestionViewModel AddQ = new AddQuestionViewModel();

            foreach(var item in AddQ.QuestionCollection)
            {
                QuestionSelectedCollection.Add(item);
            }
        }

        #region ���� ������������ �����
        private string _name = "";

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        #endregion

        #region ���� �������� �����
        private string _description = "";

        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }
        #endregion

        #region ������ ��������
        private ObservableCollection<Questions> _questionSelectedCollection = new ObservableCollection<Questions>();

        public ObservableCollection<Questions> QuestionSelectedCollection
        {
            get => _questionSelectedCollection;
            set => this.RaiseAndSetIfChanged(ref _questionSelectedCollection, value);
        }
        #endregion
    }
}