namespace VoteWithYourWallet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class causeformrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Causes", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Causes", "ShortDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Causes", "LongDescription", c => c.String(nullable: false));
            AlterColumn("dbo.Causes", "Target", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Causes", "Target", c => c.String());
            AlterColumn("dbo.Causes", "LongDescription", c => c.String());
            AlterColumn("dbo.Causes", "ShortDescription", c => c.String());
            AlterColumn("dbo.Causes", "Title", c => c.String());
        }
    }
}
