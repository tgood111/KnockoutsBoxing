namespace KnockoutsBoxing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedcommentmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "FlagComment", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "FlagComment");
        }
    }
}
