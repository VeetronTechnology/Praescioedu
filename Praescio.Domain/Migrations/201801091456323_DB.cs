namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mst_Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityName = c.String(),
                        Query = c.String(),
                        ContactName = c.String(),
                        URL = c.String(),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.Int(),
                        Phone = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mst_Account", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Mst_StudentParentAccount",
                c => new
                    {
                        ParentId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        AccountTypeId = c.Int(),
                        MobileNo = c.String(),
                        Email = c.String(),
                        ParentMobileNo = c.String(),
                        LastLoginOn = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ParentId)
                .ForeignKey("dbo.Mst_AccountType", t => t.AccountTypeId)
                .ForeignKey("dbo.Mst_Account", t => t.AccountId)
                .Index(t => t.AccountId)
                .Index(t => t.AccountTypeId);
            
            AddColumn("dbo.Mst_Account", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mst_StudentParentAccount", "AccountId", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_StudentParentAccount", "AccountTypeId", "dbo.Mst_AccountType");
            DropForeignKey("dbo.Mst_Activities", "CreatedBy", "dbo.Mst_Account");
            DropIndex("dbo.Mst_StudentParentAccount", new[] { "AccountTypeId" });
            DropIndex("dbo.Mst_StudentParentAccount", new[] { "AccountId" });
            DropIndex("dbo.Mst_Activities", new[] { "CreatedBy" });
            DropColumn("dbo.Mst_Account", "DateOfBirth");
            DropTable("dbo.Mst_StudentParentAccount");
            DropTable("dbo.Mst_Activities");
        }
    }
}
