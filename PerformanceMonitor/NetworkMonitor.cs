using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceMonitor
{
    public sealed class NetworkMonitor
    {
        private readonly NetworkInterfaceMonitor[] _networkInterfaceMonitors = NetworkInterfaceMonitor.GetMonitors();

        private NetworkMonitor()
        {
        }


        public double Utilization => _networkInterfaceMonitors.Average(n => n.Utilization);

        public static NetworkMonitor Get()
        {
            return new NetworkMonitor();
        }

    }
}
