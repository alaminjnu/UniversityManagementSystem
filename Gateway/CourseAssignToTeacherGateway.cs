using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class CourseAssignToTeacherGateway:CommonGateway
    {

        TeacherManager teacherManager = new TeacherManager();


        public IEnumerable<CourseAssignToTeacher> GetAll
        {
            get
            {

                Query = "SELECT * FROM CourseAssignToTeacher WHERE IsActive='True'";
                SqlCommand Command = new SqlCommand(Query, Connection);
                List<CourseAssignToTeacher> courseAssignToTeachers = new List<CourseAssignToTeacher>();
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    CourseAssignToTeacher courseAssignToTeacher = new CourseAssignToTeacher();

                    courseAssignToTeacher.Id = Convert.ToInt32(reader["Id"].ToString());
                    courseAssignToTeacher.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());
                    courseAssignToTeacher.TeacherId = Convert.ToInt32(reader["TeacherId"].ToString());
                    courseAssignToTeacher.CourseId = Convert.ToInt32(reader["CourseId"].ToString());
                    courseAssignToTeacher.Status = Convert.ToBoolean(reader["IsActive"].ToString());
                    courseAssignToTeachers.Add(courseAssignToTeacher);
                }

                reader.Close();
                Connection.Close();
                return courseAssignToTeachers;
                
                
                
            }
        }

        public int Update(CourseAssignToTeacher courseAssignToTeacher)
        {
            Query= "UPDATE CourseAssignToTeacher SET IsActive=1 WHERE TeacherId='" + courseAssignToTeacher.TeacherId + "' AND CourseId='" + courseAssignToTeacher.CourseId + "'";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowsAffected;
        }

        public int Insert(CourseAssignToTeacher courseAssignToTeacher)
        {
            Query = "INSERT INTO CourseAssignToTeacher(DepartmentId,TeacherId,CourseId,IsActive)" +
                  " VALUES(@DepartmentId,@TeacherId,@CourseId,@IsActive)";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = courseAssignToTeacher.DepartmentId;

            Command.Parameters.Add("TeacherId", SqlDbType.Int);
            Command.Parameters["TeacherId"].Value = courseAssignToTeacher.TeacherId;

            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = courseAssignToTeacher.CourseId;

            Command.Parameters.Add("IsActive", SqlDbType.Bit);
            Command.Parameters["IsActive"].Value = 1;

            Connection.Open();
            Command.ExecuteNonQuery();
            int updateResult = UpdateTeacher(courseAssignToTeacher);
            Connection.Close();
            return updateResult;
        }

        private int UpdateTeacher(CourseAssignToTeacher courseAssignToTeacher)
        {
            Teacher teacher = teacherManager.GetAllTeacher().ToList().Find(t => t.Id == courseAssignToTeacher.TeacherId);
            double creditTakenbyTeacher = Convert.ToDouble(teacher.CreditTaken) + Convert.ToDouble(courseAssignToTeacher.Credit);
            Query = "Update Teachers Set CreditTaken='" + creditTakenbyTeacher + "' WHERE Id='" +
                                     courseAssignToTeacher.TeacherId + "'";
            SqlCommand Command = new SqlCommand(Query, Connection);
            int i = Command.ExecuteNonQuery();
            Connection.Close();
            return i;
        }
    }
}