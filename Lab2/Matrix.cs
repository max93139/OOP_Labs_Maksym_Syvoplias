namespace Console_Lab2
{
    /// <summary>
    /// Клас для роботи з матрицею.
    /// </summary>
    class Matrix
    {
        private int rows;
        private const int MONTH_COUNT = 12;
        private int cols = MONTH_COUNT;
        private int[,] data = null!;

        /// <summary>
        /// Елементи матриці.
        /// </summary>
        public int[,] Data => data!;

        /// <summary>
        /// Генерує матрицю випадкових зарплат.
        /// </summary>
        /// <param name="employees">Кількість співробітників (рядків).</param>
        public void Generate(int employees)
        {
            rows = employees ; 
            data = new int [rows, cols];
            Random rand = new Random();
            for (int i = 0; i < rows; i++)
            {                
                for (int j = 0; j < cols; j++)
                {                   
                     data[i, j] = rand.Next(1000, 5001); // Генерація випадкових чисел для бюджету
                }   
            }
        }

        /// <summary>
        /// Обчислює річний бюджет (загальну суму всіх виплат).
        /// </summary>
        /// <returns>Сумарна зарплата всіх співробітників за всі місяці.</returns>
        public int TotalSum()
        {
            int totalSum = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    totalSum += data[i, j];
                }
            }
            return totalSum;
        }

        /// <summary>
        /// Обчислює загальну зарплату всіх співробітників за вказаний місяць.
        /// </summary>
        /// <param name="month">Номер місяця (1-12).</param>
        /// <returns>Загальна зарплата за місяць.</returns>
        public int MonthSalary(int month)
        {
            int sum = 0;
            int m = month - 1;

            for (int i = 0; i < rows; i++)
            {
                sum += data[i, m];
            }

            return sum;
        }

        /// <summary>
        /// Обчислює середню зарплату одного співробітника за вказаний місяць.
        /// </summary>
        /// <param name="month">Номер місяця (1-12).</param>
        /// <returns>Середня місячна зарплата.</returns>
        public double MonthAverageSalary(int month)
        {
            if (rows == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round((double)MonthSalary(month) / rows, 2);
            }
        }
    }
}
