namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setcountryidnulleable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            AlterColumn("dbo.AspNetUsers", "CountryId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "CountryId" });
            AlterColumn("dbo.AspNetUsers", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CountryId");
            AddForeignKey("dbo.AspNetUsers", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
