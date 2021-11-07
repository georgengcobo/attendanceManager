
using System;

namespace Attendance.Web.Api.DTO
{
    public struct PeriodReportResult
    {
        public DateTime AttendanceDate { get; set; }

        public string ClassName { get; set; }

        public string Grade { get; set; }

        public string Student { get; set; }

        public string IdNumber { get; set; }

        public bool IsPresent { get; set; }

        public string MarkedBy { get; set; }

    }
}
