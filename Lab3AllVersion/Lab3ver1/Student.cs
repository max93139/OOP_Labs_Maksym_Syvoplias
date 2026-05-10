//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
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
        private string educationProgram; // Спеціальність  підготовки
        private List<int> grades;        // Список оцінок
        private int workloadLevel;       // Обсяг навчального навантаження
        private double rating;           // Рейтинг успішності
        private int studentNumber;       // Номер студента
        private string admissionReason;  // Причина додавання/вступу

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Student"/> зі значеннями за замовчуванням.
        /// </summary>
        public Student()
        {
            name             = "";
            educationProgram = "";
            grades           = new List<int>();
            workloadLevel    = 0;
            rating           = 0.0;
            studentNumber    = 0;
            admissionReason  = "";
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Student"/> із заданими параметрами.
        /// </summary>
        /// <param name="name">Ім'я студента.</param>
        /// <param name="educationProgram">Спеціальність підготовки.</param>
        /// <param name="workloadLevel">Обсяг навчального навантаження (кредити).</param>
        /// <param name="studentNumber">Порядковий номер студента в списку (за замовчуванням 0).</param>
        public Student(string name, string educationProgram,
                       int workloadLevel, int studentNumber = 0)
        {
            this.name             = name;
            this.educationProgram = educationProgram;
            this.workloadLevel    = workloadLevel;
            this.studentNumber    = studentNumber;
            this.grades           = new List<int>();
            this.rating           = 0.0;
            this.admissionReason  = "";
        }

        /// <summary>
        /// Отримує або встановлює ім'я студента.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Отримує або встановлює Спеціальність  підготовки студента.
        /// </summary>
        public string EducationProgram
        {
            get
            {
                return educationProgram;
            }
            set
            {
                educationProgram = value;
            }
        }

        /// <summary>
        /// Отримує або встановлює список оцінок студента.
        /// </summary>
        public List<int> Grades
        {
            get
            {
                return new List<int>(grades);
            }
            set
            {
                grades = new List<int>(value);
            }
        }

        /// <summary>
        /// Отримує або встановлює Обсяг навчального навантаження (у кредитах ЄКТС).
        /// </summary>
        public int WorkloadLevel
        {
            get
            {
                return workloadLevel;
            }
            set
            {
                workloadLevel = value;
            }
        }


        /// <summary>
        /// Отримує поточний рейтинг студента (середній бал).
        /// Зовні можна прочитати, але змінювати — лише через відповідний метод.
        /// </summary>
        public double Rating
        {
            get
            {
                return rating;
            }
        }

        /// <summary>
        /// Отримує або встановлює порядковий номер студента в системі/списку.
        /// </summary>
        public int StudentNumber
        {
            get
            {
                return studentNumber;
            }
            set
            {
                studentNumber = value;
            }
        }

        /// <summary>
        /// Отримує або встановлює причину вступу/додавання студента.
        /// </summary>
        public string AdmissionReason
        {
            get
            {
                return admissionReason;
            }
            set
            {
                admissionReason = value;
            }
        }

        // Відкриті методи

        /// <summary>
        /// Додати оцінку. Оцінка дійсна, якщо вона не перевищує 100 балів.
        /// </summary>
        /// <param name="grade">Оцінка для додавання (0-100).</param>
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
        /// <returns>Список оцінок студента.</returns>
        public List<int> ViewGrades()
        {
            return new List<int>(grades);
        }

        /// <summary>Змінити спеціальність (Спеціальність  підготовки).</summary>
        /// <param name="newSpecialty">Новий Спеціальність  підготовки.</param>
        public void ChangeSpecialty(string newSpecialty)
        {
            if (!string.IsNullOrWhiteSpace(newSpecialty))
            {
                educationProgram = newSpecialty;
            }
            else
            {
                // Порожній рядок ігнорується, щоб не зіпсувати дані
            }
        }

        /// <summary>Змінити Обсяг навчального навантаження.</summary>
        /// <param name="newWorkload">Нове значення кредитів.</param>
        /// <returns>true — успішно змінено; false — значення поза межами 1-30.</returns>
        public bool ChangeWorkload(int newWorkload)
        {
            bool result;
            if (newWorkload >= 1 && newWorkload <= 30)
            {
                workloadLevel = newWorkload;
                result = true;
            }
            else
            {
                // Навантаження має бути в межах 1-30
                result = false;
            }
            return result;
        }

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