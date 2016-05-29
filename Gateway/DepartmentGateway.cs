using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class DepartmentGateway:CommonGateway
    {
        public int Save(Department department)
        {
            Query = "INSERT INTO Departments(Name,Code) VALUES(@Name,@Code)";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = department.Name;

            Command.Parameters.Add("Code", SqlDbType.VarChar);
            Command.Parameters["Code"].Value = department.Code;

            Connection.Open();
            int rowsAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowsAffected;
        }


        public bool IsCodeExist(string codeName)
        {
            Query = "SELECT *FROM Departments WHERE Code='" + codeName + "'";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool isCodeExist = reader.HasRows;
            reader.Close();
            Connection.Close();
            return isCodeExist;
        }

        public List<Department> GetAll()
        {
            Query = "SELECT *FROM Departments";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Department> departmentList = new List<Department>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Department department = new Department();

                    department.Id = Convert.ToInt32(reader["Id"].ToString());
                    department.Code = reader["Code"].ToString();
                    department.Name = reader["Name"].ToString();
                    departmentList.Add(department);
                }
                reader.Close();
            }
            Connection.Close();
            return departmentList;
        }

        public bool IsNameExist(string name)
        {
            Query = "SELECT *FROM Departments WHERE Name='" + name + "'";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            bool isCodeExist = reader.HasRows;
            reader.Close();
            Connection.Close();
            return isCodeExist;
        }
    }
}