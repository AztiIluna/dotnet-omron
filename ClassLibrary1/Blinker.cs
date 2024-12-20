using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class Blinker
    {
        protected int period;
        protected int startMilliseconds;

        public Blinker(int perdiodInMilliseconds)
        {
            period = perdiodInMilliseconds;
            startMilliseconds = 0;
        }

        public Blinker(TimeSpan periodAsTimespan)
        {
            period = (int)periodAsTimespan.TotalMilliseconds;
            startMilliseconds = 0;
        }

        public bool Status
        {
            get
            {
                return (((Environment.TickCount - startMilliseconds) % period) / (period / 2)) == 1;
            }
        }
    }
}
