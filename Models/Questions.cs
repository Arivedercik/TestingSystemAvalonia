using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystemAvalonia.Models
{
    public class Questions
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        public string Name { get; set; }    
        
        /*
        /// <summary>
        /// Список ответов
        /// </summary>
        public ObservableCollection<Answer> AnswerCollection = new ObservableCollection<Answer>();
        */
    }
}
