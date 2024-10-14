using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ReactiveUI;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.ViewModels
{
	public class PassTestViewModel : ReactiveObject
	{
		public PassTestViewModel()
		{
            WorkWithFileViewModel wwf = new WorkWithFileViewModel();
			TestCollection = wwf.SearchTest();
		}


		int _allpoint;
		int _userPoint;

		Tests _currentTest;

		ObservableCollection<Tests> _testCollection;

        ObservableCollection<Questions> _questionTestCollection;


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

		public Tests CurrentTest
		{
			get => _currentTest;
			set 
			{
				this.RaiseAndSetIfChanged(ref _currentTest, value);
                foreach (var item in CurrentTest.questionCollection)
                {
					QuestionTestCollection.Add(item);
                }
            } 
		}

		public ObservableCollection<Tests> TestCollection
		{
			get => _testCollection;
			set => this.RaiseAndSetIfChanged(ref this._testCollection, value);
		}
        public ObservableCollection<Questions> QuestionTestCollection
		{
            get => _questionTestCollection;
            set => this.RaiseAndSetIfChanged(ref this._questionTestCollection, value);
        }
    }
}