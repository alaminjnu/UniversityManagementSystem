using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Gateway
{
    public class ClassRoomGateway:CommonGateway
    {
        public int Save(ClassRoomAllocation aClassRoom)
        {


            string query = "INSERT INTO AllocateClassRoom VALUES(@deptId,@courseId,@roomId,@dayId,@startTime,@endTime,@allocateStatus)";

            Command = new SqlCommand(query, Connection);
            Connection.Open();

            Command.Parameters.Clear();

            Command.Parameters.Add("deptId", SqlDbType.Int);
            Command.Parameters["deptId"].Value = aClassRoom.DepartmentId;

            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = aClassRoom.CourseId;

            Command.Parameters.Add("roomId", SqlDbType.Int);
            Command.Parameters["roomId"].Value = aClassRoom.RoomId;

            Command.Parameters.Add("dayId", SqlDbType.Int);
            Command.Parameters["dayId"].Value = aClassRoom.DayId;

            Command.Parameters.Add("startTime", SqlDbType.VarChar);
            Command.Parameters["startTime"].Value = aClassRoom.Fromtime.ToShortTimeString();

            Command.Parameters.Add("endTime", SqlDbType.VarChar);
            Command.Parameters["endTime"].Value = aClassRoom.Totime.ToShortTimeString();


            Command.Parameters.Add("allocateStatus", SqlDbType.VarChar);
            Command.Parameters["allocateStatus"].Value = "Allocated";

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }
        public List<ClassRoomAllocation> GetClassSchedulByStartAndEndingTime(int roomId, int dayId, DateTime startTime,
          DateTime endTime)
        {

            string query = "Select * from AllocateClassRoom Where DayId=" + dayId + " AND RoomId=" + roomId +
                           " AND AllocateStatus='Allocated'" ;
            List<ClassRoomAllocation> tempClassSchedules = new List<ClassRoomAllocation>();
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            SqlDataReader Reader = Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                ClassRoomAllocation temp = new ClassRoomAllocation
                {
                    //AllocatedRoomId = Convert.ToInt32(Reader["Id"].ToString()),
                    DepartmentId = Convert.ToInt32(Reader["DepartmentId"].ToString()),
                    CourseId = Convert.ToInt32(Reader["CourseId"].ToString()),
                    RoomId = Convert.ToInt32(Reader["RoomId"].ToString()),
                    DayId = Convert.ToInt32(Reader["DayId"].ToString()),
                    Fromtime = Convert.ToDateTime(Reader["FromTime"].ToString()),
                    Totime = Convert.ToDateTime(Reader["ToTime"].ToString())

                };
                tempClassSchedules.Add(temp);
            }
            Reader.Close();
            Connection.Close();
            return tempClassSchedules;

        }

        //internal bool isexist(ClassRoomAllocation aClassRoom)
        //{


        //    //string query = "SELECT DepartmentCode FROM Department WHERE DepartmentCode='" + p + "'";
        //    string query = "SELECT * FROM AllocateClassRoom WHERE RoomId = @RoomId and Day = @Day and Fromtime=@Fromtime";
        //    SqlCommand command = new SqlCommand(query, Connection);

        //    command.Parameters.AddWithValue("RoomId", aClassRoom.RoomId);
        //    command.Parameters.AddWithValue("Day", aClassRoom.Day);
        //    command.Parameters.AddWithValue("Fromtime", aClassRoom.Fromtime);


        //    Connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();

        //    if (reader.HasRows)
        //    {
        //        Connection.Close();
        //        return true;
        //    }
        //    else
        //    {
        //        Connection.Close();
        //        return false;
        //    }
        //}
        public int UnAllocateClassroom()
        {
            Connection.Open();


            Query = "UPDATE AllocateClassRoom SET AllocateStatus='Unallocated'";
            SqlCommand Command = new SqlCommand(Query, Connection);

            int i = Command.ExecuteNonQuery();
            //int i = UnAllocate();
            Connection.Close();
            return i;
        }


        //private int UnAllocate()
        //{
        //    Query = "UPDATE VIEW ClassSchedule SET Schedule_Information='Not Scheduled Yet ' WHERE Schedule_Information LIKE 'R%'";
        //    SqlCommand Command = new SqlCommand(Query, Connection);
        //    return Command.ExecuteNonQuery();
        //}
    }
}