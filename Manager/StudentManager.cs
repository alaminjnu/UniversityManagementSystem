using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemByReturnNull.Gateway;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Manager
{
    public class StudentManager
    {
        DepartmentGateway departmentGateway = new DepartmentGateway();

        public string GetLastAddedStudentRegistration(string searchKey)
        {
            StudentGateway studentGateway = new StudentGateway();
            return studentGateway.GetLastAddedStudentRegistration(searchKey);

        }

        public int Save(Student student)
        {
            int counter;
            Random random = new Random();
            int value = random.Next(1000);
            Department department = departmentGateway.GetAll().Single(depid => depid.Id == student.DepartmentId);
            string searchKey = department.Code + "-" + DateTime.Now.Year + "-";
            string lastAddedRegistrationNo = GetLastAddedStudentRegistration(searchKey);
            if (lastAddedRegistrationNo == null)
            {
                student.RegistrationNo = searchKey + "001";

            }

            if (lastAddedRegistrationNo != null)
            {
                string tempId = lastAddedRegistrationNo.Substring((lastAddedRegistrationNo.Length - 3), 3);
                counter = Convert.ToInt32(tempId);
                string studentSl = (counter + 1).ToString();


                if (studentSl.Length == 1)
                {

                    student.RegistrationNo = searchKey + "00" + studentSl;

                }
                else if (studentSl.Count() == 2)
                {

                    student.RegistrationNo = searchKey + "0" + studentSl;
                }
                else
                {

                    student.RegistrationNo = searchKey + studentSl;
                }

            }

            StudentGateway studentGateway = new StudentGateway();
            return studentGateway.Save(student);
        }

        public bool IsEmailExist(string email)
        {
            StudentGateway studentGateway = new StudentGateway();
            return studentGateway.IsEmailExist(email);
        }


        public List<Student> GetAll()
        {
            StudentGateway studentGateway = new StudentGateway();
            return studentGateway.GetAll();
        }


        public List<StudentViewModel> GetStudentInformationById(int studentId)
        {
            StudentGateway studentGateway = new StudentGateway();
            return studentGateway.GetStudentInformationById(studentId);
        }

        public List<Student> GetAllLastStudent()
        {
            StudentGateway studentGateway = new StudentGateway();
            return studentGateway.GetAllLastStudent();
        }
    }
}