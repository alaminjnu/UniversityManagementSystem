using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class SemesterGateway:CommonGateway
    {
        public List<Semester> GetAll()
        {
            Query = "SELECT * FROM Semesters";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Semester> semesters = new List<Semester>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Semester semester = new Semester();

                    semester.Id = Convert.ToInt32(reader["Id"].ToString());
                    semester.Name = reader["SemesterName"].ToString();
                    semesters.Add(semester);
                }
                reader.Close();
            }
            Connection.Close();

            return semesters;
        }
    }
}