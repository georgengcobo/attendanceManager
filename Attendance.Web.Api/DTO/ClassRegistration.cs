using System.ComponentModel.DataAnnotations;

namespace Attendance.Web.Api.DTO
{
    public struct ClassRegistration
    {
        [Required]
        public int ClassId { get; set; }

        [Required]
        public int StudentId { get; set; }
    }
}
