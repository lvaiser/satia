namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_process_documents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsAvailable = c.Boolean(nullable: false),
                        Document_Id = c.Int(),
                        Process_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProcessTypeDocuments", t => t.Document_Id)
                .ForeignKey("dbo.Processes", t => t.Process_Id)
                .Index(t => t.Document_Id)
                .Index(t => t.Process_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessDocuments", "Process_Id", "dbo.Processes");
            DropForeignKey("dbo.ProcessDocuments", "Document_Id", "dbo.ProcessTypeDocuments");
            DropIndex("dbo.ProcessDocuments", new[] { "Process_Id" });
            DropIndex("dbo.ProcessDocuments", new[] { "Document_Id" });
            DropTable("dbo.ProcessDocuments");
        }
    }
}
