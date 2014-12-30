using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetInternals
{
    public class RemoteProcess
    {
        internal IntPtr ProcessHandle { get; set; }
        internal int Pid { get; set; }

        public RemoteProcess(IntPtr pHandle, int pId)
        {
            this.Pid = pId;
            this.ProcessHandle = pHandle;
        }

        public byte[] ReadMemory(IntPtr address, int len)
        {
            byte[] buffer = new byte[len];
            IntPtr bytesReaded = IntPtr.Zero;

            WinApi.WinApi.ReadProcessMemory(ProcessHandle, address, buffer, (uint) len, out bytesReaded);
            return buffer;
        }
    }

}
