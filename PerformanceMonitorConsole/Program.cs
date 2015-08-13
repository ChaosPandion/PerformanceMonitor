using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Management;
using PerformanceMonitor;

namespace PerformanceMonitorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var ns = NetworkMonitor.Get();
        }
    }
}
