using Praescio.Domain.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.Domain.AppCode
{
    public static class VersionType
    {
        public const string Paid = "Paid";
        public const string Trial = "Trial";
    }

    public static class KnowledgeBankReceiver
    {
        public const string ALL = "1,2,3,4,5,6";
        public const string TEACHER = "3,5";
        public const string STUDENT = "4,6";
    }

    public static class CommonCode
    {
        public static string Antonyms = @"~\Template\Antonyms.xlsx";
        public static string Synonyms = @"~\Template\Synonyms.xlsx";
        public static string TeacherList = @"~\Template\UserBulkUpload\TeacherList.xlsx";
        public static string QuestionUploadFile = @"~\Template\XMLQuestion\Question.xlsx";
        public static string StudentList = @"~\Template\UserBulkUpload\StudentList.xlsx";
        

        private static PraescioContext db = new PraescioContext();
        public static string UserAccountCode()
        {
            string RequestCode = "100000";
            if (db.Account.Any())
            {
                var maxCode = db.Account.Max(x => x.AccountId) + 1;
                RequestCode = RequestCode + (maxCode).ToString();
            }
            else
            {
                RequestCode = "1000001";
            }

            return RequestCode;
        }
    }
}
