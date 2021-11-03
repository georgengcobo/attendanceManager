namespace Attendance.Web.Api.Enum
{
    /// <summary>
    /// Enum for exception codes.
    /// </summary>
    public enum ResultCodes
    {
        /// <summary>
        /// Operation concluded successfully
        /// </summary>
        OkResult = -1,

        /// <summary>
        /// Invalid User PassWord Supplied.
        /// </summary>
        InvalidPasswordException = 0,

        /// <summary>
        /// User name Supplied not found in System.
        /// </summary>
        UserNotFoundException = 2,

        /// <summary>
        /// User name Supplied not found in System.
        /// </summary>
        AccessForbiddenException = 3,

        /// <summary>
        /// User name Supplied not found in System.
        /// </summary>
        InvalidTokenException = 4,

        /// <summary>
        /// Duplicate record found in System.
        /// </summary>
        DuplicateRecordException = 5,

        /// <summary>
        /// Record Created in System.
        /// </summary>
        RecordCreated = 6,

        /// <summary>
        /// Operation not executed as expected.
        /// </summary>
        UnexpectedOperationException = 7,

        /// <summary>
        /// Record not found.
        /// </summary>
        RecordNotFoundException = 8,

    }
}
