using System;
using System.Collections.Generic;

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
    }
}
