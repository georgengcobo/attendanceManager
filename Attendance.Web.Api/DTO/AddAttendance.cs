using System;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Web.Api.DTO
{
    public struct AddAttendance
    {
        [Required]
        public DateTime AttendanceDate { get; set; }

        [Required]
        public int RegistrationId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public bool IsPresent { get; set; }
    }
}
