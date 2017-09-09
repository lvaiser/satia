namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_process_type_fields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessTypeFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                        ProcessType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProcessTypes", t => t.ProcessType_Id)
                .Index(t => t.ProcessType_Id);
            
            AddColumn("dbo.ProcessTypes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProcessTypes", "URLVideo", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessTypeFields", "ProcessType_Id", "dbo.ProcessTypes");
            DropIndex("dbo.ProcessTypeFields", new[] { "ProcessType_Id" });
            DropColumn("dbo.ProcessTypes", "URLVideo");
            DropColumn("dbo.ProcessTypes", "IsActive");
            DropTable("dbo.ProcessTypeFields");
        }
    }
}
