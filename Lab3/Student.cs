//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    /// <summary>
    /// Клас "Студент" — зберігає дані про студента та реалізує бізнес-логіку.
    /// Використовує Service для введення/виведення даних.
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

        /// <summary>
        /// Форматує детальну інформацію про студента у вигляді рядка.
        /// </summary>
        /// <returns>Відформатований рядок з даними студента.</returns>
        public string ToFormattedString()
        {
            return $"  #{studentNumber} {name}  Спеціальність : {educationProgram} " +
                   $" Кредити: {workloadLevel} " +
                   $" Рейтинг: {rating:F2}  Оцінки: [{string.Join(", ", grades)}] " +
                   $" Причина: {(!string.IsNullOrEmpty(admissionReason) ? admissionReason : "не вказано")}";
        }

        /// <summary>
        /// Зчитує кількість кредитів ЄКТС (1-30) з консолі через Service.
        /// Повертає null при 0 (скасування).
        /// </summary>
        /// <param name="svc">Сервіс для вводу/виводу.</param>
        /// <returns>Кількість кредитів або null при скасуванні.</returns>
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
                        valid = true;
                    }
                    else if (credits >= 1 && credits <= maxCredits)
                    {
                        result = credits;
                        valid = true;
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
        /// Фабричний метод: зчитує дані нового студента з консолі через Service.
        /// </summary>
        /// <param name="svc">Сервіс для вводу/виводу.</param>
        /// <param name="studentNumber">Номер, що буде присвоєно студенту.</param>
        /// <param name="dept">Кафедра для вибору спеціальності.</param>
        /// <returns>Новий студент або null при скасуванні.</returns>
        public static Student? ReadFromConsole(Service svc, int studentNumber, Department dept)
        {
            Student? result;

            svc.WriteToConsole("  Ім'я студента (0 — назад): ");
            string name = svc.ReadFromConsole();

            if (name.Trim() == "0")
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
                        result = new Student(name, program, workload.Value, studentNumber);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Обробляє процес додавання оцінок через Service.
        /// </summary>
        /// <param name="svc">Сервіс для вводу/виводу.</param>
        public void HandleAddGrades(Service svc)
        {
            bool addingGrades = true;
            while (addingGrades)
            {
                int grade = svc.ReadInt($"  Оцінка для «{name} (0–100, -1 — завершити): ");
                if (grade == -1)
                {
                    addingGrades = false;
                    continue;
                }
                else
                {
                    // Продовжуємо перевірку оцінки
                }

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

        /// <summary>
        /// Обробляє зміну навантаження через Service.
        /// </summary>
        /// <param name="svc">Сервіс для вводу/виводу.</param>
        public void HandleChangeWorkload(Service svc)
        {
            bool success = false;
            while (!success)
            {
                int newWorkload = svc.ReadInt($"  Новий обсяг кредитів ЄКТС для «{name} (1-30, 0 — скасувати): ");
                if (newWorkload == 0)
                {
                    svc.WriteToConsole("   Зміну навантаження скасовано.");
                    break;
                }
                else
                {
                    if (ChangeWorkload(newWorkload))
                    {
                        svc.WriteToConsole($"   Обсяг навантаження студента «{name} змінено на {workloadLevel} кредитів.");
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