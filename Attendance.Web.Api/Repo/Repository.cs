using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Enum;
using Attendance.Web.Api.Interfaces;
using Attendance.Web.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;
using MySqlConnector;

namespace Attendance.Web.Api.Repo
{

    /// <summary>
    /// initiates repository.
    /// </summary>
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;
        private readonly string _sqlConfig;
        private readonly int _dbTimeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="logger">logger implementation.</param>
        /// <param name="config">configure application settings.</param>
        public Repository(ILogger<Repository> logger, IConfiguration config)
        {
            this._logger = logger;
            this._sqlConfig = config["ConnectionStrings:MainApp"];
            this._dbTimeout = Convert.ToInt32(config["ConnectionStrings:DBTimeOut"]);
        }


        /// <inheritdoc/>
        public async Task<(Teacher, ResultCodes code)> GetUserDetailsByEmailAsync(string emailAddress)
        {
            var queryParams = new { UserEmail = emailAddress };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.TeachersTable} WHERE Email = @UserEmail";

            var (result, code) =  await this.TryQueryDbAsync<Teacher>(query, queryParams).ConfigureAwait(false);
            return (result.FirstOrDefault(), code);
        }

        /// <inheritdoc/>
        public async Task<(int, bool)> CreateNewUserAsync(Register userDetails)
        {
            var query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(DatabaseTables.Database);
            query.Append(DatabaseTables.DbSchema);
            query.Append(DatabaseTables.TeachersTable);
            query.AppendLine(" ");
            query.AppendLine(" (Name, Surname, Email, Password) ");
            query.AppendLine(" VALUES (@UserName, @UserSurname, @EmailAddress, MD5(@UserPassword)); ");
            query.AppendLine(" select LAST_INSERT_ID(); ");

            var queryParams = new
            {
                UserName = userDetails.Name,
                UserSurname = userDetails.Surname,
                EmailAddress = userDetails.Email,
                UserPassword = userDetails.Password,
            };

            var (insertId, resultCode) = await this.TryExecuteDbAsync<int>(query.ToString(), queryParams).ConfigureAwait(false);
            return (insertId, resultCode == ResultCodes.OkResult );
        }

        /// <inheritdoc />
        public async Task<(Student, ResultCodes code)> GetStudentDetailsByIdNumberAsync(string identityNumber)
        {
            var parameters = new { Param1 = identityNumber };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.StudentsTable} WHERE IdNumber = @Param1";

            var (result, code) = await this.TryQueryDbAsync<Student>(query, parameters).ConfigureAwait(false);

            return (result.FirstOrDefault(), code);
        }

        /// <inheritdoc />
        public async Task<bool> CreateNewStudentAsync(AddStudent newStudent)
        {
            var query = new StringBuilder();

            query.Append("INSERT INTO ");
            query.Append(DatabaseTables.Database);
            query.Append(DatabaseTables.DbSchema);
            query.Append(DatabaseTables.StudentsTable);
            query.AppendLine(" ");
            query.AppendLine(" (Name, Surname, IdNumber) ");
            query.AppendLine(" VALUES (@Param1, @Param2, @Param3); ");
            query.AppendLine(" select LAST_INSERT_ID(); ");

            var queryParams = new
            {
                Param1 = newStudent.Name,
                Param2 = newStudent.Surname,
                Param3 = newStudent.IdNumber,
            };

            var ( _ , resultCode) = await this.TryExecuteDbAsync<int>(query.ToString(), queryParams).ConfigureAwait(false);
            return (resultCode == ResultCodes.OkResult);
        }

        /// <inheritdoc />
        public async Task<bool> CreateNewClassAsync(AddClass newClass)
        {
            var query = new StringBuilder();

            query.Append("INSERT INTO ");
            query.Append(DatabaseTables.Database);
            query.Append(DatabaseTables.DbSchema);
            query.Append(DatabaseTables.ClassesTable);
            query.AppendLine(" ");
            query.AppendLine(" (ClassName, Grade, TeacherId) ");
            query.AppendLine(" VALUES (@Param1, @Param2, @Param3); ");
            query.AppendLine(" select LAST_INSERT_ID(); ");

            var queryParams = new
            {
                Param1 = newClass.ClassName,
                Param2 = newClass.Grade,
                Param3 = newClass.TeacherId,
            };

            var (_, resultCode) = await this.TryExecuteDbAsync<int>(query.ToString(), queryParams).ConfigureAwait(false);
            return (resultCode == ResultCodes.OkResult);
        }

        /// <inheritdoc />
        public async Task<(Classes, ResultCodes code)> GetClassDetailsAsync(AddClass targetClass)
        {
            var parameters = new { Param1 = targetClass.ClassName, Param2 = targetClass.Grade, Param3 = targetClass.TeacherId };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.ClassesTable} WHERE ClassName = @Param1 AND Grade = @Param2 AND TeacherId = @Param3";

            var (result, code) = await this.TryQueryDbAsync<Classes>(query, parameters).ConfigureAwait(false);

            return (result.FirstOrDefault(), code);
        }

        /// <inheritdoc />
        public async Task<(Classes, ResultCodes code)> GetClassDetailsByIdAsync(int classId)
        {
            var parameters = new { Param1 = classId };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.ClassesTable} WHERE ClassId = @Param1";

            var (result, code) = await this.TryQueryDbAsync<Classes>(query, parameters).ConfigureAwait(false);

            return (result.FirstOrDefault(), code);
        }

        /// <inheritdoc />
        public async Task<(List<ClassesResponse>, ResultCodes code)> GetAllClassesAsync()
        {
            var query = new StringBuilder();
             query.AppendLine($" SELECT C.*, CONCAT(T.Name,' ' ,T.Surname)AS TeacherName FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.ClassesTable} C ");
             query.AppendLine($" JOIN {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.TeachersTable} T ");
             query.AppendLine(" ON C.TeacherId = T.TeacherId ");

            var (result, code) = await this.TryQueryDbAsync<ClassesResponse>(query.ToString(), null).ConfigureAwait(false);

            return (result.ToList(), code);
        }

        /// <inheritdoc />
        public async Task<(List<RegisteredStudents>, ResultCodes code)> GetAllRegisteredStudentsAsync(
            int filterByClassId = -1, int filterByStudentId = -1)
        {
            var query = new StringBuilder();

            object parameters = null;

            query.Append("SELECT C.ClassId, C.ClassName, C.Grade, R.RegistrationId, S.StudentId ");
            query.AppendLine(", CONCAT(S.Name,' ' ,S.Surname)AS StudentName, S.IdNumber ");
            query.AppendLine(", T.TeacherId, CONCAT(T.Name,' ' ,T.Surname)AS TeacherName ");
            query.AppendLine($" FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.ClassesTable} C ");
            query.AppendLine($" JOIN {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.RegistrationsTable} R ");
            query.AppendLine("ON C.ClassId =  R.ClassId ");
            query.AppendLine($" JOIN {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.StudentsTable} S ");
            query.AppendLine(" ON R.StudentId = S.StudentId ");
            query.AppendLine($" JOIN {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.TeachersTable} T ");
            query.AppendLine(" ON C.TeacherId = T.TeacherId ");

            if (filterByClassId != -1 && filterByStudentId != -1)
            {
                query.AppendLine($" WHERE  C.ClassId  = @Param1  AND  S.StudentId = @Param2");
                parameters = new { Param1 = filterByClassId, @Param2 = filterByStudentId };
            }

            if (filterByClassId != -1 && filterByStudentId == -1)
            {
                query.AppendLine($" WHERE  C.ClassId  = @Param1");
                parameters = new { Param1 = filterByClassId};
            }

            if (filterByClassId == -1 && filterByStudentId != -1)
            {
                query.AppendLine($" WHERE   S.StudentId = @Param1");
                parameters = new { Param1 = filterByStudentId };
            }

            var (result, code) = await this.TryQueryDbAsync<RegisteredStudents>(query.ToString(), parameters).ConfigureAwait(false);

            return (result.ToList(), code);
        }

        /// <inheritdoc />
        public async Task<(List<PeriodReportResult>, ResultCodes)> GetPeriodReportAsync(PeriodRequest period)
        {
            var query = new StringBuilder();

            query.Append("SELECT A.AttendanceDatetime As AttendanceDate, ");
            query.AppendLine(" C.ClassName, C.Grade, CONCAT(S.Name, ' ' ,S.Surname) AS Student, S.IdNumber, A.IsPresent, ");
            query.AppendLine(" CONCAT(T.Name, ' ' , T.Surname) AS MarkedBy ");
            query.AppendLine($" FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.AttendanceTable} A ");
            query.AppendLine($" JOIN {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.RegistrationsTable} R ");
            query.AppendLine(" ON A.RegistrationId = R.RegistrationId ");
            query.AppendLine($" JOIN {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.TeachersTable} T ");
            query.AppendLine(" ON A.TeacherId = T.TeacherId ");
            query.AppendLine($" JOIN {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.ClassesTable} C ");
            query.AppendLine(" ON C.ClassId =  R.ClassId ");
            query.AppendLine($" JOIN {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.StudentsTable} S ");
            query.AppendLine(" ON S.StudentId = R.StudentId ");
            query.AppendLine(" WHERE  A.AttendanceDatetime >= @Param1 AND A.AttendanceDatetime <= @Param2");

            var parameters = new { Param1 = period.StartDateTime, @Param2 = period.EndDateTime };
            var (result, code) = await this.TryQueryDbAsync<PeriodReportResult>(query.ToString(), parameters).ConfigureAwait(false);

            return (result.ToList(), code);
        }

        /// <inheritdoc />
        public async Task<List<Student>> GetStudentDetailsByKeyAsync(int studentKey = -1)
        {
            object parameters = null;

            var query = new StringBuilder();

            query.Append($"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.StudentsTable} ");

            if (studentKey != -1)
            {
                query.AppendLine(" WHERE StudentId = @Param1 ");

                parameters = new { Param1 = studentKey };
            }

            var (result, _) = await this.TryQueryDbAsync<Student>(query.ToString(), parameters).ConfigureAwait(false);

            return result.ToList();
        }

        /// <inheritdoc />
        public async Task<Registration> GetRegistrationDetailsAsync(ClassRegistration registration)
        {
            var parameters = new { Param1 = registration.ClassId, Param2 = registration.StudentId};
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.RegistrationsTable} WHERE ClassId = @Param1 AND StudentId = @Param2";

            var (result, _) = await this.TryQueryDbAsync<Registration>(query, parameters).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<bool> CreateNewRegistrationAsync(ClassRegistration registration)
        {
            var query = new StringBuilder();

            query.Append("INSERT INTO ");
            query.Append(DatabaseTables.Database);
            query.Append(DatabaseTables.DbSchema);
            query.Append(DatabaseTables.RegistrationsTable);
            query.AppendLine(" ");
            query.AppendLine(" (ClassId, StudentId) ");
            query.AppendLine(" VALUES (@Param1, @Param2); ");
            query.AppendLine(" select LAST_INSERT_ID(); ");

            var queryParams = new
            {
                Param1 = registration.ClassId,
                Param2 = registration.StudentId,
            };

            var (_, resultCode) = await this.TryExecuteDbAsync<int>(query.ToString(), queryParams).ConfigureAwait(false);
            return (resultCode == ResultCodes.OkResult);
        }

        /// <inheritdoc />
        public async Task<Registration> GetStudentRegistrationByKeyAsync(int registrationKey)
        {
            var parameters = new { Param1 = registrationKey };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.RegistrationsTable} WHERE RegistrationId = @Param1";

            var (result, _) = await this.TryQueryDbAsync<Registration>(query, parameters).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<List<Teacher>> GetTeacherDetailsByKeyAsync(int teacherId = -1)
        {
            object parameters = null;
            var query = new StringBuilder();
            query.Append($"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.TeachersTable} ");


            if (teacherId != -1)
            {
                query.AppendLine(" WHERE TeacherId = @Param1 ");
                parameters = new { Param1 = teacherId };
            }
            var (result, _) = await this.TryQueryDbAsync<Teacher>(query.ToString(), parameters).ConfigureAwait(false);

            return result.ToList();
        }

        /// <inheritdoc />
        public async Task<AttendanceRecord> GetAttendanceRecordDetailsAsync(AddAttendance attendance)
        {
            var dateOfAttendance = attendance.AttendanceDate.Date.ToString("yyyy-MM-dd");
            var parameters = new { Param1 = dateOfAttendance, Param2 = attendance.RegistrationId};
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.AttendanceTable} WHERE DATE(AttendanceDatetime) = @Param1 AND RegistrationId = @Param2";

            var (result, _) = await this.TryQueryDbAsync<AttendanceRecord>(query, parameters).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<AttendanceRecord> GetAttendanceRecordAsync(AddAttendance attendance)
        {
            var dateOfAttendance = attendance.AttendanceDate.Date;
            var parameters = new { Param1 = dateOfAttendance, Param2 = attendance.RegistrationId, Param3 = attendance.TeacherId, Param4 = attendance.IsPresent };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.AttendanceTable} WHERE DATE(AttendanceDate) = @Param1 AND RegistrationId = @Param2 AND TeacherId = @Param3 AND IsPresent = @Param4";

            var (result, _) = await this.TryQueryDbAsync<AttendanceRecord>(query, parameters).ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<bool> AddAttendanceRecordAsync(AddAttendance marRegister)
        {
            var query = new StringBuilder();

            query.Append("INSERT INTO ");
            query.Append(DatabaseTables.Database);
            query.Append(DatabaseTables.DbSchema);
            query.Append(DatabaseTables.AttendanceTable);
            query.AppendLine(" ");
            query.AppendLine(" (AttendanceDatetime, RegistrationId, TeacherId, IsPresent) ");
            query.AppendLine(" VALUES (@Param1, @Param2, @Param3, @Param4); ");
            query.AppendLine(" select LAST_INSERT_ID(); ");

            var queryParams = new
            {
                Param1 = marRegister.AttendanceDate,
                Param2 = marRegister.RegistrationId,
                Param3 = marRegister.TeacherId,
                Param4 = marRegister.IsPresent,
            };

            var (_, resultCode) = await this.TryExecuteDbAsync<int>(query.ToString(), queryParams).ConfigureAwait(false);
            return (resultCode == ResultCodes.OkResult);
        }

        private async Task<(TR, ResultCodes)> TryExecuteDbAsync<TR>(string query ,object queryParams)
        {
            await using var db = this.ConnectToDb();
            try
            {
                var insertId = await db.ExecuteScalarAsync<TR>(query, queryParams , commandTimeout:this._dbTimeout).ConfigureAwait(false);
                return (insertId, ResultCodes.OkResult);
            }
            catch (Exception ex)
            {
                this._logger.LogWarning((int)ResultCodes.DatabaseLevelException,ex.Message,ex.Source,ex.StackTrace);
                return (default(TR), ResultCodes.DatabaseLevelException);
            }

        }

        private async Task<(IEnumerable<TR>, ResultCodes)> TryQueryDbAsync<TR>(string query, object queryParams)
        {
            await using var db = this.ConnectToDb();
            try
            {
                var result = await db.QueryAsync<TR>(query, queryParams, commandTimeout: this._dbTimeout).ConfigureAwait(false);
                return (result, ResultCodes.OkResult);
            }
            catch (Exception ex)
            {
                this._logger.LogWarning((int)ResultCodes.DatabaseLevelException, ex.Message, ex.Source, ex.StackTrace);
                return (default(IEnumerable<TR>), ResultCodes.DatabaseLevelException);
            }

        }

        private MySqlConnection ConnectToDb()
        {
            var db = new MySqlConnection(_sqlConfig);
            db.Open();
            return db;
        }
    }
}
