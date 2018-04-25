namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mst_AccountType", t => t.PackageRoleId)
                .Index(t => t.PackageRoleId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mst_AccountType", t => t.PackageRoleId)
                .Index(t => t.PackageRoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mst_PackageHistory", "PackageRoleId", "dbo.Mst_AccountType");
            DropForeignKey("dbo.Mst_Package", "PackageRoleId", "dbo.Mst_AccountType");
            DropIndex("dbo.Mst_PackageHistory", new[] { "PackageRoleId" });
            DropIndex("dbo.Mst_Package", new[] { "PackageRoleId" });
            DropTable("dbo.Mst_PackageHistory");
            DropTable("dbo.Mst_Package");
        }
    }
}
