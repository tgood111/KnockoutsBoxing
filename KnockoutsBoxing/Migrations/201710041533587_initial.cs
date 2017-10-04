namespace KnockoutsBoxing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        PollID = c.Int(nullable: false, identity: true),
                        PollName = c.String(),
                        PollCreationDate = c.DateTime(nullable: false),
                        PollBoxer1 = c.String(),
                        PollBoxer2 = c.String(),
                        PollClosingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PollID);
            
            CreateTable(
                "dbo.YesOrNoes",
                c => new
                    {
                        YesOrNoID = c.Int(nullable: false, identity: true),
                        FansSaidYesOrNO = c.Boolean(nullable: false),
                        PollID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.YesOrNoID)
                .ForeignKey("dbo.Polls", t => t.PollID, cascadeDelete: true)
                .Index(t => t.PollID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YesOrNoes", "PollID", "dbo.Polls");
            DropIndex("dbo.YesOrNoes", new[] { "PollID" });
            DropTable("dbo.YesOrNoes");
            DropTable("dbo.Polls");
        }
    }
}
