using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generictrain
{
    public class MyList<T>
    {
        public List<T> list = new List<T>();
        public void Add(T item)
        {
            list.Add(item);
        }

        public void Remove(T item)
        {
            list.Remove(item);
        }

        public int Count()
        {
            int count = list.Count;
            return count;
        }

        private T[] array = new T[0];

        public void Add2(T item)
        {
            T[] newArray = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            newArray[array.Length] = item;
            array = newArray;
        }

        public void Remove2(T item)
        {
            if (array.Contains(item))
            {
                T[] newArray = new T[array.Length - 1];
                int newIndex = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    if (!array[i].Equals(item))
                    {
                        newArray[newIndex] = array[i];
                        newIndex++;
                    }
                }
                array = newArray;
            }
        }

        public int Count2()
        {
            return array.Length;
        }

    }
}
