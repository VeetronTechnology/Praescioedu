namespace Praescio.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mst_Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AccountTypeId = c.Int(),
                        InstitutionName = c.String(),
                        VersionType = c.String(),
                        IsIndividual = c.Boolean(),
                        ActivateDate = c.DateTime(),
                        ExpiredDate = c.DateTime(),
                        StudentAccountId = c.Int(),
                        MotherName = c.String(),
                        FatherName = c.String(),
                        ParentEmail = c.String(),
                        ParentNo = c.String(),
                        URL = c.String(),
                        Address = c.String(),
                        StateId = c.String(),
                        City = c.String(),
                        PinCode = c.Int(),
                        Phone = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ParentMobileNo = c.String(),
                        InstitutionAccountId = c.Int(),
                        InstitutionAddress = c.String(),
                        FacebookID = c.String(),
                        LastLoginOn = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        IsConfirmed = c.Boolean(nullable: false),
                        ContactName = c.String(),
                        ConfirmationCode = c.String(),
                        ComfirmationCodeExpiration = c.DateTime(),
                        UpdatePwdDateTime = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Mst_Account", t => t.StudentAccountId)
                .ForeignKey("dbo.Mst_AccountType", t => t.AccountTypeId)
                .ForeignKey("dbo.Mst_InstitutionAccount", t => t.InstitutionAccountId)
                .Index(t => t.AccountTypeId)
                .Index(t => t.StudentAccountId)
                .Index(t => t.InstitutionAccountId);
            
            CreateTable(
                "dbo.Mst_AccountType",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        AccountType = c.String(),
                        Description = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.Mst_InstitutionAccount",
                c => new
                    {
                        InstitutionAccountId = c.Int(nullable: false, identity: true),
                        DomainKey = c.String(),
                        RegistrationNo = c.String(),
                        InstitutionName = c.String(),
                        CustomerName = c.String(),
                        MobileNo = c.String(),
                        LandlineNo = c.String(),
                        Email = c.String(),
                        AddressProofSrc = c.String(),
                        Address = c.String(),
                        StateId = c.Int(),
                        City = c.String(),
                        Pincode = c.String(),
                        BoardId = c.Int(),
                        MediumId = c.Int(),
                        Description = c.String(),
                        ActivateDate = c.DateTime(),
                        ExpiredDate = c.DateTime(),
                        NoOfUser = c.Int(nullable: false),
                        NativeLanguage = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionAccountId)
                .ForeignKey("dbo.Mst_Board", t => t.BoardId)
                .ForeignKey("dbo.Medium", t => t.MediumId)
                .ForeignKey("dbo.Mst_State", t => t.StateId)
                .Index(t => t.StateId)
                .Index(t => t.BoardId)
                .Index(t => t.MediumId);
            
            CreateTable(
                "dbo.Mst_Board",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoardName = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medium",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mst_State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Assignment",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        AccountId = c.Int(),
                        StandardId = c.Int(),
                        SubjectId = c.Int(),
                        MediumId = c.Int(),
                        CreatedRole = c.Int(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        isQuestionAdded = c.Boolean(),
                        isInstitution = c.Boolean(),
                        UploadFileSrc = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Mst_Account", t => t.CreatedBy)
                .ForeignKey("dbo.Mst_AccountType", t => t.CreatedRole)
                .ForeignKey("dbo.Medium", t => t.MediumId)
                .ForeignKey("dbo.Standard", t => t.StandardId)
                .ForeignKey("dbo.Subject", t => t.SubjectId)
                .Index(t => t.StandardId)
                .Index(t => t.SubjectId)
                .Index(t => t.MediumId)
                .Index(t => t.CreatedRole)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Standard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StandardName = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        VisibleTo = c.String(),
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
                "dbo.Question",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        AssignmentId = c.Int(),
                        QuestionTypeId = c.Int(),
                        NoOfBlanks = c.Int(),
                        CorrectAnswer = c.String(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Assignment", t => t.AssignmentId)
                .ForeignKey("dbo.QuestionType", t => t.QuestionTypeId)
                .Index(t => t.AssignmentId)
                .Index(t => t.QuestionTypeId);
            
            CreateTable(
                "dbo.QuestionType",
                c => new
                    {
                        QuestionTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedBy = c.Int(),
                        ModifiedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionTypeId);
            
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
            
            CreateTable(
                "dbo.TeacherStandardMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        StandardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Standard", t => t.StandardId, cascadeDelete: true)
                .Index(t => t.StandardId);
            
            CreateTable(
                "dbo.TeacherSubjectMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeacherId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subject", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherSubjectMapping", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.TeacherStandardMapping", "StandardId", "dbo.Standard");
            DropForeignKey("dbo.QuestionOption", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Question", "QuestionTypeId", "dbo.QuestionType");
            DropForeignKey("dbo.Question", "AssignmentId", "dbo.Assignment");
            DropForeignKey("dbo.Mst_PrincipalDetail", "InstitutionAccountId", "dbo.Mst_InstitutionAccount");
            DropForeignKey("dbo.KnowledgeBank", "ModifiedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.KnowledgeBank", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.AssignmentTeacherMapping", "TeacherAccountId", "dbo.Mst_Account");
            DropForeignKey("dbo.AssignmentTeacherMapping", "AssignmentId", "dbo.Assignment");
            DropForeignKey("dbo.Assignment", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.Assignment", "StandardId", "dbo.Standard");
            DropForeignKey("dbo.Assignment", "MediumId", "dbo.Medium");
            DropForeignKey("dbo.Assignment", "CreatedRole", "dbo.Mst_AccountType");
            DropForeignKey("dbo.Assignment", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_Account", "InstitutionAccountId", "dbo.Mst_InstitutionAccount");
            DropForeignKey("dbo.Mst_InstitutionAccount", "StateId", "dbo.Mst_State");
            DropForeignKey("dbo.Mst_InstitutionAccount", "MediumId", "dbo.Medium");
            DropForeignKey("dbo.Mst_InstitutionAccount", "BoardId", "dbo.Mst_Board");
            DropForeignKey("dbo.Mst_Account", "AccountTypeId", "dbo.Mst_AccountType");
            DropForeignKey("dbo.Mst_Account", "StudentAccountId", "dbo.Mst_Account");
            DropIndex("dbo.TeacherSubjectMapping", new[] { "SubjectId" });
            DropIndex("dbo.TeacherStandardMapping", new[] { "StandardId" });
            DropIndex("dbo.QuestionOption", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "QuestionTypeId" });
            DropIndex("dbo.Question", new[] { "AssignmentId" });
            DropIndex("dbo.Mst_PrincipalDetail", new[] { "InstitutionAccountId" });
            DropIndex("dbo.KnowledgeBank", new[] { "ModifiedBy" });
            DropIndex("dbo.KnowledgeBank", new[] { "CreatedBy" });
            DropIndex("dbo.AssignmentTeacherMapping", new[] { "TeacherAccountId" });
            DropIndex("dbo.AssignmentTeacherMapping", new[] { "AssignmentId" });
            DropIndex("dbo.Assignment", new[] { "CreatedBy" });
            DropIndex("dbo.Assignment", new[] { "CreatedRole" });
            DropIndex("dbo.Assignment", new[] { "MediumId" });
            DropIndex("dbo.Assignment", new[] { "SubjectId" });
            DropIndex("dbo.Assignment", new[] { "StandardId" });
            DropIndex("dbo.Mst_InstitutionAccount", new[] { "MediumId" });
            DropIndex("dbo.Mst_InstitutionAccount", new[] { "BoardId" });
            DropIndex("dbo.Mst_InstitutionAccount", new[] { "StateId" });
            DropIndex("dbo.Mst_Account", new[] { "InstitutionAccountId" });
            DropIndex("dbo.Mst_Account", new[] { "StudentAccountId" });
            DropIndex("dbo.Mst_Account", new[] { "AccountTypeId" });
            DropTable("dbo.TeacherSubjectMapping");
            DropTable("dbo.TeacherStandardMapping");
            DropTable("dbo.QuestionOption");
            DropTable("dbo.QuestionType");
            DropTable("dbo.Question");
            DropTable("dbo.Mst_PrincipalDetail");
            DropTable("dbo.KnowledgeBank");
            DropTable("dbo.AssignmentTeacherMapping");
            DropTable("dbo.Subject");
            DropTable("dbo.Standard");
            DropTable("dbo.Assignment");
            DropTable("dbo.Mst_State");
            DropTable("dbo.Medium");
            DropTable("dbo.Mst_Board");
            DropTable("dbo.Mst_InstitutionAccount");
            DropTable("dbo.Mst_AccountType");
            DropTable("dbo.Mst_Account");
        }
    }
}
