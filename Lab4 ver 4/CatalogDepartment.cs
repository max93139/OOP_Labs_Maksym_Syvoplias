//Для перевірки і виправлення помилок використовувався Antigravity з моделью Claude Sonnet 4.6
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab4
{
    /// <summary>
    /// Компаратор кафедр за кількістю студентів та кількістю дисциплін.
    /// Реалізує IComparer для багатокритерійного порівняння.
    /// Спочатку порівнюємо за кількістю студентів, потім — за кількістю дисциплін.
    /// </summary>
    public class DepartmentByStudentsAndDisciplinesComparer : IComparer<Department>
    {
        /// <summary>
        /// Порівнює дві кафедри спочатку за кількістю студентів,
        /// а при рівності — за кількістю дисциплін.
        /// </summary>
        /// <param name="first">Перша кафедра для порівняння.</param>
        /// <param name="second">Друга кафедра для порівняння.</param>
        /// <returns>Від'ємне — перша менша; 0 — рівні; додатнє — перша більша.</returns>
        public int Compare(Department? first, Department? second)
        {
            if (first == null && second == null)
            {
                return 0;
            }
            else if (first == null)
            {
                return -1;
            }
            else if (second == null)
            {
                return 1;
            }
            else
            {
                int studentComparison = first.StudentCount.CompareTo(second.StudentCount);
                if (studentComparison != 0)
                {
                    return studentComparison;
                }
                else
                {
                    return first.Disciplines.Count.CompareTo(second.Disciplines.Count);
                }
            }
        }
    }

    /// <summary>
    /// Клас «Каталог кафедр» — зберігає масив об'єктів Department та надає:
    /// - порівняння двох каталогів за сумарною кількістю студентів (IComparable),
    /// - сортування кафедр за студентами і дисциплінами (IComparer через окремий клас),
    /// - перебір кафедр у порядку зростання студентів (IEnumerable / IEnumerator).
    /// </summary>
    public class CatalogDepartment : IComparable<CatalogDepartment>, IEnumerable<Department>
    {
        private Department[] departments;
        private int departmentCount;
        private string catalogName;

        /// <summary>
        /// Ініціалізує порожній каталог із заданою назвою та максимальною місткістю.
        /// </summary>
        /// <param name="name">Назва каталогу (для виводу при порівнянні).</param>
        /// <param name="capacity">Максимальна кількість кафедр у каталозі.</param>
        public CatalogDepartment(string name, int capacity)
        {
            catalogName     = name;
            departments     = new Department[capacity];
            departmentCount = 0;
        }

        /// <summary>Отримує кількість кафедр у каталозі.</summary>
        public int DepartmentCount
        {
            get { return departmentCount; }
        }

        /// <summary>Отримує назву каталогу.</summary>
        public string CatalogName
        {
            get { return catalogName; }
        }

        /// <summary>
        /// Повертає компаратор для порівняння кафедр за студентами і дисциплінами.
        /// </summary>
        public static IComparer<Department> ByStudentsAndDisciplines
        {
            get { return new DepartmentByStudentsAndDisciplinesComparer(); }
        }

        // Методи колекції

        /// <summary>
        /// Додає кафедру до каталогу, якщо є вільне місце.
        /// </summary>
        /// <param name="department">Кафедра для додавання.</param>
        /// <returns>true — додано; false — каталог заповнено.</returns>
        public bool AddDepartment(Department department)
        {
            bool result;
            if (departmentCount >= departments.Length)
            {
                result = false;
            }
            else
            {
                departments[departmentCount] = department;
                departmentCount++;
                result = true;
            }
            return result;
        }

        // Реалізація IComparable

        /// <summary>
        /// Порівнює поточний каталог з іншим за загальною кількістю студентів.
        /// Реалізує IComparable для сортування каталогів між собою.
        /// </summary>
        /// <param name="other">Інший каталог для порівняння.</param>
        /// <returns>Від'ємне — поточний менший; 0 — рівні; додатнє — поточний більший.</returns>
        public int CompareTo(CatalogDepartment? other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                int totalThis  = CalculateTotalStudents();
                int totalOther = other.CalculateTotalStudents();
                return totalThis.CompareTo(totalOther);
            }
        }

        // Реалізація IEnumerable

        /// <summary>
        /// Повертає перелічувач кафедр, впорядкованих за зростанням кількості студентів.
        /// Реалізує IEnumerable для підтримки foreach.
        /// </summary>
        public IEnumerator<Department> GetEnumerator()
        {
            Department[] sorted = BuildSortedSnapshot();
            return new DepartmentEnumerator(sorted);
        }

        /// <summary>
        /// Неузагальнена версія GetEnumerator для сумісності з IEnumerable.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Handle-методи (оркестрація через Service)

        /// <summary>
        /// Додає передану кафедру до каталогу та повідомляє про результат.
        /// </summary>
        /// <param name="svc">Сервіс для виводу.</param>
        /// <param name="department">Кафедра для додавання.</param>
        public void HandleAddDepartment(Service svc, Department department)
        {
            if (string.IsNullOrWhiteSpace(department.EntityName))
            {
                svc.WriteToConsole("  Кафедра ще не створена. Спочатку виконайте п.1.");
            }
            else if (AddDepartment(department))
            {
                svc.WriteToConsole($"  Кафедру «{department.EntityName}» додано до каталогу «{catalogName}»." +
                                   $" Всього у каталозі: {departmentCount}.");
            }
            else
            {
                svc.WriteToConsole($"  Каталог «{catalogName}» заповнено. Більше кафедр додати неможливо.");
            }
        }

        /// <summary>
        /// Виводить усі кафедри каталогу через foreach (IEnumerable / IEnumerator).
        /// Кафедри відображаються впорядковано за кількістю студентів.
        /// </summary>
        /// <param name="svc">Сервіс для виводу.</param>
        public void HandleShowCatalog(Service svc)
        {
            svc.WriteToConsole($"\n[Каталог: {catalogName}] Кафедри впорядковано за к-стю студентів:");
            if (departmentCount == 0)
            {
                svc.WriteToConsole("  Каталог порожній. Додайте кафедру (п.13 або п.14).");
            }
            else
            {
                int position = 1;
                foreach (Department dept in this)
                {
                    svc.WriteToConsole($"  {position}. {dept.EntityName}" +
                                       $" | Студентів: {dept.StudentCount}" +
                                       $" | Дисциплін: {dept.Disciplines.Count}");
                    position++;
                }
            }
        }

        /// <summary>
        /// Порівнює поточний каталог з іншим (IComparable) за сумарною кількістю студентів
        /// та виводить результат порівняння.
        /// </summary>
        /// <param name="svc">Сервіс для виводу.</param>
        /// <param name="other">Каталог для порівняння з поточним.</param>
        public void HandleCompareTo(Service svc, CatalogDepartment other)
        {
            int totalThis  = CalculateTotalStudents();
            int totalOther = other.CalculateTotalStudents();
            int result     = CompareTo(other);

            svc.WriteToConsole($"\n[IComparable] Порівняння каталогів:");
            svc.WriteToConsole($"  «{catalogName}»: {totalThis} студентів");
            svc.WriteToConsole($"  «{other.CatalogName}»: {totalOther} студентів");

            if (result < 0)
            {
                svc.WriteToConsole($"  Результат: «{catalogName}» < «{other.CatalogName}»");
            }
            else if (result > 0)
            {
                svc.WriteToConsole($"  Результат: «{catalogName}» > «{other.CatalogName}»");
            }
            else
            {
                svc.WriteToConsole($"  Результат: «{catalogName}» == «{other.CatalogName}»");
            }
        }

        /// <summary>
        /// Виводить кафедри каталогу, відсортовані за IComparer
        /// (спочатку за кількістю студентів, при рівності — за кількістю дисциплін).
        /// </summary>
        /// <param name="svc">Сервіс для виводу.</param>
        public void HandleSortByStudentsAndDisciplines(Service svc)
        {
            svc.WriteToConsole($"\n[IComparer] Каталог «{catalogName}» — сортування за студентами і дисциплінами:");
            if (departmentCount == 0)
            {
                svc.WriteToConsole("  Каталог порожній. Додайте кафедру (п.13 або п.14).");
            }
            else
            {
                List<Department> sortedList = BuildDepartmentList();
                sortedList.Sort(ByStudentsAndDisciplines);

                for (int i = 0; i < sortedList.Count; i++)
                {
                    svc.WriteToConsole($"  {i + 1}. {sortedList[i].EntityName}" +
                                       $" | Студентів: {sortedList[i].StudentCount}" +
                                       $" | Дисциплін: {sortedList[i].Disciplines.Count}");
                }
            }
        }

        // Приватні допоміжні методи

        /// <summary>
        /// Підраховує загальну кількість студентів по всіх кафедрах каталогу.
        /// Використовується у CompareTo та HandleCompareTo.
        /// </summary>
        public int CalculateTotalStudents()
        {
            int total = 0;
            for (int i = 0; i < departmentCount; i++)
            {
                total += departments[i].StudentCount;
            }
            return total;
        }

        /// <summary>
        /// Будує відсортований за StudentCount знімок масиву кафедр для IEnumerator.
        /// Не змінює оригінальний масив.
        /// </summary>
        private Department[] BuildSortedSnapshot()
        {
            Department[] snapshot = new Department[departmentCount];
            for (int i = 0; i < departmentCount; i++)
            {
                snapshot[i] = departments[i];
            }
            SortByStudentCount(snapshot);
            return snapshot;
        }

        /// <summary>
        /// Будує список кафедр для сортування через IComparer.
        /// </summary>
        private List<Department> BuildDepartmentList()
        {
            List<Department> list = new List<Department>();
            for (int i = 0; i < departmentCount; i++)
            {
                list.Add(departments[i]);
            }
            return list;
        }

        /// <summary>
        /// Сортує масив кафедр за зростанням StudentCount методом вставки (in-place).
        /// </summary>
        private static void SortByStudentCount(Department[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                Department key = array[i];
                int        j   = i - 1;
                while (j >= 0 && array[j].StudentCount > key.StudentCount)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }
    }

    /// <summary>
    /// Перелічувач кафедр. Реалізує IEnumerator для перебору кафедр у foreach.
    /// Отримує готовий відсортований масив від CatalogDepartment.
    /// </summary>
    public class DepartmentEnumerator : IEnumerator<Department>
    {
        private readonly Department[] sortedDepartments;
        private int currentIndex;

        /// <summary>
        /// Ініціалізує перелічувач із відсортованим масивом кафедр.
        /// </summary>
        /// <param name="sortedDepartments">Масив кафедр, впорядкований за кількістю студентів.</param>
        public DepartmentEnumerator(Department[] sortedDepartments)
        {
            this.sortedDepartments = sortedDepartments;
            currentIndex           = -1;
        }

        /// <summary>
        /// Повертає поточну кафедру. Кидає виняток, якщо перелік не розпочато або вичерпано.
        /// </summary>
        public Department Current
        {
            get
            {
                if (currentIndex < 0 || currentIndex >= sortedDepartments.Length)
                {
                    throw new InvalidOperationException("Перелічувач знаходиться поза межами колекції.");
                }
                else
                {
                    return sortedDepartments[currentIndex];
                }
            }
        }

        /// <summary>Неузагальнена версія Current для сумісності з IEnumerator.</summary>
        object IEnumerator.Current
        {
            get { return Current; }
        }

        /// <summary>
        /// Переміщує перелічувач до наступного елемента.
        /// </summary>
        /// <returns>true — є наступний елемент; false — перелік вичерпано.</returns>
        public bool MoveNext()
        {
            currentIndex++;
            return currentIndex < sortedDepartments.Length;
        }

        /// <summary>Скидає перелічувач на початкову позицію (перед першим елементом).</summary>
        public void Reset()
        {
            currentIndex = -1;
        }

        /// <summary>Звільняє ресурси перелічувача (немає некерованих ресурсів).</summary>
        public void Dispose()
        {
            // Немає некерованих ресурсів для звільнення
        }
    }
}
