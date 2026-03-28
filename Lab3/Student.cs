using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    /// <summary>
    /// Клас "Студент" — зберігає дані про студента та реалізує бізнес-логіку.
    /// </summary>
    public class Student
    {
        // Закриті поля
        private string name;             // Ім'я студента
        private string educationProgram; // Напрям підготовки
        private List<int> grades;        // Список оцінок
        private int workloadLevel;       // Рівень навчального навантаження
        private string recordBookNumber; // Номер залікової книжки
        private double rating;           // Рейтинг успішності
        private int studentNumber;       // Номер студента

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Student"/> зі значеннями за замовчуванням.
        /// </summary>
        public Student()
        {
            name             = "";
            educationProgram = "";
            grades           = new List<int>();
            workloadLevel    = 0;
            recordBookNumber = "";
            rating           = 0.0;
            studentNumber    = 0;
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Student"/> із заданими параметрами.
        /// </summary>
        /// <param name="name">Ім'я студента.</param>
        /// <param name="educationProgram">Напрям підготовки.</param>
        /// <param name="workloadLevel">Рівень навчального навантаження (кредити).</param>
        /// <param name="recordBookNumber">Номер залікової книжки.</param>
        /// <param name="studentNumber">Порядковий номер студента в списку (за замовчуванням 0).</param>
        public Student(string name, string educationProgram,
                       int workloadLevel, string recordBookNumber, int studentNumber = 0)
        {
            this.name             = name;
            this.educationProgram = educationProgram;
            this.workloadLevel    = workloadLevel;
            this.recordBookNumber = recordBookNumber;
            this.studentNumber    = studentNumber;
            this.grades           = new List<int>();
            this.rating           = 0.0;
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Student"/> копіюючи дані іншого студента.
        /// </summary>
        /// <param name="other">Об'єкт студента для копіювання.</param>
        public Student(Student other)
        {
            this.name             = other.name;
            this.educationProgram = other.educationProgram;
            this.grades           = new List<int>(other.grades);
            this.workloadLevel    = other.workloadLevel;
            this.recordBookNumber = other.recordBookNumber;
            this.rating           = other.rating;
            this.studentNumber    = other.studentNumber;
        }

        /// <summary>
        /// Отримує або встановлює ім'я студента.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Отримує або встановлює напрям підготовки студента.
        /// </summary>
        public string EducationProgram
        {
            get => educationProgram;
            set => educationProgram = value;
        }

        /// <summary>
        /// Отримує або встановлює список оцінок студента.
        /// </summary>
        public List<int> Grades
        {
            get => new List<int>(grades);
            set => grades = new List<int>(value);
        }

        /// <summary>
        /// Отримує або встановлює рівень навчального навантаження (у кредитах ЄКТС).
        /// </summary>
        public int WorkloadLevel
        {
            get => workloadLevel;
            set => workloadLevel = value;
        }

        /// <summary>
        /// Отримує або встановлює номер залікової книжки.
        /// </summary>
        public string RecordBookNumber
        {
            get => recordBookNumber;
            set => recordBookNumber = value;
        }

        /// <summary>
        /// Отримує поточний рейтинг студента (середній бал).
        /// Зовні можна прочитати, але змінювати — лише через відповідний метод.
        /// </summary>
        public double Rating
        {
            get => rating;
            // Зовні можна прочитати, але змінювати — лише через CalculateRating()
        }

        /// <summary>
        /// Отримує або встановлює порядковий номер студента в системі/списку.
        /// </summary>
        public int StudentNumber
        {
            get => studentNumber;
            set => studentNumber = value;
        }

        // Відкриті методи

        /// <summary>
        /// Додати оцінку. Оцінка дійсна, якщо вона не перевищує 100 балів.
        /// </summary>
        /// <returns>true — додано; false — оцінка невалідна (> 100).</returns>
        public bool AddGrade(int grade)
        {
            bool result;

            if (grade < 0 || grade > 100)
            {
                result = false;
            }
            else
            {
                grades.Add(grade);
                CalculateRating();
                result = true;
            }

            return result;
        }

        /// <summary>Повертає копію списку оцінок (без можливості зовнішнього редагування).</summary>
        public List<int> ViewGrades() => new List<int>(grades);
        /// <summary>Обчислити і зберегти рейтинг успішності.</summary>
        /// <returns>Розрахований рейтинг (середня оцінка).</returns>
        public double CalculateRating()
        {
            if (grades.Count == 0)
            {
                rating = 0.0;
            }
            else
            {
                rating = grades.Average();
            }

            return rating;
        }


    }
}