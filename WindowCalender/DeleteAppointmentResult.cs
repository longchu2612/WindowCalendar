﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowCalender
{
    public class DeleteAppointmentResult
    {
        public bool Success { get; set; }
        public bool TokenInvalid { get; set; }
        public string ErrorMessage { get; set; }
    }
}

