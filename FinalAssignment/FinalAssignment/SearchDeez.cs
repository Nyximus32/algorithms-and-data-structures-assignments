using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class SearchDeez<T>
    {
        public int BinarySearch(CoolerArrayList<T> arrayList, int low, int high, T searchable, Func<T, IComparable> propertySelector)
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
                    return BinarySearch(arrayList, low, mid - 1, searchable, propertySelector);

                //Else the element can only be present
                //in right subarray
                return BinarySearch(arrayList, mid + 1, high, searchable, propertySelector);
            }

            //We reach here when element is not present
            //in array
            return -1;
        }

        public int BinarySearch(LinkedListBeyond<T> linkedList, int low, int high, T searchable, Func<T, IComparable> propertySelector)
        {
            if (high >= low)
            {
                int mid = low + (high - low) / 2;

                // Get the middle element of the linked list
                LinkedListBeyond<T>.Node current = linkedList.GetHead();
                for (int i = 0; i < mid; i++)
                {
                    current = current.Next;
                }

                //If the element is present at the
                //middle itself
                if (propertySelector(current.Value).CompareTo(propertySelector(searchable)) == 0)
                {
                    return mid;
                }

                //If element is smaller than mid, then
                //it can only be present in left subarray
                if (propertySelector(current.Value).CompareTo(propertySelector(searchable)) > 0)
                    return BinarySearch(linkedList, low, mid - 1, searchable, propertySelector);

                //Else the element can only be present
                //in right subarray
                return BinarySearch(linkedList, mid + 1, high, searchable, propertySelector);
            }

            //We reach here when element is not present
            //in array
            return -1;
        }

        public int BinarySearch(StackBetter<T> stack, int low, int high, Func<T, IComparable> propertySelector, T searchable)
        {
            if (high >= low && stack.Count > 0)
            {
                int mid = low + (high - low) / 2;

                // Get the middle element of the stack
                StackBetter<T> tempStack = new StackBetter<T>();
                for (int i = 0; i < mid; i++)
                {
                    tempStack.Push(stack.Pop());
                }
                T middleElement = stack.Peek();
                while (tempStack.Count > 0)
                {
                    stack.Push(tempStack.Pop());
                }

                // If the element is the middle element
                if (propertySelector(middleElement).CompareTo(propertySelector(searchable)) == 0)
                {
                    // Calculate the index of the found element in the original stack
                    int index = stack.Count - mid - 1;
                    return index;
                }

                // If element is smaller than mid, then it can only be present in left subarray
                if (propertySelector(middleElement).CompareTo(propertySelector(searchable)) > 0)
                {
                    // Recursively search the left subarray
                    return BinarySearch(stack, low, mid - 1, propertySelector, searchable);
                }

                // Else the element can only be present in right subarray
                // Recursively search the right subarray
                return BinarySearch(stack, mid + 1, high, propertySelector, searchable);
            }

            // Element doesn't exist in stack
            return -1;
        }


        public T LinearSearch(CoolerArrayList<T> arrayList, T searchable, Func<T, IComparable> propertySelector)
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

        
        public T LinearSearch(StackBetter<T> stack, T searchable, Func<T, IComparable> propertySelector)
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

        public T LinearSearch(LinkedListBeyond<T> linkedList, T searchable, Func<T, IComparable> propertySelector)
        {
            LinkedListBeyond<T>.Node current = linkedList.GetHead();
            while (current != null)
            {
                if (propertySelector(searchable).CompareTo(propertySelector(current.Value)) == 0)
                {
                    return current.Value;
                }
                current = current.Next;
            }
            return default;
        }
    }
}
