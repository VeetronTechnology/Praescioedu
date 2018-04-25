namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mst_Account", "ActivateDate", c => c.DateTime());
            AddColumn("dbo.Mst_Account", "ExpiredDate", c => c.DateTime());
            AddColumn("dbo.Mst_Account", "StudentAccountId", c => c.Int());
            CreateIndex("dbo.Mst_Account", "StudentAccountId");
            AddForeignKey("dbo.Mst_Account", "StudentAccountId", "dbo.Mst_Account", "AccountId");
            DropColumn("dbo.Mst_Account", "ExpiredOn");
            DropColumn("dbo.Mst_Account", "IndividualInstitutionAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mst_Account", "IndividualInstitutionAddress", c => c.String());
            AddColumn("dbo.Mst_Account", "ExpiredOn", c => c.DateTime());
            DropForeignKey("dbo.Mst_Account", "StudentAccountId", "dbo.Mst_Account");
            DropIndex("dbo.Mst_Account", new[] { "StudentAccountId" });
            DropColumn("dbo.Mst_Account", "StudentAccountId");
            DropColumn("dbo.Mst_Account", "ExpiredDate");
            DropColumn("dbo.Mst_Account", "ActivateDate");
        }
    }
}
