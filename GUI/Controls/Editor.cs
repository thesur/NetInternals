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
        private MiniFormEditor miniEditor;

        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {

        }

        internal void AddArgument(Argument argument)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Tag = argument;

            DataGridViewTextBoxCell cellType = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cellName = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell cellValue = new DataGridViewTextBoxCell();
            
            cellType.Value = argument.Type;
            cellName.Value = argument.Name;
            cellValue.Value = argument.ToString() ;

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
                miniEditor = new MiniFormEditor(new IntPtr((int)((Argument)dgv.Rows[e.RowIndex].Tag).Value));
                miniEditor.StartPosition = FormStartPosition.CenterScreen;
                miniEditor.ShowDialog();

                if (miniEditor.data != null)
                {
                    dgv.Rows[e.RowIndex].Cells[2].Value = "MODIFICADO";
                }
            }
            else
            {
                if (miniEditor != null)
                    miniEditor.Close();
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
