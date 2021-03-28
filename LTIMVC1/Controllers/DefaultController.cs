using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LTIMVC1.Models;

namespace LTIMVC1.Controllers
{
    public class DefaultController : Controller
    {
        LTIMVCEntities1 db = new LTIMVCEntities1();
        // GET: Default
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(usertable u)
        {
            u.username = Request.Form["txtname"];
            u.email = Request.Form["email"];
            u.password = Request.Form["txtpass"];
            u.city = Request.Form["city"];
            u.mobile = Convert.ToInt32(Request.Form["mobile"]);
            db.usertables.Add(u);
            int res = db.SaveChanges();
            if (res>0)
                ModelState.AddModelError("", "Registration Complete");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string sname, string spass)
        {
            sname = Request.Form["uid"];
            spass = Request.Form["pid"];
            var query = from t in db.usertables
                        where t.username == sname && t.password == spass
                        select t;
            if (query.Count() != 1)
            {
                ModelState.AddModelError("", "Invalid User");
                Response.Write("<script language='javascript'>window.alert('INVALID USER');</script>");
            }
            else
            {
                Response.Write("<script language='javascript'>window.alert('Welcome Back " + sname + "');</script>");
                ModelState.AddModelError("", "Welcome  Back " + sname);
            }
            return View();
        }
    }
}