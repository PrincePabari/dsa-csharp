using System.Collections;

namespace dsa_core
{
    public class Array<T> : IEnumerable<T>
    {
        private const int defaultCapacity = 1 << 3;

        public T[] array;
        public int lengthOfArray = 0;
        public int capacity = 0;

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Array()
        {
            this.array = new T[defaultCapacity];
        }

        public Array(int capacity)
        {
            if (capacity < 0) throw new ArgumentException("" + capacity);
            this.capacity = capacity;
            array = new T[capacity];
        }

        public Array(T[] array)
        {
            if(array == null) throw new ArgumentNullException("array cannot be null.");
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
            if(isIndexOutOfRange(index)) throw new ArgumentOutOfRangeException("index is out of array length.");
            return array[index];
        }

        public void set(int index, T value)
        {
            if(isIndexOutOfRange(index)) throw new ArgumentOutOfRangeException("index is out of array length.");
            array[index] = value;
        }

        public bool isIndexOutOfRange(int index)
        {
            return index >=0 && index < capacity;
        }

        public void add(T value)
        {
            if(value == null) throw new ArgumentNullException("value is not in expected format.");

            if (lengthOfArray + 1 >= capacity)
            {
                if (capacity == 0) capacity = 1;
                capacity *= 2;
                Array.Resize(ref array, capacity);
            }
            array[lengthOfArray++] = value;
        }
        
        public void removeAt(int index)
        {
            Array.Copy(array, index + 1, array, index, lengthOfArray - index - 1);
            --lengthOfArray;
            --capacity;
        }
    }
}
