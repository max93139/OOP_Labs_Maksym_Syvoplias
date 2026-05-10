//Для перевірки і виправлення помилок використовувався Antigravity з моделью Claude Sonnet 4.6

namespace Lab4
{
    /// <summary>
    /// Клас «Меню» — чиста утиліта відображення.
    /// Відповідає виключно за показ пунктів меню та зчитування вибору користувача.
    /// Не знає нічого про класи Department або Student.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Виводить головне меню та повертає вибір користувача.
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
            svc.WriteToConsole("  13  — Додати кафедру до Каталогу A ");
            svc.WriteToConsole("  14  — Додати кафедру до Каталогу B ");
            svc.WriteToConsole("  15  — Показати каталог A (IEnumerable)");
            svc.WriteToConsole("  16  — Порівняти A і B  (IComparable)");
            svc.WriteToConsole("  17  — Сортув. A за студ.+дисц.(IComparer)");
            svc.WriteToConsole("  0   — Вихід                         ");
            svc.WriteToConsole("Ваш вибір: ");
            return svc.ReadFromConsole();
        }
    }
}
