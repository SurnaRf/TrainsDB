namespace Pathfinding
{
    public class BinaryHeap<T>
        where T : IComparable<T>
    {
        private const int DefaultCapacity = 8;

        public T[] items;
        private int count;
        private readonly bool ascending;

        public int Count => count;

        public int Capacity
        {
            get => items.Length;
            private set
            {
                if (value < count)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value), "Cannot shrink collect below Count size.");
                }

                T[] newItems = new T[value];
                Array.Copy(items, 0, newItems, 0, count);
                items = newItems;
            }
        }

        public BinaryHeap(bool ascending = true)
        {
            count = 0;
            items = Array.Empty<T>();
            this.ascending = ascending;
        }

        public BinaryHeap(int capacity, bool ascending = true)
        {
            count = 0;
            items = new T[capacity];
            this.ascending = ascending;
        }

        public void Add(T item)
        {
            EnsureCapacity(count + 1);

            items[count] = item;

            count++;
            Ascend(count - 1);
        }

        public T Peek()
        {
            if (count == 0)
                throw new InvalidOperationException("Collection is empty.");

            return items[0];
        }

        public T Pop()
        {
            if (count == 0)
                throw new InvalidOperationException("Collection is empty.");

            T result = items[0];

            count--;
            items[0] = items[count];
            items[count] = default;
            Descend(0);

            return result;
        }

        public bool TryPeek(out T value)
        {
            if (count == 0)
            {
                value = default;
                return false;
            }

            value = items[0];

            return true;
        }

        public bool TryPop(out T value)
        {
            if (count == 0)
            {
                value = default;
                return false;
            }

            value = items[0];

            count--;
            items[0] = items[count];
            items[count] = default;
            Descend(0);

            return true;
        }

        private void Ascend(int index)
        {
            while (index != 0)
            {
                int parent = Parent(index);
                if (Compare(index, parent) < 0)
                    return;

                Swap(index, parent);
                index = parent;
            }
        }

        private void Descend(int index)
        {
            while (index < count)
            {
                int toSwap = index;
                int left = Left(index), right = Right(index);
                if (Compare(toSwap, left) < 0)
                    toSwap = left;
                if (Compare(toSwap, right) < 0)
                    toSwap = right;
                if (toSwap == index)
                    break;
                Swap(toSwap, index);
                index = toSwap;
            }
        }

        private int Parent(int index) => (index - 1) >> 1;

        private int Left(int index) => (index << 1) + 1;

        private int Right(int index) => (index + 1) << 1;

        private void Swap(int indexA, int indexB)
        {
            (items[indexB], items[indexA]) = (items[indexA], items[indexB]);
        }

        private int Compare(int indexA, int indexB)
        {
            if (indexA >= count) return -1;
            if (indexB >= count) return 1;
            int result = items[indexA].CompareTo(items[indexB]);
            if (ascending) result = -result;
            return result;
        }

        private void EnsureCapacity(int min)
        {
            if (min <= items.Length) return;

            int newCapacity = items.Length == 0 ? DefaultCapacity : (items.Length << 1);
            if ((uint)newCapacity > int.MaxValue) newCapacity = int.MaxValue;
            if (newCapacity < min) newCapacity = min;

            Capacity = newCapacity;
        }
    }
}