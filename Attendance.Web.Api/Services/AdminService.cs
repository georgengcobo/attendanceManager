using System.Collections.Generic;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Enum;
using Attendance.Web.Api.Interfaces;
using Attendance.Web.Api.Models;
using Microsoft.Extensions.Logging;

namespace Attendance.Web.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository _repo;
        private readonly ILogger<AdminService> _logging;

        public AdminService(IRepository repo, ILogger<AdminService> logging)
        {
            this._repo = repo;
            this._logging = logging;
        }

        public async Task<(ResultCodes resultCode, string clientMessage)> AddStudent(AddStudent newStudent)
        {
            var targetStudent = await this._repo.GetStudentDetailsByIdNumberAsync(newStudent.IdNumber).ConfigureAwait(false);

            if (targetStudent.Equals(default(Student)))
            {
                var result = await this._repo.CreateNewStudentAsync(newStudent).ConfigureAwait(false);

                return result ? (ResultCodes.OkResult, "Student Created Ok") : (ResultCodes.UnexpectedOperationException, "Error creating student in database");
            }

            var msg = $"Student With that IDNumber already exists: {newStudent.IdNumber}";
            this._logging.LogWarning((int)ResultCodes.DuplicateRecordException, msg);
            return (ResultCodes.DuplicateRecordException, msg);

        }

        public async Task<(ResultCodes resultCode, string clientMessage)> AddClass(AddClass newClass)
        {
            var targetClass = await this._repo.GetClassDetailsAsync(newClass).ConfigureAwait(false);

            var targetTeacher = await this._repo.GetTeacherDetailsByKeyAsync(newClass.TeacherId).ConfigureAwait(false);

            if (targetTeacher.Equals(default(Teacher)))
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

        public async Task<(ResultCodes resultCode, string clientMessage)> RegisterInClass(ClassRegistration newRegistrations)
        {
            var targetClass = await this._repo.GetClassDetailsByIdAsync(newRegistrations.ClassId).ConfigureAwait(false);
            var targetStudent = await this._repo.GetStudentDetailsByKeyAsync(newRegistrations.StudentId).ConfigureAwait(false);

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

        public async Task<(ResultCodes resultCode, string clientMessage)> CaptureAttendance(AddAttendance marRegister)
        {
            var targetRegistration = await this._repo.GetStudentRegistrationByKeyAsync(marRegister.RegistrationId).ConfigureAwait(false);
            var targetTeacher = await this._repo.GetTeacherDetailsByKeyAsync(marRegister.TeacherId).ConfigureAwait(false);

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

        public async Task<(List<Classes> classes, ResultCodes resultCode, string clientMessage)> GetClass()
        {
            var result = await this._repo.GetAllClassesAsync().ConfigureAwait(false);

            return (result, ResultCodes.OkResult, "OK");
        }

        public async Task<(List<RegisteredStudents> classes, ResultCodes resultCode, string clientMessage)> GetRegisteredStudents(int filterByClassId = -1)
        {
            var result = await this._repo.GetAllRegisteredStudentsAsync(filterByClassId).ConfigureAwait(false);

            return (result, ResultCodes.OkResult, "Ok");
        }
    }
}
