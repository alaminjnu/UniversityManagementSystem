using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class ViewCourseController : Controller
    {
       
            //
            // GET: /ViewCourse/
            DepartmentManager departmentManager = new DepartmentManager();
            CourseManager courseManager = new CourseManager();

            public ActionResult ShowCourseStatistics()
            {
                IEnumerable<Department> departments = departmentManager.GetAll();
                ViewBag.Departments = departments;
                IEnumerable<ViewCourseModel> courseViewModels = courseManager.GetViewCourseModel();
                return View(courseViewModels);
            }
            public JsonResult GetCourseInformationByDepartmentId(int departmentId)
            {
                IEnumerable<ViewCourseModel> courseViewModels = courseManager.GetViewCourseModel().ToList().FindAll(deptId => deptId.DepartmentId == departmentId);
                return Json(courseViewModels, JsonRequestBehavior.AllowGet);


            
        }
	}
}