namespace WindowCalender
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnToday = new System.Windows.Forms.Button();
            this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.btnPreviousMonth = new System.Windows.Forms.Button();
            this.btnThursday = new System.Windows.Forms.Button();
            this.btnSunday = new System.Windows.Forms.Button();
            this.btnSaturday = new System.Windows.Forms.Button();
            this.btnFriday = new System.Windows.Forms.Button();
            this.btnWednesday = new System.Windows.Forms.Button();
            this.btnTuesday = new System.Windows.Forms.Button();
            this.btnMonday = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lstAppointment = new System.Windows.Forms.ListView();
            this.txtNotification = new System.Windows.Forms.Label();
            this.pnlMatrix = new System.Windows.Forms.Panel();
            this.tmNotify = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 686);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnToday);
            this.panel3.Controls.Add(this.dtpDateTime);
            this.panel3.Location = new System.Drawing.Point(50, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(847, 61);
            this.panel3.TabIndex = 1;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // btnToday
            // 
            this.btnToday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnToday.Location = new System.Drawing.Point(548, 15);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(95, 25);
            this.btnToday.TabIndex = 1;
            this.btnToday.Text = "Hôm nay";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpDateTime
            // 
            this.dtpDateTime.Location = new System.Drawing.Point(236, 18);
            this.dtpDateTime.Name = "dtpDateTime";
            this.dtpDateTime.Size = new System.Drawing.Size(263, 22);
            this.dtpDateTime.TabIndex = 0;
            this.dtpDateTime.ValueChanged += new System.EventHandler(this.dtpDateTime_ValueChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnNextMonth);
            this.panel4.Controls.Add(this.btnPreviousMonth);
            this.panel4.Controls.Add(this.btnThursday);
            this.panel4.Controls.Add(this.btnSunday);
            this.panel4.Controls.Add(this.btnSaturday);
            this.panel4.Controls.Add(this.btnFriday);
            this.panel4.Controls.Add(this.btnWednesday);
            this.panel4.Controls.Add(this.btnTuesday);
            this.panel4.Controls.Add(this.btnMonday);
            this.panel4.Location = new System.Drawing.Point(50, 70);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(844, 53);
            this.panel4.TabIndex = 0;
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Location = new System.Drawing.Point(784, 9);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(57, 36);
            this.btnNextMonth.TabIndex = 7;
            this.btnNextMonth.Text = ">>";
            this.btnNextMonth.UseVisualStyleBackColor = true;
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);
            // 
            // btnPreviousMonth
            // 
            this.btnPreviousMonth.Location = new System.Drawing.Point(55, 9);
            this.btnPreviousMonth.Name = "btnPreviousMonth";
            this.btnPreviousMonth.Size = new System.Drawing.Size(57, 36);
            this.btnPreviousMonth.TabIndex = 0;
            this.btnPreviousMonth.Text = "<<";
            this.btnPreviousMonth.UseVisualStyleBackColor = true;
            this.btnPreviousMonth.Click += new System.EventHandler(this.btnPreviousMonth_Click);
            // 
            // btnThursday
            // 
            this.btnThursday.Location = new System.Drawing.Point(409, 4);
            this.btnThursday.Name = "btnThursday";
            this.btnThursday.Size = new System.Drawing.Size(92, 44);
            this.btnThursday.TabIndex = 3;
            this.btnThursday.Text = "Thursday";
            this.btnThursday.UseVisualStyleBackColor = true;
            // 
            // btnSunday
            // 
            this.btnSunday.Location = new System.Drawing.Point(702, 5);
            this.btnSunday.Name = "btnSunday";
            this.btnSunday.Size = new System.Drawing.Size(76, 44);
            this.btnSunday.TabIndex = 6;
            this.btnSunday.Text = "Sunday";
            this.btnSunday.UseVisualStyleBackColor = true;
            // 
            // btnSaturday
            // 
            this.btnSaturday.Location = new System.Drawing.Point(604, 4);
            this.btnSaturday.Name = "btnSaturday";
            this.btnSaturday.Size = new System.Drawing.Size(92, 44);
            this.btnSaturday.TabIndex = 5;
            this.btnSaturday.Text = "Saturday";
            this.btnSaturday.UseVisualStyleBackColor = true;
            // 
            // btnFriday
            // 
            this.btnFriday.Location = new System.Drawing.Point(507, 5);
            this.btnFriday.Name = "btnFriday";
            this.btnFriday.Size = new System.Drawing.Size(91, 43);
            this.btnFriday.TabIndex = 4;
            this.btnFriday.Text = "Friday";
            this.btnFriday.UseVisualStyleBackColor = true;
            // 
            // btnWednesday
            // 
            this.btnWednesday.Location = new System.Drawing.Point(297, 5);
            this.btnWednesday.Name = "btnWednesday";
            this.btnWednesday.Size = new System.Drawing.Size(106, 44);
            this.btnWednesday.TabIndex = 2;
            this.btnWednesday.Text = "Wednesday";
            this.btnWednesday.UseVisualStyleBackColor = true;
            // 
            // btnTuesday
            // 
            this.btnTuesday.Location = new System.Drawing.Point(209, 8);
            this.btnTuesday.Name = "btnTuesday";
            this.btnTuesday.Size = new System.Drawing.Size(82, 41);
            this.btnTuesday.TabIndex = 1;
            this.btnTuesday.Text = "Tuesday";
            this.btnTuesday.UseVisualStyleBackColor = true;
            // 
            // btnMonday
            // 
            this.btnMonday.Location = new System.Drawing.Point(124, 6);
            this.btnMonday.Name = "btnMonday";
            this.btnMonday.Size = new System.Drawing.Size(79, 44);
            this.btnMonday.TabIndex = 0;
            this.btnMonday.Text = "Monday";
            this.btnMonday.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.pnlMatrix);
            this.panel2.Location = new System.Drawing.Point(3, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(894, 613);
            this.panel2.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lstAppointment);
            this.panel5.Controls.Add(this.txtNotification);
            this.panel5.Location = new System.Drawing.Point(48, 397);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(829, 213);
            this.panel5.TabIndex = 2;
            // 
            // lstAppointment
            // 
            this.lstAppointment.HideSelection = false;
            this.lstAppointment.Location = new System.Drawing.Point(173, 42);
            this.lstAppointment.Name = "lstAppointment";
            this.lstAppointment.Size = new System.Drawing.Size(493, 147);
            this.lstAppointment.TabIndex = 1;
            this.lstAppointment.UseCompatibleStateImageBehavior = false;
            this.lstAppointment.View = System.Windows.Forms.View.Details;
            this.lstAppointment.Visible = false;
            // 
            // txtNotification
            // 
            this.txtNotification.AutoSize = true;
            this.txtNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotification.Location = new System.Drawing.Point(296, 11);
            this.txtNotification.Name = "txtNotification";
            this.txtNotification.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNotification.Size = new System.Drawing.Size(37, 16);
            this.txtNotification.TabIndex = 0;
            this.txtNotification.Text = "Text";
            this.txtNotification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtNotification.Visible = false;
            // 
            // pnlMatrix
            // 
            this.pnlMatrix.Location = new System.Drawing.Point(48, 59);
            this.pnlMatrix.Name = "pnlMatrix";
            this.pnlMatrix.Size = new System.Drawing.Size(829, 332);
            this.pnlMatrix.TabIndex = 1;
            this.pnlMatrix.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMatrix_Paint);
            // 
            // tmNotify
            // 
            this.tmNotify.Enabled = true;
            this.tmNotify.Interval = 60000;
            this.tmNotify.Tick += new System.EventHandler(this.tmNotify_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 710);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calendar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlMatrix;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.DateTimePicker dtpDateTime;
        private System.Windows.Forms.Button btnThursday;
        private System.Windows.Forms.Button btnSunday;
        private System.Windows.Forms.Button btnSaturday;
        private System.Windows.Forms.Button btnFriday;
        private System.Windows.Forms.Button btnWednesday;
        private System.Windows.Forms.Button btnTuesday;
        private System.Windows.Forms.Button btnMonday;
        private System.Windows.Forms.Button btnPreviousMonth;
        private System.Windows.Forms.Button btnNextMonth;
        private System.Windows.Forms.Timer tmNotify;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label txtNotification;
        private System.Windows.Forms.ListView lstAppointment;
    }
}

