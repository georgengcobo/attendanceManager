using System.Collections.Generic;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Enum;
using Attendance.Web.Api.Models;
using TeacherResponse = Attendance.Web.Api.DTO.TeacherResponse;

namespace Attendance.Web.Api.Interfaces
{
    public interface IAdminService
    {

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="newStudent">new student</param>
        /// <returns>creates a new student and returns Student ID</returns>
        public Task<(ResultCodes resultCode, string clientMessage)> AddStudentAsync(AddStudent newStudent);

        /// <summary>
        /// creates a new class for students to attend
        /// </summary>
        /// <param name="newClass">new teachable class and grade</param>
        /// <returns>creates teachable class</returns>
        public Task<(ResultCodes resultCode, string clientMessage)> AddClassAsync(AddClass newClass);

        /// <summary>
        /// Registers a student to class.
        /// </summary>
        /// <param name="newRegistrations">new student registration</param>
        /// <returns>creates a new student and returns Student ID</returns>
        public Task<(ResultCodes resultCode, string clientMessage)> RegisterInClassAsync(ClassRegistration newRegistrations);

        /// <summary>
        /// Marks an attendance register
        /// </summary>
        /// <param name="marRegister">new attendance record</param>
        /// <returns>marks attendance record</returns>
        public Task<(ResultCodes resultCode, string clientMessage)> CaptureAttendanceAsync(AddAttendance marRegister);

        /// <summary>
        /// Gets List of Classes.
        /// </summary>
        /// <returns>classes</returns>
        public Task<(List<ClassesResponse> classes, ResultCodes resultCode, string clientMessage)> GetClassAsync();

        /// <summary>
        /// Gets List of Classes.
        /// </summary>
        /// <param name="filterByClassId">filter by class</param>
        /// <returns>registered students.</returns>
        public Task<(List<RegisteredStudents> classes, ResultCodes resultCode, string clientMessage)>GetRegisteredStudentsAsync(int filterByClassId = -1, int filterStudentId = -1);

        /// <summary>
        /// Gets list of all available teachers
        /// </summary>
        /// <returns>teachers.</returns>
        public Task<(List<TeacherResponse> classes, ResultCodes resultCode, string clientMessage)> GetTeachersAsync(
            int teacherId = -1);

        /// <summary>
        /// Gets list of students.
        /// </summary>
        /// <returns>student.</returns>
        public Task<(List<Student> classes, ResultCodes resultCode, string clientMessage)> GetStudentsAsync(
            int studentId = -1);
    }
}
