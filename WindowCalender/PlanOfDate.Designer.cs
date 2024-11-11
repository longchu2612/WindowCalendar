namespace WindowCalender
{
    partial class PlanOfDate
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnTomorrow = new System.Windows.Forms.Button();
            this.dtpkDate = new System.Windows.Forms.DateTimePicker();
            this.pnlJob = new System.Windows.Forms.Panel();
            this.lblMessageBox = new System.Windows.Forms.Label();
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.mnsHomNay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsThemViec = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlJob.SuspendLayout();
            this.mnsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pnlJob);
            this.panel1.Location = new System.Drawing.Point(9, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 321);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnYesterday);
            this.panel3.Controls.Add(this.btnTomorrow);
            this.panel3.Controls.Add(this.dtpkDate);
            this.panel3.Location = new System.Drawing.Point(10, 14);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(594, 28);
            this.panel3.TabIndex = 1;
            // 
            // btnYesterday
            // 
            this.btnYesterday.Location = new System.Drawing.Point(119, 2);
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(2);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(74, 20);
            this.btnYesterday.TabIndex = 1;
            this.btnYesterday.Text = "Hôm qua";
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnTomorrow
            // 
            this.btnTomorrow.Location = new System.Drawing.Point(396, 2);
            this.btnTomorrow.Margin = new System.Windows.Forms.Padding(2);
            this.btnTomorrow.Name = "btnTomorrow";
            this.btnTomorrow.Size = new System.Drawing.Size(80, 24);
            this.btnTomorrow.TabIndex = 2;
            this.btnTomorrow.Text = "Ngày Mai";
            this.btnTomorrow.UseVisualStyleBackColor = true;
            this.btnTomorrow.Click += new System.EventHandler(this.btnTomorrow_Click);
            // 
            // dtpkDate
            // 
            this.dtpkDate.Location = new System.Drawing.Point(205, 4);
            this.dtpkDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpkDate.Name = "dtpkDate";
            this.dtpkDate.Size = new System.Drawing.Size(181, 20);
            this.dtpkDate.TabIndex = 0;
            this.dtpkDate.ValueChanged += new System.EventHandler(this.dtpkDate_ValueChanged);
            // 
            // pnlJob
            // 
            this.pnlJob.Controls.Add(this.lblMessageBox);
            this.pnlJob.Location = new System.Drawing.Point(10, 46);
            this.pnlJob.Margin = new System.Windows.Forms.Padding(2);
            this.pnlJob.Name = "pnlJob";
            this.pnlJob.Size = new System.Drawing.Size(594, 262);
            this.pnlJob.TabIndex = 0;
            this.pnlJob.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlJob_Paint);
            // 
            // lblMessageBox
            // 
            this.lblMessageBox.AutoSize = true;
            this.lblMessageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageBox.Location = new System.Drawing.Point(215, 9);
            this.lblMessageBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessageBox.Name = "lblMessageBox";
            this.lblMessageBox.Size = new System.Drawing.Size(41, 13);
            this.lblMessageBox.TabIndex = 0;
            this.lblMessageBox.Text = "label2";
            this.lblMessageBox.Click += new System.EventHandler(this.lblMessageBox_Click);
            // 
            // mnsMain
            // 
            this.mnsMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnsHomNay,
            this.mnsThemViec});
            this.mnsMain.Location = new System.Drawing.Point(0, 0);
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mnsMain.Size = new System.Drawing.Size(635, 24);
            this.mnsMain.TabIndex = 1;
            this.mnsMain.Text = "menuStrip1";
            this.mnsMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // mnsHomNay
            // 
            this.mnsHomNay.Name = "mnsHomNay";
            this.mnsHomNay.Size = new System.Drawing.Size(68, 20);
            this.mnsHomNay.Text = "Hôm nay";
            this.mnsHomNay.Click += new System.EventHandler(this.mnsHomNay_Click);
            // 
            // mnsThemViec
            // 
            this.mnsThemViec.Name = "mnsThemViec";
            this.mnsThemViec.Size = new System.Drawing.Size(73, 20);
            this.mnsThemViec.Text = "Thêm việc";
            this.mnsThemViec.Click += new System.EventHandler(this.mnsThemViec_Click);
            // 
            // PlanOfDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 362);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnsMain);
            this.MainMenuStrip = this.mnsMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PlanOfDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch hôm nay";
            this.Load += new System.EventHandler(this.PlanOfDate_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlJob.ResumeLayout(false);
            this.pnlJob.PerformLayout();
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip mnsMain;
        private System.Windows.Forms.ToolStripMenuItem mnsThemViec;
        private System.Windows.Forms.ToolStripMenuItem mnsHomNay;
        private System.Windows.Forms.Panel pnlJob;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpkDate;
        private System.Windows.Forms.Button btnYesterday;
        private System.Windows.Forms.Button btnTomorrow;
        private System.Windows.Forms.Label lblMessageBox;
    }
}