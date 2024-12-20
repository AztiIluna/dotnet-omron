using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class CommunicationWatchdog<T>
    {
        protected Timer timer;
        protected T watchdogValue;
        protected bool watchdogValueInitialized;

        public CommunicationWatchdog(int milliseconds)
        {
            timer = new Timer(milliseconds);
            watchdogValueInitialized = false;
        }

        public CommunicationWatchdog(TimeSpan timespan)
        {
            timer = new Timer(timespan);
            watchdogValueInitialized = false;
        }

        public void Update(T value)
        {
            if ((!watchdogValue.Equals(value)) || (!watchdogValueInitialized))
            {
                timer.Reset();
            }
            watchdogValueInitialized = true;
            watchdogValue = value;
        }

        public bool CommunicationOk
        {
            get
            {
                return (!timer.Completed) && watchdogValueInitialized;
            }
        }
    }
}
