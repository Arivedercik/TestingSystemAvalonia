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
    /// Работа с файлом Вопросы
    /// </summary>
    public class QuestionRepo
    {
        private const string PathJsonFileQuestions = "Questions.json";


        /// <summary>
        /// Конструктор с проверкой наличия файла
        /// </summary>
        /// <exception cref="Exception">Ошибка наличия файла или доступа</exception>
        public QuestionRepo()
        {
            try
            {
                if (!File.Exists(PathJsonFileQuestions))
                {
                    File.Create(PathJsonFileQuestions).Close();
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Добавление вопросов в файл
        /// </summary>
        /// <param name="question">Вопрос</param>
        public void AddQuestion(Question question)
        {
            List<Question> questions = new List<Question>(GetAllQuestion());
            int id = questions.Count + 1;
            question.Id = id;
            questions.Add(question);
            File.WriteAllText(PathJsonFileQuestions, JsonSerializer.Serialize(questions));
        }

        /// <summary>
        /// Получение списка всех вопросов
        /// </summary>
        /// <returns>Список вопросов</returns>
        public Question[] GetAllQuestion()
        {
            using (var file = File.OpenText(PathJsonFileQuestions))
            {
                try
                {
                    return JsonSerializer.Deserialize<Question[]>(file.ReadToEnd());
                }
                catch (Exception ex)
                {
                    return [];
                }
            }
        }
    }
}
