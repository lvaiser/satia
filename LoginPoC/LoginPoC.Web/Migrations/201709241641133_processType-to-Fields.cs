namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class processTypetoFields : DbMigration
    {
        public override void Up()
        {
            Sql("Delete dbo.ProcessTypeFields");

            DropForeignKey("dbo.ProcessTypeFields", "ProcessType_Id", "dbo.ProcessTypes");
            DropIndex("dbo.ProcessTypeFields", new[] { "ProcessType_Id" });
            RenameColumn(table: "dbo.ProcessTypeFields", name: "ProcessType_Id", newName: "ProcessTypeId");
            AlterColumn("dbo.ProcessTypeFields", "ProcessTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProcessTypeFields", "ProcessTypeId");
            AddForeignKey("dbo.ProcessTypeFields", "ProcessTypeId", "dbo.ProcessTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessTypeFields", "ProcessTypeId", "dbo.ProcessTypes");
            DropIndex("dbo.ProcessTypeFields", new[] { "ProcessTypeId" });
            AlterColumn("dbo.ProcessTypeFields", "ProcessTypeId", c => c.Int());
            RenameColumn(table: "dbo.ProcessTypeFields", name: "ProcessTypeId", newName: "ProcessType_Id");
            CreateIndex("dbo.ProcessTypeFields", "ProcessType_Id");
            AddForeignKey("dbo.ProcessTypeFields", "ProcessType_Id", "dbo.ProcessTypes", "Id");
        }
    }
}
