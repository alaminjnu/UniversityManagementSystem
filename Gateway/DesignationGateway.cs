using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class DesignationGateway:CommonGateway
    {
        public List<Designation> GetAll()
        {
            Query = "SELECT *FROM Designations";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            List<Designation> designationList = new List<Designation>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Designation designation = new Designation();

                    designation.Id = Convert.ToInt32(reader["Id"].ToString());
                    designation.DesignationName = reader["DesignationName"].ToString();

                    designationList.Add(designation);
                }
                reader.Close();
            }
            Connection.Close();
            return designationList;
        }
    }
}