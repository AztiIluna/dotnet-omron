using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class TimeBenchmarks
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly Queue<TimeSpan> times = new Queue<TimeSpan>();

        public TimeSpan Max { get; private set; }
        public TimeSpan Avg { 
            get 
            {
                if (!times.Any()) return TimeSpan.Zero;
                return TimeSpan.FromMilliseconds(times.Average(x => x.TotalMilliseconds));
            } 
        }

        public void Start() => stopwatch.Restart();
        public void Stop()
        {

            times.Enqueue(stopwatch.Elapsed);
            if (times.Last() > Max) Max = times.Last();
            if (times.Count > 50) times.Dequeue();
            stopwatch.Reset();
        }

        public override string ToString()
        {
            return $"Count: {times.Count} // Max: {Max} // Avg: {Avg}";
        }
    }
}
