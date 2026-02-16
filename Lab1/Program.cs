using System;

namespace lab1
{
    class Program
    {
         /// <summary>
         /// Задача 1. Вивести на екран анкетні дані та обчислити значення виразу , де x - число, яке вводиться користувачем.
         /// </summary>
         static void Task1()
        {
            const float a = 3.5f; //константи задані умовою завдання
            const float b = 3f;
            const float c = 10f;
            const float d = 8.3f;

            Console.WriteLine("Прізвище - Сивопляс");
            Console.WriteLine("Ім'я - Максим");
            Console.WriteLine("Вік - 17");
            Console.WriteLine("Група - ІПЗ-13");
            Console.WriteLine("1 курс");
            Console.WriteLine("e-mail - sivoplyasmv@gmail.com");

            Console.WriteLine("Вираз: p = 3.5 * x^4 -  3 * x^3 + 10 * x + 8.3");
            Console.Write("Введіть x: ");

            float x = Convert.ToSingle(Console.ReadLine());
            float p = (float)(a * Math.Pow(x, 4)
                            - b * Math.Pow(x, 3)
                            + c * x + d);

            Console.WriteLine("Результат: " + p);
        }
        /// <summary>
        /// Задача 2. Визначити значення виразу x = (a+b)^2 / √(sin(c) - cos(d)), де a, b, c, d - числа, які вводяться користувачем.
        /// </summary>
        static void Task2()
        {
            Console.WriteLine("x = (a+b)^2 / √(sin(c) - cos(d))");

            Console.Write("Введіть a: ");
            float a = Convert.ToSingle(Console.ReadLine());

            Console.Write("Введіть b: ");
            float b = Convert.ToSingle(Console.ReadLine());

            Console.Write("Введіть c: ");
            float c = Convert.ToSingle(Console.ReadLine());

            Console.Write("Введіть d: ");
            float d = Convert.ToSingle(Console.ReadLine());

            float x = (float)(Math.Pow(a + b, 2)
                            / Math.Sqrt(Math.Sin(c)
                            - Math.Cos(d)));

            Console.WriteLine("Результат: " + x);
        }
        /// <summary>
        /// Задача 3. Обчислити значення функції f(x) в точці x, якщо f(x) = 4x - 1, коли x > 0; f(x) = 25x + 10, коли x < 0; f(x) = 0, коли x = 0.
        /// </summary>
        static void Task3()
        {
            Console.Write("f(x) = 4x - 1");
            Console.Write("Введіть x: ");
            float x = Convert.ToSingle(Console.ReadLine());

            if ( x > 0 )
            {
                Console.WriteLine("f(x) = " + (4 * x - 1));
            }
            else if (x < 0)
            {
                Console.WriteLine("f(x) = " + (25 * x +10));
            }
            else if (x == 0 )
            {
                Console.WriteLine("f(x) = 0");
            }
        }
        /// <summary>
        ///  Задача 4. Визначити день тижня за його номером .
        /// </summary>
        static void Task4()
        {
            Console.Write("Введіть номер дня тижня (1–7): ");
            int day = Convert.ToInt32(Console.ReadLine()); // Номер дня тижня , яке вводиться користувачем

            switch (day)
            {
                case 1:
                    Console.WriteLine("Понеділок");
                    break;
                case 2:
                    Console.WriteLine("Вівторок");
                    break;
                case 3:
                    Console.WriteLine("Середа");
                    break;
                case 4:
                    Console.WriteLine("Четвер");
                    break;
                case 5:
                    Console.WriteLine("Пʼятниця");
                    break;
                case 6:
                    Console.WriteLine("Субота");
                    break;
                case 7:
                    Console.WriteLine("Неділя");
                    break;
                default:
                    Console.WriteLine("Помилка: номер дня має бути від 1 до 7");
                    break;
            }
        }
        /// <summary>
        /// Задача 5. Визначити суму чисел, які вводяться користувачем, до тих пір, поки не буде введено число 0.
        /// </summary>
        static void Task5()
        {
            const int min = 1;
            const int max = 100;

            int sum = 0;
            int value = -1;

            Console.WriteLine($"Вводьте числа в діапазоні [{min}; {max}]");
            Console.WriteLine("Для завершення введіть 0");

            do
            {
                Console.Write("Число: ");
                string input = Console.ReadLine() ?? "" ;

                if (int.TryParse(input, out int temp))
                {
                    value = temp;

                    if (value != 0)
                    {
                        if (value < min || value > max)
                        {
                            Console.WriteLine("Помилка! Число поза діапазоном.");
                        }
                        else
                        {
                            sum += value;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Помилка! Введено не число.");
                }
            } while (value != 0);

            Console.WriteLine($"Сума чисел = {sum}");
        }
        /// <summary>
        /// Головний метод, який відображає меню та виконує вибрані завдання.
        /// </summary>
        static void Main()
        {
            string choice;

            do
            {
                Console.Clear();
                Console.WriteLine("Виберіть потрібний пункт меню");
                Console.WriteLine("1. Вивести анкетні дані і вираз");
                Console.WriteLine("2. Визначити значення виразу");
                Console.WriteLine("3. Обчислити значення функції в точці x");
                Console.WriteLine("4. Визначити день тижня за його номером");
                Console.WriteLine("5. Обчислити суму чисел в заданому діапазоні");
                Console.WriteLine("0. Вихід з програми");

                choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Task3();
                        break;
                    case "4":
                        Task4();
                        break;
                    case "5":
                        Task5();
                        break;
                    case "0":
                        Console.WriteLine("Завершення програми...");
                         Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }
                if (choice != "0")
                {
                    Console.WriteLine("\nНатисніть Enter, щоб повернутися до меню...");
                    Console.ReadLine();
                }
            } while (choice != "0");
        }
    }
}