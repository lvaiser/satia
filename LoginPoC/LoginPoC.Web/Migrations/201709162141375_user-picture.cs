namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userpicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PictureId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "PictureId");
            AddForeignKey("dbo.AspNetUsers", "PictureId", "dbo.Files", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "PictureId", "dbo.Files");
            DropIndex("dbo.AspNetUsers", new[] { "PictureId" });
            DropColumn("dbo.AspNetUsers", "PictureId");
        }
    }
}
