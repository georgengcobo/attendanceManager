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
            var (state, clientMessage) = await this._adminService.AddStudentAsync(userRequest).ConfigureAwait(false);
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
            var (state, clientMessage) = await this._adminService.AddClassAsync(userRequest).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { Message = clientMessage });
        }


        /// <summary>
        /// Enroll Student into a class.
        /// </summary>
        /// <param name="userRequest">User request Object.</param>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("Enroll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterInClass([FromBody] ClassRegistration userRequest)
        {
            var (state, clientMessage) = await this._adminService.RegisterInClassAsync(userRequest).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { Message = clientMessage });
        }

        /// <summary>
        /// Record student attendance of class.
        /// </summary>
        /// <param name="userRequest">User request Object.</param>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("Attendance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CaptureAttendance([FromBody] AddAttendance userRequest)
        {
            var (state, clientMessage) = await this._adminService.CaptureAttendanceAsync(userRequest).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { Message = clientMessage });
        }

        /// <summary>
        /// Lists available classes.
        /// </summary>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Classes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClassesResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClasses()
        {
            var (result, state, clientMessage) = await this._adminService.GetClassAsync().ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new {Classes = result, Message = clientMessage });
        }

        /// <summary>
        /// List of registered students in each class
        /// </summary>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Registrations/Class/{filterClass}/Student/{filterStudentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegisteredStudents>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisteredStudents(int filterClass = -1, int filterStudentId = -1)
        {
            var (result, state, clientMessage) = await this._adminService.GetRegisteredStudentsAsync(filterClass, filterStudentId).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { RegisteredStudents = result, Message = clientMessage });
        }

        /// <summary>
        /// List of registered students in each class
        /// </summary>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Teachers/{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeacherResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Teachers(int teacherId = -1)
        {
            var (result, state, clientMessage) = await this._adminService.GetTeachersAsync(teacherId).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { Teachers = result, Message = clientMessage });
        }


        /// <summary>
        /// List of registered students in each class
        /// </summary>
        /// <returns>Status of request.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Students/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Student>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Students(int studentId = -1)
        {
            var (result, state, clientMessage) = await this._adminService.GetStudentsAsync(studentId).ConfigureAwait(false);
            return HttpStatusCodeResolver.Resolve(state, new { Students = result, Message = clientMessage });
        }


    }
}
