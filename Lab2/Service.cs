using System;

// ReadInt, PrintVector, PrintMatrix, VectorBlock , MatrixBlock
namespace Console_Lab2
{
    class Service
    {
        public int ReadInt(string message) //Порадив зробити чат GPT при перевірці мого коду
        {
            Console.Write(message);
            return Convert.ToInt32(Console.ReadLine());
        }
        public void PrintVector(int[] data)
        {
            Console.WriteLine("Вектор:");
            for (int i = 0; i < data.Length; i++)
            {
                Console.Write(data[i] + " ");
            }
            Console.WriteLine();
        }
        public void PrintMatrix(int[,] data)
        {
            Console.WriteLine("Матриця:");
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Console.Write(data[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public void VectorBlock(Vector vector)
        {
            int size = ReadInt("Введіть розмір масиву: ");

            vector.Generate(size);

            PrintVector(vector.Elements);

            vector.ShellSort();
            Console.WriteLine("Після сортування: ");
            PrintVector(vector.Elements);

            Console.WriteLine("Сума: " + vector.Sum());
            Console.WriteLine("Середнє: " + vector.Average());
            Console.WriteLine("Максимум: " + vector.Max());

            int value = ReadInt("Введіть число для пошуку повторень: ");
            Console.WriteLine("Кількість повторень: " + vector.CountOccurrences(value));
        }
        public void MatrixBlock(Matrix matrix)
        {
            int employees = ReadInt("Введіть кількість співробітників: ");

            matrix.Generate(employees);

            PrintMatrix(matrix.Data);

            int month = ReadInt("Введіть номер місяця (1-12): ");

            Console.WriteLine("Зарплата за місяць: " + matrix.MonthSalary(month));
            Console.WriteLine("Річний бюджет: " + matrix.TotalSum()); 
        }
    }
}
