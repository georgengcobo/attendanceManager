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
        /// Create a new student.
        /// </summary>
        /// <param name="newStudent">new student.</param>
        /// <returns><see cref="ResultCodes"/> </returns>
        public Task<(ResultCodes resultCode, string clientMessage)> AddStudentAsync(AddStudent newStudent);

        /// <summary>
        /// Creates a new class for students to attend.
        /// </summary>
        /// <param name="newClass">new teachable class and grade</param>
        /// <returns><see cref="ResultCodes"/> </returns>
        public Task<(ResultCodes resultCode, string clientMessage)> AddClassAsync(AddClass newClass);

        /// <summary>
        /// Registers a student to class.
        /// </summary>
        /// <param name="newRegistrations">new student registration</param>
        /// <returns><see cref="ResultCodes"/> </returns>
        public Task<(ResultCodes resultCode, string clientMessage)> RegisterInClassAsync(ClassRegistration newRegistrations);

        /// <summary>
        /// Marks an attendance register
        /// </summary>
        /// <param name="marRegister">new attendance record</param>
        /// <returns><see cref="ResultCodes"/> </returns>
        public Task<(ResultCodes resultCode, string clientMessage)> CaptureAttendanceAsync(AddAttendance marRegister);

        /// <summary>
        /// Gets List of Classes.
        /// </summary>
        /// <returns><see cref="List&lt;ClassesResponse&gt;"/> </returns>
        public Task<(List<ClassesResponse> classes, ResultCodes resultCode)> GetClassAsync();

        /// <summary>
        /// Gets List of Classes.
        /// </summary>
        /// <param name="filterByClassId">filter by class.</param>
        /// <param name="filterStudentId">filter by specific student.</param>
        /// <returns><see cref="List&lt;RegisteredStudents&gt;"/> </returns>
        public Task<(List<RegisteredStudents> classes, ResultCodes resultCode)> GetRegisteredStudentsAsync(
            int filterByClassId = -1, int filterStudentId = -1);

        /// <summary>
        /// Gets list of all available teachers
        /// </summary>
        /// <returns><see cref="List&lt;TeacherResponse&gt;"/> </returns>
        public Task<(List<TeacherResponse> classes, ResultCodes resultCode, string clientMessage)> GetTeachersAsync(
            int teacherId = -1);

        /// <summary>
        /// Gets list of students.
        /// </summary>
        /// <returns><see cref="List&lt;Student&gt;"/> </returns>
        public Task<(List<Student> classes, ResultCodes resultCode, string clientMessage)> GetStudentsAsync(
            int studentId = -1);

        /// <summary>
        /// Returns period report result.
        /// </summary>
        /// <returns><see cref="PeriodReportResult"/> </returns>
        public Task<(List<PeriodReportResult> classes, ResultCodes resultCode)> PeriodReportAsync(PeriodRequest period);
    }
}
