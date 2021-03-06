USE [PraescioCreate]
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[AssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[AssignmentTypeId] [int] NULL,
	[AccountId] [int] NULL,
	[BoardId] [int] NULL,
	[MediumId] [int] NULL,
	[StandardId] [int] NULL,
	[SubjectId] [int] NULL,
	[CreatedRole] [int] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[isQuestionAdded] [bit] NULL,
	[isInstitution] [bit] NULL,
	[UploadFileSrc] [nvarchar](max) NULL,
	[IsPublished] [bit] NOT NULL,
	[PublishedDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Assignment] PRIMARY KEY CLUSTERED 
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AssignmentTeacherMapping]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignmentTeacherMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentId] [int] NULL,
	[TeacherAccountId] [int] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.AssignmentTeacherMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KnowledgeBank]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KnowledgeBank](
	[KnowledgeBankId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[VisibleToRole] [nvarchar](max) NULL,
	[PDFFileSrc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.KnowledgeBank] PRIMARY KEY CLUSTERED 
(
	[KnowledgeBankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medium]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medium](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Medium] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_Account]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[DateOfBirth] [datetime] NULL,
	[AccountTypeId] [int] NULL,
	[InstitutionAccountId] [int] NULL,
	[InstitutionName] [nvarchar](max) NULL,
	[VersionType] [nvarchar](max) NULL,
	[IsIndividual] [bit] NULL,
	[StudentRegisterAllowed] [bit] NULL,
	[ActivateOn] [datetime] NULL,
	[ExpiredOn] [datetime] NULL,
	[BoardId] [int] NULL,
	[MediumId] [int] NULL,
	[InstitutionAddress] [nvarchar](max) NULL,
	[StudentStandardId] [int] NULL,
	[PackageId] [int] NULL,
	[AmountPaid] [int] NOT NULL,
	[MotherName] [nvarchar](max) NULL,
	[FatherName] [nvarchar](max) NULL,
	[ParentEmail] [nvarchar](max) NULL,
	[ParentNo] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[StateId] [int] NULL,
	[City] [nvarchar](max) NULL,
	[PinCode] [int] NULL,
	[MobileNo] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[ParentMobileNo] [nvarchar](max) NULL,
	[FacebookID] [nvarchar](max) NULL,
	[LastLoginOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[IsConfirmed] [bit] NOT NULL,
	[ContactName] [nvarchar](max) NULL,
	[ConfirmationCode] [nvarchar](max) NULL,
	[ComfirmationCodeExpiration] [datetime] NULL,
	[UpdatePwdDateTime] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_AccountType]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_AccountType](
	[AccountTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AccountType] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_AccountType] PRIMARY KEY CLUSTERED 
(
	[AccountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_Activities]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityName] [nvarchar](max) NULL,
	[Query] [nvarchar](max) NULL,
	[ContactName] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_AssignmentType]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_AssignmentType](
	[AssignmentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentTypeName] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_AssignmentType] PRIMARY KEY CLUSTERED 
