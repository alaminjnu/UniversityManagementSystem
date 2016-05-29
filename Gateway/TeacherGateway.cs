using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class TeacherGateway:CommonGateway
    {
        
        public int UpdateTeacherInformation()
        {
            Connection.Open();
            Query = "UPDATE Teachers SET CreditTaken=0";
            SqlCommand Command=new SqlCommand(Query,Connection);
            int i = Command.ExecuteNonQuery();
            Connection.Close();
            return i;
        }

        public IEnumerable<Teacher> GetAllTeacher()
        {
            Query = "SELECT * FROM Teachers";
            SqlCommand Command = new SqlCommand(Query, Connection);
            List<Teacher> teachers = new List<Teacher>();
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                Teacher teacher=new Teacher();

                teacher.Id = Convert.ToInt32(reader["Id"].ToString());
                teacher.Name = reader["Name"].ToString();
                teacher.Address = reader["Address"].ToString();
                teacher.Email = reader["Email"].ToString();
                teacher.ContactNo = reader["ContactNo"].ToString();
                teacher.DesignationId = Convert.ToInt32(reader["DesignationId"].ToString());

                teacher.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());
                teacher.CreditToBeTaken = Convert.ToInt32(reader["CreditToBeTaken"].ToString());
                teacher.CreditTaken = Convert.ToInt32(reader["CreditTaken"].ToString());
                teachers.Add(teacher);
            }

            reader.Close();
            Connection.Close();
            return teachers;
        }

        public bool IsEmailExist(string email)
        {
            Query = "SELECT * FROM Teachers WHERE Email='" + email + "'";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool isEmailExist = reader.HasRows;
            reader.Close();
            Connection.Close();
            return isEmailExist;
        }

        public int Save(Teacher teacher)
        {
            Query = "INSERT INTO Teachers(Name,Address,Email,ContactNo,DesignationId,DepartmentId,CreditToBeTaken,CreditTaken) " +
                    "VALUES(@Name,@Address,@Email,@ContactNo,@DesignationId,@DepartmentId,@CreditToBeTaken,@CreditTaken)";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = teacher.Name;

            Command.Parameters.Add("Address", SqlDbType.VarChar);
            Command.Parameters["Address"].Value = teacher.Address;

            Command.Parameters.Add("Email", SqlDbType.VarChar);
            Command.Parameters["Email"].Value = teacher.Email;

            Command.Parameters.Add("ContactNo", SqlDbType.VarChar);
            Command.Parameters["ContactNo"].Value = teacher.ContactNo;

            Command.Parameters.Add("DesignationId", SqlDbType.Int);
            Command.Parameters["DesignationId"].Value = teacher.DesignationId;

            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = teacher.DepartmentId;

            Command.Parameters.Add("CreditToBeTaken", SqlDbType.Float);
            Command.Parameters["CreditToBeTaken"].Value = teacher.CreditToBeTaken;

            Command.Parameters.Add("CreditTaken", SqlDbType.Float);
            Command.Parameters["CreditTaken"].Value = 0;

            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowsAffected;
        }

    }
    }
