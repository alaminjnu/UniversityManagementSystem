using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class DayGateway : CommonGateway
    {
        public IEnumerable<Day> GetAllDays()
        {
            Query = "SELECT * FROM Days";
            SqlCommand Command = new SqlCommand(Query, Connection);
            List<Day> days = new List<Day>();
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                Day day = new Day();

                day.Id = Convert.ToInt32(reader["Id"].ToString());
                day.Name = reader["Name"].ToString();
               
                days.Add(day);
            }
            reader.Close();
            Connection.Close();
                return days;
            
        }
    }
}