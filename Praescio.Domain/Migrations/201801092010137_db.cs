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
                        Gender = c.String(),
                        AccountTypeId = c.Int(),
                        InstitutionName = c.String(),
                        VersionType = c.String(),
                        IsIndividual = c.Boolean(),
                        StudentRegisterAllowed = c.Boolean(),
                        ActivateOn = c.DateTime(),
                        ExpiredOn = c.DateTime(),
                        InstitutionAccountId = c.Int(),
                        InstitutionAddress = c.String(),
                        StudentStandardId = c.Int(),
                        PackageId = c.Int(),
                        MotherName = c.String(),
                        FatherName = c.String(),
                        ParentEmail = c.String(),
                        ParentNo = c.String(),
                        URL = c.String(),
                        Address = c.String(),
                        StateId = c.Int(),
                        City = c.String(),
                        PinCode = c.Int(),
                        MobileNo = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ParentMobileNo = c.String(),
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
                .ForeignKey("dbo.Mst_AccountType", t => t.AccountTypeId)
                .ForeignKey("dbo.Mst_InstitutionAccount", t => t.InstitutionAccountId)
                .ForeignKey("dbo.Mst_Package", t => t.PackageId)
                .ForeignKey("dbo.Standard", t => t.StudentStandardId)
                .ForeignKey("dbo.Mst_State", t => t.StateId)
                .Index(t => t.AccountTypeId)
                .Index(t => t.InstitutionAccountId)
                .Index(t => t.StudentStandardId)
                .Index(t => t.PackageId)
                .Index(t => t.StateId);
            
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
                        DomainKey = c.String(nullable: false),
                        RegistrationNo = c.String(),
                        InstitutionName = c.String(),
                        PackageId = c.Int(),
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
                        ActivateOn = c.DateTime(),
                        ExpiredOn = c.DateTime(),
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
                .ForeignKey("dbo.Mst_Package", t => t.PackageId)
                .ForeignKey("dbo.Mst_State", t => t.StateId)
                .Index(t => t.PackageId)
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
                "dbo.Mst_Package",
                c => new
                    {
                        PackageId = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                        PackageData = c.String(),
                        PackageRoleId = c.Int(),
                        InstitutionAccountId = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PackageId)
                .ForeignKey("dbo.Mst_AccountType", t => t.PackageRoleId)
                .Index(t => t.PackageRoleId);
            
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
                "dbo.Mst_ExceptionLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LoggerName = c.String(),
                        ExceptionType = c.String(),
                        ExceptionMessage = c.String(),
                        ExceptionStackTrace = c.String(),
                        IPAddress = c.String(),
                        URL = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        TotalMarks = c.Int(nullable: false),
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
                        TeacherId = c.Int(),
                        StandardId = c.Int(),
                        SubjectId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Standard", t => t.StandardId)
                .ForeignKey("dbo.Subject", t => t.SubjectId)
                .ForeignKey("dbo.Mst_Account", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.StandardId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.UserAssessment",
                c => new
                    {
                        AssessmentId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(),
                        QuestionOptionId = c.Int(),
                        UserId = c.Int(),
                        MaxScore = c.Int(),
                        MarkScored = c.Int(),
                        IsFinalScore = c.Boolean(nullable: false),
                        Answer = c.String(),
                        IsGradable = c.Boolean(nullable: false),
                        GradedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AssessmentId)
                .ForeignKey("dbo.Mst_Account", t => t.GradedBy)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.QuestionOption", t => t.QuestionOptionId)
                .ForeignKey("dbo.Mst_Account", t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.QuestionOptionId)
                .Index(t => t.UserId)
                .Index(t => t.GradedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAssessment", "UserId", "dbo.Mst_Account");
            DropForeignKey("dbo.UserAssessment", "QuestionOptionId", "dbo.QuestionOption");
            DropForeignKey("dbo.UserAssessment", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.UserAssessment", "GradedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.TeacherStandardMapping", "TeacherId", "dbo.Mst_Account");
            DropForeignKey("dbo.TeacherStandardMapping", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.TeacherStandardMapping", "StandardId", "dbo.Standard");
            DropForeignKey("dbo.QuestionOption", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.QuestionContent", "ModifiedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.QuestionContent", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.QuestionContent", "CategoryTypeId", "dbo.Mst_CategoryType");
            DropForeignKey("dbo.Question", "QuestionTypeId", "dbo.QuestionType");
            DropForeignKey("dbo.Question", "AssignmentId", "dbo.Assignment");
            DropForeignKey("dbo.Mst_PrincipalDetail", "InstitutionAccountId", "dbo.Mst_InstitutionAccount");
            DropForeignKey("dbo.PasswordResetAccount", "UserAccountId", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_PackageHistory", "PackageRoleId", "dbo.Mst_AccountType");
            DropForeignKey("dbo.KnowledgeBank", "ModifiedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.KnowledgeBank", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_CategoryType", "ModifiedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_CategoryType", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.AssignmentTeacherMapping", "TeacherAccountId", "dbo.Mst_Account");
            DropForeignKey("dbo.AssignmentTeacherMapping", "AssignmentId", "dbo.Assignment");
            DropForeignKey("dbo.Assignment", "SubjectId", "dbo.Subject");
            DropForeignKey("dbo.Assignment", "StandardId", "dbo.Standard");
            DropForeignKey("dbo.Assignment", "MediumId", "dbo.Medium");
            DropForeignKey("dbo.Assignment", "CreatedRole", "dbo.Mst_AccountType");
            DropForeignKey("dbo.Assignment", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_Activities", "CreatedBy", "dbo.Mst_Account");
            DropForeignKey("dbo.Mst_Account", "StateId", "dbo.Mst_State");
            DropForeignKey("dbo.Mst_Account", "StudentStandardId", "dbo.Standard");
            DropForeignKey("dbo.Mst_Account", "PackageId", "dbo.Mst_Package");
            DropForeignKey("dbo.Mst_Account", "InstitutionAccountId", "dbo.Mst_InstitutionAccount");
            DropForeignKey("dbo.Mst_InstitutionAccount", "StateId", "dbo.Mst_State");
            DropForeignKey("dbo.Mst_InstitutionAccount", "PackageId", "dbo.Mst_Package");
            DropForeignKey("dbo.Mst_Package", "PackageRoleId", "dbo.Mst_AccountType");
            DropForeignKey("dbo.Mst_InstitutionAccount", "MediumId", "dbo.Medium");
            DropForeignKey("dbo.Mst_InstitutionAccount", "BoardId", "dbo.Mst_Board");
            DropForeignKey("dbo.Mst_Account", "AccountTypeId", "dbo.Mst_AccountType");
            DropIndex("dbo.UserAssessment", new[] { "GradedBy" });
            DropIndex("dbo.UserAssessment", new[] { "UserId" });
            DropIndex("dbo.UserAssessment", new[] { "QuestionOptionId" });
            DropIndex("dbo.UserAssessment", new[] { "QuestionId" });
            DropIndex("dbo.TeacherStandardMapping", new[] { "SubjectId" });
            DropIndex("dbo.TeacherStandardMapping", new[] { "StandardId" });
            DropIndex("dbo.TeacherStandardMapping", new[] { "TeacherId" });
            DropIndex("dbo.QuestionOption", new[] { "QuestionId" });
            DropIndex("dbo.QuestionContent", new[] { "ModifiedBy" });
            DropIndex("dbo.QuestionContent", new[] { "CreatedBy" });
            DropIndex("dbo.QuestionContent", new[] { "CategoryTypeId" });
            DropIndex("dbo.Question", new[] { "QuestionTypeId" });
            DropIndex("dbo.Question", new[] { "AssignmentId" });
            DropIndex("dbo.Mst_PrincipalDetail", new[] { "InstitutionAccountId" });
            DropIndex("dbo.PasswordResetAccount", new[] { "UserAccountId" });
            DropIndex("dbo.Mst_PackageHistory", new[] { "PackageRoleId" });
            DropIndex("dbo.KnowledgeBank", new[] { "ModifiedBy" });
            DropIndex("dbo.KnowledgeBank", new[] { "CreatedBy" });
            DropIndex("dbo.Mst_CategoryType", new[] { "ModifiedBy" });
            DropIndex("dbo.Mst_CategoryType", new[] { "CreatedBy" });
            DropIndex("dbo.AssignmentTeacherMapping", new[] { "TeacherAccountId" });
            DropIndex("dbo.AssignmentTeacherMapping", new[] { "AssignmentId" });
            DropIndex("dbo.Assignment", new[] { "CreatedBy" });
            DropIndex("dbo.Assignment", new[] { "CreatedRole" });
            DropIndex("dbo.Assignment", new[] { "MediumId" });
            DropIndex("dbo.Assignment", new[] { "SubjectId" });
            DropIndex("dbo.Assignment", new[] { "StandardId" });
            DropIndex("dbo.Mst_Activities", new[] { "CreatedBy" });
            DropIndex("dbo.Mst_Package", new[] { "PackageRoleId" });
            DropIndex("dbo.Mst_InstitutionAccount", new[] { "MediumId" });
            DropIndex("dbo.Mst_InstitutionAccount", new[] { "BoardId" });
            DropIndex("dbo.Mst_InstitutionAccount", new[] { "StateId" });
            DropIndex("dbo.Mst_InstitutionAccount", new[] { "PackageId" });
            DropIndex("dbo.Mst_Account", new[] { "StateId" });
            DropIndex("dbo.Mst_Account", new[] { "PackageId" });
            DropIndex("dbo.Mst_Account", new[] { "StudentStandardId" });
            DropIndex("dbo.Mst_Account", new[] { "InstitutionAccountId" });
            DropIndex("dbo.Mst_Account", new[] { "AccountTypeId" });
            DropTable("dbo.UserAssessment");
            DropTable("dbo.TeacherStandardMapping");
            DropTable("dbo.QuestionOption");
            DropTable("dbo.QuestionContent");
            DropTable("dbo.QuestionType");
            DropTable("dbo.Question");
            DropTable("dbo.Mst_PrincipalDetail");
            DropTable("dbo.PasswordResetAccount");
            DropTable("dbo.Mst_PackageHistory");
            DropTable("dbo.KnowledgeBank");
            DropTable("dbo.Mst_ExceptionLog");
            DropTable("dbo.Mst_CategoryType");
            DropTable("dbo.AssignmentTeacherMapping");
            DropTable("dbo.Subject");
            DropTable("dbo.Assignment");
            DropTable("dbo.Mst_Activities");
            DropTable("dbo.Standard");
            DropTable("dbo.Mst_State");
            DropTable("dbo.Mst_Package");
            DropTable("dbo.Medium");
            DropTable("dbo.Mst_Board");
            DropTable("dbo.Mst_InstitutionAccount");
            DropTable("dbo.Mst_AccountType");
            DropTable("dbo.Mst_Account");
        }
    }
}
