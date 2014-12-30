using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllInjector
{
    public class DllInjector
    {
        public static void InjectByPid(int pid, string dll)
        {
            IntPtr pHandle = OpenProcess(pid);
            IntPtr pStrDll = VirtualAllocEx(pHandle, dll + (char)0x00);
            IntPtr pLoadLibrary = GetFunctionAddress("Kernel32.dll", "LoadLibraryA");
            CallRemoteFunction(pHandle, pLoadLibrary, pStrDll);
        }

        private static void CallRemoteFunction(IntPtr processHandle, IntPtr pFunction, IntPtr pParameter)
        {
            WinApi.WinApi.CreateRemoteThread(processHandle, IntPtr.Zero, 0, pFunction, pParameter, 0, IntPtr.Zero);
        }

        private static IntPtr GetFunctionAddress(string module, string function)
        {
            // Gets the address of an API function.
            // What should I do to get the address of a function of a process?

            IntPtr pModule = WinApi.WinApi.GetModuleHandle(module);
            IntPtr pFunction = WinApi.WinApi.GetProcAddress(pModule, function);
            return pFunction;
        }

        private static IntPtr OpenProcess(int pid)
        {
            return WinApi.WinApi.OpenProcess(WinApi.WinApi.ProcessAccessFlags.All, false, pid);
        }

        private static IntPtr VirtualAllocEx(IntPtr processHandle, string str)
        {
            return VirtualAllocEx(processHandle, ASCIIEncoding.ASCII.GetBytes(str));
        }

        private static IntPtr VirtualAllocEx(IntPtr processHandle, byte[] content)
        {
            IntPtr outBytes = IntPtr.Zero;
            IntPtr pMemoryStr = WinApi.WinApi.VirtualAllocEx(processHandle, IntPtr.Zero, (uint)content.Length, WinApi.WinApi.AllocationType.Commit | WinApi.WinApi.AllocationType.Reserve, WinApi.WinApi.MemoryProtection.ReadWrite);
            WinApi.WinApi.WriteProcessMemory(processHandle, pMemoryStr, content, content.Length, out outBytes);
            return pMemoryStr;
        }
    }
}
