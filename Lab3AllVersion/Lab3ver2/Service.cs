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
        public void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>Прочитати рядок з консолі.</summary>
        public string ReadFromConsole()
        {
            return Console.ReadLine() ?? "";
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
            {
                return null;
            }
            else
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
        }

        /// <summary>Ввести дані нової кафедри з консолі.</summary>
        private Department ReadDepartmentFromConsole()
        {
            WriteToConsole("  Назва кафедри: ");
            string name = ReadFromConsole();

            WriteToConsole("  Спеціальність  підготовки: ");
            string program = ReadFromConsole();

            int maxStudents = ReadInt("  Максимальна кількість студентів: ");

            return new Department(name, 0, program, new List<string>(), maxStudents);
        }

        /// <summary>Ввести дані нового студента з консолі.</summary>
        private Student? ReadStudentFromConsole(int studentNumber, Department dept)
        {
            Student? result = null;

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
        /// Показати пронумерований список Спеціальність ів підготовки кафедри
        /// та повернути вибраний Спеціальність . Повертає null при скасуванні (0).
        /// </summary>
        private string? ReadProgramFromList(Department dept)
        {
            string? result = null;
            var disciplines = dept.Disciplines;

            if (disciplines.Count == 0)
            {
                WriteToConsole("   На кафедрі ще немає жодного Спеціальність у підготовки.");
                WriteToConsole("      Спочатку додайте дисципліни (пункт 1.2 або 4).");
                result = null;
            }
            else
            {
                bool chosen = false;
                result = null;

                while (!chosen)
                {
                    WriteToConsole("  Оберіть Спеціальність  підготовки (0 — назад до меню):");
                    for (int i = 0; i < disciplines.Count; i++)
                    {
                        WriteToConsole($"    {i + 1}. {disciplines[i]}");
                    }
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

        /// <summary>Запитати кількість кредитів ЄКТС (1-30). Повертає null при 0 (скасування).</summary>
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
            int result = 0;

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
            string disciplines = "";
            if (dept.Disciplines.Count > 0)
            {
                disciplines = string.Join(", ", dept.Disciplines);
            }
            else
            {
                disciplines = "—";
            }

            return $"Кафедра: {dept.Name}\n" +
                   $"Спеціальність : {dept.EducationProgram}\n" +
                   $"Студентів: {dept.StudentCount}/{dept.MaxStudents}\n" +
                   $"Дисципліни: {disciplines}";
        }

        /// <summary>
        /// Форматує детальну інформацію про одного студента.
        /// </summary>
        /// <param name="s">Студент для форматування.</param>
        /// <returns>Відформатований рядок.</returns>
        private string FormatStudent(Student student)
        {
            string reason = "";
            if (!string.IsNullOrEmpty(student.AdmissionReason))
            {
                reason = student.AdmissionReason;
            }
            else
            {
                reason = "не вказано";
            }

            return $"  #{student.StudentNumber} {student.Name}  Спеціальність : {student.EducationProgram} " +
                   $" Кредити: {student.WorkloadLevel} " +
                   $" Рейтинг: {student.Rating:F2}  Оцінки: [{string.Join(", ", student.Grades)}] " +
                   $" Причина: {reason}";
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
                    case "1":
                        HandleCreateDepartment(ref department, ref nextStudentNumber);
                        break;
                    case "2":
                        HandleAddStudent(department, ref nextStudentNumber);
                        break;
                    case "3":
                        HandleRemoveStudent(department);
                        break;
                    case "4":
                        HandleAddDiscipline(department);
                        break;
                    case "5":
                        HandleRemoveDiscipline(department);
                        break;
                    case "6":
                        HandleCalculateRating(department);
                        break;
                    case "7":
                        HandleViewGrades(department);
                        break;
                    case "8":
                        HandleAddGrade(department);
                        break;
                    case "9":
                        HandleSaveData(department);
                        break;
                    case "10":
                        HandleChangeSpecialty(department);
                        break;
                    case "11":
                        HandleChangeWorkload(department);
                        break;
                    case "12":
                        HandleReadData();
                        break;
                    case "13":
                        HandleCompetitiveWorkMenu(department);
                        break;
                    case "0":
                        WriteToConsole("\nДо побачення!");
                        Console.Clear();
                        running = false;  
                        break; 
                    default: 
                        WriteToConsole("   Невідома опція. Спробуйте ще раз.");
                        break;
                }
            }
        }

        /// <summary>
        /// Обробляє процес створення нової кафедри через консоль.
        /// </summary>
        /// <param name="department">Посилання на об'єкт кафедри для його ініціалізації.</param>
        /// <param name="nextStudentNumber">Посилання на лічильник студентів для його скидання.</param>
        private void HandleCreateDepartment(ref Department department, ref int nextStudentNumber)
        {
            department = ReadDepartmentFromConsole();
            nextStudentNumber = 1;
            WriteToConsole($"   Кафедру «{department.Name} створено.");
            AddDisciplinesDuringCreation(department);
        }

        /// <summary>
        /// Допоміжний метод для додавання дисциплін під час створення кафедри.
        /// </summary>
        /// <param name="department">Кафедра, до якої додаються дисципліни.</param>
        private void AddDisciplinesDuringCreation(Department department)
        {
            WriteToConsole("\n  Додавання дисциплін. Введіть 0 для завершення");
            string disciplineName = "";
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
                else
                {
                    // Завершуємо введення дисциплін
                }
            } while (disciplineName != "0");

            string disciplinesText = "";
            if (department.Disciplines.Count > 0)
            {
                disciplinesText = string.Join(", ", department.Disciplines);
            }
            else
            {
                disciplinesText = "немає";
            }
            WriteToConsole($"   Дисципліни: {disciplinesText}");
        }

        /// <summary>
        /// Обробляє додавання нового студента.
        /// </summary>
        /// <param name="department">Кафедра, куди додається студент.</param>
        /// <param name="nextStudentNumber">Лічильник студентів (оновлюється при успішному додаванні).</param>
        private void HandleAddStudent(Department department, ref int nextStudentNumber)
        {
            Student? newStudent = ReadStudentFromConsole(nextStudentNumber, department);
            if (newStudent == null)
            {
                WriteToConsole("   Додавання студента скасовано.");
            }
            else
            {
                if (department.AddStudent(newStudent))
                {
                    WriteToConsole("   Вкажіть причину додавання студента (вступ, поновлення, переведення тощо): ");
                    string reason = ReadFromConsole();
                    newStudent.AdmissionReason = reason;
                    nextStudentNumber++;
                    WriteToConsole($"   Студента «{newStudent.Name} додано (Причина: {reason}). Всього: {department.StudentCount}/{department.MaxStudents}");
                }
                else
                {
                    WriteToConsole("   Досягнуто максимальну кількість студентів або невірна спеціальність!");
                }
            }
        }

        /// <summary>
        /// Обробляє видалення студента.
        /// </summary>
        /// <param name="department">Кафедра, з якої видаляється студент.</param>
        private void HandleRemoveStudent(Department department)
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
                // Повернення до меню
            }
        }

        /// <summary>
        /// Обробляє додавання нової дисципліни до кафедри.
        /// </summary>
        /// <param name="department">Кафедра, до якої додається дисципліна.</param>
        private void HandleAddDiscipline(Department department)
        {
            WriteToConsole("\n[4] Назва нової дисципліни: ");
            department.AddDiscipline(ReadFromConsole());
            WriteToConsole($"   Дисципліни: {string.Join(", ", department.Disciplines)}");
        }

        /// <summary>
        /// Обробляє видалення дисципліни з кафедри.
        /// </summary>
        /// <param name="department">Кафедра, з якої видаляється дисципліна.</param>
        private void HandleRemoveDiscipline(Department department)
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
                {
                    WriteToConsole($"  {i + 1}. {discs[i]}");
                }
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
        }

        /// <summary>
        /// Викликає розрахунок рейтингу для обраного студента та виводить його.
        /// </summary>
        /// <param name="department">Кафедра, до якої належить студент.</param>
        private void HandleCalculateRating(Department department)
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
                // Повернення до меню
            }
        }

        /// <summary>
        /// Виводить список оцінок обраного студента.
        /// </summary>
        /// <param name="department">Кафедра, до якої належить студент.</param>
        private void HandleViewGrades(Department department)
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
                        WriteToConsole($"  Оцінки «{s.Name}»: [{string.Join(", ", gs)}]");
                    }
                }
            }
            else
            {
                // Повернення до меню
            }
        }

        /// <summary>
        /// Обробляє процес додавання нових оцінок обраному студенту.
        /// </summary>
        /// <param name="department">Кафедра, до якої належить студент.</param>
        private void HandleAddGrade(Department department)
        {
            int idx = ReadStudentIndex(department, "  Виберіть номер студента для додавання оцінки (0 — назад до меню): ");
            if (idx >= 1)
            {
                Student? student = department.GetStudentByNumber(idx);
                if (student == null)
                {
                    WriteToConsole("   Невірний номер.");
                }
                else
                {
                    bool isAddingGrades = true;
                    while (isAddingGrades)
                    {
                        int grade = ReadInt($"  Оцінка для «{student.Name} (0–100, -1 — завершити): ");
                        if (grade == -1)
                        {
                            isAddingGrades = false;
                        }
                        else
                        {
                            if (student.AddGrade(grade))
                            {
                                WriteToConsole($"   Оцінку {grade} додано. Новий рейтинг: {student.Rating:F2}");
                            }
                            else
                            {
                                WriteToConsole("   Недійсна оцінка. Допустимо: 0–100.");
                            }
                        }
                    }
                }
            }
            else
            {
                // Повернення до меню
            }
        }

        /// <summary>
        /// Формує звіт і зберігає дані кафедри у текстовий файл.
        /// </summary>
        /// <param name="department">Кафедра, дані якої потрібно зберегти.</param>
        private void HandleSaveData(Department department)
        {
            WriteToConsole("\n[9] Збереження даних...");
            string fileContent = BuildFileContent(department);
            WriteToFile(fileContent);
            WriteToConsole($"   Дані збережено у файл «{filePath}».");
        }

        /// <summary>
        /// Зчитує дані з текстового файлу та виводить їх у консоль.
        /// </summary>
        private void HandleReadData()
        {
            WriteToConsole("\n[12] Читання даних з файлу...");
            string? content = ReadFromFile();
            if (content != null)
            {
                WriteToConsole($"\nВміст файлу «{filePath}»:\n");
                WriteToConsole(content);
            }
            else
            {
                WriteToConsole($"   Файл «{filePath}» не знайдено.");
            }
        }

        /// <summary>
        /// Обробляє зміну спеціальності обраного студента.
        /// </summary>
        /// <param name="department">Кафедра, до якої належить студент.</param>
        private void HandleChangeSpecialty(Department department)
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
                        WriteToConsole("   Зміну спеціальності скасовано.");
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
                // Повернення до меню
            }
        }

        /// <summary>
        /// Обробляє зміну навчального навантаження  обраного студента.
        /// </summary>
        /// <param name="department">Кафедра, до якої належить студент.</param>
        private void HandleChangeWorkload(Department department)
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
                    bool success = false;
                    while (!success)
                    {
                        int newWorkload = ReadInt($"  Новий обсяг кредитів ЄКТС для «{s.Name} (1-30, 0 — скасувати): ");
                        if (newWorkload == 0)
                        {
                            WriteToConsole("   Зміну навантаження скасовано.");
                            break;
                        }
                        else
                        {
                            if (s.ChangeWorkload(newWorkload))
                            {
                                WriteToConsole($"   Обсяг навантаження студента «{s.Name} змінено на {s.WorkloadLevel} кредитів.");
                                success = true;
                            }
                            else
                            {
                                WriteToConsole("   Некоректний обсяг кредитів. Допустимо: 1–30.");
                            }
                        }
                    }
                }
            }
            else
            {
                // Повернення до меню
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
            WriteToConsole("  12  — Читати дані з файлу           ");
            WriteToConsole("  13  — Робота з конкурсною роботою   ");
            WriteToConsole("  0   — Вихід                         ");
            Console.Write("Ваш вибір: ");
        }

        /// <summary>
        /// Обробляє підменю для конкурсної роботи обраного студента.
        /// </summary>
        /// <param name="department">Кафедра, до якої належить студент.</param>
        private void HandleCompetitiveWorkMenu(Department department)
        {
            int idx = ReadStudentIndex(department, "  Виберіть номер студента для роботи з конкурсною роботою (0 — назад до меню): ");
            if (idx >= 1)
            {
                Student? s = department.GetStudentByNumber(idx);
                if (s == null)
                {
                    WriteToConsole("   Невірний номер.");
                }
                else
                {
                    bool workMenuRunning = true;
                    while (workMenuRunning)
                    {
                        WriteToConsole($"\nКОНКУРСНА РОБОТА (Студент: {s.Name})");
                        WriteToConsole("  1 — Подати роботу");
                        if (s.SubmittedWork != null)
                        {
                            WriteToConsole("  2 — Визначити відповідність теми");
                            WriteToConsole("  3 — Оцінити роботу");
                            WriteToConsole("  4 — Змінити статус");
                            WriteToConsole("  5 — Присвоїти призове місце");
                            WriteToConsole("  6 — Змінити назву роботи");
                            WriteToConsole("  7 — Переглянути деталі");
                        }
                        else
                        {
                            // Робота ще не подана — показуємо лише опцію подати
                        }
                        WriteToConsole("  0 — Назад");
                        Console.Write("Ваш вибір: ");
                        string choice = ReadFromConsole();

                        switch (choice.Trim())
                        {
                            case "1":
                                WriteToConsole("  Введіть назву конкурсної роботи: ");
                                string title = ReadFromConsole();
                                WriteToConsole("  Введіть тематику конкурсу: ");
                                string theme = ReadFromConsole();
                                if (s.SubmitCompetitiveWork(title, theme))
                                {
                                    WriteToConsole("   Конкурсну роботу успішно подано.");
                                }
                                else
                                {
                                    WriteToConsole("   Помилка: Студент вже подав конкурсну роботу.");
                                }
                                break;
                            case "2":
                                if (s.SubmittedWork != null)
                                {
                                    List<string> matches = s.SubmittedWork.CheckThemeRelevance("themes.txt");
                                    if (matches.Count > 0)
                                    {
                                        WriteToConsole("Знайдені збіги тематики:");
                                        foreach (string match in matches)
                                        {
                                            WriteToConsole($"- {match}");
                                        }
                                    }
                                    else
                                    {
                                        WriteToConsole("   Збігів з темами конкурсу не знайдено.");
                                    }
                                }
                                else
                                {
                                    WriteToConsole("   Робота ще не подана.");
                                }
                                break;
                            case "3":
                                if (s.SubmittedWork != null)
                                {
                                    int score = ReadInt("  Введіть оцінку: ");
                                    s.SubmittedWork.EvaluateWork(score);
                                    WriteToConsole("   Оцінку встановлено.");
                                }
                                else
                                {
                                    WriteToConsole("   Робота ще не подана.");
                                }
                                break;
                            case "4":
                                if (s.SubmittedWork != null)
                                {
                                    WriteToConsole("  Оберіть статус (1 - Подано, 2 - Прийнято, 3 - Відхилено): ");
                                    string statChoice = ReadFromConsole();
                                    if (statChoice == "1")
                                    {
                                        s.SubmittedWork.AssignStatus(WorkStatus.Submitted);
                                    }
                                    else if (statChoice == "2")
                                    {
                                        s.SubmittedWork.AssignStatus(WorkStatus.Accepted);
                                    }
                                    else if (statChoice == "3")
                                    {
                                        s.SubmittedWork.AssignStatus(WorkStatus.Rejected);
                                    }
                                    else
                                    {
                                        WriteToConsole("  Невірний вибір.");
                                    }
                                }
                                else
                                {
                                    WriteToConsole("   Робота ще не подана.");
                                }
                                break;
                            case "5":
                                if (s.SubmittedWork != null)
                                {
                                    int place = ReadInt("  Введіть призове місце: ");
                                    s.SubmittedWork.AssignPrizePlace(place);
                                    WriteToConsole("   Призове місце встановлено.");
                                }
                                else
                                {
                                    WriteToConsole("   Робота ще не подана.");
                                }
                                break;
                            case "6":
                                if (s.SubmittedWork != null)
                                {
                                    WriteToConsole("  Введіть нову назву: ");
                                    string newTitle = ReadFromConsole();
                                    s.SubmittedWork.ChangeTitle(newTitle);
                                    WriteToConsole("   Назву змінено.");
                                }
                                else
                                {
                                    WriteToConsole("   Робота ще не подана.");
                                }
                                break;
                            case "7":
                                if (s.SubmittedWork != null)
                                {
                                    WriteToConsole(s.SubmittedWork.ViewDetails());
                                }
                                else
                                {
                                    WriteToConsole("   Робота ще не подана.");
                                }
                                break;
                            case "0":
                                workMenuRunning = false;
                                break;
                            default:
                                WriteToConsole("   Невідома опція.");
                                break;
                        }
                    }
                }
            }
            else
            {
                // Повернення до меню
            }
        }
    }
}
