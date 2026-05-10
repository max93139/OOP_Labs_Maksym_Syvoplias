//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lab3
{
    /// <summary>
    /// Статус конкурсної роботи
    /// </summary>
    public enum WorkStatus
    {
        Submitted,
        Accepted,
        Rejected
    }

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
        private CompetitiveWork? submittedWork; // Подана конкурсна робота (модель 1:1)

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

        /// <summary>
        /// Отримує подану конкурсну роботу (якщо є).
        /// </summary>
        public CompetitiveWork? SubmittedWork
        {
            get
            {
                return submittedWork;
            }
        }

        // Відкриті методи

        /// <summary>
        /// Подати конкурсну роботу (реалізує зв'язок 1:1)
        /// </summary>
        public bool SubmitCompetitiveWork(string title, string theme)
        {
            bool result;

            if (this.submittedWork != null)
            {
                // Студент вже подав роботу (модель 1:1)
                result = false;
            }
            else
            {
                this.submittedWork = new CompetitiveWork(title, theme, this);
                result = true;
            }

            return result;
        }

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
        /// Вкладений клас КонкурснаРобота — творчий результат студента, який оцінюється в межах конкурсу.
        /// </summary>
        public class CompetitiveWork
        {
            /// <summary>Назва конкурсної роботи.</summary>
            public string Title { get; private set; }

            /// <summary>Тематика (назва) конкурсу.</summary>
            public string Theme { get; private set; }

            /// <summary>Дата подання роботи.</summary>
            public DateTime SubmissionDate { get; private set; }

            /// <summary>Оцінка / рейтинг роботи (null, якщо ще не оцінено).</summary>
            public int? Score { get; private set; }

            /// <summary>Статус роботи (подано, прийнято, відхилено).</summary>
            public WorkStatus Status { get; private set; }

            /// <summary>Призове місце (null, якщо не присвоєно).</summary>
            public int? PrizePlace { get; private set; }

            /// <summary>Автор роботи (посилання на студента).</summary>
            public Student Author { get; private set; }

            /// <summary>
            /// Ініціалізує нову конкурсну роботу зі статусом "Подано" та поточною датою.
            /// </summary>
            /// <param name="title">Назва роботи.</param>
            /// <param name="theme">Тематика конкурсу.</param>
            /// <param name="author">Автор (студент).</param>
            public CompetitiveWork(string title, string theme, Student author)
            {
                Title = title;
                Theme = theme;
                SubmissionDate = DateTime.Now;
                Status = WorkStatus.Submitted;
                Author = author;
                Score = null;
                PrizePlace = null;
            }

            /// <summary>Перевірити відповідність назви роботи тематиці конкурсу.</summary>
            /// <param name="themesFilePath">Шлях до файлу з тематиками.</param>
            /// <returns>Список рядків-тем, у яких знайдено збіги ключових слів з назви роботи.</returns>
            public List<string> CheckThemeRelevance(string themesFilePath)
            {
                List<string> matchedThemes = new List<string>();

                if (!File.Exists(themesFilePath))
                {
                    // Файл не знайдено — повертаємо порожній список
                }
                else
                {
                    string[] fileThemes = File.ReadAllLines(themesFilePath);
                    string[] titleWords = Title.Split(new[] { ' ', ',', '.', '!', '?', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in fileThemes)
                    {
                        bool lineMatched = false;
                        foreach (string word in titleWords)
                        {
                            if (line.Contains(word, StringComparison.OrdinalIgnoreCase))
                            {
                                matchedThemes.Add(line);
                                lineMatched = true;
                                break; // Збіг у цьому рядку вже знайдено
                            }
                            else
                            {
                                // Слово не збігається — продовжуємо пошук
                            }
                        }

                        if (!lineMatched)
                        {
                            // Жодного збігу у цьому рядку
                        }
                        else
                        {
                            // Рядок вже доданий
                        }
                    }
                }

                return matchedThemes;
            }

            /// <summary>Призначити оцінку конкурсній роботі.</summary>
            /// <param name="score">Оцінка.</param>
            public void EvaluateWork(int score)
            {
                Score = score;
            }

            /// <summary>Змінити статус конкурсної роботи.</summary>
            /// <param name="status">Новий статус.</param>
            public void AssignStatus(WorkStatus status)
            {
                Status = status;
            }

            /// <summary>Присвоїти призове місце конкурсній роботі.</summary>
            /// <param name="place">Номер призового місця.</param>
            public void AssignPrizePlace(int place)
            {
                PrizePlace = place;
            }

            /// <summary>Повертає форматований рядок з деталями конкурсної роботи.</summary>
            /// <returns>Рядок із повною інформацією про роботу.</returns>
            public string ViewDetails()
            {
                string scoreText = "";
                if (Score.HasValue)
                {
                    scoreText = Score.Value.ToString();
                }
                else
                {
                    scoreText = "Не оцінено";
                }

                string placeText = "";
                if (PrizePlace.HasValue)
                {
                    placeText = PrizePlace.Value.ToString();
                }
                else
                {
                    placeText = "Немає";
                }

                return $"Назва: {Title}\n" +
                       $"Тема конкурсу: {Theme}\n" +
                       $"Дата подання: {SubmissionDate.ToString("dd.MM.yyyy")}\n" +
                       $"Оцінка: {scoreText}\n" +
                       $"Статус: {Status}\n" +
                       $"Призове місце: {placeText}\n" +
                       $"Автор: {Author.Name}";
            }

            /// <summary>Змінити назву конкурсної роботи.</summary>
            /// <param name="newTitle">Нова назва.</param>
            public void ChangeTitle(string newTitle)
            {
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    Title = newTitle;
                }
                else
                {
                    // Порожній рядок ігнорується, щоб не зіпсувати дані
                }
            }
        }
    }
}