using System;


namespace Attendance.Web.Api.Models
{
    public struct Classes
    {
        public int ClassId { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string ClassName { get; set; }

        public string Grade { get; set; }

        public int TeacherId { get; set; }


    }
}
