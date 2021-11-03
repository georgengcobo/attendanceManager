using System;

namespace Attendance.Web.Api.Models
{
    public struct AttendanceRecord
    {
        public DateTime AttendanceDatetime { get; set; }

        public int AttendanceId { get; set; }

        public int RegistrationId { get; set; }

        public int TeacherId { get; set; }

        public bool IsPresent { get; set; }
    }
}
