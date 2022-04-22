using instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace instagram.Controllers
{
    public class ReactsController : ApiController
    {
        private instagramdb db = new instagramdb();
        
        public string post()
        {
            int iduser = Convert.ToInt32(HttpContext.Current.Request.Form["iduser"]);
            int idpost = Convert.ToInt32(HttpContext.Current.Request.Form["idpost"]);
            int rea = Convert.ToInt32(HttpContext.Current.Request.Form["react"]);

            try
            {
                Post post = db.posts.Find(idpost);
                userinfo u = db.userinfos.Find(iduser);
                
                db.reacts.RemoveRange(db.reacts.Where(m => m.Post == post && m.Userinfo == u).ToList());
                db.SaveChanges();

                React r = new React();
                r.Rect = rea; r.Userinfo = u; r.Post = post;
                db.reacts.Add(r);
                db.SaveChanges();
                return "goodjop";
            }
            catch
            {
                return "badjop";
            }
            return "badjop";
        } 
    }
}
