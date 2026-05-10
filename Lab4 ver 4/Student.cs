//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    /// <summary>
    /// Клас «Студент» — індивідуальний освітній суб'єкт.
    /// Успадковує від EducationalEntity спільні атрибути (ім'я, напрям підготовки)
    /// та реалізує власну бізнес-логіку (оцінки, рейтинг, навантаження).
    /// </summary>
    public class Student : EducationalEntity
    {
        private List<int> grades;
        private int workloadLevel;
        private double rating;
        private int studentNumber;
        private string admissionReason;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Student"/> зі значеннями за замовчуванням.
        /// </summary>
        public Student() : base()
        {
            grades          = new List<int>();
            workloadLevel   = 0;
            rating          = 0.0;
            studentNumber   = 0;
            admissionReason = "";
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Student"/> із заданими параметрами.
        /// </summary>
        /// <param name="name">Ім'я студента.</param>
        /// <param name="educationDirection">Напрям підготовки.</param>
        /// <param name="workloadLevel">Обсяг навчального навантаження (кредити).</param>
        /// <param name="studentNumber">Порядковий номер студента.</param>
        public Student(string name, string educationDirection,
                       int workloadLevel, int studentNumber = 0)
            : base(name, educationDirection)
        {
            this.workloadLevel   = workloadLevel;
            this.studentNumber   = studentNumber;
            this.grades          = new List<int>();
            this.rating          = 0.0;
            this.admissionReason = "";
        }

        // Властивості

        /// <summary>Отримує або встановлює список оцінок студента.</summary>
        public List<int> Grades
        {
            get { return new List<int>(grades); }
            set { grades = new List<int>(value); }
        }

        /// <summary>Отримує або встановлює обсяг навчального навантаження (кредити ЄКТС).</summary>
        public int WorkloadLevel
        {
            get { return workloadLevel; }
            set { workloadLevel = value; }
        }

        /// <summary>
        /// Отримує поточний рейтинг студента (середній бал).
        /// Змінюється лише через <see cref="CalculateRating"/>.
        /// </summary>
        public double Rating
        {
            get { return rating; }
        }

        /// <summary>Отримує або встановлює порядковий номер студента.</summary>
        public int StudentNumber
        {
            get { return studentNumber; }
            set { studentNumber = value; }
        }

        /// <summary>Отримує або встановлює причину вступу/додавання студента.</summary>
        public string AdmissionReason
        {
            get { return admissionReason; }
            set { admissionReason = value; }
        }

        // Відкриті методи

        /// <summary>
        /// Додати оцінку. Дійсна оцінка — від 0 до 100 балів.
        /// </summary>
        /// <param name="grade">Оцінка для додавання (0–100).</param>
        /// <returns>true — додано; false — оцінка невалідна.</returns>
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

        /// <summary>Повертає копію списку оцінок.</summary>
        public List<int> ViewGrades()
        {
            return new List<int>(grades);
        }

        /// <summary>Змінити напрям підготовки студента.</summary>
        /// <param name="newSpecialty">Новий напрям підготовки.</param>
        public void ChangeSpecialty(string newSpecialty)
        {
            if (!string.IsNullOrWhiteSpace(newSpecialty))
            {
                EducationDirection = newSpecialty;
            }
            else
            {
                // порожній рядок ігнорується, щоб не зіпсувати дані
            }
        }

        /// <summary>Змінити обсяг навчального навантаження.</summary>
        /// <param name="newWorkload">Нове значення кредитів (1–30).</param>
        /// <returns>true — успішно; false — значення поза межами.</returns>
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
                // навантаження має бути в межах 1–30
                result = false;
            }
            return result;
        }

        /// <summary>Обчислити і зберегти рейтинг успішності (середня оцінка).</summary>
        /// <returns>Розрахований рейтинг.</returns>
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

        /// <summary>Форматує детальну інформацію про студента у рядок.</summary>
        public string ToFormattedString()
        {
            return $"  #{studentNumber} {EntityName}  Спеціальність : {EducationDirection} " +
                   $" Кредити: {workloadLevel} " +
                   $" Рейтинг: {rating:F2}  Оцінки: [{string.Join(", ", grades)}] " +
                   $" Причина: {(!string.IsNullOrEmpty(admissionReason) ? admissionReason : "не вказано")}";
        }

        /// <summary>
        /// Зчитує кількість кредитів ЄКТС (1–30) з консолі.
        /// Повертає null при 0 (скасування).
        /// </summary>
        public static int? ReadCredits(Service svc)
        {
            const int maxCredits = 30;
            int? result = null;
            bool valid = false;

            while (!valid)
            {
                svc.WriteToConsole($"  Кількість кредитів ЄКТС (1–{maxCredits}, 0 — назад): ");
                if (int.TryParse(svc.ReadFromConsole(), out int credits))
                {
                    if (credits == 0)
                    {
                        result = null;
                        valid  = true;
                    }
                    else if (credits >= 1 && credits <= maxCredits)
                    {
                        result = credits;
                        valid  = true;
                    }
                    else
                    {
                        svc.WriteToConsole($"   Введіть число від 0 до {maxCredits}.");
                    }
                }
                else
                {
                    svc.WriteToConsole($"   Введіть число від 0 до {maxCredits}.");
                }
            }
            return result;
        }

        /// <summary>
        /// Фабричний метод: зчитує дані нового студента з консолі.
        /// Повертає null при скасуванні.
        /// </summary>
        public static Student? ReadFromConsole(Service svc, int studentNumber, Department dept)
        {
            Student? result;

            svc.WriteToConsole("  Ім'я студента (0 — назад): ");
            string inputName = svc.ReadFromConsole();

            if (inputName.Trim() == "0")
            {
                result = null;
            }
            else
            {
                string? program = dept.ReadProgramFromList(svc);
                if (program == null)
                {
                    result = null;
                }
                else
                {
                    int? workload = ReadCredits(svc);
                    if (workload == null)
                    {
                        result = null;
                    }
                    else
                    {
                        result = new Student(inputName, program, workload.Value, studentNumber);
                    }
                }
            }
            return result;
        }

        /// <summary>Обробляє процес додавання оцінок через Service.</summary>
        public void HandleAddGrades(Service svc)
        {
            bool addingGrades = true;
            while (addingGrades)
            {
                int grade = svc.ReadInt($"  Оцінка для «{EntityName}» (0–100, -1 — завершити): ");
                if (grade == -1)
                {
                    addingGrades = false;
                }
                else
                {
                    if (AddGrade(grade))
                    {
                        svc.WriteToConsole($"   Оцінку {grade} додано. Новий рейтинг: {rating:F2}");
                    }
                    else
                    {
                        svc.WriteToConsole("   Недійсна оцінка. Допустимо: 0–100.");
                    }
                }
            }
        }

        /// <summary>Обробляє зміну навантаження через Service.</summary>
        public void HandleChangeWorkload(Service svc)
        {
            bool success = false;
            while (!success)
            {
                int newWorkload = svc.ReadInt($"  Новий обсяг кредитів ЄКТС для «{EntityName}» (1–30, 0 — скасувати): ");
                if (newWorkload == 0)
                {
                    svc.WriteToConsole("   Зміну навантаження скасовано.");
                    success = true;
                }
                else
                {
                    if (ChangeWorkload(newWorkload))
                    {
                        svc.WriteToConsole($"   Обсяг навантаження студента «{EntityName}» змінено на {workloadLevel} кредитів.");
                        success = true;
                    }
                    else
                    {
                        svc.WriteToConsole("   Некоректний обсяг кредитів. Допустимо: 1–30.");
                    }
                }
            }
        }
    }
}