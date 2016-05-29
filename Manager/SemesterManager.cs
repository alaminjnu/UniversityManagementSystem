using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class SemesterManager
    {
        private SemesterGateway semesterGateway = new SemesterGateway();

        public List<Semester> GetAll()
        {
            return semesterGateway.GetAll();
        }
    }
}