using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Enum;

namespace Attendance.Web.Api.Interfaces
{
    /// <summary>
    /// User Registration Service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// validates the user details.
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns>A Client Token and result code and client message.</returns>
        public Task<(string token, ResultCodes resultCode, string clientMessage)> AuthenticateUser(Login user);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="regData">User registration Details.</param>
        /// <returns>returns user token .</returns>
        public Task<(string token, ResultCodes resultCode, string clientMessage)> RegisterUserAsync(Register regData);


    }

}
