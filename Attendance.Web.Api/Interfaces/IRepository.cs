﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Attendance.Web.Api.DTO;
using Attendance.Web.Api.Models;
using Teacher = Attendance.Web.Api.Models.Teacher;

namespace Attendance.Web.Api.Interfaces
{
    /// <summary>
    /// Generic Repository.
    /// </summary>
    public interface IRepository
    {

        /// <summary>
        /// Gets User Details from the database by Contact number.
        /// </summary>
        /// <param name="emailAddress">User Registered Email.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public Task<Teacher> GetUserDetailsByEmailAsync(string emailAddress);

        /// <summary>
        /// Creates user and issues a temporary token to access the system.
        /// </summary>
        /// <param name="userDetails">User specific information.</param>
        /// <returns>returns insertId if and true or false if the record was added.</returns>
        public Task<(int, bool)> CreateNewUserAsync(Register userDetails);


        /// <summary>
        /// Gets Student Details.
        /// </summary>
        /// <param name="identityNumber">Student Identifier.</param>
        /// <returns>returns existing student.</returns>
        public Task<Student> GetStudentDetailsByIdNumberAsync(string identityNumber);

        /// <summary>
        /// Creates a new student
        /// </summary>
        /// <param name="newStudent">new student</param>
        /// <returns>returns existing student.</returns>
        public Task<bool> CreateNewStudentAsync(AddStudent newStudent);

        /// <summary>
        /// Creates a new Class
        /// </summary>
        /// <param name="newClass">new class</param>
        /// <returns>create class.</returns>
        public Task<bool> CreateNewClassAsync(AddClass newClass);

        /// <summary>
        /// Gets details of an existing class
        /// </summary>
        /// <param name="targetClass">target class</param>
        /// <returns>create class that can be taught.</returns>
        public Task<Classes> GetClassDetailsAsync(AddClass targetClass);

        /// <summary>
        /// Gets details of an existing class
        /// </summary>
        /// <param name="classId">target class</param>
        /// <returns>create class that can be taught.</returns>
        public Task<Classes> GetClassDetailsByIdAsync(int classId);

        /// <summary>
        /// Gets Student Details.
        /// </summary>
        /// <param name="studentKey">Student Db Identifier.</param>
        /// <returns>returns existing student.</returns>
        public Task<List<Student>> GetStudentDetailsByKeyAsync(int studentKey = -1);

        /// <summary>
        /// Gets Student class registration Details.
        /// </summary>
        /// <param name="registration">Student registration.</param>
        /// <returns>returns existing student registration.</returns>
        public Task<Registration> GetRegistrationDetailsAsync(ClassRegistration registration);


        /// <summary>
        /// Created Student class registration.
        /// </summary>
        /// <param name="registration">Student registration.</param>
        /// <returns>returns creation status.</returns>
        public Task<bool> CreateNewRegistrationAsync(ClassRegistration registration);

        /// <summary>
        /// Get Student class registration.
        /// </summary>
        /// <param name="registrationKey">Student registration.</param>
        /// <returns>returns student registration.</returns>
        public Task<Registration> GetStudentRegistrationByKeyAsync(int registrationKey);

        /// <summary>
        /// Teacher registration.
        /// </summary>
        /// <param name="teacherId">Teacher Identifier.</param>
        /// <returns>returns teacher.</returns>
        public Task<List<Teacher>> GetTeacherDetailsByKeyAsync(int teacherId);

        /// <summary>
        /// Gets attendance record.
        /// </summary>
        /// <param name="attendance">attendance record.</param>
        /// <returns>returns attendance record</returns>
        public Task<AttendanceRecord> GetAttendanceRecordDetailsAsync(AddAttendance attendance);

        /// <summary>
        ///Marks attendance record.
        /// </summary>
        /// <param name="marRegister">Marks attendance record.</param>
        /// <returns>returns attendance record</returns>
        public Task<bool> AddAttendanceRecordAsync(AddAttendance marRegister);

        /// <summary>
        /// gets Attendance record.
        /// </summary>
        /// <param name="attendance">attendance record.</param>
        /// <returns>returns attendance record</returns>
        public Task<AttendanceRecord> GetAttendanceRecordAsync(AddAttendance attendance);

        /// <summary>
        /// Gets list of classes.
        /// </summary>
        /// <returns>returns list of classes.</returns>
        public Task<List<Classes>> GetAllClassesAsync();

        /// <summary>
        /// Gets list of registered users.
        /// </summary>
        /// <returns>list of registered users.</returns>
        public Task<List<RegisteredStudents>> GetAllRegisteredStudentsAsync(int filterByClassId = -1, int filterByStudentId = -1);
    }
}
