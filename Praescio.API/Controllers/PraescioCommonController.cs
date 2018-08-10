using Praescio.Domain.DAL;
using Praescio.Domain.Entities;
using Praescio.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Praescio.Domain.Models;
using Praescio.API.CustomFilter;
using Praescio.BusinessEntities.Common;
using Praescio.BusinessEntities;

namespace Praescio.API.Controllers
{
    [AuthorizeInitializeAttribute]
    [RoutePrefix("api/PraescioCommon")]
    public class PraescioCommonController : BaseApiController
    {
        [HttpGet]
        [Route("GetActivityList")]
        public HttpResponseMessage GetActivityList(int pageNo = 1, int itemsPerPage = 20, string flag = "")
        {
            PraescioContext db = new PraescioContext();
            var content = db.Activities;
            List<ActivityViewModel> Activity = (from a in content
                                                orderby a.Id descending
                                                select new ActivityViewModel
                                                {
                                                    Activities = a
                                                }).Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, Activity);

        }

        [HttpGet]
        [Route("GetTeacherProfile")]
        public HttpResponseMessage GetTeacherProfile(int? userid)
        {
            User Teacher = new User();
            PraescioContext db = new PraescioContext();

            Teacher.UserDetail = db.Account.FirstOrDefault(x => (x.AccountId == LoggedInAccount.AccountId || x.AccountId == userid) && (x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Teacher || x.AccountTypeId == (int)BusinessEntities.Common.AccountType.IndividualTeacher));

            //Teacher.TeacherStandard = (from s in db.StandardMapping
            //                           where s.TeacherId == LoggedInAccount.AccountId || s.TeacherId == userid
            //                           group s.Subject.SubjectName by s.Standard.StandardName into g
            //                           select new TeacherStandard { StandardName = g.Key.Replace("th", ""), SubjectName = g.ToList() }).ToList();

            Teacher.TeacherStandard = (from s in db.StandardMapping
                                       where s.TeacherId == LoggedInAccount.AccountId || s.TeacherId == userid
                                       group s.Subject.SubjectName by s.Standard.StandardName into g
                                       select new TeacherStandard { StandardName = g.Key.Replace("th", ""), SubjectName = g.ToList() }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, Teacher);

        }

        [HttpGet]
        [Route("GetPackage")]
        public HttpResponseMessage GetPackage(int? userPackageRole)
        {
            PraescioContext db = new PraescioContext();
            var packageList = (from p in db.Package
                               where p.PackageRoleId == userPackageRole
                               select new { id = p.PackageId, text = p.PackageName, amount = p.PackageAmount, isActive = p.IsActive, intervalType = p.IntervalType, interval = p.Interval,
                                   standardId = p.StandardId, mediumId = p.MediumId, boardId = p.BoardId
                               }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, packageList);

        }

        [HttpGet]
        [Route("GetStudentProfile")]
        public HttpResponseMessage GetStudentProfile(int? userid)
        {

            User Teacher = new User();
            PraescioContext db = new PraescioContext();

            Teacher.UserDetail = db.Account.FirstOrDefault(x => (x.AccountId == LoggedInAccount.AccountId || x.AccountId == userid) && (x.AccountTypeId == (int)BusinessEntities.Common.AccountType.Student || x.AccountTypeId == (int)BusinessEntities.Common.AccountType.IndividualStudent));


            Teacher.TeacherStandard = (from s in db.StandardMapping
                                       where s.TeacherId == LoggedInAccount.AccountId || s.TeacherId == userid
                                       group s.Subject.SubjectName by s.Standard.StandardName into g
                                       select new TeacherStandard { StandardName = g.Key.Replace("th", ""), SubjectName = g.ToList() }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, Teacher);

        }

        [HttpGet]
        [Route("GetUserRegisterationDetail")]
        public HttpResponseMessage GetUserRegisterationDetail(int? accountId)
        {
            PraescioContext db = new PraescioContext();
            AccountDetailViewModel accountDetail = new AccountDetailViewModel();

            accountDetail.account = db.Account.FirstOrDefault(x => x.AccountId == (accountId ?? LoggedInAccount.AccountId));
            accountDetail.subject = db.StandardMapping.Where(x => x.TeacherId == accountId).Select(x => x.SubjectId).Cast<int>().ToList();


            if (accountDetail.account.AccountTypeId == (int)AccountType.IndividualTeacher || accountDetail.account.AccountTypeId == (int)AccountType.Teacher)
            {
                var subject = (from s in db.StandardMapping
                               where s.TeacherId == accountId
                               select s).ToList();

                //var standard = (from s in subject
                //                select new TeacherMappingStandard { StandardId = Convert.ToInt32(s.StandardId) }).Distinct().ToList();

                //standard.ForEach(x => x.SubjectId = subject.Select(s => s.SubjectId).Cast<int>().ToList());


                var standard = (from s in subject
                                group s.StandardId by s.StandardId into g
                                select new TeacherMappingStandard { StandardId = Convert.ToInt32(g.Key.Value) }).Distinct().ToList();

                //standard.ForEach(x => x.SubjectId = subject.Select(s => s.SubjectId).Cast<int>().ToList());
                standard.ForEach(x => x.SubjectId = subject.Where(sb => sb.StandardId == x.StandardId).Select(s => s.SubjectId).Cast<int>().ToList());


                accountDetail.teacherMappingStandard = standard;

                //PraescioContext db1 = new PraescioContext();
                //accountDetail.teacherMappingStandard = db1.StandardMapping.Where(t => t.TeacherId == accountDetail.account.AccountId).ToList();

                //accountDetail.teacherMappingStandard = (from s in db.StandardMapping
                //                           where s.TeacherId == LoggedInAccount.AccountId
                //                           group s.Subject.SubjectName by s.Standard.StandardName into g
                //                           select new TeacherStandard { StandardName = g.Key.Replace("th", ""), SubjectName = g.ToList() }).ToList();


            }

            return Request.CreateResponse(HttpStatusCode.OK, accountDetail);

        }

        [HttpGet]
        [Route("GetTeacherList")]
        public HttpResponseMessage GetTeacherList(bool isIndividual, string version = "", int institutionId = 0, int pageNo = 1, int itemPerPage = 10, string searchText = "", int IsActive = -1)
        {
            UserList Teacher = new UserList();
            PraescioContext db = new PraescioContext();
            PraescioContext db1 = new PraescioContext();

            if (isIndividual)
            {
                var account = db.Account.Where(x => ((string.IsNullOrEmpty(searchText) && x.FirstName == x.FirstName) ||
                (searchText != "" && x.FirstName.Contains(searchText)) || (searchText != "" && x.LastName.Contains(searchText)) || (searchText != "" && x.Email.Contains(searchText))
                || (searchText != "" && x.MobileNo.Contains(searchText))) &&
                x.VersionType == version && x.IsIndividual == true && x.AccountType.AccountTypeId == (int)AccountType.IndividualTeacher);
                
                if (IsActive > -1)
                {
                    bool status = Convert.ToBoolean(IsActive);
                    DateTime dtnow = DateTime.UtcNow;
                    if(status == true)
                    {
                        account = account.Where(m => m.IsActive == status && dtnow < m.ExpiredOn.Value);
                    } else
                    {
                        account = account.Where(m => m.IsActive == status || dtnow > m.ExpiredOn.Value);
                    }
                }

                Teacher.AccountDetail = account.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                Teacher.TotalRecord = account.Count();
                
                Teacher.AccountDetail.ForEach(
                 x => { x.TeacherStandardMapping = db1.StandardMapping.Where(t => t.TeacherId == x.AccountId).ToList(); }
                );

                return Request.CreateResponse(HttpStatusCode.OK, Teacher);
            }
            else
            {
                var account = db.Account.Where(x => ((string.IsNullOrEmpty(searchText) && x.FirstName == x.FirstName) ||
                (searchText != "" && x.FirstName.Contains(searchText)) || (searchText != "" && x.LastName.Contains(searchText)) || (searchText != "" && x.Email.Contains(searchText))
                || (searchText != "" && x.MobileNo.Contains(searchText))) &&
                (x.InstitutionAccountId == LoggedInAccount.InstitutionAccountId || x.InstitutionAccountId == institutionId) && x.AccountType.AccountTypeId == (int)AccountType.Teacher).ToList();
                
                if (IsActive > -1)
                {
                    bool status = Convert.ToBoolean(IsActive);
                    DateTime dtnow = DateTime.UtcNow;
                    if (status == true)
                    {
                        account = account.Where(m => m.IsActive == status && dtnow < m.ExpiredOn.Value).ToList();
                    }
                    else
                    {
                        account = account.Where(m => m.IsActive == status || dtnow > m.ExpiredOn.Value).ToList();
                    }
                }

                Teacher.AccountDetail = account.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                Teacher.TotalRecord = account.Count();
                
                Teacher.AccountDetail.ForEach(
                x => { x.TeacherStandardMapping = db1.StandardMapping.Where(t => t.TeacherId == x.AccountId).ToList(); }
                );

                return Request.CreateResponse(HttpStatusCode.OK, Teacher);
            }
        }

        [HttpGet]
        [Route("GetStudentList")]
        public HttpResponseMessage GetStudentList(bool isIndividual, string version, int institutionId = 0, int boardId = 0, int mediumId = 0, int standardId = 0, int pageNo = 1, int itemPerPage = 10, string searchText = "", int IsActive = -1)
        {
            UserList Student = new UserList();
            PraescioContext db = new PraescioContext();
            PraescioContext db1 = new PraescioContext();

            if (isIndividual)
            {
                var account = db.Account.Where(x => ((string.IsNullOrEmpty(searchText) && x.FirstName == x.FirstName) ||
                                        (searchText != "" && x.FirstName.Contains(searchText)) || (searchText != "" && x.LastName.Contains(searchText)) || (searchText != "" && x.Email.Contains(searchText))
                                        || (searchText != "" && x.MobileNo.Contains(searchText)) || (searchText != "" && x.ParentNo.Contains(searchText))) &&
                (x.VersionType == version && x.IsIndividual == true && x.AccountType.AccountTypeId == (int)AccountType.IndividualStudent));

                if (boardId > 0)
                {
                    account = account.Where(m => m.BoardId == boardId);
                }
                if (mediumId > 0)
                {
                    account = account.Where(m => m.MediumId == mediumId);
                }
                if (standardId > 0)
                {
                    account = account.Where(m => m.Standard.Id == standardId);
                }

                if (IsActive > -1)
                {
                    bool status = Convert.ToBoolean(IsActive);
                    DateTime dtnow = DateTime.UtcNow;
                    if (status == true)
                    {
                        account = account.Where(m => m.IsActive == status && dtnow < m.ExpiredOn.Value);
                    }
                    else
                    {
                        account = account.Where(m => m.IsActive == status || dtnow > m.ExpiredOn.Value);
                    }
                }

                Student.AccountDetail = account.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                Student.TotalRecord = account.Count();

                Student.AccountDetail.ForEach(
                x => { x.TeacherStandardMapping = db1.StandardMapping.Where(t => t.TeacherId == x.AccountId).ToList(); }
                );

                return Request.CreateResponse(HttpStatusCode.OK, Student);
            }
            else
            {
                var account = db.Account.Where(x => ((string.IsNullOrEmpty(searchText) && x.FirstName == x.FirstName) ||
                                        (searchText != "" && x.FirstName.Contains(searchText)) || (searchText != "" && x.LastName.Contains(searchText)) || (searchText != "" && x.Email.Contains(searchText))
                                        || (searchText != "" && x.MobileNo.Contains(searchText)) || (searchText != "" && x.ParentNo.Contains(searchText))) &&
                (x.InstitutionAccountId == LoggedInAccount.InstitutionAccountId || x.InstitutionAccountId == institutionId) && x.AccountType.AccountTypeId == (int)AccountType.Student);

                if (boardId > 0)
                {
                    account = account.Where(m => m.BoardId == boardId);
                }
                if (mediumId > 0)
                {
                    account = account.Where(m => m.MediumId == mediumId);
                }
                if (standardId > 0)
                {
                    account = account.Where(m => m.Standard.Id == standardId);
                }

                if (IsActive > -1)
                {
                    bool status = Convert.ToBoolean(IsActive);
                    DateTime dtnow = DateTime.UtcNow;
                    if (status == true)
                    {
                        account = account.Where(m => m.IsActive == status && dtnow < m.ExpiredOn.Value);
                    }
                    else
                    {
                        account = account.Where(m => m.IsActive == status || dtnow > m.ExpiredOn.Value);
                    }
                }

                Student.AccountDetail = account.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                Student.TotalRecord = account.Count();

                Student.AccountDetail.ForEach(
               x => { x.TeacherStandardMapping = db1.StandardMapping.Where(t => t.TeacherId == x.AccountId).ToList(); }
               );

                return Request.CreateResponse(HttpStatusCode.OK, Student);
            }

        }


