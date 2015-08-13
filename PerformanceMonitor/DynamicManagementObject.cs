using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceMonitor
{

    [DebuggerStepThrough]
    internal sealed class DynamicManagementObject : DynamicObject
    {
        private readonly ManagementBaseObject _obj;

        public DynamicManagementObject(ManagementBaseObject o)
        {
            _obj = o;
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _obj.Properties.Cast<PropertyData>().Select(p => p.Name);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _obj[binder.Name];
            return result != null;
        }
    }

}
