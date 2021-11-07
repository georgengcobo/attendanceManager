using System;

namespace Attendance.Web.Api.DTO
{
    public struct PeriodRequest
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
