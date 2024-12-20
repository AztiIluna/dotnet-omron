using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ClassLibrary1.ControlledStop
{
    public class ControlledStopBase : IControlledStop
    {
        private readonly AutoResetEvent stoppedAutoResetEvent;
        private bool stopRequested;
        public ControlledStopBase()
        {
            stoppedAutoResetEvent = new AutoResetEvent(false);
            stopRequested = false;
        }
        public void RequestToStop()
        {
            stopRequested = true;
            OnStopRequested();
        }
        public bool WaitStopped()
        {
            return stoppedAutoResetEvent.WaitOne();
        }
        public bool WaitStopped(int millisecondsTimeout)
        {
            return stoppedAutoResetEvent.WaitOne(millisecondsTimeout);
        }        
        protected void SetStopped()
        {
            stoppedAutoResetEvent.Set();
        }
        protected virtual void OnStopRequested() { }

        public bool Stopped() => WaitStopped(0);

        protected bool IsStopRequested { get; }
    }
}
