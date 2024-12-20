using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class NiceConcurrentQueue<T>
    {
        private readonly ConcurrentQueue<T> _queue;
        public NiceConcurrentQueue()
        {
            _queue = new ConcurrentQueue<T>();
        }
        public void Enqueue(T item) { _queue.Enqueue(item); }
        public void Enqueue(IEnumerable<T> items)
        {
            items.ToList().ForEach(Enqueue);
        }
        public void DequeueAllWithActions(Action<T> action)
        {
            while (_queue.TryDequeue(out var item)) action(item);
        }
        public List<T> DequeueAllAsList() 
        { 
            var list = new List<T>();
            DequeueAllWithActions(i => list.Add(i));
            return list;
        }
        public bool TryDequeue(out T item) { return _queue.TryDequeue(out item); }
    }
}
