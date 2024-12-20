using System;
using System.Diagnostics;

namespace ClassLibrary1
{
    public class Timer
    {
        private Stopwatch stopWatch;
        private TimeSpan timeToCount;
        private bool enabled;
        private bool startCompleted;

        public bool Enabled
        {
            get => enabled;
            set
            {
                if (value && !enabled) Reset();
                enabled = value;
            }
        }

        public Timer(TimeSpan timespan) : this(timespan, false) { }
        public Timer(int milliseconds) : this(TimeSpan.FromMilliseconds(milliseconds)) { }
        public Timer(int milliseconds, bool startCompleted) : this(TimeSpan.FromMilliseconds(milliseconds), startCompleted) { }
        public Timer(TimeSpan timespan, bool startCompleted)
        {
            timeToCount = timespan;
            enabled = true;
            this.startCompleted = startCompleted;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public bool CheckCompletedAndResetIfApplicable()
        {
            if (Completed)
            {
                Reset();
                return true;
            }
            return false;
        }

        public void ExecuteIfCompleted(Action action)
        {
            if (Completed)
            {
                action();
                Reset();
            }
        }

        public void Reset()
        {
            startCompleted = false;
            stopWatch.Restart();
        }

        public bool Completed
        {
            get
            {
                if (startCompleted || (enabled && (stopWatch.Elapsed >= timeToCount)))
                {
                    startCompleted = false;
                    return true;
                }
                return false;
            }
        }

    }
}