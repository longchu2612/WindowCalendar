namespace WindowCalender
{
    partial class ADayJob
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nudToHours = new System.Windows.Forms.NumericUpDown();
            this.nudMinuteTo = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMinuteFrom = new System.Windows.Forms.NumericUpDown();
            this.nudFromHours = new System.Windows.Forms.NumericUpDown();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudToHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinuteTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinuteFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFromHours)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtNote);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 41);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(608, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 29);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(517, 7);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(85, 29);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nudToHours);
            this.panel2.Controls.Add(this.nudMinuteTo);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.nudMinuteFrom);
            this.panel2.Controls.Add(this.nudFromHours);
            this.panel2.Location = new System.Drawing.Point(242, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 35);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // nudToHours
            // 
            this.nudToHours.Location = new System.Drawing.Point(136, 7);
            this.nudToHours.Name = "nudToHours";
            this.nudToHours.ReadOnly = true;
            this.nudToHours.Size = new System.Drawing.Size(44, 22);
            this.nudToHours.TabIndex = 4;
            // 
            // nudMinuteTo
            // 
            this.nudMinuteTo.Location = new System.Drawing.Point(186, 7);
            this.nudMinuteTo.Name = "nudMinuteTo";
            this.nudMinuteTo.ReadOnly = true;
            this.nudMinuteTo.Size = new System.Drawing.Size(44, 22);
            this.nudMinuteTo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "To";
            // 
            // nudMinuteFrom
            // 
            this.nudMinuteFrom.Location = new System.Drawing.Point(53, 7);
            this.nudMinuteFrom.Name = "nudMinuteFrom";
            this.nudMinuteFrom.ReadOnly = true;
            this.nudMinuteFrom.Size = new System.Drawing.Size(44, 22);
            this.nudMinuteFrom.TabIndex = 1;
            // 
            // nudFromHours
            // 
            this.nudFromHours.Location = new System.Drawing.Point(3, 6);
            this.nudFromHours.Name = "nudFromHours";
            this.nudFromHours.ReadOnly = true;
            this.nudFromHours.Size = new System.Drawing.Size(44, 22);
            this.nudFromHours.TabIndex = 0;
            this.nudFromHours.ValueChanged += new System.EventHandler(this.nudFromHours_ValueChanged);
            this.nudFromHours.Validated += new System.EventHandler(this.nudFromHours_Validated);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(17, 9);
            this.txtNote.Name = "txtNote";
            this.txtNote.ReadOnly = true;
            this.txtNote.Size = new System.Drawing.Size(219, 22);
            this.txtNote.TabIndex = 0;
            // 
            // ADayJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ADayJob";
            this.Size = new System.Drawing.Size(726, 47);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudToHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinuteTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinuteFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFromHours)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMinuteFrom;
        private System.Windows.Forms.NumericUpDown nudFromHours;
        private System.Windows.Forms.NumericUpDown nudToHours;
        private System.Windows.Forms.NumericUpDown nudMinuteTo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
    }
}
