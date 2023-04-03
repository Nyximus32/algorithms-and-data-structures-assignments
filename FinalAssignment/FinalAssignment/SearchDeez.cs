using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class SearchDeez<T>
    {
        public SearchDeez() { }

        public int binarySearch(CoolerArrayList<Anime> arrayList, int low, int high, string x, Func<T, IComparable> propertySelector)
        {
            if (high >= low)
            {
                int mid = low + (high - low) / 2;

                // If the element is present at the
                // middle itself
                if (propertySelector(arrayList[mid]).CompareTo(x) == 0)
                {
                    return mid;
                }

                // If element is smaller than mid, then
                // it can only be present in left subarray
                if (propertySelector(arrayList[mid].).CompareTo(x) > x)
                    return binarySearch(arrayList, low, mid - 1, x, propertySelector);

                // Else the element can only be present
                // in right subarray
                return binarySearch(arrayList, mid + 1, high, x, propertySelector);
            }

            // We reach here when element is not present
            // in array
            return -1;
        }

        public Anime SearchFor(CoolerArrayList<Anime> arrayList, string title)
        {
            for(int i = 0; i <= arrayList.Count(); i++)
            {
                if(title == arrayList[i].Title)
                {
                    return arrayList[i];
                }
            }
            return null;
        }
    }
}
