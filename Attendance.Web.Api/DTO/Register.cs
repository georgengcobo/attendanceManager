using System.ComponentModel.DataAnnotations;

namespace Attendance.Web.Api.DTO
{
    public struct Register
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
