using Praescio.API.CustomFilter;
using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using Praescio.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Praescio.BusinessEntities.Common;
using System.Data.SqlClient;

namespace Praescio.API.Controllers
{
    [AuthorizeInitializeAttribute]
    [RoutePrefix("api/InstitutionTeacher")]
    public class InstitutionTeacherController : BaseApiController
    {
        [HttpGet]
        [Route("GetTeacherStandard")]
        public HttpResponseMessage GetTeacherStandard()
        {
                PraescioContext db = new PraescioContext();

                var standard = (from s in db.StandardMapping
                                where s.TeacherId == LoggedInAccount.AccountId
                                select new { Id = s.StandardId, Text = s.Standard.StandardName }).Distinct().ToList();

                return Request.CreateResponse(HttpStatusCode.OK, standard);
        }

        [HttpGet]
        [Route("GetTeacherSubject")]
        public HttpResponseMessage GetTeacherSubject(int? standardid)
        {
                PraescioContext db = new PraescioContext();

                var subject = (from s in db.StandardMapping
                               where s.TeacherId == LoggedInAccount.AccountId && s.StandardId == standardid
                               select new { Id = s.SubjectId, Text = s.Subject.SubjectName }).Distinct().ToList();

                return Request.CreateResponse(HttpStatusCode.OK, subject);
        }

        [HttpGet]
        [Route("GetStudentByAssignment")]
        public HttpResponseMessage GetStudentByAssignment(int boardid, int standardId, int subjectId, int assignmentId, int pageNo = 1, int itemPerPage = 10)
        {
                PraescioContext db = new PraescioContext();

                var student = (from u in db.UserAssessment
                               join s in db.Account on u.UserId equals s.AccountId
                               join d in db.UserAssessmentDetail on u.Question.AssignmentId equals d.AssignmentId
                               where s.StudentStandardId == standardId && (s.BoardId == boardid || s.OrganizationAccount.BoardId == boardid) && u.Question.AssignmentId == assignmentId
                               && (d.StatusId== (int)BusinessEntities.Common.AssignmentStatus.SubmittedByStudent || d.StatusId == (int)BusinessEntities.Common.AssignmentStatus.CheckingByTeacher || d.StatusId == (int)BusinessEntities.Common.AssignmentStatus.CheckedByTeacher)
                               select s).Distinct().ToList();


                return Request.CreateResponse(HttpStatusCode.OK, new { studentList = student, TotalRecord = student.Count });
        }

        [HttpGet]
        [Route("GetStudent")]
        public HttpResponseMessage GetStudent(int standardId, int subjectId, int pageNo = 1, int itemPerPage = 10)
        {
                PraescioContext db = new PraescioContext();

                var student = (from s in db.StandardMapping
                               where s.StandardId == standardId && s.SubjectId == subjectId
                                && s.UserAccount.InstitutionAccountId == LoggedInAccount.InstitutionAccountId && s.UserAccount.AccountTypeId == (int)BusinessEntities.Common.AccountType.Student
                               select s).Distinct().ToList();


                return Request.CreateResponse(HttpStatusCode.OK, new { studentList = student, TotalRecord = student.Count });
        }


        [HttpPost]
        [Route("StudentActivityCheck")]
        public HttpResponseMessage StudentActivityCheck(List<StudentActivityViewModel> studentActivity, int questionType)
        {
                PraescioContext db = new PraescioContext();
                foreach (var activity in studentActivity)
                {
                    var userAssessment = db.UserAssessment.FirstOrDefault(x => x.AssessmentId == activity.UserAssessment.AssessmentId);
                    userAssessment.MarkScored = activity.TeacherMarks;
                    userAssessment.GradedBy = LoggedInAccount.AccountId;
                    userAssessment.ModifiedOn = DateTime.Now;
                    userAssessment.IsFinalScore = true;

                    db.Entry(userAssessment).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }

                int assignmentid = (int)studentActivity.FirstOrDefault().UserAssessment.Question.AssignmentId;
                List<int> QuestionType = new List<int>(new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });

                string sql = "SELECT COUNT(DISTINCT A.QuestionTypeId)UserAssessmentCount FROM Question A" +
                              " INNER JOIN UserAssessment B ON A.QuestionId =B.QuestionId WHERE AssignmentId=@AssignmentId AND IsFinalScore=1 AND A.QuestionTypeId IN(4, 5, 6, 7, 8, 9, 10, 11, 12, 13)";

                int UserAssessmentCount = db.Database.SqlQuery<int>(sql, new SqlParameter("@AssignmentId", assignmentid)).First();

                var studentAssessment = db.UserAssessment.Where(x => x.Question.AssignmentId == assignmentid && QuestionType.Contains((int)x.Question.QuestionTypeId));

                if (db.Question.Where(x => x.AssignmentId == assignmentid && QuestionType.Contains((int)x.QuestionTypeId)).Select(x => x.QuestionTypeId).Distinct().Count() == UserAssessmentCount)
                {
                    var userAssessment = db.UserAssessmentDetail.Where(x => x.AssignmentId == assignmentid).FirstOrDefault();
                    int totalMarks = (int)studentAssessment.Sum(x => x.MaxScore);
                    int totalMarksAchieved = (int)studentAssessment.Sum(x => x.MarkScored);

                    userAssessment.MaxTotalScore = totalMarks;
                    userAssessment.TotalMarksScored = totalMarksAchieved;
                    userAssessment.StatusId = (int)BusinessEntities.Common.AssignmentStatus.CheckedByTeacher;
                    userAssessment.ModifiedOn = DateTime.Now;

                    db.Entry(userAssessment).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Record successfully saved!!!");
        }
    }
}
