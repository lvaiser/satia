namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addassignedagenttoprocess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Processes", "AssignedAgent_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Processes", "ReviewDate", c => c.DateTime());
            CreateIndex("dbo.Processes", "AssignedAgent_Id");
            AddForeignKey("dbo.Processes", "AssignedAgent_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Processes", "AssignedAgent_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Processes", new[] { "AssignedAgent_Id" });
            AlterColumn("dbo.Processes", "ReviewDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Processes", "AssignedAgent_Id");
        }
    }
}
