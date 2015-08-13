using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceMonitor
{
    [DebuggerStepThrough]
    internal static class PerformanceCounterName
    {
        public static class NetworkInterface
        {
            /// <summary>
            /// Bytes Received/sec is the rate at which bytes are received over each network adapter, 
            /// including framing characters. Network Interface\Bytes Received/sec is a subset of Network Interface\Bytes Total/sec.
            /// </summary>
            public const string BytesReceivedPerSec = "Bytes Received/sec";

            /// <summary>
            /// Bytes Sent/sec is the rate at which bytes are sent over each network adapter, including framing characters. 
            /// Network Interface\Bytes Sent/sec is a subset of Network Interface\Bytes Total/sec.
            /// </summary>
            public const string BytesSentPerSec  = "Bytes Sent/sec";

            /// <summary>
            /// Bytes Total/sec is the rate at which bytes are sent and received over each network adapter, including framing characters. 
            /// Network Interface\Bytes Total/sec is a sum of Network Interface\Bytes Received/sec and Network Interface\Bytes Sent/sec.
            /// </summary>
            public const string BytesTotalPerSec = "Bytes Total/sec";

            /// <summary>
            /// Current Bandwidth is an estimate of the current bandwidth of the network interface in bits per second (BPS).  
            /// For interfaces that do not vary in bandwidth or for those where no accurate estimation can be made, this value is the nominal bandwidth.
            /// </summary>
            public const string CurrentBandwidth = "Current Bandwidth";
        }


        public static string Format(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
            if (name.Length > 128)
                throw new ArgumentException(nameof(name));
            var sb = new StringBuilder(name);
            for (var i = 0; i < sb.Length; i++)
            {
                var c = sb[i];
                switch (c)
                {
                    case '(':
                        sb[i] = '[';
                        break;
                    case ')':
                        sb[i] = ']';
                        break;
                    case '#':
                    case '\\':
                    case '/':
                        sb[i] = '_';
                        break;
                    default:
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
