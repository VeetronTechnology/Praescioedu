using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.Domain.Entities
{
    public class Mst_Package
    {
        [Key]
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int? PackageAmount { get; set; }
        public string PackageData { get; set; }
        [ForeignKey("PackageRole")]
        public int? PackageRoleId { get; set; }
        public int? InstitutionAccountId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string IntervalType { get; set; }
        public int Interval { get; set; }
        public int BoardId { get; set; }
        public int MediumId { get; set; }
        public int StandardId { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_AccountType PackageRole { get; set; }
    }

    public class Mst_PackageHistory
    {
        [Key]
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string PackageData { get; set; }
        [ForeignKey("PackageRole")]
        public int? PackageRoleId { get; set; }
        public int? InstitutionAccountId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_AccountType PackageRole { get; set; }
    }

    public class Mst_Board
    {
        [Key]
        public int Id { get; set; }
        public string BoardName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }

    public class Mst_PrincipalDetail
    {
        [Key]
        public int Id { get; set; }
        public int? InstitutionAccountId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string PersonPhotoSrc { get; set; }
        public string AuthorityLetterSrc { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_InstitutionAccount InstitutionAccount { get; set; }
    }

    public class Mst_InstitutionAccount
    {
        [Key]
        public int InstitutionAccountId { get; set; }
        [Required(ErrorMessage = "Domain key is required")]
        public string DomainKey { get; set; }
        public string RegistrationNo { get; set; }
        public string InstitutionName { get; set; }
        public int? PackageId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNo { get; set; }
        public string LandlineNo { get; set; }
        public string Email { get; set; }
        public string AddressProofSrc { get; set; }
        public string Address { get; set; }
        [ForeignKey("State")]
        public int? StateId { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        [ForeignKey("Board")]
        public int? BoardId { get; set; }
        [ForeignKey("Medium")]
        public int? MediumId { get; set; }
        public string Description { get; set; }
        public DateTime? ActivateOn { get; set; }
        public DateTime? ExpiredOn { get; set; }
        public int? AmountPaid { get; set; }
        public int? NoOfStudent { get; set; }
        public int? NoOfTeacher { get; set; }
        public string NativeLanguage { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Medium Medium { get; set; }
        public virtual Mst_Board Board { get; set; }
        public virtual Mst_State State { get; set; }
        public virtual Mst_Package Package { get; set; }
    }

    public class Mst_AccountType
    {
        [Key]
        public int AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
    }

    public class Mst_Account
    {
        [Key]
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? AccountTypeId { get; set; }
        public int? InstitutionAccountId { get; set; }
        public string InstitutionName { get; set; }
        public string VersionType { get; set; }
        public bool? IsIndividual { get; set; }
        public bool? StudentRegisterAllowed { get; set; }
        public DateTime? ActivateOn { get; set; }
        public DateTime? ExpiredOn { get; set; }
        [ForeignKey("Board")]
        public int? BoardId{ get; set; }
        [ForeignKey("Medium")]
        public int? MediumId { get; set; }
        public string InstitutionAddress { get; set; }

        [ForeignKey("Standard")]
        public int? StudentStandardId { get; set; }
        public int? PackageId { get; set; }
        public int AmountPaid { get; set; }

        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string ParentEmail { get; set; }
        public string ParentNo { get; set; }

        public string URL { get; set; }
        public string Address { get; set; }
        [ForeignKey("State")]
        public int? StateId { get; set; }
        public string City { get; set; }
        public int? PinCode { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ParentMobileNo { get; set; }
        
        public string FacebookID { get; set; }
        public DateTime? LastLoginOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        private bool _isConfirmed = true;
        public bool IsConfirmed
        {
            get { return _isConfirmed; }
            set { _isConfirmed = value; }
        }
        public string ContactName { get; set; }
        public string ConfirmationCode { get; set; }
        public DateTime? ComfirmationCodeExpiration { get; set; }
        public DateTime? UpdatePwdDateTime { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }

        public virtual Mst_AccountType AccountType { get; set; }
        public virtual Mst_InstitutionAccount OrganizationAccount { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual Mst_State State { get; set; }
        public virtual Mst_Board Board { get; set; }
        public virtual Medium Medium { get; set; }

        [NotMapped]
        public string StandardSubject { get; set; }

        [NotMapped]
        public List<TeacherStandardMapping> TeacherStandardMapping { get; set; }
    }

    public class Mst_StudentParentAccount
    {
        [Key]
        public int ParentId { get; set; }
        public int? AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? AccountTypeId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ParentMobileNo { get; set; }
        public DateTime? LastLoginOn { get; set; }
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

        public virtual Mst_Account UserAccount { get; set; }
        public virtual Mst_AccountType AccountType { get; set; }

        [NotMapped]
        public string StandardSubject { get; set; }

    }

    public class Mst_State
    {
        [Key]
        public int Id { get; set; }
        public string StateName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }

    [Table("cities")]
    public class Mst_City
    {
        [Key]
        public int city_id { get; set; }
        public string city_name { get; set; }
        public int state_id { get; set; }
    }

    public class Mst_CategoryType
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [ForeignKey("CreatedAccount")]
        public int? CreatedBy { get; set; }
        [ForeignKey("ModifiedAccount")]
        public int? ModifiedBy { get; set; }

        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_Account CreatedAccount { get; set; }
        public virtual Mst_Account ModifiedAccount { get; set; }
    }

    public class Mst_Activities
    {
        [Key]
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public string Query { get; set; }
        public string ContactName { get; set; }
        public string URL { get; set; }
        public DateTime? CreatedOn { get; set; }
        [ForeignKey("CreatedAccount")]
        public int? CreatedBy { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        private bool _isactive = true;
        public bool IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual Mst_Account CreatedAccount { get; set; }
    }
    
    public class PaymentTransaction
    {
        [Key]
        public Int64 PaymentTransactionId { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        [ForeignKey("InstitutionAccount")]
        public int? InstitutionAccountId { get; set; }
        [ForeignKey("Package")]
        public int? PackageId { get; set; }
        public string PaymentRequestId { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public string MetaData { get; set; }
        public DateTime? CreatedOn { get; set; }
        [ForeignKey("CreatedAccount")]
        public int? CreatedBy { get; set; }
        public virtual Mst_Account CreatedAccount { get; set; }
        public virtual Mst_Account Account { get; set; }
        public virtual Mst_InstitutionAccount InstitutionAccount { get; set; }
        public virtual Mst_Package Package { get; set; }
    }
}
