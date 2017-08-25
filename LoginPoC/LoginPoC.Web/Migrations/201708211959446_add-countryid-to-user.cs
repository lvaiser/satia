namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcountryidtouser : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[AspNetUsers] SET Country_Id = 1 WHERE Country_Id IS NULL");

            DropForeignKey("dbo.AspNetUsers", "Country_Id", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "Country_Id" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Country_Id", newName: "CountryId");
            AlterColumn("dbo.AspNetUsers", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            AlterColumn("dbo.AspNetUsers", "CountryId", c => c.Int());
            RenameColumn(table: "dbo.AspNetUsers", name: "CountryId", newName: "Country_Id");
            CreateIndex("dbo.AspNetUsers", "Country_Id");
            AddForeignKey("dbo.AspNetUsers", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
