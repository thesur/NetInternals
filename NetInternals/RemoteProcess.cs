using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetInternals
{
    /// <summary>
    /// Represents the process that is being hooked
    /// </summary>
    public class RemoteProcess
    {
        internal IntPtr ProcessHandle { get; set; }
        internal int Pid { get; set; }

        /// <summary>
        /// Represents the process that is being hooked by a process handle
        /// </summary>
        /// <param name="pHandle">Process handle pointer</param>
        /// <param name="pId">Process id</param>
        public RemoteProcess(IntPtr pHandle, int pId)
        {
            this.Pid = pId;
            this.ProcessHandle = pHandle;
        }

        /// <summary>
        /// Reads from the remote process memory
        /// </summary>
        /// <param name="address"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public byte[] ReadMemory(IntPtr address, int len)
        {
            byte[] buffer = new byte[len];
            IntPtr bytesReaded = IntPtr.Zero;

            WinApi.WinApi.ReadProcessMemory(ProcessHandle, address, buffer, (uint)len, out bytesReaded);
            return buffer;
        }

        public void WriteMemory(IntPtr address, byte[] data)
        {
            IntPtr bytesWritten = IntPtr.Zero;
            WinApi.WinApi.WriteProcessMemory(ProcessHandle, address, data, data.Length, out bytesWritten);
        }

        public void WriteMemory(IntPtr address, string ascii)
        {
            WriteMemory(address, ASCIIEncoding.ASCII.GetBytes(ascii));
        }
    }

}
