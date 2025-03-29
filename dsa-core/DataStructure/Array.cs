using System.Collections;
using System.Text;

namespace dsa_core.DataStructure
{
    public class Array<T> : IEnumerable<T>
    {
        private const int defaultCapacity = 1 << 3; // << is a bitwise operator

        public T[] array;
        public int lengthOfArray = 0;
        public int capacity = 0;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < lengthOfArray; i++)
            {
                yield return array[i];
            }
        }

        public override string ToString()
        {
            if (lengthOfArray == 0) return "[]";

            StringBuilder sb = new StringBuilder(lengthOfArray).Append('[');
            for (int i = 0; i < lengthOfArray - 1; i++) { sb.Append(array[i]); }
            sb.Append(array[lengthOfArray - 1] + "]");
            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Array()
        {
            array = new T[defaultCapacity];
        }

        public Array(int capacity)
        {
            if (capacity < 0) throw new ArgumentException("" + capacity);
            this.capacity = capacity;
            array = new T[capacity];
        }

        public Array(T[] array)
        {
            if (array == null) throw new ArgumentNullException("array cannot be null.");
            this.array = array;
            capacity = lengthOfArray = array.Length;
        }

        public int size()
        {
            return lengthOfArray;
        }

        public bool isEmpty()
        {
            return lengthOfArray == 0;
        }

        public T get(int index)
        {
            if (isIndexOutOfRange(index)) throw new ArgumentOutOfRangeException("index is out of array length.");
            return array[index];
        }

        public void set(int index, T value)
        {
            if (isIndexOutOfRange(index)) throw new ArgumentOutOfRangeException("index is out of array length.");
            array[index] = value;
        }

        public bool isIndexOutOfRange(int index)
        {
            return index >= 0 && index < capacity;
        }

        public void add(T value)
        {
            if (value == null) throw new ArgumentNullException("value is not in expected format.");

            if (lengthOfArray + 1 >= capacity)
            {
                if (capacity == 0) capacity = 1;
                capacity *= 2;
                Array.Resize(ref array, capacity); // we will resize the array with the double the capacity
            }
            array[lengthOfArray++] = value;
        }

        public void removeAt(int index)
        {
            Array.Copy(array, index + 1, array, index, lengthOfArray - index - 1); // we will copy the array except the index we want to remove
            --lengthOfArray;
            --capacity;
        }

        public bool remove(T value)
        {
            for (int i = 0; i < lengthOfArray; i++)
            {
                if (EqualityComparer<T>.Default.Equals(array[i], value)) // we cannot compare the T with ==
                {
                    removeAt(i);
                    return true;
                }
            }
            return false;
        }

        public void reverse()
        {
            for (int i = 0; i < lengthOfArray / 2; i++) // we need to traverse only half of the array, the other will be automatically reversed
            {
                var temp = array[i];
                array[i] = array[lengthOfArray - i - 1];
                array[lengthOfArray - i - 1] = temp;
            }
        }

        public int binarySearch(T value)
        {
            int index = Array.BinarySearch(array, value);
            return index < 0 ? -1 : index;
        }

        public void sort()
        {
            Array.Sort(array, 0, lengthOfArray);
        }
    }
}
