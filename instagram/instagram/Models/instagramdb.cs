using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace instagram.Models
{
    public class instagramdb:DbContext
    {
        public instagramdb() : base("instagramdb")
        {
        }
        public DbSet<userinfo> userinfos { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<React> reacts { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<FriendRequstes> friendRequstes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Post>()
            .HasRequired<userinfo>(s => s.Userinfo)
            .WithMany(g => g.Posts)
            .WillCascadeOnDelete(true);
            
            modelBuilder.Entity<React>()
            .HasRequired<Post>(s => s.Post)
            .WithMany(g => g.Reacts)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
            .HasRequired<Post>(s => s.Post)
            .WithMany(g => g.Comments)
            .WillCascadeOnDelete(true);

           modelBuilder.Entity<Friends>()
            .HasRequired<userinfo>(s => s.FriendOne)
            .WithMany(g => g.Friends)
            .WillCascadeOnDelete(true);

           modelBuilder.Entity<FriendRequstes>()
            .HasRequired<userinfo>(s => s.Sender)
            .WithMany(g => g.FriendRequstes)
            .WillCascadeOnDelete(true);

        }

    }

    public class userinfo
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string lname { get; set; }
        public string photo { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }


        public virtual List<Post> Posts {get; set;}
        public virtual List<Friends> Friends { get; set;}
        public virtual List<FriendRequstes> FriendRequstes {get; set;}
        public virtual List<React> Reacts {get; set;}
        public virtual List<Comment> Comments { get; set; }

    }


    public class Post
    {
        public int id { get; set; }
        public string photo { get; set; }
        public virtual userinfo Userinfo {get; set;}
        
        public virtual List<React> Reacts { get; set; }
        public virtual List<Comment> Comments { get; set;}
    }

    public class Comment
    {
        public int Id { get; set; }
        public string Coment { get; set; }

        
        public virtual userinfo Userinfo {get;set;}
        public virtual Post Post {get;set;}

    }

    public class React
    {
        public int Id { get; set; }
        public int Rect { get; set; }

        public virtual userinfo Userinfo {get; set;}
        
        public virtual Post Post {get; set;}
    }

    public class Friends
    {
        public int id {get;set;}
      
        public virtual userinfo FriendOne {get;set;}
     
        public virtual userinfo FriendTwo {get;set;}
    }
    
    public class FriendRequstes
    {   
        public int Id {set;get;}
        
        public virtual userinfo Sender { get; set; }
        public virtual userinfo  Receiver { get; set; }

    }
}