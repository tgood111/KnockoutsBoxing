namespace KnockoutsBoxing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelmodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ArticleCreatedBy", c => c.String());
            AddColumn("dbo.Comments", "CommentCreatedBy", c => c.String());
            AddColumn("dbo.Polls", "PollCreatedBy", c => c.String());
            AddColumn("dbo.YesOrNoes", "YesOrNoCreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.YesOrNoes", "YesOrNoCreatedBy");
            DropColumn("dbo.Polls", "PollCreatedBy");
            DropColumn("dbo.Comments", "CommentCreatedBy");
            DropColumn("dbo.Articles", "ArticleCreatedBy");
        }
    }
}
