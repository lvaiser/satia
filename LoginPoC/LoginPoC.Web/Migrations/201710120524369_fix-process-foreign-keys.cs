namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixprocessforeignkeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProcessDocuments", "Document_Id", "dbo.ProcessTypeDocuments");
            DropForeignKey("dbo.ProcessFields", "Type_Id", "dbo.ProcessTypeFields");
            DropForeignKey("dbo.ProcessDocuments", "Process_Id", "dbo.Processes");
            DropForeignKey("dbo.Processes", "Type_Id", "dbo.ProcessTypes");
            DropForeignKey("dbo.ProcessFields", "Process_Id", "dbo.Processes");
            DropIndex("dbo.ProcessDocuments", new[] { "Document_Id" });
            DropIndex("dbo.ProcessDocuments", new[] { "Process_Id" });
            DropIndex("dbo.Processes", new[] { "Type_Id" });
            DropIndex("dbo.ProcessFields", new[] { "Process_Id" });
            DropIndex("dbo.ProcessFields", new[] { "Type_Id" });
            RenameColumn(table: "dbo.ProcessDocuments", name: "Process_Id", newName: "ProcessId");
            RenameColumn(table: "dbo.Processes", name: "AssignedAgent_Id", newName: "AssignedAgentId");
            RenameColumn(table: "dbo.Processes", name: "Creator_Id", newName: "CreatorId");
            RenameColumn(table: "dbo.Processes", name: "Type_Id", newName: "TypeId");
            RenameColumn(table: "dbo.ProcessFields", name: "Process_Id", newName: "ProcessId");
            RenameIndex(table: "dbo.Processes", name: "IX_Creator_Id", newName: "IX_CreatorId");
            RenameIndex(table: "dbo.Processes", name: "IX_AssignedAgent_Id", newName: "IX_AssignedAgentId");
            AddColumn("dbo.ProcessDocuments", "Name", c => c.String());
            AddColumn("dbo.ProcessDocuments", "IsRequired", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProcessFields", "Name", c => c.String());
            AddColumn("dbo.ProcessFields", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.ProcessFields", "IsRequired", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProcessDocuments", "ProcessId", c => c.Int(nullable: false));
            AlterColumn("dbo.Processes", "TypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProcessFields", "ProcessId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProcessDocuments", "ProcessId");
            CreateIndex("dbo.Processes", "TypeId");
            CreateIndex("dbo.ProcessFields", "ProcessId");
            AddForeignKey("dbo.ProcessDocuments", "ProcessId", "dbo.Processes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Processes", "TypeId", "dbo.ProcessTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProcessFields", "ProcessId", "dbo.Processes", "Id", cascadeDelete: true);
            DropColumn("dbo.ProcessDocuments", "Document_Id");
            DropColumn("dbo.ProcessFields", "Type_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcessFields", "Type_Id", c => c.Int());
            AddColumn("dbo.ProcessDocuments", "Document_Id", c => c.Int());
            DropForeignKey("dbo.ProcessFields", "ProcessId", "dbo.Processes");
            DropForeignKey("dbo.Processes", "TypeId", "dbo.ProcessTypes");
            DropForeignKey("dbo.ProcessDocuments", "ProcessId", "dbo.Processes");
            DropIndex("dbo.ProcessFields", new[] { "ProcessId" });
            DropIndex("dbo.Processes", new[] { "TypeId" });
            DropIndex("dbo.ProcessDocuments", new[] { "ProcessId" });
            AlterColumn("dbo.ProcessFields", "ProcessId", c => c.Int());
            AlterColumn("dbo.Processes", "TypeId", c => c.Int());
            AlterColumn("dbo.ProcessDocuments", "ProcessId", c => c.Int());
            DropColumn("dbo.ProcessFields", "IsRequired");
            DropColumn("dbo.ProcessFields", "Type");
            DropColumn("dbo.ProcessFields", "Name");
            DropColumn("dbo.ProcessDocuments", "IsRequired");
            DropColumn("dbo.ProcessDocuments", "Name");
            RenameIndex(table: "dbo.Processes", name: "IX_AssignedAgentId", newName: "IX_AssignedAgent_Id");
            RenameIndex(table: "dbo.Processes", name: "IX_CreatorId", newName: "IX_Creator_Id");
            RenameColumn(table: "dbo.ProcessFields", name: "ProcessId", newName: "Process_Id");
            RenameColumn(table: "dbo.Processes", name: "TypeId", newName: "Type_Id");
            RenameColumn(table: "dbo.Processes", name: "CreatorId", newName: "Creator_Id");
            RenameColumn(table: "dbo.Processes", name: "AssignedAgentId", newName: "AssignedAgent_Id");
            RenameColumn(table: "dbo.ProcessDocuments", name: "ProcessId", newName: "Process_Id");
            CreateIndex("dbo.ProcessFields", "Type_Id");
            CreateIndex("dbo.ProcessFields", "Process_Id");
            CreateIndex("dbo.Processes", "Type_Id");
            CreateIndex("dbo.ProcessDocuments", "Process_Id");
            CreateIndex("dbo.ProcessDocuments", "Document_Id");
            AddForeignKey("dbo.ProcessFields", "Process_Id", "dbo.Processes", "Id");
            AddForeignKey("dbo.Processes", "Type_Id", "dbo.ProcessTypes", "Id");
            AddForeignKey("dbo.ProcessDocuments", "Process_Id", "dbo.Processes", "Id");
            AddForeignKey("dbo.ProcessFields", "Type_Id", "dbo.ProcessTypeFields", "Id");
            AddForeignKey("dbo.ProcessDocuments", "Document_Id", "dbo.ProcessTypeDocuments", "Id");
        }
    }
}
