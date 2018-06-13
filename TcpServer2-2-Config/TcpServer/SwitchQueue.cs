using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class SwitchQueue<T>
    {
        private Queue<T> mConsumeQueue;
        private Queue<T> mProduceQueue;
        public SwitchQueue()
        {
            mConsumeQueue = new Queue<T>(16);
            mProduceQueue = new Queue<T>(16);
        }
        //Produce;
        public void Push(T obj)
        {
            lock (mProduceQueue)
            {
                mProduceQueue.Enqueue(obj);
            }
        }
        //consumer
        public T pop()
        {
            return (T)mConsumeQueue.Dequeue();
        }
        public bool Empty()
        {
            return 0 == mConsumeQueue.Count;
        }
        public void Switch()
        {
            lock (mProduceQueue)
            {
                Swap(ref mConsumeQueue, ref mProduceQueue);
            }
        }
        public void Clear()
        {
            lock (mProduceQueue)
            {
                mConsumeQueue.Clear();
                mProduceQueue.Clear();
            }
        }
        void Swap<T>(ref T a,ref T b)
        {
            T c = a;
            a = b;
            b = c;
            return;
        }
    }
}
