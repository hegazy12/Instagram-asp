using instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace instagram.Controllers
{
    public class PorfileController : Controller
    {
        private instagramdb db = new instagramdb();
        // GET: Porfile
        public ActionResult Index()
        {
            if (Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt32(Session["Iduser"]);

            userinfo userinfo = new userinfo();

            userinfo = db.userinfos.Find(x);

            if (userinfo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(userinfo);
            
        }




    }
}