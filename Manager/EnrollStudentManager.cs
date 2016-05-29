using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class EnrollStudentManager
    {

        EnrollStudentGateway enrollStudentGateway = new EnrollStudentGateway();
        //public int Save(EnrollStudent enrollStudent)
        //{
        //    EnrollStudentGateway enrollStudentGateway = new EnrollStudentGateway();
        //    return enrollStudentGateway.Save(enrollStudent);
        //}


        public string Save(EnrollStudent enrollStudent)
        {
            EnrollStudent aenrollStudent =
                GetEnrollCourses.ToList()
                    .Find(
                        st =>
                            st.StudentId == enrollStudent.StudentId &&
                            st.CourseId == enrollStudent.CourseId);
            if (aenrollStudent == null)
            {
                if (enrollStudentGateway.Save(enrollStudent) > 0)
                {
                    return "Saved Sucessfully!";
                }
                return "Failed to save";
            }
            return "This course already taken by the student";
        }

        public IEnumerable<EnrollStudent> GetEnrollCourses
        {
            get { return enrollStudentGateway.GetEnrollCourses; }
        }


        public bool IsStudentIdExist(int studentId)
        {

            return enrollStudentGateway.IsStudentIdExist(studentId);
        }

        public bool IsCourseIdExist(int courseId)
        {

            return enrollStudentGateway.IsCourseIdExist(courseId);
        }
    }
}