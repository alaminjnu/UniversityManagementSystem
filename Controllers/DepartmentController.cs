using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(Department department)
        {
            if (!departmentManager.IsNameExist(department.Name))
            {
                if (!departmentManager.IsCodeExist(department.Code))
                {
                    if (department.Code.Length >= 2 && department.Code.Length <= 7)
                    {
                        ViewBag.Msg = departmentManager.Save(department) > 0 ? "Saved Successfully" : "Save Failed";
                    }
                    else
                    {
                        ViewBag.Message = "Code Must be 2-7 Chars Long ";

                    }
                }
                else
                {
                    ViewBag.Message = "Code Already Exist ";
                }
            }
            else
            {
                ViewBag.Message = "Name Already Exist ";
            }
            return View();
        }

        public ActionResult Show()
        {
            ViewBag.Departments = departmentManager.GetAll();
            return View();
        }
    }
}