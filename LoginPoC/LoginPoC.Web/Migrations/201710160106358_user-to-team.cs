namespace LoginPoC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertoteam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsersToTeams",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TeamId, t.UserId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersToTeams", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersToTeams", "TeamId", "dbo.Teams");
            DropIndex("dbo.UsersToTeams", new[] { "UserId" });
            DropIndex("dbo.UsersToTeams", new[] { "TeamId" });
            DropTable("dbo.UsersToTeams");
            DropTable("dbo.Teams");
        }
    }
}
