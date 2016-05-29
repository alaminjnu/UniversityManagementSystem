using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class CourseAssignToTeacherController : Controller
    {
        CourseAssignToTeacherManager courseAssignToTeacherManager = new CourseAssignToTeacherManager();
        DepartmentManager departmentManager = new DepartmentManager();
        TeacherManager teacherManager = new TeacherManager();
        CourseManager courseManager = new CourseManager();
        
        public ActionResult CourseAssignToTeacher()
        {
            ViewBag.Departments = departmentManager.GetAll();
           // ViewBag.Teachers = teacherManager.GetAllTeacher();
            //ViewBag.Courses = courseManager.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult CourseAssignToTeacher(CourseAssignToTeacher courseAssignToTeacher)
        {
            ViewBag.Message = courseAssignToTeacherManager.Save(courseAssignToTeacher);
            ViewBag.Departments = departmentManager.GetAll();
            ViewBag.Teachers = teacherManager.GetAllTeacher();
            ViewBag.Courses = courseManager.GetAll();
            return View();
        }

        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {
            var teachers = teacherManager.GetAllTeacher();
            var teacherList = teachers.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(teacherList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            IEnumerable<Course> courses = courseManager.GetAll();
            var courseList = courses.Where(c => c.DepartmentId == departmentId).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherById(int teacherId)
        {
            IEnumerable<Teacher> teachers = teacherManager.GetAllTeacher();
            Teacher aTeacher = teachers.ToList().Find(t => t.Id == teacherId);
            return Json(aTeacher, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseById(int courseId)
        {
            IEnumerable<Course> courses = courseManager.GetAll();
            Course aCourse = courses.ToList().Find(c => c.Id == courseId);
            return Json(aCourse, JsonRequestBehavior.AllowGet);
        }
	}
}