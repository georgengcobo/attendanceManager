using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Enum;

namespace Attendance.Web.Api.Interfaces
{
    public interface IAdminService
    {

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="newStudent">new student</param>
        /// <returns>creates a new student and returns Student ID</returns>
        public Task<(ResultCodes resultCode, string clientMessage)> AddStudent(AddStudent newStudent);

        /// <summary>
        /// creates a new class for students to attend
        /// </summary>
        /// <param name="newClass">new teachable class and grade</param>
        /// <returns>creates teachable class</returns>
        public Task<(ResultCodes resultCode, string clientMessage)> AddClass(AddClass newClass);

        /// <summary>
        /// Registers a student to class.
        /// </summary>
        /// <param name="newRegistrations">new student registration</param>
        /// <returns>creates a new student and returns Student ID</returns>
        public Task<(ResultCodes resultCode, string clientMessage)> RegisterInClass(ClassRegistration newRegistrations);

        /// <summary>
        /// Marks an attendance register
        /// </summary>
        /// <param name="marRegister">new attendance record</param>
        /// <returns>marks attendance record</returns>
        public Task<(ResultCodes resultCode, string clientMessage)> CaptureAttendance(AddAttendance marRegister);
    }
}
