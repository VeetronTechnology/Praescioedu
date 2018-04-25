namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Mst_Account", name: "StudentAccountId", newName: "Account_AccountId");
            RenameIndex(table: "dbo.Mst_Account", name: "IX_StudentAccountId", newName: "IX_Account_AccountId");
            CreateTable(
                "dbo.Mst_CategoryType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mst_Account", t => t.CreatedBy)
                .ForeignKey("dbo.Mst_Account", t => t.ModifiedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy);
            
            CreateTable(
                "dbo.PasswordResetAccount",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UserAccountId = c.Int(),
                        isEmailConfirmed = c.Boolean(),
                        CreatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mst_Account", t => t.UserAccountId)
                .Index(t => t.UserAccountId);
            
            CreateTable(
                "dbo.QuestionContent",
                c => new
                    {
                        ContentId = c.Int(nullable: false, identity: true),
                        CategoryTypeId = c.Int(),
                        ContentOption = c.String(),
                        ContentAnswer = c.String(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContentId)
                .ForeignKey("dbo.Mst_CategoryType", t => t.CategoryTypeId)
                .ForeignKey("dbo.Mst_Account", t => t.CreatedBy)
                .ForeignKey("dbo.Mst_Account", t => t.ModifiedBy)
                .Index(t => t.CategoryTypeId)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy);
            
            AddColumn("dbo.Mst_Account", "ActivateOn", c => c.DateTime());
            AddColumn("dbo.Mst_Account", "ExpiredOn", c => c.DateTime());
            AddColumn("dbo.Mst_Account", "StudentStandardId", c => c.Int());
            AddColumn("dbo.Mst_Account", "MobileNo", c => c.String());
            AddColumn("dbo.Mst_InstitutionAccount", "ActivateOn", c => c.DateTime());
            AddColumn("dbo.Mst_InstitutionAccount", "ExpiredOn", c => c.DateTime());
            CreateIndex("dbo.Mst_Account", "StudentStandardId");
            AddForeignKey("dbo.Mst_Account", "StudentStandardId", "dbo.Standard", "Id");
            DropColumn("dbo.Mst_Account", "ActivateDate");
            DropColumn("dbo.Mst_Account", "ExpiredDate");
            DropColumn("dbo.Mst_Account", "Phone");
            DropColumn("dbo.Mst_InstitutionAccount", "ActivateDate");
            DropColumn("dbo.Mst_InstitutionAccount", "ExpiredDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mst_InstitutionAccount", "ExpiredDate", c => c.DateTime());
            AddColumn("dbo.Mst_InstitutionAccount", "ActivateDate", c => c.DateTime());
            AddColumn("dbo.Mst_Account", "Phone", c => c.String());
            AddColumn("dbo.Mst_Account", "ExpiredDate", c => c.DateTime());
            AddColumn("dbo.Mst_Account", "ActivateDate", c => c.DateTime());
            DropForeignKey("dbo.QuestionContent", "ModifiedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.QuestionContent", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.QuestionContent", "CategoryTypeId", "dbo.Mst_CategoryType");
            DropForeignKey("dbo.PasswordResetAccount", "UserAccountId", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_CategoryType", "ModifiedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_CategoryType", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_Account", "StudentStandardId", "dbo.Standard");
            DropIndex("dbo.QuestionContent", new[] { "ModifiedBy" });
            DropIndex("dbo.QuestionContent", new[] { "CreatedBy" });
            DropIndex("dbo.QuestionContent", new[] { "CategoryTypeId" });
            DropIndex("dbo.PasswordResetAccount", new[] { "UserAccountId" });
            DropIndex("dbo.Mst_CategoryType", new[] { "ModifiedBy" });
            DropIndex("dbo.Mst_CategoryType", new[] { "CreatedBy" });
            DropIndex("dbo.Mst_Account", new[] { "StudentStandardId" });
            DropColumn("dbo.Mst_InstitutionAccount", "ExpiredOn");
            DropColumn("dbo.Mst_InstitutionAccount", "ActivateOn");
            DropColumn("dbo.Mst_Account", "MobileNo");
            DropColumn("dbo.Mst_Account", "StudentStandardId");
            DropColumn("dbo.Mst_Account", "ExpiredOn");
            DropColumn("dbo.Mst_Account", "ActivateOn");
            DropTable("dbo.QuestionContent");
            DropTable("dbo.PasswordResetAccount");
            DropTable("dbo.Mst_CategoryType");
            RenameIndex(table: "dbo.Mst_Account", name: "IX_Account_AccountId", newName: "IX_StudentAccountId");
            RenameColumn(table: "dbo.Mst_Account", name: "Account_AccountId", newName: "StudentAccountId");
        }
    }
}
