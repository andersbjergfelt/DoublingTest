using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DoublingTest
{
    /*
     * 
     * Doubling test. W rite a client that performs a doubling test for sort algorithms. 
     Start at N equal to 1000, and print N, the predicted number of seconds, the actual number of seconds, and the ratio as N doubles.
      Use your program to validate that insertion sort and selection sort are quadratic for random inputs, and formulate and test a hypothesis for mergesort. 
     * 
     */
    internal class Program
    {
        private const int MaxInt = 1000000;
        private static readonly Random rand = new Random();

        private static void Main(string[] args)
        {
           
            var previousR = TimeT(512);
           
            for (var n = 1000;; n += n)
            {
                var time = TimeT(n);
                var predicted = time * 2;
                Console.WriteLine("Size: " + n + " Predicted " + predicted +  " Time = " + time + " Ratio: " + time / previousR);
                previousR = time;
            }
        }


        public static double TimeT(int n)
        {
            var a = new int[n];

            for (var i = 0; i < n; i++)
                a[i] = rand.Next(-MaxInt, MaxInt);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            MergeSort(a);
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        private static int[] PerformInsertionSort(int[] array)
        {
            var length = array.Length;

            for (var i = 1; i < length; i++)
            {
                var j = i;

                while (j > 0 && array[j] < array[j - 1])
                {
                    var k = j - 1;
                    var temp = array[k];
                    array[k] = array[j];
                    array[j] = temp;

                    j--;
                }
            }
            return array;
        }


        private static int[] selectSort(int[] arr)
        {
            
            int pos_min, temp;

            for (var i = 0; i < arr.Length - 1; i++)
            {
                pos_min = i; 

                for (var j = i + 1; j < arr.Length; j++)
                    if (arr[j] < arr[pos_min])
                        pos_min = j;

               
                if (pos_min != i)
                {
                    temp = arr[i];
                    arr[i] = arr[pos_min];
                    arr[pos_min] = temp;
                }
            }
            return arr;
        }

        public static int[] MergeSort(int[] array)
        {
            
            if (array.Length <= 1)
                return array;

           
            var middleIndex = array.Length / 2;
            var left = new int[middleIndex];
            var right = new int[array.Length - middleIndex];

            Array.Copy(array, left, middleIndex);
            Array.Copy(array, middleIndex, right, 0, right.Length);

          
            left = MergeSort(left);
            right = MergeSort(right);

            
            return Merge(left, right);
        }

        public static int[] Merge(int[] left, int[] right)
        {
          
            var leftList = left.OfType<int>().ToList();
            var rightList = right.OfType<int>().ToList();
            var resultList = new List<int>();

          
            while (leftList.Count > 0 || rightList.Count > 0)
                if (leftList.Count > 0 && rightList.Count > 0)
                {
                   
                    if (leftList[0] <= rightList[0])
                    {
                        resultList.Add(leftList[0]);
                        leftList.RemoveAt(0);
                    }

                    else
                    {
                        resultList.Add(rightList[0]);
                        rightList.RemoveAt(0);
                    }
                }

                else if (leftList.Count > 0)
                {
                    resultList.Add(leftList[0]);
                    leftList.RemoveAt(0);
                }

                else if (rightList.Count > 0)
                {
                    resultList.Add(rightList[0]);
                    rightList.RemoveAt(0);
                }

           
            var result = resultList.ToArray();
            return result;
        }
    }
}
