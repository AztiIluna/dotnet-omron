using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class IntBenchmarks
    {
        private readonly int maxCapacity;
        private readonly Queue<int> values = new();
        private int max;
        private int min;
        public IntBenchmarks(int maxCapacity)
        {
            this.maxCapacity = maxCapacity;
        }

        public void Add(int value)
        {
            values.Enqueue(value);
            if (value > max) max = value;
            if (value < min) min = value;
            if (values.Count > maxCapacity) values.Dequeue();
            if (values.Count > maxCapacity)
            {
                var ee = 33;
            }
        }

        public int Max { get => max; }
        public int Min { get => min; }
        public int Avg { get => (int)values.Average(x => (double)x); }
    }
}
