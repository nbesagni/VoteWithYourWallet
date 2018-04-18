namespace VoteWithYourWallet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CauseIDinSig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Signatures", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Signatures", new[] { "ApplicationUserID" });
            AddColumn("dbo.Signatures", "CauseID", c => c.String(nullable: false));
            AlterColumn("dbo.Signatures", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Signatures", "ApplicationUserID");
            AddForeignKey("dbo.Signatures", "ApplicationUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Signatures", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Signatures", new[] { "ApplicationUserID" });
            AlterColumn("dbo.Signatures", "ApplicationUserID", c => c.String(maxLength: 128));
            DropColumn("dbo.Signatures", "CauseID");
            CreateIndex("dbo.Signatures", "ApplicationUserID");
            AddForeignKey("dbo.Signatures", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
