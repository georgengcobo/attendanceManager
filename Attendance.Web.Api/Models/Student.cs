using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Web.Api.Models
{
    public struct Student
    {
        public int StudentId { get; set; }

        public DateTime DateTimeAdded { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string IdNumber { get; set; }

    }
}
