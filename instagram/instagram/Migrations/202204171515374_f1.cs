namespace instagram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coment = c.String(),
                        Userinfo_Id = c.Int(),
                        Post_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.userinfoes", t => t.Userinfo_Id)
                .ForeignKey("dbo.Posts", t => t.Post_id, cascadeDelete: true)
                .Index(t => t.Userinfo_Id)
                .Index(t => t.Post_id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(),
                        Userinfo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.userinfoes", t => t.Userinfo_Id, cascadeDelete: true)
                .Index(t => t.Userinfo_Id);
            
            CreateTable(
                "dbo.Reacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rect = c.Int(nullable: false),
                        Post_id = c.Int(nullable: false),
                        Userinfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_id, cascadeDelete: true)
                .ForeignKey("dbo.userinfoes", t => t.Userinfo_Id)
                .Index(t => t.Post_id)
                .Index(t => t.Userinfo_Id);
            
            CreateTable(
                "dbo.userinfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fname = c.String(),
                        lname = c.String(),
                        photo = c.String(),
                        city = c.String(),
                        country = c.String(),
                        email = c.String(),
                        phone = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FriendRequstes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Receiver_Id = c.Int(),
                        Sender_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.userinfoes", t => t.Receiver_Id)
                .ForeignKey("dbo.userinfoes", t => t.Sender_Id, cascadeDelete: true)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FriendOne_Id = c.Int(nullable: false),
                        FriendTwo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.userinfoes", t => t.FriendOne_Id, cascadeDelete: true)
                .ForeignKey("dbo.userinfoes", t => t.FriendTwo_Id)
                .Index(t => t.FriendOne_Id)
                .Index(t => t.FriendTwo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Post_id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Userinfo_Id", "dbo.userinfoes");
            DropForeignKey("dbo.Reacts", "Userinfo_Id", "dbo.userinfoes");
            DropForeignKey("dbo.Friends", "FriendTwo_Id", "dbo.userinfoes");
            DropForeignKey("dbo.Friends", "FriendOne_Id", "dbo.userinfoes");
            DropForeignKey("dbo.FriendRequstes", "Sender_Id", "dbo.userinfoes");
            DropForeignKey("dbo.FriendRequstes", "Receiver_Id", "dbo.userinfoes");
            DropForeignKey("dbo.Comments", "Userinfo_Id", "dbo.userinfoes");
            DropForeignKey("dbo.Reacts", "Post_id", "dbo.Posts");
            DropIndex("dbo.Friends", new[] { "FriendTwo_Id" });
            DropIndex("dbo.Friends", new[] { "FriendOne_Id" });
            DropIndex("dbo.FriendRequstes", new[] { "Sender_Id" });
            DropIndex("dbo.FriendRequstes", new[] { "Receiver_Id" });
            DropIndex("dbo.Reacts", new[] { "Userinfo_Id" });
            DropIndex("dbo.Reacts", new[] { "Post_id" });
            DropIndex("dbo.Posts", new[] { "Userinfo_Id" });
            DropIndex("dbo.Comments", new[] { "Post_id" });
            DropIndex("dbo.Comments", new[] { "Userinfo_Id" });
            DropTable("dbo.Friends");
            DropTable("dbo.FriendRequstes");
            DropTable("dbo.userinfoes");
            DropTable("dbo.Reacts");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
