using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class SearchDeez
    {
        public SearchDeez() { }

        public T SearchFor<T>(CoolerArrayList<T> arrayList, T searchable, Func<T, IComparable> propertySelector)
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


        public Anime SearchFor(StackBetter<Anime> stack, string title)
        {
            while (stack.Count > 0)
            {
                if (title == stack.Peek().Title)
                {
                    return stack.Pop();
                }
                stack.Pop();
            }
            return null;
        }
    }
}
