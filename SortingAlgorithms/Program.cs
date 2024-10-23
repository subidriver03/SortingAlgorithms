using System;
using System.Diagnostics;
//Vibe Rantz - Sorting Algorithms - HW1
namespace SortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
           Stopwatch stopwatch = new Stopwatch();

            int[] largeArr = GenerateRandomArray(100000, 1, 1000);
            Console.WriteLine("Array generated.");

            int[] arrForBubbleSort = (int[])largeArr.Clone();
            int[] arrForInsertionSort = (int[])largeArr.Clone();
            int[] arrForMergeSort = (int[])largeArr.Clone();
            int[] arrForQuickSort = (int[])largeArr.Clone();

            stopwatch.Start();
            BubbleSort(arrForBubbleSort);
            stopwatch.Stop();
            DisplayRuntime("Bubble Sort", stopwatch);

            stopwatch.Restart();
            InsertionSort(arrForInsertionSort);
            stopwatch.Stop();
            DisplayRuntime("Insertion Sort", stopwatch);

            stopwatch.Restart();
            MergeSort(arrForMergeSort, 0, arrForMergeSort.Length - 1);
            stopwatch.Stop();
            DisplayRuntime("Merge Sort", stopwatch);

            stopwatch.Restart();
            QuickSort(arrForQuickSort, 0, arrForQuickSort.Length - 1);
            stopwatch.Stop();
            DisplayRuntime("Quick Sort", stopwatch);

            Console.WriteLine("\nExplanation:");
            Console.WriteLine("Bubble Sort and Insertion Sort are simple algorithms with O(n^2) time complexity, making them inefficient for large datasets.");
            Console.WriteLine("Merge Sort and Quick Sort are more efficient for large datasets due to their O(n log n) time complexity.");
        }

        // Bubble Sort algorithm
        static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        static void InsertionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
        }

        // Merge Sort algorithm
        static void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);

                Merge(array, left, middle, right);
            }
        }

        static void Merge(int[] array, int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;

            int[] L = new int[n1];
            int[] R = new int[n2];

            for (int i = 0; i < n1; ++i)
                L[i] = array[left + i];
            for (int j = 0; j < n2; ++j)
                R[j] = array[middle + 1 + j];


            int iIndex = 0, jIndex = 0;

            int k = left;
            while (iIndex < n1 && jIndex < n2)
            {
                if (L[iIndex] <= R[jIndex])
                {
                    array[k] = L[iIndex];
                    iIndex++;
                }
                else
                {
                    array[k] = R[jIndex];
                    jIndex++;
                }
                k++;
            }

            while (iIndex < n1)
            {
                array[k] = L[iIndex];
                iIndex++;
                k++;
            }

            while (jIndex < n2)
            {
                array[k] = R[jIndex];
                jIndex++;
                k++;
            }
        }

        // Quick Sort algorithm
        static void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(array, low, high);

                QuickSort(array, low, pi - 1);
                QuickSort(array, pi + 1, high);
            }
        }

        static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high]; 
            int i = (low - 1); 

            for (int j = low; j <= high - 1; j++)
            {
                if (array[j] <= pivot)
                {
                    i++; 
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;

            return i + 1;
        }

        static int[] GenerateRandomArray(int length, int minValue, int maxValue)
        {
            Random rand = new Random();
            int[] array = new int[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = rand.Next(minValue, maxValue); 
            }

            return array;
        }

        // Function to display the runtime
        static void DisplayRuntime(string algorithmName, Stopwatch stopwatch)
        {
            TimeSpan ts = stopwatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            Console.WriteLine($"Algorithm: {algorithmName} Time Taken: {elapsedTime}");
        }
    }
}
