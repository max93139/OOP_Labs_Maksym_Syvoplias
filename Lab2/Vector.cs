namespace Console_Lab2
{
     // тут: Generate, ShellSort, Sum, Average
    class Vector
    {
        
        private int[]? elements;

        public int[] Elements => elements!;
        public void Generate(int elementsLength)
        {
            elements = new int[elementsLength];
            Random rand = new Random();
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = rand.Next(-50 , 51); // Генерація випадкових чисел 
            }
        }

        public void ShellSort() //https://www.geeksforgeeks.org/dsa/shell-sort/
        {
            int n = elements.Length;

            // Start with a large gap, then reduce it step by step
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                // Perform a "gapped" insertion sort for this gap elements.Length
                for (int i = gap; i < n; i++)
                {   
                    // Current element to be placed correctly
                    int temp = elements[i]; 
                    int j = i;

                    // Shift earlier elements that are greater than temp
                    while (j >= gap && elements[j - gap] > temp)
                    {
                        elements[j] = elements[j - gap];
                        j -= gap;
                    }

                    // Place temp in its correct position
                    elements[j] = temp;
                }
            }
        }

        public int Sum ()
        {
            int sum = 0;
            for(int i = 0 ; i < elements.Length ; i++)
            {
                sum += elements[i];
            }
            return sum;
        }
        public double Average ()
        {
            return (double)Sum() / elements.Length;
        }
        public int Max ()
        {
            int max = elements[0];
            for(int i = 1 ; i < elements.Length; i++)
            {
                if (elements[i] > max)
                max = elements[i];
            }
            return max ;
        }
        public int CountOccurrences(int value)
        {
            int count = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == value)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
