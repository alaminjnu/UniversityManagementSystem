using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class DayManager
    {
        DayGateway aDayGateway=new DayGateway();
        public IEnumerable<Day> GetAllDays()
        {
            return aDayGateway.GetAllDays();
        }
    }
}