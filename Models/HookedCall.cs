using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class HookedCall
    {
        [DataMember]
        public object ReturnedValue { get; set; }
        [DataMember]
        public List<Object> Arguments = new List<object>();
        [DataMember]
        public Hook Hook { get; set; }
        public HookedCall() : this(null, null, new List<object>())
        {
        }

        public HookedCall(Hook hook, object returnedValue, List<object> arguments)
        {
            this.Hook = hook;
            this.ReturnedValue = returnedValue;
            this.Arguments = arguments;
        }
    }
}
