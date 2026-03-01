using System;

namespace Console_Lab2
{
    class Service
    {
        public void PrintVector(int[] data)
        {
            Console.WriteLine("Вектор:");
            for (int i = 0; i < data.Length; i++)
                Console.Write(data[i] + " ");
            Console.WriteLine();
        }

        public void PrintMatrix(int[,] data)
        {
            Console.WriteLine("Матриця:");
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                    Console.Write(data[i, j] + "\t");
                Console.WriteLine();
            }
        }

        public void VectorBlock(Vector vector)
        {
            int size;

            while (true)
            {
                Console.Write("Введіть розмір масиву: ");
                if (int.TryParse(Console.ReadLine(), out size) && size > 0)
                    break;

                Console.WriteLine("Помилка! Введіть додатне ціле число.");
            }

            vector.Generate(size);

            PrintVector(vector.Elements);

            vector.ShellSort();
            Console.WriteLine("Після сортування:");
            PrintVector(vector.Elements);

            Console.WriteLine("Сума: " + vector.Sum());
            Console.WriteLine("Середнє: " + vector.Average());
            Console.WriteLine("Максимум: " + vector.Max());

            int value;

            while (true)
            {
                Console.Write("Введіть число для пошуку повторень: ");
                if (int.TryParse(Console.ReadLine(), out value))
                    break;

                Console.WriteLine("Помилка! Введіть ціле число.");
            }

            Console.WriteLine("Кількість повторень: " + vector.CountOccurrences(value));
        }

        public void MatrixBlock(Matrix matrix)
        {
            int employees;

            while (true)
            {
                Console.Write("Введіть кількість співробітників: ");
                if (int.TryParse(Console.ReadLine(), out employees) && employees > 0)
                    break;

                Console.WriteLine("Помилка! Введіть додатне ціле число.");
            }

            matrix.Generate(employees);
            PrintMatrix(matrix.Data);

            int month;

            while (true)
            {
                Console.Write("Введіть номер місяця (1-12): ");
                if (int.TryParse(Console.ReadLine(), out month) && month >= 1 && month <= 12)
                    break;

                Console.WriteLine("Помилка! Введіть число від 1 до 12.");
            }

            Console.WriteLine("Зарплата за місяць: " + matrix.MonthSalary(month));
            Console.WriteLine("Річний бюджет: " + matrix.TotalSum());
        }
    }
}
