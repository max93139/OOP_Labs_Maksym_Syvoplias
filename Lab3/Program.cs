using System;

namespace Lab3
{
    /// <summary>
    /// Головний клас програми, що містить точку входу.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Головний метод програми. Налаштовує кодування консолі та ініціалізує сервіс.
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding  = System.Text.Encoding.UTF8;
            Console.Clear();

            Service service = new Service("console", "department_data.txt");
            service.RunMenu();
        }
    }
}
