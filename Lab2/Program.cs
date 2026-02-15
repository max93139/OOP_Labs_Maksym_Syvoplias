using System;

namespace Console_Lab2
{
    class Program
    {
        static void Main()
        {
            Service service = new Service();
            Vector vector = new Vector();
            Matrix matrix = new Matrix();
            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Робота з масивом:");
                Console.WriteLine("2. Робота з матрицею:");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("Ваш вибір: ");

                choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                        service.VectorBlock(vector);
                        break;
                    case "2":
                        service.MatrixBlock(matrix);
                        break;
                    case "0":
                        Console.WriteLine("Завершення програми...");
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                } 
                   if (choice != "0")
                {
                    Console.WriteLine("\nНатисніть Enter, щоб повернутися до меню...");
                    Console.ReadLine();
                }
            }while (choice != "0");
        }
    }
}
