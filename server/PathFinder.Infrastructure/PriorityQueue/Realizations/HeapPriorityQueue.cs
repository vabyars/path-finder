using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PathFinder.Domain;

namespace PathFinder.Infrastructure.PriorityQueue.Realizations
{
    public class HeapPriorityQueue<TKey> : IPriorityQueue<TKey>
    {
        private readonly List<TKey> points = new();
        private long pointsEverEnqueued;
        private readonly Dictionary<TKey, double> priority = new();
        private readonly Dictionary<TKey, int> keyToIndex = new();
        private readonly Dictionary<TKey, long> insertionIndex = new();
        
        public int Count { get; private set; }
        
        public void Add(TKey key, double value)
        {
            priority[key] = value;
            Count++;
            this[Count] = key;
            keyToIndex[key] = Count;
            insertionIndex[key] = pointsEverEnqueued++;
            HeapifyUp(key);
        }
        
        public void Update(TKey key, double newValue)
        {
            priority[key] = newValue;
            OnNodeUpdated(key);
        }

        private void HeapifyUp(TKey node)
        {
            var parent = keyToIndex[node] / 2;
            while (parent >= 1) 
            {
                var parentNode = this[parent];
                if (HasHigherPriority(parentNode, node))
                    break;

                Swap(node, parentNode);

                parent = keyToIndex[node] / 2;
            }
        }
        
        private void HeapifyDown(TKey node)
        {
            var finalQueueIndex = keyToIndex[node];
            while (true) 
            {
                var newParent = node;
                var childLeftIndex = 2 * finalQueueIndex;

                if (childLeftIndex > Count) 
                {
                    keyToIndex[node] = finalQueueIndex;
                    this[finalQueueIndex] = node;
                    break;
                }

                var childLeft = this[childLeftIndex];
                if (HasHigherPriority(childLeft, newParent))
                    newParent = childLeft;

                var childRightIndex = childLeftIndex + 1;
                if (childRightIndex <= Count) 
                {
                    var childRight = this[childRightIndex];
                    if (HasHigherPriority(childRight, newParent))
                        newParent = childRight;
                }

                if (!newParent.Equals(node)) 
                {
                    this[finalQueueIndex] = newParent;
                    var temp = keyToIndex[newParent];
                    keyToIndex[newParent] = finalQueueIndex;
                    finalQueueIndex = temp;
                }
                else 
                {
                    keyToIndex[node] = finalQueueIndex;
                    this[finalQueueIndex] = node;
                    break;
                }
            }
        }

        private void Swap(TKey point1, TKey point2)
        {
            this[keyToIndex[point1]] = point2;
            this[keyToIndex[point2]] = point1;

            var temp = keyToIndex[point1];
            keyToIndex[point1] = keyToIndex[point2];
            keyToIndex[point2] = temp;
        }
        
        public void Delete(TKey key)
        {
            if (Count <= 1) 
            {
                this[1] = default;
                Count = 0;
                return;
            }

            var wasSwapped = false;
            var formerLastNode = this[Count];
            if (keyToIndex[key] != Count) 
            {
                Swap(key, formerLastNode);
                wasSwapped = true;
            }

            Count--;
            this[keyToIndex[key]] = default;

            if (wasSwapped)
                OnNodeUpdated(formerLastNode);
        }
        
        private void OnNodeUpdated(TKey key)
        {
            var parentIndex = keyToIndex[key] / 2;
            var parent = this[parentIndex];

            if (parentIndex > 0 && HasHigherPriority(key, parent)) 
                HeapifyUp(key);
            else 
                HeapifyDown(key);
        }

        public (TKey key, double value) ExtractMin()
        {
            var point = this[1];
            Delete(point);
            return (point, priority[point]);
        }

        public bool TryGetValue(TKey key, out double value)
        {
            if (Contains(key))
            {
                value = priority[key];
                return true;
            }

            value = default;
            return false;
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            for (var i = 1; i <= Count; i++)
                yield return points[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private TKey this[int i]
        {
            get
            {
                if (i < 0 || i >= points.Count)
                    return default;
                return points[i];
            }
            set
            {
                if (i < 0) 
                    throw new IndexOutOfRangeException($"index({i}) out of range");
                if (i >= points.Count)
                    points.AddRange(Enumerable.Repeat(default(TKey), i + 1 - points.Count));
                points[i] = value;
            }
        }

        private bool Contains(TKey node)
        {
            return keyToIndex.ContainsKey(node) && this[keyToIndex[node]].Equals(node);
        }

        private bool HasHigherPriority(TKey higher, TKey lower) =>
            priority[higher] < priority[lower] ||
            Math.Abs(priority[higher] - priority[lower]) < double.Epsilon &&
            insertionIndex[higher] < insertionIndex[lower];
    }
}