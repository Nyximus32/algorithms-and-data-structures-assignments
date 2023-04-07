using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalAssignment
{
    public class CoolerArrayList<T> : ICollection<T>
    {
        private Object[] items;
        private int size;
        private static Object[] emptyArray = new Object[0];
        private readonly int defaultCapacity = 4;
        internal const int MaxArrayLength = 0X7FEFFFFF;

        //default - make an empty array
        public CoolerArrayList()
        {
            items = emptyArray;
        }

        //returns the size of the arrayList
        public int Count()
        {
            return size;
        }

        //adds an item to the array
        public void Add(T item)
        {
            if(size == items.Length)
            {
                EnsureCapacity(size + 1);
            }
            items[size] = item;
            size++;
        }

        //makes sure that when something is added that there is enough space for it in the list
        public void EnsureCapacity(int min)
        {
            if(items.Length < min)
            {
                int newCapacity = 0;
                if(items.Length == 0)
                {
                    newCapacity = defaultCapacity;
                } else
                {
                    newCapacity = items.Length * 2;
                }
                if(newCapacity > MaxArrayLength)
                {
                    newCapacity = MaxArrayLength;
                }
                if(newCapacity < min)
                {
                    newCapacity = min;
                }
                Capacity = newCapacity;
            }
        }

        //changes the size of the list by making a new one with more size
        public int Capacity
        {
            //returns the length of the arraylist
            get
            {
                return items.Length;
            }
            set
            {
                //if the value is smaller than the original size throw an error is not possible
                if(value < size)
                {
                    throw new ArgumentOutOfRangeException("value too small");
                }
                //if the value is not the same size as the arraylist make a new arrayList with the
                //changed value is the size and copy the items into the new array and turn the items array
                //into the new array
                if(value != items.Length)
                {
                    if(value > 0)
                    {
                        Object[] newItems = new object[value];
                        if(size > 0)
                        {
                            Array.Copy(items ,0, newItems, 0, size);
                        }
                        items = newItems;
                    } else
                    {
                        items = new object[defaultCapacity];
                    }
                }
            }
        }

        int ICollection<T>.Count { get { return size; } }

        public bool IsReadOnly { get { return false; } }

        //finds the index of the first occurence of a value
        public int IndexOf(T value)
        {
            return Array.IndexOf((Array) items, value, 0, size);
        }

        //remove the element at a given index
        public void RemoveAt(int index)
        {
            //checks if index is within range of the Arraylist
            if(index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("Not in range of the ArrayList");
            }
            size--;

            //shifts remaining elements one down the list
            if(index < size)
            {
                Array.Copy(items, index + 1, items, index, size - index);
            }

            //last element set to null so it can be garbage collected
            items[size] = null;
        }

        public void Clear()
        {
            items = null;
            size = 0;
        }

        public bool Contains(T item)
        {
            if (item == null)
            {
                //looks for the first null value and if it finds one return true else false
                for(int i = 0; i < size; i++)
                    if (items[i] == null)
                    {
                        return true;
                    }
                return false;
            } else
            {
                //looks for value and checks if they are equal if true return true else false
                for(int i = 0; i < size; i++)
                    if((items[i] != null) && (items[i].Equals(item)))
                    {
                        return true;
                    }
                return false;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if((array != null) && (array.Rank != 1))
            {
                throw new ArgumentException("array is empty or has more than 1 dimension");
            }
            //copies array list into array of compatible type
            Array.Copy(items, 0, array, arrayIndex, size);
        }

        //removes the object by finding its index and removing that index
        bool ICollection<T>.Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get
            {
                return (T)items[index];
            }
            set
            {
                items[index] = value;
            }
        }

    }
}
