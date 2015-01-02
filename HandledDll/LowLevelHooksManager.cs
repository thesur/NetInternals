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

            if (hook.Module == "WS2_32" && hook.Function == "send")
                llHook = new APIs.WS2_32.Send(hook);
            else if (hook.Module == "WS2_32" && hook.Function == "recv")
                llHook = new APIs.WS2_32.Recv(hook);
            // ReadFile looks like doesn't work, why? is the pipes using readfile while is being hooked?
            /*else if (hook.Module == "Kernel32" && hook.Function == "ReadFile")
                llHook = new APIs.Kernel32.ReadFile(hook);*/
            else
                return false;
            AddHook(llHook);

            return true;
        }

        internal void AddHook(LowLevelHook llHook)
        {
            this.Hooks.Add(llHook);
            llHook.Enable(pHandle);
        }
    }
}
