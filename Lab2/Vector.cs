namespace Console_Lab2
{
    /// <summary>
    /// Клас для роботи з одновимірним масивом .
    /// </summary>
    class Vector
    {
        private int length;
        private int[] elements = new int[0];
        //не вистачає кількості елементів масиву за умовою, порушення функціональних вимог (виправлено)
        /// <summary>
        /// Елементи вектора. // Відкритий геттер для доступу до елементів масиву.
        /// </summary>
        public int[] Elements => elements;

        /// <summary>
        /// Генерує елементи масиву випадковими числами.
        /// </summary>
        /// <param name="elementsLength">Кількість елементів для генерації.</param>
        public void Generate(int elementsLength)
        {
            length = elementsLength;
            elements = new int[length];
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                elements[i] = rand.Next(-50 , 51); // Генерація випадкових чисел 
            }
        }

        /// <summary>
        /// Сортує масив за спаданням за алгоритмом Шелла.
        /// </summary>
        public void ShellSort() //https://www.geeksforgeeks.org/dsa/shell-sort/
        {
            int n = length;

            // Start with a large gap, then reduce it step by step
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                // Perform a "gapped" insertion sort for this gap elements.Length
                for (int i = gap; i < n; i++)
                {   
                    // Current element to be placed correctly
                    int temp = elements[i]; 
                    int j = i;

                    // Shift earlier elements that are LESS than temp (to sort descending)
                    while (j >= gap && elements[j - gap] < temp)
                    {
                        elements[j] = elements[j - gap];
                        j -= gap;
                    }

                    // Place temp in its correct position
                    elements[j] = temp;
                }
            }
        }

        /// <summary>
        /// Обчислює суму елементів масиву.
        /// </summary>
        /// <returns>Сума елементів.</returns>
        public int Sum()
        {
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += elements[i];
            }
            return sum;
        }
        /// <summary>
        /// Обчислює середнє арифметичне елементів масиву.
        /// </summary>
        /// <returns>Середнє арифметичне, заокруглене до 2 знаків.</returns>
        public double Average()
        {
            return length == 0 ? 0 : Math.Round((double)Sum() / length, 2);
        }
        /// <summary>
        /// Знаходить максимальний елемент масиву та його індекс.
        /// </summary>
        /// <param name="index">Індекс максимального елемента (або -1, якщо масив порожній).</param>
        /// <returns>Значення максимального елемента.</returns>
        public int Max(out int index)
        {
            if (length == 0)
            {
                index = -1;
                return 0;
            }

            int max = elements[0];
            index = 0;
            for (int i = 1; i < length; i++)
            {
                if (elements[i] > max)
                {
                    max = elements[i];
                    index = i;
                }
            }
            return max;
        }
        /// <summary>
        /// Визначає кількість повторень заданого елемента у масиві (лінійний пошук).
        /// </summary>
        /// <param name="value">Значення для пошуку.</param>
        /// <returns>Кількість знайдених повторень.</returns>
        public int CountOccurrences(int value)
        {
            int count = 0;
            for (int i = 0; i < length; i++)
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
