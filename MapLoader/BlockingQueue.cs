using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace MapLoader
{
	public class BlockingQueue<T>
	{
        private bool open = false;
        private object highPrioLock = new object();
        private object stackLock = new object();
        private Queue<T> queue;
        private List<T> highPrio;
        private Semaphore stackEmpty;
		public BlockingQueue ()
		{
            queue = new Queue<T>();
            highPrio = new List<T>();
            stackEmpty = new Semaphore(0,int.MaxValue);
            open = true;
		}

		public bool TryDequeue(out T value)
		{
            if (highPrio.Count > 0)
            {
                lock(highPrioLock)
                {
                    value = highPrio[highPrio.Count - 1];
                    highPrio.RemoveAt(highPrio.Count - 1);
                }
            }
			else
            {
                stackEmpty.WaitOne();
                if (highPrio.Count > 0)
                {
                    return TryDequeue(out value);
                }
                else if (queue.Count == 0)
                {
                    value = default(T);
                    return open;
                }
                lock (stackLock)
                {
                    value = queue.Dequeue();
                }
            }
            return open;
		}
		public void Enqueue(T value)
		{
            lock (stackLock)
            {
                queue.Enqueue(value);
                stackEmpty.Release();
            }
		}
        public void Enqueue(T value,int priority=-1)
        {
            if (priority == -1 || priority >= highPrio.Count)
                Enqueue(value);
            lock (highPrioLock)
            {
                if (priority == 0)
                {
                    highPrio.Add(value);
                }
                else
                {
                    highPrio.Insert(highPrio.Count - priority - 1, value);
                }
                if (queue.Count == 0)
                    stackEmpty.Release();
            }
        }

		public void Close()
		{
            open = false;
            stackEmpty.Release(100);
		}
	}
}

