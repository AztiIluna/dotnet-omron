using ClassLibrary1.ControlledStop;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public abstract class CycleBase : ControlledStopBase, ICycle
    {
        public void MainLoop()
        {
            if (Stopped()) return;
            Cycle_MainLoop();
        }
        public abstract bool Setup();
        protected abstract void Cycle_MainLoop();
    }
}
