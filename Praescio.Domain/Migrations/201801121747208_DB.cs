namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mst_Package", "PackageRoleId", "dbo.Mst_AccountType");
            DropForeignKey("dbo.Mst_PackageHistory", "PackageRoleId", "dbo.Mst_AccountType");
            DropIndex("dbo.Mst_Package", new[] { "PackageRoleId" });
            DropIndex("dbo.Mst_PackageHistory", new[] { "PackageRoleId" });
            CreateTable(
                "dbo.UserAssessmentDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignmentId = c.Int(nullable: false),
                        TotalQuestion = c.Int(),
                        UserId = c.Int(),
                        TotalScore = c.Int(),
                        TotalMarksScored = c.Int(),
                        IsCompleted = c.Boolean(nullable: false),
                        ExamStartDate = c.DateTime(),
                        ExamEndDate = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignment", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.Mst_Account", t => t.UserId)
                .Index(t => t.AssignmentId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Mst_Account", "PackageId", c => c.Int());
            AddColumn("dbo.Mst_Account", "AmountPaid", c => c.Int(nullable: false));
            AddColumn("dbo.Mst_InstitutionAccount", "PackageId", c => c.Int());
            DropTable("dbo.Mst_Package");
            DropTable("dbo.Mst_PackageHistory");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mst_PackageHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                        PackageData = c.String(),
                        PackageRoleId = c.Int(),
                        InstitutionAccountId = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mst_Package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                        PackageData = c.String(),
                        PackageRoleId = c.Int(),
                        InstitutionAccountId = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.UserAssessmentDetail", "UserId", "dbo.Mst_Account");
            DropForeignKey("dbo.UserAssessmentDetail", "AssignmentId", "dbo.Assignment");
            DropIndex("dbo.UserAssessmentDetail", new[] { "UserId" });
            DropIndex("dbo.UserAssessmentDetail", new[] { "AssignmentId" });
            DropColumn("dbo.Mst_InstitutionAccount", "PackageId");
            DropColumn("dbo.Mst_Account", "AmountPaid");
            DropColumn("dbo.Mst_Account", "PackageId");
            DropTable("dbo.UserAssessmentDetail");
            CreateIndex("dbo.Mst_PackageHistory", "PackageRoleId");
            CreateIndex("dbo.Mst_Package", "PackageRoleId");
            AddForeignKey("dbo.Mst_PackageHistory", "PackageRoleId", "dbo.Mst_AccountType", "AccountTypeId");
            AddForeignKey("dbo.Mst_Package", "PackageRoleId", "dbo.Mst_AccountType", "AccountTypeId");
        }
    }
}
