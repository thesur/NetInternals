using NetInternals;
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
    public partial class AttachForm : Form
    {
        public Process SelectedProcess { get; set; }
        public AttachForm()
        {
            InitializeComponent();
        }

        private void AttachForm_Load(object sender, EventArgs e)
        {
            LoadProcessesList();
        }

        private void LoadProcessesList()
        {
            lbProcess.Items.Clear();
            foreach (Process p in NetInternals.NetInternalsMgr.Processes())
            {
                lbProcess.Items.Add(p);
            }
            lbProcess.Sorted = true;
            tbSearch.Select();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btAttach_Click(object sender, EventArgs e)
        {
            this.SelectedProcess = (Process) lbProcess.SelectedItem;
            this.Close();
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') && (lbProcess.SelectedIndex != -1))
                btAttach_Click(null, null);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            int c = 0;
            foreach (Process p in lbProcess.Items)
            {
                if (p.Name.StartsWith(tbSearch.Text, StringComparison.InvariantCultureIgnoreCase))
                {
                    lbProcess.SelectedIndex = c;
                    break;
                }
                c++;
            }
            tbSearch.Select();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadProcessesList();
        }
    }
}
