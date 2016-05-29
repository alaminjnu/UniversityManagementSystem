using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;
using UniversityManagementSystemByReturnNull.Models;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class AllocateClassRoomController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        RoomManager aRoomManager=new RoomManager();
        CourseManager aCourseManager = new CourseManager();
        DayManager aDayManager=new DayManager();

        ClassRoomManager aClassRoomManager = new ClassRoomManager();

        public ActionResult AllocateClassRoom()
        {
            ViewBag.AllDepartment = aDepartmentManager.GetAll();
            ViewBag.AllClassRoom = aRoomManager.GetAllRooms();
            ViewBag.AllDays = aDayManager.GetAllDays();
            return View();
        }
        [HttpPost]
        public ActionResult AllocateClassRoom(ClassRoomAllocation aClassRoom)
        {
            ViewBag.Message = aClassRoomManager.Save(aClassRoom);
            ViewBag.AllDepartment = aDepartmentManager.GetAll();
            ViewBag.AllClassRoom = aRoomManager.GetAllRooms();
            ViewBag.AllDays = aDayManager.GetAllDays();
            return View();
        }

        public JsonResult GettAllCourseByDepartmentId(int departmentId)
        {
            var courses = aCourseManager.GetAll();

            var courseList = courses.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);


        }


        public ActionResult ViewClassRoomAllocation()
        {

            ViewBag.AllDepartment = aDepartmentManager.GetAll();
            return View();
        }

        public JsonResult GetCourseScheduleByDepartmentId(int departmentId)
        {
            List<ViewCourseSchedule> aCourseSchedules = aCourseManager.GetAllCourseSchedule();
            //foreach (var courseSchedule in aCourseSchedules)
            //{
            //    if (courseSchedule.AllocateStatus == "Unallocated")
            //    {
            //        courseSchedule.ScheduleInfo = "Not Scheduled Yet";
            //    }
            //}
            var courseScheduleList = aCourseSchedules.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(courseScheduleList, JsonRequestBehavior.AllowGet);
        }
	}
}