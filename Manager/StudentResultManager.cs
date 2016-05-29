using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class StudentResultManager
    {
        StudentResultGateway studentResultGateway = new StudentResultGateway();
        public IEnumerable<Student> GetAllStudent
        {
            get { return studentResultGateway.GetAllStudent; }
        }


        public IEnumerable<Course> GetAllCourse
        {
            get { return studentResultGateway.GetAllCourse; }
        }

        public string Save(StudentResult studentResult)
        {
            bool isResultExits = IsResulExits(studentResult);
            if (isResultExits)
            {
                return "This course result already saved";

            }
            if (studentResultGateway.Insert(studentResult) > 0)
            {
                return "Saved sucessfull!";
            }
            return "Failed to save";
        }
        private bool IsResulExits(StudentResult studentResult)
        {
            StudentResult result =
                GetAllResult.ToList()
                    .Find(st => st.StudentId == studentResult.StudentId && st.CourseId == studentResult.CourseId);
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<StudentResult> GetAllResult
        {
            get { return studentResultGateway.GetAllResult; }
        }

        public List<StudentViewModel> GetStudentInformationById(int id)
        {
            return studentResultGateway.GetStudentInformationById(id);
        }

        public IEnumerable<Course> GetCoursesTakenByaStudentById(int id)
        {
            return studentResultGateway.GetCoursesTakeByaStudentByStudentId(id);
        }

        public IEnumerable<StudentResultViewModel> GetStudentResultByStudentId(int id)
        {
            return studentResultGateway.GetStudentResultByStudentId(id);
        }
    }
}