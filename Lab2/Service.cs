using System;

namespace Console_Lab2
{
    /// <summary>
    /// Сервісний клас для взаємодії з користувачем.
    /// </summary>
    class Service
    {
        private Vector vector = new Vector();
        private Matrix matrix = new Matrix();

        /// <summary>
        /// Запускає головний цикл меню програми.
        /// </summary>
        public void RunMenu()
        {
            string choice;

            do 
            {
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("1 - Робота з масивом");
                Console.WriteLine("2 - Робота з матрицею");
                Console.WriteLine("0 - Вихід");
                Console.Write("Оберіть пункт: ");

                choice = Console.ReadLine() ?? string.Empty;

                switch (choice) // перевіряємо вибір
                {
                    case "1":
                        VectorBlock(); // запускаємо вектор
                        break;

                    case "2":
                        MatrixBlock(); // запускаємо матрицю
                        break;

                    case "0":
                        Console.WriteLine("Завершення програми...");
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Невірний вибір!"); 
                        break;
                }

                Console.WriteLine(); 
            } while (choice != "0");
        }

        /// <summary>
        /// Виводить одновимірний масив на консоль.
        /// </summary>
        /// <param name="data">Дані для виводу.</param>
        public void PrintVector(int[] data)
        {
            Console.WriteLine("Вектор:");
            for (int i = 0; i < data.Length; i++)
                Console.Write(data[i] + " ");
            Console.WriteLine();
        }

        /// <summary>
        /// Виводить двовимірний масив (матрицю) на консоль.
        /// </summary>
        /// <param name="data">Дані для виводу.</param>
        public void PrintMatrix(int[,] data)
        {
            Console.WriteLine("Матриця:");

            // Виводимо номери місяців як заголовки стовпців
            for (int j = 1; j <= data.GetLength(1); j++)
            {
                Console.Write(j + "\t");
            }
            Console.WriteLine();
            Console.WriteLine(new string('-', data.GetLength(1) * 8)); 

            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Console.Write(data[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Блок коду для роботи з вектором.
        /// Координує виконання всіх операцій над вектором.
        /// </summary>
        public void VectorBlock()
        {
            InitVector();

            int maxElement = FindMax(out int maxIndex);

            SortVector();
            PrintSortedVector();
            PrintVectorStats(maxElement, maxIndex);
            CountOccurrences();
        }

        /// <summary>
        /// Зчитує з консолі ціле число в допустимому діапазоні.
        /// </summary>
        /// <param name="prompt">Текст запиту.</param>
        /// <param name="errorMessage">Повідомлення при некоректному введенні.</param>
        /// <param name="minValue">Мінімально допустиме значення.</param>
        /// <param name="maxValue">Максимально допустиме значення.</param>
        /// <returns>Введене ціле число.</returns>
        private int ReadInt(string prompt, string errorMessage, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value >= minValue && value <= maxValue)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        /// <summary>
        /// Генерує вектор заданого розміру.
        /// </summary>
        /// <param name="size">Розмір вектора.</param>
        private void GenerateVector(int size)
        {
            vector.Generate(size);
        }

        /// <summary>
        /// Зчитує розмір від користувача та ініціалізує вектор.
        /// </summary>
        private void InitVector()
        {
            int size = ReadInt("Введіть розмір масиву: ", "Помилка! Введіть додатне ціле число.", 1);
            GenerateVector(size);
            PrintVector(vector.Elements);
        }

        /// <summary>
        /// Знаходить максимальний елемент вектора та його індекс до сортування.
        /// </summary>
        /// <param name="maxIndex">Індекс максимального елемента до сортування.</param>
        /// <returns>Максимальний елемент.</returns>
        private int FindMax(out int maxIndex)
        {
            return vector.Max(out maxIndex);
        }

        /// <summary>
        /// Сортує вектор.
        /// </summary>
        private void SortVector()
        {
            vector.ShellSort();
        }

        /// <summary>
        /// Виводить відсортований вектор.
        /// </summary>
        private void PrintSortedVector()
        {
            Console.WriteLine("Після сортування:");
            PrintVector(vector.Elements);
        }

        /// <summary>
        /// Виводить суму, середнє та максимум вектора.
        /// </summary>
        /// <param name="maxElement">Максимальний елемент (знайдений до сортування).</param>
        /// <param name="maxIndex">Індекс максимального елемента до сортування.</param>
        private void PrintVectorStats(int maxElement, int maxIndex)
        {
            Console.WriteLine("Сума: " + vector.Sum());
            Console.WriteLine("Середнє: " + vector.Average());
            Console.WriteLine($"Максимум: {maxElement} (Індекс до сортування: {maxIndex})");
        }

        /// <summary>
        /// Зчитує значення та виводить кількість його повторень у векторі.
        /// </summary>
        private void CountOccurrences()
        {
            while (true)
            {
                Console.Write("Введіть число для пошуку повторень (або '*' для виходу): ");
                string input = Console.ReadLine() ?? string.Empty;

                if (input == "*")
                {
                    break;
                }

                if (int.TryParse(input, out int value))
                {
                    Console.WriteLine("Кількість повторень: " + vector.CountOccurrences(value));
                }
                else
                {
                    Console.WriteLine("Помилка! Введіть ціле число або '*'.");
                }
            }
        }

        /// <summary>
        /// Блок коду для роботи з матрицею.
        /// Координує виконання всіх операцій над матрицею.
        /// </summary>
        public void MatrixBlock()
        {
            InitMatrix();

            int month = ReadMonth();
            PrintMatrixStats(month);
        }

        /// <summary>
        /// Зчитує кількість співробітників та ініціалізує матрицю.
        /// </summary>
        private void InitMatrix()
        {
            int employees = ReadInt("Введіть кількість співробітників: ", "Помилка! Введіть додатне ціле число.", 1);
            GenerateMatrix(employees);
            PrintMatrix(matrix.Data);
        }

        /// <summary>
        /// Генерує матрицю заданої кількості співробітників.
        /// </summary>
        /// <param name="employees">Кількість співробітників.</param>
        private void GenerateMatrix(int employees)
        {
            matrix.Generate(employees);
        }

        /// <summary>
        /// Зчитує номер місяця від користувача (1–12).
        /// </summary>
        /// <returns>Валідний номер місяця.</returns>
        private int ReadMonth()
        {
            return ReadInt("Введіть номер місяця (1-12): ", "Помилка! Введіть число від 1 до 12.", 1, 12);
        }

        /// <summary>
        /// Виводить зарплату, середню зарплату за місяць та річний бюджет.
        /// </summary>
        /// <param name="month">Номер місяця.</param>
        private void PrintMatrixStats(int month)
        {
            Console.WriteLine("Зарплата за місяць: " + matrix.MonthSalary(month));
            Console.WriteLine("Середня зарплата за місяць: " + matrix.MonthAverageSalary(month));
            Console.WriteLine("Річний бюджет: " + matrix.TotalSum());
        }
    }
}
