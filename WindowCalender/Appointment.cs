using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowCalender
{
    public class Appointment
    {

        [Required(ErrorMessage = "Id không được để trống")]

        public int id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Note không được để trống.")]
        [StringLength(100, ErrorMessage = "Note không được quá 100 ký tự.")]
        public string reason { get; set; }

        [Range(0, 23, ErrorMessage = "Giờ bắt đầu phải từ 0 đến 23.")]
        public int? fromX { get; set; }
        [Range(0, 59, ErrorMessage = "Phút bắt đầu phải từ 0 đến 59.")]
        public int? fromY { get; set; }
        [Range(0, 23, ErrorMessage = "Giờ bắt đầu phải từ 0 đến 23.")]
        public int? toX { get; set; }
        [Range(0, 59, ErrorMessage = "Phút bắt đầu phải từ 0 đến 59.")]
        public int? toY { get; set; }

        
    }
}
