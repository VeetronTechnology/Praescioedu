namespace Praescio.Domain.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Praescio.Domain.DAL.PraescioContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Praescio.Domain.DAL.PraescioContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Package.AddOrUpdate(
            c => c.PackageId,
            new Mst_Package() { PackageId = 1, PackageName = "Package No 1", PackageAmount = 10000, PackageData = "{  'NoOfStudent': 5000,  'NoOfTeacher': 5000,  'PackageType': 'Institution Package'}", PackageRoleId = 2, CreatedOn = DateTime.Now, IsActive = true },
            new Mst_Package() { PackageId = 2, PackageName = "Package No 2", PackageAmount = 10000, PackageData = "{  'NoOfStudent': 2500,  'NoOfTeacher': 2500,  'PackageType': 'Institution Package'}", PackageRoleId = 2, CreatedOn = DateTime.Now, IsActive = true },
            new Mst_Package() { PackageId = 3, PackageName = "Package No 3", PackageAmount = 10000, PackageData = "{  'NoOfStudent': 1000,  'NoOfTeacher': 1000,  'PackageType': 'Institution Package'}", PackageRoleId = 2, CreatedOn = DateTime.Now, IsActive = true },
            new Mst_Package() { PackageId = 4, PackageName = "Package No 1", PackageAmount = 1000, PackageData = "{  'NoOfStudent': 1000,  'NoOfTeacher': 1000,  'PackageType': 'Individual Student Package'}", PackageRoleId = 5, CreatedOn = DateTime.Now, IsActive = true },
            new Mst_Package() { PackageId = 5, PackageName = "Package No 2", PackageAmount = 1500, PackageData = "{  'NoOfStudent': 1000,  'NoOfTeacher': 1000,  'PackageType': 'Individual Student Package'}", PackageRoleId = 5, CreatedOn = DateTime.Now, IsActive = true },
            new Mst_Package() { PackageId = 6, PackageName = "Package No 3", PackageAmount = 2000, PackageData = "{  'NoOfStudent': 1000,  'NoOfTeacher': 1000,  'PackageType': 'Individual Student Package'}", PackageRoleId = 5, CreatedOn = DateTime.Now, IsActive = true },
            new Mst_Package() { PackageId = 7, PackageName = "Package No 1", PackageAmount = 1000, PackageData = "{  'NoOfStudent': 1000,  'NoOfTeacher': 1000,  'PackageType': 'Individual Teacher Package'}", PackageRoleId = 6, CreatedOn = DateTime.Now, IsActive = true },
            new Mst_Package() { PackageId = 8, PackageName = "Package No 2", PackageAmount = 1500, PackageData = "{  'NoOfStudent': 1000,  'NoOfTeacher': 1000,  'PackageType': 'Individual Teacher Package'}", PackageRoleId = 6, CreatedOn = DateTime.Now, IsActive = true },
            new Mst_Package() { PackageId = 9, PackageName = "Package No 3", PackageAmount = 2000, PackageData = "{  'NoOfStudent': 1000,  'NoOfTeacher': 1000,  'PackageType': 'Individual Teacher Package'}", PackageRoleId = 6, CreatedOn = DateTime.Now, IsActive = true }
            );

            context.CategoryType.AddOrUpdate(
              c => c.Id,
              new Mst_CategoryType() { Id = 1, CategoryName = "Synonyms", CreatedOn = DateTime.Now, IsActive = true },
              new Mst_CategoryType() { Id = 2, CategoryName = "Antonyms", CreatedOn = DateTime.Now, IsActive = true }
              );

            context.AssignmentStatus.AddOrUpdate(
              c => c.StatusId,
              new AssignmentStatus() { StatusId = 1, StatusTitle = "Pending With Student", IsActive = true },
              new AssignmentStatus() { StatusId = 2, StatusTitle = "Completed By Student", IsActive = true },
              new AssignmentStatus() { StatusId = 3, StatusTitle = "Submitted By Student", IsActive = true },
              new AssignmentStatus() { StatusId = 4, StatusTitle = "Checking By Teacher", IsActive = true },
              new AssignmentStatus() { StatusId = 5, StatusTitle = "Checked By Teacher", IsActive = true },
              new AssignmentStatus() { StatusId = 6, StatusTitle = "Submitted By Teacher", IsActive = true }
              );

            context.AssignmentType.AddOrUpdate(
              c => c.AssignmentTypeId,
              new Mst_AssignmentType() { AssignmentTypeId = 1, AssignmentTypeName = "Praescio Lesson", CreatedOn = DateTime.Now, IsActive = true },
              new Mst_AssignmentType() { AssignmentTypeId = 2, AssignmentTypeName = "Institution Assignment", CreatedOn = DateTime.Now, IsActive = true },
              new Mst_AssignmentType() { AssignmentTypeId = 3, AssignmentTypeName = "Individual Assignment", CreatedOn = DateTime.Now, IsActive = true },
              new Mst_AssignmentType() { AssignmentTypeId = 4, AssignmentTypeName = "Handwriting", CreatedOn = DateTime.Now, IsActive = true },
              new Mst_AssignmentType() { AssignmentTypeId = 5, AssignmentTypeName = "Pronounciation", CreatedOn = DateTime.Now, IsActive = true },
              new Mst_AssignmentType() { AssignmentTypeId = 6, AssignmentTypeName = "PExtra", CreatedOn = DateTime.Now, IsActive = true }
              );

            context.Board.AddOrUpdate(
                c => c.Id,
                new Mst_Board() { Id = 1, BoardName = "MH Board", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_Board() { Id = 2, BoardName = "CBSE Board", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_Board() { Id = 3, BoardName = "ICSE Board", CreatedOn = DateTime.Now, IsActive = true }
                );

            context.AccountType.AddOrUpdate(
                c => c.AccountTypeId,
                new Mst_AccountType() { AccountTypeId = 1, AccountType = "SuperAdmin", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_AccountType() { AccountTypeId = 2, AccountType = "Admin", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_AccountType() { AccountTypeId = 3, AccountType = "Teacher", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_AccountType() { AccountTypeId = 4, AccountType = "Student", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_AccountType() { AccountTypeId = 5, AccountType = "IndividualTeacher", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_AccountType() { AccountTypeId = 6, AccountType = "IndividualStudent", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_AccountType() { AccountTypeId = 7, AccountType = "StudentParent", CreatedOn = DateTime.Now, IsActive = true }
                );

            context.OrganizationAccount.AddOrUpdate(
                c => c.InstitutionAccountId,
                new Mst_InstitutionAccount() { InstitutionAccountId = 1, InstitutionName = "Praescio Organization", DomainKey = "ins", CreatedOn = DateTime.Now, Description = "initial setup organization" }
                );

            context.Account.AddOrUpdate(
                c => c.AccountId,
                new Mst_Account() { AccountId = 1, FirstName = "Praescio", LastName = "Admin", MobileNo = "7208305705", UserName = "100001", Password = "123456", ContactName = "Ali", Email = "ali.tech1108@gmail.com", City = "Mumbai", InstitutionName = "SuperAdmin", AccountTypeId = 1, InstitutionAccountId = 1, CreatedOn = DateTime.Now, IsActive = true }
                );

            context.Standard.AddOrUpdate(
                c => c.Id,
                new Standard() { Id = 1, StandardName = "5th", CreatedOn = DateTime.Now, IsActive = true },
                new Standard() { Id = 2, StandardName = "6th", CreatedOn = DateTime.Now, IsActive = true },
                new Standard() { Id = 3, StandardName = "7th", CreatedOn = DateTime.Now, IsActive = true },
                new Standard() { Id = 4, StandardName = "8th", CreatedOn = DateTime.Now, IsActive = true },
                new Standard() { Id = 5, StandardName = "9th", CreatedOn = DateTime.Now, IsActive = true },
                new Standard() { Id = 6, StandardName = "10th", CreatedOn = DateTime.Now, IsActive = true }
                );

            context.QuestionType.AddOrUpdate(
                  c => c.QuestionTypeId,
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 1, Name = "MeaningOfLesson", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 2, Name = "SynonymsOfLesson", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 3, Name = "AntonymsOfLesson", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 4, Name = "WriteReason", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 5, Name = "FillInTheBlanks", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 6, Name = "MatchTheFollowing", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 7, Name = "MCQ", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 8, Name = "TrueFalse", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 9, Name = "OneSentenceAnswer", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 10, Name = "DescribeBriefly", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 11, Name = "DifferentiateBetween", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 12, Name = "Exercise", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 13, Name = "WriteShortNote", CreatedOn = DateTime.Now, IsActive = true },
                  new QuestionType() { TotalMarks = 1, QuestionTypeId = 14, Name = "TopicConcept", CreatedOn = DateTime.Now, IsActive = true }
                  );

            context.Subject.AddOrUpdate(
                c => c.Id,
                //5//
                new Subject() { Id = 1, SubjectName = "English", StandardId = 1, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 2, SubjectName = "EVS Part-1", StandardId = 1, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 3, SubjectName = "EVS Part-2", StandardId = 1, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 4, SubjectName = "Marathi", StandardId = 1, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 5, SubjectName = "Math", StandardId = 1, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 6, SubjectName = "English", StandardId = 1, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 7, SubjectName = "Marathi", StandardId = 1, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 8, SubjectName = "Math ", StandardId = 1, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 9, SubjectName = "Science", StandardId = 1, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 10, SubjectName = "Social Science Part-1", StandardId = 1, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 11, SubjectName = "Social Science Part-2", StandardId = 1, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },

                //6//
                new Subject() { Id = 11, SubjectName = "English", StandardId = 2, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 12, SubjectName = "General Science", StandardId = 2, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 13, SubjectName = "Geography", StandardId = 2, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 14, SubjectName = "History (Social Science)", StandardId = 1, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 15, SubjectName = "Marathi", StandardId = 2, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 16, SubjectName = "Math", StandardId = 2, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 17, SubjectName = "Social (Science History)", StandardId = 2, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 18, SubjectName = "English ", StandardId = 2, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 19, SubjectName = "History (Social Science)", StandardId = 2, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 20, SubjectName = "Marathi", StandardId = 2, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 21, SubjectName = "Math", StandardId = 2, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 22, SubjectName = "Science", StandardId = 2, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },

                 //7//
                 new Subject() { Id = 23, SubjectName = "English", StandardId = 3, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 24, SubjectName = "Geography", StandardId = 3, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 25, SubjectName = "History (Social Science)", StandardId = 3, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 26, SubjectName = "Marathi", StandardId = 3, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 27, SubjectName = "Math", StandardId = 3, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 28, SubjectName = "Science", StandardId = 3, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 29, SubjectName = "English ", StandardId = 3, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 30, SubjectName = "Geography", StandardId = 3, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 31, SubjectName = "History (Social Science)", StandardId = 3, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 32, SubjectName = "Marathi", StandardId = 3, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 33, SubjectName = "Math", StandardId = 3, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 34, SubjectName = "Science", StandardId = 3, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },

                 //8//
                 new Subject() { Id = 35, SubjectName = "English", StandardId = 4, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 36, SubjectName = "Geography", StandardId = 4, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 37, SubjectName = "History (Social Science)", StandardId = 4, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 38, SubjectName = "Marathi", StandardId = 4, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 39, SubjectName = "Math", StandardId = 4, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 40, SubjectName = "Science", StandardId = 4, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 41, SubjectName = "English ", StandardId = 4, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 42, SubjectName = "Geography", StandardId = 4, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 43, SubjectName = "History (Social Science)", StandardId = 4, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 44, SubjectName = "Marathi", StandardId = 4, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 45, SubjectName = "Math", StandardId = 4, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 46, SubjectName = "Science", StandardId = 4, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },

                 //9//
                 new Subject() { Id = 47, SubjectName = "English", StandardId = 5, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 48, SubjectName = "Geography", StandardId = 5, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 49, SubjectName = "History (Social Science)", StandardId = 5, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 50, SubjectName = "Marathi", StandardId = 5, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 51, SubjectName = "Math Part-1", StandardId = 5, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 52, SubjectName = "Math Part-2", StandardId = 5, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 53, SubjectName = "Science ", StandardId = 5, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 54, SubjectName = "English ", StandardId = 5, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 55, SubjectName = "Geography", StandardId = 5, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 56, SubjectName = "History (Social Science)", StandardId = 5, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 57, SubjectName = "Marathi", StandardId = 5, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                 new Subject() { Id = 58, SubjectName = "Math Part-1", StandardId = 5, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 59, SubjectName = "Math Part-2", StandardId = 5, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 60, SubjectName = "Science", StandardId = 5, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },

                 //10//
                 new Subject() { Id = 61, SubjectName = "Algebra (Math)", StandardId = 6, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 62, SubjectName = "English", StandardId = 6, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 63, SubjectName = "Geography", StandardId = 6, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 64, SubjectName = "Geometry (Math)", StandardId = 6, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 65, SubjectName = "History (Social Science)", StandardId = 6, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 66, SubjectName = "Marathi", StandardId = 6, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 67, SubjectName = "Science", StandardId = 6, MediumId = 1, CreatedOn = DateTime.Now, IsActive = true },

                new Subject() { Id = 68, SubjectName = "Algebra (Math) ", StandardId = 6, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 69, SubjectName = "English ", StandardId = 6, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 70, SubjectName = "Geography", StandardId = 6, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 71, SubjectName = "Geometry (Math)", StandardId = 6, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 72, SubjectName = "History (Social Science)", StandardId = 6, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 73, SubjectName = "Marathi", StandardId = 6, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true },
                new Subject() { Id = 74, SubjectName = "Science", StandardId = 6, MediumId = 2, CreatedOn = DateTime.Now, IsActive = true }
                );

            context.Medium.AddOrUpdate(
               c => c.Id,
               new Medium() { Id = 1, Name = "English", CreatedOn = DateTime.Now, IsActive = true },
               new Medium() { Id = 2, Name = "Marathi", CreatedOn = DateTime.Now, IsActive = true }
               );



            context.State.AddOrUpdate(
                c => c.Id,
                new Mst_State() { Id = 1, StateName = "Maharashtra", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 2, StateName = "Delhi", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 3, StateName = "Rajasthan", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 4, StateName = "Punjab", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 5, StateName = "Uttar Pradesh", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 6, StateName = "Hyderabad", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 7, StateName = "Kerala", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 8, StateName = "West Bengal", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 9, StateName = "Goa", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 10, StateName = "Haryana", CreatedOn = DateTime.Now, IsActive = true },
                new Mst_State() { Id = 11, StateName = "Gujarat", CreatedOn = DateTime.Now, IsActive = true }
                );

            //context.City.AddOrUpdate(
            //    c => c.Id,
            //    new Mst_City() { Id = 1, Name = "Mumbai", CreatedOn = DateTime.Now, StateId = 1, IsActive = true },
            //    new Mst_City() { Id = 2, Name = "Pune", CreatedOn = DateTime.Now, StateId = 1, IsActive = true },
            //    new Mst_City() { Id = 3, Name = "Nashik", CreatedOn = DateTime.Now, StateId = 1, IsActive = true },
            //    new Mst_City() { Id = 4, Name = "Kalyan", CreatedOn = DateTime.Now, StateId = 1, IsActive = true },
            //    new Mst_City() { Id = 5, Name = "Thane", CreatedOn = DateTime.Now, StateId = 1, IsActive = true },
            //    new Mst_City() { Id = 6, Name = "Lucknow", CreatedOn = DateTime.Now, StateId = 5, IsActive = true },
            //    new Mst_City() { Id = 7, Name = "Ahmedabad", CreatedOn = DateTime.Now, StateId = 11, IsActive = true },
            //    new Mst_City() { Id = 8, Name = "Chandni Chowk", CreatedOn = DateTime.Now, StateId = 3, IsActive = true },
            //    new Mst_City() { Id = 9, Name = "Charminar", CreatedOn = DateTime.Now, StateId = 6, IsActive = true },
            //    new Mst_City() { Id = 10, Name = "Panaji", CreatedOn = DateTime.Now, StateId = 9, IsActive = true }
            //    );
        }

    }
}
