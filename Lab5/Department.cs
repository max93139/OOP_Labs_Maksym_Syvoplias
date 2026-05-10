//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    /// <summary>
    /// Клас «Кафедра» — організаційний освітній суб'єкт.
    /// Успадковує від EducationalEntity спільні атрибути (ім'я, напрям підготовки)
    /// та реалізує власну бізнес-логіку управління студентами і дисциплінами.
    /// </summary>
    public class Department : EducationalEntity
    {
        private int studentCount;
        private List<string> disciplines;
        private int maxStudents;
        private List<Student> students;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Department"/> зі значеннями за замовчуванням.
        /// </summary>
        public Department() : base()
        {
            studentCount = 0;
            disciplines  = new List<string>();
            maxStudents  = 0;
            students     = new List<Student>();
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Department"/> із заданими параметрами.
        /// </summary>
        /// <param name="name">Назва кафедри.</param>
        /// <param name="studentCount">Поточна кількість студентів.</param>
        /// <param name="educationDirection">Напрям підготовки кафедри.</param>
        /// <param name="disciplines">Список дисциплін кафедри.</param>
        /// <param name="maxStudents">Максимальна кількість студентів.</param>
        public Department(string name, int studentCount, string educationDirection,
                          List<string> disciplines, int maxStudents)
            : base(name, educationDirection)
        {
            this.studentCount = studentCount;
            this.disciplines  = new List<string>(disciplines);
            this.maxStudents  = maxStudents;
            this.students     = new List<Student>();
        }

        // Властивості

        /// <summary>Отримує або встановлює кількість студентів.</summary>
        public int StudentCount
        {
            get { return studentCount; }
            set { studentCount = value; }
        }

        /// <summary>Отримує або встановлює список дисциплін.</summary>
        public List<string> Disciplines
        {
            get { return new List<string>(disciplines); }
            set { disciplines = new List<string>(value); }
        }

        /// <summary>Отримує або встановлює максимальну кількість студентів.</summary>
        public int MaxStudents
        {
            get { return maxStudents; }
            set { maxStudents = value; }
        }

        /// <summary>Отримує список студентів (тільки читання).</summary>
        public IReadOnlyList<Student> Students
        {
            get { return students.AsReadOnly(); }
        }

        // Бізнес-логіка

        /// <summary>Додати студента до кафедри.</summary>
        public bool AddStudent(Student student)
        {
            bool result;
            if (studentCount >= maxStudents || !disciplines.Contains(student.EducationDirection))
            {
                result = false;
            }
            else
            {
                students.Add(student);
                studentCount++;
                result = true;
            }
            return result;
        }

        /// <summary>Видалити студента за порядковим номером (1-based).</summary>
        public Student? RemoveStudentByNumber(int number)
        {
            Student? result;
            int index = number - 1;
            if (index < 0 || index >= students.Count)
            {
                result = null;
            }
            else
            {
                Student removed = students[index];
                students.RemoveAt(index);
                studentCount = Math.Max(0, studentCount - 1);
                result = removed;
            }
            return result;
        }

        /// <summary>Отримати студента за порядковим номером (1-based).</summary>
        public Student? GetStudentByNumber(int number)
        {
            Student? result;
            int index = number - 1;
            if (index < 0 || index >= students.Count)
            {
                result = null;
            }
            else
            {
                result = students[index];
            }
            return result;
        }

        /// <summary>Додати дисципліну (якщо ще не існує).</summary>
        public void AddDiscipline(string discipline)
        {
            if (!string.IsNullOrWhiteSpace(discipline) && !disciplines.Contains(discipline))
            {
                disciplines.Add(discipline);
            }
            else
            {
                // дисципліна порожня або вже існує
            }
        }

        /// <summary>Видалити дисципліну.</summary>
        public bool RemoveDiscipline(string discipline)
        {
            return disciplines.Remove(discipline);
        }

        // Форматування

        /// <summary>Форматує інформацію про кафедру у рядок.</summary>
        public string ToFormattedString()
        {
            string disciplinesList;
            if (disciplines.Count > 0)
            {
                disciplinesList = string.Join(", ", disciplines);
            }
            else
            {
                disciplinesList = "—";
            }
            return $"Кафедра: {EntityName}\n" +
                   $"Спеціальність : {EducationDirection}\n" +
                   $"Студентів: {studentCount}/{maxStudents}\n" +
                   $"Дисципліни: {disciplinesList}";
        }

        /// <summary>Будує повний текстовий звіт для файлу.</summary>
        public string BuildFileContent()
        {
            var sb = new StringBuilder();
            sb.AppendLine(ToFormattedString());
            sb.AppendLine("Студенти:");
            if (students.Count == 0)
            {
                sb.AppendLine("  (немає)");
            }
            else
            {
                foreach (var s in students)
                {
                    sb.AppendLine(s.ToFormattedString());
                }
            }
            return sb.ToString();
        }

        // Методи управління

        /// <summary>Виводить пронумерований список студентів.</summary>
        public void PrintStudentList(Service svc)
        {
            svc.WriteToConsole("\n Список студентів ");
            for (int i = 0; i < students.Count; i++)
            {
                svc.WriteToConsole($"  {i + 1}. {students[i].EntityName}");
            }
        }

        /// <summary>Зчитує індекс студента зі списку.</summary>
        public int ReadStudentIndex(Service svc, string prompt)
        {
            int result;
            if (students.Count == 0)
            {
                svc.WriteToConsole("  Список студентів порожній.");
                result = -1;
            }
            else
            {
                PrintStudentList(svc);
                result = svc.ReadInt(prompt);
            }
            return result;
        }

        /// <summary>Показує список напрямів та повертає вибраний.</summary>
        public string? ReadProgramFromList(Service svc)
        {
            string? result;
            if (disciplines.Count == 0)
            {
                svc.WriteToConsole("   На кафедрі ще немає жодного Спеціальність у підготовки.");
                svc.WriteToConsole("      Спочатку додайте дисципліни (пункт 1.2 або 4).");
                result = null;
            }
            else
            {
                bool chosen = false;
                result = null;
                while (!chosen)
                {
                    svc.WriteToConsole("  Оберіть Спеціальність  підготовки (0 — назад до меню):");
                    for (int i = 0; i < disciplines.Count; i++)
                    {
                        svc.WriteToConsole($"    {i + 1}. {disciplines[i]}");
                    }
                    svc.WriteToConsole("  Ваш вибір (номер): ");
                    if (int.TryParse(svc.ReadFromConsole(), out int choice))
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
                            svc.WriteToConsole($"   Введіть число від 0 до {disciplines.Count}.");
                        }
                    }
                    else
                    {
                        svc.WriteToConsole($"   Введіть число від 0 до {disciplines.Count}.");
                    }
                }
            }
            return result;
        }

        /// <summary>Заповнює дані кафедри через консоль.</summary>
        public void ReadFromConsole(Service svc)
        {
            svc.WriteToConsole("  Назва кафедри: ");
            EntityName = svc.ReadFromConsole();
            svc.WriteToConsole("  Спеціальність  підготовки: ");
            EducationDirection = svc.ReadFromConsole();
            maxStudents = svc.ReadInt("  Максимальна кількість студентів: ");
        }

        /// <summary>Додає дисципліни під час створення кафедри.</summary>
        public void AddDisciplinesDuringCreation(Service svc)
        {
            svc.WriteToConsole("\n  Додавання дисциплін. Введіть 0 для завершення");
            string disciplineName;
            do
            {
                svc.WriteToConsole("  Введіть назву дисципліни (0 — завершити): ");
                disciplineName = svc.ReadFromConsole();
                if (disciplineName != "0")
                {
                    if (!string.IsNullOrWhiteSpace(disciplineName))
                    {
                        AddDiscipline(disciplineName);
                    }
                    else
                    {
                        svc.WriteToConsole("   Назва не може бути порожньою.");
                    }
                }
                else
                {
                    // завершуємо
                }
            } while (disciplineName != "0");
            svc.WriteToConsole($"   Дисципліни: {(disciplines.Count > 0 ? string.Join(", ", disciplines) : "немає")}");
        }

        // Handle-методи (оркестрація через Service)

        /// <summary>Ініціалізація або оновлення даних кафедри.</summary>
        public void HandleCreate(Service svc)
        {
            ReadFromConsole(svc);
            svc.WriteToConsole($"   Дані кафедри «{EntityName}» успішно оновлено.");
            AddDisciplinesDuringCreation(svc);
        }

        /// <summary>Додавання студента. Повертає оновлений лічильник.</summary>
        public int HandleAddStudent(Service svc, int nextStudentNumber)
        {
            Student? newStudent = Student.ReadFromConsole(svc, nextStudentNumber, this);
            if (newStudent == null)
            {
                svc.WriteToConsole("   Додавання студента скасовано.");
            }
            else
            {
                if (AddStudent(newStudent))
                {
                    svc.WriteToConsole("   Вкажіть причину додавання студента (вступ, поновлення, переведення тощо): ");
                    string reason = svc.ReadFromConsole();
                    newStudent.AdmissionReason = reason;
                    nextStudentNumber++;
                    svc.WriteToConsole($"   Студента «{newStudent.EntityName}» додано (Причина: {reason}). Всього: {studentCount}/{maxStudents}");
                }
                else
                {
                    svc.WriteToConsole("   Досягнуто максимальну кількість студентів або невірний напрям!");
                }
            }
            return nextStudentNumber;
        }

        /// <summary>Видалення студента.</summary>
        public void HandleRemoveStudent(Service svc)
        {
            int idx = ReadStudentIndex(svc, "  Виберіть номер студента для видалення (0 — назад до меню): ");
            if (idx >= 1)
            {
                svc.WriteToConsole("   Вкажіть причину видалення студента (переїхав, перевівся, відрахування тощо): ");
                string reason = svc.ReadFromConsole();
                Student? removed = RemoveStudentByNumber(idx);
                if (removed != null)
                {
                    svc.WriteToConsole($"   Студента «{removed.EntityName}» видалено (Причина: {reason}). Залишилось: {studentCount}");
                }
                else
                {
                    svc.WriteToConsole("   Невірний номер.");
                }
            }
            else
            {
                // повернення до меню
            }
        }

        /// <summary>Додавання дисципліни.</summary>
        public void HandleAddDiscipline(Service svc)
        {
            svc.WriteToConsole("\n[4] Назва нової дисципліни: ");
            AddDiscipline(svc.ReadFromConsole());
            svc.WriteToConsole($"   Дисципліни: {string.Join(", ", disciplines)}");
        }

        /// <summary>Видалення дисципліни.</summary>
        public void HandleRemoveDiscipline(Service svc)
        {
            if (disciplines.Count == 0)
            {
                svc.WriteToConsole("  Дисциплін немає.");
            }
            else
            {
                svc.WriteToConsole("\n[5] Дисципліни:");
                for (int i = 0; i < disciplines.Count; i++)
                {
                    svc.WriteToConsole($"  {i + 1}. {disciplines[i]}");
                }
                svc.WriteToConsole("  Введіть назву дисципліни для видалення: ");
                string discName = svc.ReadFromConsole();
                if (RemoveDiscipline(discName))
                {
                    svc.WriteToConsole($"   «{discName}» видалено.");
                }
                else
                {
                    svc.WriteToConsole($"   Дисципліну «{discName}» не знайдено.");
                }
            }
        }

        /// <summary>Розрахунок рейтингу студента.</summary>
        public void HandleCalculateRating(Service svc)
        {
            int idx = ReadStudentIndex(svc, "  Виберіть номер студента для розрахунку рейтингу (0 — назад до меню): ");
            if (idx >= 1)
            {
                Student? s = GetStudentByNumber(idx);
                if (s == null)
                {
                    svc.WriteToConsole("   Невірний номер.");
                }
                else
                {
                    double r = s.CalculateRating();
                    svc.WriteToConsole($"  Рейтинг «{s.EntityName}»: {r:F2} балів");
                }
            }
            else
            {
                // повернення до меню
            }
        }

        /// <summary>Перегляд оцінок студента.</summary>
        public void HandleViewGrades(Service svc)
        {
            int idx = ReadStudentIndex(svc, "  Виберіть номер студента для перегляду оцінок (0 — назад до меню): ");
            if (idx >= 1)
            {
                Student? s = GetStudentByNumber(idx);
                if (s == null)
                {
                    svc.WriteToConsole("   Невірний номер.");
                }
                else
                {
                    var gs = s.ViewGrades();
                    if (gs.Count == 0)
                    {
                        svc.WriteToConsole($"  У «{s.EntityName}» оцінок немає.");
                    }
                    else
                    {
                        svc.WriteToConsole($"  Оцінки «{s.EntityName}»: [{string.Join(", ", gs)}]");
                    }
                }
            }
            else
            {
                // повернення до меню
            }
        }

        /// <summary>Додавання оцінки студенту.</summary>
        public void HandleAddGrade(Service svc)
        {
            int idx = ReadStudentIndex(svc, "  Виберіть номер студента для додавання оцінки (0 — назад до меню): ");
            if (idx < 1)
            {
                return;
            }
            else
            {
                // продовжуємо
            }

            Student? s = GetStudentByNumber(idx);
            if (s == null)
            {
                svc.WriteToConsole("   Невірний номер.");
                return;
            }
            else
            {
                // студента знайдено
            }

            s.HandleAddGrades(svc);
        }

        /// <summary>Збереження даних у файл.</summary>
        public void HandleSaveData(Service svc)
        {
            svc.WriteToConsole("\n[9] Збереження даних...");
            string fileContent = BuildFileContent();
            svc.WriteToFile(fileContent);
            svc.WriteToConsole($"   Дані збережено у файл «{svc.FilePath}».");
        }

        /// <summary>Читання даних з файлу.</summary>
        public static void HandleReadData(Service svc)
        {
            svc.WriteToConsole("\n[12] Читання даних з файлу...");
            string? content = svc.ReadFromFile();
            if (content != null)
            {
                svc.WriteToConsole($"\nВміст файлу «{svc.FilePath}»:\n");
                svc.WriteToConsole(content);
            }
            else
            {
                svc.WriteToConsole($"   Файл «{svc.FilePath}» не знайдено.");
            }
        }

        /// <summary>Зміна напряму підготовки студента.</summary>
        public void HandleChangeSpecialty(Service svc)
        {
            int idx = ReadStudentIndex(svc, "  Виберіть номер студента для зміни спеціальності (0 — назад до меню): ");
            if (idx >= 1)
            {
                Student? s = GetStudentByNumber(idx);
                if (s == null)
                {
                    svc.WriteToConsole("   Невірний номер.");
                }
                else
                {
                    string? newProgram = ReadProgramFromList(svc);
                    if (newProgram == null)
                    {
                        svc.WriteToConsole("   Зміну спеціальності скасовано.");
                    }
                    else
                    {
                        s.ChangeSpecialty(newProgram);
                        svc.WriteToConsole($"   Спеціальність студента «{s.EntityName}» змінено на «{newProgram}».");
                    }
                }
            }
            else
            {
                // повернення до меню
            }
        }

        /// <summary>Зміна навантаження студента.</summary>
        public void HandleChangeWorkload(Service svc)
        {
            int idx = ReadStudentIndex(svc, "  Виберіть номер студента для зміни навантаження (0 — назад до меню): ");
            if (idx >= 1)
            {
                Student? s = GetStudentByNumber(idx);
                if (s == null)
                {
                    svc.WriteToConsole("   Невірний номер.");
                }
                else
                {
                    s.HandleChangeWorkload(svc);
                }
            }
            else
            {
                // повернення до меню
            }
        }

        /// <summary>Виводить повідомлення про вихід.</summary>
        public static void HandleExit(Service svc)
        {
            svc.WriteToConsole("\nДо побачення!");
            Console.Clear();
        }

        /// <summary>Виводить повідомлення про невідому опцію.</summary>
        public static void HandleUnknownOption(Service svc)
        {
            svc.WriteToConsole("   Невідома опція. Спробуйте ще раз.");
        }

        // Перевизначені віртуальні методи
        public override string GetName()
        {
            return base.GetName();
        }

        public override void SetName(string name)
        {
            base.SetName(name);
        }

        public override string GetEducationDirection()
        {
            return base.GetEducationDirection();
        }

        public override void SetEducationDirection(string direction)
        {
            base.SetEducationDirection(direction);
        }
    }
}
