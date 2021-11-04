using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Web.Api.Models
{
    public struct RegisteredStudents
    {

        public int ClassId { get; set; }

        public string ClassName { get; set; }

        public int Grade { get; set; }

        public int RegistrationId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string IdNumber { get; set; }

        public int TeacherId { get; set; }

        public string TeacherName { get; set; }

    }
}