(
	[AssignmentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_Board]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_Board](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BoardName] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_Board] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_CategoryType]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_CategoryType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_CategoryType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_ExceptionLog]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_ExceptionLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LoggerName] [nvarchar](max) NULL,
	[ExceptionType] [nvarchar](max) NULL,
	[ExceptionMessage] [nvarchar](max) NULL,
	[ExceptionStackTrace] [nvarchar](max) NULL,
	[IPAddress] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[ControllerName] [nvarchar](max) NULL,
	[ActionName] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Mst_ExceptionLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_InstitutionAccount]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_InstitutionAccount](
	[InstitutionAccountId] [int] IDENTITY(1,1) NOT NULL,
	[DomainKey] [nvarchar](max) NOT NULL,
	[RegistrationNo] [nvarchar](max) NULL,
	[InstitutionName] [nvarchar](max) NULL,
	[PackageId] [int] NULL,
	[CustomerName] [nvarchar](max) NULL,
	[MobileNo] [nvarchar](max) NULL,
	[LandlineNo] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[AddressProofSrc] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[StateId] [int] NULL,
	[City] [nvarchar](max) NULL,
	[Pincode] [nvarchar](max) NULL,
	[BoardId] [int] NULL,
	[MediumId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[ActivateOn] [datetime] NULL,
	[ExpiredOn] [datetime] NULL,
	[AmountPaid] [int] NULL,
	[NoOfStudent] [int] NULL,
	[NoOfTeacher] [int] NULL,
	[NativeLanguage] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_InstitutionAccount] PRIMARY KEY CLUSTERED 
(
	[InstitutionAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_Package]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_Package](
	[PackageId] [int] IDENTITY(1,1) NOT NULL,
	[PackageName] [nvarchar](max) NULL,
	[PackageAmount] [int] NULL,
	[PackageData] [nvarchar](max) NULL,
	[PackageRoleId] [int] NULL,
	[InstitutionAccountId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_Package] PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_PackageHistory]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_PackageHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PackageName] [nvarchar](max) NULL,
	[PackageData] [nvarchar](max) NULL,
	[PackageRoleId] [int] NULL,
	[InstitutionAccountId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_PackageHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_PrincipalDetail]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_PrincipalDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InstitutionAccountId] [int] NULL,
	[Name] [nvarchar](max) NULL,
	[Designation] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[PersonPhotoSrc] [nvarchar](max) NULL,
	[AuthorityLetterSrc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_PrincipalDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_State]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_StudentParentAccount]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_StudentParentAccount](
	[ParentId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[DateOfBirth] [datetime] NULL,
	[AccountTypeId] [int] NULL,
	[MobileNo] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[ParentMobileNo] [nvarchar](max) NULL,
	[LastLoginOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Mst_StudentParentAccount] PRIMARY KEY CLUSTERED 
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PasswordResetAccount]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordResetAccount](
	[Id] [uniqueidentifier] NOT NULL,
	[UserAccountId] [int] NULL,
	[isEmailConfirmed] [bit] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.PasswordResetAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Question]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[AssignmentId] [int] NULL,
	[QuestionTypeId] [int] NULL,
	[NoOfBlanks] [int] NULL,
	[MCQQuestionImageSrc] [nvarchar](max) NULL,
	[MCQText1] [nvarchar](max) NULL,
	[MCQText2] [nvarchar](max) NULL,
	[MCQText3] [nvarchar](max) NULL,
	[MCQText4] [nvarchar](max) NULL,
	[MCQImage1Src] [nvarchar](max) NULL,
	[MCQImage2Src] [nvarchar](max) NULL,
	[MCQImage3Src] [nvarchar](max) NULL,
	[MCQImage4Src] [nvarchar](max) NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
	[TotalMarks] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Question] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuestionContent]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionContent](
	[ContentId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryTypeId] [int] NULL,
	[ContentOption] [nvarchar](max) NULL,
	[ContentAnswer] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.QuestionContent] PRIMARY KEY CLUSTERED 
