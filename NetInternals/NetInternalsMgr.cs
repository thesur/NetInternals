using InterCom.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace NetInternals
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class NetInternalsMgr : IFromClientToServer
    {
        public RemoteProcess RemoteProcess { get; set; }
        private IntPtr pHandle = IntPtr.Zero;
        private const string UNMANAGED_DLL = @"C:\Users\Administrator\Documents\Visual Studio 2013\Projects\NetInternals15\Dll\Debug\dll.dll";

        InterCom.Server interComServer;

        public NetInternalsMgr()
        {
            interComServer = new InterCom.Server(this);
            RemoteProcess = null;
        }

        public void Inject(Process process)
        {
            pHandle = WinApi.WinApi.OpenProcess(WinApi.WinApi.ProcessAccessFlags.All, false, process.Pid);
            RemoteProcess = new RemoteProcess(pHandle, process.Pid);

            interComServer.Listen();
            DllInjector.DllInjector.InjectByPid(this.RemoteProcess.Pid, UNMANAGED_DLL);
        }

        public Response Hook(Hook hook)
        {
            return interComServer.Hook(hook);
        }

        public static Process[] Processes()
        {
            List<Process> lstProcess = new List<Process>();

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses())
                lstProcess.Add(new Process(process));

            return lstProcess.ToArray();
        }


        #region Log
        public delegate void LogDelegate(string message);
        public event LogDelegate OnNewLog;

        public void Log(string str)
        {
            if (OnNewLog != null)
                OnNewLog(string.Concat("[Remote - ", DateTime.Now.ToShortTimeString(), "] " ,str));
        }
        #endregion

        #region HookedCall
        public delegate void HookedCallDelegate(HookedCall hookedCall);
        public event HookedCallDelegate OnHookedCall;
        public void HookedCall(ref HookedCall hookedCall)
        {
            if (OnHookedCall != null)
                OnHookedCall(hookedCall);
        }
        #endregion
    }
}
