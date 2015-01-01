using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class HexEditorForm : Form
    {
        IntPtr address;
        int bytesToRead;
        internal bool modified;
        internal byte[] data;

        private MemoryStream ms;

        public HexEditorForm(IntPtr address, int bytesToRead)
        {
            this.modified = false;
            this.address = address;
            this.bytesToRead = bytesToRead;

            InitializeComponent();
        }

        private void Read()
        {
            byte[] data = Program.internalsMgr.RemoteProcess.ReadMemory(address, bytesToRead);
            ms = new MemoryStream(data);
            hBox.ByteProvider = new DynamicFileByteProvider(ms);
        }

        private void HexEditorForm_Load(object sender, EventArgs e)
        {
            Read();
        }

        private IntPtr SaveResults()
        {
            if (ms == null)
                return IntPtr.Zero;

            hBox.ByteProvider.ApplyChanges();
            int len = (int) ms.Length;
            data = new byte[len];
            ms.Seek(0, SeekOrigin.Begin);
            ms.Read(data, 0, len);
            ms.Close();

            IntPtr addr = Program.internalsMgr.RemoteProcess.VirtualAllocEx((uint)len);
            Program.internalsMgr.RemoteProcess.WriteMemory(addr, data);

            return addr;
        }
        private void btModify_Click(object sender, EventArgs e)
        {
            IntPtr address = SaveResults();
            modified = true;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            modified = false;
            this.Close();
        }
    }
}
