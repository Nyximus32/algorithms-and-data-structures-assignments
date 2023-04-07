using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment
{
    internal class LinkedListBeyond<T>
    {
        public class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }
            public Node(T value)
            {
                Value = value;
                Next = null;
                Previous = null;
            }
        }

        private Node _head;

        public LinkedListBeyond()
        {
            _head = null;
        }

        public void Add(T value)
        {
            AddLast(value);
        }

        public void AddFirst(T value)
        {
            Node temp = new Node(value)
            {
                Next = _head
            };
            _head = temp;
        }

        public void AddLast(T value)
        {
            Node temp = new Node(value);
            if (_head == null)
            {
                _head = temp;
            }
            else
            {
                Node current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = temp;
                temp.Previous = current;
            }
        }

        public void AddAfter(T value, T newValue)
        {
            Node temp = new Node(newValue);
            Node current = _head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    temp.Next = current.Next;
                    current.Next = temp;
                    temp.Previous = current;
                    if (temp.Next != null)
                    {
                        temp.Next.Previous = temp;
                    }
                    break;
                }
                current = current.Next;
            }
        }

        public void AddBefore(T value, T newValue)
        {
            Node temp = new Node(newValue);
            Node current = _head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    temp.Next = current;
                    temp.Previous = current.Previous;
                    current.Previous = temp;
                    if (temp.Previous != null)
                    {
                        temp.Previous.Next = temp;
                    }
                    else
                    {
                        _head = temp;
                    }
                    break;
                }
                current = current.Next;
            }
        }

        public void RemoveFirst()
        {
            if (_head != null)
            {
                _head = _head.Next;
                if (_head != null)
                {
                    _head.Previous = null;
                }
            }
        }

        public void RemoveLast()
        {
            if (_head != null)
            {
                if (_head.Next == null)
                {
                    _head = null;
                }
                else
                {
                    Node current = _head;
                    while (current.Next.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = null;
                }
            }
        }

        public void Remove(T value)
        {
            if (_head != null)
            {
                if (_head.Value.Equals(value))
                {
                    _head = _head.Next;
                    if (_head != null)
                    {
                        _head.Previous = null;
                    }
                }
                else
                {
                    Node current = _head;
                    while (current.Next != null)
                    {
                        if (current.Next.Value.Equals(value))
                        {
                            current.Next = current.Next.Next;
                            if (current.Next != null)
                            {
                                current.Next.Previous = current;
                            }
                            break;
                        }
                        current = current.Next;
                    }
                }
            }
        }

        public void Clear()
        {
            _head = null;
        }

        public bool Contains(T value)
        {
            Node current = _head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public int Count
        {
            get
            {
                int count = 0;
                Node current = _head;
                while (current != null)
                {
                    count++;
                    current = current.Next;
                }
                return count;
            }
        }

        public T Find(T value)
        {
            Node current = _head;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    return current.Value;
                }
                current = current.Next;
            }
            return default;
        }

        public Node GetHead()
        {
            return _head;
        }
    }
}
