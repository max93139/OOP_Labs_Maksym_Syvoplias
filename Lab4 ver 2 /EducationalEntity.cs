using System;

namespace Lab4
{
    /// <summary>
    /// Абстрактний базовий клас «Освітній суб'єкт» — уніфікує спільні атрибути
    /// та операції для всіх учасників освітнього процесу.
    /// Оголошено abstract: унеможливлює пряме створення об'єкта цього типу,
    /// оскільки клас представляє загальну концепцію, а не конкретну сутність.
    /// Успадкування збережено — Department і Student є конкретними реалізаціями.
    /// </summary>
    public abstract class EducationalEntity
    {
        private string entityName;
        private string educationDirection;

        /// <summary>
        /// Ініціалізує поля зі значеннями за замовчуванням.
        /// </summary>
        protected EducationalEntity()
        {
            entityName         = "";
            educationDirection = "";
        }

        /// <summary>
        /// Ініціалізує поля базового класу із заданими параметрами.
        /// </summary>
        /// <param name="entityName">Ім'я сутності.</param>
        /// <param name="educationDirection">Назва напряму підготовки.</param>
        protected EducationalEntity(string entityName, string educationDirection)
        {
            this.entityName         = entityName;
            this.educationDirection = educationDirection;
        }

        /// <summary>
        /// Отримує або встановлює ім'я сутності.
        /// </summary>
        public string EntityName
        {
            get { return entityName; }
            set { entityName = value; }
        }

        /// <summary>
        /// Отримує або встановлює назву напряму підготовки.
        /// </summary>
        public string EducationDirection
        {
            get { return educationDirection; }
            set { educationDirection = value; }
        }

        /// <summary>
        /// Повертає ім'я сутності.
        /// </summary>
        public string GetName()
        {
            return entityName;
        }

        /// <summary>
        /// Встановлює нове ім'я сутності.
        /// </summary>
        /// <param name="name">Нове ім'я.</param>
        public void SetName(string name)
        {
            entityName = name;
        }

        /// <summary>
        /// Повертає назву напряму підготовки.
        /// </summary>
        public string GetEducationDirection()
        {
            return educationDirection;
        }

        /// <summary>
        /// Встановлює нову назву напряму підготовки.
        /// </summary>
        /// <param name="direction">Новий напрям підготовки.</param>
        public void SetEducationDirection(string direction)
        {
            educationDirection = direction;
        }
    }
}
