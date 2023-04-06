using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    public class StackBetter<T>
    {
        private T[] _array;
        private int _size;

        public StackBetter()
        {
            _array = new T[10];
            _size = 0;
        }

        public StackBetter(StackBetter<T> stackBetter)
        {
            _array = stackBetter._array;
            _size = stackBetter._size;
        }

        public int Count
        {
            get { return _size; }
        }

        public void Push(T value)
        {
            if (_size == _array.Length) //If max size reached
            {
                //Create new array with double the size
                T[] temp = new T[_array.Length * 2];
                //Move elements from old array to new one
                for (int i = 0; i < _array.Length; i++) 
                {
                    temp[i] = _array[i];
                }
                _array = temp;
            }
            //Otherwise put the value in the next available spot
            _array[_size] = value;
            _size++;
        }

        public T Pop()
        {
            //Cant remove if empty
            if (_size == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            //Set the last element to null or 0
            T result = _array[--_size];
            _array[_size] = default;
            return result;
        }

        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            return _array[_size - 1];
        }

        //reverses the stack
        internal StackBetter<T> Reverse()
        {
            StackBetter<T> reversedStack = new StackBetter<T>();

            while (this.Count > 0)
            {
                reversedStack.Push(this.Pop());
            }

            return reversedStack;
        }
    }
}
