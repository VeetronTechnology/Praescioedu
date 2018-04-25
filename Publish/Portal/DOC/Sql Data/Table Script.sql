GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 1/6/2018 9:45:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[AssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[AccountId] [int] NULL,
	[StandardId] [int] NULL,
	[SubjectId] [int] NULL,
	[MediumId] [int] NULL,
	[CreatedRole] [int] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[isQuestionAdded] [bit] NULL,
	[isInstitution] [bit] NULL,
	[UploadFileSrc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Assignment] PRIMARY KEY CLUSTERED 
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AssignmentTeacherMapping]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[KnowledgeBank]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[Medium]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[Mst_Account]    Script Date: 1/6/2018 9:45:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[AccountTypeId] [int] NULL,
	[InstitutionName] [nvarchar](max) NULL,
	[VersionType] [nvarchar](max) NULL,
	[IsIndividual] [bit] NULL,
	[ActivateOn] [datetime] NULL,
	[ExpiredOn] [datetime] NULL,
	[StudentStandardId] [int] NULL,
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
	[Password] [nvarchar](max) NULL,
	[ParentMobileNo] [nvarchar](max) NULL,
	[InstitutionAccountId] [int] NULL,
	[InstitutionAddress] [nvarchar](max) NULL,
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
	[Account_AccountId] [int] NULL,
 CONSTRAINT [PK_dbo.Mst_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Mst_AccountType]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[Mst_Board]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[Mst_CategoryType]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[Mst_InstitutionAccount]    Script Date: 1/6/2018 9:45:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mst_InstitutionAccount](
	[InstitutionAccountId] [int] IDENTITY(1,1) NOT NULL,
	[DomainKey] [nvarchar](max) NOT NULL,
	[RegistrationNo] [nvarchar](max) NULL,
	[InstitutionName] [nvarchar](max) NULL,
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
	[NoOfUser] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Mst_PrincipalDetail]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[Mst_State]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[PasswordResetAccount]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[Question]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[QuestionContent]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[QuestionOption]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[QuestionType]    Script Date: 1/6/2018 9:45:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionType](
	[QuestionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
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
/****** Object:  Table [dbo].[Standard]    Script Date: 1/6/2018 9:45:31 AM ******/
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
/****** Object:  Table [dbo].[Subject]    Script Date: 1/6/2018 9:45:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Subject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TeacherStandardMapping]    Script Date: 1/6/2018 9:45:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherStandardMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NOT NULL,
	[StandardId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.TeacherStandardMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAssessment]    Script Date: 1/6/2018 9:45:31 AM ******/
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
SET IDENTITY_INSERT [dbo].[Medium] ON 

GO
INSERT [dbo].[Medium] ([Id], [Name], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'English', CAST(0x0000A8600025E251 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Medium] ([Id], [Name], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Hindi', CAST(0x0000A8600025E251 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Medium] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_Account] ON 

GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (1, N'100001', N'ALI', N'AHMED', 1, N'SuperAdmin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Mumbai', NULL, N'7208305705', N'ali.tech1108@gmail.com', N'123456', NULL, 1, NULL, NULL, NULL, NULL, NULL, 1, N'Ali', NULL, NULL, NULL, CAST(0x0000A8600025E218 AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (2, N'1000002', NULL, NULL, 2, N'MICHAEL HIGH SCHOOL', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'MUMBAI', NULL, N'', N'aliahmedk4@gmail.com', N'password', NULL, 2, NULL, NULL, NULL, NULL, NULL, 1, N'Ali', NULL, NULL, NULL, CAST(0x0000A860002724CB AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (3, N'indteacher', N'indteacher', N'indteacher', 5, N'MICHAEL HIGH SCHOOL', N'Paid', 1, CAST(0x0000A86000000000 AS DateTime), CAST(0x0000A9CD00000000 AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, N'MUMBAI', NULL, NULL, NULL, N'7208305705', N'indteacher@gmail.com', N'indteacher', NULL, 1, N'KURLA', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, CAST(0x0000A86000276CD4 AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (4, N'student1', N'ALI AHMNED', N'asd', 6, N'MICHAEL asd', N'Paid', 1, NULL, NULL, NULL, N'asd', N'asd', N'a@gmail.com', N'4645546546654', NULL, NULL, NULL, NULL, NULL, NULL, N'aliahmedk4@gmail.com', N'student1', N'213312132132', 1, N'asd', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, CAST(0x0000A8600027C137 AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (5, N'student123', N'student123', N'student1', 6, N'student1@gmail.com', N'Trail', 1, CAST(0x0000A9CD00000000 AS DateTime), CAST(0x0000A9CD00000000 AS DateTime), NULL, N'student1@gmail.com', N'student1@gmail.com', N'student1@gmail.com', N'2123231321', NULL, NULL, NULL, NULL, NULL, NULL, N'student1@gmail.com', N'student123', N'789546456456', 1, N'student1@gmail.com', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, CAST(0x0000A8600029AAB6 AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (6, N'student123123', N'student123123', N'student123123', 6, N'asdasd', N'Paid', 1, CAST(0x0000A86000000000 AS DateTime), CAST(0x0000A9CD00000000 AS DateTime), NULL, N'student123123@gmail.com', N'student123123@gmail.com', N'a@gmail.com', N'564564546546', NULL, NULL, NULL, NULL, NULL, NULL, N'student123123@gmail.com', N'student123123', N'465546546', 1, N'asd', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, CAST(0x0000A860002DD6B5 AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (7, N'student1231', N'student1231', N'student123123', 6, N'asdasd', N'Paid', 1, CAST(0x0000A86000000000 AS DateTime), CAST(0x0000A9CD00000000 AS DateTime), NULL, N'student123123@gmail.com', N'student123123@gmail.com', N'a@gmail.com', N'564564546546', NULL, NULL, NULL, NULL, NULL, NULL, N'student123123@gmail.com', N'student1231', N'465546546', 1, N'asd', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, CAST(0x0000A860002DFF11 AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (8, N'TeacherMIC', N'TeacherMIC', N'TeacherMIC', 3, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'asd', NULL, NULL, NULL, N'2131232121', N'TeacherMIC@gmail.com', N'TeacherMIC', NULL, 2, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, CAST(0x0000A8600030C4B8 AS DateTime), NULL, 1, NULL)
GO
INSERT [dbo].[Mst_Account] ([AccountId], [UserName], [FirstName], [LastName], [AccountTypeId], [InstitutionName], [VersionType], [IsIndividual], [ActivateOn], [ExpiredOn], [StudentStandardId], [MotherName], [FatherName], [ParentEmail], [ParentNo], [URL], [Address], [StateId], [City], [PinCode], [MobileNo], [Email], [Password], [ParentMobileNo], [InstitutionAccountId], [InstitutionAddress], [FacebookID], [LastLoginOn], [CreatedBy], [ModifiedBy], [IsConfirmed], [ContactName], [ConfirmationCode], [ComfirmationCodeExpiration], [UpdatePwdDateTime], [CreatedOn], [ModifiedOn], [IsActive], [Account_AccountId]) VALUES (9, N'teachermic1', N'teachermic1', N'teachermic1', 3, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'asd', NULL, NULL, NULL, N'456545546', N'teachermic1@gmail.com', N'teachermic1', NULL, 2, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, CAST(0x0000A86000336F82 AS DateTime), NULL, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Mst_Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_AccountType] ON 

GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'SuperAdmin', NULL, 0, 0, CAST(0x0000A8600025E1FB AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Admin', NULL, 0, 0, CAST(0x0000A8600025E1FB AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'Teacher', NULL, 0, 0, CAST(0x0000A8600025E1FB AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'Student', NULL, 0, 0, CAST(0x0000A8600025E1FB AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'IndividualTeacher', NULL, 0, 0, CAST(0x0000A8600025E1FB AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_AccountType] ([AccountTypeId], [AccountType], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'IndividualStudent', NULL, 0, 0, CAST(0x0000A8600025E1FB AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_AccountType] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_Board] ON 

GO
INSERT [dbo].[Mst_Board] ([Id], [BoardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'MH Board', CAST(0x0000A8600025E1F2 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Board] ([Id], [BoardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'CBSE Board', CAST(0x0000A8600025E1F2 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_Board] ([Id], [BoardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'ICSE Board', CAST(0x0000A8600025E1F2 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_Board] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_CategoryType] ON 

GO
INSERT [dbo].[Mst_CategoryType] ([Id], [CategoryName], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (1, N'Synonyms', CAST(0x0000A8600025E19A AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Mst_CategoryType] ([Id], [CategoryName], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy], [IsActive]) VALUES (2, N'Antonyms', CAST(0x0000A8600025E19D AS DateTime), NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_CategoryType] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_InstitutionAccount] ON 

GO
INSERT [dbo].[Mst_InstitutionAccount] ([InstitutionAccountId], [DomainKey], [RegistrationNo], [InstitutionName], [CustomerName], [MobileNo], [LandlineNo], [Email], [AddressProofSrc], [Address], [StateId], [City], [Pincode], [BoardId], [MediumId], [Description], [ActivateOn], [ExpiredOn], [NoOfUser], [NativeLanguage], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'ins', NULL, N'Praescio Organization', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'initial setup organization', NULL, NULL, 0, NULL, 0, 0, CAST(0x0000A8600025E208 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_InstitutionAccount] ([InstitutionAccountId], [DomainKey], [RegistrationNo], [InstitutionName], [CustomerName], [MobileNo], [LandlineNo], [Email], [AddressProofSrc], [Address], [StateId], [City], [Pincode], [BoardId], [MediumId], [Description], [ActivateOn], [ExpiredOn], [NoOfUser], [NativeLanguage], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'mic', N'MH00002', N'MICHAEL HIGH SCHOOL', NULL, N'720833058705', N'213231564564', N'aliahmedk4@gmail.com', N'Upload/RequestUpload/eb2ef1ea-2d89-49e2-802f-06f9b88202ec//Hinduja Rs 1500 6 Oct.pdf', N'MUMBAI', 1, N'MUMBAI', N'400070', 1, 1, NULL, CAST(0x0000A86000000000 AS DateTime), CAST(0x0000A9CD00000000 AS DateTime), 0, NULL, 0, 0, CAST(0x0000A86000272398 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_InstitutionAccount] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_PrincipalDetail] ON 

GO
INSERT [dbo].[Mst_PrincipalDetail] ([Id], [InstitutionAccountId], [Name], [Designation], [Email], [Gender], [PersonPhotoSrc], [AuthorityLetterSrc], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, 2, NULL, N'TEST', N'aliahmedk4@gmail.com', NULL, N'Upload/RequestUpload/c20a8e84-42f2-4097-8ad8-907b9220ac22//Hinduja Rs 1500 6 Oct.pdf', N'Upload/RequestUpload/298b0add-3adb-45a1-a839-a462f9e0e394//Hinduja Rs 1500 6 Oct.pdf', CAST(0x0000A860002724DF AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_PrincipalDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Mst_State] ON 

GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'Maharashtra', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Delhi', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'Rajasthan', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'Punjab', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'Uttar Pradesh', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'Hyderabad', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (7, N'Kerala', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (8, N'West Bengal', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (9, N'Goa', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (10, N'Haryana', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Mst_State] ([Id], [StateName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (11, N'Gujarat', CAST(0x0000A8600025E257 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Mst_State] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionType] ON 

GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'MeaningOfLesson', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'SynonymsOfLesson', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'AntonymsOfLesson', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'WriteReason', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'FillInTheBlanks', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'MatchTheFollowing', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (7, N'MCQ', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (8, N'TrueFalse', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (9, N'OneSentenceAnswer', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (10, N'DescribeBriefly', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (11, N'DifferentiateBetween', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (12, N'Exercise', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (13, N'WriteShortNote', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
INSERT [dbo].[QuestionType] ([QuestionTypeId], [Name], [Description], [CreatedBy], [ModifiedBy], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (14, N'TopicConcept', NULL, NULL, NULL, CAST(0x0000A8600025E234 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[QuestionType] OFF
GO
SET IDENTITY_INSERT [dbo].[Standard] ON 

GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'5th', CAST(0x0000A8600025E22D AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'6th', CAST(0x0000A8600025E22D AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'7th', CAST(0x0000A8600025E22D AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'8th', CAST(0x0000A8600025E22D AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'9th', CAST(0x0000A8600025E22D AS DateTime), NULL, 1)
GO
INSERT [dbo].[Standard] ([Id], [StandardName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'10th', CAST(0x0000A8600025E22D AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Standard] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (1, N'English', CAST(0x0000A8600025E248 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (2, N'Marathi', CAST(0x0000A8600025E248 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (3, N'Science', CAST(0x0000A8600025E248 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (4, N'Mathematic', CAST(0x0000A8600025E248 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (5, N'History', CAST(0x0000A8600025E248 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (6, N'Algebra', CAST(0x0000A8600025E248 AS DateTime), NULL, 1)
GO
INSERT [dbo].[Subject] ([Id], [SubjectName], [CreatedOn], [ModifiedOn], [IsActive]) VALUES (7, N'EVS', CAST(0x0000A8600025E248 AS DateTime), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[TeacherStandardMapping] ON 

GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (1, 3, 1, 1)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (2, 3, 1, 3)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (3, 3, 1, 5)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (4, 3, 1, 1)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (5, 3, 1, 4)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (6, 3, 1, 5)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (7, 6, 1, 1)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (8, 6, 1, 5)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (9, 7, 4, 1)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (10, 7, 4, 5)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (11, 9, 4, 3)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (12, 9, 4, 5)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (13, 9, 1, 2)
GO
INSERT [dbo].[TeacherStandardMapping] ([Id], [TeacherId], [StandardId], [SubjectId]) VALUES (14, 9, 1, 5)
GO
SET IDENTITY_INSERT [dbo].[TeacherStandardMapping] OFF
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
ALTER TABLE [dbo].[Mst_Account]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_Account_Account_AccountId] FOREIGN KEY([Account_AccountId])
REFERENCES [dbo].[Mst_Account] ([AccountId])
GO
ALTER TABLE [dbo].[Mst_Account] CHECK CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_Account_Account_AccountId]
GO
ALTER TABLE [dbo].[Mst_Account]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_AccountType_AccountTypeId] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[Mst_AccountType] ([AccountTypeId])
GO
ALTER TABLE [dbo].[Mst_Account] CHECK CONSTRAINT [FK_dbo.Mst_Account_dbo.Mst_AccountType_AccountTypeId]
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
ALTER TABLE [dbo].[Mst_InstitutionAccount]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Mst_State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[Mst_State] ([Id])
GO
ALTER TABLE [dbo].[Mst_InstitutionAccount] CHECK CONSTRAINT [FK_dbo.Mst_InstitutionAccount_dbo.Mst_State_StateId]
GO
ALTER TABLE [dbo].[Mst_PrincipalDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Mst_PrincipalDetail_dbo.Mst_InstitutionAccount_InstitutionAccountId] FOREIGN KEY([InstitutionAccountId])
REFERENCES [dbo].[Mst_InstitutionAccount] ([InstitutionAccountId])
GO
ALTER TABLE [dbo].[Mst_PrincipalDetail] CHECK CONSTRAINT [FK_dbo.Mst_PrincipalDetail_dbo.Mst_InstitutionAccount_InstitutionAccountId]
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
ALTER TABLE [dbo].[TeacherStandardMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Standard_StandardId] FOREIGN KEY([StandardId])
REFERENCES [dbo].[Standard] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TeacherStandardMapping] CHECK CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Standard_StandardId]
GO
ALTER TABLE [dbo].[TeacherStandardMapping]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TeacherStandardMapping_dbo.Subject_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([Id])
ON DELETE CASCADE
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
