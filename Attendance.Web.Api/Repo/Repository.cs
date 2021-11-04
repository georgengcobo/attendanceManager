using System;
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
using Microsoft.VisualBasic.CompilerServices;
using MySqlConnector;

namespace Attendance.Web.Api.Repo
{

    /// <summary>
    /// initiates repository.
    /// </summary>
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;
        private readonly IConfiguration _config;
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
            this._config = config;
            this._sqlConfig = config["ConnectionStrings:MainApp"];
            this._dbTimeout = Convert.ToInt32(config["ConnectionStrings:DBTimeOut"]);
        }


        /// <inheritdoc/>
        public async Task<User> GetUserDetailsByEmailAsync(string emailAddress)
        {
            var queryParams = new { UserEmail = emailAddress };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.TeachersTable} WHERE Email = @UserEmail";

            var (user, _ ) =  await this.TryQueryDbAsync<User>(query, queryParams).ConfigureAwait(false);
            return user;
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

        public async Task<Student> GetStudentDetailsByIdNumberAsync(string identityNumber)
        {
            var parameters = new { Param1 = identityNumber };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.StudentsTable} WHERE IdNumber = @Param1";

            var (student, _) = await this.TryQueryDbAsync<Student>(query, parameters).ConfigureAwait(false);

            return student;
        }

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

        public async Task<Classes> GetClassDetailsAsync(AddClass targetClass)
        {
            var parameters = new { Param1 = targetClass.ClassName, Param2 = targetClass.Grade, Param3 = targetClass.TeacherId };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.ClassesTable} WHERE ClassName = @Param1 AND Grade = @Param2 AND TeacherId = @Param3";

            var (result, _) = await this.TryQueryDbAsync<Classes>(query, parameters).ConfigureAwait(false);

            return result;
        }

        public async Task<Classes> GetClassDetailsByIdAsync(int classId)
        {
            var parameters = new { Param1 = classId };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.ClassesTable} WHERE ClassId = @Param1";

            var (result, _) = await this.TryQueryDbAsync<Classes>(query, parameters).ConfigureAwait(false);

            return result;
        }

        public async Task<Student> GetStudentDetailsByKeyAsync(int studentKey)
        {
            var parameters = new { Param1 = studentKey };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.StudentsTable} WHERE StudentId = @Param1";

            var (result, _) = await this.TryQueryDbAsync<Student>(query, parameters).ConfigureAwait(false);

            return result;
        }

        public async Task<Registration> GetRegistrationDetailsAsync(ClassRegistration registration)
        {
            var parameters = new { Param1 = registration.ClassId, Param2 = registration.StudentId};
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.RegistrationsTable} WHERE ClassId = @Param1 AND StudentId = @Param2";

            var (result, _) = await this.TryQueryDbAsync<Registration>(query, parameters).ConfigureAwait(false);

            return result;
        }

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

        public async Task<Registration> GetStudentRegistrationByKeyAsync(int registrationKey)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetTeacherDetailsByKeyAsync(int teacherId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AttendanceRecord> GetAttendanceRecordDetailsAsync(AddAttendance attendance)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddAttendanceRecordAsync(AddAttendance marRegister)
        {
            var query = new StringBuilder();

            query.Append("INSERT INTO ");
            query.Append(DatabaseTables.Database);
            query.Append(DatabaseTables.DbSchema);
            query.Append(DatabaseTables.AttendanceTable);
            query.AppendLine(" ");
            query.AppendLine(" (RegistrationId, TeacherId, IsPresent) ");
            query.AppendLine(" VALUES (@Param1, @Param2, @Param3); ");
            query.AppendLine(" select LAST_INSERT_ID(); ");

            var queryParams = new
            {
                Param1 = marRegister.RegistrationId,
                Param2 = marRegister.TeacherId,
                Param3 = marRegister.IsPresent,
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

        private async Task<(TR, ResultCodes)> TryQueryDbAsync<TR>(string query, object queryParams)
        {
            await using var db = this.ConnectToDb();
            try
            {
                var result = await db.QueryAsync<TR>(query, queryParams, commandTimeout: this._dbTimeout).ConfigureAwait(false);
                return (result.FirstOrDefault(), ResultCodes.OkResult);
            }
            catch (Exception ex)
            {
                this._logger.LogWarning((int)ResultCodes.DatabaseLevelException, ex.Message, ex.Source, ex.StackTrace);
                return (default(TR), ResultCodes.DatabaseLevelException);
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
