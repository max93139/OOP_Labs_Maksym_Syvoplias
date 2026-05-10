using System;

namespace Lab4
{
    /// <summary>
    /// Інтерфейс «Освітній суб'єкт» — вершина трирівневої ієрархії.
    /// Визначає контракт, якому мають відповідати всі освітні сутності.
    /// Дозволяє звертатись до Department та Student через спільне посилання,
    /// що демонструє поліморфізм і принцип підстановки Ліскова (LSP).
    /// </summary>
    public interface IEducationalEntity
    {
        /// <summary>Ім'я сутності.</summary>
        string EntityName { get; set; }

        /// <summary>Напрям підготовки.</summary>
        string EducationDirection { get; set; }

        /// <summary>Повертає ім'я сутності.</summary>
        string GetName();

        /// <summary>Встановлює нове ім'я сутності.</summary>
        /// <param name="name">Нове ім'я.</param>
        void SetName(string name);

        /// <summary>Повертає назву напряму підготовки.</summary>
        string GetEducationDirection();

        /// <summary>Встановлює новий напрям підготовки.</summary>
        /// <param name="direction">Новий напрям.</param>
        void SetEducationDirection(string direction);

        /// <summary>
        /// Повертає відформатований рядок з основною інформацією про сутність.
        /// Кожна конкретна сутність форматує дані по-своєму — поліморфна поведінка.
        /// </summary>
        string ToFormattedString();
    }
}
