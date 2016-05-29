using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemByReturnNull.Manager;

namespace UniversityManagementSystemByReturnNull.Controllers
{
    public class UnallocateClassroomController : Controller
    {
        ClassRoomManager classRoomManager = new ClassRoomManager();
        //
        // GET: /UnassignCourse/
        public ActionResult UnAllocate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAllocate(int? id)
        {
            ViewBag.Message = classRoomManager.UnAllocateClassroom();
            return View();
        }
	}
}