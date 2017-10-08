namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disableusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Disabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Disabled");
        }
    }
}
