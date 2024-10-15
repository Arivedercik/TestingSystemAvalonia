using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestingSystemAvalonia.Models;

namespace TestingSystemAvalonia.Services
{
    /// <summary>
    /// Работа с файлом Ответов
    /// </summary>
    public class AnswerRepo
    {
        private const string PathJsonFileAnswers = "Answers.json";


        /// <summary>
        /// Конструктор с проверкой наличия файла
        /// </summary>
        /// <exception cref="Exception">Ошибка наличия файла или доступа</exception>
        public AnswerRepo()
        {
            try
            {
                if (!File.Exists(PathJsonFileAnswers))
                {
                    File.Create(PathJsonFileAnswers).Close();
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Добавление ответов в файл
        /// </summary>
        /// <param name="answerCollection">Список ответов</param>
        /// <param name="idQuestion">Идентификатор вопроса</param>
        public void AddAnswer(List<Answer> answerCollection, int idQuestion)
        {
            List<Answer> allAnswer = new List<Answer>(GetAllAnswers());
            int id = allAnswer.Count + 1;

            foreach (var item in answerCollection)
            {
                item.Id = id;
                item.IdQuestion = idQuestion;
                allAnswer.Add(item);
            }

            File.WriteAllText(PathJsonFileAnswers, JsonSerializer.Serialize(allAnswer));
        }

        /// <summary>
        /// Получение списка всех ответов
        /// </summary>
        /// <returns>Список ответов</returns>
        public Answer[] GetAllAnswers()
        {
            using (var file = File.OpenText(PathJsonFileAnswers))
            {
                try
                {
                    return JsonSerializer.Deserialize<Answer[]>(file.ReadToEnd());
                }
                catch (Exception ex)
                {
                    return [];
                }
            }
        }

        /// <summary>
        /// Получение ответов для конкретного вопроса
        /// </summary>
        /// <param name="idQuestion">Идентификатор вопроса</param>
        /// <returns>Список ответов</returns>
        public Answer[] GetAllAnswersOnQuestion(int idQuestion)
        {
            return GetAllAnswers().Where(a => a.IdQuestion == idQuestion)
                .ToArray();
        }
    }
}