        [HttpPost]
        [Route("AddAssignment")]
        public HttpResponseMessage AddAssignment(LessonViewModel assignmentDetail)
        {

            using (PraescioContext db = new PraescioContext())
            {
                assignmentDetail.Assignment.CreatedBy = LoggedInAccount.AccountId;
                assignmentDetail.Assignment.CreatedOn = DateTime.Now;
                db.Assignment.Add(assignmentDetail.Assignment);
                db.SaveChanges();

                if (assignmentDetail.Assignment.CreatedRole == (int)AccountType.Admin)
                {
                    foreach (var teacher in assignmentDetail.AssignmentTeacher)
                    {
                        db.AssignmentTeacherMapping.Add(new Domain.Entities.AssignmentTeacherMapping { AssignmentId = assignmentDetail.Assignment.AssignmentId, TeacherAccountId = teacher, CreatedOn = DateTime.Now, CreatedBy = assignmentDetail.Assignment.CreatedBy });
                        db.SaveChanges();
                    }
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully saved");

        }

        [HttpPost]
        [Route("SaveXMLQuestion")]
        public HttpResponseMessage SaveXMLQuestion(List<QuestionUploadViewModel> questionList, string uploadFileLocation = "", bool overrideFile = false)
        {
            PraescioContext _db = new PraescioContext();

            int assignmentId = Convert.ToInt16(questionList.FirstOrDefault().Question.AssignmentId);
            //if (_db.Assignment.Any(x => x.AssignmentId == assignmentId && !string.IsNullOrEmpty(x.UploadFileSrc) && overrideFile == false))
            if (_db.Question.Any(x => x.AssignmentId == assignmentId && overrideFile == false))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { isAlreadyExist = true });
            }
            else
            {
                var updateAssignment = _db.Assignment.Where(x => x.AssignmentId == assignmentId).FirstOrDefault();
                updateAssignment.UploadFileSrcQuestion = uploadFileLocation;
                _db.Entry(updateAssignment).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }

            if (overrideFile)
            {
                int noOfRowDeleted = _db.Database.ExecuteSqlCommand("delete from QuestionOption where QuestionId IN(SELECT QuestionId from Question where AssignmentId =" + assignmentId + " )" +
                                                                    "delete from Question where AssignmentId = " + assignmentId);
                foreach (var qts in _db.Question.Where(x => x.AssignmentId == assignmentId && x.IsActive == true).ToList())
                {
                    qts.IsActive = false;
                    qts.ModifiedOn = DateTime.Now;
                    qts.ModifiedBy = LoggedInAccount.AccountId;
                }

                _db.SaveChanges();
            }

            using (PraescioContext db = new PraescioContext())
            {
                var questionType = db.QuestionType.Where(x => x.IsActive == true).ToList();

                foreach (var questionItem in questionList)
                {
                    questionItem.Question.TotalMarks = Convert.ToInt16(questionType.FirstOrDefault(x => x.QuestionTypeId == questionItem.Question.QuestionTypeId).TotalMarks);

                    questionItem.Question.CreatedBy = LoggedInAccount.AccountId;
                    questionItem.Question.CreatedOn = DateTime.Now;
                    db.Question.Add(questionItem.Question);
                    db.SaveChanges();

                    if (questionItem.Question.QuestionTypeId == (int)TypeOfQuestion.MatchTheFollowing)
                    {
                        foreach (var option in questionItem.QuestionOption)
                        {
                            option.CreatedOn = DateTime.Now;
                            option.QuestionId = questionItem.Question.QuestionId;
                            db.QuestionOption.Add(option);
                        }
                        db.SaveChanges();
                    }
                    else if (questionItem.Question.QuestionTypeId == (int)TypeOfQuestion.MCQ)
                    {
                        foreach (var option in questionItem.QuestionOption)
                        {
                            option.CreatedOn = DateTime.Now;
                            option.QuestionId = questionItem.Question.QuestionId;
                            db.QuestionOption.Add(option);
                        }
                        db.SaveChanges();
                    }

                    else if (questionItem.Question.QuestionTypeId == (int)TypeOfQuestion.DifferentiateBetween)
                    {
                        foreach (var option in questionItem.QuestionOption)
                        {
                            option.CreatedOn = DateTime.Now;
                            option.QuestionId = questionItem.Question.QuestionId;
                            db.QuestionOption.Add(option);
                        }
                        db.SaveChanges();
                    }
                    else if (questionItem.Question.QuestionTypeId == (int)TypeOfQuestion.Exercise)
                    {
                        foreach (var option in questionItem.QuestionOption)
                        {
                            option.CreatedOn = DateTime.Now;
                            option.QuestionId = questionItem.Question.QuestionId;
                            db.QuestionOption.Add(option);
                        }
                        db.SaveChanges();
                    }

                }
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully loaded");

        }

        [HttpPost]
        [Route("SaveMCQ")]
        public HttpResponseMessage SaveMCQ(List<MCQContent> MCQContent, int assignmentId)
        {
            using (PraescioContext db = new PraescioContext())
            {
                int TotalMarks = (int)db.QuestionType.FirstOrDefault(x => x.QuestionTypeId == (int)BusinessEntities.Common.TypeOfQuestion.MCQ).TotalMarks;
                foreach (var mcq in MCQContent)
                {
                    Question option = new Question
                    {
                        AssignmentId = assignmentId,
                        Title = mcq.Question,
                        QuestionTypeId = (int)BusinessEntities.Common.TypeOfQuestion.MCQ,
                        MCQQuestionImageSrc = mcq.MCQQuestionImageSrc,
                        MCQText1 = mcq.MCQText1,
                        MCQText2 = mcq.MCQText2,
                        MCQText3 = mcq.MCQText3,
                        MCQText4 = mcq.MCQText4,
                        MCQImage1Src = mcq.MCQImage1Src,
                        MCQImage2Src = mcq.MCQImage2Src,
                        MCQImage3Src = mcq.MCQImage3Src,
                        MCQImage4Src = mcq.MCQImage4Src,
                        CorrectAnswer = mcq.MCQAnswer,
                        TotalMarks = TotalMarks,
                        CreatedBy = LoggedInAccount.AccountId,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    };
                    db.Question.Add(option);
                }

                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully saved");

        }
        
        [HttpPost]
        [Route("SaveMCQV2")]
        public HttpResponseMessage SaveMCQV2(List<Question> questionList, int assignmentId)
        {
            using (PraescioContext db = new PraescioContext())
            {
                var questionType = db.QuestionType.Where(x => x.IsActive == true).ToList();

                foreach (var question in questionList)
                {
                    question.QuestionTypeId = (int)TypeOfQuestion.MCQ;
                    question.TotalMarks = Convert.ToInt16(questionType.FirstOrDefault(x => x.QuestionTypeId == question.QuestionTypeId).TotalMarks);

                    question.CreatedBy = LoggedInAccount.AccountId;
                    question.CreatedOn = DateTime.Now;
                    db.Question.Add(question);
                    db.SaveChanges();

                    if (question.QuestionTypeId == (int)TypeOfQuestion.MCQ)
                    {
                        foreach (var option in question.QuestionOption)
                        {
                            option.CreatedOn = DateTime.Now;
                            option.QuestionId = question.QuestionId;
                            db.QuestionOption.Add(option);
                        }
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully saved");
        }

        [HttpPost]
        [Route("SaveVideo")]
        public HttpResponseMessage SaveVideo(Video video, int assignmentId)
        {
            using (PraescioContext db = new PraescioContext())
            {
                video.AssignmentId = assignmentId;
                video.CreatedBy = LoggedInAccount.AccountId;
                video.CreatedOn = DateTime.Now;
                db.Video.Add(video);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully saved");
        }

        [HttpPost]
        [Route("SaveCreativity")]
        public HttpResponseMessage SaveCreativity(Creativity creativity)
        {
            try
            {
                using (PraescioContext db = new PraescioContext())
                {
                    creativity.CreatedBy = LoggedInAccount.AccountId;
                    creativity.CreatedOn = DateTime.Now;
                    db.Creativity.Add(creativity);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Creativity saved successfully!" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Creativity update failed!" });
                //throw;
            }
        }


        [HttpPost]
        [Route("DeleteCreativity")]
        public HttpResponseMessage DeleteCreativity(Creativity creativity)
        {
            try
            {
                using (PraescioContext db = new PraescioContext())
                {
                    Creativity model = db.Creativity.Where(m => m.CreativityId == creativity.CreativityId).FirstOrDefault();
                    model.ModifiedBy = LoggedInAccount.AccountId;
                    model.ModifiedOn = DateTime.Now;
                    model.IsActive = false;

                    db.Creativity.Attach(model);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Creativity deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Creativity delete failed!" });
                //throw;
            }
        }


        [HttpPost]
        [Route("DeleteCreativityComment")]
        public HttpResponseMessage DeleteCreativityComment(CreativityComment creativityComment)
        {
            try
            {
                using (PraescioContext db = new PraescioContext())
                {
                    CreativityComment model = db.CreativityComment.Where(m => m.CreativityCommentId == creativityComment.CreativityCommentId).FirstOrDefault();
                    model.ModifiedBy = LoggedInAccount.AccountId;
                    model.ModifiedOn = DateTime.Now;
                    model.IsActive = false;

                    db.CreativityComment.Attach(model);
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Creativity comment deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Creativity comment delete failed!" });
                //throw;
            }
        }

        [HttpGet]
        [Route("GetCreativityList")]
        public HttpResponseMessage GetCreativityList(int pageNo = 1, int itemPerPage = 10, string searchText = "", int StandardId = 0, int SubjectId = 0)
        {
            PraescioContext db = new PraescioContext();

            //List<CreativityWithComment> creativities = new List<CreativityWithComment>();
            List<Creativity> creativities = new List<Creativity>();
            //var creativities = new System.Dynamic.ExpandoObject();

            if (LoggedInAccount.AccountTypeId == (int)AccountType.Student || LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent)
            {
                creativities = (from crt in db.Creativity
                                where
                                ((string.IsNullOrEmpty(searchText) && crt.Title == crt.Title) || (searchText != "" && crt.Title.Contains(searchText)))
                                && crt.IsActive == true && crt.AccountId == LoggedInAccount.AccountId
                                select crt).Distinct().ToList();
            } else
            {
                creativities = (from crt in db.Creativity
                                where
                                ((string.IsNullOrEmpty(searchText) && crt.Title == crt.Title) || (searchText != "" && crt.Title.Contains(searchText)))
                                && crt.IsActive == true
                                select crt).Distinct().ToList();

                //creativities = (from crt in db.Creativity
                //                join cmts in db.CreativityComment on crt.CreativityId equals cmts.CreativityId into cmtsall
                //                from cmt in cmtsall.DefaultIfEmpty()
                //                where
                //                ((string.IsNullOrEmpty(searchText) && crt.Title == crt.Title) || (searchText != "" && crt.Title.Contains(searchText)))
                //                && crt.IsActive == true && cmt.AccountId == LoggedInAccount.AccountId
                //                select new { Creativity = crt, Comments = cmtsall.ToList()}).Distinct().ToList();
            }

            //creativities.ForEach(
            //x =>
            //{
            //    x.Comments = db.CreativityComment.Where(u => u.CreativityId == x.CreativityId && u.AccountId == LoggedInAccount.AccountId).ToList();
            //    //x.IsCurrentUserCommented = db.CreativityComment.Count(u => u.CreativityId == x.CreativityId && u.AccountId == LoggedInAccount.AccountId) > 0;
            //}
            //);

            //for(int i=0;i< creativities.Count; i++)
            //{
            //    creativities[i].Comments = db.CreativityComment.Where(u => u.CreativityId == creativities[i].CreativityId && u.AccountId == LoggedInAccount.AccountId).ToList();
            //}

            if (StandardId > 0)
            {
                creativities = creativities.Where(m => m.StandardId == StandardId).ToList();
            }
            if (SubjectId > 0)
            {
                creativities = creativities.Where(m => m.SubjectId == SubjectId).ToList();
            }
            int totalRecords = creativities.Count;
            creativities = creativities.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = creativities, totalRecord = totalRecords });
        }

        [HttpGet]
        [Route("GetCreativityCommentList")]
        public HttpResponseMessage GetCreativityCommentList(int CreativityId, int pageNo = 1, int itemPerPage = 10, string searchText = "")
        {
            PraescioContext db = new PraescioContext();

            Creativity creativity = db.Creativity.Where(m => m.CreativityId == CreativityId).FirstOrDefault();
            var modelList = (from crt in db.CreativityComment
                             where
                             ((string.IsNullOrEmpty(searchText) && crt.Description == crt.Description) || (searchText != "" && crt.Description.Contains(searchText)))
                             && crt.IsActive == true && crt.CreativityId == CreativityId
                             select crt).Distinct().ToList();
            
            int totalRecords = modelList.Count;
            modelList = modelList.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();

            creativity.Comments = modelList;

            return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = creativity, totalRecord = totalRecords });
        }

        [HttpPost]
        [Route("SaveCreativityComment")]
        public HttpResponseMessage SaveCreativityComment(CreativityComment creativityComment)
        {
            try
            {
                using (PraescioContext db = new PraescioContext())
                {
                    creativityComment.CreatedOn = DateTime.Now;
                    db.CreativityComment.Add(creativityComment);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Comment saved successfully!" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Comment update failed!" });
                //throw;
            }
        }

        [HttpGet]
        [Route("GetAssignmentListDropDown")]
        public HttpResponseMessage GetAssignmentListDropDown(int pageNo = 1, int itemPerPage = 10, int assignmentType = 0)
        {
            List<Assignment> assignmentList = new List<Assignment>();
            PraescioContext db = new PraescioContext();

            if ((LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assignmentType == (int)AssignmentType.PraescioLesson)
            {
                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assign.Account.InstitutionAccountId == 1
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true
                                        //&& (searchText!="" && assign.Title.Contains(searchText))
                                        //&& (assign.Title.Trim().ToLower().Contains(searchText.Trim() == ""? assign.Title.ToLower().Trim():searchText.ToLower().Trim()) || assign.Description.ToLower().Trim() == searchText.ToLower().Trim())
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });

            }
            else if (LoggedInAccount.AccountTypeId == (int)AccountType.Student && assignmentType == (int)AssignmentType.InstitutionAssignment)
            {
                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        assign.StandardId == LoggedInAccount.StudentStandardId && assign.Account.InstitutionAccountId == LoggedInAccount.InstitutionAccountId
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.Student || LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) && (assignmentType == (int)AssignmentType.PraescioLesson || assignmentType == (int)AssignmentType.PExtra))
            {
                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assign.Account.InstitutionAccountId == 1
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) && assignmentType == (int)AssignmentType.IndividualAssignment)
            {
                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assign.Account.InstitutionAccountId == 1
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) && assignmentType == (int)AssignmentType.All)
            {
                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent || LoggedInAccount.AccountTypeId == (int)AccountType.Student)
                                            //&& assign.Account.InstitutionAccountId == 1
                                            //&& assign.AssignmentTypeId == assignmentType 
                                        && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else
            {
                var content = (from assign in db.Assignment
                               join teacher in db.AssignmentTeacherMapping on assign.AssignmentId equals teacher.AssignmentId
                               into assignment
                               from rt in assignment.DefaultIfEmpty()
                               where
                               rt.TeacherAccountId == LoggedInAccount.AccountId || assign.CreatedBy == LoggedInAccount.AccountId
                               && assign.AssignmentTypeId == assignmentType && assign.IsActive == true
                               select assign).Distinct().ToList();

                assignmentList = content.ToList();

                AssignmentListContent data = new AssignmentListContent();
                data.dataContent = assignmentList;
                data.totalRecord = content.Count();


                return Request.CreateResponse(HttpStatusCode.OK, data);
            }

        }

        [HttpGet]
        [Route("GetAssignmentList")]
        public HttpResponseMessage GetAssignmentList(int pageNo = 1, int itemPerPage = 10, int assignmentType = 0, string searchText = "", Boolean HidePublished = false)
        {
            List<Assignment> assignmentList = new List<Assignment>();
            PraescioContext db = new PraescioContext();

            if ((LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assignmentType == (int)AssignmentType.PraescioLesson)
            {
                var schoolAssignment = (from assign in db.Assignment where
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assign.Account.InstitutionAccountId == 1
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true
                                        select assign).Distinct().ToList();

                if (HidePublished)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.IsPublished == false).ToList();
                }
                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });

            }
            else if (LoggedInAccount.AccountTypeId == (int)AccountType.Student && assignmentType == (int)AssignmentType.InstitutionAssignment)
            {
                var schoolAssignment = (from assign in db.Assignment where 
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        assign.StandardId == LoggedInAccount.StudentStandardId && assign.Account.InstitutionAccountId == LoggedInAccount.InstitutionAccountId
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.Student || LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) && (assignmentType == (int)AssignmentType.PraescioLesson || assignmentType == (int)AssignmentType.PExtra))
            {
                var schoolAssignment = (from assign in db.Assignment where
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assign.Account.InstitutionAccountId == 1
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) && assignmentType == (int)AssignmentType.IndividualAssignment)
            {
                var schoolAssignment = (from assign in db.Assignment where
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assign.Account.InstitutionAccountId == 1
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) && assignmentType == (int)AssignmentType.All)
            {
                var schoolAssignment = (from assign in db.Assignment where
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent || LoggedInAccount.AccountTypeId == (int)AccountType.Student)
                                        && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else
            {
                var content = (from assign in db.Assignment
                               join teacher in db.AssignmentTeacherMapping on assign.AssignmentId equals teacher.AssignmentId
                               into assignment
                               from rt in assignment.DefaultIfEmpty()
                               where ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                               (searchText != "" && assign.Title.Contains(searchText))) &&
                               rt.TeacherAccountId == LoggedInAccount.AccountId || assign.CreatedBy == LoggedInAccount.AccountId
                               && assign.AssignmentTypeId == assignmentType && assign.IsActive == true
                               select assign).Distinct().ToList();

                assignmentList = content.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();

                AssignmentListContent data = new AssignmentListContent();
                data.dataContent = assignmentList;
                data.totalRecord = content.Count();


                return Request.CreateResponse(HttpStatusCode.OK, data);
            }

        }

        [HttpGet]
        [Route("GetAssignmentListV2")]
        public HttpResponseMessage GetAssignmentListV2(int pageNo = 1, int itemPerPage = 10, int assignmentType = 0, string searchText = "", Boolean HidePublished = false, int BoardId = 0, int MediumId = 0, int StandardId = 0, int SubjectId = 0, bool AttemptedOnly = false)
        {
            List<Assignment> assignmentList = new List<Assignment>();
            PraescioContext db = new PraescioContext();

            if ((LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) 
                && assignmentType == (int)AssignmentType.PraescioLesson)
            {
                var schoolAssignment = 
                    (from assign in db.Assignment where
                    ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) || (searchText != "" && assign.Title.Contains(searchText))) 
                    && (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) 
                    && assign.Account.InstitutionAccountId == 1 && assign.AssignmentTypeId == assignmentType && assign.IsActive == true
                    select assign).Distinct().ToList();

                if (HidePublished)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.IsPublished == false).ToList();
                }
                if(BoardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.BoardId == BoardId).ToList();
                }
                if (MediumId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.MediumId == MediumId).ToList();
                }
                if (StandardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.StandardId == StandardId).ToList();
                }
                if (SubjectId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.SubjectId == SubjectId).ToList();
                }
                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });

            } else if (LoggedInAccount.AccountTypeId == (int)AccountType.Student 
                && assignmentType == (int)AssignmentType.InstitutionAssignment)
            {
                var schoolAssignment = (from assign in db.Assignment
                                       where
                                       ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                       (searchText != "" && assign.Title.Contains(searchText))) &&
                                       assign.StandardId == LoggedInAccount.StudentStandardId && assign.Account.InstitutionAccountId == LoggedInAccount.InstitutionAccountId
                                       && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                       select assign).Distinct().ToList();

                if (HidePublished)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.IsPublished == false).ToList();
                }
                if (BoardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.BoardId == BoardId).ToList();
                }
                if (MediumId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.MediumId == MediumId).ToList();
                }
                if (StandardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.StandardId == StandardId).ToList();
                }
                if (SubjectId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.SubjectId == SubjectId).ToList();
                }
                if(LoggedInAccount.VersionType == null || LoggedInAccount.VersionType.ToLower() == "free" || LoggedInAccount.VersionType.ToLower() == "trial")
                {
                    schoolAssignment = schoolAssignment.Take(2).ToList();
                }
                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            } else if ((LoggedInAccount.AccountTypeId == (int)AccountType.Student 
                || LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) 
                && (assignmentType == (int)AssignmentType.PraescioLesson || assignmentType == (int)AssignmentType.PExtra))
            {
                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assign.Account.InstitutionAccountId == 1
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                if (HidePublished)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.IsPublished == false).ToList();
                }
                if (BoardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.BoardId == BoardId).ToList();
                }
                if (MediumId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.MediumId == MediumId).ToList();
                }
                if (StandardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.StandardId == StandardId).ToList();
                }
                if (SubjectId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.SubjectId == SubjectId).ToList();
                }

                if (LoggedInAccount.VersionType == null || LoggedInAccount.VersionType.ToLower() == "free" || LoggedInAccount.VersionType.ToLower() == "trial")
                {
                    schoolAssignment = schoolAssignment.Take(2).ToList();
                }
                if (AttemptedOnly)
                {
                    var userAssessmentDetail = db.UserAssessmentDetail.Where(q => q.UserId == LoggedInAccount.AccountId && q.IsCompleted == true).ToList();
                    schoolAssignment = schoolAssignment.Where(x1 => userAssessmentDetail.Any(x2 => x2.AssignmentId == x1.AssignmentId)).ToList();
                }
                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) 
                && assignmentType == (int)AssignmentType.IndividualAssignment)
            {
                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.SuperAdmin) && assign.Account.InstitutionAccountId == 1
                                        && assign.AssignmentTypeId == assignmentType && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                if (HidePublished)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.IsPublished == false).ToList();
                }
                if (BoardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.BoardId == BoardId).ToList();
                }
                if (MediumId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.MediumId == MediumId).ToList();
                }
                if (StandardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.StandardId == StandardId).ToList();
                }
                if (SubjectId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.SubjectId == SubjectId).ToList();
                }

                if (LoggedInAccount.VersionType == null || LoggedInAccount.VersionType.ToLower() == "free" || LoggedInAccount.VersionType.ToLower() == "trial")
                {
                    schoolAssignment = schoolAssignment.Take(2).ToList();
                }

                if (AttemptedOnly)
                {
                    var userAssessmentDetail = db.UserAssessmentDetail.Where(q => q.UserId == LoggedInAccount.AccountId && q.IsCompleted == true).ToList();
                    schoolAssignment = schoolAssignment.Where(x1 => userAssessmentDetail.Any(x2 => x2.AssignmentId == x1.AssignmentId)).ToList();
                }
                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent) 
                && assignmentType == (int)AssignmentType.All)
            {
                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        (assign.StandardId == LoggedInAccount.StudentStandardId || LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent || LoggedInAccount.AccountTypeId == (int)AccountType.Student)
                                        && assign.IsActive == true && assign.IsPublished
                                        select assign).Distinct().ToList();

                if (HidePublished)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.IsPublished == false).ToList();
                }
                if (BoardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.BoardId == BoardId).ToList();
                }
                if (MediumId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.MediumId == MediumId).ToList();
                }
                if (StandardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.StandardId == StandardId).ToList();
                }
                if (SubjectId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.SubjectId == SubjectId).ToList();
                }

                if (LoggedInAccount.VersionType == null || LoggedInAccount.VersionType.ToLower() == "free" || LoggedInAccount.VersionType.ToLower() == "trial")
                {
                    schoolAssignment = schoolAssignment.Take(2).ToList();
                }
                if (AttemptedOnly)
                {
                    var userAssessmentDetail = db.UserAssessmentDetail.Where(q => q.UserId == LoggedInAccount.AccountId && q.IsCompleted == true).ToList();
                    schoolAssignment = schoolAssignment.Where(x1 => userAssessmentDetail.Any(x2 => x2.AssignmentId == x1.AssignmentId)).ToList();
                }
                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else if ((LoggedInAccount.AccountTypeId == (int)AccountType.StudentParent))
            {
                var studentAccount = db.Account.Where(t => (t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.Student || t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent) && t.ParentEmail == LoggedInAccount.Email).FirstOrDefault();

                var schoolAssignment = (from assign in db.Assignment
                                        where
                                        ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                                        (searchText != "" && assign.Title.Contains(searchText))) &&
                                        (assign.StandardId == studentAccount.StudentStandardId || studentAccount.AccountTypeId == (int)AccountType.IndividualStudent || studentAccount.AccountTypeId == (int)AccountType.Student)
                                        && assign.IsActive == true && assign.IsPublished //&& assign.AccountId == studentAccount.AccountId
                                        select assign).Distinct().ToList();
                
                if (HidePublished)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.IsPublished == false).ToList();
                }
                if (BoardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.BoardId == BoardId).ToList();
                }
                if (MediumId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.MediumId == MediumId).ToList();
                }
                if (StandardId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.StandardId == StandardId).ToList();
                }
                if (SubjectId > 0)
                {
                    schoolAssignment = schoolAssignment.Where(m => m.SubjectId == SubjectId).ToList();
                }

                if (LoggedInAccount.VersionType == null || LoggedInAccount.VersionType.ToLower() == "free" || LoggedInAccount.VersionType.ToLower() == "trial")
                {
                    schoolAssignment = schoolAssignment.Take(2).ToList();
                }
                if (AttemptedOnly)
                {
                    var userAssessmentDetail = db.UserAssessmentDetail.Where(q => q.UserId == studentAccount.AccountId && q.IsCompleted == true).ToList();
                    schoolAssignment = schoolAssignment.Where(x1 => userAssessmentDetail.Any(x2 => x2.AssignmentId == x1.AssignmentId)).ToList();
                }
                assignmentList = schoolAssignment.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = assignmentList, totalRecord = schoolAssignment.Count });
            }
            else
            {
                var content = (from assign in db.Assignment
                               join teacher in db.AssignmentTeacherMapping on assign.AssignmentId equals teacher.AssignmentId
                               into assignment
                               from rt in assignment.DefaultIfEmpty()
                               where ((string.IsNullOrEmpty(searchText) && assign.Title == assign.Title) ||
                               (searchText != "" && assign.Title.Contains(searchText))) &&
                               (rt.TeacherAccountId == LoggedInAccount.AccountId || assign.CreatedBy == LoggedInAccount.AccountId)
                               && assign.AssignmentTypeId == assignmentType && assign.IsActive == true
                               select assign).Distinct().ToList();


                if (HidePublished)
                {
                    content = content.Where(m => m.IsPublished == false).ToList();
                }
                if (BoardId > 0)
                {
                    content = content.Where(m => m.BoardId == BoardId).ToList();
                }
                if (MediumId > 0)
                {
                    content = content.Where(m => m.MediumId == MediumId).ToList();
                }
                if (StandardId > 0)
                {
                    content = content.Where(m => m.StandardId == StandardId).ToList();
                }
                if (SubjectId > 0)
                {
                    content = content.Where(m => m.SubjectId == SubjectId).ToList();
                }

                if ((LoggedInAccount.AccountTypeId == (int)AccountType.IndividualTeacher) && (LoggedInAccount.VersionType == null || LoggedInAccount.VersionType.ToLower() == "free" || LoggedInAccount.VersionType.ToLower() == "trial"))
                {
                    content = content.Take(2).ToList();
                }

                assignmentList = content.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();

                AssignmentListContent data = new AssignmentListContent();
                data.dataContent = assignmentList;
                data.totalRecord = content.Count();


                return Request.CreateResponse(HttpStatusCode.OK, data);
            }

        }

        [HttpGet]
        [Route("GetAssignmentListHKP")]
        public HttpResponseMessage GetAssignmentListHKP(int pageNo = 1, int itemPerPage = 10, int assignmentType = 0, string searchText = "", Boolean HidePublished = false, int BoardId = 0, int MediumId = 0, int StandardId = 0, int SubjectId = 0)
        {
            //List<Assignment> assignmentList = new List<Assignment>();
            PraescioContext db = new PraescioContext();

            try
            {

                var assignmentHKPList = (from assignHKP in db.AssignmentHKPMapping
                                         join aq in db.Assignment on assignHKP.AssignmentId equals aq.AssignmentId
                                         where aq.AssignmentTypeId == assignmentType && assignHKP.IsActive == true && assignHKP.IsPublished == true
                                         select assignHKP).ToList();

                if (BoardId > 0)
                {
                    assignmentHKPList = assignmentHKPList.Where(m => m.BoardId == BoardId).ToList();
                }
                if (MediumId > 0)
                {
                    assignmentHKPList = assignmentHKPList.Where(m => m.MediumId == MediumId).ToList();
                }
                if (StandardId > 0)
                {
                    assignmentHKPList = assignmentHKPList.Where(m => m.StandardId == StandardId).ToList();
                }
                //if (SubjectId > 0)
                //{
                //    assignmentHKPList = assignmentHKPList.Where(m => m.SubjectId == SubjectId).ToList();
                //}

                /*assignmentList = (from assign in db.Assignment
                                  where assign.AssignmentId == assignmentHKPList.Where(x => x.AssignmentId == assign.AssignmentId).FirstOrDefault().AssignmentId
                                  select assign).Distinct().ToList();*/

                /*assignmentList = (from assign in db.Assignment
                                  where assignmentHKPList.Any(x => x.AssignmentId == assign.AssignmentId)
                                  select assign).Distinct().ToList();*/

                var assignmentList = (from assign in db.Assignment
                                  select assign).ToList();

                assignmentList = assignmentList.Where(x1 => assignmentHKPList.Any(x2 => x2.AssignmentId == x1.AssignmentId)).ToList();

                int totalRecords = assignmentList.Count;
                assignmentList = assignmentList.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();

                List<AssignmentHKPListViewModel> result = new List<AssignmentHKPListViewModel>();
                foreach (var item in assignmentHKPList)
                {
                    var assign = assignmentList.Where(x1 => x1.AssignmentId == item.AssignmentId).FirstOrDefault();
                    if(assign != null)
                    {
                        AssignmentHKPListViewModel ashkp = new AssignmentHKPListViewModel();
                        ashkp.Assignment = assign;
                        ashkp.AssignmentHKPMapping = item;

                        if(LoggedInAccount.AccountTypeId == (int)AccountType.Student || LoggedInAccount.AccountTypeId == (int)AccountType.IndividualStudent)
                        {
                            ashkp.AssignmentHKPStudent = db.AssignmentHKPStudent.Where(m => m.AssignmentId == assign.AssignmentId && m.StudentId == LoggedInAccount.AccountId).FirstOrDefault();
                        }
                        
                        result.Add(ashkp);
                    }
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = result, totalRecord = totalRecords });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetAssignmentHKP")]
        public HttpResponseMessage GetAssignmentHKP(int assignmentId, int userId)
        {
            PraescioContext db = new PraescioContext();

            try
            {
                var result = (from u in db.AssignmentHKPStudent
                               join s in db.Account on u.StudentId equals s.AccountId
                               join n in db.Assignment on u.AssignmentId equals n.AssignmentId
                               where //s.StudentStandardId == standardId && (s.BoardId == boardid || s.OrganizationAccount.BoardId == boardid) && 
                               u.AssignmentId == assignmentId && u.StudentId == userId
                               && (u.StatusId == (int)BusinessEntities.Common.AssignmentStatus.SubmittedByStudent || u.StatusId == (int)BusinessEntities.Common.AssignmentStatus.CheckingByTeacher || u.StatusId == (int)BusinessEntities.Common.AssignmentStatus.CheckedByTeacher)
                               select new { Student = s, AssignmentHKPStudent = u, Assignment = n }).Distinct().FirstOrDefault();
                
                return Request.CreateResponse(HttpStatusCode.OK, new { dataContent = result });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("AssignmentHKPAttempt")]
        public HttpResponseMessage AssignmentHKPAttempt(AssignmentHKPStudent assignmentDetail)
        {
            using (PraescioContext db = new PraescioContext())
            {
                var assignment = db.AssignmentHKPStudent.FirstOrDefault(a => a.AssignmentId == assignmentDetail.AssignmentId && a.StudentId == assignmentDetail.StudentId);
                if(assignment == null)
                {
                    assignmentDetail.StatusId = (int)BusinessEntities.Common.AssignmentStatus.SubmittedByStudent;
                    assignmentDetail.CreatedBy = LoggedInAccount.AccountId;
                    assignmentDetail.CreatedOn = DateTime.Now;
                    db.AssignmentHKPStudent.Add(assignmentDetail);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Assignment attempted successfully!" });
                } else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Assignment already attempted!" });
                }
            }
        }

        [HttpPost]
        [Route("AssignmentHKPCheck")]
        public HttpResponseMessage AssignmentHKPCheck(AssignmentHKPStudent assignmentDetail)
        {
            using (PraescioContext db = new PraescioContext())
            {
                var assignment = db.AssignmentHKPStudent.FirstOrDefault(a => a.Id == assignmentDetail.Id);
                if (assignment != null)
                {
                    assignment.TotalMarksScored = assignmentDetail.TotalMarksScored;
                    assignment.StatusId = (int)BusinessEntities.Common.AssignmentStatus.CheckedByTeacher;
                    assignment.MarksBy = LoggedInAccount.AccountId;
                    assignment.ModifiedBy = LoggedInAccount.AccountId;
                    assignment.ModifiedOn = DateTime.Now;
                    db.AssignmentHKPStudent.Attach(assignment);
                    db.Entry(assignment).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Assignment attempted successfully!" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Assignment already attempted!" });
                }
            }
        }

        [HttpGet]
        [Route("GetAssignmentDetail")]
        public HttpResponseMessage GetAssignmentDetail(int assignmentid)
        {
            PraescioContext db = new PraescioContext();
            var assignment = db.Assignment.Where(x => x.AssignmentId == assignmentid).FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, new { Assignment = assignment });

        }

        [HttpGet]
        [Route("GetAssignmentVideos")]
        public HttpResponseMessage GetAssignmentVideos(int assignmentid)
        {
            PraescioContext db = new PraescioContext();
            var items = db.Video.Where(x => x.AssignmentId == assignmentid).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, new { Videos = items });

        }

        [HttpGet]
        [Route("GetAssignmentTypeList")]
        public HttpResponseMessage GetAssignmentTypeList()
        {
            PraescioContext db = new PraescioContext();
            var assignmentType = db.AssignmentType.Where(x => x.IsActive == true).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, assignmentType);

        }

        [HttpPost]
        [Route("PublishAssignment")]
        public HttpResponseMessage PublishAssignment(AssignmentListContent data)
        {
            PraescioContext db = new PraescioContext();
            foreach (var assignment in data.dataContent)
            {
                var _assignment = db.Assignment.FirstOrDefault(x => x.AssignmentId == assignment.AssignmentId);

                _assignment.ModifiedOn = DateTime.Now;
                _assignment.ModifiedBy = LoggedInAccount.AccountId;
                if (assignment.IsPublished && !_assignment.IsPublished)
                {
                    _assignment.PublishedDate = DateTime.Now;
                }
                _assignment.IsPublished = assignment.IsPublished;
                db.Entry(_assignment).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "success");

        }

        [HttpPost]
        [Route("PublishAssignmentHKP")]
        public HttpResponseMessage PublishAssignmentHKP(AssignmentHKPMappingListContent data)
        {
            PraescioContext db = new PraescioContext();
            foreach (var assignment in data.dataContent)
            {
                var _assignment = db.AssignmentHKPMapping.FirstOrDefault(x => x.AssignmentId == assignment.AssignmentId && x.BoardId == assignment.BoardId && x.MediumId == assignment.MediumId && x.StandardId == assignment.StandardId);
                if(_assignment == null)
                {
                    assignment.CreatedOn = DateTime.Now;
                    assignment.CreatedBy = LoggedInAccount.AccountId;

                    db.AssignmentHKPMapping.Add(assignment);

                    var assign = db.Assignment.Where(x => x.AssignmentId == assignment.AssignmentId).FirstOrDefault();
                    if(assign.IsPublished == false)
                    {
                        assign.IsPublished = true;
                        db.Entry(assign);
                    }                    
                }
            }
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpGet]
        [Route("GetQuestionByAssignmentId")]
        public HttpResponseMessage GetQuestionByAssignmentId(int assignmentId, int userId = 0)
        {
            QuestionList assignmnetQuestion = new QuestionList();
            PraescioContext db = new PraescioContext();
            if(userId == 0)
            {
                userId = LoggedInAccount.AccountId;
            }
            assignmnetQuestion.Assignment = db.Assignment.Where(q => q.AssignmentId == assignmentId).FirstOrDefault();
            assignmnetQuestion.UserAssessmentDetail = db.UserAssessmentDetail.Where(q => q.AssignmentId == assignmentId && q.UserId == userId).FirstOrDefault();
            assignmnetQuestion.Question = db.Question.Where(q => q.AssignmentId == assignmentId && q.IsActive == true).ToList();
            assignmnetQuestion.QuestionAssessmentDetail = (from q in db.Question
                                                           join u in db.UserAssessment on q.QuestionId equals u.QuestionId into t
                                                           where q.IsActive == true && q.AssignmentId == assignmentId
                                                           from rt in t.DefaultIfEmpty()
                                                           where rt.UserId == userId
                                                           select new QuestionAssessmentDetail
                                                           {
                                                               Question = q,
                                                               UserAssessment = rt
                                                           }
                                                           )//.Where(x => x.UserAssessment.UserId == LoggedInAccount.AccountId)
                                                           .ToList();

            assignmnetQuestion.Video = db.Video.Where(q => q.AssignmentId == assignmentId && q.IsActive == true).ToList();

            assignmnetQuestion.Question.ForEach(
                x => {
                    x.IsUserSubmitted = db.UserAssessment.Count(u => u.QuestionId == x.QuestionId && u.UserId == LoggedInAccount.AccountId) > 0;
                    x.IsCheckedByTeacher = db.UserAssessment.Count(u => u.QuestionId == x.QuestionId && u.IsFinalScore == true && u.UserId == LoggedInAccount.AccountId) > 0; }
                );

            return Request.CreateResponse(HttpStatusCode.OK, assignmnetQuestion);
        }


        [HttpPost]
        [Route("ChangedAssignmentStatus")]
        public HttpResponseMessage ChangedAssignmentStatus(int assignmentid, int assignmentstatusid, int studentId)
        {
            PraescioContext db = new PraescioContext();
            UserAssessmentDetail userAssessment = new UserAssessmentDetail();
            userAssessment = db.UserAssessmentDetail.Where(x => x.AssignmentId == assignmentid && x.UserId == studentId).FirstOrDefault();

            userAssessment.StatusId = assignmentstatusid;
            userAssessment.ModifiedOn = DateTime.Now;

            if(assignmentstatusid == (int)BusinessEntities.Common.AssignmentStatus.SubmittedByTeacher)
            {
                userAssessment.IsCompleted = true;
            }

            db.UserAssessmentDetail.Attach(userAssessment);
            db.Entry(userAssessment).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "send");
        }

        [HttpGet]
        [Route("GetQuestionList")]
        public HttpResponseMessage GetQuestionList(int assignmentId, int questiontype)
        {
            PraescioContext db = new PraescioContext();
            PraescioContext db1 = new PraescioContext();

            if ((int)TypeOfQuestion.MeaningOfLesson == questiontype)
            {
                var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && q.IsActive == true).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, question);
            }
            else if ((int)TypeOfQuestion.DifferentiateBetween == questiontype)
            {
                var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && q.IsActive == true).FirstOrDefault();
                question.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == question.QuestionId).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, question);
            }
            else if ((int)TypeOfQuestion.DifferentiateBetween == questiontype)
            {
                var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && q.IsActive == true).FirstOrDefault();
                question.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == question.QuestionId).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, question);
            }
            else
            {
                var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && q.IsActive == true).ToList();
                question.ForEach(x =>
                {
                    x.Title = x.QuestionTypeId != (int)TypeOfQuestion.FillInTheBlanks ? x.Title : x.Title.Replace("@textbox", "_________");
                    x.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == x.QuestionId).ToList();
                });
                return Request.CreateResponse(HttpStatusCode.OK, question);

            }

        }


        [HttpGet]
        [Route("GetStudentActivity")]
        public HttpResponseMessage GetStudentActivity(int assignmentId, int questiontype, int userid)
        {
            PraescioContext db = new PraescioContext();
            PraescioContext db1 = new PraescioContext();
            List<StudentActivityViewModel> studentActivity = new List<StudentActivityViewModel>();

            if ((int)TypeOfQuestion.WriteReason == questiontype || (int)TypeOfQuestion.FillInTheBlanks == questiontype
                || (int)TypeOfQuestion.TrueFalse == questiontype || (int)TypeOfQuestion.OneSentenceAnswer == questiontype 
                || (int)TypeOfQuestion.DescribeBriefly == questiontype
                || (int)TypeOfQuestion.WriteShortNote == questiontype)
            {
                studentActivity = (from u in db.UserAssessment
                                   join q in db.Question on u.QuestionId equals q.QuestionId
                                   where q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && u.UserId == userid && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       UserAssessment = u,
                                       Question = q
                                   }).ToList();

                studentActivity.ForEach(s => s.Question.Title = s.Question.Title.Replace("#textbox", "___________"));
                
                return Request.CreateResponse(HttpStatusCode.OK, studentActivity);
            }
            else if ((int)TypeOfQuestion.MatchTheFollowing == questiontype || (int)TypeOfQuestion.MCQ == questiontype)
            {
                studentActivity = (from u in db.UserAssessment
                                   join q in db.Question on u.QuestionId equals q.QuestionId
                                   join qo in db.QuestionOption on u.QuestionOptionId equals qo.Id
                                   where q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && u.UserId == userid && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       Question = q,
                                       QuestionOption = qo,
                                       UserAssessment = u
                                   }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, studentActivity);
            }
            else if ((int)TypeOfQuestion.DifferentiateBetween == questiontype)
            {
                studentActivity = (from u in db.UserAssessment
                                   join q in db.Question on u.QuestionId equals q.QuestionId
                                   //join qo in db.QuestionOption on u.QuestionOptionId equals qo.Id
                                   where q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && u.UserId == userid && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       Question = q,
                                       QuestionOptionList = db.QuestionOption.Where(m => m.QuestionId == q.QuestionId).ToList(),
                                       UserAssessment = u
                                   }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, studentActivity);
            }
            else if ((int)TypeOfQuestion.Exercise == questiontype)
            {
                studentActivity = (from u in db.UserAssessment
                                   join q in db.QuestionOption on u.QuestionOptionId equals q.Id
                                   where q.Question.AssignmentId == assignmentId && q.Question.QuestionTypeId == questiontype && u.UserId == userid && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       UserAssessment = u,
                                       QuestionOption = q
                                   }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, studentActivity);
            }
            //else if ((int)TypeOfQuestion.MeaningOfLesson == questiontype)
            //{
            //    var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype).FirstOrDefault();
            //    return Request.CreateResponse(HttpStatusCode.OK, question);
            //}
            //else if ((int)TypeOfQuestion.DifferentiateBetween == questiontype)
            //{
            //    var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype).FirstOrDefault();
            //    question.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == question.QuestionId).ToList();
            //    return Request.CreateResponse(HttpStatusCode.OK, question);
            //}
            //else if ((int)TypeOfQuestion.DifferentiateBetween == questiontype)
            //{
            //    var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype).FirstOrDefault();
            //    question.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == question.QuestionId).ToList();
            //    return Request.CreateResponse(HttpStatusCode.OK, question);
            //}
            else
            {
                var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && q.IsActive == true).ToList();
                question.ForEach(x =>
                {
                    x.Title = x.QuestionTypeId != (int)TypeOfQuestion.FillInTheBlanks ? x.Title : x.Title.Replace("@textbox", "_________");
                    x.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == x.QuestionId).ToList();
                });
                return Request.CreateResponse(HttpStatusCode.OK, question);

            }

        }

        [HttpGet]
        [Route("GetStudentActivityMarks")]
        public HttpResponseMessage GetStudentActivityMarks(int assignmentId = 5, int questiontype = 5, int userid = 0)
        {
            PraescioContext db = new PraescioContext();
            PraescioContext db1 = new PraescioContext();
            List<StudentActivityViewModel> studentActivity = new List<StudentActivityViewModel>();

            int userId = LoggedInAccount.AccountId;
            if ((LoggedInAccount.AccountTypeId == (int)AccountType.StudentParent))
            {
                var studentAccount = db.Account.Where(t => (t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.Student || t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent) && t.ParentEmail == LoggedInAccount.Email).FirstOrDefault();

                userId = studentAccount.AccountId;
            }

            if ((int)TypeOfQuestion.FillInTheBlanks == questiontype || (int)TypeOfQuestion.OneSentenceAnswer == questiontype || (int)TypeOfQuestion.DescribeBriefly == questiontype
                || (int)TypeOfQuestion.WriteShortNote == questiontype || (int)TypeOfQuestion.TrueFalse == questiontype || (int)TypeOfQuestion.WriteReason == questiontype)
            {
                studentActivity = (from u in db.UserAssessment
                                   join q in db.Question on u.QuestionId equals q.QuestionId
                                   where q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && (u.UserId == userid || u.UserId == userId) && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       UserAssessment = u,
                                       Question = q
                                   }).ToList();

                studentActivity.ForEach(s => s.Question.Title = s.Question.Title.Replace("#textbox", "___________"));

                return Request.CreateResponse(HttpStatusCode.OK, new { studentActivity = studentActivity, totalMarks = studentActivity.Sum(x => x.UserAssessment.MaxScore), MarksScored = studentActivity.Sum(x => x.UserAssessment.MarkScored) });
            }
            else if ((int)TypeOfQuestion.MatchTheFollowing == questiontype)
            {
                studentActivity = (from u in db.UserAssessment
                                   join q in db.QuestionOption on u.QuestionOptionId equals q.Id
                                   where q.Question.AssignmentId == assignmentId && q.Question.QuestionTypeId == questiontype && (u.UserId == userid || u.UserId == userId) && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       UserAssessment = u,
                                       QuestionOption = q
                                   }).ToList();


                return Request.CreateResponse(HttpStatusCode.OK, new { studentActivity = studentActivity, totalMarks = studentActivity.Sum(x => x.UserAssessment.MaxScore), MarksScored = studentActivity.Sum(x => x.UserAssessment.MarkScored) });
            }
            else if ((int)TypeOfQuestion.DifferentiateBetween == questiontype)
            {
                studentActivity = (from u in db.UserAssessment
                                   join q in db.Question on u.QuestionId equals q.QuestionId
                                   //join qo in db.QuestionOption on u.QuestionOptionId equals qo.Id
                                   where q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && (u.UserId == userid || u.UserId == userId) && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       Question = q,
                                       QuestionOptionList = db.QuestionOption.Where(m => m.QuestionId == q.QuestionId).ToList(),
                                       UserAssessment = u
                                   }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new { studentActivity = studentActivity, totalMarks = studentActivity.Sum(x => x.UserAssessment.MaxScore), MarksScored = studentActivity.Sum(x => x.UserAssessment.MarkScored) });
            }
            else if ((int)TypeOfQuestion.Exercise == questiontype)
            {

                studentActivity = (from u in db.UserAssessment
                                   join q in db.QuestionOption on u.QuestionOptionId equals q.Id
                                   where q.Question.AssignmentId == assignmentId && q.Question.QuestionTypeId == questiontype && (u.UserId == userid || u.UserId == userId) && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       UserAssessment = u,
                                       QuestionOption = q
                                   }).ToList();


                return Request.CreateResponse(HttpStatusCode.OK, new { studentActivity = studentActivity, totalMarks = studentActivity.Sum(x => x.UserAssessment.MaxScore), MarksScored = studentActivity.Sum(x => x.UserAssessment.MarkScored) });
            }
            else if ((int)TypeOfQuestion.MCQ == questiontype)
            {

                studentActivity = (from u in db.UserAssessment
                                   join q in db.Question on u.QuestionId equals q.QuestionId
                                   where q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && (u.UserId == userid || u.UserId == userId) && q.IsActive == true
                                   select new StudentActivityViewModel
                                   {
                                       UserAssessment = u,
                                       Question = q
                                   }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new { studentActivity = studentActivity, totalMarks = studentActivity.Sum(x => x.UserAssessment.MaxScore), MarksScored = studentActivity.Sum(x => x.UserAssessment.MarkScored) });
            }

            //else if ((int)TypeOfQuestion.MeaningOfLesson == questiontype)
            //{
            //    var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype).FirstOrDefault();
            //    return Request.CreateResponse(HttpStatusCode.OK, question);
            //}
            //else if ((int)TypeOfQuestion.DifferentiateBetween == questiontype)
            //{
            //    var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype).FirstOrDefault();
            //    question.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == question.QuestionId).ToList();
            //    return Request.CreateResponse(HttpStatusCode.OK, question);
            //}
            //else if ((int)TypeOfQuestion.DifferentiateBetween == questiontype)
            //{
            //    var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype).FirstOrDefault();
            //    question.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == question.QuestionId).ToList();
            //    return Request.CreateResponse(HttpStatusCode.OK, question);
            //}
            else
            {
                var question = db.Question.Where(q => q.AssignmentId == assignmentId && q.QuestionTypeId == questiontype && q.IsActive == true).ToList();
                question.ForEach(x =>
                {
                    x.Title = x.QuestionTypeId != (int)TypeOfQuestion.FillInTheBlanks ? x.Title : x.Title.Replace("@textbox", "_________");
                    x.QuestionOption = db1.QuestionOption.Where(o => o.QuestionId == x.QuestionId).ToList();
                    //x.UserAssessment = db1.UserAssessment.Where(m => m.QuestionId == x.QuestionId && m.UserId == userid);
                });
                return Request.CreateResponse(HttpStatusCode.OK, question);

            }

        }

        [HttpGet]
        [Route("GetStudentAssessment")]
        public HttpResponseMessage GetStudentAssessment(int assignmentid, int studendid, int questiontypeid)
        {
            PraescioContext db = new PraescioContext();
            QuestionList assignmnetQuestion = new QuestionList();

            if (questiontypeid == (int)TypeOfQuestion.FillInTheBlanks)
            {
                var data = db.UserAssessment.Where(x => x.IsActive == true && x.UserId == studendid && x.Question.AssignmentId == assignmentid && x.Question.QuestionTypeId == questiontypeid).ToList();
                int TotalMarksScored = data.Sum(x => x.MarkScored).Value;
                int TotalMarks = data.Sum(x => x.MaxScore).Value;

                return Request.CreateResponse(HttpStatusCode.OK, new { Assessment = data, TotalMarksScored = TotalMarksScored, TotalMarks = TotalMarks });
            }
            else if (questiontypeid == (int)TypeOfQuestion.MatchTheFollowing)
            {
                List<QuestionAssessmentDetail> questionAssessmentList = new List<Domain.Model.QuestionAssessmentDetail>();
                foreach (var assessment in db.UserAssessment.Where(x => x.IsActive == true && x.UserId == studendid && x.Question.AssignmentId == assignmentid && x.Question.QuestionTypeId == questiontypeid).ToList())
                {
                    QuestionAssessmentDetail assessmentDetail = new QuestionAssessmentDetail();
                    assessmentDetail.UserAssessment = assessment;
                    assessmentDetail.MatchQuestion = string.Join(",", db.QuestionOption.Where(x => x.QuestionId == assessment.QuestionId).Select(x => x.Option).ToArray());
                    assessmentDetail.QuestionOption = string.Join(",", db.QuestionOption.Where(x => x.QuestionId == assessment.QuestionId).Select(x => x.MatchQuestion).ToArray());
                    assessmentDetail.CorrectAnswer = string.Join(",", db.QuestionOption.Where(x => x.QuestionId == assessment.QuestionId).Select(x => x.MatchAnswer).ToArray());
                    questionAssessmentList.Add(assessmentDetail);
                }

                int TotalMarksScored = questionAssessmentList.Sum(x => x.UserAssessment.MarkScored).Value;
                int TotalMarks = questionAssessmentList.Sum(x => x.UserAssessment.MaxScore).Value;

                return Request.CreateResponse(HttpStatusCode.OK, new { Assessment = questionAssessmentList, TotalMarksScored = TotalMarksScored, TotalMarks = TotalMarks });
            }
            else if (questiontypeid == (int)TypeOfQuestion.MCQ)
            {
                var data = db.UserAssessment.Where(x => x.IsActive == true && x.UserId == studendid && x.Question.AssignmentId == assignmentid && x.Question.QuestionTypeId == questiontypeid).ToList();
                int TotalMarksScored = data.Sum(x => x.MarkScored).Value;
                int TotalMarks = data.Sum(x => x.MaxScore).Value;
                var qts = db.Question.Where(x => x.AssignmentId == assignmentid).FirstOrDefault();

                List<MCQAssessmentDetail> MCQAssessmentDetail = new List<Domain.Model.MCQAssessmentDetail>();
                foreach (var item in data)
                {
                    MCQAssessmentDetail AssessmentDetail = new MCQAssessmentDetail();
                    AssessmentDetail.UserAssessment = item;

                    AssessmentDetail.StudentTextAnswer = item.Answer == "A" ? item.Question.MCQText1 : item.Answer == "B" ? item.Question.MCQText2 : item.Answer == "C" ? item.Question.MCQText3 : item.Answer == "D" ? item.Question.MCQText4 : "";
                    AssessmentDetail.StudentImageAnswer = item.Answer == "A" ? item.Question.MCQImage1Src : item.Answer == "B" ? item.Question.MCQImage2Src : item.Answer == "C" ? item.Question.MCQImage3Src : item.Answer == "D" ? item.Question.MCQImage4Src : "";
                    AssessmentDetail.TextCorrectAnswer = item.Question.CorrectAnswer == "A" ? item.Question.MCQText1 : item.Question.CorrectAnswer == "B" ? item.Question.MCQText2 : item.Question.CorrectAnswer == "C" ? item.Question.MCQText3 : item.Question.CorrectAnswer == "D" ? item.Question.MCQText4 : "";
                    AssessmentDetail.ImageCorrectAnswer = item.Question.CorrectAnswer == "A" ? item.Question.MCQImage1Src : item.Question.CorrectAnswer == "B" ? item.Question.MCQImage2Src : item.Question.CorrectAnswer == "C" ? item.Question.MCQImage3Src : item.Question.CorrectAnswer == "D" ? item.Question.MCQImage4Src : "";
                    MCQAssessmentDetail.Add(AssessmentDetail);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { AssessmentDetail = MCQAssessmentDetail, TotalMarksScored = TotalMarksScored, TotalMarks = TotalMarks });
            }
            else if (questiontypeid == (int)TypeOfQuestion.TrueFalse)
            {
                var data = db.UserAssessment.Where(x => x.IsActive == true && x.UserId == studendid && x.Question.AssignmentId == assignmentid && x.Question.QuestionTypeId == questiontypeid).ToList();
                int TotalMarksScored = data.Sum(x => x.MarkScored).Value;
                int TotalMarks = data.Sum(x => x.MaxScore).Value;

                return Request.CreateResponse(HttpStatusCode.OK, new { Assessment = data, TotalMarksScored = TotalMarksScored, TotalMarks = TotalMarks });
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");


        }

        [HttpPost]
        [Route("AddKnowledge")]
        public HttpResponseMessage AddKnowledge(KnowledgeViewModel knowledgeViewModel)
        {
            using (PraescioContext db = new PraescioContext())
            {
                knowledgeViewModel.KnowledgeBank.CreatedBy = LoggedInAccount.AccountId;
                knowledgeViewModel.KnowledgeBank.VisibleToRole = string.Join(",", knowledgeViewModel.KnowledgeBank.VisibleToRole.Split(',').Distinct().ToArray());
                knowledgeViewModel.KnowledgeBank.CreatedOn = DateTime.Now;
                db.KnowledgeBank.Add(knowledgeViewModel.KnowledgeBank);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully saved");

        }

        [HttpPost]
        [Route("UpdateKnowledge")]
        public HttpResponseMessage UpdateKnowledge(KnowledgeViewModel knowledgeViewModel, int knowledgeBankId)
        {
            using (PraescioContext db = new PraescioContext())
            {
                var knowledge = db.KnowledgeBank.FirstOrDefault(x => x.KnowledgeBankId == knowledgeBankId);
                knowledge.Title = knowledgeViewModel.KnowledgeBank.Title;
                knowledge.Description = knowledgeViewModel.KnowledgeBank.Description;
                knowledge.VisibleToRole = string.Join(",", knowledgeViewModel.KnowledgeBank.VisibleToRole.Split(',').Distinct().ToArray());
                knowledge.ModifiedOn = DateTime.Now;
                knowledge.ModifiedBy = LoggedInAccount.AccountId;
                knowledge.PDFFileSrc = !string.IsNullOrEmpty(knowledgeViewModel.KnowledgeBank.PDFFileSrc) ? knowledgeViewModel.KnowledgeBank.PDFFileSrc : knowledge.PDFFileSrc;
                db.Entry(knowledge);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully saved");

        }

        [HttpGet]
        [Route("GetKnowledgeBank")]
        public HttpResponseMessage GetKnowledgeBank(string flag = "", int pageNo = 1, int itemPerPage = 10)
        {
            PraescioContext db = new PraescioContext();
            List<KnowledgeViewModel> knowledgeBankList = new List<KnowledgeViewModel>();
            IEnumerable<KnowledgeViewModel> list;

            if (flag == "history")
            {
                list = (from k in db.KnowledgeBank
                        where k.CreatedBy == LoggedInAccount.AccountId
                        select new KnowledgeViewModel { KnowledgeBank = k });

                knowledgeBankList = list.OrderByDescending(x => x.KnowledgeBank.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
            }
            else
            {
                list = (from k in db.KnowledgeBank
                        where k.VisibleToRole.Contains(LoggedInAccount.AccountTypeId.ToString())
                        select new KnowledgeViewModel { KnowledgeBank = k });

                if(LoggedInAccount.AccountTypeId != (int)BusinessEntities.Common.AccountType.SuperAdmin)
                {
                    list = list.Where(m => m.KnowledgeBank.IsActive == true);
                }
                knowledgeBankList = list.OrderByDescending(x => x.KnowledgeBank.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { contentData = knowledgeBankList, totalRecord = list.Count() });
        }


        [HttpGet]
        [Route("GetKnowledgeBankDetail")]
        public HttpResponseMessage GetKnowledgeBankDetail(int knowledgeBankId)
        {
            KnowledgeViewModel knowledgeViewModel = new KnowledgeViewModel();
            PraescioContext db = new PraescioContext();
            knowledgeViewModel.KnowledgeBank = db.KnowledgeBank.FirstOrDefault(k => k.KnowledgeBankId == knowledgeBankId);
            return Request.CreateResponse(HttpStatusCode.OK, knowledgeViewModel);
        }



        [HttpPost]
        [Route("SaveQuestionContent")]
        public HttpResponseMessage SaveQuestionContent(List<QuestionContent> QuestionContent, int CategoryTypeId)
        {
            PraescioContext db = new PraescioContext();
            foreach (var content in QuestionContent)
            {
                if (!db.QuestionContent.Any(x => x.ContentOption.Equals(content.ContentOption, StringComparison.CurrentCultureIgnoreCase) && x.ContentAnswer.Equals(content.ContentAnswer, StringComparison.CurrentCultureIgnoreCase)))
                {
                    content.InstitutionAccountId = LoggedInAccount.OrganizationAccount.InstitutionAccountId;
                    content.CategoryTypeId = CategoryTypeId;
                    content.CreatedOn = DateTime.Now;
                    content.CreatedBy = LoggedInAccount.AccountId;
                    db.QuestionContent.Add(content);
                }
            }
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "successfully Saved");
        }




        [HttpGet]
        [Route("GetQuestionContent")]
        public HttpResponseMessage GetQuestionContent(int CategoryTypeId, int pageNo = 1, int itemsPerPage = 20, string searchText = "")
        {
            PraescioContext db = new PraescioContext();
            var ListContent = db.QuestionContent.Where(x => x.CategoryTypeId == CategoryTypeId).Distinct();

            if (searchText != "")
            {
                ListContent = ListContent.Where(x => x.ContentOption.Contains(searchText) ||
                                                x.ContentAnswer.Contains(searchText));
            }
            var content = ListContent.ToList();
            if(CategoryTypeId == (int)Praescio.BusinessEntities.Common.CategoryType.Synonyms || CategoryTypeId == (int)Praescio.BusinessEntities.Common.CategoryType.Antonyms)
            {
                content = content.OrderBy(x => x.ContentOption).Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            } else
            {
                content = content.OrderBy(x => x.ContentId).Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            }
            
            var count = ListContent.Count();

            return Request.CreateResponse(HttpStatusCode.OK, new { contentData = content, totalRecord = count });

        }

        [HttpPost]
        [Route("SaveQuestionContentToAssignment")]
        public HttpResponseMessage SaveQuestionContentToAssignment(List<QuestionContentViewModel> ContentViewModel, int CategoryTypeId, int AssignmentId)
        {

            PraescioContext db = new PraescioContext();
            var questionType = db.QuestionType.Where(x => x.IsActive == true).ToList();

            foreach (var content in ContentViewModel.Where(x => x.Checked))
            {
                int questionTypeId = (int)Praescio.BusinessEntities.Common.CategoryType.Antonyms == CategoryTypeId ? (int)TypeOfQuestion.AntonymsOfLesson : (int)TypeOfQuestion.SynonymsOfLesson;
                Question qts = new Question
                {
                    Title = content.ContentOption,
                    AssignmentId = AssignmentId,
                    QuestionTypeId = questionTypeId,
                    CorrectAnswer = content.ContentAnswer,
                    TotalMarks = Convert.ToInt16(questionType.FirstOrDefault(x => x.QuestionTypeId == questionTypeId).TotalMarks),
                    CreatedBy = LoggedInAccount.AccountId,
                    CreatedOn = DateTime.Now
                };
                db.Question.Add(qts);
            }
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        [HttpPost]
        [Route("DeleteAssignment")]
        public HttpResponseMessage DeleteAssignment(int id)
        {
            using (PraescioContext db = new PraescioContext())
            {
                var assignment = db.Assignment.FirstOrDefault(x => x.AssignmentId == id);
                assignment.IsActive = false;
                assignment.ModifiedBy = LoggedInAccount.AccountId;
                assignment.ModifiedOn = DateTime.Now;

                db.Entry(assignment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully deleted");

        }

        [HttpPost]
        [Route("UpdateAssignment")]
        public HttpResponseMessage UpdateAssignment(LessonViewModel assignmentDetail)
        {
            using (PraescioContext db = new PraescioContext())
            {
                var assignment = db.Assignment.FirstOrDefault(a => a.AssignmentId == assignmentDetail.Assignment.AssignmentId);


                assignment.Title = assignmentDetail.Assignment.Title;
                assignment.UploadFileSrc = assignmentDetail.Assignment.UploadFileSrc;
                assignment.Description = assignmentDetail.Assignment.Description;
                assignment.BoardId = assignmentDetail.Assignment.BoardId;
                assignment.MediumId = assignmentDetail.Assignment.MediumId;
                assignment.StandardId = assignmentDetail.Assignment.StandardId;
                assignment.SubjectId = assignmentDetail.Assignment.SubjectId;

                assignment.ModifiedBy = LoggedInAccount.AccountId;
                assignment.ModifiedOn = DateTime.Now;
                db.Entry(assignment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //if (assignmentDetail.Assignment.CreatedRole == (int)AccountType.Admin)
                //{
                //    foreach (var teacher in assignmentDetail.AssignmentTeacher)
                //    {
                //        db.AssignmentTeacherMapping.Add(new Domain.Entities.AssignmentTeacherMapping { AssignmentId = assignmentDetail.Assignment.AssignmentId, TeacherAccountId = teacher, CreatedOn = DateTime.Now, CreatedBy = assignmentDetail.Assignment.CreatedBy });
                //        db.SaveChanges();
                //    }
                //}
            }
            return Request.CreateResponse(HttpStatusCode.OK, "successfully updated");

        }

        [HttpPost]
        [Route("UploadBulkTeacher")]
        public HttpResponseMessage UploadBulkTeacher(BulkUploadUser BulkUploadUser, bool isIndividual = false)
        {
            BulkUploadUser.institutionId = LoggedInAccount.InstitutionAccountId == (int)BusinessEntities.Common.AccountType.Admin ? LoggedInAccount.InstitutionAccountId : LoggedInAccount.InstitutionAccountId == (int)BusinessEntities.Common.AccountType.Teacher ? LoggedInAccount.InstitutionAccountId : BulkUploadUser.institutionId;
            PraescioContext db = new PraescioContext();

            var boardlist = db.Board.ToList();
            var statelist = db.State.ToList();
            var standardlist = db.Standard.ToList();

            int AddedCount = 0;
            foreach (var user in BulkUploadUser.Account)
            {
                Random rand = new Random();

                Mst_Account account = new Mst_Account();

                account.FirstName = user.FirstName;
                account.LastName = user.LastName;
                account.Gender = user.Gender;
                account.DateOfBirth = user.DateOfBirth;
                account.Email = user.Email;
                account.MobileNo = user.MobileNo;
                //account.VersionType = user.Version;
                //account.BoardId = boardlist.FirstOrDefault(s => s.BoardName.ToLower().Contains(user.Board.ToLower())) != null ? (int?)boardlist.FirstOrDefault(s => s.BoardName.ToLower().Contains(user.Board.ToLower())).Id : null;
                account.MotherName = user.MotherName;
                account.FatherName = user.FatherName;
                account.ParentEmail = user.ParentEmailId;
                account.ParentMobileNo = user.ParentMobileNo;
                account.ActivateOn = user.ActivateOn;
                //account.ExpiredOn = user.ExpiredOn;
                account.Address = user.Address;
                //account.StateId = statelist.FirstOrDefault(s => s.StateName.ToLower().Contains(user.State.ToLower())) != null ? (int?)statelist.FirstOrDefault(s => s.StateName.ToLower().Contains(user.State.ToLower())).Id : null;
                //account.City = user.City;
                //account.PinCode = user.PinCode;

                account.BoardId = user.BoardId;
                account.MediumId = user.MediumId;

                if (String.IsNullOrEmpty(account.FirstName) ||
                    String.IsNullOrEmpty(account.LastName) ||
                    String.IsNullOrEmpty(account.Gender) ||
                    account.DateOfBirth.HasValue == false ||
                    String.IsNullOrEmpty(account.Email) ||
                    String.IsNullOrEmpty(account.MobileNo) ||
                    //String.IsNullOrEmpty(account.MotherName) ||
                    //String.IsNullOrEmpty(account.FatherName) ||
                    //String.IsNullOrEmpty(account.ParentEmail) ||
                    //String.IsNullOrEmpty(account.ParentMobileNo) ||
                    account.ActivateOn.HasValue == false ||
                    String.IsNullOrEmpty(account.Address))
                {
                    // TODO - DO not allow any empty value column in a row.
                    continue;
                }

            SET_UserName:
                account.UserName = user.FirstName.Trim() + rand.Next(100000).ToString();
                account.Password = BusinessEntities.AppCode.Common.RandomString(8);
                account.AccountTypeId = isIndividual ? (int)BusinessEntities.Common.AccountType.IndividualTeacher : (int)BusinessEntities.Common.AccountType.Teacher;
                account.IsIndividual = isIndividual;
                account.InstitutionAccountId = isIndividual ? 1 : BulkUploadUser.institutionId;

                if (db.Account.Any(x => x.UserName.ToLower() == account.UserName))
                {
                    goto SET_UserName;
                }

                account.CreatedOn = DateTime.Now;
                db.Account.Add(account);
                db.SaveChanges();

                AddedCount++;

                var subject = user.StandardSubject.Replace("\r\n", "").Replace("&#10;", "").Split(',').ToArray();
                foreach (var s in subject)
                {
                    var standard = s.Split('-')[0];
                    var standardsubject = s.Split('-')[1];

                    if (standard.Length > 1)
                    {
                        var Standard = db.Standard.FirstOrDefault(x => standard.Contains(x.StandardName));
                        var tSubject = db.Subject.FirstOrDefault(x => standardsubject.Contains(x.SubjectName) && x.StandardId == Standard.Id && x.BoardId == account.BoardId && x.MediumId == account.MediumId);
                        if(Standard != null && tSubject != null)
                        {
                            db.StandardMapping.Add(new TeacherStandardMapping
                            {
                                StandardId = Standard.Id,
                                SubjectId = tSubject.Id,
                                TeacherId = account.AccountId
                            });
                        }
                    }
                }
                db.SaveChanges();

                // SEND EMAIL

                var organizationAccount = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == account.InstitutionAccountId);
                string emailsubject = string.Empty;
                Dictionary<string, string> accountDetail = new Dictionary<string, string>();
                accountDetail.Add("username", organizationAccount.DomainKey.ToLower() + "/" + account.UserName);
                accountDetail.Add("password", account.Password);
                accountDetail.Add("name", account.FirstName + " " + account.LastName);
                string emailcontent = Email.GetEmailContent(accountDetail, MailType.InstitutionTeacher, ref emailsubject);

                Email.SendEmail(emailcontent, user.Email, "", emailsubject);
                string smsContent = "Hello " + accountDetail["name"] + ", Thanks for registering with Praescioedu. UserName: " + accountDetail["username"] + " and Password: " + accountDetail["password"];
                SMS.SendAlertSMS(account.MobileNo, smsContent);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, AddedCount + " Teacher's has been created successfully!!!");
        }

        [HttpPost]
        [Route("UploadBulkStudent")]
        public HttpResponseMessage UploadBulkStudent(BulkUploadUser BulkUploadUser, bool isIndividual = false)
        {
            BulkUploadUser.institutionId = LoggedInAccount.InstitutionAccountId == (int)BusinessEntities.Common.AccountType.Admin ? LoggedInAccount.InstitutionAccountId : LoggedInAccount.InstitutionAccountId == (int)BusinessEntities.Common.AccountType.Teacher ? LoggedInAccount.InstitutionAccountId : BulkUploadUser.institutionId;
            PraescioContext db = new PraescioContext();
            var institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == BulkUploadUser.institutionId);
            var boardlist = db.Board.ToList();
            var statelist = db.State.ToList();
            var standardlist = db.Standard.ToList();

            int AddedCount = 0;
            foreach (var user in BulkUploadUser.Account)
            {
                Random rand = new Random();
                Mst_Account account = new Mst_Account();

                account.FirstName = user.FirstName;
                account.LastName = user.LastName;
                account.Gender = user.Gender;
                account.DateOfBirth = user.DateOfBirth;
                account.Email = user.Email;
                account.MobileNo = user.MobileNo;
                //account.VersionType = user.Version;
                //account.BoardId = boardlist.FirstOrDefault(s => s.BoardName.ToLower().Contains(user.Board.ToLower())) != null ? (int?)boardlist.FirstOrDefault(s => s.BoardName.ToLower().Contains(user.Board.ToLower())).Id : null;
                account.StudentStandardId = standardlist.FirstOrDefault(s => user.StandardSubject.Contains(s.StandardName)) != null ? (int?)standardlist.FirstOrDefault(s => user.StandardSubject.Contains(s.StandardName)).Id : null;
                account.MotherName = user.MotherName;
                account.FatherName = user.FatherName;
                account.ParentEmail = user.ParentEmailId;
                account.ParentMobileNo = user.ParentMobileNo;
                account.ParentNo = user.ParentMobileNo;
                //account.ActivateOn = user.ActivateOn;
                account.ActivateOn = DateTime.Now;
                account.ExpiredOn = institution.ExpiredOn;
                account.Address = user.Address;
                //account.StateId = statelist.FirstOrDefault(s => s.StateName.ToLower().Contains(user.State.ToLower())) != null ? (int?)statelist.FirstOrDefault(s => s.StateName.ToLower().Contains(user.State.ToLower())).Id : null;
                //account.City = user.City;
                //account.PinCode = user.PinCode;

                account.BoardId = user.BoardId;
                account.MediumId = user.MediumId;

                if(String.IsNullOrEmpty(account.FirstName) ||
                    String.IsNullOrEmpty(account.LastName) ||
                    String.IsNullOrEmpty(account.Gender) ||
                    account.DateOfBirth.HasValue == false ||
                    String.IsNullOrEmpty(account.Email) ||
                    String.IsNullOrEmpty(account.MobileNo) ||
                    account.StudentStandardId.HasValue == false ||
                    String.IsNullOrEmpty(account.MotherName) ||
                    String.IsNullOrEmpty(account.FatherName) ||
                    String.IsNullOrEmpty(account.ParentEmail) ||
                    String.IsNullOrEmpty(account.ParentMobileNo) ||
                    account.ActivateOn.HasValue == false ||
                    String.IsNullOrEmpty(account.Address))
                {
                    // TODO - DO not allow any empty value column in a row.
                    continue;
                }

            SET_UserName:
                account.UserName = user.FirstName.Trim() + rand.Next(100000).ToString();
                account.Password = BusinessEntities.AppCode.Common.RandomString(8);
                account.AccountTypeId = isIndividual ? (int)BusinessEntities.Common.AccountType.IndividualStudent : (int)BusinessEntities.Common.AccountType.Student;
                account.IsIndividual = isIndividual;
                account.InstitutionAccountId = isIndividual ? 1 : BulkUploadUser.institutionId;

                if (db.Account.Any(x => x.UserName.ToLower() == account.UserName))
                {
                    goto SET_UserName;
                }

                account.CreatedOn = DateTime.Now;
                account.CreatedBy = 1;
                db.Account.Add(account);
                db.SaveChanges();

                AddedCount++;

                //var subject = user.StandardSubject.Replace("\r\n", "").Replace("&#10;", "").Split(',').ToArray();
                //foreach (var s in subject)
                //{
                //    var standard = s.Split('-')[0];
                //    //var standardsubject = s.Split('-')[1];

                //    //if (standard.Length > 1)
                //    //{
                //    //    var Standard = db.Standard.FirstOrDefault(x => standard.Contains(x.StandardName));
                //    //    db.StandardMapping.Add(new TeacherStandardMapping
                //    //    {
                //    //        StandardId = Standard.Id,
                //    //        SubjectId = db.Subject.FirstOrDefault(x => standardsubject.Contains(x.SubjectName) && x.StandardId == Standard.Id && x.BoardId == account.BoardId && x.MediumId == account.MediumId).Id,
                //    //        TeacherId = account.AccountId
                //    //    });
                //    //}

                //    var Standard = db.Standard.FirstOrDefault(x => standard.Contains(x.StandardName));
                //    if (Standard != null)
                //    {
                //        var subjects = db.Subject.Where(x => x.StandardId == Standard.Id && x.BoardId == account.BoardId && x.MediumId == account.MediumId).ToList();
                //        foreach (var item in subjects)
                //        {
                //            db.StandardMapping.Add(new TeacherStandardMapping
                //            {
                //                StandardId = Standard.Id,
                //                SubjectId = item.Id,
                //                TeacherId = account.AccountId
                //            });
                //        }
                //    }
                //}
                //db.SaveChanges();

                //for (int i = 0; i < 4; i++)
                //{
                //    Mst_StudentParentAccount Mst_StudentParentAccount = new Mst_StudentParentAccount
                //    {
                //        Username = "Parent" + account.AccountId.ToString() + "_" + rand.Next(100000).ToString(),
                //        Password = BusinessEntities.AppCode.Common.RandomString(8),
                //        AccountTypeId = (int)AccountType.StudentParent,
                //        CreatedBy = LoggedInAccount.AccountId,
                //        CreatedOn = DateTime.Now,
                //        IsActive = false
                //    };
                //    db.StudentParentAccount.Add(Mst_StudentParentAccount);
                //}

                List<Mst_Account> ParentAccounts = new List<Mst_Account>();
                Mst_Account Mst_AccountParent = new Mst_Account
                {
                    UserName = "Parent" + account.AccountId.ToString() + "_" + rand.Next(100000).ToString(),
                    Password = BusinessEntities.AppCode.Common.RandomString(8),
                    AccountTypeId = (int)AccountType.StudentParent,
                    CreatedBy = LoggedInAccount.AccountId,
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    Email = account.ParentEmail,
                    MobileNo = account.ParentNo,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    BoardId = account.BoardId,
                    MediumId = account.MediumId,
                    //Standard = accountDetail.account.Standard,
                    //StandardSubject = accountDetail.account.StandardSubject
                    InstitutionAccountId = account.InstitutionAccountId
                };
                db.Account.Add(Mst_AccountParent);
                ParentAccounts.Add(Mst_AccountParent);
                db.SaveChanges();

                // SEND EMAIL

                var organizationAccount = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == account.InstitutionAccountId);
                string emailsubject = string.Empty;
                Dictionary<string, string> accountDetail = new Dictionary<string, string>();
                accountDetail.Add("username", organizationAccount.DomainKey.ToLower() + "/" + account.UserName);
                accountDetail.Add("password", account.Password);
                accountDetail.Add("name", account.FirstName + " " + account.LastName);
                string emailcontent = Email.GetEmailContent(accountDetail, MailType.InstitutionStudent, ref emailsubject);

                Email.SendEmail(emailcontent, user.Email, "", emailsubject);
                string smsContent = "Hello " + accountDetail["name"] + ", Thanks for registering with Praescioedu. UserName: " + accountDetail["username"] + " and Password: " + accountDetail["password"];
                SMS.SendAlertSMS(account.MobileNo, smsContent);

                var type = MailType.InstitutionStudent;
                foreach (var parent in ParentAccounts)
                {
                    string emailsubject2 = string.Empty;
                    Dictionary<string, string> account2 = new Dictionary<string, string>();
                    account2.Add("username", organizationAccount.DomainKey + "/" + parent.UserName);
                    account2.Add("password", parent.Password);
                    account2.Add("name", parent.FirstName + " " + parent.LastName);
                    string emailcontent2 = Email.GetEmailContent(account2, type, ref emailsubject);

                    Email.SendEmail(emailcontent2, parent.Email, "", emailsubject);
                    smsContent = "Hello " + account2["name"] + ", Thanks for registering with Praescioedu. UserName: " + account2["username"] + " and Password: " + account2["password"];
                    SMS.SendAlertSMS(parent.MobileNo, smsContent);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, AddedCount + " Student's has been created successfully!!!");
        }

        [HttpGet]
        [Route("GetUserAssessmentDetail")]
        public HttpResponseMessage GetUserAssessmentDetail(int assignmentId)
        {
            PraescioContext db = new PraescioContext();
            int userId = LoggedInAccount.AccountId;
            if ((LoggedInAccount.AccountTypeId == (int)AccountType.StudentParent))
            {
                var studentAccount = db.Account.Where(t => (t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.Student || t.AccountTypeId == (int)Praescio.BusinessEntities.Common.AccountType.IndividualStudent) && t.ParentEmail == LoggedInAccount.Email).FirstOrDefault();

                userId = studentAccount.AccountId;
            }
            var data = db.UserAssessmentDetail.Where(x => x.AssignmentId == assignmentId && x.UserId == userId).FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, data);

        }

        [HttpGet]
        [Route("IsExamStarted")]
        public HttpResponseMessage IsExamStarted(int assignmentId)
        {
            PraescioContext db = new PraescioContext();
            bool ExamStarted = db.UserAssessmentDetail.Any(x => x.AssignmentId == assignmentId && x.UserId == LoggedInAccount.AccountId);

            return Request.CreateResponse(HttpStatusCode.OK, ExamStarted);

        }

        [HttpPost]
        [Route("LogExamStartDate")]
        public HttpResponseMessage LogExamStartDate(int assignmentId)
        {
            PraescioContext db = new PraescioContext();
            if (!db.UserAssessmentDetail.Any(x => x.AssignmentId == assignmentId && x.UserId == LoggedInAccount.AccountId))
            {
                int totalQuestion = db.Question.Count(q => q.AssignmentId == assignmentId && q.IsActive == true);

                db.UserAssessmentDetail.Add(new UserAssessmentDetail
                {
                    AssignmentId = assignmentId,
                    CreatedOn = DateTime.Now,
                    StatusId = 1,
                    IsActive = true,
                    TotalQuestion = totalQuestion,
                    UserId = LoggedInAccount.AccountId,
                    ExamStartDate = DateTime.Now
                });
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");

        }


        [HttpPost]
        [Route("ToggleBlockInstitution")]
        public HttpResponseMessage ToggleBlockInstitution(bool status, int institutionid, string blockpassword)
        {
            PraescioContext db = new PraescioContext();
            string password = Praescio.BusinessEntities.AppCode.Common.BlockPassword;
            blockpassword = Uri.UnescapeDataString(blockpassword);

            if (blockpassword.Equals(password, StringComparison.InvariantCultureIgnoreCase))
            {
                var institution = db.OrganizationAccount.FirstOrDefault(x => x.InstitutionAccountId == institutionid);
                institution.IsActive = status;
                institution.ModifiedBy = LoggedInAccount.AccountId;
                institution.ModifiedOn = DateTime.Now;
                db.Entry(institution).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { isSuccess = true, message = "institution has been blocked successfully!!!" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { isSuccess = false, message = "Password is incorrect!!!" });
            }

        }

        [HttpPost]
        [Route("ToggleBlockUser")]
        public HttpResponseMessage ToggleBlockUser(bool status, int userid, string blockpassword)
        {
            PraescioContext db = new PraescioContext();
            string password = Praescio.BusinessEntities.AppCode.Common.BlockPassword;
            blockpassword = Uri.UnescapeDataString(blockpassword);

            if (blockpassword.Equals(password, StringComparison.InvariantCultureIgnoreCase))
            {
                var account = db.Account.FirstOrDefault(x => x.AccountId == userid);
                account.IsActive = status;
                account.ModifiedBy = LoggedInAccount.AccountId;
                account.ModifiedOn = DateTime.Now;
                db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { isSuccess = true, message = "user has been blocked successfully!!!" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { isSuccess = false, message = "Password is incorrect!!!" });
            }
        }

        [HttpGet]
        [Route("GetStandard")]
        public HttpResponseMessage GetStandard()
        {
            PraescioContext db = new PraescioContext();
            var standardList = (from s in db.Standard
                               where s.IsActive == true
                               select new { Id = s.Id, StandardName = s.StandardName }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, standardList);

        }

        [HttpGet]
        [Route("GetSubject")]
        public HttpResponseMessage GetSubject(int? standardid, int mediumid)
        {
            PraescioContext db = new PraescioContext();
            var subjectlist = db.Subject.Where(x => x.StandardId == standardid && (x.MediumId == mediumid || LoggedInAccount.OrganizationAccount.MediumId == mediumid)).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, subjectlist);

        }

        [HttpGet]
        [Route("GetSubjectSingleMedium")]
        public HttpResponseMessage GetSubjectSingleMedium(int? standardid, int mediumid, int boardid)
        {
            PraescioContext db = new PraescioContext();
            var subjectlist = db.Subject.Where(x => x.StandardId == standardid && x.MediumId == mediumid && x.BoardId == boardid).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, subjectlist);
        }
        
        [HttpPost]
        [Route("AddNotice")]
        public HttpResponseMessage AddNotice(Notice notice)
        {
            try
            {
                using (PraescioContext db = new PraescioContext())
                {
                    notice.CreatedBy = LoggedInAccount.AccountId;
                    notice.VisibleToRole = string.Join(",", notice.VisibleToRole.Split(',').Distinct().ToArray());
                    notice.CreatedOn = DateTime.Now;
                    db.Notice.Add(notice);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Notice saved successfully!" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Notice update failed!" });
                //throw;
            }
        }
        
        [HttpGet]
        [Route("GetNoticeList")]
        public HttpResponseMessage GetNoticeList(string flag = "history", int pageNo = 1, int itemPerPage = 10, string searchText = "", int StandardId = 0, bool SentByMe = false)
        {
            PraescioContext db = new PraescioContext();
            List<Notice> notices = new List<Notice>();

            if (SentByMe || flag == "history")
            {
                notices = (from k in db.Notice
                        where k.CreatedBy == LoggedInAccount.AccountId
                        select k).ToList();
            }
            else
            {
                notices = (from k in db.Notice
                           where k.VisibleToRole.Contains(LoggedInAccount.AccountTypeId.ToString())
                           select k).ToList();
            }

            //if (SentByMe)
            //{
            //    notices = notices.Where(m => m.CreatedBy == LoggedInAccount.AccountId).ToList();
            //}
            int totalCount = notices.Count;
            notices = notices.OrderByDescending(x => x.CreatedOn).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, new { contentData = notices, totalRecord = totalCount });
        }
    }
}
