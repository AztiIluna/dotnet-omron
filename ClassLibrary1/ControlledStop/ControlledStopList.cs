using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.ControlledStop
{
    public class ControlledStopList : IControlledStop
    {
        private readonly List<IControlledStop> _controlledStops;
        public ControlledStopList() { _controlledStops = new List<IControlledStop>(); }
        public void Add(IControlledStop controlledStop) { _controlledStops.Add(controlledStop); }
        public void RequestToStop() { _controlledStops.ForEach(controlledStop => controlledStop.RequestToStop()); }
        public bool Stopped()
        {
            foreach (var controlledStop in _controlledStops)
                if (!controlledStop.Stopped())
                    return false;
            return true;
        }

        public bool WaitStopped()
        {
            foreach (var controlledStop in _controlledStops)
                if (!controlledStop.WaitStopped())
                    return false;
            return true;
        }

        public bool WaitStopped(int millisecondsTimeout)
        {
            foreach (var controlledStop in _controlledStops)
                if (!controlledStop.WaitStopped(millisecondsTimeout))
                    return false;
            return true;
        }
    }
}
