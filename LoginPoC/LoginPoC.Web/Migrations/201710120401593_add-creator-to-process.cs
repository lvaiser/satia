namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcreatortoprocess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Processes", "Creator_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Processes", "Creator_Id");
            AddForeignKey("dbo.Processes", "Creator_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Processes", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Processes", new[] { "Creator_Id" });
            DropColumn("dbo.Processes", "Creator_Id");
        }
    }
}
