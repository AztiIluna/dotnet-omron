using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.ConnectionPathManager
{
    public class Manager
    {
        private List<ConnectionPath> connectionPaths;
        protected int selectedIndex;
        protected int requestedIndex;

        public Manager(List<ConnectionPath> connectionPaths, int defaultIndex)
        {
            this.connectionPaths = connectionPaths;
            selectedIndex = defaultIndex;
            requestedIndex = defaultIndex;
        }

        public string GetIpAdress
        {
            get
            {
                selectedIndex = requestedIndex;
                return connectionPaths[selectedIndex].IpAdress;
            }                 
        }

        public bool RequestToChangeSelectedConnectionPathActive
        {
            get { return selectedIndex != requestedIndex; }            
        }

        public void RequestToChangeSelectedConnectionPath(int newIndex)
        {
            if (newIndex >= 0 && newIndex < connectionPaths.Count)
                requestedIndex = newIndex;
        }
    }
}
