using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class MiniFormEditor : Form
    {
        HexEditorForm hexEditorForm;
        IntPtr address;
        int bytesToRead = 200;
        internal byte[] data;

        public MiniFormEditor(IntPtr address)
        {
            this.address = address;
            InitializeComponent();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            hexEditorForm = new HexEditorForm(address, bytesToRead);
            hexEditorForm.ShowDialog();

            if (hexEditorForm.modified)
                this.data = hexEditorForm.data;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MiniFormEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
