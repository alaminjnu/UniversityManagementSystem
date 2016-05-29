using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class EnrollStudentController : Controller
    {
        StudentManager studentManager = new StudentManager();
        CourseManager courseManager = new CourseManager();
        EnrollStudentManager enrollStudentManager = new EnrollStudentManager();

        public ActionResult Enroll()
        {
            ViewBag.Students = studentManager.GetAll();
            ViewBag.Courses = courseManager.GetAll();
            return View();
        }

        [HttpPost]

        public ActionResult Enroll(EnrollStudent enrollStudent)
        {

            ViewBag.Message = enrollStudentManager.Save(enrollStudent);
            ViewBag.Students = studentManager.GetAll();
            ViewBag.Courses = courseManager.GetAll();
            return View();
        }


        public JsonResult GetStudentById(int studentId)
        {
            List<StudentViewModel> students = studentManager.GetStudentInformationById(studentId);
            StudentViewModel aStudentViewModel = students.ToList().Find(st => st.Id == studentId);
            return Json(aStudentViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByStudentId(int studentId)
        {
            Student aStudent = studentManager.GetAll().ToList().Find(st => st.Id == studentId);
            IEnumerable<Course> courses = courseManager.GetAll().ToList().FindAll(d => d.DepartmentId == aStudent.DepartmentId);
            return Json(courses, JsonRequestBehavior.AllowGet);


        }
	}
}