using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Enum;
using Attendance.Web.Api.Interfaces;
using Attendance.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TeacherResponse = Attendance.Web.Api.DTO.TeacherResponse;

namespace Attendance.Web.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository _repo;
        private readonly ILogger<AdminService> _logging;
        private readonly IHttpContextAccessor _httpContextAccessor;


        /// <summary>
        /// Initializes a new instance of the <see cref="AdminService"/> class.
        /// </summary>
        /// <param name="repo">repository where data is stored.</param>
        /// <param name="logging">application logging.</param>
        /// <param name="httpContextAccessor">Http Context to access User token.</param>
        public AdminService(IRepository repo, ILogger<AdminService> logging, IHttpContextAccessor httpContextAccessor)
        {
            this._repo = repo;
            this._logging = logging;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<(ResultCodes resultCode, string clientMessage)> AddStudentAsync(AddStudent newStudent)
        {
            var targetStudent = await this._repo.GetStudentDetailsByIdNumberAsync(newStudent.IdNumber).ConfigureAwait(false);

            if (targetStudent.Equals(default(Student)))
            {
                var teacherId = int.Parse(this._httpContextAccessor.HttpContext?.User.Claims.First(x => x.Type == "UserId").Value ?? string.Empty);

                var result = await this._repo.CreateNewStudentAsync(newStudent).ConfigureAwait(false);

                return result ? (ResultCodes.OkResult, "Student Created Ok") : (ResultCodes.UnexpectedOperationException, "Error creating student in database");
            }

            var msg = $"Student With that IDNumber already exists: {newStudent.IdNumber}";
            this._logging.LogWarning((int)ResultCodes.DuplicateRecordException, msg);
            return (ResultCodes.DuplicateRecordException, msg);

        }

        public async Task<(ResultCodes resultCode, string clientMessage)> AddClassAsync(AddClass newClass)
        {
            var targetClass = await this._repo.GetClassDetailsAsync(newClass).ConfigureAwait(false);

            var targetTeacher = await this._repo.GetTeacherDetailsByKeyAsync(newClass.TeacherId).ConfigureAwait(false);

            if (targetTeacher.FirstOrDefault().Equals(default(Teacher)))
            {

                this._logging.LogWarning((int)ResultCodes.UserNotFoundException, "Specified Teacher does not exists");
                return (ResultCodes.UserNotFoundException, "Specified Teacher does not exists");
            }

            if (targetClass.Equals(default(Classes)))
            {
                var result = await this._repo.CreateNewClassAsync(newClass).ConfigureAwait(false);

                return result ? (ResultCodes.OkResult, "Class Created Ok") : (ResultCodes.UnexpectedOperationException, "Error creating Class in database");
            }

            const string msg = "Specified class already exists";
            this._logging.LogWarning((int)ResultCodes.DuplicateRecordException, msg);
            return (ResultCodes.DuplicateRecordException, msg);
        }

        public async Task<(ResultCodes resultCode, string clientMessage)> RegisterInClassAsync(ClassRegistration newRegistrations)
        {
            var targetClass = await this._repo.GetClassDetailsByIdAsync(newRegistrations.ClassId).ConfigureAwait(false);
            var students = await this._repo.GetStudentDetailsByKeyAsync(newRegistrations.StudentId).ConfigureAwait(false);
            var targetStudent = students.FirstOrDefault();

            if (targetClass.Equals(default(Classes)) || targetStudent.Equals(default(Student)))
            {
                return (ResultCodes.RecordNotFoundException, "Student record or class record not found on the System");
            }

            var registrationDetails = await this._repo.GetRegistrationDetailsAsync(newRegistrations).ConfigureAwait(false);

            if (!registrationDetails.Equals(default(Registration)))
            {
                var msg = $"Student {targetStudent.IdNumber} Already registered for this class {targetClass.ClassName}";
                this._logging.LogWarning((int)ResultCodes.DuplicateRecordException, msg);

                return (ResultCodes.DuplicateRecordException, "Student Already registered for this class");
            }

            var result = await this._repo.CreateNewRegistrationAsync(newRegistrations).ConfigureAwait(false);

            return result ? (ResultCodes.OkResult, "Student registered for class") : (ResultCodes.UnexpectedOperationException, "Error creating Registration in database");
        }

        public async Task<(ResultCodes resultCode, string clientMessage)> CaptureAttendanceAsync(AddAttendance marRegister)
        {
            var targetRegistration = await this._repo.GetStudentRegistrationByKeyAsync(marRegister.RegistrationId).ConfigureAwait(false);
            var targetTeachers = await this._repo.GetTeacherDetailsByKeyAsync(marRegister.TeacherId).ConfigureAwait(false);
            var targetTeacher = targetTeachers.FirstOrDefault();

            if (targetTeacher.Equals(default(Teacher)) || targetRegistration.Equals(default(Registration)))
            {

                return (ResultCodes.RecordNotFoundException, "Teacher record or Student registration record not found on the System");
            }

            var attendanceDetails = await this._repo.GetAttendanceRecordDetailsAsync(marRegister).ConfigureAwait(false);

            if (attendanceDetails.Equals(default(AttendanceRecord)))
            {
                var result = await this._repo.AddAttendanceRecordAsync(marRegister).ConfigureAwait(false);

                return result ? (ResultCodes.OkResult, "Register Marked Ok") : (ResultCodes.UnexpectedOperationException, "Error creating Marking attendance in database");
            }
            var msg = $"Record of attendance for this class on this day already exists {marRegister.AttendanceDate} By  {targetTeacher.Name} {targetTeacher.Surname}";
            this._logging.LogWarning((int)ResultCodes.DuplicateRecordException, msg);

            return (ResultCodes.DuplicateRecordException, "Student attendance already marked for class");
        }

        public async Task<(List<ClassesResponse> classes, ResultCodes resultCode, string clientMessage)> GetClassAsync()
        {
            var result = await this._repo.GetAllClassesAsync().ConfigureAwait(false);

            return (result, ResultCodes.OkResult, "OK");
        }

        public async Task<(List<RegisteredStudents> classes, ResultCodes resultCode, string clientMessage)> GetRegisteredStudentsAsync(int filterByClassId = -1, int filterStudentId = -1)
        {
            var result = await this._repo.GetAllRegisteredStudentsAsync(filterByClassId, filterStudentId).ConfigureAwait(false);

            return (result, ResultCodes.OkResult, "Ok");
        }

        public async Task<(List<TeacherResponse> classes, ResultCodes resultCode, string clientMessage)> GetTeachersAsync(int teacherId = -1)
        {
            var result = await this._repo.GetTeacherDetailsByKeyAsync(teacherId).ConfigureAwait(false);

            var teachers = result.Select(teacher => new TeacherResponse() {FullName = $"{teacher.Name} {teacher.Surname}", TeacherId = teacher.TeacherId}).ToList();

            return (teachers, ResultCodes.OkResult, "Ok");
        }

        public async Task<(List<Student> classes, ResultCodes resultCode, string clientMessage)> GetStudentsAsync(int studentId = -1)
        {
            var result = await this._repo.GetStudentDetailsByKeyAsync(studentId).ConfigureAwait(false);

            return (result, ResultCodes.OkResult, "Ok");
        }
    }
}
