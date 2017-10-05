namespace KnockoutsBoxing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Article : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        ArticleTitle = c.String(),
                        ArticleCreationDate = c.DateTime(nullable: false),
                        ArticleAuthor = c.String(),
                        ArticleContent = c.String(),
                    })
                .PrimaryKey(t => t.ArticleID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        CommentAuthor = c.String(),
                        ArticleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Articles", t => t.ArticleID, cascadeDelete: true)
                .Index(t => t.ArticleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ArticleID", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ArticleID" });
            DropTable("dbo.Comments");
            DropTable("dbo.Articles");
        }
    }
}
