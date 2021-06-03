using System;

namespace PathFinder.Infrastructure.Interfaces
{
    public interface IPriorityQueueProvider<T>
    {
        IPriorityQueue<T> Create();
    }
}