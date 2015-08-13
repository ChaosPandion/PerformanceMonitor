using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceMonitor
{

    /// <summary>
    /// Represents a performance monitor for a network interface.
    /// </summary>
    public sealed class NetworkInterfaceMonitor
    {
        private static readonly PerformanceCounterCategory _category = new PerformanceCounterCategory("Network Interface");

        private NetworkInterfaceMonitor(ManagementObject managementObject)
        {
            dynamic d = new DynamicManagementObject(managementObject);
            Name = d.InterfaceDescription;
            Speed = d.Speed;
            Properties = managementObject.Properties.Cast<PropertyData>().ToDictionary(p => p.Name, p => p.Value);
            PerformanceCounters = _category.GetCounters(PerformanceCounterName.Format(Name)).ToDictionary(c => c.CounterName);
        }

        /// <summary>
        /// Gets a unique name assigned to the network interface during installation.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the bandwidth, in bits per second, of the network interface.
        /// </summary>
        public ulong Speed { get; }

        /// <summary>
        /// Gets the utilization of the network interface as a percentage.
        /// </summary>
        public double Utilization => TotalBytesPerSec / (Speed / 8);

        /// <summary>
        /// Gets the rate at which bytes are sent and received
        /// </summary>
        public double TotalBytesPerSec => PerformanceCounters[PerformanceCounterName.NetworkInterface.BytesTotalPerSec].NextValue();

        /// <summary>
        /// Gets the named properties of the network interface.
        /// </summary>
        public IReadOnlyDictionary<string, object> Properties { get; }

        /// <summary value=''>
        /// Gets the named performance counters of the network interface.
        /// </summary>
        public IReadOnlyDictionary<string, PerformanceCounter> PerformanceCounters { get; }


        /// <summary>
        /// Returns the name of the network interface.
        /// </summary>
        [DebuggerStepThrough]
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gets a monitor for all hardware network interfaces.
        /// </summary>
        public static NetworkInterfaceMonitor[] GetMonitors()
        {
            const string wmiScope = "/Root/StandardCimv2";
            const string wmiQuery = "SELECT * FROM MSFT_NetAdapter WHERE HardwareInterface = true";
            using (var searcher = new ManagementObjectSearcher(wmiScope, wmiQuery))
                return searcher.Get().Cast<ManagementObject>().Select(o => new NetworkInterfaceMonitor(o)).ToArray();
        }
    }
}
