using System;

namespace Lab5
{
    /// <summary>
    /// Клас «Конкурсна робота студента» — творчий результат студента.
    /// Відображає індивідуальну активність. Зв'язаний асоціаціями з Кафедрою і Студентом.
    /// </summary>
    public class CompetitiveWork
    {
        private string workTitle;
        private string contestTheme;
        private DateTime submissionDate;
        private double gradeRating;
        private string status;
        private int prizePlace;
        private Student author;
        private Department managingDepartment;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="CompetitiveWork"/>.
        /// </summary>
        public CompetitiveWork(string workTitle, string contestTheme, Student author, Department managingDepartment)
        {
            this.workTitle          = workTitle;
            this.contestTheme       = contestTheme;
            this.author             = author;
            this.managingDepartment = managingDepartment;
            this.submissionDate     = DateTime.MinValue;
            this.gradeRating        = 0.0;
            this.status             = "not submitted";
            this.prizePlace         = 0;
        }

        // Властивості

        public string WorkTitle
        {
            get { return workTitle; }
            set { workTitle = value; }
        }

        public string ContestTheme
        {
            get { return contestTheme; }
            set { contestTheme = value; }
        }

        public DateTime SubmissionDate
        {
            get { return submissionDate; }
        }

        public double GradeRating
        {
            get { return gradeRating; }
        }

        public string Status
        {
            get { return status; }
        }

        public int PrizePlace
        {
            get { return prizePlace; }
        }

        public Student Author
        {
            get { return author; }
        }

        public Department ManagingDepartment
        {
            get { return managingDepartment; }
        }

        // Операції

        /// <summary>
        /// Подати роботу.
        /// </summary>
        public void SubmitWork()
        {
            submissionDate = DateTime.Now;
            status = "submitted";
        }

        /// <summary>
        /// Визначити відповідність теми роботи тематиці конкурсу.
        /// </summary>
        public bool DetermineRelevanceToTheme(string theme)
        {
            bool isRelevant;
            if (string.Equals(contestTheme, theme, StringComparison.OrdinalIgnoreCase))
            {
                isRelevant = true;
            }
            else
            {
                isRelevant = false;
            }
            return isRelevant;
        }

        /// <summary>
        /// Оцінити роботу.
        /// </summary>
        public void GradeWork(double grade)
        {
            if (grade >= 0 && grade <= 100)
            {
                gradeRating = grade;
            }
            else
            {
                // Невірна оцінка
            }
        }

        /// <summary>
        /// Призначити статус.
        /// </summary>
        public void AssignStatus(string newStatus)
        {
            if (newStatus == "submitted" || newStatus == "accepted" || newStatus == "rejected")
            {
                status = newStatus;
            }
            else
            {
                // Невідомий статус
            }
        }

        /// <summary>
        /// Присвоїти призове місце.
        /// </summary>
        public void AssignPrizePlace(int place)
        {
            if (place >= 1 && place <= 3)
            {
                prizePlace = place;
            }
            else
            {
                // Не призове місце
                prizePlace = 0;
            }
        }

        /// <summary>
        /// Переглянути деталі конкурсної роботи.
        /// </summary>
        public string ViewDetails()
        {
            return $"Конкурсна робота: '{workTitle}'\n" +
                   $"Тематика: {contestTheme}\n" +
                   $"Автор: {author.EntityName}\n" +
                   $"Кафедра: {managingDepartment.EntityName}\n" +
                   $"Дата подання: {(submissionDate == DateTime.MinValue ? "не подано" : submissionDate.ToString("yyyy-MM-dd HH:mm:ss"))}\n" +
                   $"Оцінка: {gradeRating:F2}\n" +
                   $"Статус: {status}\n" +
                   $"Призове місце: {(prizePlace > 0 ? prizePlace.ToString() : "немає")}";
        }
    }
}
