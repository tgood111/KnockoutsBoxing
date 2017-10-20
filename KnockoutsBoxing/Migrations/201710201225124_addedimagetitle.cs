namespace KnockoutsBoxing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedimagetitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ImageFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "ImageFileName");
        }
    }
}
