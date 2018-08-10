using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.Domain.Model
{
    public class ActivityViewModel
    {
        public Mst_Activities Activities { get; set; }

    }

    public class PackageRequestViewModel
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Query { get; set; }
    }

    public class InstitutionPackageContent
    {
        public string PackageType { get; set; }
        public int NoOfStudent { get; set; }
        public int NoOfTeacher { get; set; }
        public int RegisteredStudent { get; set; }
        public int RegisteredTeacher { get; set; }
        public int PendingStudent { get; set; }
        public int PendingTeacher { get; set; }
        public Mst_PrincipalDetail PrincipalDetail { get; set; }
    }

    public class LessonViewModel
    {
        public Assignment Assignment { get; set; }
        public bool? isAdmin { get; set; }
        public List<int> AssignmentTeacher { get; set; }
    }

    public class QuestionUploadViewModel
    {
        public Question Question { get; set; }
        public List<string> QuestionType { get; set; }
        public List<QuestionOption> QuestionOption { get; set; }
        public List<String> StudentAnswer { get; set; }
        public string AttemptAnswer { get; set; }
        public string MCQAnswer { get; set; }

    }

    public class MCQContent
    {
        public string Question { get; set; }
        public string MCQQuestionImageSrc { get; set; }
        public string MCQText1 { get; set; }
        public string MCQText2 { get; set; }
        public string MCQText3 { get; set; }
        public string MCQText4 { get; set; }
        public string MCQImage1Src { get; set; }
        public string MCQImage2Src { get; set; }
        public string MCQImage3Src { get; set; }
        public string MCQImage4Src { get; set; }
        public string MCQAnswer { get; set; }
    }

    public class KnowledgeViewModel
    {
        public KnowledgeBank KnowledgeBank { get; set; }
    }

    public class QuestionList
    {
        public Assignment Assignment { get; set; }
        public List<Question> Question { get; set; }
        public List<QuestionAssessmentDetail> QuestionAssessmentDetail { get; set; }
        public UserAssessmentDetail UserAssessmentDetail { get; set; }
        public List<Video> Video { get; set; }
    }

    public class QuestionAssessmentDetail
    {
        public Question Question { get; set; }
        public UserAssessment UserAssessment { get; set; }
        public string CorrectAnswer { get; set; }
        public string MatchQuestion { get; set; }
        public string QuestionOption { get; set; }
    }

    public class MCQAssessmentDetail
    {
        public UserAssessment UserAssessment { get; set; }
        public string StudentTextAnswer { get; set; }
        public string StudentImageAnswer { get; set; }
        public string TextCorrectAnswer { get; set; }
        public string ImageCorrectAnswer { get; set; }
    }

    public class StudentActivityViewModel
    {
        public UserAssessment UserAssessment { get; set; }
        public Question Question { get; set; }
        public Assignment Assignment { get; set; }
        public List<QuestionOption> QuestionOptionList { get; set; }
        public QuestionOption QuestionOption { get; set; }
        public int? TeacherMarks { get; set; }
    }

    public class StudentStandardTotal
    {
        public int Fifth { get; set; }
        public int Sixth { get; set; }
        public int Seventh { get; set; }
        public int Eight { get; set; }
        public int Nineth { get; set; }
        public int Tenth { get; set; }

    }

    public class AssignmentListContent
    {
        public List<Assignment> dataContent { get; set; }
        public int totalRecord { get; set; }

    }

    public class AssignmentHKPMappingListContent
    {
        public List<AssignmentHKPMapping> dataContent { get; set; }
        public int totalRecord { get; set; }

    }

    public class QuestionContentViewModel
    {
        public int ContentId { get; set; }
        public int? CategoryTypeId { get; set; }
        public string ContentOption { get; set; }
        public string ContentAnswer { get; set; }
        public bool Checked { get; set; }

    }


    public class AssignmentHKPListViewModel
    {
        public Assignment Assignment { get; set; }
        public AssignmentHKPMapping AssignmentHKPMapping { get; set; }
        public AssignmentHKPStudent AssignmentHKPStudent { get; set; }
    }
}
