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
            ManagedDll.ManagedDll.InterComClient.Log(hook.Module + " " + hook.Function);
            if (hook.Module == "WS2_32" && hook.Function == "send")
                llHook = new APIs.WS2_32.Send(hook);
            else if (hook.Module == "WS2_32" && hook.Function == "recv")
                llHook = new APIs.WS2_32.Recv(hook);
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
