using System;

namespace FinalAssignment
{
    internal class SkipList<T> where T : IComparable
    {
        public class Node
        {
            public Node[] Next { get; private set; }
            public T Element { get; private set; }

            public Node(T element, int level)
            {
                Element = element;
                Next = new Node[level];
            }
        }
        private int _levels = 0;
        private Node _head = new Node(default, 20);

        public int GenerateRandomLevel()
        {
            Random _rand = new Random();
            int level = 0;
            for (int R = _rand.Next(); (R & 1) == 1; R >>= 1)
            {
                level++;
                if (level == _levels)
                {
                    _levels++; break;
                }
            }
            return level;
        }

        public Node CreateNode(T element, int level)
        {
            Node n = new Node(element, level);
            return n;
        }

        public string Insert(T element)
        {
            Node current = _head;



            Node[] update = new Node[21];

            for (int i = _levels; i >= 0; i--)
            {
                for (; current.Next[i] != null; current = current.Next[i])
                {
                    current = current.Next[i];
                }
                update[i] = current;
            }

            current = current.Next[0];

            if (current == null || current.Element.CompareTo(element) != 0)
            {
                int rlevel = GenerateRandomLevel();

                if (rlevel > _levels)
                {
                    for (int i = _levels + 1; i < rlevel + 1; i++)
                    {
                        update[i] = _head;
                    }

                    _levels = rlevel;
                }

                Node newNode = CreateNode(element, rlevel);

                for (int i = _levels - 1; i >= 0; i--)
                {
                    newNode.Next[i] = update[i].Next[i];
                    update[i].Next[i] = newNode;
                }
                return ("Successfully Inserted key "
                                  + element);
            }
            return "Nope";
        }
    }
}