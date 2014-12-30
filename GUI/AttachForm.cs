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
            foreach(Process p in NetInternals.NetInternalsMgr.Processes())
            {
                lbProcess.Items.Add(p);
            }
            lbProcess.Sorted = true;
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
    }
}
