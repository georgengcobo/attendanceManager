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
        private readonly ILogger<Repository> logger;
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="logger">logger implementation.</param>
        /// <param name="config">configure application settings.</param>
        public Repository(ILogger<Repository> logger, IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }


        /// <inheritdoc/>
        public async Task<User> GetUserDetailsByEmailAsync(string emailAddress)
        {
            var parameters = new { UserEmail = emailAddress };
            var query = $"SELECT * FROM {DatabaseTables.Database}{DatabaseTables.DbSchema}{DatabaseTables.TeachersTable} WHERE Email = @UserEmail";

            await using var db = this.ConnectToDb();
            var user = await db.QueryAsync<User>(query, parameters).ConfigureAwait(false);
            return user.FirstOrDefault();
        }

        /// <inheritdoc/>
        public async Task<(int, bool)> CreateNewUserAsync(Register userDetails)
        {
            var userExists = await this.GetUserDetailsByEmailAsync(userDetails.Email).ConfigureAwait(false);
            if (!userExists.Equals(default(User)))
            {
                return (-1, false);
            }

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

            await using var db = this.ConnectToDb();
            var insertId = await db.ExecuteScalarAsync<int>(query.ToString(), queryParams).ConfigureAwait(false);

            return (insertId, true);
        }

        private MySqlConnection ConnectToDb()
        {
            var sqlConfig = this.config["ConnectionStrings:MainApp"];
            var db = new MySqlConnection(sqlConfig);
            db.Open();
            return db;
        }
    }
}
