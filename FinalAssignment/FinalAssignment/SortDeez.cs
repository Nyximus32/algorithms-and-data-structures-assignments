using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                        (array[j + 1], array[j]) = (array[j], array[j + 1]);
                    }
        }

        public void BubbleSort(LinkedListBeyond<T> list, Func<T, IComparable> propertySelector)
        {
            int count = list.Count;
            if (count <= 1)
            {
                return;
            }

            bool swapped;
            do
            {
                swapped = false;
                var current = list.GetHead();

                for (int i = 0; i < count - 1; i++)
                {
                    if (propertySelector(current.Value).CompareTo(propertySelector(current.Next.Value)) > 0)
                    {
                        (current.Next.Value, current.Value) = (current.Value, current.Next.Value);
                        swapped = true;
                    }
                    current = current.Next;
                }
                count--;
            }
            while (swapped);
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
                    (array[j], array[i]) = (array[i], array[j]);
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

        public LinkedListBeyond<T> QuickSort(LinkedListBeyond<T> list, Func<T, IComparable> propertySelector)
        {
            if (list == null || list.Count <= 1)
            {
                return list;
            }

            var pivot = list.GetHead().Value;
            var leftList = new LinkedListBeyond<T>();
            var rightList = new LinkedListBeyond<T>();

            for (var current = list.GetHead().Next; current != null; current = current.Next)
            {
                var currentValue = current.Value;
                if (propertySelector(currentValue).CompareTo(propertySelector(pivot)) < 0)
                {
                    leftList.AddLast(currentValue);
                }
                else
                {
                    rightList.AddLast(currentValue);
                }
            }
            
            leftList = QuickSort(leftList, propertySelector);
            rightList = QuickSort(rightList, propertySelector);

            var result = new LinkedListBeyond<T>();
            LinkedListBeyond<T>.Node currentNodeL = leftList.GetHead();
            while (currentNodeL != null)
            {
                result.AddLast(currentNodeL.Value);
                currentNodeL = currentNodeL.Next;
            }
            result.AddLast(pivot);
            LinkedListBeyond<T>.Node currentNodeR = rightList.GetHead();
            while (currentNodeR != null)
            {
                result.AddLast(currentNodeR.Value);
                currentNodeR = currentNodeR.Next;
            }

            return result;
        }

    }
}
