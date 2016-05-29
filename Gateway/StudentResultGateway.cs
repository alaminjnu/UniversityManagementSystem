using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class StudentResultGateway:CommonGateway
    {
        public IEnumerable<Student> GetAllStudent
        {
            get
            {

                Query = "SELECT * FROM Students";
                SqlCommand Command = new SqlCommand(Query, Connection);
                List<Student> studenList = new List<Student>();
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student();

                    student.Id = Convert.ToInt32(reader["Id"].ToString());
                    student.RegistrationNo = reader["RegistrationNo"].ToString();
                    student.Name = reader["Name"].ToString();
                    student.Email = reader["Email"].ToString();
                    student.ContactNo = reader["ContactNo"].ToString();
                    student.RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"].ToString());
                    student.Address = reader["Address"].ToString();
                    student.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());
                    studenList.Add(student);
                }

                reader.Close();
                Connection.Close();
                return studenList;
            }

        }


        public IEnumerable<Course> GetAllCourse
        {
            get
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
        }

        public IEnumerable<StudentResult> GetAllResult
        {
            get
            {

                Query = "SELECT * FROM StudentResult";
                SqlCommand Command = new SqlCommand(Query, Connection);
                List<StudentResult> studentResults = new List<StudentResult>();
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    StudentResult studentResult = new StudentResult();

                    studentResult.Id = Convert.ToInt32(reader["Id"].ToString());
                    studentResult.CourseId = Convert.ToInt32(reader["CourseId"].ToString());
                    studentResult.StudentId = Convert.ToInt32(reader["StudentId"].ToString());
                    studentResult.Grade = reader["Grade"].ToString();

                    studentResults.Add(studentResult);
                }
                reader.Close();
                Connection.Close();
                return studentResults;

            }
        }

        public int Insert(StudentResult studentResult)
        {

            Query = "INSERT INTO StudentResult(StudentId,CourseId,Grade)" +
                   " VALUES(@StudentId,@CourseId,@Grade)";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("StudentId", SqlDbType.Int);
            Command.Parameters["StudentId"].Value = studentResult.StudentId;

            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = studentResult.CourseId;

            Command.Parameters.Add("Grade", SqlDbType.VarChar);
            Command.Parameters["Grade"].Value = studentResult.Grade;

            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowsAffected;

        }

        public List<StudentViewModel> GetStudentInformationById(int id)
        {


            Query = "spGetStudentInformationById";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            //Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Id", id);
            //StudentViewModel student = null;
            List<StudentViewModel> studentViewModels = new List<StudentViewModel>();
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            if (reader.Read())
            {
                StudentViewModel studentViewModel = new StudentViewModel
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    RegNo = reader["RegistrationNo"].ToString(),
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    ContactNo = reader["ContactNo"].ToString(),
                    RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"].ToString()),
                    Address = reader["Address"].ToString(),
                    Department = reader["Department"].ToString()
                };
                studentViewModels.Add(studentViewModel);
            }

            reader.Close();
            Connection.Close();
            return studentViewModels;


        }

        public IEnumerable<Course> GetCoursesTakeByaStudentByStudentId(int id)
        {



            Query = "spGetCoursesTakenByaStudent";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@StudentId", id);
            List<Course> courses = new List<Course>();
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

        public IEnumerable<StudentResultViewModel> GetStudentResultByStudentId(int id)
        {
            List<StudentResultViewModel> studentResults = new List<StudentResultViewModel>();
            Query = "spGetStudentResult";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            Command.Parameters.AddWithValue("@studentId", id);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            while (reader.Read())
            {
                StudentResultViewModel studentResult = new StudentResultViewModel
                {
                    StudentId = Convert.ToInt32(reader["StudentId"].ToString()),
                    Code = reader["Code"].ToString(),
                    Name = reader["Name"].ToString(),
                    Grade = reader["Grade"].ToString()
                };
                studentResults.Add(studentResult);
            }
            reader.Close();
            return studentResults;
        }
    }
}