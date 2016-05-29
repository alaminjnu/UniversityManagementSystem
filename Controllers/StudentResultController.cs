using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class StudentResultController : Controller
    {
        StudentResultManager studentResultManager = new StudentResultManager();

        public ActionResult Save()
        {
            IEnumerable<Student> students = studentResultManager.GetAllStudent;
            IEnumerable<Course> courses = studentResultManager.GetAllCourse;
            ViewBag.Students = students;
            ViewBag.Courses = courses;
            return View();
        }

        [HttpPost]
        public ActionResult Save(StudentResult studentResult)
        {
            try
            {
                ViewBag.Message = studentResultManager.Save(studentResult);
                IEnumerable<Student> students = studentResultManager.GetAllStudent;
                IEnumerable<Course> courses = studentResultManager.GetAllCourse;
                ViewBag.Students = students;
                ViewBag.Courses = courses;

                return View();
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetStudentById(int studentId)
        {
            List<StudentViewModel> student = studentResultManager.GetStudentInformationById(studentId);
            StudentViewModel aStudentViewModel = student.ToList().Find(st => st.Id == studentId);
            return Json(aStudentViewModel, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCoursesTakebByStudent(int studentId)
        {

            IEnumerable<Course> courses = studentResultManager.GetCoursesTakenByaStudentById(studentId);
            return Json(courses, JsonRequestBehavior.AllowGet);

            //Course aStudentResult = studentResultManager.GetCoursesTakenByaStudentById(studentId).ToList().Find(st => st.Id == studentId);
            //IEnumerable<Course> courses = studentResultManager.GetCoursesTakenByaStudentById(studentId).ToList().FindAll(d =>d.Id == studentId );
            //return Json(courses, JsonRequestBehavior.AllowGet);


            //StudentResult aStudentResult = studentResultManager.GetCoursesTakenByStudentId(studentId).ToList().Find(st => st.Id == studentId);
            //IEnumerable<StudentResult> courses = studentResultManager.GetCoursesTakenByStudentId(studentId).ToList().FindAll(d => d.CourseId == aStudentResult.CourseId);
            //return Json(courses, JsonRequestBehavior.AllowGet);
        }
	}
}