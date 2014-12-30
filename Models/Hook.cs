using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class Hook
    {
        [DataMember]
        public string Module { get; set; }
        [DataMember]
        public string Function { get; set; }
        [DataMember]
        public HookType Type { get; set; }

        public Hook(string module, string function, HookType type)
        {
            this.Module = module;
            this.Function = function;
            this.Type = type;
        }
    }
}
