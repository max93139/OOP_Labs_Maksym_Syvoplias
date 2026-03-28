using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    /// <summary>
    /// Клас "Кафедра" — зберігає дані про кафедру та реалізує бізнес-логіку.
    /// </summary>
    public class Department
    {
        // Закриті поля (атрибути предметної області)
        private string name;                   // Назва кафедри
        private int studentCount;              // Кількість студентів
        private string educationProgram;       // Напрям підготовки
        private List<string> disciplines;      // Перелік дисциплін
        private int maxStudents;               // Максимальна кількість студентів
        private char accreditationLevel;       // Рівень акредитації (A, B, E, F)
        private List<Student> students;        // Список студентів кафедри

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Department"/> зі значеннями за замовчуванням.
        /// </summary>
        public Department()
        {
            name             = "";
            studentCount     = 0;
            educationProgram = "";
            disciplines      = new List<string>();
            maxStudents      = 0;
            accreditationLevel = '\0';
            students         = new List<Student>();
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Department"/> із заданими параметрами.
        /// </summary>
        /// <param name="name">Назва кафедри.</param>
        /// <param name="studentCount">Початкова кількість студентів.</param>
        /// <param name="educationProgram">Напрям підготовки.</param>
        /// <param name="disciplines">Список дисциплін.</param>
        /// <param name="maxStudents">Максимальна допустима кількість студентів.</param>
        public Department(string name, int studentCount, string educationProgram,
                          List<string> disciplines, int maxStudents)
        {
            this.name             = name;
            this.studentCount     = studentCount;
            this.educationProgram = educationProgram;
            this.disciplines      = new List<string>(disciplines);
            this.maxStudents      = maxStudents;
            this.accreditationLevel = '\0';
            this.students         = new List<Student>();
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Department"/> копіюючи дані іншої кафедри.
        /// </summary>
        /// <param name="other">Об'єкт кафедри для глибокого копіювання.</param>
        public Department(Department other)
        {
            this.name              = other.name;
            this.studentCount      = other.studentCount;
            this.educationProgram  = other.educationProgram;
            this.disciplines       = new List<string>(other.disciplines);
            this.maxStudents       = other.maxStudents;
            this.accreditationLevel = other.accreditationLevel;
            // Deep copy студентів
            this.students = other.students.Select(s => new Student(s)).ToList();
        }

        /// <summary>
        /// Отримує або встановлює назву кафедри.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Отримує або встановлює поточну кількість студентів.
        /// </summary>
        public int StudentCount
        {
            get => studentCount;
            set => studentCount = value;
        }

        /// <summary>
        /// Отримує або встановлює основний напрям підготовки кафедри.
        /// </summary>
        public string EducationProgram
        {
            get => educationProgram;
            set => educationProgram = value;
        }

        /// <summary>
        /// Отримує або встановлює перелік дисциплін.
        /// </summary>
        public List<string> Disciplines
        {
            get => new List<string>(disciplines);
            set => disciplines = new List<string>(value);
        }

        /// <summary>
        /// Отримує або встановлює максимально допустиму кількість студентів.
        /// </summary>
        public int MaxStudents
        {
            get => maxStudents;
            set => maxStudents = value;
        }

        /// <summary>
        /// Отримує або встановлює рівень акредитації кафедри (A, B, E, F).
        /// </summary>
        public char AccreditationLevel
        {
            get => accreditationLevel;
            set => accreditationLevel = value;
        }

        /// <summary>
        /// Отримує список студентів (тільки для читання). Захищений від зовнішніх змін.
        /// </summary>
        public IReadOnlyList<Student> Students => students.AsReadOnly();

        //  Відкриті методи

        /// <summary>Додати студента до кафедри.</summary>
        /// <returns>true — успішно; false — досягнуто ліміт.</returns>
        public bool AddStudent(Student student)
        {
            bool result;

            if (studentCount >= maxStudents)
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

        /// <summary>Видалити студента за його порядковим номером у списку (1-based).</summary>
        /// <returns>Видалений студент або null.</returns>
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
                // дисципліна порожня або вже існує — нічого не робимо
            }
        }

        /// <summary>Видалити дисципліну.</summary>
        /// <returns>true — знайдена і видалена.</returns>
        public bool RemoveDiscipline(string discipline) =>
            disciplines.Remove(discipline);

        /// <summary>
        /// Алгоритм зміни кількості студентів за результатами акредитації.
        /// Генерує оцінку А/B/E/F та змінює studentCount відповідно до умов задачі.
        /// </summary>
        /// <returns>Результат акредитації (A, B, E або F).</returns>
        public char RunAccreditation()
        {
            // Генерація оцінки акредитації
            char[] grades = { 'A', 'B', 'E', 'F' };
            Random rnd = new Random();
            accreditationLevel = grades[rnd.Next(grades.Length)];

            ApplyAccreditationResult(accreditationLevel);
            return accreditationLevel;
        }

        /// <summary>
        /// Застосувати алгоритм зміни кількості студентів за результатом акредитації.
        /// </summary>
        /// <param name="grade">Оцінка акредитації (A, B, E, F).</param>
        private void ApplyAccreditationResult(char grade)
        {
            switch (grade)
            {
                case 'A':
                    // Збільшення на 20%, але не більше maxStudents
                    studentCount = Math.Min(maxStudents, (int)(studentCount * 1.20));
                    break;
                case 'B':
                    // Кількість студентів не змінюється
                    break;
                case 'E':
                    // Зменшення на 10%
                    studentCount = (int)(studentCount * 0.90);
                    break;
                case 'F':
                    // Зменшення на 50%
                    studentCount = (int)(studentCount * 0.50);
                    break;
            }

            // Синхронізуємо фізичний список студентів з оновленим лічильником
            while (students.Count > studentCount)
                students.RemoveAt(students.Count - 1);
        }


    }
}
