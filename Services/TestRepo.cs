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
    /// Работа с файлом Тесты
    /// </summary>
    public class TestRepo
    {
        private const string PathJsonFileTest = "Tests.json";


        /// <summary>
        /// Конструктор с проверкой наличия файла
        /// </summary>
        /// <exception cref="Exception">Ошибка наличия файла или доступа</exception>
        public TestRepo()
        {
            try
            {
                if (!File.Exists(PathJsonFileTest))
                {
                    File.Create(PathJsonFileTest).Close();
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Добавление теста в файл
        /// </summary>
        /// <param name="test">Тест</param>
        public void AddTest(Test test)
        {
            List<Test> tests = new List<Test>(GetAllTests());
            int id = tests.Count + 1;
            test.Id = id;
            tests.Add(test);
            File.WriteAllText(PathJsonFileTest, JsonSerializer.Serialize(tests));
        }

        /// <summary>
        /// Получение списка всех тестов
        /// </summary>
        /// <returns>Список тестов</returns>
        public Test[] GetAllTests()
        {
            using (var file = File.OpenText(PathJsonFileTest))
            {
                try
                {
                    return JsonSerializer.Deserialize<Test[]>(file.ReadToEnd());
                }
                catch (Exception ex)
                {
                    return [];
                }
            }
        }

        /// <summary>
        /// Поиск теста по идентификатору
        /// </summary>
        /// <param name="idTest">Идентификатор теста</param>
        /// <returns>Тест</returns>
        public Test GetTest(int idTest)
        {
            return GetAllTests().Where(x => x.Id == idTest)
                .FirstOrDefault();                
        }
    }
}
