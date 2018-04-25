namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mst_Account", "PackageId", "dbo.Mst_Package");
            DropIndex("dbo.Mst_Account", new[] { "PackageId" });
            AddColumn("dbo.Mst_Account", "MediumId", c => c.Int());
            AddColumn("dbo.Mst_InstitutionAccount", "AmountPaid", c => c.Int());
            AddColumn("dbo.Mst_InstitutionAccount", "NoOfStudent", c => c.Int());
            AddColumn("dbo.Mst_InstitutionAccount", "NoOfTeacher", c => c.Int());
            AddColumn("dbo.Assignment", "BoardId", c => c.Int());
            CreateIndex("dbo.Mst_Account", "BoardId");
            CreateIndex("dbo.Mst_Account", "MediumId");
            CreateIndex("dbo.Assignment", "BoardId");
            AddForeignKey("dbo.Mst_Account", "BoardId", "dbo.Mst_Board", "Id");
            AddForeignKey("dbo.Mst_Account", "MediumId", "dbo.Medium", "Id");
            AddForeignKey("dbo.Assignment", "BoardId", "dbo.Mst_Board", "Id");
            DropColumn("dbo.Mst_InstitutionAccount", "NoOfUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mst_InstitutionAccount", "NoOfUser", c => c.Int(nullable: false));
            DropForeignKey("dbo.Assignment", "BoardId", "dbo.Mst_Board");
            DropForeignKey("dbo.Mst_Account", "MediumId", "dbo.Medium");
            DropForeignKey("dbo.Mst_Account", "BoardId", "dbo.Mst_Board");
            DropIndex("dbo.Assignment", new[] { "BoardId" });
            DropIndex("dbo.Mst_Account", new[] { "MediumId" });
            DropIndex("dbo.Mst_Account", new[] { "BoardId" });
            DropColumn("dbo.Assignment", "BoardId");
            DropColumn("dbo.Mst_InstitutionAccount", "NoOfTeacher");
            DropColumn("dbo.Mst_InstitutionAccount", "NoOfStudent");
            DropColumn("dbo.Mst_InstitutionAccount", "AmountPaid");
            DropColumn("dbo.Mst_Account", "MediumId");
            CreateIndex("dbo.Mst_Account", "PackageId");
            AddForeignKey("dbo.Mst_Account", "PackageId", "dbo.Mst_Package", "PackageId");
        }
    }
}
