using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class SearchDeez<T>
    {
        public int binarySearch(CoolerArrayList<T> arrayList, int low, int high, T searchable, Func<T, IComparable> propertySelector)
        {
            if (high >= low)
            {
                int mid = low + (high - low) / 2;

                //If the element is present at the
                //middle itself
                if (propertySelector(arrayList[mid]).CompareTo(propertySelector(searchable)) == 0)
                {
                    return mid;
                }

                //If element is smaller than mid, then
                //it can only be present in left subarray
                if (propertySelector(arrayList[mid]).CompareTo(propertySelector(searchable)) > 0)
                    return binarySearch(arrayList, low, mid - 1, searchable, propertySelector);

                //Else the element can only be present
                //in right subarray
                return binarySearch(arrayList, mid + 1, high, searchable, propertySelector);
            }

            //We reach here when element is not present
            //in array
            return -1;
        }

        public int binarySearch(StackBetter<T> stack, int low, int high, Func<T, IComparable> propertySelector, T searchable)
        {
            if (high >= low)
            {
                int mid = low + (high - low) / 2;

                //If the element is the middle element
                if (propertySelector(stack.Peek()).CompareTo(propertySelector(searchable)) == 0)
                {
                    return mid;
                }

                //If element smaller than mid then its on the left
                if (propertySelector(stack.Peek()).CompareTo(propertySelector(searchable)) > 0)
                {
                    //Pop the top element and recursively search the left subarray
                    stack.Pop();
                    int result = binarySearch(stack, low, mid - 1, propertySelector, searchable);
                    stack.Push(searchable); // Push the top element back onto the stack
                    return result;
                }

                //Else the element on the right
                //Pop top element and search the right subarray
                stack.Pop();
                int result2 = binarySearch(stack, mid + 1, high, propertySelector, searchable);
                //Put the top element back
                stack.Push(searchable); 
                return result2;
            }

            //Element doesnt exist in stack
            return -1;
        }

        public T SearchFor(CoolerArrayList<T> arrayList, T searchable, Func<T, IComparable> propertySelector)
        {
            for (int i = 0; i <= arrayList.Count(); i++)
            {
                if (propertySelector(searchable).CompareTo(propertySelector(arrayList[i])) == 0)
                {
                    return arrayList[i];
                }
            }
            return default;
        }

        
        public T SearchFor(StackBetter<T> stack, T searchable, Func<T, IComparable> propertySelector)
        {
            while (stack.Count > 0)
            {
                if (propertySelector(searchable).CompareTo(propertySelector(stack.Peek())) == 0)
                {
                    return stack.Pop();
                }
                stack.Pop();
            }
            return default;
        }
    }
}
