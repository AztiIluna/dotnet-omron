using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.ControlledStop
{
    public interface IControlledStop
    {
        void RequestToStop();
        bool Stopped();
        bool WaitStopped();
        bool WaitStopped(int millisecondsTimeout);
    }
}
