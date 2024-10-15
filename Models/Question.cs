using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystemAvalonia.Models
{
    /// <summary>
    /// Вопросы
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        public string Name { get; set; }  
    }
}
