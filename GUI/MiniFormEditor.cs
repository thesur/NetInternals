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

        public MiniFormEditor()
        {
            InitializeComponent();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            hexEditorForm = new HexEditorForm();
            hexEditorForm.ShowDialog();
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
