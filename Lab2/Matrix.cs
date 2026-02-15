namespace Console_Lab2
{
    // тут: Generate, TotalSum, MonthSalary
    class Matrix
    {
        private int rows;
        private int cols = 12;
        private int[,]? data;
        public int[,] Data => data!;

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
    }
}
