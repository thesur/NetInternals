using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandledDll.APIs.Kernel32
{
    class ReadFile : LowLevelHook
    {
        private const uint PROLOGUE_LEN = 10;
        private IntPtr pJmp = IntPtr.Zero; 
        internal delegate bool CallDelegate(IntPtr hFile, IntPtr lpBuffer, IntPtr nNumberOfBytesToRead, IntPtr lpNumberOfBytesRead, IntPtr lpOverlapped);

        public ReadFile(Hook hook)
            : base(hook, PROLOGUE_LEN)
        {

        }
        
        internal override void Enable(IntPtr pHandle)
        {
            CallDelegate c = new CallDelegate(HookCall);
            pJmp = base.Enable(pHandle, c);
        }

        internal bool HookCall(IntPtr hFile, IntPtr lpBuffer, IntPtr nNumberOfBytesToRead, IntPtr lpNumberOfBytesRead, IntPtr lpOverlapped)
        {
            HookedCall hCall = new HookedCall();
            hCall.Hook = base.Hook;

            if (Hook.Type == HookType.PreCall)
            {

                hCall.Arguments.Add((int)hFile);
                hCall.Arguments.Add((int)lpBuffer);
                hCall.Arguments.Add((int)nNumberOfBytesToRead);
                hCall.Arguments.Add((int)lpNumberOfBytesRead);
                hCall.Arguments.Add((int)lpOverlapped);
                ManagedDll.ManagedDll.InterComClient.HookedCall(ref hCall);

                bool rValue = CallOriginalFunction(new IntPtr((int)hCall.Arguments[0]),
                                                    new IntPtr((int)hCall.Arguments[1]),
                                                    new IntPtr((int)hCall.Arguments[2]),
                                                    new IntPtr((int)hCall.Arguments[3]),
                                                    new IntPtr((int)hCall.Arguments[4]));
                                                    
                
                return rValue;
            }
            else if (Hook.Type == HookType.PostCall)
            {
                bool originalReturnedValue = CallOriginalFunction(hFile, lpBuffer, nNumberOfBytesToRead, lpNumberOfBytesRead, lpOverlapped);

                hCall.Arguments.Add((int)hFile);
                hCall.Arguments.Add((int)lpBuffer);
                hCall.Arguments.Add((int)nNumberOfBytesToRead);
                hCall.Arguments.Add((int)lpNumberOfBytesRead);
                hCall.Arguments.Add((int)lpOverlapped);
                hCall.ReturnedValue = originalReturnedValue;

                try
                {
                    ManagedDll.ManagedDll.InterComClient.HookedCall(ref hCall);
                }
                catch
                {
                    ManagedDll.ManagedDll.InterComClient.Log("Excepcion");
                }
                return originalReturnedValue;
            }
            else
                throw new Exception();
        }

        private bool CallOriginalFunction(IntPtr hFile, IntPtr lpBuffer, IntPtr nNumberOfBytesToRead, IntPtr lpNumberOfBytesRead, IntPtr lpOverlapped)
        {
            CallDelegate func = (CallDelegate)Marshal.GetDelegateForFunctionPointer(pJmp, typeof(CallDelegate));
            return func(hFile, lpBuffer, nNumberOfBytesToRead, lpNumberOfBytesRead, lpOverlapped);
        }
    }
}