(
	[ContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuestionOption]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NULL,
	[Category] [nvarchar](max) NULL,
	[Option] [nvarchar](max) NULL,
	[MatchQuestion] [nvarchar](max) NULL,
	[MatchAnswer] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.QuestionOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuestionType]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionType](
	[QuestionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[TotalMarks] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.QuestionType] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Standard]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Standard](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StandardName] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Standard] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subject]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](max) NULL,
	[StandardId] [int] NULL,
	[MediumId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TeacherStandardMapping]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherStandardMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NULL,
	[StandardId] [int] NULL,
	[SubjectId] [int] NULL,
 CONSTRAINT [PK_dbo.TeacherStandardMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAssessment]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAssessment](
	[AssessmentId] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NULL,
	[QuestionOptionId] [int] NULL,
	[UserId] [int] NULL,
	[MaxScore] [int] NULL,
	[MarkScored] [int] NULL,
	[IsFinalScore] [bit] NOT NULL,
	[Answer] [nvarchar](max) NULL,
	[IsGradable] [bit] NOT NULL,
	[GradedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserAssessment] PRIMARY KEY CLUSTERED 
(
	[AssessmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAssessmentDetail]    Script Date: 1/20/2018 3:28:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAssessmentDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentId] [int] NOT NULL,
	[TotalQuestion] [int] NULL,
	[UserId] [int] NULL,
	[TotalScore] [int] NULL,
	[TotalMarksScored] [int] NULL,
	[IsCompleted] [bit] NOT NULL,
	[ExamStartDate] [datetime] NULL,
	[ExamEndDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserAssessmentDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Medium] ON 

GO
INSERT [dbo].[Medium] ([Id], [Name], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'English', CAST(0x0000A86E00FE959A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Medium] ([Id], [Name], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Marathi', CAST(0x0000A86E00FE959A AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Medium] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_Account] ON 

GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [Password], [FirstName], [LastName], [Gender], [DateOfBirth], [AccountTypeId], [InstitutionAccountId], [InstitutionName], [VersionType], [IsIndividual], [StudentRegisterAllowed], [ActivateOn], [ExpiredOn], [BoardId], [MediumId], [InstitutionAddress], [StudentStandardId], [PackageId], [AmountPaid], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [ParentMobileNo], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'100001', N'123456', N'Praescio', N'Admin', NULL, NULL, 1, 1, N'SuperAdmin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Mumbai', NULL, N'7208305705', N'ali.tech1108@gmail.com', NULL, NULL, NULL, NULL, NULL, 1, N'Ali', NULL, NULL, NULL, CAST(0x0000A86E00FE94E6 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_AccountType] ON 

GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'SuperAdmin', NULL, 0, 0, CAST(0x0000A86E00FE94C9 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Admin', NULL, 0, 0, CAST(0x0000A86E00FE94C9 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'Teacher', NULL, 0, 0, CAST(0x0000A86E00FE94C9 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'Student', NULL, 0, 0, CAST(0x0000A86E00FE94C9 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'IndividualTeacher', NULL, 0, 0, CAST(0x0000A86E00FE94C9 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'IndividualStudent', NULL, 0, 0, CAST(0x0000A86E00FE94C9 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (7, N'InstitutionTeacherParent', NULL, 0, 0, CAST(0x0000A86E00FE94C9 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_AccountType] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_AssignmentType] ON 

GO
INSERT [dbo].[Mst_AssignmentType] ([AssignmentTypeId], [AssignmentTypeName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'Praescio Lesson', CAST(0x0000A86E00FE94B3 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AssignmentType] ([AssignmentTypeId], [AssignmentTypeName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Institution Assignment', CAST(0x0000A86E00FE94B3 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AssignmentType] ([AssignmentTypeId], [AssignmentTypeName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'Individual Assignment', CAST(0x0000A86E00FE94B3 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AssignmentType] ([AssignmentTypeId], [AssignmentTypeName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'Handwriting', CAST(0x0000A86E00FE94B3 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AssignmentType] ([AssignmentTypeId], [AssignmentTypeName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'Pronounciation', CAST(0x0000A86E00FE94B3 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AssignmentType] ([AssignmentTypeId], [AssignmentTypeName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'PExtra', CAST(0x0000A86E00FE94B3 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_AssignmentType] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_Board] ON 

GO
INSERT [dbo].[Mst_Board] ([Id], [BoardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'MH Board', CAST(0x0000A86E00FE94BC AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Board] ([Id], [BoardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'CBSE Board', CAST(0x0000A86E00FE94BC AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Board] ([Id], [BoardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'ICSE Board', CAST(0x0000A86E00FE94BC AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_Board] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_CategoryType] ON 

GO
INSERT [dbo].[Mst_CategoryType] ([Id], [CategoryName], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (1, N'Synonyms', CAST(0x0000A86E00FE94A4 AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Mst_CategoryType] ([Id], [CategoryName], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (2, N'Antonyms', CAST(0x0000A86E00FE94A4 AS DateTime), NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_CategoryType] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_InstitutionAccount] ON 

GO
INSERT [dbo].[Mst_InstitutionAccount] ([InstitutionAccountId], [DomainKey], [RegistrationNo], [InstitutionName], [PackageId], [CustomerName], [MobileNo], [LandlineNo], [Email], [AddressProofSrc], [Address], [StateId], [City], [Pincode], [BoardId], [MediumId], [Description], [ActivateOn], [ExpiredOn], [AmountPaid], [NoOfStudent], [NoOfTeacher], [NativeLanguage], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'ins', NULL, N'Praescio Organization', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'initial setup organization', NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, CAST(0x0000A86E00FE94D6 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_InstitutionAccount] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_Package] ON 

GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'Package No 1', 10000, N'{  ''NoOfStudent'': 5000,  ''NoOfTeacher'': 5000,  ''PackageType'': ''Institution Package''}', 2, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Package No 2', 10000, N'{  ''NoOfStudent'': 2500,  ''NoOfTeacher'': 2500,  ''PackageType'': ''Institution Package''}', 2, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'Package No 3', 10000, N'{  ''NoOfStudent'': 1000,  ''NoOfTeacher'': 1000,  ''PackageType'': ''Institution Package''}', 2, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'Package No 1', 1000, N'{  ''NoOfStudent'': 1000,  ''NoOfTeacher'': 1000,  ''PackageType'': ''Individual Student Package''}', 5, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'Package No 2', 1500, N'{  ''NoOfStudent'': 1000,  ''NoOfTeacher'': 1000,  ''PackageType'': ''Individual Student Package''}', 5, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'Package No 3', 2000, N'{  ''NoOfStudent'': 1000,  ''NoOfTeacher'': 1000,  ''PackageType'': ''Individual Student Package''}', 5, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (7, N'Package No 1', 1000, N'{  ''NoOfStudent'': 1000,  ''NoOfTeacher'': 1000,  ''PackageType'': ''Individual Teacher Package''}', 6, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (8, N'Package No 2', 1500, N'{  ''NoOfStudent'': 1000,  ''NoOfTeacher'': 1000,  ''PackageType'': ''Individual Teacher Package''}', 6, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Package] ([PackageId], [PackageName], [PackageAmount], [PackageData], [PackageRoleId], [InstitutionAccountId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (9, N'Package No 3', 2000, N'{  ''NoOfStudent'': 1000,  ''NoOfTeacher'': 1000,  ''PackageType'': ''Individual Teacher Package''}', 6, NULL, CAST(0x0000A86E00FE9474 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_Package] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_State] ON 

GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'Maharashtra', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Delhi', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'Rajasthan', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'Punjab', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'Uttar Pradesh', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'Hyderabad', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (7, N'Kerala', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (8, N'West Bengal', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (9, N'Goa', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (10, N'Haryana', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (11, N'Gujarat', CAST(0x0000A86E00FE95A7 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_State] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionType] ON 

GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'MeaningOfLesson', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'SynonymsOfLesson', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'AntonymsOfLesson', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'WriteReason', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'FillInTheBlanks', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'MatchTheFollowing', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (7, N'MCQ', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (8, N'TrueFalse', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (9, N'OneSentenceAnswer', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (10, N'DescribeBriefly', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (11, N'DifferentiateBetween', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (12, N'Exercise', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (13, N'WriteShortNote', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [TotalMarks], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (14, N'TopicConcept', 1, NULL, NULL, NULL, CAST(0x0000A86E00FE9515 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[QuestionType] OFF
GO
SET IDENTITY_INSERT [dbo].[Standard] ON 

GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'5th', CAST(0x0000A86E00FE9506 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'6th', CAST(0x0000A86E00FE9506 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'7th', CAST(0x0000A86E00FE9506 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'8th', CAST(0x0000A86E00FE9506 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'9th', CAST(0x0000A86E00FE9506 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'10th', CAST(0x0000A86E00FE9506 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Standard] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'English', 1, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'EVS Part-1', 1, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'EVS Part-2', 1, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'Marathi', 1, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'Math', 1, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'English', 1, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (7, N'Marathi', 1, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (8, N'Math ', 1, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (9, N'Science', 1, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (10, N'Social Science Part-1', 1, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (11, N'English', 2, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (12, N'General Science', 2, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (13, N'Geography', 2, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (14, N'History (Social Science)', 1, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (15, N'Marathi', 2, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (16, N'Math', 2, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (17, N'Social (Science History)', 2, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (18, N'English ', 2, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (19, N'History (Social Science)', 2, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (20, N'Marathi', 2, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (21, N'Math', 2, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (22, N'Science', 2, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (23, N'English', 3, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (24, N'Geography', 3, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (25, N'History (Social Science)', 3, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (26, N'Marathi', 3, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (27, N'Math', 3, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (28, N'Science', 3, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (29, N'English ', 3, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (30, N'Geography', 3, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (31, N'History (Social Science)', 3, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (32, N'Marathi', 3, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (33, N'Math', 3, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (34, N'Science', 3, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (35, N'English', 4, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (36, N'Geography', 4, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (37, N'History (Social Science)', 4, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (38, N'Marathi', 4, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (39, N'Math', 4, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (40, N'Science', 4, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (41, N'English ', 4, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (42, N'Geography', 4, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (43, N'History (Social Science)', 4, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (44, N'Marathi', 4, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (45, N'Math', 4, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (46, N'Science', 4, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (47, N'English', 5, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (48, N'Geography', 5, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (49, N'History (Social Science)', 5, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (50, N'Marathi', 5, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (51, N'Math Part-1', 5, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (52, N'Math Part-2', 5, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (53, N'Science ', 5, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (54, N'English ', 5, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (55, N'Geography', 5, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (56, N'History (Social Science)', 5, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (57, N'Marathi', 5, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (58, N'Math Part-1', 5, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (59, N'Math Part-2', 5, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (60, N'Science', 5, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (61, N'Algebra (Math)', 6, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (62, N'English', 6, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (63, N'Geography', 6, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (64, N'Geometry (Math)', 6, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (65, N'History (Social Science)', 6, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (66, N'Marathi', 6, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (67, N'Science', 6, 1, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (68, N'Algebra (Math) ', 6, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (69, N'English ', 6, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (70, N'Geography', 6, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (71, N'Geometry (Math)', 6, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (72, N'History (Social Science)', 6, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (73, N'Marathi', 6, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (74, N'Science', 6, 2, CAST(0x0000A86E00FE952A AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [StandardId], [MediumId], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (75, N'Science', 6, 2, CAST(0x0000A86E00FE3FD3 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
ALTER TABLE [dbo].[PasswordResetAccount] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignment_dbo.Medium_MediumId] FOREIGN KEY([MediumId])
REFERENCES [dbo].[Medium] ([Id])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_dbo.Assignment_dbo.Medium_MediumId]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignment_dbo.Mst_Account_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_dbo.Assignment_dbo.Mst_Account_CreatedBy]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignment_dbo.Mst_AccountType_CreatedRole] FOREIGN KEY([CreatedRole])
REFERENCES [dbo].[Mst_AccountType] ([AccountTypeId])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_dbo.Assignment_dbo.Mst_AccountType_CreatedRole]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignment_dbo.Mst_AssignmentType_AssignmentTypeId] FOREIGN KEY([AssignmentTypeId])
REFERENCES [dbo].[Mst_AssignmentType] ([AssignmentTypeId])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_dbo.Assignment_dbo.Mst_AssignmentType_AssignmentTypeId]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignment_dbo.Mst_Board_BoardId] FOREIGN KEY([BoardId])
REFERENCES [dbo].[Mst_Board] ([Id])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_dbo.Assignment_dbo.Mst_Board_BoardId]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignment_dbo.Standard_StandardId] FOREIGN KEY([StandardId])
REFERENCES [dbo].[Standard] ([Id])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_dbo.Assignment_dbo.Standard_StandardId]
GO
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Assignment_dbo.Subject_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_dbo.Assignment_dbo.Subject_SubjectId]
GO
ALTER TABLE [dbo].[AssignmentTeacherMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AssignmentTeacherMapping_dbo.Assignment_AssignmentId] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([AssignmentId])
GO
ALTER TABLE [dbo].[AssignmentTeacherMapping] CHECK CONSTRAINT [FK_dbo.AssignmentTeacherMapping_dbo.Assignment_AssignmentId]
GO
ALTER TABLE [dbo].[AssignmentTeacherMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AssignmentTeacherMapping_dbo.Mst_Account_TeacherAccountId] FOREIGN KEY([TeacherAccountId])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[AssignmentTeacherMapping] CHECK CONSTRAINT [FK_dbo.AssignmentTeacherMapping_dbo.Mst_Account_TeacherAccountId]
GO
ALTER TABLE [dbo].[KnowledgeBank]  WITH CHECK ADD  CONSTRAINT [FK_dbo.KnowledgeBank_dbo.Mst_Account_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[KnowledgeBank] CHECK CONSTRAINT [FK_dbo.KnowledgeBank_dbo.Mst_Account_CreatedBy]
GO
ALTER TABLE [dbo].[KnowledgeBank]  WITH CHECK ADD  CONSTRAINT [FK_dbo.KnowledgeBank_dbo.Mst_Account_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[KnowledgeBank] CHECK CONSTRAINT [FK_dbo.KnowledgeBank_dbo.Mst_Account_ModifiedBy]
GO
ALTER TABLE [dbo].[Mst_Account]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Account_dbo.Medium_MediumId] FOREIGN KEY([MediumId])
REFERENCES [dbo].[Medium] ([Id])
GO
ALTER TABLE [dbo].[Mst_Account] CHECK CONSTRAINT [FK_dbo.Mst_Account_dbo.Medium_MediumId]
GO
ALTER TABLE [dbo].[Mst_Account]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_AccountType_AccountTypeId] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[Mst_AccountType] ([AccountTypeId])
GO
ALTER TABLE [dbo].[Mst_Account] CHECK CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_AccountType_AccountTypeId]
GO
ALTER TABLE [dbo].[Mst_Account]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_Board_BoardId] FOREIGN KEY([BoardId])
REFERENCES [dbo].[Mst_Board] ([Id])
GO
ALTER TABLE [dbo].[Mst_Account] CHECK CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_Board_BoardId]
GO
ALTER TABLE [dbo].[Mst_Account]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_InstitutionAccount_InstitutionAccountId] FOREIGN KEY([InstitutionAccountId])
REFERENCES [dbo].[Mst_InstitutionAccount] ([InstitutionAccountId])
GO
ALTER TABLE [dbo].[Mst_Account] CHECK CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_InstitutionAccount_InstitutionAccountId]
GO
ALTER TABLE [dbo].[Mst_Account]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[Mst_State] ([Id])
GO
ALTER TABLE [dbo].[Mst_Account] CHECK CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_State_StateId]
GO
ALTER TABLE [dbo].[Mst_Account]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Account_dbo.Standard_StudentStandardId] FOREIGN KEY([StudentStandardId])
REFERENCES [dbo].[Standard] ([Id])
GO
ALTER TABLE [dbo].[Mst_Account] CHECK CONSTRAINT [FK_dbo.Mst_Account_dbo.Standard_StudentStandardId]
GO
ALTER TABLE [dbo].[Mst_Activities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Activities_dbo.Mst_Account_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[Mst_Activities] CHECK CONSTRAINT [FK_dbo.Mst_Activities_dbo.Mst_Account_CreatedBy]
GO
ALTER TABLE [dbo].[Mst_CategoryType]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_CategoryType_dbo.Mst_Account_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[Mst_CategoryType] CHECK CONSTRAINT [FK_dbo.Mst_CategoryType_dbo.Mst_Account_CreatedBy]
GO
ALTER TABLE [dbo].[Mst_CategoryType]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_CategoryType_dbo.Mst_Account_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[Mst_CategoryType] CHECK CONSTRAINT [FK_dbo.Mst_CategoryType_dbo.Mst_Account_ModifiedBy]
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Medium_MediumId] FOREIGN KEY([MediumId])
REFERENCES [dbo].[Medium] ([Id])
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount] CHECK CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Medium_MediumId]
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Mst_Board_BoardId] FOREIGN KEY([BoardId])
REFERENCES [dbo].[Mst_Board] ([Id])
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount] CHECK CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Mst_Board_BoardId]
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Mst_Package_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Mst_Package] ([PackageId])
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount] CHECK CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Mst_Package_PackageId]
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Mst_State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[Mst_State] ([Id])
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount] CHECK CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Mst_State_StateId]
GO
ALTER TABLE [dbo].[Mst_Package]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Package_dbo.Mst_AccountType_PackageRoleId] FOREIGN KEY([PackageRoleId])
REFERENCES [dbo].[Mst_AccountType] ([AccountTypeId])
GO
ALTER TABLE [dbo].[Mst_Package] CHECK CONSTRAINT [FK_dbo.Mst_Package_dbo.Mst_AccountType_PackageRoleId]
GO
ALTER TABLE [dbo].[Mst_PackageHistory]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_PackageHistory_dbo.Mst_AccountType_PackageRoleId] FOREIGN KEY([PackageRoleId])
REFERENCES [dbo].[Mst_AccountType] ([AccountTypeId])
GO
ALTER TABLE [dbo].[Mst_PackageHistory] CHECK CONSTRAINT [FK_dbo.Mst_PackageHistory_dbo.Mst_AccountType_PackageRoleId]
GO
ALTER TABLE [dbo].[Mst_PrincipalDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_PrincipalDetail_dbo.Mst_InstitutionAccount_InstitutionAccountId] FOREIGN KEY([InstitutionAccountId])
REFERENCES [dbo].[Mst_InstitutionAccount] ([InstitutionAccountId])
GO
ALTER TABLE [dbo].[Mst_PrincipalDetail] CHECK CONSTRAINT [FK_dbo.Mst_PrincipalDetail_dbo.Mst_InstitutionAccount_InstitutionAccountId]
GO
ALTER TABLE [dbo].[Mst_StudentParentAccount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_StudentParentAccount_dbo.Mst_Account_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[Mst_StudentParentAccount] CHECK CONSTRAINT [FK_dbo.Mst_StudentParentAccount_dbo.Mst_Account_AccountId]
GO
ALTER TABLE [dbo].[Mst_StudentParentAccount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_StudentParentAccount_dbo.Mst_AccountType_AccountTypeId] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[Mst_AccountType] ([AccountTypeId])
GO
ALTER TABLE [dbo].[Mst_StudentParentAccount] CHECK CONSTRAINT [FK_dbo.Mst_StudentParentAccount_dbo.Mst_AccountType_AccountTypeId]
GO
ALTER TABLE [dbo].[PasswordResetAccount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PasswordResetAccount_dbo.Mst_Account_UserAccountId] FOREIGN KEY([UserAccountId])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[PasswordResetAccount] CHECK CONSTRAINT [FK_dbo.PasswordResetAccount_dbo.Mst_Account_UserAccountId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Question_dbo.Assignment_AssignmentId] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([AssignmentId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_dbo.Question_dbo.Assignment_AssignmentId]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Question_dbo.QuestionType_QuestionTypeId] FOREIGN KEY([QuestionTypeId])
REFERENCES [dbo].[QuestionType] ([QuestionTypeId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_dbo.Question_dbo.QuestionType_QuestionTypeId]
GO
ALTER TABLE [dbo].[QuestionContent]  WITH CHECK ADD  CONSTRAINT [FK_dbo.QuestionContent_dbo.Mst_Account_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[QuestionContent] CHECK CONSTRAINT [FK_dbo.QuestionContent_dbo.Mst_Account_CreatedBy]
GO
ALTER TABLE [dbo].[QuestionContent]  WITH CHECK ADD  CONSTRAINT [FK_dbo.QuestionContent_dbo.Mst_Account_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[QuestionContent] CHECK CONSTRAINT [FK_dbo.QuestionContent_dbo.Mst_Account_ModifiedBy]
GO
ALTER TABLE [dbo].[QuestionContent]  WITH CHECK ADD  CONSTRAINT [FK_dbo.QuestionContent_dbo.Mst_CategoryType_CategoryTypeId] FOREIGN KEY([CategoryTypeId])
REFERENCES [dbo].[Mst_CategoryType] ([Id])
GO
ALTER TABLE [dbo].[QuestionContent] CHECK CONSTRAINT [FK_dbo.QuestionContent_dbo.Mst_CategoryType_CategoryTypeId]
GO
ALTER TABLE [dbo].[QuestionOption]  WITH CHECK ADD  CONSTRAINT [FK_dbo.QuestionOption_dbo.Question_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO
ALTER TABLE [dbo].[QuestionOption] CHECK CONSTRAINT [FK_dbo.QuestionOption_dbo.Question_QuestionId]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Subject_dbo.Medium_MediumId] FOREIGN KEY([MediumId])
REFERENCES [dbo].[Medium] ([Id])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_dbo.Subject_dbo.Medium_MediumId]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Subject_dbo.Standard_StandardId] FOREIGN KEY([StandardId])
REFERENCES [dbo].[Standard] ([Id])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_dbo.Subject_dbo.Standard_StandardId]
GO
ALTER TABLE [dbo].[TeacherStandardMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Mst_Account_TeacherId] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[TeacherStandardMapping] CHECK CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Mst_Account_TeacherId]
GO
ALTER TABLE [dbo].[TeacherStandardMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Standard_StandardId] FOREIGN KEY([StandardId])
REFERENCES [dbo].[Standard] ([Id])
GO
ALTER TABLE [dbo].[TeacherStandardMapping] CHECK CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Standard_StandardId]
GO
ALTER TABLE [dbo].[TeacherStandardMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Subject_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
GO
ALTER TABLE [dbo].[TeacherStandardMapping] CHECK CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Subject_SubjectId]
GO
ALTER TABLE [dbo].[UserAssessment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserAssessment_dbo.Mst_Account_GradedBy] FOREIGN KEY([GradedBy])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[UserAssessment] CHECK CONSTRAINT [FK_dbo.UserAssessment_dbo.Mst_Account_GradedBy]
GO
ALTER TABLE [dbo].[UserAssessment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserAssessment_dbo.Mst_Account_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[UserAssessment] CHECK CONSTRAINT [FK_dbo.UserAssessment_dbo.Mst_Account_UserId]
GO
ALTER TABLE [dbo].[UserAssessment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserAssessment_dbo.Question_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Question] ([QuestionId])
GO
ALTER TABLE [dbo].[UserAssessment] CHECK CONSTRAINT [FK_dbo.UserAssessment_dbo.Question_QuestionId]
GO
ALTER TABLE [dbo].[UserAssessment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserAssessment_dbo.QuestionOption_QuestionOptionId] FOREIGN KEY([QuestionOptionId])
REFERENCES [dbo].[QuestionOption] ([Id])
GO
ALTER TABLE [dbo].[UserAssessment] CHECK CONSTRAINT [FK_dbo.UserAssessment_dbo.QuestionOption_QuestionOptionId]
GO
ALTER TABLE [dbo].[UserAssessmentDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserAssessmentDetail_dbo.Assignment_AssignmentId] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([AssignmentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAssessmentDetail] CHECK CONSTRAINT [FK_dbo.UserAssessmentDetail_dbo.Assignment_AssignmentId]
GO
ALTER TABLE [dbo].[UserAssessmentDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserAssessmentDetail_dbo.Mst_Account_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[UserAssessmentDetail] CHECK CONSTRAINT [FK_dbo.UserAssessmentDetail_dbo.Mst_Account_UserId]
GO
