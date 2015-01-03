using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandledDll.APIs.WS2_32
{
    class Send : LowLevelHook
    {
        private const uint PROLOGUE_LEN = 10;
        private IntPtr pJmp = IntPtr.Zero; // Where it has to jump after the hook (just after the overwritten prologue)
        internal delegate Int16 CallDelegate(UInt16 socket, IntPtr pBuffer, Int16 len, Int16 flags);

        //internal HookCallBack Call;

        public Send(Hook hook)
            : base(hook, PROLOGUE_LEN)
        {

        }

        internal override bool Enable(IntPtr pHandle)
        {
            CallDelegate c = new CallDelegate(HookCall);
            pJmp = base.Enable(pHandle, c);
            return (pJmp != IntPtr.Zero);
        }

        internal Int16 HookCall(UInt16 socket, IntPtr pBuffer, Int16 len, Int16 flags)
        {
            HookedCall hCall = new HookedCall();
            hCall.Hook = base.Hook;

            if (Hook.Type == HookType.PreCall)
            {
                hCall.Arguments.Add(socket);
                // There is an issue passing by WCF an IntPtr since each process can have a different arquitecture. That's why is being passed as Int (but it requieres a fix!)
                // https://social.msdn.microsoft.com/Forums/en-US/3e0f0598-36f3-400f-9aec-32cec2bd1117/passing-intptr-to-wcf-service?forum=netfxremoting
                hCall.Arguments.Add((int)pBuffer);
                hCall.Arguments.Add(len);
                hCall.Arguments.Add(flags);
                ManagedDll.ManagedDll.InterComClient.HookedCall(ref hCall);
                return CallOriginalFunction((UInt16)hCall.Arguments[0], new IntPtr((int)hCall.Arguments[1]), (Int16)hCall.Arguments[2], (Int16)hCall.Arguments[3]);
            }
            else if (Hook.Type == HookType.PostCall)
            {
                hCall.ReturnedValue = CallOriginalFunction(socket, pBuffer, len, flags);
                hCall.Arguments.Add(socket);
                // There is an issue passing by WCF an IntPtr since each process can have a different arquitecture. That's why is being passed as Int (but it requieres a fix!)
                // https://social.msdn.microsoft.com/Forums/en-US/3e0f0598-36f3-400f-9aec-32cec2bd1117/passing-intptr-to-wcf-service?forum=netfxremoting
                hCall.Arguments.Add((int)pBuffer);
                hCall.Arguments.Add(len);
                hCall.Arguments.Add(flags);
                ManagedDll.ManagedDll.InterComClient.HookedCall(ref hCall);
                return (Int16)hCall.ReturnedValue;
            }
            else
                throw new Exception();
        }

        private Int16 CallOriginalFunction(UInt16 socket, IntPtr pBuffer, Int16 len, Int16 flags)
        {
            CallDelegate func = (CallDelegate)Marshal.GetDelegateForFunctionPointer(pJmp, typeof(CallDelegate));
            return func(socket, pBuffer, len, flags);
        }
    }
}
