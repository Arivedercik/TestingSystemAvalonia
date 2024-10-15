using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystemAvalonia.Models
{
    /// <summary>
    /// Результат прохождения тестирования
    /// </summary>
    public static class ResultTestUser
    {
        private static Dictionary<Question, List<Answer>> _answearsOnQuestions = new Dictionary<Question, List<Answer>>();

        public static Dictionary<Question, List<Answer>> AnswearsOnQuestions
        {
            get => _answearsOnQuestions;
        }
    }
}
