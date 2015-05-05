namespace PinGo
{
    partial class MainWin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.resultBox = new System.Windows.Forms.ListBox();
            this.pingBtn = new System.Windows.Forms.Button();
            this.hostName = new System.Windows.Forms.TextBox();
            this.scanBtn = new System.Windows.Forms.Button();
            this.clrBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resultBox
            // 
            this.resultBox.BackColor = System.Drawing.SystemColors.Menu;
            this.resultBox.FormattingEnabled = true;
            this.resultBox.ItemHeight = 12;
            this.resultBox.Location = new System.Drawing.Point(43, 59);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(488, 268);
            this.resultBox.TabIndex = 0;
            this.resultBox.SelectedIndexChanged += new System.EventHandler(this.resultBox_SelectedIndexChanged);
            // 
            // pingBtn
            // 
            this.pingBtn.Location = new System.Drawing.Point(339, 15);
            this.pingBtn.Name = "pingBtn";
            this.pingBtn.Size = new System.Drawing.Size(70, 23);
            this.pingBtn.TabIndex = 1;
            this.pingBtn.Text = "PinGo!";
            this.pingBtn.UseVisualStyleBackColor = true;
            this.pingBtn.Click += new System.EventHandler(this.pingBtn_Click);
            // 
            // hostName
            // 
            this.hostName.Location = new System.Drawing.Point(43, 17);
            this.hostName.Name = "hostName";
            this.hostName.Size = new System.Drawing.Size(262, 21);
            this.hostName.TabIndex = 2;
            // 
            // scanBtn
            // 
            this.scanBtn.Location = new System.Drawing.Point(438, 15);
            this.scanBtn.Name = "scanBtn";
            this.scanBtn.Size = new System.Drawing.Size(40, 23);
            this.scanBtn.TabIndex = 3;
            this.scanBtn.Text = "扫描";
            this.scanBtn.UseVisualStyleBackColor = true;
            this.scanBtn.Click += new System.EventHandler(this.scanBtn_Click);
            // 
            // clrBtn
            // 
            this.clrBtn.Location = new System.Drawing.Point(494, 15);
            this.clrBtn.Name = "clrBtn";
            this.clrBtn.Size = new System.Drawing.Size(40, 23);
            this.clrBtn.TabIndex = 4;
            this.clrBtn.Text = "清屏";
            this.clrBtn.UseVisualStyleBackColor = true;
            this.clrBtn.Click += new System.EventHandler(this.clrBtn_Click);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.clrBtn);
            this.Controls.Add(this.scanBtn);
            this.Controls.Add(this.hostName);
            this.Controls.Add(this.pingBtn);
            this.Controls.Add(this.resultBox);
            this.Name = "MainWin";
            this.Text = "PinGo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox resultBox;
        private System.Windows.Forms.Button pingBtn;
        private System.Windows.Forms.TextBox hostName;
        private System.Windows.Forms.Button scanBtn;
        private System.Windows.Forms.Button clrBtn;
    }
}

