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
    public partial class ErrorForm : Form
    {
        public string Message { get; set; }
        public ErrorForm(string message)
        {
            this.Message = message;
            InitializeComponent();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            lbMessage.Text = Message;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
