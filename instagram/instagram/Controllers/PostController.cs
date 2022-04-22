using instagram.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace instagram.Controllers
{
    public class PostController : Controller
    {
        private instagramdb db = new instagramdb();

        // GET: Post
        public ActionResult Create()
        {
            if(Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase photo)
        {
            int x = Convert.ToInt32(Session["Iduser"]);
            userinfo User = db.userinfos.Find(x);
            Post post = new Post();
            
            try
            {
                
                if (photo != null)
                {
                    string _FileName = Path.GetFileName(photo.FileName);
                    string _path = Path.Combine(Server.MapPath("~/photos"), _FileName);
                    photo.SaveAs(_path);
                    post.photo = "/PostPhoto/" + _FileName;
                    post.Userinfo = User;
                }
                db.posts.Add(post);
                db.SaveChanges();
            }
            catch
            {
                ViewBag.Ex = "";
                return View();
            }

            return RedirectToAction("Index", "porfile");
        }

    }
}