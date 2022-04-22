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
    public class CommentsController : ApiController
    {           
        private instagramdb db = new instagramdb();
                
        public string post()
        {       
            int iduser = Convert.ToInt32(HttpContext.Current.Request.Form["iduser"]);
            int idpost = Convert.ToInt32(HttpContext.Current.Request.Form["idpost"]);
            string comment = HttpContext.Current.Request.Form["comment"];
                
            try 
            {   
                Comment comm = new Comment();
                comm.Post = db.posts.Find(idpost);
                comm.Userinfo = db.userinfos.Find(iduser);
                comm.Coment = comment;
                
                db.comments.Add(comm);
                db.SaveChanges();
                return comment;
            }   
            catch
            {   
                return "BadJop";
            }   
                
                return comment;
        }
    }
}
