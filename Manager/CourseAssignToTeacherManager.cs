using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class CourseAssignToTeacherManager
    {
        CourseAssignToTeacherGateway courseAssignToTeacherGateway = new CourseAssignToTeacherGateway();
        //public string Save(CourseAssignToTeacher courseAssignToTeacher)
        //{
            
        //    return courseAssignToTeacherGateway.Save(courseAssignToTeacher);
        //}


        public string Save(CourseAssignToTeacher courseAssignToTeacher)
        {

            CourseAssignToTeacher courseAssignTo = GetAll.ToList().Find(ca => ca.CourseId == courseAssignToTeacher.CourseId);

            if (courseAssignTo == null)
            {
                if (courseAssignToTeacherGateway.Insert(courseAssignToTeacher) > 0)
                {
                    return "Saved sucessfully";
                }
                return "Failed to save";
            }
            CourseAssignToTeacher assignTo = GetAll.ToList().Find(c => c.CourseId == courseAssignToTeacher.CourseId && c.TeacherId == courseAssignToTeacher.TeacherId);
            if (assignTo != null)
            {
                bool st = assignTo.Status;
                if (st)
                {
                    return "Overlapping occured(Course has been assigned to this Teacher Already)";
                }
                if (courseAssignToTeacherGateway.Update(courseAssignToTeacher) > 0)
                {
                    return "Saved sucessfully";
                }
                return "Failed to save";

            }

            return "Overlapping Occured!(Course has already been Assigned to Another Teacher )";
        }


        public IEnumerable<CourseAssignToTeacher> GetAll
        {
            get { return courseAssignToTeacherGateway.GetAll; }
        } 


    }
}