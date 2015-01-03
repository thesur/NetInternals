using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandledDll
{
    internal class LowLevelHooksManager
    {
        private IntPtr pHandle { get; set; }
        private IList<LowLevelHook> Hooks { get; set; }

        public LowLevelHooksManager()
        {
            pHandle = WinApi.WinApi.OpenProcess(WinApi.WinApi.ProcessAccessFlags.All, false, System.Diagnostics.Process.GetCurrentProcess().Id);
            Hooks = new List<LowLevelHook>();
        }

        internal bool AddHook(Hook hook)
        {
            LowLevelHook llHook;

            try
            {
                if (hook.Module == "WS2_32" && hook.Function == "send")
                    llHook = new APIs.WS2_32.Send(hook);
                else if (hook.Module == "WS2_32" && hook.Function == "recv")
                    llHook = new APIs.WS2_32.Recv(hook);
                
                /*  =============================================================================
                //  Problematic API's, due that this functions are called by the WCF (Windows
                //  Communication Foundation) for the interprocess communication, so when this 
                //  API's are hooked, the communication is lost. There is any easy way to solve
                //  this issue?                                                                  
                // ============================================================================= */
                // This function (Secur32!EncryptMessage) works because I've dissable the authentication
                // in the security transport layer in the binding (WCF). So now this API isn't called
                // by WCF during the intercomunication between process.
                else if (hook.Module == "Secur32" && hook.Function == "EncryptMessage")
                    llHook = new APIs.Secur32.EncryptMessage(hook);
                // No idea yet how to fix the issue with ReadFile... Is called by WCF to send data
                // through pipes.
                //else if (hook.Module == "Kernel32" && hook.Function == "ReadFile")
                //    llHook = new APIs.Kernel32.ReadFile(hook);
                else
                    return false;
            }
            catch
            {
                return false;
            }

            return AddHook(llHook);
        }

        internal bool AddHook(LowLevelHook llHook)
        {

            this.Hooks.Add(llHook);
            return llHook.Enable(pHandle);

        }
    }
}
