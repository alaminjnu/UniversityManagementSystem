using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemByReturnNull.Models
{
    public class ViewCourseModel
    {
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string Teacher { get; set; }
    }
}