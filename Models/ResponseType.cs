using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public enum ResponseType
    {
        Unknow = -1,
        OK = 0,
        Error = 1
    }
}
