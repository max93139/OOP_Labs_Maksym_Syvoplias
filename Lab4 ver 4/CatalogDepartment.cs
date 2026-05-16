// Використовувався Antigravity з моделью Claude Sonnet 4.6
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab4
{
    /// <summary>
    /// Клас «Каталог кафедр» — зберігає масив об'єктів Department.
    /// Відповідно до вимог, реалізує 4 інтерфейси у цьому ж класі:
    /// - IComparable: порівняння двох каталогів за сумарною кількістю студентів
    /// - IComparer: порівняння двох кафедр за студентами і дисциплінами
    /// - IEnumerable та IEnumerator: перебір кафедр у порядку зростання студентів
    /// </summary>
    public class CatalogDepartment : IComparable<CatalogDepartment>, IComparer<Department>, IEnumerable<Department>, IEnumerator<Department>
    {
        private Department[] departments;
        private int departmentCount;
        private string catalogName;

        // Поля для реалізації IEnumerator
        private Department[] sortedSnapshot;
        private int currentIndex = -1;

        /// <summary>
        /// Ініціалізує порожній каталог із заданою назвою та максимальною місткістю.
        /// </summary>
        /// <param name="name">Назва каталогу.</param>
        /// <param name="capacity">Максимальна кількість кафедр у каталозі.</param>
        public CatalogDepartment(string name, int capacity)
        {
            catalogName     = name;
            departments     = new Department[capacity];
            departmentCount = 0;
            sortedSnapshot  = new Department[0];
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

        // Реалізація IComparable<CatalogDepartment>

        /// <summary>
        /// Порівнює поточний каталог з іншим за загальною кількістю студентів.
        /// </summary>
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

        // Реалізація IComparer<Department>

        /// <summary>
        /// Порівнює дві кафедри спочатку за кількістю студентів,
        /// а при рівності — за кількістю дисциплін.
        /// </summary>
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

        // Реалізація IEnumerable<Department>

        /// <summary>
        /// Повертає перелічувач (сам об'єкт), готуючи відсортований список.
        /// </summary>
        public IEnumerator<Department> GetEnumerator()
        {
            sortedSnapshot = BuildSortedSnapshot();
            Reset();
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Реалізація IEnumerator<Department>

        /// <summary>Повертає поточну кафедру під час ітерації.</summary>
        public Department Current
        {
            get
            {
                if (currentIndex < 0 || currentIndex >= sortedSnapshot.Length)
                {
                    throw new InvalidOperationException("Перелічувач знаходиться поза межами колекції.");
                }
                return sortedSnapshot[currentIndex];
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        /// <summary>Переміщує перелічувач до наступного елемента.</summary>
        public bool MoveNext()
        {
            currentIndex++;
            return currentIndex < sortedSnapshot.Length;
        }

        /// <summary>Скидає перелічувач на початкову позицію.</summary>
        public void Reset()
        {
            currentIndex = -1;
        }

        public void Dispose()
        {
            // Немає ресурсів для звільнення
        }

        // Handle-методи (оркестрація через Service)

        /// <summary>
        /// Додає передану кафедру до каталогу та повідомляє про результат.
        /// </summary>
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
        /// Виводить усі кафедри каталогу через foreach.
        /// Кафедри відображаються впорядковано за кількістю студентів.
        /// </summary>
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
        /// Порівнює поточний каталог з іншим (IComparable).
        /// </summary>
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
                sortedList.Sort(this); // Використовує поточний клас як IComparer

                for (int i = 0; i < sortedList.Count; i++)
                {
                    svc.WriteToConsole($"  {i + 1}. {sortedList[i].EntityName}" +
                                       $" | Студентів: {sortedList[i].StudentCount}" +
                                       $" | Дисциплін: {sortedList[i].Disciplines.Count}");
                }
            }
        }

        // Приватні допоміжні методи

        public int CalculateTotalStudents()
        {
            int total = 0;
            for (int i = 0; i < departmentCount; i++)
            {
                total += departments[i].StudentCount;
            }
            return total;
        }

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

        private List<Department> BuildDepartmentList()
        {
            List<Department> list = new List<Department>();
            for (int i = 0; i < departmentCount; i++)
            {
                list.Add(departments[i]);
            }
            return list;
        }

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
}
