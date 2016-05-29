using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class RoomGateway:CommonGateway
    {
        public List<Room> GetAllRooms()
        {
            var roomList = new List<Room>();
            Query = "SELECT * FROM Rooms";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();

            while (reader.Read())
            {
                var room = new Room
                {
                    Id = Convert.ToInt32(reader["RoomId"].ToString()),
                    Name = reader["RoomNumber"].ToString()
                };
                roomList.Add(room);
            }
            reader.Close();
            Connection.Close();
            return roomList;
        }
    }
}