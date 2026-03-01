using System;

namespace Console_Lab2
{
    /// <summary>
    /// Головний клас програми.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входу, що запускає меню програми.
        /// </summary>
        static void Main()
        {
            Console.Clear();
            Service service = new Service();
            service.RunMenu();
        }
    }
}
