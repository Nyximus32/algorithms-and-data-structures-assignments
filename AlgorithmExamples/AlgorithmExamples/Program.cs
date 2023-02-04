using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmExamples
{
    internal class Program
    {
        static string[] exampleStringArray =
        {
            "Netherlands",
            "Belgium",
            "France",
            "Bulgaria",
            "Romania",
            "Germany",
            "Poland",
            "Switzerland",
            "Hungary",
            "Italy",
            "Portugal",
            "Norway",
            "Sweden",
            "Iceland"
        };

        static int[] exampleIntArray =
        {
            100,
            201,
            12,
            134,
            532,
            23,
            1,
            234,
            532,
            122,
            764,
            467,
            835,
            521
        };

        static void Main(string[] args)
        {
            Console.WriteLine(OnExample(exampleStringArray, "Netherlands"));

            int[] mergedIntArray = MergeSort(exampleIntArray, 0, exampleIntArray.Length - 1);
            for(int i = 0;i < mergedIntArray.Length; i++)
            {
                Console.WriteLine(mergedIntArray[i]);
            }
            Console.ReadKey();

            ConstantTime(exampleStringArray);
            LogarithmicTime(exampleStringArray, "Bulgaria");
        }

        //O(N)
        //O(N) describes an algorithm whose performance will grow linearly and in direct proportion to
        //the size of the input data set. The example below also demonstrates how Big O favours the worst-case performance scenario;
        //a matching string could be found during any iteration of the for loop and the function would return early,
        //but Big O notation will always assume the upper limit where the algorithm will perform the maximum number of iterations.
        //https://robbell.io/2009/06/a-beginners-guide-to-big-o-notation
        static bool OnExample(IEnumerable<string> elements, string value)
        {
            foreach (var element in elements)
            {
                if (element == value) return true;
            }
            return false;
        }

        //OnLogn
        //O(nlogn), also known as loglinear complexity, implies that logn operations will occur n times.
        //It’s commonly used in recursive sorting algorithms and binary tree sorting algorithms
        //https://builtin.com/software-engineering-perspectives/nlogn
        //example
        //An example of an algorithm with this efficiency is merge sort,
        //which breaks up an array into two halves, sorts those two halves by recursively calling itself on them,
        //and then merging the result back into a single array. Because it splits the array in half each time,
        //the outer loop has an efficiency of logn, and for each “level” of the array that has been split up
        //(when the array is in two halves, then in quarters, and so forth),
        //it will have to merge together all of the elements, an operations that has order of n.
        //https://mytechnetknowhows.wordpress.com/2014/12/20/o-notation-with-c-csharp-my-take/
        static int[] MergeSort(int[] inputItems, int lowerBound, int upperBound)
        {
            if (lowerBound < upperBound)
            {
                int middle = (lowerBound + upperBound) / 2;

                MergeSort(inputItems, lowerBound, middle);
                MergeSort(inputItems, middle + 1, upperBound);

                //Merge
                int[] leftArray = new int[middle - lowerBound + 1];
                int[] rightArray = new int[upperBound - middle];

                Array.Copy(inputItems, lowerBound, leftArray, 0, middle - lowerBound + 1);
                Array.Copy(inputItems, middle + 1, rightArray, 0, upperBound - middle);

                int i = 0;
                int j = 0;
                for (int count = lowerBound; count < upperBound + 1; count++)
                {
                    if (i == leftArray.Length)
                    {
                        inputItems[count] = rightArray[j];
                        j++;
                    }
                    else if (j == rightArray.Length)
                    {
                        inputItems[count] = leftArray[i];
                        i++;
                    }
                    else if (leftArray[i] <= rightArray[j])
                    {
                        inputItems[count] = leftArray[i];
                        i++;
                    }
                    else
                    {
                        inputItems[count] = rightArray[j];
                        j++;
                    }
                }
            }
            return inputItems;
        }

        //It's always gonna take the same amount of time to display the country
        static void ConstantTime(string[] countries)
        {
            Console.WriteLine("The first county is: " + countries[0]);
        }

        //Basically a shorter version of O(n). Again, the amount of operations increases based on the amount of values, but at a much slower rate
        static void LogarithmicTime(string[] countries, string countryToFind)
        {
            int low = 0;
            int high = countries.Length - 1;
            int mid;
            int counter = 1;

            while (low <= high)
            {
                mid = (low + high) / 2;

                if (countries[mid].CompareTo(countryToFind) < 0)
                {
                    low = mid + 1;
                }
                else if (countries[mid].CompareTo(countryToFind) > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    Console.WriteLine("Times searched:" + counter);
                    return;
                }
                counter++;
            }
        }
    }
}
