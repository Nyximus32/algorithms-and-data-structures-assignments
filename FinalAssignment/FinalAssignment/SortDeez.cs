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
                while (propertySelector(array[i]) != null && propertySelector(array[i]).CompareTo(propertySelector(pivot)) < 0)
                {
                    i++;
                }

                while (propertySelector(array[j]) != null && propertySelector(array[j]).CompareTo(propertySelector(pivot)) > 0)
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

        //Quicksort Stack
        public StackBetter<T> QuickSort(StackBetter<T> stack, Func<T, IComparable> propertySelector)
        {
            if (stack.Count <= 1)
                return stack;

            T pivot = stack.Pop();

            StackBetter<T> leftStack = new StackBetter<T>();
            StackBetter<T> rightStack = new StackBetter<T>();

            while (stack.Count > 0)
            {
                T element = stack.Pop();
                if (propertySelector(element).CompareTo(propertySelector(pivot)) <= 0)
                    leftStack.Push(element);
                else
                    rightStack.Push(element);
            }

            leftStack = QuickSort(leftStack, propertySelector);
            rightStack = QuickSort(rightStack, propertySelector);

            //Merge the sorted stacks into the temporary array
            T[] temp = new T[leftStack.Count + rightStack.Count + 1];
            int index = 0;
            while (leftStack.Count > 0)
                temp[index++] = leftStack.Pop();
            temp[index++] = pivot;
            while (rightStack.Count > 0)
                temp[index++] = rightStack.Pop();

            //Push the sorted elements back onto the stack
            for (int i = temp.Length - 1; i >= 0; i--)
                stack.Push(temp[i]);

            return stack;
        }
    }
}
