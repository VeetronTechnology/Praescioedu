using Praescio.API.CustomFilter;
using Praescio.BusinessEntities;
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
    [RoutePrefix("api/InstitutionStudent")]
    public class InstitutionStudentController : BaseApiController
    {
        [HttpGet]
        [Route("GetAssignmentQuestion")]
        public HttpResponseMessage GetAssignmentQuestion(int assignmentid, int typeOfQuestion)
        {
                PraescioContext db = new PraescioContext();

                if ((int)TypeOfQuestion.MeaningOfLesson == typeOfQuestion || (int)TypeOfQuestion.WriteShortNote == typeOfQuestion || (int)TypeOfQuestion.WriteReason == typeOfQuestion
                    || (int)TypeOfQuestion.TopicConcept == typeOfQuestion || (int)TypeOfQuestion.OneSentenceAnswer == typeOfQuestion || (int)TypeOfQuestion.DescribeBriefly == typeOfQuestion
                    || (int)TypeOfQuestion.DifferentiateBetween == typeOfQuestion)
                {
                    var question = (from q in db.Question
                                    where q.AssignmentId == assignmentid && q.QuestionTypeId == typeOfQuestion
                                    select new QuestionUploadViewModel
                                    {
                                        Question = q,
                                    }).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, question);
                }
                else if ((int)BusinessEntities.Common.TypeOfQuestion.SynonymsOfLesson == typeOfQuestion || (int)BusinessEntities.Common.TypeOfQuestion.AntonymsOfLesson == typeOfQuestion)
                {
                    var question = db.Question.Where(x => x.AssignmentId == assignmentid && x.QuestionTypeId == typeOfQuestion).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, question);
                }
                else if ((int)BusinessEntities.Common.TypeOfQuestion.MatchTheFollowing == typeOfQuestion || (int)BusinessEntities.Common.TypeOfQuestion.MCQ == typeOfQuestion || (int)BusinessEntities.Common.TypeOfQuestion.Exercise == typeOfQuestion)
                {
                    var question = (from q in db.Question
                                    where q.AssignmentId == assignmentid && q.QuestionTypeId == typeOfQuestion
                                    select new QuestionUploadViewModel
                                    {
                                        Question = q,
                                        QuestionOption = db.QuestionOption.Where(x => x.QuestionId == q.QuestionId).ToList()
                                    }).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, question);
                }
                else if ((int)BusinessEntities.Common.TypeOfQuestion.FillInTheBlanks == typeOfQuestion || (int)BusinessEntities.Common.TypeOfQuestion.TrueFalse == typeOfQuestion)
                {
                    var question = (from q in db.Question
                                    where q.AssignmentId == assignmentid && q.QuestionTypeId == typeOfQuestion
                                    select new QuestionUploadViewModel
                                    {
                                        Question = q
                                    }).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, question);
                }
                else
                {
                    var question = db.Question.Where(x => x.AssignmentId == assignmentid && x.QuestionTypeId == typeOfQuestion).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, question);
                }
          
        }

        [HttpPost]
        [Route("SaveAnswer")]
        public HttpResponseMessage SaveAnswer(List<QuestionUploadViewModel> model)
        {
                PraescioContext db = new PraescioContext();
                int TotalMarks = 0;

                if ((int)BusinessEntities.Common.TypeOfQuestion.FillInTheBlanks == model.FirstOrDefault().Question.QuestionTypeId)
                {
                    foreach (var question in model)
                    {
                        int marksObtained = 0;


                        var data = db.Question.Where(x => x.QuestionId == question.Question.QuestionId).FirstOrDefault().CorrectAnswer.Split(',').ToList();

                        foreach (var answer in question.StudentAnswer)
                        {
                            if (data.Contains(answer))
                            {
                                marksObtained = marksObtained + 1;
                                TotalMarks = TotalMarks + 1;
                            }
                            UserAssessment userActivity = new UserAssessment
                            {
                                QuestionId = question.Question.QuestionId,
                                UserId = LoggedInAccount.AccountId,
                                MaxScore = question.Question.TotalMarks,
                                Answer = string.Join(",", question.StudentAnswer),
                                CreatedOn = DateTime.Now,
                                IsFinalScore = true,
                                MarkScored = marksObtained
                            };
                            db.UserAssessment.Add(userActivity);
                            db.SaveChanges();
                        }
                    }
                }

                else if ((int)BusinessEntities.Common.TypeOfQuestion.TrueFalse == model.FirstOrDefault().Question.QuestionTypeId)
                {
                    foreach (var question in model)
                    {
                        int marksObtained = 0;


                        if (db.Question.Any(x => x.QuestionId == question.Question.QuestionId && x.CorrectAnswer.ToLower() == question.AttemptAnswer.ToLower()))
                        {
                            marksObtained = 1;
                            TotalMarks = TotalMarks + 1;
                        }

                        UserAssessment userActivity = new UserAssessment
                        {
                            QuestionId = question.Question.QuestionId,
                            UserId = LoggedInAccount.AccountId,
                            MaxScore = question.Question.TotalMarks,
                            CreatedOn = DateTime.Now,
                            Answer = question.AttemptAnswer.ToLower(),
                            IsFinalScore = true,
                            MarkScored = marksObtained
                        };
                        db.UserAssessment.Add(userActivity);
                        db.SaveChanges();
                    }
                }
                else if ((int)BusinessEntities.Common.TypeOfQuestion.MatchTheFollowing == model.FirstOrDefault().Question.QuestionTypeId)
                {
                    foreach (var question in model)
                    {
                        int marksObtained = 0;

                        foreach (var answer in question.QuestionOption)
                        {
                            if (db.QuestionOption.Any(x => x.Id == answer.Id && x.MatchAnswer.ToLower() == answer.StudentAnswer.ToLower()))
                            {
                                marksObtained = 1;
                                TotalMarks = TotalMarks + 1;
                            }

                            UserAssessment userActivity = new UserAssessment
                            {
                                QuestionId = question.Question.QuestionId,
                                QuestionOptionId = answer.Id,
                                UserId = LoggedInAccount.AccountId,
                                MaxScore = question.Question.TotalMarks,
                                Answer = answer.StudentAnswer.ToLower(),
                                CreatedOn = DateTime.Now,
                                IsFinalScore = true,
                                MarkScored = marksObtained
                            };
                            db.UserAssessment.Add(userActivity);
                            db.SaveChanges();
                        }
                    }
                }
                else if ((int)BusinessEntities.Common.TypeOfQuestion.MCQ == model.FirstOrDefault().Question.QuestionTypeId)
                {
                    foreach (var question in model)
                    {
                        int marksObtained = 0;

                        if (db.Question.Any(x => x.QuestionId == question.Question.QuestionId && x.CorrectAnswer.ToLower() == question.MCQAnswer.ToLower()))
                        {
                            marksObtained = 1;
                            TotalMarks = TotalMarks + 1;
                        }

                        UserAssessment userActivity = new UserAssessment
                        {
                            QuestionId = question.Question.QuestionId,
                            UserId = LoggedInAccount.AccountId,
                            MaxScore = question.Question.TotalMarks,
                            Answer = question.MCQAnswer.ToLower(),
                            CreatedOn = DateTime.Now,
                            IsFinalScore = true,
                            MarkScored = marksObtained
                        };
                        db.UserAssessment.Add(userActivity);
                        db.SaveChanges();
                    }
                }
                else if ((int)TypeOfQuestion.OneSentenceAnswer == model.FirstOrDefault().Question.QuestionTypeId || (int)TypeOfQuestion.DescribeBriefly == model.FirstOrDefault().Question.QuestionTypeId
                    || (int)TypeOfQuestion.DifferentiateBetween == model.FirstOrDefault().Question.QuestionTypeId || (int)TypeOfQuestion.WriteShortNote == model.FirstOrDefault().Question.QuestionTypeId
                    || (int)TypeOfQuestion.WriteReason == model.FirstOrDefault().Question.QuestionTypeId)
                {
                    foreach (var question in model)
                    {
                        UserAssessment userActivity = new UserAssessment
                        {
                            QuestionId = question.Question.QuestionId,
                            UserId = LoggedInAccount.AccountId,
                            MaxScore = question.Question.TotalMarks,
                            CreatedOn = DateTime.Now,
                            Answer = question.AttemptAnswer,
                            IsFinalScore = false,
                            IsGradable = true
                        };
                        db.UserAssessment.Add(userActivity);
                        db.SaveChanges();
                    }
                }
                else if ((int)BusinessEntities.Common.TypeOfQuestion.Exercise == model.FirstOrDefault().Question.QuestionTypeId)
                {
                    foreach (var question in model)
                    {

                        foreach (var answer in question.QuestionOption)
                        {
                            UserAssessment userActivity = new UserAssessment
                            {
                                QuestionId = question.Question.QuestionId,
                                QuestionOptionId = answer.Id,
                                UserId = LoggedInAccount.AccountId,
                                MaxScore = question.Question.TotalMarks,
                                CreatedOn = DateTime.Now,
                                Answer = answer.StudentAnswer,
                                IsFinalScore = false,
                                IsGradable = true
                            };
                            db.UserAssessment.Add(userActivity);
                            db.SaveChanges();
                        }
                    }
                }

                int assignmentid = (int)model.FirstOrDefault().Question.AssignmentId;
                List<int> QuestionType = new List<int>(new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });

                string sql = "SELECT COUNT(DISTINCT A.QuestionTypeId)UserAssessmentCount FROM Question A" +
                              " INNER JOIN UserAssessment B ON A.QuestionId =B.QuestionId WHERE AssignmentId=@AssignmentId AND B.UserId=@UserId AND A.QuestionTypeId IN(4, 5, 6, 7, 8, 9, 10, 11, 12, 13)";

                int UserAssessmentCount = db.Database.SqlQuery<int>(sql, new object[] { new SqlParameter("@AssignmentId", assignmentid), new SqlParameter("@UserId", LoggedInAccount.AccountId) }).First();

                var studentAssessment = db.UserAssessment.Where(x => x.Question.AssignmentId == assignmentid && QuestionType.Contains((int)x.Question.QuestionTypeId));

                if (db.Question.Where(x => x.AssignmentId==assignmentid && QuestionType.Contains((int)x.QuestionTypeId)).Select(x => x.QuestionTypeId).Distinct().Count() == UserAssessmentCount)
                {
                    var userAssessment = db.UserAssessmentDetail.Where(x => x.AssignmentId == assignmentid && x.UserId == LoggedInAccount.AccountId).FirstOrDefault();
                    int totalMarks = (int)studentAssessment.Sum(x => x.MaxScore);
                    int totalMarksAchieved = (int)studentAssessment.Sum(x => x.MarkScored);

                    userAssessment.MaxTotalScore = totalMarks;
                    userAssessment.TotalMarksScored = totalMarksAchieved;
                    userAssessment.StatusId = (int)BusinessEntities.Common.AssignmentStatus.CompletedByStudent;
                    //userAssessment.StatusId = (int)BusinessEntities.Common.AssignmentStatus.CheckedByTeacher;
                    userAssessment.ModifiedOn = DateTime.Now;
                    userAssessment.ExamEndDate = DateTime.Now;

                    db.UserAssessmentDetail.Attach(userAssessment);
                    db.Entry(userAssessment).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                int c = db.Question.Where(x => x.AssignmentId == assignmentid && QuestionType.Contains((int)x.QuestionTypeId)).Select(x => x.QuestionTypeId).Distinct().Count();
                return Request.CreateResponse(HttpStatusCode.OK, "assignment completed");
        }
        
    }
}
