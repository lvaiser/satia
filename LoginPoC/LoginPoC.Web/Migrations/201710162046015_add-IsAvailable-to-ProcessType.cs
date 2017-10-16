namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsAvailabletoProcessType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessTypes", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProcessTypes", "IsAvailable");
        }
    }
}
