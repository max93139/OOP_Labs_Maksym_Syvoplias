using System;
using System.Collections.Generic;
using System.IO;

namespace Lab3
{
    public class Service
    {
        // Закриті поля
        private string outputFormat;
        private string filePath;
        private string data;

        // 1. Конструктор за замовчуванням (без параметрів)
        public Service()
        {
            outputFormat = "";
            filePath = "";
            data = "";
        }

        // 2. Конструктор з параметрами
        public Service(string outputFormat, string filePath)
        {
            this.outputFormat = outputFormat;
            this.filePath = filePath;
            this.data = "";
        }

        // 3. Конструктор копії
        public Service(Service other)
        {
            this.outputFormat = other.outputFormat;
            this.filePath = other.filePath;
            this.data = other.data;
        }

        // Відкриті властивості (get, set)
        public string OutputFormat
        {
            get { return outputFormat; }
            set { outputFormat = value; }
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        // Відкриті методи

        // Читання з консолі
        public string ReadFromConsole()
        {
            data = Console.ReadLine() ?? "";
            return data;
        }

        // Запис у консоль
        public void WriteToConsole(string dataToWrite)
        {
            Console.WriteLine(dataToWrite);
        }

        // Читання з файлу
        public string ReadFromFile()
        {
            if (File.Exists(filePath))
            {
                data = File.ReadAllText(filePath);
                return data;
            }
            else
            {
                return "Файл не знайдено.";
            }
        }

        // Запис у файл
        public void WriteToFile(string dataToWrite)
        {
            File.WriteAllText(filePath, dataToWrite);
        }

        // Головне меню програми
        public void RunMenu()
        {
            Department department = new Department();
            Student student = new Student();

            bool running = true;
            while (running)
            {
                WriteToConsole("\n===== МЕНЮ =====");
                WriteToConsole("1. Створити кафедру");
                WriteToConsole("2. Створити студента");
                WriteToConsole("3. Додати дисципліну до кафедри");
                WriteToConsole("4. Видалити дисципліну з кафедри");
                WriteToConsole("5. Збільшити кількість студентів");
                WriteToConsole("6. Зменшити кількість студентів");
                WriteToConsole("7. Перевірка акредитації");
                WriteToConsole("8. Додати оцінку студенту");
                WriteToConsole("9. Переглянути оцінки студента");
                WriteToConsole("10. Розрахувати рейтинг студента");
                WriteToConsole("11. Показати інформацію про кафедру");
                WriteToConsole("12. Показати інформацію про студента");
                WriteToConsole("0. Вихід");
                WriteToConsole("================");
                WriteToConsole("Оберіть опцію:");

                string choice = ReadFromConsole();

                switch (choice)
                {
                    case "1":
                        WriteToConsole("Введіть назву кафедри:");
                        string deptName = ReadFromConsole();
                        WriteToConsole("Введіть освітню програму:");
                        string deptProgram = ReadFromConsole();
                        WriteToConsole("Введіть максимальну кількість студентів:");
                        int maxSt = int.Parse(ReadFromConsole());
                        department = new Department(deptName, 0, deptProgram, new List<string>(), maxSt);
                        WriteToConsole("Кафедру створено.");
                        break;

                    case "2":
                        WriteToConsole("Введіть ім'я студента:");
                        string stName = ReadFromConsole();
                        WriteToConsole("Введіть освітню програму:");
                        string stProgram = ReadFromConsole();
                        WriteToConsole("Введіть рівень навантаження:");
                        int workload = int.Parse(ReadFromConsole());
                        WriteToConsole("Введіть номер залікової книжки:");
                        string recordBook = ReadFromConsole();
                        student = new Student(stName, stProgram, workload, recordBook);
                        WriteToConsole("Студента створено.");
                        break;

                    case "3":
                        WriteToConsole("Введіть назву дисципліни:");
                        string disc = ReadFromConsole();
                        department.AddDiscipline(disc);
                        WriteToConsole($"Дисципліну \"{disc}\" додано.");
                        break;

                    case "4":
                        WriteToConsole("Введіть назву дисципліни для видалення:");
                        string discRemove = ReadFromConsole();
                        department.RemoveDiscipline(discRemove);
                        WriteToConsole($"Дисципліну \"{discRemove}\" видалено.");
                        break;

                    case "5":
                        department.IncreaseStudents();
                        WriteToConsole($"Кількість студентів: {department.StudentCount}");
                        break;

                    case "6":
                        department.DecreaseStudents();
                        WriteToConsole($"Кількість студентів: {department.StudentCount}");
                        break;

                    case "7":
                        bool passed = department.AccreditationCheck();
                        if (passed)
                            WriteToConsole($"Кафедра \"{department.Name}\" пройшла акредитацію.");
                        else
                            WriteToConsole($"Кафедра \"{department.Name}\" НЕ пройшла акредитацію.");
                        break;

                    case "8":
                        WriteToConsole("Введіть оцінку:");
                        int grade = int.Parse(ReadFromConsole());
                        student.AddGrade(grade);
                        WriteToConsole("Оцінку додано.");
                        break;

                    case "9":
                        List<int> grades = student.ViewGrades();
                        WriteToConsole($"Оцінки студента {student.Name}: {string.Join(", ", grades)}");
                        break;

                    case "10":
                        double rating = student.CalculateRating();
                        WriteToConsole($"Рейтинг студента {student.Name}: {rating:F2}");
                        break;

                    case "11":
                        WriteToConsole($"Кафедра: {department.Name}");
                        WriteToConsole($"Освітня програма: {department.EducationProgram}");
                        WriteToConsole($"Кількість студентів: {department.StudentCount}/{department.MaxStudents}");
                        WriteToConsole($"Дисципліни: {string.Join(", ", department.Disciplines)}");
                        break;

                    case "12":
                        WriteToConsole($"Студент: {student.Name}");
                        WriteToConsole($"Освітня програма: {student.EducationProgram}");
                        WriteToConsole($"Навантаження: {student.WorkloadLevel}");
                        WriteToConsole($"Залікова книжка: {student.RecordBookNumber}");
                        WriteToConsole($"Оцінки: {string.Join(", ", student.ViewGrades())}");
                        WriteToConsole($"Рейтинг: {student.CalculateRating():F2}");
                        break;

                    case "0":
                        running = false;
                        WriteToConsole("До побачення!");
                        break;

                    default:
                        WriteToConsole("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
