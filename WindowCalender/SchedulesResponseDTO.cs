using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowCalender
{
    public class SchedulesResponseDTO
    {
        public List<Appointment> scheduleList { get; set; }
        public string accessToken { get; set; } 
    }
}
