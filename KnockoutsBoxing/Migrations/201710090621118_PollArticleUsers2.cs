namespace KnockoutsBoxing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PollArticleUsers2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PollArticleUsers",
                c => new
                    {
                        PollArticleUsersID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PollArticleUsersID);
            
            AddColumn("dbo.Articles", "PollArticleUsers_PollArticleUsersID", c => c.Int());
            AddColumn("dbo.Comments", "PollArticleUsers_PollArticleUsersID", c => c.Int());
            AddColumn("dbo.Polls", "PollArticleUsers_PollArticleUsersID", c => c.Int());
            CreateIndex("dbo.Articles", "PollArticleUsers_PollArticleUsersID");
            CreateIndex("dbo.Comments", "PollArticleUsers_PollArticleUsersID");
            CreateIndex("dbo.Polls", "PollArticleUsers_PollArticleUsersID");
            AddForeignKey("dbo.Articles", "PollArticleUsers_PollArticleUsersID", "dbo.PollArticleUsers", "PollArticleUsersID");
            AddForeignKey("dbo.Comments", "PollArticleUsers_PollArticleUsersID", "dbo.PollArticleUsers", "PollArticleUsersID");
            AddForeignKey("dbo.Polls", "PollArticleUsers_PollArticleUsersID", "dbo.PollArticleUsers", "PollArticleUsersID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Polls", "PollArticleUsers_PollArticleUsersID", "dbo.PollArticleUsers");
            DropForeignKey("dbo.Comments", "PollArticleUsers_PollArticleUsersID", "dbo.PollArticleUsers");
            DropForeignKey("dbo.Articles", "PollArticleUsers_PollArticleUsersID", "dbo.PollArticleUsers");
            DropIndex("dbo.Polls", new[] { "PollArticleUsers_PollArticleUsersID" });
            DropIndex("dbo.Comments", new[] { "PollArticleUsers_PollArticleUsersID" });
            DropIndex("dbo.Articles", new[] { "PollArticleUsers_PollArticleUsersID" });
            DropColumn("dbo.Polls", "PollArticleUsers_PollArticleUsersID");
            DropColumn("dbo.Comments", "PollArticleUsers_PollArticleUsersID");
            DropColumn("dbo.Articles", "PollArticleUsers_PollArticleUsersID");
            DropTable("dbo.PollArticleUsers");
        }
    }
}
