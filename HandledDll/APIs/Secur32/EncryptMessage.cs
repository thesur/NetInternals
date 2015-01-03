using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandledDll.APIs.Secur32
{
    class EncryptMessage : LowLevelHook
    {
        private const uint PROLOGUE_LEN = 8;
        private IntPtr pJmp = IntPtr.Zero; 
        internal delegate Int32 CallDelegate(IntPtr PCtxtHandle, UInt32 fQOP, IntPtr pMessage, UInt32 MessageSeqNo);

        public EncryptMessage(Hook hook)
            : base(hook, PROLOGUE_LEN)
        {

        }
        
        internal override bool Enable(IntPtr pHandle)
        {
            CallDelegate c = new CallDelegate(HookCall);
            pJmp = base.Enable(pHandle, c);
            return (pJmp != IntPtr.Zero);
        }

        internal Int32 HookCall(IntPtr PCtxtHandle, UInt32 fQOP, IntPtr pMessage, UInt32 MessageSeqNo)
        {
            HookedCall hCall = new HookedCall();
            hCall.Hook = base.Hook;

            if (Hook.Type == HookType.PreCall)
            {

                hCall.Arguments.Add((int)PCtxtHandle);
                hCall.Arguments.Add((UInt32)fQOP);
                hCall.Arguments.Add((int)pMessage);
                hCall.Arguments.Add((UInt32)MessageSeqNo);
                ManagedDll.ManagedDll.InterComClient.HookedCall(ref hCall);

                Int32 rValue = CallOriginalFunction(new IntPtr((int)hCall.Arguments[0]),
                                                    (UInt32)hCall.Arguments[1],
                                                    new IntPtr((int)hCall.Arguments[2]),
                                                    (UInt32)hCall.Arguments[3]);
                                                    
                
                return rValue;
            }
            else if (Hook.Type == HookType.PostCall)
            {
                Int32 originalReturnedValue = CallOriginalFunction(PCtxtHandle, fQOP, pMessage, MessageSeqNo);

                hCall.Arguments.Add((int)PCtxtHandle);
                hCall.Arguments.Add((UInt32)fQOP);
                hCall.Arguments.Add((int)pMessage);
                hCall.Arguments.Add((UInt32)MessageSeqNo);
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

        private Int32 CallOriginalFunction(IntPtr PCtxtHandle, UInt32 fQOP, IntPtr pMessage, UInt32 MessageSeqNo)
        {
            CallDelegate func = (CallDelegate)Marshal.GetDelegateForFunctionPointer(pJmp, typeof(CallDelegate));
            return func(PCtxtHandle, fQOP, pMessage, MessageSeqNo);
        }
    }
}
