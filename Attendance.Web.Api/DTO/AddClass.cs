using System.ComponentModel.DataAnnotations;

namespace Attendance.Web.Api.DTO
{
    public struct AddClass
    {
        [Required]
        public string ClassName { get; set; }

        [Required]
        public string Grade { get; set; }

        [Required]
        public int TeacherId { get; set; }
    }
}
