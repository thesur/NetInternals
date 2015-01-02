using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    public class Response
    {
        public ResponseType Type { get; set; }
        [DataMember]
        public string Message { get; set; }

        public Response(ResponseType type, string message)
        {
            this.Type = type;
            this.Message = message;
        }

        public Response(ResponseType type) : this(type, string.Empty) { }
    }
}
