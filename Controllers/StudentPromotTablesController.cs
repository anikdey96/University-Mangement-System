﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;

namespace University_Mangement_System.Controllers
{
    public class StudentPromotTablesController : Controller
    {
        private UniversityMgtDbEntities db = new UniversityMgtDbEntities();

        // GET: StudentPromotTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var studentPromotTables = db.StudentPromotTables.Include(s => s.ClassTable).Include(s => s.ProgrameSessionTable).Include(s => s.SectionTable).Include(s => s.StudentTable).OrderByDescending(s => s.StudentPromotID);
            return View(studentPromotTables.ToList());
        }

        public ActionResult GetPromotClsList(string sid)
        {
            int studentid = Convert.ToInt32(sid);
            var student = db.StudentTables.Find(studentid);
            {
                List<ClassTable> classTable = new List<ClassTable>();
                foreach (var cls in db.ClassTables.Where(cls => cls.ClassID > student.ClassID))
                {
                    classTable.Add(new ClassTable { ClassID = cls.ClassID, Name = cls.Name });
                }
                return Json(new { data = classTable }, JsonRequestBehavior.AllowGet);
            }
        }

            public ActionResult GetAnnulFee(string sid)
            {
                int progressid = Convert.ToInt32(sid);
                var ps = db.ProgrameSessionTables.Find(progressid);
                var annulfee = db.AnnualTables.Where(a => a.AnnualID == ps.ProgrameID).SingleOrDefault();
                double? fee = annulfee.Fees;
                return Json(new { fees = fee }, JsonRequestBehavior.AllowGet);
            }

        

        // GET: StudentPromotTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromotTable studentPromotTable = db.StudentPromotTables.Find(id);
            if (studentPromotTable == null)
            {
                return HttpNotFound();
            }
            return View(studentPromotTable);
        }

        // GET: StudentPromotTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name");
            ViewBag.ProgrameSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details");
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName");
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name");
            return View();
        }

        // POST: StudentPromotTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentPromotTable studentPromotTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            //int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            //studentPromotTable.UserID = userid;

            if (ModelState.IsValid)
            {
                db.StudentPromotTables.Add(studentPromotTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentPromotTable.ClassID);
            ViewBag.ProgrameSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromotTable.ProgrameSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromotTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromotTable.StudentID);
            return View(studentPromotTable);
        }

        // GET: StudentPromotTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromotTable studentPromotTable = db.StudentPromotTables.Find(id);
            if (studentPromotTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentPromotTable.ClassID);
            ViewBag.ProgrameSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromotTable.ProgrameSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromotTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromotTable.StudentID);
            return View(studentPromotTable);
        }

        // POST: StudentPromotTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentPromotTable studentPromotTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            //int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            //studentPromotTable.UserID = userid;
            
            if (ModelState.IsValid)
            {
                db.Entry(studentPromotTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentPromotTable.ClassID);
            ViewBag.ProgrameSessionID = new SelectList(db.ProgrameSessionTables, "ProgrameSessionID", "Details", studentPromotTable.ProgrameSessionID);
            ViewBag.SectionID = new SelectList(db.SectionTables, "SectionID", "SectionName", studentPromotTable.SectionID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", studentPromotTable.StudentID);
            return View(studentPromotTable);
        }

        // GET: StudentPromotTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentPromotTable studentPromotTable = db.StudentPromotTables.Find(id);
            if (studentPromotTable == null)
            {
                return HttpNotFound();
            }
            return View(studentPromotTable);
        }

        // POST: StudentPromotTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            StudentPromotTable studentPromotTable = db.StudentPromotTables.Find(id);
            db.StudentPromotTables.Remove(studentPromotTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
