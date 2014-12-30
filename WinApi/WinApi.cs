using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinApi
{
    public static class WinApi
    {
        //Enums
        [Flags]
        public enum ProcessAccessFlags : int
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        //[DllImport("Kernel32.dll")]
        //public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, IntPtr dwDesiredAccess, IntPtr dwFileOffsetHigh, IntPtr dwFileOffsetLow, IntPtr dwNumberOfBytesToMap);

        //[DllImport("Kernel32.dll")]
        //public static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr flProtect, IntPtr dwMaximumSizeHigh, IntPtr dwMaximumSizeLow);

        [DllImport("Kernel32.dll")]
        public static extern IntPtr CreateFile(IntPtr lpFileName, IntPtr dwDesiredAccess, IntPtr dwShareMode, out IntPtr lpSecurityAttributes, IntPtr dwCreationDisposition, IntPtr dwFlagsAndAttributes, out IntPtr hTemplateFile);


        [DllImport("Kernel32.dll")]
        public static extern IntPtr OpenFile(IntPtr lpFileName, out IntPtr lpReOpenBuff, IntPtr uStyle);

        [DllImport("Kernel32.dll")]
        public static extern IntPtr GetFinalPathNameByHandle(IntPtr hFile, out IntPtr lpszFilePath, IntPtr cchFilePath, IntPtr dwFlags);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UInt32 nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
    }
}
