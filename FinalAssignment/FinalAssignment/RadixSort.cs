using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    public class RadixSort<T> where T : ICollection
    {
        T list = default;

        public RadixSort(T list)
        {
            this.list = list;
        }

        public void CountSort(int[] arr, int exp)
        {
            int n = arr.Length;
            int[] output = new int[n];
            int[] count = new int[10];
        }

        private int findMaximumElement(IComparable[] values)
        {
            //checks if the values that will be checked are numbers or letters
            if (values.GetType() == typeof(int))
            {
                return (int)(Math.Log10((int)values.Max()) + 1);
            } else if(values.GetType() == typeof(string))
            {
                string max = (string)values[0];

                for (int i = 1; i < values.Length; i++)
                {
                    if (values[i].CompareTo(max) > 0)
                    {
                        max = (string)values[i];
                    }
                }
                return max.Length;
            }
            return 0;
        }
    }
}
