using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace University_Mangement_System.Controllers
{
    public class HomeController : Controller
    {
        private UniversityMgtDbEntities db = new UniversityMgtDbEntities();
        public ActionResult Login()
        {
            return View();
        }

      [HttpPost]
        public ActionResult LoginUser (string email, string password)
        {
            try
            {
                if (email != null && password != null)
                {
                    var finduser = db.UserTables.Where(u => u.EmailAddress == email && u.Password == password).ToList();
                    if (finduser.Count() == 1)
                    {
                        Session["UserID"] = finduser[0].UserID;
                        Session ["UserTypeID"] = finduser[0].UserTypeID;
                        Session ["FullName"] = finduser[0].FullName;
                        Session ["UserName"] = finduser[0].UserName;
                        Session ["Password"] = finduser[0].Password;
                        Session ["ContactNo"] = finduser[0].ContactNo;
                        Session ["EmailAddress"] = finduser[0].EmailAddress;
                        Session ["Address"] = finduser[0].Address;


                        string url = string.Empty;
                        if (finduser[0].UserTypeID == 1)
                        {
                            return RedirectToAction("About");
                        }

                        else if (finduser[0].UserTypeID == 1)
                        {
                            return RedirectToAction("About");
                        }

                        else if (finduser[0].UserTypeID == 1)
                        {
                           url =  "About";
                        }
                        else
                        {
                            url = "About";
                        }

                        return RedirectToAction(url);
                    }

                    else
                    {
                        Session["UserID"] = string.Empty;
                        Session["UserTypeID"] = string.Empty;
                        Session["FullName"] = string.Empty;
                        Session["UserName"] = string.Empty;
                        Session["Password"] = string.Empty;
                        Session["ContactNo"] = string.Empty;
                        Session["EmailAddress"] = string.Empty;
                        Session["Address"] = string.Empty;
                        ViewBag.message = "User Name and Password is incorrect!";
                    }

                }

                else
                {

                    Session["UserID"] = string.Empty;
                    Session["UserTypeID"] = string.Empty;
                    Session["FullName"] = string.Empty;
                    Session["UserName"] = string.Empty;
                    Session["Password"] = string.Empty;
                    Session["ContactNo"] = string.Empty;
                    Session["EmailAddress"] = string.Empty;
                    Session["Address"] = string.Empty;
                    ViewBag.message = "Some unexpected occur please try again!";
                }

            }

            catch(Exception ex)
            {
                Session["UserID"] = string.Empty;
                Session["UserTypeID"] = string.Empty;
                Session["FullName"] = string.Empty;
                Session["UserName"] = string.Empty;
                Session["Password"] = string.Empty;
                Session["ContactNo"] = string.Empty;
                Session["EmailAddress"] = string.Empty;
                Session["Address"] = string.Empty;
                ViewBag.message = "Some unexpected occur please try again!";

            }

            return View("Login");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to Jashore University Of Science & Technology Arena";

            return View();
        }

        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["FullName"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["ContactNo"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["Address"] = string.Empty;
            return RedirectToAction("Login");
        }
    }
}