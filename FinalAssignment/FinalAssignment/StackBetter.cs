using System;

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

        public StackBetter(int initialCapacity)
        {
            if (initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException("Specify a positive number");
            }

            if (initialCapacity < 10)
            {
                initialCapacity = 10;
            }

            _array = new T[initialCapacity];
            _size = 0;
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

        public virtual object Clone()
        {
            StackBetter<T> stack = new StackBetter<T>(_size)
            {
                _size = _size
            };
            Array.Copy(_array, 0, stack._array, 0, _size);
            return stack;
        }
    }
}
