namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Assignment", name: "Standard_Id", newName: "StandardId");
            RenameColumn(table: "dbo.Assignment", name: "Subject_Id", newName: "SubjectId");
            RenameIndex(table: "dbo.Assignment", name: "IX_Standard_Id", newName: "IX_StandardId");
            RenameIndex(table: "dbo.Assignment", name: "IX_Subject_Id", newName: "IX_SubjectId");
            AddColumn("dbo.Mst_Account", "InstitutionName", c => c.String());
            AddColumn("dbo.Mst_Account", "VersionType", c => c.String());
            AddColumn("dbo.Mst_Account", "IsIndividual", c => c.Boolean());
            AddColumn("dbo.Mst_Account", "ExpiredOn", c => c.DateTime());
            AddColumn("dbo.Assignment", "CreatedRole", c => c.Int());
            AddColumn("dbo.Assignment", "isQuestionAdded", c => c.Boolean());
            AddColumn("dbo.Assignment", "isInstitution", c => c.Boolean());
            CreateIndex("dbo.Assignment", "CreatedRole");
            AddForeignKey("dbo.Assignment", "CreatedRole", "dbo.Mst_AccountType", "AccountTypeId");
            DropColumn("dbo.Mst_Account", "InstituteName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mst_Account", "InstituteName", c => c.String());
            DropForeignKey("dbo.Assignment", "CreatedRole", "dbo.Mst_AccountType");
            DropIndex("dbo.Assignment", new[] { "CreatedRole" });
            DropColumn("dbo.Assignment", "isInstitution");
            DropColumn("dbo.Assignment", "isQuestionAdded");
            DropColumn("dbo.Assignment", "CreatedRole");
            DropColumn("dbo.Mst_Account", "ExpiredOn");
            DropColumn("dbo.Mst_Account", "IsIndividual");
            DropColumn("dbo.Mst_Account", "VersionType");
            DropColumn("dbo.Mst_Account", "InstitutionName");
            RenameIndex(table: "dbo.Assignment", name: "IX_SubjectId", newName: "IX_Subject_Id");
            RenameIndex(table: "dbo.Assignment", name: "IX_StandardId", newName: "IX_Standard_Id");
            RenameColumn(table: "dbo.Assignment", name: "SubjectId", newName: "Subject_Id");
            RenameColumn(table: "dbo.Assignment", name: "StandardId", newName: "Standard_Id");
        }
    }
}
