using System.ComponentModel;

namespace Praescio.BusinessEntities.Common
{
    public enum AccountType
    {
        SuperAdmin = 1,
        Admin = 2,
        Teacher = 3,
        Student = 4,
        IndividualTeacher = 5,
        IndividualStudent = 6,
        StudentParent=7
    }

    public enum ActivityType
    {

        InstitutionRegisteration ,
        InstitutionStudentRegisteration,
        InstitutionTeacherRegisteration,
        IndividualStudentRegisteration,
        IndividualTeacherRegisteration ,
        InstitutionPackageRequest
    }

    public enum ExceptionType
    {
        Email = 1,
        PraescioAdmin = 2,
        InstitutionTeacher = 3,
        InstitutionStudent = 4,
        IndividualTeacher = 5,
        IndividualStudent = 6,
        Mail = 7,
        Error = 8
    }

    public enum TypeOfQuestion
    {
        [Description("Display")]
        MeaningOfLesson = 1,
        [Description("Display")]
        SynonymsOfLesson = 2,
        [Description("Display")]
        AntonymsOfLesson = 3,
        [Description("Gradable")]
        WriteReason = 4,
        [Description("NonGradable")]
        FillInTheBlanks = 5,
        [Description("NonGradable")]
        MatchTheFollowing = 6,
        [Description("NonGradable")]
        MCQ = 7,
        [Description("NonGradable")]
        TrueFalse = 8,
        [Description("Gradable")]
        OneSentenceAnswer = 9,
        [Description("Gradable")]
        DescribeBriefly = 10,
        [Description("Gradable")]
        DifferentiateBetween = 11,
        [Description("Gradable")]
        Exercise = 12,
        [Description("Gradable")]
        WriteShortNote = 13,
        [Description("Gradable")]
        TopicConcept = 14
    }

    public enum CategoryType
    {
        Synonyms = 1,
        Antonyms = 2,
    }

    public enum AssignmentStatus
    {
        PendingWithStudent = 1,
        CompletedByStudent = 2,
        SubmittedByStudent = 3,
        CheckingByTeacher = 4,
        CheckedByTeacher = 5,
        SubmittedByTeacher = 6,
    }

    public enum AssignmentType
    {
        PraescioLesson = 1,
        InstitutionAssignment = 2,
        IndividualAssignment = 3,
        Handwriting = 4,
        Pronounciation = 5,
        PExtra = 6,
        All = 7
    }

    public enum VersionType
    {
        Paid ,
        Trial ,
    }
    public enum MailType
    {
        NoContent,
        ResetPassword,
        IndividualStudentRegister,
        IndividualTeacherRegister,
        InstitutionTeacher,
        InstitutionStudent
    };

}
