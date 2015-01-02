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
        

        bool modifiyng = false;
        bool intercepting = false;

        public MainForm()
        {
            try
            {
                Program.internalsMgr = new NetInternalsMgr();
            }
            catch (Exception ex)
            {
                ErrorForm errorFrm = new ErrorForm(ex.Message);
                errorFrm.ShowDialog();
                this.Close();
            }

            Program.internalsMgr.OnNewLog += internalsMgr_OnNewLog;
            Program.internalsMgr.OnHookedCall += internalsMgr_OnHookedCall;
            InitializeComponent();
        }

        private void ProcessSend(HookedCall hc)
        {
            editor.AddArgument(new Argument(DataType.Int16, "Socket", hc.Arguments[0]));
            editor.AddArgument(new Argument(DataType.Pointer, "Buffer", hc.Arguments[1]));
            editor.AddArgument(new Argument(DataType.Int16, "Length", hc.Arguments[2]));
            editor.AddArgument(new Argument(DataType.Int16, "Flags", hc.Arguments[3]));
        }

        private void ProcessRecv(HookedCall hc)
        {
            editor.AddArgument(new Argument(DataType.Int16, "Socket", hc.Arguments[0]));
            editor.AddArgument(new Argument(DataType.Pointer, "Buffer", hc.Arguments[1]));
            editor.AddArgument(new Argument(DataType.Int16, "Length", hc.Arguments[2]));
            editor.AddArgument(new Argument(DataType.Int16, "Flags", hc.Arguments[3]));
        }

        private void ProcessReadFile(HookedCall hc)
        {
            editor.AddArgument(new Argument(DataType.Int16, "hFile", hc.Arguments[0]));
            editor.AddArgument(new Argument(DataType.Pointer, "lpBuffer", hc.Arguments[1]));
            editor.AddArgument(new Argument(DataType.Int16, "nNumberOfBytesToRead", hc.Arguments[2]));
            editor.AddArgument(new Argument(DataType.Int16, "lpNumberOfBytesRead", hc.Arguments[3]));
            editor.AddArgument(new Argument(DataType.Int16, "lpOverlapped", hc.Arguments[4]));
        }

        void internalsMgr_OnHookedCall(HookedCall hc)
        {
            if (!intercepting)
                return;
            editor.Clear();
            
            if (hc.Hook.Function == "send")
                ProcessSend(hc);
            else if (hc.Hook.Function == "recv")
                ProcessRecv(hc);
            else if (hc.Hook.Function == "ReadFile")
                ProcessReadFile(hc);

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


        private void button1_Click(object sender, EventArgs e)
        {
            Hook hook = new Hook("WS2_32", "send", HookType.PreCall);
            Response r = Program.internalsMgr.Hook(hook);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hook hook = new Hook("WS2_32", "recv", HookType.PostCall);
            Response r = Program.internalsMgr.Hook(hook);
        }

        private void btHookReadFile_Click(object sender, EventArgs e)
        {
            Hook hook = new Hook("Kernel32", "ReadFile", HookType.PostCall);
            Response r = Program.internalsMgr.Hook(hook);
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
                Program.internalsMgr.Inject(fAttach.SelectedProcess);
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
