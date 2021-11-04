using System.Collections.Generic;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Helpers;
using Attendance.Web.Api.Interfaces;
using Attendance.Web.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attendance.Web.Api.Controllers
{
    /// <summary>
    /// controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="adminService">Admin Service.</param>
        public AdminController(IAdminService adminService)
        {
            this._adminService = adminService;
        }

        /// <summary>
        /// Add student.
        /// </summary>
        /// <param name="userRequest">User request Object.</param>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("AddStudent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddStudent([FromBody] AddStudent userRequest)
        {
            var (state, clientMessage) = await this._adminService.AddStudent(userRequest).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new {Message = clientMessage });
        }

        /// <summary>
        /// Add Class That can be offered to students.
        /// </summary>
        /// <param name="userRequest">User request Object.</param>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("AddClass")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddClass([FromBody] AddClass userRequest)
        {
            var (state, clientMessage) = await this._adminService.AddClass(userRequest).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { Message = clientMessage });
        }


        /// <summary>
        /// Enroll Student into a class.
        /// </summary>
        /// <param name="userRequest">User request Object.</param>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("EnrollClass")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterInClass([FromBody] ClassRegistration userRequest)
        {
            var (state, clientMessage) = await this._adminService.RegisterInClass(userRequest).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { Message = clientMessage });
        }

        /// <summary>
        /// Record student attendance of class.
        /// </summary>
        /// <param name="userRequest">User request Object.</param>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("RecordAttendance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CaptureAttendance([FromBody] AddAttendance userRequest)
        {
            var (state, clientMessage) = await this._adminService.CaptureAttendance(userRequest).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { Message = clientMessage });
        }

        /// <summary>
        /// Lists available classes.
        /// </summary>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Classes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegisteredStudents>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClasses()
        {
            var (result, state, clientMessage) = await this._adminService.GetClass().ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new {Classes = result, Message = clientMessage });
        }
        /// <summary>
        /// List of registered students in each class
        /// </summary>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("RegisteredStudents/{filterClass}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegisteredStudents>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisteredStudents(int filterClass = -1)
        {
            var (result, state, clientMessage) = await this._adminService.GetRegisteredStudents(filterClass).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { RegisteredStudents = result, Message = clientMessage });
        }

    }
}
