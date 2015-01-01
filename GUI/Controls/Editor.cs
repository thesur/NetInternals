using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Controls
{
    public partial class Editor : UserControl
    {
        private MiniFormEditor miniEditor = new MiniFormEditor();

        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {

        }

        public void AddArgument(DataType type, string name, object value)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell cellType = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cellName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cellValue = new DataGridViewTextBoxCell();

            cellType.Value = type;
            cellName.Value = name;
            cellValue.Value = value;

            row.Cells.Add(cellType);
            row.Cells.Add(cellName);
            row.Cells.Add(cellValue);

            dgv.Rows.Add(row);
        }

        public void Clear()
        {
            dgv.Rows.Clear();
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataType)dgv.Rows[e.RowIndex].Cells[0].Value) == DataType.Pointer)
            {
                miniEditor = new MiniFormEditor();
                miniEditor.StartPosition = FormStartPosition.CenterScreen;
                miniEditor.Show();
            }
            else
                miniEditor.Close();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
