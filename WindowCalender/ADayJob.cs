using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowCalender
{
    public partial class ADayJob : UserControl
    {



        //private bool isEditing = false;

        public bool isEditing { get; set; } = false;
        public Appointment Appointment { get; set; }

        private event EventHandler edited;

        public event EventHandler Edited
        {
            add { edited += value; }
            remove { edited -= value; }
        }

        private event EventHandler deleted;

        public event EventHandler Deleted
        {
            add { deleted += value; }
            remove { deleted -= value; }
        }
        public ADayJob(Appointment appointment)
        {
            InitializeComponent();
            
            this.Appointment = appointment;
            this.txtNote.Text = appointment.reason;
            this.nudFromHours.Value = (decimal)(appointment.fromX == null ? 0 : appointment.fromX);
            this.nudMinuteFrom.Value = (decimal)(appointment.fromY == null ? 0 : appointment.fromY);
            this.nudToHours.Value = (decimal)(appointment.toX == null ? 0 : appointment.toX);
            this.nudMinuteTo.Value = (decimal)(appointment.toY == null ? 0 : appointment.toY);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void setFieldReadOnly(bool readOnly) {
            txtNote.ReadOnly = readOnly;
            nudFromHours.ReadOnly = readOnly;
            nudMinuteFrom.ReadOnly = readOnly;
            nudToHours.ReadOnly = readOnly;
            nudMinuteTo.ReadOnly = readOnly;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (!isEditing)
            {
                setFieldReadOnly(false);
                btnUpdate.Text = "Lưu";
                txtNote.Focus();
                isEditing = true;
            }
            else
            {
                Appointment.reason = txtNote.Text;
                Appointment.fromX = (int)nudFromHours.Value;
                Appointment.fromY = (int)nudMinuteFrom.Value;
                Appointment.toX = (int)nudToHours.Value;
                Appointment.toY = (int)nudMinuteTo.Value;
                //Appointment.From = new Point((int)nudFromHours.Value, (int)nudMinuteFrom.Value);
                //Appointment.To = new Point((int)nudToHours.Value, (int)nudMinuteTo.Value);

                if (edited != null)
                {
                    Console.WriteLine(this);
                    edited(this, new EventArgs());
                }

                //setFieldReadOnly(true);
                //btnUpdate.Text = "Sửa";
                //isEditing = false;  
            }
        }

        public void changeTextValueOfButtonEdit()
        {
            btnUpdate.Text = "Sửa";
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (deleted != null)
                {
                    deleted(this, new EventArgs());
                }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nudFromHours_Validated(object sender, EventArgs e)
        {

        }

        private void nudFromHours_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
