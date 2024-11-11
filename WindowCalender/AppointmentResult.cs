using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowCalender
{
    public class AppointmentResult
    {
        public List<Appointment> Appointments { get; set; }
        public bool IsTokenValid { get; set; }

    }
}
