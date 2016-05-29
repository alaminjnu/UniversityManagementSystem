using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class EnrollStudentGateway:CommonGateway
    {
        public int Save(EnrollStudent enrollStudent)
        {
            
                Query = "INSERT INTO EnrollCourse(StudentId,CourseId,EnrollDate,IsStudentActive)" +
                        " VALUES(@StudentId,@CourseId,@EnrollDate,@IsStudentActive)";
                SqlCommand Command = new SqlCommand(Query, Connection);

                Command.Parameters.Clear();

                Command.Parameters.Add("StudentId", SqlDbType.Int);
                Command.Parameters["StudentId"].Value = enrollStudent.StudentId;

                Command.Parameters.Add("CourseId", SqlDbType.Int);
                Command.Parameters["CourseId"].Value = enrollStudent.CourseId;

                Command.Parameters.Add("EnrollDate", SqlDbType.Date);
                Command.Parameters["EnrollDate"].Value = enrollStudent.EnrollDate;

                Command.Parameters.Add("IsStudentActive", SqlDbType.Bit);
                Command.Parameters["IsStudentActive"].Value = true;

                Connection.Open();
                int rowsAffected = Command.ExecuteNonQuery();
                Connection.Close();
                return rowsAffected;
            
        }

        public bool IsStudentIdExist(int studentId)
        {
            Query = "SELECT *FROM EnrollCourse WHERE StudentId='" + studentId + "'";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool isStudentIdExist = reader.HasRows;
            reader.Close();
            Connection.Close();
            return isStudentIdExist;
        }

        public bool IsCourseIdExist(int courseId)
        {
            Query = "SELECT *FROM EnrollCourse WHERE CourseId='" + courseId + "'";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool isCourseIdExist = reader.HasRows;
            reader.Close();
            Connection.Close();
            return isCourseIdExist;
        }

        public IEnumerable<EnrollStudent> GetEnrollCourses
        {
            get
            {
                Query = "SELECT *FROM EnrollCourse";
                SqlCommand Command = new SqlCommand(Query, Connection);
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                List<EnrollStudent> enrollStudentList = new List<EnrollStudent>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EnrollStudent enrollStudent = new EnrollStudent();

                        enrollStudent.Id = Convert.ToInt32(reader["Id"].ToString());
                        enrollStudent.StudentId = Convert.ToInt32(reader["StudentId"].ToString());
                        enrollStudent.CourseId = Convert.ToInt32(reader["CourseId"].ToString());
                        enrollStudent.EnrollDate = Convert.ToDateTime(reader["EnrollDate"].ToString());
                        enrollStudentList.Add(enrollStudent);
                    }
                    reader.Close();
                }
                Connection.Close();
                return enrollStudentList;
            }
        }
    }
}