namespace WindowSchedule
{
    partial class Form1
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
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.mnsThemViec = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsHomNay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(666, 374);
            this.panel1.TabIndex = 0;
            // 
            // mnsMain
            // 
            this.mnsMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnsThemViec,
            this.mnsHomNay});
            this.mnsMain.Location = new System.Drawing.Point(0, 0);
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.Size = new System.Drawing.Size(690, 28);
            this.mnsMain.TabIndex = 1;
            this.mnsMain.Text = "menuStrip1";
            this.mnsMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // mnsThemViec
            // 
            this.mnsThemViec.Name = "mnsThemViec";
            this.mnsThemViec.Size = new System.Drawing.Size(90, 24);
            this.mnsThemViec.Text = "Thêm việc";
            // 
            // mnsHomNay
            // 
            this.mnsHomNay.Name = "mnsHomNay";
            this.mnsHomNay.Size = new System.Drawing.Size(83, 24);
            this.mnsHomNay.Text = "Hôm nay";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 426);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnsMain);
            this.Name = "Form1";
            this.Text = "Form";
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
    }
}

