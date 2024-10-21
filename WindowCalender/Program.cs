using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowCalender
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HttpClient httpClient = new HttpClient();
            AppointmentService appointmentService = new AppointmentService(httpClient);
            Application.Run(new Form1(appointmentService));
        }
    }
}
