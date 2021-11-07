using System;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Web.Api.DTO
{
    public struct PeriodRequest
    {
        [Required]
        public DateTime StartDateTime { get; set; }
        [Required]
        public DateTime EndDateTime { get; set; }
    }
}
