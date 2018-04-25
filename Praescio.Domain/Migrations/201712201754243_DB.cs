namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Mst_OrganizationAccount", newName: "Mst_InstitutionAccount");
            DropForeignKey("dbo.Mst_School", "BoardId", "dbo.Mst_Board");
            DropForeignKey("dbo.Mst_School", "StateId", "dbo.Mst_State");
            DropIndex("dbo.Mst_School", new[] { "StateId" });
            DropIndex("dbo.Mst_School", new[] { "BoardId" });
            DropColumn("dbo.Assignment", "CreatedBy");
            RenameColumn(table: "dbo.Assignment", name: "AccountId", newName: "CreatedBy");
            RenameColumn(table: "dbo.Assignment", name: "Standard_Id", newName: "StandardId");
            RenameColumn(table: "dbo.Assignment", name: "Subject_Id", newName: "SubjectId");
            RenameIndex(table: "dbo.Assignment", name: "IX_Standard_Id", newName: "IX_StandardId");
            RenameIndex(table: "dbo.Assignment", name: "IX_Subject_Id", newName: "IX_SubjectId");
            RenameIndex(table: "dbo.Assignment", name: "IX_AccountId", newName: "IX_CreatedBy");
            CreateTable(
                "dbo.AssignmentTeacherMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignmentId = c.Int(),
                        TeacherAccountId = c.Int(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignment", t => t.AssignmentId)
                .ForeignKey("dbo.Mst_Account", t => t.TeacherAccountId)
                .Index(t => t.AssignmentId)
                .Index(t => t.TeacherAccountId);
            
            CreateTable(
                "dbo.KnowledgeBank",
                c => new
                    {
                        KnowledgeBankId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        VisibleToRole = c.String(),
                        PDFFileSrc = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KnowledgeBankId)
                .ForeignKey("dbo.Mst_Account", t => t.CreatedBy)
                .ForeignKey("dbo.Mst_Account", t => t.ModifiedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.ModifiedBy);
            
            CreateTable(
                "dbo.Mst_PrincipalDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstitutionAccountId = c.Int(),
                        Name = c.String(),
                        Designation = c.String(),
                        Email = c.String(),
                        Gender = c.String(),
                        PersonPhotoSrc = c.String(),
                        AuthorityLetterSrc = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mst_InstitutionAccount", t => t.InstitutionAccountId)
                .Index(t => t.InstitutionAccountId);
            
            CreateTable(
                "dbo.QuestionOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(),
                        Category = c.String(),
                        Option = c.String(),
                        MatchQuestion = c.String(),
                        MatchAnswer = c.String(),
                        Description = c.String(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            AddColumn("dbo.Mst_Account", "InstitutionName", c => c.String());
            AddColumn("dbo.Mst_Account", "VersionType", c => c.String());
            AddColumn("dbo.Mst_Account", "IsIndividual", c => c.Boolean());
            AddColumn("dbo.Mst_Account", "ActivateDate", c => c.DateTime());
            AddColumn("dbo.Mst_Account", "ExpiredDate", c => c.DateTime());
            AddColumn("dbo.Mst_Account", "StudentAccountId", c => c.Int());
            AddColumn("dbo.Mst_Account", "MotherName", c => c.String());
            AddColumn("dbo.Mst_Account", "FatherName", c => c.String());
            AddColumn("dbo.Mst_Account", "ParentEmail", c => c.String());
            AddColumn("dbo.Mst_Account", "ParentNo", c => c.String());
            AddColumn("dbo.Mst_Account", "ParentMobileNo", c => c.String());
            AddColumn("dbo.Mst_Account", "InstitutionAddress", c => c.String());
            AddColumn("dbo.Mst_InstitutionAccount", "AddressProofSrc", c => c.String());
            AddColumn("dbo.Assignment", "MediumId", c => c.Int());
            AddColumn("dbo.Assignment", "CreatedRole", c => c.Int());
            AddColumn("dbo.Assignment", "isQuestionAdded", c => c.Boolean());
            AddColumn("dbo.Assignment", "isInstitution", c => c.Boolean());
            AddColumn("dbo.Question", "Title", c => c.String());
            AlterColumn("dbo.Question", "CorrectAnswer", c => c.String());
            CreateIndex("dbo.Mst_Account", "StudentAccountId");
            CreateIndex("dbo.Assignment", "MediumId");
            CreateIndex("dbo.Assignment", "CreatedRole");
            AddForeignKey("dbo.Mst_Account", "StudentAccountId", "dbo.Mst_Account", "AccountId");
            AddForeignKey("dbo.Assignment", "CreatedRole", "dbo.Mst_AccountType", "AccountTypeId");
            AddForeignKey("dbo.Assignment", "MediumId", "dbo.Medium", "Id");
            DropColumn("dbo.Mst_Account", "InstituteName");
            DropColumn("dbo.Mst_InstitutionAccount", "AddressProof");
            DropColumn("dbo.Question", "Name");
            DropTable("dbo.Mst_School");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mst_School",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        RegistrationNo = c.String(),
                        Address = c.String(),
                        MobileNo = c.String(),
                        LandlineNo = c.String(),
                        StateId = c.Int(),
                        City = c.String(),
                        Pincode = c.String(),
                        Password = c.String(),
                        AddressProof = c.String(),
                        AddressImage = c.String(),
                        Email = c.String(),
                        Medium = c.String(),
                        BoardId = c.Int(),
                        Validtill = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Question", "Name", c => c.String());
            AddColumn("dbo.Mst_InstitutionAccount", "AddressProof", c => c.String());
            AddColumn("dbo.Mst_Account", "InstituteName", c => c.String());
            DropForeignKey("dbo.QuestionOption", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Mst_PrincipalDetail", "InstitutionAccountId", "dbo.Mst_InstitutionAccount");
            DropForeignKey("dbo.KnowledgeBank", "ModifiedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.KnowledgeBank", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.AssignmentTeacherMapping", "TeacherAccountId", "dbo.Mst_Account");
            DropForeignKey("dbo.AssignmentTeacherMapping", "AssignmentId", "dbo.Assignment");
            DropForeignKey("dbo.Assignment", "MediumId", "dbo.Medium");
            DropForeignKey("dbo.Assignment", "CreatedRole", "dbo.Mst_AccountType");
            DropForeignKey("dbo.Mst_Account", "StudentAccountId", "dbo.Mst_Account");
            DropIndex("dbo.QuestionOption", new[] { "QuestionId" });
            DropIndex("dbo.Mst_PrincipalDetail", new[] { "InstitutionAccountId" });
            DropIndex("dbo.KnowledgeBank", new[] { "ModifiedBy" });
            DropIndex("dbo.KnowledgeBank", new[] { "CreatedBy" });
            DropIndex("dbo.AssignmentTeacherMapping", new[] { "TeacherAccountId" });
            DropIndex("dbo.AssignmentTeacherMapping", new[] { "AssignmentId" });
            DropIndex("dbo.Assignment", new[] { "CreatedRole" });
            DropIndex("dbo.Assignment", new[] { "MediumId" });
            DropIndex("dbo.Mst_Account", new[] { "StudentAccountId" });
            AlterColumn("dbo.Question", "CorrectAnswer", c => c.Int());
            DropColumn("dbo.Question", "Title");
            DropColumn("dbo.Assignment", "isInstitution");
            DropColumn("dbo.Assignment", "isQuestionAdded");
            DropColumn("dbo.Assignment", "CreatedRole");
            DropColumn("dbo.Assignment", "MediumId");
            DropColumn("dbo.Mst_InstitutionAccount", "AddressProofSrc");
            DropColumn("dbo.Mst_Account", "InstitutionAddress");
            DropColumn("dbo.Mst_Account", "ParentMobileNo");
            DropColumn("dbo.Mst_Account", "ParentNo");
            DropColumn("dbo.Mst_Account", "ParentEmail");
            DropColumn("dbo.Mst_Account", "FatherName");
            DropColumn("dbo.Mst_Account", "MotherName");
            DropColumn("dbo.Mst_Account", "StudentAccountId");
            DropColumn("dbo.Mst_Account", "ExpiredDate");
            DropColumn("dbo.Mst_Account", "ActivateDate");
            DropColumn("dbo.Mst_Account", "IsIndividual");
            DropColumn("dbo.Mst_Account", "VersionType");
            DropColumn("dbo.Mst_Account", "InstitutionName");
            DropTable("dbo.QuestionOption");
            DropTable("dbo.Mst_PrincipalDetail");
            DropTable("dbo.KnowledgeBank");
            DropTable("dbo.AssignmentTeacherMapping");
            RenameIndex(table: "dbo.Assignment", name: "IX_CreatedBy", newName: "IX_AccountId");
            RenameIndex(table: "dbo.Assignment", name: "IX_SubjectId", newName: "IX_Subject_Id");
            RenameIndex(table: "dbo.Assignment", name: "IX_StandardId", newName: "IX_Standard_Id");
            RenameColumn(table: "dbo.Assignment", name: "SubjectId", newName: "Subject_Id");
            RenameColumn(table: "dbo.Assignment", name: "StandardId", newName: "Standard_Id");
            RenameColumn(table: "dbo.Assignment", name: "CreatedBy", newName: "AccountId");
            AddColumn("dbo.Assignment", "CreatedBy", c => c.Int());
            CreateIndex("dbo.Mst_School", "BoardId");
            CreateIndex("dbo.Mst_School", "StateId");
            AddForeignKey("dbo.Mst_School", "StateId", "dbo.Mst_State", "Id");
            AddForeignKey("dbo.Mst_School", "BoardId", "dbo.Mst_Board", "Id");
            RenameTable(name: "dbo.Mst_InstitutionAccount", newName: "Mst_OrganizationAccount");
        }
    }
}
