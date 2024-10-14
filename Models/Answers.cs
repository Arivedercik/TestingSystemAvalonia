using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystemAvalonia.Models
{
    /// <summary>
    /// Ответы
    /// </summary>
    public class Answers
    {
        /// <summary>
        /// Идентификатор ответа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тело ответа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор вопроса к ответу
        /// </summary>
        public int IdQuestion { get; set; }

        /// <summary>
        /// Состояние ответа: верно\неверно
        /// </summary>
        public bool IsTrue {  get; set; }
    }
}
