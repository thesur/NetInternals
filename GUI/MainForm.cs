using Be.Windows.Forms;
using InterCom.Interfaces;
using Models;
using NetInternals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class MainForm : Form
    {
        NetInternalsMgr internalsMgr;

        bool modifiyng = false;

        public MainForm()
        {
            internalsMgr = new NetInternalsMgr();
            internalsMgr.OnNewLog += internalsMgr_OnNewLog;
            internalsMgr.OnHookedCall += internalsMgr_OnHookedCall;
            InitializeComponent();
        }

        private void ProcessSend(HookedCall hc)
        {
            IntPtr address = new IntPtr((int)hc.Arguments[1]);
            int len = Convert.ToInt32(hc.Arguments[2]);
            byte[] modifiedData = InterceptData(internalsMgr.RemoteProcess.ReadMemory(address, len));
        }

        private void ProcessRecv(HookedCall hc)
        {
            IntPtr address = new IntPtr((int)hc.Arguments[1]);
            byte[] modifiedData = InterceptData(internalsMgr.RemoteProcess.ReadMemory(address, (Int16)hc.ReturnedValue));
        }

        void internalsMgr_OnHookedCall(HookedCall hc)
        {
            if (hc.Hook.Function == "send")
                ProcessSend(hc);
            else if (hc.Hook.Function == "recv")
                ProcessRecv(hc);
        }

        void internalsMgr_OnNewLog(string message)
        {
            lbLog.Items.Add(message.ToString());
            lbLog.SelectedIndex = lbLog.Items.Count - 1;
        }

        internal void AddLocalLog(string message)
        {
            internalsMgr_OnNewLog(string.Concat("[Local  - ", DateTime.Now.ToShortTimeString(), " ] ", message));
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void SetHTextBox(MemoryStream msData)
        {
            DynamicFileByteProvider dynamicFileByteProvider = new DynamicFileByteProvider(msData);
            hBox.ByteProvider = dynamicFileByteProvider;
        }

        private byte[] InterceptData(byte[] data)
        {
            MemoryStream msData = new MemoryStream(data);
            
            SetHTextBox(msData);

            // This is crap. Have to find out other way to do this.
            modifiyng = true;
            while (modifiyng)
            {
                System.Threading.Thread.Sleep(1);
                System.Windows.Forms.Application.DoEvents();
            }

            hBox.ByteProvider.ApplyChanges();
            int len = (int) msData.Length;
            byte[] modifiedData = new byte[len];
            msData.Read(modifiedData, 0, len);
            msData.Close();
            hBox.ByteProvider.DeleteBytes(0, len);
            return modifiedData;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Hook hook = new Hook("WS2_32", "send", HookType.PreCall);
            Response r = internalsMgr.Hook(hook);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hook hook = new Hook("WS2_32", "recv", HookType.PostCall);
            Response r = internalsMgr.Hook(hook);
        }

        private void btForward_Click(object sender, EventArgs e)
        {
            modifiyng = false;
        }

        private void attachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AttachForm fAttach = new AttachForm();
            fAttach.ShowDialog();

            if (fAttach.SelectedProcess != null)
            {
                AddLocalLog(string.Format("PING! - Attaching to {0} (Pid: {1})", fAttach.SelectedProcess.Name, fAttach.SelectedProcess.Pid));
                internalsMgr.Inject(fAttach.SelectedProcess);
            }
        }
    }
}
