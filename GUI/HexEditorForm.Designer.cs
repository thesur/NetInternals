namespace GUI
{
    partial class HexEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HexEditorForm));
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.lbNote = new System.Windows.Forms.Label();
            this.btModify = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hexBox1
            // 
            this.hexBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexBox1.Location = new System.Drawing.Point(12, 72);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(718, 254);
            this.hexBox1.TabIndex = 0;
            // 
            // lbNote
            // 
            this.lbNote.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNote.ForeColor = System.Drawing.Color.Red;
            this.lbNote.Location = new System.Drawing.Point(12, 9);
            this.lbNote.Name = "lbNote";
            this.lbNote.Size = new System.Drawing.Size(718, 60);
            this.lbNote.TabIndex = 1;
            this.lbNote.Text = resources.GetString("lbNote.Text");
            // 
            // btModify
            // 
            this.btModify.Location = new System.Drawing.Point(222, 348);
            this.btModify.Name = "btModify";
            this.btModify.Size = new System.Drawing.Size(116, 23);
            this.btModify.TabIndex = 2;
            this.btModify.Text = "Modify";
            this.btModify.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(381, 348);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(116, 23);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // HexEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 383);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btModify);
            this.Controls.Add(this.lbNote);
            this.Controls.Add(this.hexBox1);
            this.Name = "HexEditorForm";
            this.Text = "Hex editor";
            this.ResumeLayout(false);

        }

        #endregion

        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.Label lbNote;
        private System.Windows.Forms.Button btModify;
        private System.Windows.Forms.Button btCancel;
    }
}