using System.Collections.Generic;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Enum;
using Attendance.Web.Api.Models;

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

        /// <summary>
        /// Gets List of Classes.
        /// </summary>
        /// <returns>classes</returns>
        public Task<(List<Classes> classes, ResultCodes resultCode, string clientMessage)> GetClass();

        /// <summary>
        /// Gets List of Classes.
        /// </summary>
        /// <param name="filterByClassId">filter by class</param>
        /// <returns>registered students.</returns>
        public Task<(List<RegisteredStudents> classes, ResultCodes resultCode, string clientMessage)>GetRegisteredStudents(int filterByClassId = -1);
    }
}
