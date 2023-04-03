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

                // If the element is present at the
                // middle itself
                if (propertySelector(arrayList[mid]).CompareTo(propertySelector(searchable)) == 0)
                {
                    return mid;
                }

                // If element is smaller than mid, then
                // it can only be present in left subarray
                if (propertySelector(arrayList[mid]).CompareTo(propertySelector(searchable)) > 0)
                    return binarySearch(arrayList, low, mid - 1, searchable, propertySelector);

                // Else the element can only be present
                // in right subarray
                return binarySearch(arrayList, mid + 1, high, searchable, propertySelector);
            }

            // We reach here when element is not present
            // in array
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
