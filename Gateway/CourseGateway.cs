using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class CourseGateway:CommonGateway
    {
        TeacherGateway teacherGateway = new TeacherGateway();

        public IEnumerable<Course> GetAll()
        {
            Query = "SELECT * FROM Courses";
            SqlCommand Command = new SqlCommand(Query, Connection);
            List<Course> courses = new List<Course>();
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                Course course = new Course();

                course.Id = Convert.ToInt32(reader["Id"].ToString());
                course.Name = reader["Name"].ToString();
                course.Code = reader["Code"].ToString();
                course.Credit = Convert.ToDouble(reader["Credit"].ToString());
                course.Description = reader["Description"].ToString();
                course.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());
                course.SemesterId = Convert.ToInt32(reader["SemesterId"].ToString());
                courses.Add(course);
            }

            reader.Close();
            Connection.Close();
            return courses;
        }


        public int Insert(Course aCourse)
        {
            Query = "INSERT INTO Courses(Code,Name,Credit,Description,DepartmentId,SemesterId) VALUES(@code,@name,@credit,@description,@departmentId,@semesterId)";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = aCourse.Code;

            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = aCourse.Name;

            Command.Parameters.Add("credit", SqlDbType.Float);
            Command.Parameters["credit"].Value = aCourse.Credit;

            Command.Parameters.Add("description", SqlDbType.VarChar);
            Command.Parameters["description"].Value = aCourse.Description;

            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = aCourse.DepartmentId;

            Command.Parameters.Add("semesterId", SqlDbType.Int);
            Command.Parameters["semesterId"].Value = aCourse.SemesterId;

            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowsAffected;
        }

        public IEnumerable<ViewCourseModel> GetCourseViewModels()
        {

            string query = "ProcedureViewCourseStatistics";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //Command.CommandText = query;

            List<ViewCourseModel> courseViewModels = new List<ViewCourseModel>();
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                ViewCourseModel course = new ViewCourseModel
                {
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Semester = reader["Semester"].ToString(),
                    Teacher = reader["Teacher"].ToString()
                };
                courseViewModels.Add(course);
            }
            reader.Close();
            Connection.Close();
            return courseViewModels;
        }

        internal List<ViewCourseSchedule> GetAllCourseSchedule()
        {

            string query = "SELECT * FROM FinalSchedule";



            SqlCommand command = new SqlCommand(query, Connection);

            Connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<ViewCourseSchedule> viewCourseScheduleList = new List<ViewCourseSchedule>();

            while (reader.Read())
            {
                ViewCourseSchedule aCourseSchedule = new ViewCourseSchedule();
                aCourseSchedule.DepartmentId = (int)reader["DepartmentId"];
                aCourseSchedule.CourseId = (int)reader["ID"];
                aCourseSchedule.CourseName = reader["Name"].ToString();
                aCourseSchedule.CourseCode = reader["Code"].ToString();
                string s = WebUtility.HtmlDecode(reader["Schedule"].ToString());
                String enrolledStatus = reader["AllocateStatus"].ToString();
                if (enrolledStatus == "Unallocated"||enrolledStatus == "")
                {
                    s = "Not Scheduled Yet";
                    aCourseSchedule.ScheduleInfo = s;
                }
                else
                {
                    aCourseSchedule.ScheduleInfo = s;

                }



                viewCourseScheduleList.Add(aCourseSchedule);
            }
            reader.Close();
            Connection.Close();

            return viewCourseScheduleList;
        }


        public int UnAssignCourse()
        {
            Connection.Open();
            Query = "UPDATE CourseAssignToTeacher SET IsActive=0";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.ExecuteNonQuery();
            int a = teacherGateway.UpdateTeacherInformation();
            int i = UnAssignStudentCourse();
            Connection.Close();
            return i;

        }

        private int UnAssignStudentCourse()
        {
            Query = "UPDATE EnrollCourse SET IsStudentActive=0";
            SqlCommand Command = new SqlCommand(Query, Connection);
            return Command.ExecuteNonQuery();
        }

        public IEnumerable<Course> GetCoursesByDepartmentId(int deptId)
        {
            List<Course> courses = new List<Course>();
            Query = "SELECT * FROM Courses WHERE DepartmentId=@deptId";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.Add("deptId", SqlDbType.Int);
            Command.Parameters["deptId"].Value = deptId;
            //Command.Parameters.AddWithValue("@DepartmentId", deptId);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                Course aCourse = new Course
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Credit = Convert.ToDouble(reader["Credit"].ToString()),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    Description = reader["Description"].ToString(),
                    SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())
                };
                courses.Add(aCourse);
            }
            reader.Close();
            Connection.Close();
            return courses;
        }

        public Course GetCourseByName(string name)
        {
            string query = "SELECT * FROM Courses WHERE Name=@name";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@name", name);
            Connection.Open();
            Course course = null;
            SqlDataReader reader = Command.ExecuteReader();
            if (reader.Read())
            {
                course = new Course
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Credit = Convert.ToDouble(reader["Credit"].ToString()),
                    Description = reader["Description"].ToString(),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())

                };

            }
            reader.Close();
            Connection.Close();
            return course;
        }

        public Course GetCourseByCode(string code)
        {
            string query = "SELECT * FROM Courses WHERE Code=@code";
            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@code", code);
            Connection.Open();
            Course course = null;
            SqlDataReader reader = Command.ExecuteReader();
            if (reader.Read())
            {
                course = new Course
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Credit = Convert.ToDouble(reader["Credit"].ToString()),
                    Description = reader["Description"].ToString(),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())

                };

            }
            reader.Close();
            Connection.Close();
            return course;
        }
    }
}
