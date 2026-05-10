//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.IO;
using System.Text;

namespace Lab5
{
    /// <summary>
    /// Клас «Сервіс» — відповідає виключно за введення/виведення даних на консоль
    /// та читання/записування файлів. Не знає про Student та Department.
    /// </summary>
    public class Service
    {
        private string outputFormat;
        private string filePath;
        private string protocolFilePath;
        private string data;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Service"/> зі стандартними налаштуваннями.
        /// </summary>
        public Service()
        {
            outputFormat     = "Console";
            filePath         = "department_data.txt";
            protocolFilePath = "protocol.txt";
            data             = "";
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Service"/> із заданим форматом і шляхом до файлу.
        /// </summary>
        /// <param name="outputFormat">Формат виводу.</param>
        /// <param name="filePath">Шлях до файлу для збереження даних.</param>
        /// <param name="protocolFilePath">Шлях до файлу протоколу роботи.</param>
        public Service(string outputFormat, string filePath, string protocolFilePath = "protocol.txt")
        {
            this.outputFormat     = outputFormat;
            this.filePath         = filePath;
            this.protocolFilePath = protocolFilePath;
            this.data             = "";
        }

        /// <summary>Отримує або встановлює формат виводу.</summary>
        public string OutputFormat
        {
            get { return outputFormat; }
            set { outputFormat = value; }
        }

        /// <summary>Отримує або встановлює шлях до файлу.</summary>
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        /// <summary>Отримує або встановлює шлях до файлу протоколу.</summary>
        public string ProtocolFilePath
        {
            get { return protocolFilePath; }
            set { protocolFilePath = value; }
        }

        /// <summary>Отримує або встановлює дані для обробки.</summary>
        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>Вивести рядок на консоль і в протокол.</summary>
        /// <param name="message">Повідомлення для виведення.</param>
        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
            File.AppendAllText(protocolFilePath, message + Environment.NewLine, Encoding.UTF8);
        }

        /// <summary>Прочитати рядок з консолі і записати в протокол.</summary>
        /// <returns>Зчитаний рядок або порожній рядок.</returns>
        public string ReadFromConsole()
        {
            string input = Console.ReadLine() ?? "";
            File.AppendAllText(protocolFilePath, "> " + input + Environment.NewLine, Encoding.UTF8);
            return input;
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
        /// У разі некоректного введення просить повторити.
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
                    valid  = true;
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
