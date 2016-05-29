using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    
    public class ClassRoomManager
    {
        ClassRoomGateway classRoomGateway = new ClassRoomGateway();
        public string Save(ClassRoomAllocation aClassRoom)
        {

            if (aClassRoom.Fromtime >= aClassRoom.Totime)
            {
                return "End Time Must Be Grater Than Start Time!";
            }
            else
            {
                bool isTimeScheduleValid = IsTimeScheduleValid(aClassRoom.RoomId, aClassRoom.DayId, aClassRoom.Fromtime, aClassRoom.Totime);

                if (isTimeScheduleValid != true)
                {

                    if (classRoomGateway.Save(aClassRoom) > 0)
                    {
                        return "Class Allocated Sucessfully!";
                    }
                    else
                    {
                        return "Class Allocation Failed... Please Try Again";
                    }
                }
                else
                {
                    return "Class Overlapping Is Invalid";
                }
            }


        }
          private bool IsTimeScheduleValid(int roomId, int dayId, DateTime startTime, DateTime endTime)
        {
            List<ClassRoomAllocation> schedule = classRoomGateway.GetClassSchedulByStartAndEndingTime(roomId, dayId, startTime, endTime);
            foreach (var sd in schedule)
            {
                if ((sd.DayId == dayId && roomId == sd.RoomId) &&
                                 (startTime < sd.Fromtime && endTime > sd.Fromtime)
                                 || (startTime < sd.Fromtime && endTime > sd.Fromtime) ||
                                 (startTime == sd.Fromtime) || (sd.Fromtime < startTime && sd.Totime > startTime)
                                 )
                {
                    return true;
                }

            }
            return false;
        }

        public string UnAllocateClassroom()
        {
            if (classRoomGateway.UnAllocateClassroom() > 0)
            {

                return "Unallocate classrooms Sucessfull!";
            }
            return "Failed to unallocate";

        }
    }
}