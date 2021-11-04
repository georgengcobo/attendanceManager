using System.ComponentModel.DataAnnotations;

namespace Attendance.Web.Api.DTO
{
    public struct Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
