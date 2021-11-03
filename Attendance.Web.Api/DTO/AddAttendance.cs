using System;

namespace Attendance.Web.Api.DTO
{
    public struct AddAttendance
    {
        public DateTime AttendanceDate { get; set; }

        public int RegistrationId { get; set; }

        public int TeacherId { get; set; }

        public bool IsPresent { get; set; }
    }
}
