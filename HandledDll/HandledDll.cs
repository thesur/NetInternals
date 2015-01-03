using HandledDll;
using InterCom.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagedDll
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class ManagedDll : IFromServerToClient
    {
        internal static InterCom.Client InterComClient { get; set; }
        private LowLevelHooksManager LowLevelHooksManager { get; set; }

        public static int Start(string args)
        {
            ManagedDll managed = new ManagedDll();
            managed.Instance();
            return 0;
        }

        private void Instance()
        {
            try
            {
                this.LowLevelHooksManager = new HandledDll.LowLevelHooksManager();
                InterComClient = new InterCom.Client(this);
                InterComClient.Connect();
                InterComClient.Log("PONG! - Process hooked.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// This method is called by the intercom when the process wants the hooked process to hook any API
        /// </summary>
        /// <param name="hook"></param>
        /// <returns></returns>
        public Response Hook(Hook hook)
        {
            

            if (LowLevelHooksManager.AddHook(hook))
            {
                InterComClient.Log("Hooking " + string.Concat(hook.Module, "!", hook.Function, " (", hook.Type.ToString(), ")"));
                return new Response(ResponseType.OK, "OK");
            }
            else
            {
                InterComClient.Log("Error hooking. Not implemented");
                return new Response(ResponseType.Error, "Invalid hook. Not implemented");
            }
        }
    }
}
