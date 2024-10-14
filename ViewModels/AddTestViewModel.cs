using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData.Binding;
using ReactiveUI;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.ViewModels
{
	public class AddTestViewModel : ReactiveObject
	{
        public AddTestViewModel()
        {
            AddQuestionViewModel AddQ = new AddQuestionViewModel();
            foreach(var item in AddQ.QuestionCollection)
            {
                QuestionSelectedCollection.Add(item);
            }
        }

        private string _name = "";
        private string _description = "";
        private ObservableCollection<Questions> _questionSelectedCollection = new ObservableCollection<Questions>();
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
       
        public ObservableCollection<Questions> QuestionSelectedCollection
        {
            get => _questionSelectedCollection;
            set => this.RaiseAndSetIfChanged(ref _questionSelectedCollection, value);
        }
    }
}