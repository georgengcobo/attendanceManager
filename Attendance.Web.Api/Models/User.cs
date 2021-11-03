using System;
namespace Attendance.Web.Api.Models
{

    public struct User
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegisteredTime { get; set; }

        public int UserId { get; set; }

    }
}
