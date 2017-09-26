namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprocessesandtypedocuments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        ReviewNotes = c.String(),
                        ReviewDate = c.DateTime(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProcessTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ProcessTypeDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsRequired = c.Boolean(nullable: false),
                        ProcessTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProcessTypes", t => t.ProcessTypeId, cascadeDelete: true)
                .Index(t => t.ProcessTypeId);
            
            CreateTable(
                "dbo.ProcessFields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Process_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processes", t => t.Process_Id)
                .ForeignKey("dbo.ProcessTypeFields", t => t.Type_Id)
                .Index(t => t.Process_Id)
                .Index(t => t.Type_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessFields", "Type_Id", "dbo.ProcessTypeFields");
            DropForeignKey("dbo.ProcessFields", "Process_Id", "dbo.Processes");
            DropForeignKey("dbo.Processes", "Type_Id", "dbo.ProcessTypes");
            DropForeignKey("dbo.ProcessTypeDocuments", "ProcessTypeId", "dbo.ProcessTypes");
            DropIndex("dbo.ProcessFields", new[] { "Type_Id" });
            DropIndex("dbo.ProcessFields", new[] { "Process_Id" });
            DropIndex("dbo.ProcessTypeDocuments", new[] { "ProcessTypeId" });
            DropIndex("dbo.Processes", new[] { "Type_Id" });
            DropTable("dbo.ProcessFields");
            DropTable("dbo.ProcessTypeDocuments");
            DropTable("dbo.Processes");
        }
    }
}
