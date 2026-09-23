using System;
using System.Collections.Generic;

namespace Lab04
{
    public class Repository<T> where T : IEntity
    {
        private List<T> _items;

        public Repository()
        {
            _items = new List<T>();
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public T? FindById(string id)
        {
            foreach (T item in _items)
            {
                if (item.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return default;
        }

        public List<T> Find(Func<T, bool> predicate)
        {
            List<T> result = new List<T>();
            foreach (T item in _items)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<T> GetAll()
        {
            return new List<T>(_items);
        }
    }
}
