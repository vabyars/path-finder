using System;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain.Interfaces;

namespace PathFinder.Domain
{
    public class DictionaryPriorityQueue<TKey> : IPriorityQueue<TKey>
    {
        private readonly Dictionary<TKey, double> _items = new();

        public void Add(TKey key, double value) => _items.Add(key, value);

        public void Delete(TKey key) => _items.Remove(key);

        public void Update(TKey key, double newValue) => _items[key] = newValue;

        public (TKey key, double value) ExtractMin()
        {
            if (_items.Count == 0)
                return default;
            var min = _items.Min(z => z.Value);
            var key = _items.FirstOrDefault(z => Math.Abs(z.Value - min) < 0.000009).Key;
            _items.Remove(key);
            return (key, min);
        }

        public bool TryGetValue(TKey key, out double value) => _items.TryGetValue(key, out value);

        public int Count => _items.Count;

        public IEnumerable<TKey> GetAllItems() => _items.Keys;
    }
}