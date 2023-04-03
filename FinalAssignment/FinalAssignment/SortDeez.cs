using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalAssignment.Anime;

namespace FinalAssignment
{
    internal class SortDeez<T>
    {
        //BUBBLE SORT STACKS
        public void BubbleSort(StackBetter<T> stack, Func<T, IComparable> propertySelector)
        {
            Stack<T> tempStack = new Stack<T>();
            while (stack.Count > 0)
            {
                T current = stack.Pop();
                while (tempStack.Count > 0 && propertySelector(tempStack.Peek()).CompareTo(propertySelector(current)) > 0)
                {
                    stack.Push(tempStack.Pop());
                }
                tempStack.Push(current);
            }
            while (tempStack.Count > 0)
            {
                stack.Push(tempStack.Pop());
            }
        }

        //BUBBLE SORT ARRAYLIST
        public void BubbleSort(CoolerArrayList<T> array, Func<T, IComparable> propertySelector)
        {
            int num = array.Count();
            for (int i = 0; i < num - 1; i++)
                for (int j = 0; j < num - i - 1; j++)
                    if (propertySelector(array[j]).CompareTo(propertySelector(array[j + 1])) > 0)   
                    {
                        // swap temp and array[i]
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
        }

        //QUICKSORT ARRAYLIST
        public CoolerArrayList<T> QuickSort(CoolerArrayList<T> array, int leftIndex, int rightIndex, Func<T, IComparable> propertySelector)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];

            while (i <= j)
            {
                while (propertySelector(array[i]).CompareTo(propertySelector(pivot)) < 0)
                {
                    i++;
                }

                while (propertySelector(array[j]).CompareTo(propertySelector(pivot)) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    T temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                QuickSort(array, leftIndex, j, propertySelector);

            if (i < rightIndex)
                QuickSort(array, i, rightIndex, propertySelector);

            return array;
        }

        private int Partition(CoolerArrayList<T> array, int lowIndex, int highIndex, Func<T, IComparable> propertySelector)
        {
            var pivot = array[highIndex];
            int i = (lowIndex - 1);

            for(int j = lowIndex; j <= highIndex; j++)
            {
                if (propertySelector(array[j]).CompareTo(propertySelector(pivot)) < 0)
                {
                    i++;
                    var bufferJ = array[j];
                    array[i] = array[j];
                    array[j] = bufferJ;
                }
            }
            var buffer = array[highIndex];
            array[i + 1] = array[highIndex];
            array[highIndex] = buffer;
            return i + 1;
        }
    }
}
