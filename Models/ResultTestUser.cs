using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystemAvalonia.Models
{
    public static class ResultTestUser
    {
        private static ObservableCollection<Answers> _answerUser = new ObservableCollection<Answers>();

        public static ObservableCollection<Answers> AnswerUser
        {
            get => _answerUser;
            set => _answerUser = value;
        }
    }
}
