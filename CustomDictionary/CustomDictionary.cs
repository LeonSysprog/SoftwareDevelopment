using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomDictionary
{
    public class CustomDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        private LinkedList<KeyValuePair<TKey, TValue>>[] dict;
        private int count;

        public CustomDictionary(int c)
        {
            dict = new LinkedList<KeyValuePair<TKey, TValue>>[c];
            count = c;
        }

        public int Count => dict.Length;

        public bool IsReadOnly => false;

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            var hash = item.Key.GetHashCode();
            var index = Math.Abs(hash % dict.Length);

            if (dict[index] is null)
                dict[index] = new LinkedList<KeyValuePair<TKey, TValue>>();

            dict[index].AddLast(item);
        }

        public void Clear()
        {
            dict = new LinkedList<KeyValuePair<TKey, TValue>>[count];
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return dict[Math.Abs(item.Key.GetHashCode()) % dict.Length].Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Clear();
            for (int index = arrayIndex; index < array.Length; ++index)
                Add(array[index]);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item1 in dict)
            {
                if (item1 != null)
                {
                    foreach (var item2 in item1)
                    {
                        yield return item2;
                    }
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = Math.Abs(item.Key.GetHashCode()) % count;
            if (dict[index] is null || dict[index].Any(x => x.Key.Equals(item)))
                return false;

            dict[index].Remove(item);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
