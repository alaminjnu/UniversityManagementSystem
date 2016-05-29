using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class StudentResultViewController : Controller
    {
        StudentResultManager studentResultManager = new StudentResultManager();

        public ActionResult ViewStudentResult()
        {
            IEnumerable<Student> students = studentResultManager.GetAllStudent;
            ViewBag.Students = students;
            return View();
        }
        //[HttpPost]
        //public ActionResult ViewStudentResult(int studentId)
        //{

        //    IEnumerable<Student> students = studentResultManager.GetAllStudent;
        //    ViewBag.Students = students;
        //    IEnumerable<StudentResultViewModel> studentResults = studentResultManager.GetStudentResultByStudentId(studentId);
        //    return View(studentResults);
        //   // return new Rotativa.ActionAsPdf("ViewStudentResult");
        //}


        public JsonResult GetStudentResultByStudentId(int studentId)
        {
            IEnumerable<StudentResultViewModel> studentResults = studentResultManager.GetStudentResultByStudentId(studentId);
            return Json(studentResults, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPDF()
        {
            IEnumerable<Student> students = studentResultManager.GetAllStudent;
            IEnumerable<StudentResultViewModel> studentResults = studentResultManager.GetStudentResultByStudentId(6);

            ViewBag.Students = students;
            ViewBag.StudentResults = studentResults;
            return View();
        }



        //public ActionResult GeneratePDF()
        //{
        //    return new Rotativa.ActionAsPdf("GetPDF");
        //}
	}
}