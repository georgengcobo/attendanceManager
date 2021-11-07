using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Web.Api.DTO
{
    public struct ClassesResponse
    {
        public int ClassId { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string ClassName { get; set; }

        public string Grade { get; set; }

        public int TeacherId { get; set; }

        public string TeacherName { get; set; }
    }
}
