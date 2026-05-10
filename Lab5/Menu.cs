//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;

namespace Lab5
{
    /// <summary>
    /// Клас «Меню» — чиста утиліта відображення.
    /// Відповідає виключно за показ пунктів меню та зчитування вибору користувача.
    /// Не знає нічого про класи Department або Student.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Виводить меню програми через Service та повертає вибір користувача.
        /// </summary>
        /// <param name="svc">Сервіс для вводу/виводу даних.</param>
        /// <returns>Рядок із вибором користувача.</returns>
        public string GetChoice(Service svc)
        {
            svc.WriteToConsole("СИСТЕМА УПРАВЛІННЯ КАФЕДРОЮ");
            svc.WriteToConsole("  1   — Створити кафедру              ");
            svc.WriteToConsole("  2   — Додати студента               ");
            svc.WriteToConsole("  3   — Видалити студента             ");
            svc.WriteToConsole("  4   — Додати дисципліну             ");
            svc.WriteToConsole("  5   — Видалити дисципліну           ");
            svc.WriteToConsole("  6   — Розрахувати рейтинг студента  ");
            svc.WriteToConsole("  7   — Переглянути оцінки студента   ");
            svc.WriteToConsole("  8   — Додати оцінку студенту        ");
            svc.WriteToConsole("  9   — Зберегти дані у файл          ");
            svc.WriteToConsole("  10  — Змінити спеціальність студента");
            svc.WriteToConsole("  11  — Змінити навантаження студента ");
            svc.WriteToConsole("  12  — Читати дані з файлу           ");
            svc.WriteToConsole("  0   — Вихід                         ");
            Console.Write("Ваш вибір: ");
            return svc.ReadFromConsole();
        }
    }
}
