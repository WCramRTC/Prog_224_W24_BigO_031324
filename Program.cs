using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace Prog_224_W24_BigO_031324
{

    internal class Program
    {


        static Stopwatch sw = new Stopwatch();

        static void Main(string[] args)
        {

            Console.WriteLine("Code Start");

            sw.Start();
            char[] set = { 'a', 'b', 'c' };
            // abc = 1 milli
            // abcd = 3 milli
            // abcde = 8 milli
            // abcdef = 14 milli
            // abcdefg = 31 milli
            // abcdefghijklm = 3000
            // abcdefghijklmn = 6000
            // abcdefghijklmno = 15000
            // abcdefghijklmnop = 30000


            GenerateSubsets("abcdefghijklmnop".ToCharArray());
            sw.Stop();

            Console.WriteLine("Elapsed Time: " + sw.ElapsedMilliseconds);
            
        }

        static void GenerateSubsets(char[] set)
        {
            int n = set.Length;
            int count = 0;

            // Outer loop for generating subsets of different sizes
            // Time Complexity: O(2^n), as the number of iterations doubles with each increment of 'size'
            for (int size = 0; size <= n; size++)
            {
                // Inner loop for generating subsets of a specific size
                // Time Complexity: O(2^n), as it iterates through all possible combinations of 'n' elements
                for (int i = 0; i < (1 << n); i++)
                {
                    List<char> subset = new List<char>();

                    // Iterate through each element of the set
                    // Time Complexity: O(n), as it iterates through each element of the set
                    for (int j = 0; j < n; j++)
                    {
                        // Check if jth bit of i is set
                        // Time Complexity: O(1), as it performs bitwise operation
                        if ((i & (1 << j)) > 0)
                        {
                            // If set, add corresponding element to subset
                            // Time Complexity: O(1), as it appends an element to the subset list
                            subset.Add(set[j]);
                        }
                    }
                    
                    // If the size of the subset matches the current iteration of the outer loop,
                    // print the subset
                    // Time Complexity: O(1), as it prints the subset
                    if (subset.Count == size)
                    {
                        Console.Write("{ ");
                        foreach (char item in subset)
                        {
                            // Time Complexity: O(1), as it prints an element of the subset
                            Console.Write(item + " ");
                        }
                        Console.WriteLine("}");
                    }
                }
                count++;
            }

        }



        public static void OnExp(int iterations)
        {

            // O(n^2) = 10000 ^ 10000 - 91 milli
            // O(n^2) = 100000 ^ 100000 - 6705 milli
            // O(n^2) = 100000 ^ 100000 - 6705 milli

            //Console.WriteLine("Elapsed: " + sw.ElapsedMilliseconds);

            //Console.WriteLine("Starting Code");
            //// O(n) n = 2147483647 - 1.8 
            //sw.Start();
            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine(i);
                for (int j = 0; j < iterations; j++)
                {

                }
            }
            sw.Stop();

            //Console.WriteLine("Ending Code");

            //Console.WriteLine("Elapsed Milli: " + sw.ElapsedMilliseconds);

        }

        public static void nLogN(int iterations)
        {

            // O ( n log n ) - 50 - 24 milli
            // O ( n log n ) - 500 - 18 milli
            // 500000 - 23
            // 5000000 - 26
            // 50000000 - 33
            // 500000000 - 33


            //sw.Start();
            //for (int i = iterations; i > 0; i /= 2 )
            //{
            //    for (int j = i; j > 0; j /= 2)
            //    {
            //        Console.Write(j + " ");
            //    }
            //    Console.WriteLine();
            //}
            //sw.Stop();

        }

        public static void LinearParallel()
        {
            int size = 100;
            int[] mergeDemo = GenerateUniqueRandomArray(100);

            //sw.Start();
            //MergeSort(mergeDemo, 0, mergeDemo.Length - 1);
            //sw.Stop();

            Console.WriteLine("Elapsed: " + sw.ElapsedMilliseconds);

            for (int i = 100; i < 10000000000; i *= 5)
            {
                Console.WriteLine("Current Size: " + i);
                mergeDemo = GenerateUniqueRandomArray(i);
                sw.Start();
                ParallelLinearSearch(mergeDemo, 0);
                sw.Stop();

                Console.WriteLine("Elapsed: " + sw.ElapsedMilliseconds);
            }

            Console.WriteLine("End");

        }

        static void ParallelMergeSort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;

                // Parallelize sorting of left and right halves
                Parallel.Invoke(
                    () => ParallelMergeSort(arr, l, m),
                    () => ParallelMergeSort(arr, m + 1, r)
                );

                // Merge the sorted halves
                Merge(arr, l, m, r);
            }
        }

        static int ParallelLinearSearch(int[] arr, int target)
        {
            int n = arr.Length;
            int result = -1;

            Parallel.For(0, n, (i, state) =>
            {
                if (arr[i] == target)
                {
                    result = i;
                    state.Stop();
                }
            });

            return result;
        }

        static void Merge(int[] arr, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;

            int[] L = new int[n1];
            int[] R = new int[n2];

            for (int i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (int j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            int leftIndex = 0, rightIndex = 0, mergedIndex = l;

            while (leftIndex < n1 && rightIndex < n2)
            {
                if (L[leftIndex] <= R[rightIndex])
                {
                    arr[mergedIndex] = L[leftIndex];
                    leftIndex++;
                }
                else
                {
                    arr[mergedIndex] = R[rightIndex];
                    rightIndex++;
                }
                mergedIndex++;
            }

            while (leftIndex < n1)
            {
                arr[mergedIndex] = L[leftIndex];
                leftIndex++;
                mergedIndex++;
            }

            while (rightIndex < n2)
            {
                arr[mergedIndex] = R[rightIndex];
                rightIndex++;
                mergedIndex++;
            }
        }


        static int[] GenerateUniqueRandomArray(int size)
        {
            Random rand = new Random();
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
            {
                int randomNumber;
                bool isUnique;
                do
                {
                    randomNumber = rand.Next(1, 999999999); // Generates random integers between 1 and 99
                    isUnique = true;

                    // Check if the random number is already in the array
                    for (int j = 0; j < i; j++)
                    {
                        if (array[j] == randomNumber)
                        {
                            isUnique = false;
                            break;
                        }
                    }
                } while (!isUnique);

                array[i] = randomNumber;
            }

            return array;
        }


        //// Merge two subarrays of arr[]
        //// First subarray is arr[l..m]
        //// Second subarray is arr[m+1..r]
        //static void Merge(int[] arr, int l, int m, int r)
        //{
        //    // Find sizes of two subarrays to be merged
        //    int n1 = m - l + 1;
        //    int n2 = r - m;

        //    // Create temporary arrays
        //    int[] L = new int[n1];
        //    int[] R = new int[n2];

        //    // Copy data to temporary arrays L[] and R[]
        //    for (int i = 0; i < n1; ++i)
        //        L[i] = arr[l + i];
        //    for (int j = 0; j < n2; ++j)
        //        R[j] = arr[m + 1 + j];

        //    // Merge the temporary arrays
        //    int leftIndex = 0, rightIndex = 0, mergedIndex = l;

        //    while (leftIndex < n1 && rightIndex < n2)
        //    {
        //        if (L[leftIndex] <= R[rightIndex])
        //        {
        //            arr[mergedIndex] = L[leftIndex];
        //            leftIndex++;
        //        }
        //        else
        //        {
        //            arr[mergedIndex] = R[rightIndex];
        //            rightIndex++;
        //        }
        //        mergedIndex++;
        //    }

        //    // Copy remaining elements of L[] if any
        //    while (leftIndex < n1)
        //    {
        //        arr[mergedIndex] = L[leftIndex];
        //        leftIndex++;
        //        mergedIndex++;
        //    }

        //    // Copy remaining elements of R[] if any
        //    while (rightIndex < n2)
        //    {
        //        arr[mergedIndex] = R[rightIndex];
        //        rightIndex++;
        //        mergedIndex++;
        //    }
        //}

        static void MergeSort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                // Find the middle point
                int m = l + (r - l) / 2;

                // Sort first and second halves
                MergeSort(arr, l, m);
                MergeSort(arr, m + 1, r);

                // Merge the sorted halves
                Merge(arr, l, m, r);
            }
        }



        static void DisplayMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public void MatrixDemo()
        {
            //// Jagged Array
            //int[][]
            // 2d Array

            int square = 1;

            int[,] matrix = GenerateMatrix(square, square);

            sw.Start();
            DisplayMatrix(matrix);
            sw.Stop();

            Console.WriteLine("Elapsed Milli: " + sw.ElapsedMilliseconds);


            square = 10;

            matrix = GenerateMatrix(square, square);

            sw.Start();
            DisplayMatrix(matrix);
            sw.Stop();

            Console.WriteLine("Elapsed Milli: " + sw.ElapsedMilliseconds);

            square = 100;

            matrix = GenerateMatrix(square, square);

            sw.Start();
            DisplayMatrix(matrix);
            sw.Stop();

            Console.WriteLine("Elapsed Milli: " + sw.ElapsedMilliseconds);
        }

        static int[,] GenerateMatrix(int rows, int columns)
        {
            Random rand = new Random(); // Initialize a random number generator

            // Initialize the matrix with specified rows and columns
            int[,] matrix = new int[rows, columns];

            // Fill the matrix with random integer values
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = rand.Next(1, 100); // Generates random integers between 1 and 99
                }
            }

            return matrix;
        }


        public static void LinearExample()
        {
            // Big O Notation
            //  Constant: O(1)

            int[] values = { 1, 2, 3, 4, 5 };
            int add = 2 + 1;
            int constantValue = values[1];



            bool isRunning = true;

            while (isRunning)
            {

                //  Linear time: O(n)
                // n represents the number of elements in the collection
                // Worst Case Scinario: O(n) every element will be touched at least once

                // Start stopwatch for linear time measure

                int sum = 0;

                sw.Start();

                for (int i = 0; i < 5; i++)
                {
                    sum += i;
                }

                sw.Stop(); // Stop stopwatch

                Console.WriteLine("Elapsed Milliseconds: " + sw.ElapsedMilliseconds);

                sw.Restart();

                if (Console.ReadLine() == "e")
                {
                    isRunning = false;
                }
            }

            static double[,] MultiplyMatrix(double[,] A, double[,] B)
            {
                int rA = A.GetLength(0);
                int cA = A.GetLength(1);
                int rB = B.GetLength(0);
                int cB = B.GetLength(1);

                if (cA != rB)
                {
                    Console.WriteLine("Matrixes can't be multiplied!!");
                }
                else
                {
                    double temp = 0;
                    double[,] kHasil = new double[rA, cB];

                    for (int i = 0; i < rA; i++)
                    {
                        for (int j = 0; j < cB; j++)
                        {
                            temp = 0;
                            for (int k = 0; k < cA; k++)
                            {
                                temp += A[i, k] * B[k, j];
                            }
                            kHasil[i, j] = temp;
                        }
                    }

                    return kHasil;
                }
                return null;
            }




            //  Quadratic time: O(n ^ 2)
            //  Exponential time: O(2 ^ n)
            //  Factorial time: O(n!)

        } // Main


    } // class

} // namespace
