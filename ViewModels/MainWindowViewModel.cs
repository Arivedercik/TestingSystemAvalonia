using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using System;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        UserControl _us = new AddItems();
        public UserControl US
        {
            get => _us;
            set => this.RaiseAndSetIfChanged(ref _us, value);
        }

        AddQuestionViewModel _addQVM = new AddQuestionViewModel();
        public AddQuestionViewModel AddQVM
        {
            get => _addQVM;
            set => _addQVM = value;
        }

        AddTestViewModel _addTVM = new AddTestViewModel();
        public AddTestViewModel AddTVM
        {
            get => _addTVM;
            set => _addTVM = value;
        }

        WorkWithFileViewModel _workWithFile = new WorkWithFileViewModel();
        public WorkWithFileViewModel WorkWithFile
        {
            get => _workWithFile;
            set => _workWithFile = value;
        }

        PassTestViewModel _passTest = new PassTestViewModel();
        public PassTestViewModel PassTest
        {
            get => _passTest;
            set => _passTest = value;
        }
        public void ToSaveTest()
        {
            if (!String.IsNullOrWhiteSpace(AddTVM.Name) && !String.IsNullOrWhiteSpace(AddTVM.Description))
            {                
                WorkWithFile.AddTest(AddTVM.Name, AddTVM.Description, AddTVM.QuestionSelectedCollection);
                AddTVM.Name = "";
                AddTVM.Description = "";
            }
        }
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

        public void AddNewAnswer()
        {
           if(!String.IsNullOrWhiteSpace(AddQVM.NameQuestion) && !String.IsNullOrWhiteSpace(AddQVM.NameAnswer))
            {
                Answer newAnswer = new Answer()
                {
                    Name = AddQVM.NameAnswer
                };
                AddQVM.AnswerCollection.Add(newAnswer);
                AddQVM.NameAnswer = "";
            }
        }

        public void ClearAnswerCollection()
        {
            AddQVM.AnswerCollection.Clear();
        }
    }
}

        
        


