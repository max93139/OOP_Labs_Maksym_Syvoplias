//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.IO;
using System.Text;

namespace Lab3
{
    /// <summary>
    /// Клас "Сервіс" — відповідає виключно за введення/виведення даних на консоль
    /// та читання/записування файлів. Не знає про Student та Department.
    /// </summary>
    public class Service
    {
        // Закриті поля
        private string outputFormat;  // Формат виводу
        private string filePath;      // Шлях до файлу
        private string data;          // Дані для обробки

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Service"/> зі стандартними налаштуваннями.
        /// </summary>
        public Service()
        {
            outputFormat = "Console";
            filePath = "department_data.txt";
            data = "";
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Service"/> із заданим форматом виводу та шляхом до файлу.
        /// </summary>
        /// <param name="outputFormat">Формат виводу.</param>
        /// <param name="filePath">Шлях до файлу для збереження даних.</param>
        public Service(string outputFormat, string filePath)
        {
            this.outputFormat = outputFormat;
            this.filePath = filePath;
            this.data = "";
        }

        /// <summary>Отримує або встановлює формат виводу.</summary>
        public string OutputFormat
        {
            get
            {
                return outputFormat;
            }
            set
            {
                outputFormat = value;
            }
        }

        /// <summary>Отримує або встановлює шлях до файлу.</summary>
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        /// <summary>Отримує або встановлює дані для обробки.</summary>
        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        /// <summary>Вивести рядок на консоль.</summary>
        /// <param name="message">Повідомлення для виведення.</param>
        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>Прочитати рядок з консолі.</summary>
        /// <returns>Зчитаний рядок або порожній рядок.</returns>
        public string ReadFromConsole()
        {
            return Console.ReadLine() ?? "";
        }

        /// <summary>Записати дані у файл.</summary>
        /// <param name="content">Вміст для запису.</param>
        public void WriteToFile(string content)
        {
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        /// <summary>Прочитати дані з файлу.</summary>
        /// <returns>Вміст файлу або null, якщо файл не існує.</returns>
        public string? ReadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            else
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
        }

        /// <summary>
        /// Зчитує ціле число з консолі із заданим запитом.
        /// Забезпечує коректне введення: у разі помилки просить повторити.
        /// </summary>
        /// <param name="prompt">Текст-підказка для користувача.</param>
        /// <returns>Введене ціле число.</returns>
        public int ReadInt(string prompt)
        {
            int result = 0;
            bool valid = false;

            while (!valid)
            {
                WriteToConsole(prompt);
                if (int.TryParse(ReadFromConsole(), out int value))
                {
                    result = value;
                    valid = true;
                }
                else
                {
                    WriteToConsole("   Введіть ціле число.");
                }
            }

            return result;
        }
    }
}
