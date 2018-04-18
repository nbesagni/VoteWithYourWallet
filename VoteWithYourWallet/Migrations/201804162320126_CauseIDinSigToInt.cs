namespace VoteWithYourWallet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CauseIDinSigToInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Signatures", "Causes_CauseID", "dbo.Causes");
            DropIndex("dbo.Signatures", new[] { "Causes_CauseID" });
            DropColumn("dbo.Signatures", "CauseID");
            RenameColumn(table: "dbo.Signatures", name: "Causes_CauseID", newName: "CauseID");
            AlterColumn("dbo.Signatures", "CauseID", c => c.Int(nullable: false));
            AlterColumn("dbo.Signatures", "CauseID", c => c.Int(nullable: false));
            CreateIndex("dbo.Signatures", "CauseID");
            AddForeignKey("dbo.Signatures", "CauseID", "dbo.Causes", "CauseID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Signatures", "CauseID", "dbo.Causes");
            DropIndex("dbo.Signatures", new[] { "CauseID" });
            AlterColumn("dbo.Signatures", "CauseID", c => c.Int());
            AlterColumn("dbo.Signatures", "CauseID", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Signatures", name: "CauseID", newName: "Causes_CauseID");
            AddColumn("dbo.Signatures", "CauseID", c => c.String(nullable: false));
            CreateIndex("dbo.Signatures", "Causes_CauseID");
            AddForeignKey("dbo.Signatures", "Causes_CauseID", "dbo.Causes", "CauseID");
        }
    }
}
