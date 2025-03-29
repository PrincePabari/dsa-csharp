using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsa_core.DataStructure
{
    public class DoublyLinkedList<T> : IEnumerator<T>
    {

        private int size = 0;
        private Node<T> head = null;
        private Node<T> tail = null;

        private class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Prev { get; set; }
            public Node<T> Next { get; set; }

            public Node(T data, Node<T> prev, Node<T> next)
            {
                Data = data;
                Prev = prev;
                Next = next;
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }

        // Empty the linked list, O(n)
        public void Clear()
        {
            Node<T> trav = head;

            while (trav != null)
            {
                Node<T> next = trav.Next;
                trav.Prev = trav.Next = null;
                trav.Data = default(T);
                trav = next;
            }
            head = tail = trav = null;
            size = 0;
        }

        public int Size()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public void Add(T data)
        {
            AddLast(data);
        }

        private void AddLast(T data)
        {
            if (IsEmpty()) head = tail = new Node<T>(data, null, null);
            else
            {
                tail.Prev = new Node<T>(data, tail, null);
                tail = tail.Next;
            }
            size++;
        }

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
