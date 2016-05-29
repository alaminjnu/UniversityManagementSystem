using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemByReturnNull.Models
{
    public class ClassRoomAllocation
    {

        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int RoomId { get; set; }
        public int DayId { get; set; }
        public DateTime Fromtime { get; set; }
        public DateTime Totime { get; set; }
        
    }
}