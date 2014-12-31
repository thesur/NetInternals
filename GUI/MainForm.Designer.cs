namespace GUI
{
    partial class MainForm
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
            this.lbLog = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.hBox = new Be.Windows.Forms.HexBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btForward = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageInterception = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.lbCallInfo = new System.Windows.Forms.Label();
            this.tabPageHooks = new System.Windows.Forms.TabPage();
            this.btIntercepting = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabPageInterception.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPageHooks.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLog
            // 
            this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.Location = new System.Drawing.Point(2, 269);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(721, 91);
            this.lbLog.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(32, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "Hook send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(32, 60);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 26);
            this.button2.TabIndex = 3;
            this.button2.Text = "Hook recv";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // hBox
            // 
            this.hBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hBox.BackColor = System.Drawing.Color.Black;
            this.hBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hBox.ForeColor = System.Drawing.Color.Green;
            this.hBox.Location = new System.Drawing.Point(8, 38);
            this.hBox.Name = "hBox";
            this.hBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hBox.Size = new System.Drawing.Size(701, 151);
            this.hBox.StringViewVisible = true;
            this.hBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-1, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Log";
            // 
            // btForward
            // 
            this.btForward.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btForward.Location = new System.Drawing.Point(610, 6);
            this.btForward.Name = "btForward";
            this.btForward.Size = new System.Drawing.Size(99, 26);
            this.btForward.TabIndex = 6;
            this.btForward.Text = "Forward";
            this.btForward.UseVisualStyleBackColor = true;
            this.btForward.Click += new System.EventHandler(this.btForward_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(725, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attachToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // attachToolStripMenuItem
            // 
            this.attachToolStripMenuItem.Name = "attachToolStripMenuItem";
            this.attachToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.attachToolStripMenuItem.Text = "&Attach";
            this.attachToolStripMenuItem.Click += new System.EventHandler(this.attachToolStripMenuItem_Click);
            // 
            // tabPageInterception
            // 
            this.tabPageInterception.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPageInterception.Controls.Add(this.tabMain);
            this.tabPageInterception.Controls.Add(this.tabPageHooks);
            this.tabPageInterception.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageInterception.Location = new System.Drawing.Point(0, 25);
            this.tabPageInterception.Name = "tabPageInterception";
            this.tabPageInterception.SelectedIndex = 0;
            this.tabPageInterception.Size = new System.Drawing.Size(726, 226);
            this.tabPageInterception.TabIndex = 8;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.btIntercepting);
            this.tabMain.Controls.Add(this.lbCallInfo);
            this.tabMain.Controls.Add(this.hBox);
            this.tabMain.Controls.Add(this.btForward);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(718, 200);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Interceptipon";
            this.tabMain.UseVisualStyleBackColor = true;
            this.tabMain.Click += new System.EventHandler(this.tabMain_Click);
            // 
            // lbCallInfo
            // 
            this.lbCallInfo.AutoSize = true;
            this.lbCallInfo.Location = new System.Drawing.Point(8, 13);
            this.lbCallInfo.Name = "lbCallInfo";
            this.lbCallInfo.Size = new System.Drawing.Size(67, 13);
            this.lbCallInfo.TabIndex = 7;
            this.lbCallInfo.Text = "{CallInfo}";
            // 
            // tabPageHooks
            // 
            this.tabPageHooks.Controls.Add(this.button1);
            this.tabPageHooks.Controls.Add(this.button2);
            this.tabPageHooks.Location = new System.Drawing.Point(4, 22);
            this.tabPageHooks.Name = "tabPageHooks";
            this.tabPageHooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHooks.Size = new System.Drawing.Size(718, 200);
            this.tabPageHooks.TabIndex = 1;
            this.tabPageHooks.Text = "Hooks";
            this.tabPageHooks.UseVisualStyleBackColor = true;
            // 
            // btIntercepting
            // 
            this.btIntercepting.Location = new System.Drawing.Point(467, 6);
            this.btIntercepting.Name = "btIntercepting";
            this.btIntercepting.Size = new System.Drawing.Size(137, 26);
            this.btIntercepting.TabIndex = 8;
            this.btIntercepting.Text = "Interception is {status}";
            this.btIntercepting.UseVisualStyleBackColor = true;
            this.btIntercepting.Click += new System.EventHandler(this.btIntercepting_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 365);
            this.Controls.Add(this.tabPageInterception);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Netinternals";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPageInterception.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.tabPageHooks.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Be.Windows.Forms.HexBox hBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btForward;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attachToolStripMenuItem;
        private System.Windows.Forms.TabControl tabPageInterception;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabPageHooks;
        private System.Windows.Forms.Label lbCallInfo;
        private System.Windows.Forms.Button btIntercepting;
    }
}

