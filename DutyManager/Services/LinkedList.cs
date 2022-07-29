using System;
using System.Collections;
using System.Collections.Generic;

namespace DutyManager.Services
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public Node<T> Head;
        public Node<T> Tail;
        public int CountNode;

        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (Head == null)
                Head = node;
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }
            Tail = node;
            CountNode++;
        }
        public void AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            Node<T> temp = Head;
            node.Next = temp;
            Head = node;
            if (CountNode == 0)
                Tail = Head;
            else
                temp.Previous = node;
            CountNode++;
        }

        public bool Remove(T data)
        {
            Node<T> current = Head;

            while (current != null)
            {
                if (current.Data.Equals(data))
                    break;
                current = current.Next;
            }
            if (current != null)
            {
                if (current.Next != null)
                    current.Next.Previous = current.Previous;
                else
                    Tail = current.Previous;

                if (current.Previous != null)
                    current.Previous.Next = current.Next;
                else
                    Head = current.Next;
                CountNode--;
                return true;
            }
            return false;
        }

        public int Count { get { return CountNode; } }
        public bool IsEmpty { get { return CountNode == 0; } }
        public void Clear()
        {
            Head = null;
            Tail = null;
            CountNode = 0;
        }

        public bool Contains(T data)
        {
            Node<T> current = Head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        public IEnumerable<T> BackEnumerator()
        {
            Node<T> current = Tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
    }
}