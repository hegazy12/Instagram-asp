using instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace instagram.User
{
    public class User1
    {
        private instagramdb db = new instagramdb();

        public userinfo userinfo = new userinfo();

        public userinfo login(string email, string pass)
        {
            if (email == "" || pass == "")
            {
                return null;
            }
            userinfo = db.userinfos.Where(x => x.email == email && x.password == pass).FirstOrDefault();
            if (userinfo == null)
            {
                return null;
            }
            return userinfo;
        }
        
    }
}