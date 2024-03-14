using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_224_W24_BigO_031324
{
    internal class LogDemo
    {
        static int[] GenerateSortedArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i;
            }
            return array;
        }

        static int LinearSearch(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                    return i;
            }
            return -1;
        }

        static int BinarySearch(int[] array, int target)
        {
            int low = 0;
            int high = array.Length - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (array[mid] == target)
                    return mid;

                if (array[mid] < target)
                    low = mid + 1;
                else
                    high = mid - 1;
            }

            return -1;
        }
    }
}

