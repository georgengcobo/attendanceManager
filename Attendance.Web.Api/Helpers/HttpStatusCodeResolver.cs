using Attendance.Web.Api.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Attendance.Web.Api.Helpers
{

    /// <summary>
    /// Resolves http status code based on result codes emitted by each service.
    /// </summary>
    public static class HttpStatusCodeResolver
    {
        /// <summary>
        /// Resolves http response code to be used.
        /// </summary>
        /// <param name="serviceResultCode">operation status code.</param>
        /// <param name="result">Response object that must be sent to client.</param>
        /// <returns>object result.</returns>
        public static ObjectResult Resolve(ResultCodes serviceResultCode, object result)
        {
            return serviceResultCode switch
            {
                ResultCodes.AccessForbiddenException => new UnauthorizedObjectResult(result),
                ResultCodes.InvalidPasswordException => new UnauthorizedObjectResult(result),
                ResultCodes.UserNotFoundException => new NotFoundObjectResult(result),
                ResultCodes.InvalidTokenException => new UnauthorizedObjectResult(result),
                ResultCodes.DuplicateRecordException => new ConflictObjectResult(result),
                ResultCodes.DatabaseLevelException => new UnprocessableEntityObjectResult(result),
                _ => new OkObjectResult(result),
            };
        }
    }
}
