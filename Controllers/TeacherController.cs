using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class TeacherController : Controller
    {
        TeacherManager teacherManager = new TeacherManager();
        DesignationManager designationManager = new DesignationManager();
        DepartmentManager departmentManager = new DepartmentManager();

        public ActionResult Save()
        {
            ViewBag.Designations = designationManager.GetAll();
            ViewBag.Departments = departmentManager.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
            if (!teacherManager.IsEmailExist(teacher.Email))
            {
                ViewBag.Msg = teacherManager.Save(teacher) > 0 ? "Saved Succesfully" : "Save Failed";
            }
            else
            {
                ViewBag.Message = "Email is Already Exist";
            }
            ViewBag.Designations = designationManager.GetAll();
            ViewBag.Departments = departmentManager.GetAll();
            return View();
        }
    }
}