using Be.Windows.Forms;
using GUI.Controls;
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
        bool intercepting = false;

        public MainForm()
        {
            try
            {
                internalsMgr = new NetInternalsMgr();
            }
            catch (Exception ex)
            {
                ErrorForm errorFrm = new ErrorForm(ex.Message);
                errorFrm.ShowDialog();
                this.Close();
            }

            internalsMgr.OnNewLog += internalsMgr_OnNewLog;
            internalsMgr.OnHookedCall += internalsMgr_OnHookedCall;
            InitializeComponent();
        }

        private void ProcessSend(HookedCall hc)
        {
           // IntPtr address = new IntPtr((int)hc.Arguments[1]);
            //int len = Convert.ToInt32(hc.Arguments[2]);
            //byte[] modifiedData = InterceptData(internalsMgr.RemoteProcess.ReadMemory(address, len), hc);
            editor.Clear();
            editor.AddArgument(DataType.Int16, "Socket", hc.Arguments[0]);
            editor.AddArgument(DataType.Pointer, "Buffer", hc.Arguments[1]);
            editor.AddArgument(DataType.Int16, "Length", hc.Arguments[2]);
            editor.AddArgument(DataType.Int16, "Flags", hc.Arguments[3]);

            lbCallInfo.Text = string.Format("{0}!{1} ({2})", hc.Hook.Module, hc.Hook.Function, hc.Hook.Type.ToString());
            modifiyng = true;
            while (modifiyng)
            {
                System.Threading.Thread.Sleep(1);
                System.Windows.Forms.Application.DoEvents();
            }
            lbCallInfo.Text = string.Empty;
            editor.Clear();
        }

        private void ProcessRecv(HookedCall hc)
        {
            //IntPtr address = new IntPtr((int)hc.Arguments[1]);
            //byte[] modifiedData = InterceptData(internalsMgr.RemoteProcess.ReadMemory(address, (Int16)hc.ReturnedValue), hc);
        }

        void internalsMgr_OnHookedCall(HookedCall hc)
        {
            if (!intercepting)
                return;

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
            internalsMgr_OnNewLog(string.Concat("[Local  - ", DateTime.Now.ToShortTimeString(), "] ", message));
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            lbCallInfo.Text = string.Empty;
            ToggleIntercept();
        }

        private void ToggleIntercept()
        {
            if (!intercepting)
            {
                intercepting = true;
                btIntercepting.Text = "Intercepting is ON";
            }
            else
            {
                intercepting = false;
                btIntercepting.Text = "Intercepting is OFF";
            }
        }

        private void SetHTextBox(MemoryStream msData)
        {
            DynamicFileByteProvider dynamicFileByteProvider = new DynamicFileByteProvider(msData);
            hBox.ByteProvider = dynamicFileByteProvider;
        }

        private byte[] InterceptData(byte[] data, HookedCall hCall)
        {
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //
            // This is crap. Have to find out other way to do this.
            //
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            MemoryStream msData = new MemoryStream(data);
            
            SetHTextBox(msData);
            lbCallInfo.Text = string.Format("{0}!{1} ({2})", hCall.Hook.Module, hCall.Hook.Function, hCall.Hook.Type.ToString());
            
            modifiyng = true;
            while (modifiyng)
            {
                System.Threading.Thread.Sleep(1);
                System.Windows.Forms.Application.DoEvents();
            }
            lbCallInfo.Text = string.Empty;
            hBox.ByteProvider.ApplyChanges();
            int len = (int) msData.Length;
            byte[] modifiedData = new byte[len];
            msData.Seek(0, SeekOrigin.Begin);
            msData.Read(modifiedData, 0, len);
            msData.Close();
            hBox.ByteProvider.DeleteBytes(0, len);
            hBox.Refresh();
            IntPtr address = new IntPtr(int.Parse(hCall.Arguments[1].ToString()));
            internalsMgr.RemoteProcess.WriteMemory(address, modifiedData);
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

        private void tabMain_Click(object sender, EventArgs e)
        {

        }

        private void btIntercepting_Click(object sender, EventArgs e)
        {
            ToggleIntercept();
        }
    }
}
