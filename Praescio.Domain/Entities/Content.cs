using Praescio.Domain.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.Domain.Entities
{
    public class Mst_AssignmentType
    {
        [Key]
        public int? AssignmentTypeId { get; set; }
        public string AssignmentTypeName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = false;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignmentTypeId { get; set; }
        public int? AccountId { get; set; }
        [ForeignKey("Board")]
        public int? BoardId { get; set; }
        [ForeignKey("Medium")]
        public int? MediumId { get; set; }
        [ForeignKey("Standard")]
        public int? StandardId { get; set; }
        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }

        [ForeignKey("AccountType")]
        public int? CreatedRole { get; set; }
        [ForeignKey("Account")]
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? isQuestionAdded { get; set; }
        public bool? isInstitution { get; set; }
        public string UploadFileSrc { get; set; }
        public string UploadFileSrcQuestion { get; set; }
        private bool _isPublished = false;
        public bool IsPublished
        {
            get { return _isPublished; }
            set { _isPublished = value; }
        }
        public DateTime? PublishedDate { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_Account Account { get; set; }
        public virtual Mst_AccountType AccountType { get; set; }
        public virtual Mst_Board Board { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual Mst_AssignmentType AssignmentType { get; set; }

        [NotMapped]
        public List<Question> Question { get; set; }

        [NotMapped]
        public List<Video> Video { get; set; }
    }

    public class AssignmentTeacherMapping
    {
        [Key]
        public int Id { get; set; }
        public int? AssignmentId { get; set; }
        [ForeignKey("Teacher")]
        public int? TeacherAccountId { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Assignment Assignment { get; set; }
        public virtual Mst_Account Teacher { get; set; }
    }

    public class Video
    {
        [Key]
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignmentId { get; set; }
        public string VideoSrc { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }

    public class Creativity
    {
        [Key]
        public int CreativityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public string FileSrc { get; set; }
        public int? StandardId { get; set; }
        public int? SubjectId { get; set; }
        public int AverageRating { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }

        public virtual Mst_Account Account { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual List<CreativityComment> Comments { get; set; }
        //public virtual bool? IsCurrentUserCommented { get; set; }
    }

    public class CreativityComment
    {
        [Key]
        public int CreativityCommentId { get; set; }
        public int CreativityId { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string FileSrc { get; set; }
        public int Rating { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_Account Account { get; set; }
    }

    public class CreativityWithComment
    {
        public Creativity Creativity { get; set; }
        public List<CreativityComment> Comments { get; set; }
    }

    public class QuestionType
    {
        [Key]
        public int QuestionTypeId { get; set; }
        public string Name { get; set; }
        public int? TotalMarks { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }

    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssignmentId { get; set; }
        public int? QuestionTypeId { get; set; }
        public int? NoOfBlanks { get; set; }
        public string MCQQuestionImageSrc { get; set; }
        public string MCQText1 { get; set; }
        public string MCQText2 { get; set; }
        public string MCQText3 { get; set; }
        public string MCQText4 { get; set; }
        public string MCQImage1Src { get; set; }
        public string MCQImage2Src { get; set; }
        public string MCQImage3Src { get; set; }
        public string MCQImage4Src { get; set; }

        public string CorrectAnswer { get; set; }
        public int TotalMarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual QuestionType QuestionType { get; set; }
        public virtual Assignment Assignment { get; set; }
        [NotMapped]
        public List<QuestionOption> QuestionOption { get; set; }
        [NotMapped]
        public bool? IsUserSubmitted { get; set; }
        [NotMapped]
        public bool? IsCheckedByTeacher { get; set; }
        [NotMapped]
        public int? MarksScored { get; set; }
        [NotMapped]
        public int? StudentAnswer { get; set; }

    }

    public class QuestionOption
    {
        [Key]
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string Category { get; set; }
        public string Option { get; set; }
        public string MatchQuestion { get; set; }
        public string MatchAnswer { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Question Question { get; set; }

        [NotMapped]
        public string StudentAnswer { get; set; }
        public string ImageSrc { get; set; }
    }

    public class KnowledgeBank
    {
        [Key]
        public int KnowledgeBankId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("CreatedAccount")]
        public int? CreatedBy { get; set; }
        [ForeignKey("ModifiedAccount")]
        public int? ModifiedBy { get; set; }
        public string VisibleToRole { get; set; }
        public string PDFFileSrc { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_Account CreatedAccount { get; set; }
        public virtual Mst_Account ModifiedAccount { get; set; }
    }

    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string SubjectName { get; set; }
        [ForeignKey("Standard")]
        public int? StandardId { get; set; }
        [ForeignKey("Medium")]
        public int? MediumId { get; set; }
        [ForeignKey("Board")]
        public int? BoardId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Standard Standard { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual Mst_Board Board { get; set; }

    }

    public class Standard
    {
        [Key]
        public int Id { get; set; }
        public string StandardName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }
    public class Medium
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }
    public class QuestionContent
    {
        [Key]
        public int ContentId { get; set; }
        [ForeignKey("CategoryType")]
        public int? CategoryTypeId { get; set; }
        public string ContentOption { get; set; }
        public string ContentAnswer { get; set; }
        public int? InstitutionAccountId { get; set; }
        [ForeignKey("CreatedAccount")]
        public int? CreatedBy { get; set; }
        [ForeignKey("ModifiedAccount")]
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_Account CreatedAccount { get; set; }
        public virtual Mst_Account ModifiedAccount { get; set; }
        public virtual Mst_CategoryType CategoryType { get; set; }
        public virtual Mst_InstitutionAccount InstitutionAccount { get; set; }
    }

    public class UserAssessmentDetail
    {
        [Key]
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int? TotalQuestion { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public int? MaxTotalScore { get; set; }
        public int? TotalMarksScored { get; set; }
        public bool IsCompleted { get; set; }
        public int? StatusId { get; set; }
        public DateTime? ExamStartDate { get; set; }
        public DateTime? ExamEndDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_Account User { get; set; }
        public virtual Assignment Assignment { get; set; }
        public virtual AssignmentStatus AssignmentStatus { get; set; }
    }

    public class AssignmentStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusTitle { get; set; }
        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }

    public class UserAssessment
    {
        [Key]
        public int AssessmentId { get; set; }
        public int? QuestionId { get; set; }
        [ForeignKey("QuestionOption")]
        public int? QuestionOptionId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        public int? MaxScore { get; set; }
        public int? MarkScored { get; set; }
        public bool IsFinalScore { get; set; }
        public string Answer { get; set; }
        public bool IsGradable
        {
            get { return _isgraded; }
            set { _isgraded = value; }
        }
        private bool _isgraded = false;
        [ForeignKey("GradedAccount")]
        public int? GradedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_Account User { get; set; }
        public virtual Mst_Account GradedAccount { get; set; }
        public virtual Question Question { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }
    }

    public class AssignmentHKPMapping
    {
        [Key]
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        [ForeignKey("Board")]
        public int? BoardId { get; set; }
        [ForeignKey("Medium")]
        public int? MediumId { get; set; }
        [ForeignKey("Standard")]
        public int? StandardId { get; set; }
        public string Subjects { get; set; }
        [ForeignKey("Account")]
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        private bool _isPublished = false;
        public bool IsPublished
        {
            get { return _isPublished; }
            set { _isPublished = value; }
        }
        public DateTime? PublishedDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Assignment Assignment { get; set; }
        public virtual Mst_Account Account { get; set; }
        public virtual Mst_Board Board { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual Medium Medium { get; set; }
    }


    public class AssignmentHKPStudent
    {
        [Key]
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int AssignmentHKPMappingId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public string UploadFileSrc { get; set; }
        [ForeignKey("Teacher")]
        public int? MarksBy { get; set; }
        public int? TotalMarksScored { get; set; }
        public int? MaxTotalScore { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("Account")]
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? AttemptDate { get; set; }
        public DateTime? MarkSubmittedDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Assignment Assignment { get; set; }
        public virtual Mst_Account Student { get; set; }
        public virtual Mst_Account Teacher { get; set; }
        public virtual Mst_Account Account { get; set; }
    }



    public class Notice
    {
        [Key]
        public int NoticeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Standard")]
        public int? StandardId { get; set; }
        [ForeignKey("CreatedAccount")]
        public int? CreatedBy { get; set; }
        [ForeignKey("ModifiedAccount")]
        public int? ModifiedBy { get; set; }
        public string VisibleToRole { get; set; }
        public string PDFFileSrc { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Standard Standard { get; set; }
        public virtual Mst_Account CreatedAccount { get; set; }
        public virtual Mst_Account ModifiedAccount { get; set; }
    }

}
