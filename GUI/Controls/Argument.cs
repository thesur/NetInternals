using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Controls
{
    internal class Argument
    {
        public DataType Type { get; set; }
        public string Name { get; set; }

        public object Value { get; set; }

        public Argument(DataType type, string name, object value)
        {
            this.Type = type;
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            if (this.Type == DataType.Pointer)
            {
                StringBuilder sbValue = new StringBuilder();
                sbValue.Append(string.Format("0x{0:X} - ", Value));
                int maxBytes = 20;
                byte[] data = Program.internalsMgr.RemoteProcess.ReadMemory(new IntPtr((int)this.Value), maxBytes);

                sbValue.Append("0x");
                foreach(byte b in data)
                    sbValue.Append(string.Format("{0:X}", b));

                sbValue.Append(" (");
                foreach (byte b in data)
                    sbValue.Append((char) b);
                sbValue.Append(")");
                return sbValue.ToString();
            }

            return Value.ToString();
        }
    }
}
