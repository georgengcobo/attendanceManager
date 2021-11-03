using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance.Web.Api.Enum
{
    /// <summary>
    /// Provides definition of used tables.
    /// </summary>
    public struct DatabaseTables
    {
        /// <summary>
        /// Gets Target database.
        /// </summary>
        public static string Database => "attendance_register";

        /// <summary>
        /// Gets Target database Schema.
        /// </summary>
        public static string DbSchema => ".";

        /// <summary>
        /// Gets Target database teacher Table.
        /// </summary>
        public static string TeachersTable => "teachers";

        /// <summary>
        /// Gets Target classes Table.
        /// </summary>
        public static string ClassesTable => "classes";

        /// <summary>
        /// Gets Target Registration Table.
        /// </summary>
        public static string RegistrationsTable => "registration";

        /// <summary>
        /// Gets Target students Table.
        /// </summary>
        public static string StudentsTable => "students";

        /// <summary>
        /// Gets Target students Table.
        /// </summary>
        public static string AttendanceTable => "attendance";

    }
}
