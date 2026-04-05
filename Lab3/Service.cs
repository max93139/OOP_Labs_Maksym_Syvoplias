//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab3
{
    /// <summary>
    /// Клас "Сервіс" — відповідає за введення/виведення даних та запуск меню програми.
    /// </summary>
    public class Service
    {
        // Закриті поля
        private string outputFormat;  // Формат виводу (наприклад, "console")
        private string filePath;      // Шлях до файлу
        private string data;          // Дані для обробки

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Service"/> зі стандартними налаштуваннями.
        /// </summary>
        public Service()
        {
            outputFormat = "console";
            filePath     = "department_data.txt";
            data         = "";
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Service"/> із заданим форматом виводу та шляхом до файлу.
        /// </summary>
        /// <param name="outputFormat">Формат виводу.</param>
        /// <param name="filePath">Шлях до файлу для збереження даних.</param>
        public Service(string outputFormat, string filePath)
        {
            this.outputFormat = outputFormat;
            this.filePath     = filePath;
            this.data         = "";
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Service"/> копіюючи стан іншого сервісу.
        /// </summary>
        /// <param name="other">Сервіс для копіювання.</param>
        public Service(Service other)
        {
            this.outputFormat = other.outputFormat;
            this.filePath     = other.filePath;
            this.data         = other.data;
        }

        /// <summary>
        /// Отримує або встановлює формат виведення (напр., "console").
        /// </summary>
        public string OutputFormat
        {
            get => outputFormat;
            set => outputFormat = value;
        }

        /// <summary>
        /// Отримує або встановлює шлях до файлу.
        /// </summary>
        public string FilePath
        {
            get => filePath;
            set => filePath = value;
        }

        /// <summary>
        /// Отримує або встановлює буфер даних.
        /// </summary>
        public string Data
        {
            get => data;
            set => data = value;
        }


        /// <summary>Вивести рядок на консоль.</summary>
        public void WriteToConsole(string message) => Console.WriteLine(message);

        /// <summary>Прочитати рядок з консолі.</summary>
        public string ReadFromConsole()
        {
            data = Console.ReadLine() ?? "";
            return data;
        }

        /// <summary>Записати дані у файл.</summary>
        public void WriteToFile(string content)
        {
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        /// <summary>Прочитати дані з файлу.</summary>
        public string? ReadFromFile()
        {
            if (!File.Exists(filePath))
                return null;
            data = File.ReadAllText(filePath, Encoding.UTF8);
            return data;
        }

        /// <summary>Ввести дані нової кафедри з консолі.</summary>
        private Department ReadDepartmentFromConsole()
        {
            WriteToConsole("  Назва кафедри: ");
            string name = ReadFromConsole();

            WriteToConsole("  Напрям підготовки: ");
            string program = ReadFromConsole();

            int maxStudents = ReadInt("  Максимальна кількість студентів: ");

            return new Department(name, 0, program, new List<string>(), maxStudents);
        }

        /// <summary>Ввести дані нового студента з консолі. Повертає null, якщо користувач скасував введення (0).</summary>
        private Student? ReadStudentFromConsole(int studentNumber, Department dept)
        {
            Student? result;

            WriteToConsole("  Ім'я студента (0 — назад): ");
            string name = ReadFromConsole();

            if (name.Trim() == "0")
            {
                result = null;
            }
            else
            {
                string? program = ReadProgramFromList(dept);

                if (program == null)
                {
                    result = null;
                }
                else
                {
                    int? workload = ReadCredits();

                    if (workload == null)
                    {
                        result = null;
                    }
                    else
                    {
                        result = new Student(name, program, workload.Value, studentNumber);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Показати пронумерований список напрямів підготовки кафедри
        /// та повернути вибраний напрям. Повертає null при скасуванні (0).
        /// </summary>
        private string? ReadProgramFromList(Department dept)
        {
            string? result;
            var disciplines = dept.Disciplines;

            if (disciplines.Count == 0)
            {
                WriteToConsole("   На кафедрі ще немає жодного напряму підготовки.");
                WriteToConsole("      Спочатку додайте дисципліни (пункт 1.2 або 4).");
                result = null;
            }
            else
            {
                bool chosen = false;
                result = null;

                while (!chosen)
                {
                    WriteToConsole("  Оберіть напрям підготовки (0 — назад до меню):");
                    for (int i = 0; i < disciplines.Count; i++)
                        WriteToConsole($"    {i + 1}. {disciplines[i]}");
                    WriteToConsole("  Ваш вибір (номер): ");

                    if (int.TryParse(ReadFromConsole(), out int choice))
                    {
                        if (choice == 0)
                        {
                            result = null;
                            chosen = true;
                        }
                        else if (choice >= 1 && choice <= disciplines.Count)
                        {
                            result = disciplines[choice - 1];
                            chosen = true;
                        }
                        else
                        {
                            WriteToConsole($"   Введіть число від 0 до {disciplines.Count}.");
                        }
                    }
                    else
                    {
                        WriteToConsole($"   Введіть число від 0 до {disciplines.Count}.");
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Зчитує ціле число з консолі із заданим запитом.
        /// Забезпечує коректне введення: у разі помилки просить повторити.
        /// </summary>
        /// <param name="prompt">Текст-підказка для користувача.</param>
        /// <returns>Введене ціле число.</returns>
        private int ReadInt(string prompt)
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

        /// <summary>Запитати кількість кредитів ЄКТС (1–60). Повертає null при 0 (скасування).</summary>
        private int? ReadCredits()
        {
            const int maxCredits = 30;
            int? result = null;
            bool valid = false;

            while (!valid)
            {
                WriteToConsole($"  Кількість кредитів ЄКТС (1–{maxCredits}, 0 — назад): ");

                if (int.TryParse(ReadFromConsole(), out int credits))
                {
                    if (credits == 0)
                    {
                        result = null;
                        valid = true;
                    }
                    else if (credits >= 1 && credits <= maxCredits)
                    {
                        result = credits;
                        valid = true;
                    }
                    else
                    {
                        WriteToConsole($"   Введіть число від 0 до {maxCredits}.");
                    }
                }
                else
                {
                    WriteToConsole($"   Введіть число від 0 до {maxCredits}.");
                }
            }

            return result;
        }


        /// <summary>
        /// Запитує у користувача номер студента за допомогою власного текстового запиту.
        /// Виводить список і повертає номер (або -1 у разі помилки списку, 0 — скасування).
        /// </summary>
        /// <param name="dept">Кафедра, що містить студентів.</param>
        /// <param name="prompt">Текст запиту, який виводиться користувачеві.</param>
        /// <returns>Вибраний номер або спеціальний код.</returns>
        private int ReadStudentIndex(Department dept, string prompt)
        {
            int result;

            if (dept.Students.Count == 0)
            {
                WriteToConsole("  Список студентів порожній.");
                result = -1;
            }
            else
            {
                PrintStudentList(dept);
                result = ReadInt(prompt);
            }

            return result;
        }

        /// <summary>
        /// Виводить пронумерований список студентів на консоль.
        /// </summary>
        /// <param name="dept">Кафедра.</param>
        private void PrintStudentList(Department dept)
        {
            WriteToConsole("\n Список студентів ");
            for (int i = 0; i < dept.Students.Count; i++)
            {
                WriteToConsole($"  {i + 1}. {dept.Students[i].Name}");
            }
        }

        /// <summary>
        /// Форматує загальну інформацію про кафедру у вигляд рядка.
        /// </summary>
        /// <param name="dept">Кафедра для форматування.</param>
        /// <returns>Відформатований рядок.</returns>
        private string FormatDepartment(Department dept)
        {
            string disciplines;
            if (dept.Disciplines.Count > 0)
                disciplines = string.Join(", ", dept.Disciplines);
            else
                disciplines = "—";

            return $"Кафедра: {dept.Name}\n" +
                   $"Напрям: {dept.EducationProgram}\n" +
                   $"Студентів: {dept.StudentCount}/{dept.MaxStudents}\n" +
                   $"Дисципліни: {disciplines}";
        }

        /// <summary>
        /// Форматує детальну інформацію про одного студента.
        /// </summary>
        /// <param name="s">Студент для форматування.</param>
        /// <returns>Відформатований рядок.</returns>
        private string FormatStudent(Student s)
        {
            return $"  #{s.StudentNumber} {s.Name} | Напрям: {s.EducationProgram} " +
                   $"| Кредити: {s.WorkloadLevel} " +
                   $"| Рейтинг: {s.Rating:F2} | Оцінки: [{string.Join(", ", s.Grades)}]";
        }

        /// <summary>
        /// Будує повний текстовий звіт про стан кафедри для запису у файл.
        /// </summary>
        /// <param name="dept">Кафедра для генерації звіту.</param>
        /// <returns>Повний текст для файлу.</returns>
        private string BuildFileContent(Department dept)
        {
            var sb = new StringBuilder();
            sb.AppendLine(FormatDepartment(dept));
            sb.AppendLine("Студенти:");
            if (dept.Students.Count == 0)
            {
                sb.AppendLine("  (немає)");
            }
            else
            {
                foreach (var s in dept.Students)
                {
                    sb.AppendLine(FormatStudent(s));
                }
            }
            return sb.ToString();
        }


        /// <summary>Запустити сценарій роботи програми.</summary>
        public void RunMenu()
        {
            Department department = new Department();
            int nextStudentNumber = 1; // лічильник для присвоєння унікальних номерів

            bool running = true;
            while (running)
            {
                PrintMenu();
                string choice = ReadFromConsole();

                switch (choice.Trim())
                {
                    //  Створити кафедру
                    case "1":
                        department = ReadDepartmentFromConsole();
                        nextStudentNumber = 1;
                        WriteToConsole($"   Кафедру «{department.Name} створено.");

                        WriteToConsole("\n  Додавання дисциплін. Введіть 0 для завершення");
                        string disciplineName;
                        do
                        {
                            WriteToConsole("  Введіть назву дисципліни (0 — завершити): ");
                            disciplineName = ReadFromConsole();

                            if (disciplineName != "0")
                            {
                                if (!string.IsNullOrWhiteSpace(disciplineName))
                                {
                                    department.AddDiscipline(disciplineName);
                                }
                                else
                                {
                                    WriteToConsole("   Назва не може бути порожньою.");
                                }
                            }
                        } while (disciplineName != "0");

                        WriteToConsole($"   Дисципліни: {(department.Disciplines.Count > 0 ? string.Join(", ", department.Disciplines) : "немає")}");
                        break;

                    //  Додати студента
                    case "2":
                    {
                        Student? newStudent = ReadStudentFromConsole(nextStudentNumber, department);
                        if (newStudent == null)
                        {
                            WriteToConsole("  ← Додавання студента скасовано.");
                        }
                        else
                        {
                            if (department.AddStudent(newStudent))
                            {
                                WriteToConsole("   Вкажіть причину додавання студента (вступ, поновлення, переведення тощо): ");
                                string reason = ReadFromConsole();
                                nextStudentNumber++;
                                WriteToConsole($"   Студента «{newStudent.Name} додано (Причина: {reason}). Всього: {department.StudentCount}/{department.MaxStudents}");
                            }
                            else
                            {
                                WriteToConsole("   Досягнуто максимальну кількість студентів або невірна спеціальність!");
                            }
                        }
                        break;
                    }
                    //  Видалити студента
                    case "3":
                    {
                        int idx = ReadStudentIndex(department, "  Виберіть номер студента для видалення (0 — назад до меню): ");
                        if (idx >= 1)
                        {
                            WriteToConsole("   Вкажіть причину видалення студента (переїхав, перевівся, відрахування тощо): ");
                            string reason = ReadFromConsole();
                            Student? removed = department.RemoveStudentByNumber(idx);
                            if (removed != null)
                            {
                                WriteToConsole($"   Студента «{removed.Name} видалено (Причина: {reason}). Залишилось: {department.StudentCount}");
                            }
                            else
                            {
                                WriteToConsole("   Невірний номер.");
                            }
                        }
                        else
                        {
                        }
                        break;
                    }

                    //  Додати дисципліну
                    case "4":
                        WriteToConsole("\n[4] Назва нової дисципліни: ");
                        department.AddDiscipline(ReadFromConsole());
                        WriteToConsole($"   Дисципліни: {string.Join(", ", department.Disciplines)}");
                        break;

                    //  Видалити дисципліну
                    case "5":
                    {
                        var discs = department.Disciplines;
                        if (discs.Count == 0)
                        {
                            WriteToConsole("  Дисциплін немає.");
                        }
                        else
                        {
                            WriteToConsole("\n[5] Дисципліни:");
                            for (int i = 0; i < discs.Count; i++)
                                WriteToConsole($"  {i + 1}. {discs[i]}");
                            WriteToConsole("  Введіть назву дисципліни для видалення: ");
                            string discName = ReadFromConsole();
                            if (department.RemoveDiscipline(discName))
                            {
                                WriteToConsole($"   «{discName} видалено.");
                            }
                            else
                            {
                                WriteToConsole($"   Дисципліну «{discName} не знайдено.");
                            }
                        }
                        break;
                    }

                    //  Розрахувати рейтинг студента
                    case "6":
                    {
                        int idx = ReadStudentIndex(department, "  Виберіть номер студента для розрахунку рейтингу (0 — назад до меню): ");
                        if (idx >= 1)
                        {
                            Student? s = department.GetStudentByNumber(idx);
                            if (s == null)
                            {
                                WriteToConsole("   Невірний номер.");
                            }
                            else
                            {
                                double r = s.CalculateRating();
                                WriteToConsole($"  Рейтинг «{s.Name}: {r:F2} балів");
                            }
                        }
                        else
                        {
                        }
                        break;
                    }

                    //  Переглянути оцінки студента
                    case "7":
                    {
                        int idx = ReadStudentIndex(department, "  Виберіть номер студента для перегляду оцінок (0 — назад до меню): ");
                        if (idx >= 1)
                        {
                            Student? s = department.GetStudentByNumber(idx);
                            if (s == null)
                            {
                                WriteToConsole("   Невірний номер.");
                            }
                            else
                            {
                                var gs = s.ViewGrades();
                                if (gs.Count == 0)
                                {
                                    WriteToConsole($"  У «{s.Name} оцінок немає.");
                                }
                                else
                                {
                                    WriteToConsole($"  Оцінки «{s.Name}: {string.Join(", ", gs)}");
                                }
                            }
                        }
                        else
                        {
                        }
                        break;
                    }

                    //  Додати оцінку студенту
                    case "8":
                    {
                        int idx = ReadStudentIndex(department, "  Виберіть номер студента для додавання оцінки (0 — назад до меню): ");
                        if (idx >= 1)
                        {
                            Student? s = department.GetStudentByNumber(idx);
                            if (s == null)
                            {
                                WriteToConsole("   Невірний номер.");
                            }
                            else
                            {
                                int grade = ReadInt($"  Оцінка для «{s.Name} (0–100): ");
                                double currentRating = s.CalculateRating();
                                if (s.Grades.Count > 0 && currentRating + grade > 100)
                                {
                                    WriteToConsole($"   Додавання оцінки перевищить рейтинг 100 балів. Оцінка НЕ додана.");
                                }
                                else
                                {
                                    if (s.AddGrade(grade))
                                    {
                                        WriteToConsole($"   Оцінку {grade} додано. Новий рейтинг: {s.Rating:F2}");
                                    }
                                    else
                                    {
                                        WriteToConsole("   Недійсна оцінка. Допустимо: 0–100.");
                                    }
                                }
                            }
                        }
                        else
                        {
                        }
                        break;
                    }

                    //  Зберегти дані у файл та показати поточний стан
                    case "9":
                        WriteToConsole("\n[9] Збереження даних...");
                        string fileContent = BuildFileContent(department);
                        WriteToFile(fileContent);
                        WriteToConsole($"   Дані збережено у файл «{filePath}.");

                        WriteToConsole("\n" + FormatDepartment(department));

                        WriteToConsole("\n  ══ Студенти кафедри ══");
                        if (department.Students.Count == 0)
                        {
                            WriteToConsole("  (немає)");
                        }
                        else
                        {
                            foreach (var sv in department.Students)
                                WriteToConsole(FormatStudent(sv));
                        }
                        break;


                    //  Змінити спеціальність студента
                    case "10":
                    {
                        int idx = ReadStudentIndex(department, "  Виберіть номер студента для зміни спеціальності (0 — назад до меню): ");
                        if (idx >= 1)
                        {
                            Student? s = department.GetStudentByNumber(idx);
                            if (s == null)
                            {
                                WriteToConsole("   Невірний номер.");
                            }
                            else
                            {
                                string? newProgram = ReadProgramFromList(department);
                                if (newProgram == null)
                                {
                                    WriteToConsole("  ← Зміну спеціальності скасовано.");
                                }
                                else
                                {
                                    s.ChangeSpecialty(newProgram);
                                    WriteToConsole($"   Спеціальність студента «{s.Name} змінено на «{newProgram}.");
                                }
                            }
                        }
                        else
                        {
                        }
                        break;
                    }

                    //  Змінити обсяг навчального навантаження
                    case "11":
                    {
                        int idx = ReadStudentIndex(department, "  Виберіть номер студента для зміни навантаження (0 — назад до меню): ");
                        if (idx >= 1)
                        {
                            Student? s = department.GetStudentByNumber(idx);
                            if (s == null)
                            {
                                WriteToConsole("   Невірний номер.");
                            }
                            else
                            {
                                int newWorkload = ReadInt($"  Новий обсяг кредитів ЄКТС для «{s.Name}: ");
                                s.ChangeWorkload(newWorkload);
                                WriteToConsole($"   Обсяг навантаження студента «{s.Name} змінено на {s.WorkloadLevel} кредитів.");
                            }
                        }
                        else
                        {
                        }
                        break;
                    }

                    //  Вихід
                    case "0":
                        WriteToConsole("\nДо побачення!");
                        running = false;
                        break;

                    default:
                        WriteToConsole("   Невідома опція. Спробуйте ще раз.");
                        break;
                }
            }
        }

        /// <summary>
        /// Виводить головне меню програми на консоль.
        /// </summary>
        private void PrintMenu()
        {

            WriteToConsole("СИСТЕМА УПРАВЛІННЯ КАФЕДРОЮ");
            WriteToConsole("  1   — Створити кафедру              ");
            WriteToConsole("  2   — Додати студента               ");
            WriteToConsole("  3   — Видалити студента             ");
            WriteToConsole("  4   — Додати дисципліну             ");
            WriteToConsole("  5   — Видалити дисципліну           ");
            WriteToConsole("  6   — Розрахувати рейтинг студента  ");
            WriteToConsole("  7   — Переглянути оцінки студента   ");
            WriteToConsole("  8   — Додати оцінку студенту        ");
            WriteToConsole("  9   — Зберегти дані у файл          ");
            WriteToConsole("  10  — Змінити спеціальність студента");
            WriteToConsole("  11  — Змінити навантаження студента ");
            WriteToConsole("  0   — Вихід                         ");
            Console.Write("Ваш вибір: ");
        }
    }
}
