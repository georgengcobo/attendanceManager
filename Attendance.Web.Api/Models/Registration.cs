using System;

namespace Attendance.Web.Api.Models
{
    public struct Registration
    {
        public string RegistrationId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int ClassId { get; set; }

        public int StudentId { get; set; }
    }
}
