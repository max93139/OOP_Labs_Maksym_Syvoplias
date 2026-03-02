using System;

namespace Console_Lab2
{
    /// <summary>
    /// Сервісний клас для взаємодії з користувачем.
    /// </summary>
    class Service
    {
        /// <summary>
        /// Запускає головний цикл меню програми.
        /// </summary>
        public void RunMenu()
        {
            Vector vector = new Vector();
            Matrix matrix = new Matrix();

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
                        VectorBlock(vector); // запускаємо вектор
                        break;

                    case "2":
                        MatrixBlock(matrix); // запускаємо матрицю
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
            Console.WriteLine(new string('-', data.GetLength(1) * 8)); // лінія-розділювач для краси

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
        /// </summary>
        /// <param name="vector">Екземпляр класу Vector.</param>
        public void VectorBlock(Vector vector)
        {
            int size = 0;
            bool isValidSize = false;

            while (!isValidSize)
            {
                Console.Write("Введіть розмір масиву: ");
                if (int.TryParse(Console.ReadLine(), out size) && size > 0)
                {
                    isValidSize = true;
                }
                else
                {
                    Console.WriteLine("Помилка! Введіть додатне ціле число.");
                }
            }

            vector.Generate(size);

            PrintVector(vector.Elements);

            // Знаходимо максимум та його індекс до сортування
            int maxIndex;
            int maxElement = vector.Max(out maxIndex);

            vector.ShellSort();
            Console.WriteLine("Після сортування:");
            PrintVector(vector.Elements);

            Console.WriteLine("Сума: " + vector.Sum());
            Console.WriteLine("Середнє: " + vector.Average());
            Console.WriteLine($"Максимум: {maxElement} (Індекс до сортування: {maxIndex})");

            int value = 0;
            bool isValidValue = false;

            while (!isValidValue)
            {
                Console.Write("Введіть число для пошуку повторень: ");
                if (int.TryParse(Console.ReadLine(), out value))
                {
                    isValidValue = true;
                }
                else
                {
                    Console.WriteLine("Помилка! Введіть ціле число.");
                }
            }

            Console.WriteLine("Кількість повторень: " + vector.CountOccurrences(value));
        }

        /// <summary>
        /// Блок коду для роботи з матрицею (генерація, виведення бюджету та зарплати за місяць).
        /// </summary>
        /// <param name="matrix">Екземпляр класу Matrix.</param>
        public void MatrixBlock(Matrix matrix)
        {
            int employees = 0;
            bool isValidEmployees = false;

            while (!isValidEmployees)
            {
                Console.Write("Введіть кількість співробітників: ");
                if (int.TryParse(Console.ReadLine(), out employees) && employees > 0)
                {
                    isValidEmployees = true;
                }
                else
                {
                    Console.WriteLine("Помилка! Введіть додатне ціле число.");
                }
            }

            matrix.Generate(employees);
            PrintMatrix(matrix.Data);

            int month = 0;
            bool isValidMonth = false;

            while (!isValidMonth)
            {
                Console.Write("Введіть номер місяця (1-12): ");
                if (int.TryParse(Console.ReadLine(), out month) && month >= 1 && month <= 12)
                {
                    isValidMonth = true;
                }
                else
                {
                    Console.WriteLine("Помилка! Введіть число від 1 до 12.");
                }
            }

            Console.WriteLine("Зарплата за місяць: " + matrix.MonthSalary(month));
            Console.WriteLine("Середня зарплата за місяць: " + matrix.MonthAverageSalary(month));
            Console.WriteLine("Річний бюджет: " + matrix.TotalSum());
        }
    }
}
