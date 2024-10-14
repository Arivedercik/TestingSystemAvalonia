﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystemAvalonia.Models
{
    /// <summary>
    /// Тесты
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование теста
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание теста
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Список вопросов
        /// </summary>
        public ObservableCollection<Questions> QuestionCollection { get; set; }
    }
}
