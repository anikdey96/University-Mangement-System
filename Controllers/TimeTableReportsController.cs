using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University_Mangement_System.Controllers
{
    public class TimeTableReportsController : Controller
    {
        private UniversityMgtDbEntities db = new UniversityMgtDbEntities();
        
        // GET: TimeTableReports
        public ActionResult TeacherReport(int? id)
        {
            var teacherclas = db.TimeTblTables.Where(t => t.StaffID == id).OrderByDescending(e => e.TimeTableID);

            return View(teacherclas);
        }

        public ActionResult TeacherWiseReport()
        {
            var teacherclas = db.TimeTblTables.Where(t => t.IsActive == true).OrderBy(e => e.StaffID);

            return View(teacherclas);
        }
    }
}