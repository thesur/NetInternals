namespace GUI
{
    partial class AttachForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbProcess = new System.Windows.Forms.ListBox();
            this.btAttach = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbProcess
            // 
            this.lbProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProcess.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProcess.FormattingEnabled = true;
            this.lbProcess.Location = new System.Drawing.Point(12, 12);
            this.lbProcess.Name = "lbProcess";
            this.lbProcess.Size = new System.Drawing.Size(391, 264);
            this.lbProcess.TabIndex = 0;
            // 
            // btAttach
            // 
            this.btAttach.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAttach.Location = new System.Drawing.Point(12, 293);
            this.btAttach.Name = "btAttach";
            this.btAttach.Size = new System.Drawing.Size(101, 36);
            this.btAttach.TabIndex = 1;
            this.btAttach.Text = "&Attach";
            this.btAttach.UseVisualStyleBackColor = true;
            this.btAttach.Click += new System.EventHandler(this.btAttach_Click);
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.Location = new System.Drawing.Point(302, 293);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(101, 36);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "&Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // AttachForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 350);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btAttach);
            this.Controls.Add(this.lbProcess);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AttachForm";
            this.Text = "Process list";
            this.Load += new System.EventHandler(this.AttachForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbProcess;
        private System.Windows.Forms.Button btAttach;
        private System.Windows.Forms.Button btCancel;
    }
}