using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AOPM.Domain.Entities
{
    public class T_Request
    {
        [Key]
        public int RequestID { get; set; }
        public string RequestCode { get; set; }

        [Required(ErrorMessage = "RequirementTitle is required.")]
        [DisplayName("Requirement Title")]
        public string RequirementTitle { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [DisplayName("Description Title")]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequirementType is required.")]
        [DisplayName("Requirement Type")]
        public int? RequirementTypeID { get; set; }
        [Required(ErrorMessage = "Demand Type is required.")]
        [DisplayName("Demand Type")]
        public int? DemandID { get; set; }
        public int? PriorityId { get; set; }
        public int? WorkingDay { get; set; }
        public int? RebaseCount { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [DisplayName("Status Title")]
        public int? StatusId { get; set; }
        public int? CostId { get; set; }
        public double? Amount { get; set; }
        public int? CurrencyID { get; set; }
        public string Department { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RequestTypeID { get; set; }
        public int? ExecutionTeamId{ get; set; }
        public string ImplementerUserID { get; set; }
        public string ImplementerDepartment { get; set; }
        private bool? _isactive = true;
        public bool? isActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual M_Currency Currency { get; set; }
        public virtual M_RequirementType RequirementType { get; set; }
        public virtual M_DemandType DemandType { get; set; }
        public virtual M_Priority Priority { get; set; }

        public virtual M_Status Status { get; set; }
        public virtual M_RequestType RequestType { get; set; }
        public virtual M_ExecutionTeam ExecutionTeam { get; set; }

    }
    public class T_RequestHistory
    {
        [Key]
        public int RequestHistoryID { get; set; }
        public int RequestID { get; set; }
        public string RequestCode { get; set; }

        [DisplayName("Requirement Title")]
        public string RequirementTitle { get; set; }

        [DisplayName("Description Title")]
        public string Description { get; set; }

        [DisplayName("Requirement Type")]
        public int? RequirementTypeID { get; set; }

        [DisplayName("Demand Type")]
        public int? DemandID { get; set; }
        public int? PriorityId { get; set; }
        public int? WorkingDay { get; set; }
        public int? RebaseCount { get; set; }

        [DisplayName("Status Title")]
        public int? StatusId { get; set; }
        public int? CostId { get; set; }
        public double? Amount { get; set; }
        public int? CurrencyID { get; set; }
        public string Department { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RequestTypeID { get; set; }
        public int? ExecutionTeamId { get; set; }
        public string ImplementerDepartment { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

    public class T_RequestUserRemarks
    {
        [Key]
        public long UserRemarkID { get; set; }
        public int? RequestId { get; set; }
        public int? RequestUserMappingID { get; set; }
        public string Role { get; set; }
        public int? StatusId { get; set; }
        public string Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }

        public virtual RequestUserMapping RequestUserMapping { get; set; }
        public virtual M_Status Status { get; set; }
    }

    public class T_RequestUserRemarksHistory
    {
        [Key]
        public long UserRemarkHistoryID { get; set; }
        public long UserRemarkID { get; set; }
        public int? RequestId { get; set; }
        public int? RequestUserMappingID { get; set; }
        public string Role { get; set; }
        public int? StatusId { get; set; }
        public string Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? DeleteOn { get; set; }

    }

    public class M_RoleMaster
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string Name { get; set; }

    }

    public class RequestUserMapping
    {

        [Key]
        public int RequestUserMappingID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? RequestID { get; set; }
        public int? RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Remark { get; set; }
        public int status { get; set; }
        public string ModifiedBy { get; set; }

        private bool _isactive = true;
        public bool isActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public string ApprovalType { get; set; }
        public virtual T_Request Request { get; set; }
        public virtual M_RoleMaster RoleMaster { get; set; }
    }

    public class RequestUserMappingHistory
    {

        [Key]
        public int RequestUserMappingHistoryID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? RequestID { get; set; }
        public int? RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Remark { get; set; }
        public int status { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        private bool _isactive = true;
        public bool isActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public string ApprovalType { get; set; }

    }



    public class T_Uploads
    {
        [Key]
        public long UploadID { get; set; }
        public int RequestID { get; set; }
        [Required]
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Size { get; set; }
        [Required]
        public string FilePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public bool? Active { get; set; }
        public virtual T_Request RequestActivities { get; set; }
    }

    public class Scheduler_App_Email_Master
    {
        [Key]
        public int id { get; set; }
        public int? RoleId { get; set; }
        public string type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public virtual M_RoleMaster RoleMaster { get; set; }
    }

    public class T_Scheduler_App_Email_History
    {
        [Key]
        public int id { get; set; }
        public int? RoleId { get; set; }
        public string type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string updatedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual M_RoleMaster RoleMaster { get; set; }
    }
}