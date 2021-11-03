using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Helpers;
using Attendance.Web.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Attendance.Web.Api.Controllers
{
    /// <summary>
    /// controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// initiates controller with logger.
        /// </summary>
        /// <param name="userService">User Service,</param>
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Authenticates User.
        /// </summary>
        /// <param name="userRequest">User request Object.</param>
        /// <returns>Returns user token.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AuthenticateUser([FromBody] Login userRequest)
        {
            var (token, loginState, clientMessage) = await this._userService.AuthenticateUser(userRequest).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(loginState, new { Token = token, Message = clientMessage });
        }


        /// <summary>
        /// Gets user Details.
        /// </summary>
        /// <param name="regData">User Registration data.</param>
        /// <returns>User details with token or message.</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterUser([FromBody] Register regData)
        {
            var (token, status, clientMessage) = await this._userService.RegisterUserAsync(regData).ConfigureAwait(false);

            return HttpStatusCodeResolver.Resolve(status, new { Token = token, Message = clientMessage });
        }
    }
}
