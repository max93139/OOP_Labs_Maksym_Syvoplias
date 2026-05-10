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
    internal class Service
    {
        private string outputFormat; 
        private string filePath;
        private string data;


        // Конструктор за замовчуванням 
        public Service()
        {
            this.outputFormat = "";
            this.filePath = "";
            this.data = "";
        }

        // Конструктор з параметрами
        public Service(string outputFormat, string filePath, string data)
        {
            this.outputFormat = outputFormat;
            this.filePath = filePath;
            this.data = data;
        }

        // Конструктор копії
        public Service(Service other)
        {
            this.outputFormat = other.outputFormat;
            this.filePath = other.filePath;
            this.data = other.data;
        }

        public string OutputFormat
        {
            get { return outputFormat; }
            set { outputFormat = value; }
        }

         public string WfilePath
        {
            get { return this.filePath; }
            set {  filePath = value; }
        }
        public string Data
        {
            get  {return data; }
            set  { data = value;}
        }

        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }
        public string ReadFromConsole()
        {
            return Console.ReadLine() ?? "";
        }
        public void WriteToFile(string content)
        {
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }
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
