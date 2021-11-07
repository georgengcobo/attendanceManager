using System;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Enum;
using Attendance.Web.Api.Helpers;
using Attendance.Web.Api.Interfaces;
using Attendance.Web.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Attendance.Web.Api.Services
{
    /// <summary>
    /// Provides functionality to manage a user in the system.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository _repo;
        private readonly IConfiguration _config;
        private readonly ILogger<UserService> _logging;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repo">repository where data is stored.</param>
        /// <param name="config">application configuration.</param>
        /// <param name="logging">application logging.</param>
        public UserService(IRepository repo, IConfiguration config, ILogger<UserService> logging)
        {
            this._repo = repo;
            this._config = config;
            this._logging = logging;
        }

        /// <inheritdoc />
        public async Task<(string, ResultCodes, string)> AuthenticateUser(Login user)
        {
            var (teachers, resultCode) = await this._repo.GetUserDetailsByEmailAsync(user.Email).ConfigureAwait(false);

            if (teachers.Equals(default(Teacher)) && resultCode == ResultCodes.OkResult)
            {
                var msg =
                        $"User with identifier by Email: {user.Email} was not found in AuthenticateUser request";
                    this._logging.LogWarning((int)ResultCodes.UserNotFoundException, msg);
                    return (null, ResultCodes.UserNotFoundException, msg);
            }

            if (resultCode != ResultCodes.OkResult)
            {
                return (null, resultCode, "System level exception detected");
            }

            var userHash = Validations.CalculateMD5Hash(user.Password);

            if (!string.Equals(userHash.ToLower(), teachers.Password.ToLower(), StringComparison.Ordinal))
            {
                var msg = $"Invalid user Credentials Provided for  {user.Email} ";
                this._logging.LogWarning((int)ResultCodes.UserNotFoundException, msg);
                return (null, ResultCodes.InvalidPasswordException, msg);
            }

            var token = await Jwt.GenerateJsonWebToken(teachers.TeacherId, this._config).ConfigureAwait(false);

            return (token, ResultCodes.OkResult, "Login Ok");
        }

        /// <inheritdoc />
        public async Task<(string, ResultCodes, string)> RegisterUserAsync(Register regData)
        {
            var (insertId, status) = await this._repo.CreateNewUserAsync(regData).ConfigureAwait(false);

            if (insertId == -1 && status == false)
            {
                var msg = $"User with either Email already exists, please log for Email:{regData.Email} ";
                this._logging.LogWarning((int)ResultCodes.UserNotFoundException, msg);
                return (null , ResultCodes.DuplicateRecordException, msg);
            }

            var token = await Jwt.GenerateJsonWebToken(insertId, this._config).ConfigureAwait(false);

            return (token, ResultCodes.OkResult, "User Registration Ok");
        }


    }
}
