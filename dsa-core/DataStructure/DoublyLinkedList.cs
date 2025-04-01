using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsa_core.DataStructure
{
    public class DoublyLinkedList<T> : IEnumerable<T>
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
            if (IsEmpty())
                head = tail = new Node<T>(data, null, null);

            tail.Prev = new Node<T>(data, tail, null);
            tail = tail.Next;
            size++;
        }

        public void AddFirst(T data)
        {
            if (IsEmpty())
                head = tail = new Node<T>(data, null, null);

            head.Prev = new Node<T>(data, null, head);
            head = head.Prev;
            size++;
        }

        public void AddAt(int index, T data)
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("index out of range.");

            if (index == 0)
            {
                AddFirst(data);
                return;
            }
            if (index == size)
            {
                AddLast(data);
                return;
            }

            Node<T> temp = head;
            for (int i = 0; i < index - 1; i++)
            {
                temp = temp.Next;
            }
            Node<T> newNode = new Node<T>(data, temp, temp.Next);
            temp.Next.Prev = newNode;
            temp.Next = newNode;

            size++;
        }

        public T PeekFirst()
        {
            if (IsEmpty()) throw new InvalidOperationException("Empty List");

            return head.Data;
        }

        public T PeekLast()
        {
            if (IsEmpty()) throw new InvalidOperationException("Empty List");

            return tail.Data;
        }

        public T removeFirst()
        {
            if (IsEmpty()) throw new InvalidOperationException("Empty List");

            // Extract the data at the head and reassign the head to next node
            T data = head.Data;
            head = head.Next; // this can be null if head == tail
            --size;

            //check if the list is empty, set the tail to null
            if (IsEmpty()) tail = null;

            // clean the memory cleanup of the previous node
            else head.Prev = null;

            // return the extracted data
            return data;
        }

        public T removeLast()
        {
            if (IsEmpty()) throw new InvalidOperationException("Empty List");

            // Extract the data at the end and reassign the tail to the previous node
            T data = tail.Data;
            tail = tail.Prev; // this can be null if tail == head
            --size;

            // check if the list is empty, set head to null
            if (IsEmpty()) head = null;

            // clean the memory cleanup of the next node
            else tail.Next = null;

            // return the extracted data
            return data;
        }

        private T remove(Node<T> node)
        {
            // If the node to remove is either at the start or end of the list then handle independently
            if (node.Prev == null) return removeFirst();
            if (node.Next == null) return removeLast();

            // Make the pointers of adjacent nodes skip over 'node'
            node.Next.Prev = node.Prev;
            node.Prev.Next = node.Next;

            // Extract data from node
            T data = node.Data;

            // Memory Cleanup
            node.Data = default(T);
            node = node.Prev = node.Next = null;

            --size;

            return data;
        }

        private T removeAt(int index)
        {
            if (index < 0 || index >= size) throw new IndexOutOfRangeException("index out of range.");

            int i;
            Node<T> trav;

            if (index < size / 2)
            {
                for (i = 0, trav = head; i != index; i++)
                {
                    trav = trav.Next;
                }
            }
            else
            {
                for (i = size - 1, trav = tail; i != index; i--)
                {
                    trav = trav.Prev;
                }
            }

            return remove(trav);
        }

        public bool remove(object obj)
        {
            Node<T> trav = head;

            if (obj == null)
            {
                for (trav = head; trav != null; trav = trav.Next)
                {
                    if (trav.Data == null)
                    {
                        remove(trav);
                        return true;
                    }
                }
            }
            else
            {
                for (trav = head; trav != null; trav = trav.Next)
                {
                    if (obj.Equals(trav.Data))
                    {
                        remove(trav);
                        return true;
                    }
                }
            }
            return false;
        }

        public int indexOf(object obj)
        {
            int index = 0;
            Node<T> trav = head;

            if (obj == null)
            {
                for (; trav != null; trav = trav.Next, index++)
                {
                    if (trav.Data == null)
                    {
                        return index;
                    }
                }
            }
            else
            {
                for (; trav != null; trav = trav.Next, index++)
                {
                    if (obj.Equals(trav.Data))
                    {
                        return index;
                    }
                }
            }
            return -1;
        }

        public bool Contains(object obj)
        {
            return indexOf(obj) != -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> trav = head;

            while (trav != null)
            {
                yield return trav.Data;
                trav = trav.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");

            Node<T> trav = head;
            while (trav != null)
            {
                sb.Append(trav.Data);
                if (trav.Next != null)
                {
                    sb.Append(", ");
                }
                trav = trav.Next;
            }
            sb.Append(" ]");
            return sb.ToString();
        }
    }
}
