using instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace instagram.Controllers
{
    public class FriendsController : Controller
    {
        private instagramdb db = new instagramdb();
        // GET: Friends
        public ActionResult All()
        {
            if (Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt32(Session["Iduser"]);

            List<userinfo> All = db.userinfos.Where(m=> m.Id != x).ToList();
            
            List<Friends> friends 
                = db.Friends.Where(m => m.FriendOne == db.userinfos.Find(x)).ToList();
            
            List<FriendRequstes> friendRequstes 
                = db.friendRequstes.Where(m => m.Sender == db.userinfos.Find(x)).ToList();
            
            foreach(var item in friends)
            {
                All.Remove(item.FriendTwo);
            }

            foreach(var item in friendRequstes)
            {
                All.Remove(item.Receiver);
            }
            
            return View(All);
        }

        public ActionResult Myfriends()
        {
            if (Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt32(Session["Iduser"]);
             
            return View(
                db.Friends.Where(m=>m.FriendOne == db.userinfos.Find(x)).ToList());
        }
             
        public ActionResult MyFriendRequst()
        {     
            if(Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
                
            int x = Convert.ToInt32(Session["Iduser"]);
                
            return View(
                db.friendRequstes.Where(m => m.Sender == db.userinfos.Find(x)).ToList()
                );
                 
        }


        public ActionResult FriendRequst()
        {
            if (Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt32(Session["Iduser"]);

            return View(
                db.friendRequstes.Where(m=>m.Receiver == db.userinfos.Find(x)).ToList()
                );
        }

        
        public ActionResult CancelFriendRequest(int? id)
        {
            if (Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt32(Session["Iduser"]);
            
            db.friendRequstes.RemoveRange(
                db.friendRequstes.Where(
                    m=>m.Sender==db.userinfos.Find(id) && m.Receiver == db.userinfos.Find(id)
                    ).ToList()
                );

            return RedirectToAction("MyFriendRequst");
        }


        public ActionResult Acceptfriendrequest(int? id)
        {
            if (Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt32(Session["Iduser"]);
            int y = Convert.ToInt32(id);

            userinfo user = db.userinfos.Find(x);
            userinfo friend = db.userinfos.Find(y);
            
           
            db.Friends.RemoveRange(
                db.Friends.Where(m => m.FriendOne == user && m.FriendTwo == friend).ToList());
            
            db.Friends.RemoveRange(
                db.Friends.Where(m => m.FriendOne == friend && m.FriendTwo == user).ToList());
            
            db.SaveChanges();




            Friends fr = new Friends();
            fr.FriendOne = user; fr.FriendTwo = friend;

            db.Friends.Add(fr);
            db.SaveChanges();

            fr.FriendOne = friend; fr.FriendTwo = user;
            db.SaveChanges();
            
            return RedirectToAction("Myfriends");
        }

        public ActionResult AddFriend(int? id)
        {
            if (Session["Iduser"] == "0" || Session["Iduser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            int x = Convert.ToInt32(Session["Iduser"]);
            int y = Convert.ToInt32(id);

            userinfo user = db.userinfos.Find(x);
            userinfo friend = db.userinfos.Find(y);


            db.friendRequstes.RemoveRange();

            db.friendRequstes.RemoveRange();

            db.SaveChanges();

            return RedirectToAction("All");
        }
    }
}