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

        /// <summary>
        /// Injects into a remote process
        /// </summary>
        /// <param name="process">Process to inject in</param>
        public void Inject(Process process)
        {
            pHandle = WinApi.WinApi.OpenProcess(WinApi.WinApi.ProcessAccessFlags.All, false, process.Pid);
            RemoteProcess = new RemoteProcess(pHandle, process.Pid);

            interComServer.Listen();
            DllInjector.DllInjector.InjectByPid(this.RemoteProcess.Pid, UNMANAGED_DLL);
        }

        /// <summary>
        /// Sends to the remote process the signal of hooking an API
        /// </summary>
        /// <param name="hook"></param>
        /// <returns></returns>
        public Response Hook(Hook hook)
        {
            return interComServer.Hook(hook);
        }

        /// <summary>
        /// Lists running processes
        /// </summary>
        /// <returns></returns>
        public static Process[] Processes()
        {
            List<Process> lstProcess = new List<Process>();

            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses())
                lstProcess.Add(new Process(process));

            return lstProcess.ToArray();
        }

        #region Log
        public delegate void LogDelegate(string message);
        /// <summary>
        /// Is triggered when the hooked process wants to display a log message
        /// </summary>
        public event LogDelegate OnNewLog;

        /// <summary>
        /// Implementation of Intercom.Interfaces.IFromClientToServer. This method is called from the hooked process thought by the InterCom.
        /// </summary>
        /// <param name="str"></param>
        public void Log(string str)
        {
            if (OnNewLog != null)
                OnNewLog(string.Concat("[Remote - ", DateTime.Now.ToShortTimeString(), "] " ,str));
        }
        #endregion

        #region HookedCall
        public delegate void HookedCallDelegate(HookedCall hookedCall);
        /// <summary>
        /// Is triggered when the hooked process makes a call to an hooked API. it thought the InterCom.
        /// </summary>
        public event HookedCallDelegate OnHookedCall;

        /// <summary>
        /// Implementation of Intercom.Interfaces.IFromClientToServer. This method is called from the hooked process thought by the InterCom.
        /// </summary>
        /// <param name="hookedCall"></param>
        public void HookedCall(ref HookedCall hookedCall)
        {
            if (OnHookedCall != null)
                OnHookedCall(hookedCall);
        }
        #endregion
    }
}
