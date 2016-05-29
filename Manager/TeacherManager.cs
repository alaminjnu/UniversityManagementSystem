using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class TeacherManager
    {
        TeacherGateway teacherGateway = new TeacherGateway();
        public IEnumerable<Teacher> GetAllTeacher()
        {
            return teacherGateway.GetAllTeacher();
        }

        public bool IsEmailExist(string email)
        {
            TeacherGateway teacherGateway = new TeacherGateway();
            return teacherGateway.IsEmailExist(email);
        }

        public int Save(Teacher teacher)
        {

            TeacherGateway teacherGateway = new TeacherGateway();
            return teacherGateway.Save(teacher);
        }
    }
}