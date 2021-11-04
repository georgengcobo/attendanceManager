using System.ComponentModel.DataAnnotations;

namespace Attendance.Web.Api.DTO
{
    public struct AddStudent
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string IdNumber { get; set; }
    }
}
