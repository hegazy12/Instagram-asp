using instagram.Models;
using instagram.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace instagram.Controllers
{
    public class HomeController : Controller
    {
        private User1 user = new User1();
        private instagramdb db = new instagramdb();

        public ActionResult Index()
        {
            Session["Iduser"] = "0";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            userinfo u = user.login(form["email"].ToString(), form["pass"].ToString());

            if (u == null)
            {
                return View();
            }

            Session["Iduser"] = u.Id.ToString();

            return RedirectToAction("Index", "Porfile");

        }

        [HttpGet]
        public ActionResult Sinup()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Sinup(userinfo userinfo, string Repass, HttpPostedFileBase photo)
        {
            if (userinfo.password != Repass)
            {
                return View(userinfo);
            }

            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    string _FileName = Path.GetFileName(photo.FileName);
                    string _path = Path.Combine(Server.MapPath("~/photos"), _FileName);
                    photo.SaveAs(_path);
                    userinfo.photo = "/photos/" + _FileName;
                }

                db.userinfos.Add(userinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userinfo);
        }


        public ActionResult About()
        {
            return View();
        }
        
    }
}