using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Praescio.Domain.Models
{
    public class InstitutionViewModel
    {
        public Mst_InstitutionAccount Institution { get; set; }
        public Mst_PrincipalDetail PrincipalDetail { get; set; }
        public int RegisteredStudent { get; set; }
        public int RegisteredTeacher { get; set; }
        public int PendingStudent { get; set; }
        public int PendingTeacher { get; set; }
        
    }

    public class AccountDetailViewModel
    {
        public Mst_Account account { get; set; }
        public List<TeacherMappingStandard> teacherMappingStandard { get; set; }
        public List<int> subject { get; set; }
        public List<int> standard { get; set; }
        public bool? isTeacher { get; set; }
        public int? studentstandard { get; set; }
        public List<int> studentsubject { get; set; }
        public int? accounttypeid { get; set; }
    }

    public class TeacherMappingStandard
    {
        public int StandardId { get; set; }
        public List<int> SubjectId { get; set; }
    }

    public class UserList
    {
        public List<Mst_Account> AccountDetail { get; set; }
        public int? TotalRecord { get; set; }
    }

    public class User
    {
        public Mst_Account UserDetail { get; set; }
        public List<TeacherStandard> TeacherStandard { get; set; }
    }

    public class TeacherStandard
    {
        public string StandardName { get; set; }
        public List<string> SubjectName { get; set; }
    }

    public class BulkUploadUser
    {
        public List<BulkUserDetail> Account { get; set; }
        public int? institutionId { get; set; }
    }

    public class BulkUserDetail
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string StudentMobileNo { get; set; }
        public string Version { get; set; }
        public string Board { get; set; }
        public string StandardSubject { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string ParentEmailId { get; set; }
        public string ParentMobileNo { get; set; }
        public DateTime? ActivateOn { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int? PinCode { get; set; }
    }

}