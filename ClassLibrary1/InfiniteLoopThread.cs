using ClassLibrary1.ControlledStop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public abstract class InfiniteLoopThread : ControlledStopBase
    {
        protected bool MainLoopExecutedAtLeastOnce { get; private set; }

        private Thread thread;
        private bool stopThread;
        private bool requestForThread_OnStopRequested;
        private bool thread_OnStopRequestedExecuted;
        private string threadName;
        private readonly int sleepTime;

        public InfiniteLoopThread(string threadName) : this(threadName, true, 1) { }
        public InfiniteLoopThread(string threadName, bool autoStart) : this(threadName, autoStart, 1) { }
        public InfiniteLoopThread(string threadName, int sleepTime) : this(threadName, true, sleepTime) { }
        public InfiniteLoopThread(string threadName, bool autoStart, int sleepTime)
        {
            this.sleepTime = sleepTime;
            thread = new Thread(new ThreadStart(InfiniteLoop));
            stopThread = false;
            //stopRequestedRoutineExecuted = false;
            requestForThread_OnStopRequested = false;
            thread_OnStopRequestedExecuted = false;
            thread.IsBackground = true;
            this.threadName = threadName;
            MainLoopExecutedAtLeastOnce = false;
            if (autoStart) Start();
        }

        protected void Start()
        {
            if(Unstarted()) thread.Start();
        }

        private void InfiniteLoop()
        {
            Setup();
            while (!stopThread)
            {
#if DEBUG
                MainLoop();
#else
                try
                {
                    this.MainLoop();
                }
                catch (Exception ex)
                {
                    string exePath = Assembly.GetExecutingAssembly().Location;
                    string exeDir = Path.GetDirectoryName(exePath);
                    //TraceToFile.WriteLine(exeDir, $"{threadName}_Thread_EXCEPTIONS.txt", $"{ex.Message} // {ex.StackTrace}");
                }
#endif
                MainLoopExecutedAtLeastOnce = true;
                if (requestForThread_OnStopRequested && IsStopRequested && !thread_OnStopRequestedExecuted)
                {
                    requestForThread_OnStopRequested = false;
                    thread_OnStopRequestedExecuted = true;
                    Thread_OnStopRequested();
                }
                Thread.Sleep(sleepTime);
            }
            Finally();
            SetStopped();
        }

        abstract protected void Setup();
        abstract protected void MainLoop();
        abstract protected void Finally();
        protected override void OnStopRequested()
        {
            requestForThread_OnStopRequested = true;
        }
        protected virtual void Thread_OnStopRequested() { }
        //protected virtual bool GetConditionToStop()
        //{
        //    return true;
        //}

        //public bool Stopped() { return thread.ThreadState == ThreadState.Stopped; }

        public bool Unstarted() { return ((int)thread.ThreadState & 8) == 8; }
    }
}
