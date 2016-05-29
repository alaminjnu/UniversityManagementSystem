using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemByReturnNull.Models
{
    public class StudentResult
    {

        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        public int CourseId { get; set; }

        public string Grade { get; set; }

    }
}