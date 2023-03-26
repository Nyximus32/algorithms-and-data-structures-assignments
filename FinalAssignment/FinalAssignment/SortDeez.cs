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

        public void QuickSort(CoolerArrayList<Anime> array, int lowIndex, int highIndex)
        {
            if(lowIndex < highIndex)
            {
                int pi = Partition(array, lowIndex, highIndex);
                QuickSort(array, lowIndex, pi - 1);
                QuickSort(array, pi + 1, highIndex);
            }
        }

        private int Partition(CoolerArrayList<Anime> array, int lowIndex, int highIndex)
        {
            var pivot = array[highIndex];
            int i = (lowIndex - 1);

            for(int j = lowIndex; j <= highIndex; j++)
            {
                AnimePremieredComparer comparer = new AnimePremieredComparer();
                if (comparer.Compare(array[j], pivot) < 0)
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
