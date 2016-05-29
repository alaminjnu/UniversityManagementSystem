using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class CourseController : Controller
    {

        DepartmentManager departmentManager = new DepartmentManager();
        SemesterManager semesterManager = new SemesterManager();
        CourseManager courseManager = new CourseManager();


        public ActionResult Save()
        {


            List<Department> departments = departmentManager.GetAll();
            List<Semester> semesters = semesterManager.GetAll();
            ViewBag.Departments = departments;
            ViewBag.Semesters = semesters;
            return View();
        }

        [HttpPost]
        public ActionResult Save(Course aCourse)
        {
            string msg = courseManager.Save(aCourse);
            ViewBag.message = msg;
            List<Department> departments = departmentManager.GetAll();
            List<Semester> semesters = semesterManager.GetAll();
            ViewBag.Departments = departments;
            ViewBag.Semesters = semesters;

            return View();
        }
    }
}