using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class StudentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        StudentManager studentManager = new StudentManager();

        public ActionResult StudentInfo()
        {
            List<Student> students = studentManager.GetAllLastStudent().ToList();

            return View(students);
        }

        public ActionResult Register()
        {
            ViewBag.Departments = departmentManager.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Register(Student student)
        {
            if (!studentManager.IsEmailExist(student.Email))
            {
                ViewBag.Msg= studentManager.Save(student) > 0 ? "Saved Succesfully" : "Save Failed";

                if (ViewBag.Msg == "Saved Succesfully")
                {
                    return RedirectToAction("StudentInfo");
                }
            }
            else
            {
                ViewBag.Message = "Email is Already Exist";
            }
            ViewBag.Departments = departmentManager.GetAll();
            return View();
        }
	}
}