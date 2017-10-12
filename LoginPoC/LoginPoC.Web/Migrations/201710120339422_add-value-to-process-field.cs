namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvaluetoprocessfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessFields", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProcessFields", "Value");
        }
    }
}
