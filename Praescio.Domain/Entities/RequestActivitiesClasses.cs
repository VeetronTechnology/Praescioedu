using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AOPM.Domain.Entities
{
    public class T_RequestActivities
    {
        private bool _isactive = true;
        private decimal? _number = 0;

        [Key]
        public int RequestActivityId { get; set; }
        [Required(ErrorMessage = "Task name required.")]
        public string TaskName { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage="Task type required.")]
        public int? TaskTypeId { get; set; }
        public decimal PercentAllocation { get; set; }
        //[Required(ErrorMessage = "Start date is required.")]
        public DateTime? StartDate { get; set; }
        //[Required(ErrorMessage = "End date is required.")]
        public DateTime? EndDate { get; set; }
        public int BudgetedDays { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int ActualDays { get; set; }
        public int? ActivityStatusId { get; set; }
        public int? RequestID { get; set; }
        [ForeignKey("Sprint")]
        public int? SprintId { get; set; }
        public float? Sequence { get; set; }
        public int? ParentId { get; set; }
        public int? PhaseId { get; set; }
        public int? PriorityId { get; set; }
        public bool IsFinalStage { get; set; }
        
        [DefaultValue(true)]
        public bool isActive {
            get { return _isactive; }
            set { _isactive = value; }
        }
        

        [ForeignKey("RequestUserMapping")]
        public int? ApproverId { get; set; }
        [ForeignKey("Status")]
        public int? ApproverStatusId { get; set; }
        public string FinalRemark { get; set; }
        public string Remark { get; set; }
        [ForeignKey("TMStatus")]
        public int? TMStatusId { get; set; }
        public bool IsMandetory { get; set; }
        public decimal? ActualComplition
        {
            get { return _number; }
            set { _number = value; }
        }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool ContainsSubtask { get; set; }
        public virtual T_Request Request { get; set; }
        public virtual M_ActivityStatus ActivityStatus { get; set; }
        public virtual M_Status Status { get; set; }
        public virtual M_Priority Priority { get; set; }
        public virtual M_Phase Phase { get; set; }
        public virtual M_TaskType TaskType { get; set; }
        public T_RequestActivities ParentActivity { get; set; }
        public virtual RequestUserMapping RequestUserMapping { get; set; }
        public virtual M_Status TMStatus { get; set; }
        public virtual T_Sprint Sprint { get; set; }
        
    }
    public class T_RequestActivitiesHistory
    {
        [Key]
        public int RequestActivityHistoryId { get; set; }
        public int RequestActivityId { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int? TaskTypeId { get; set; }
        public decimal? PercentAllocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BudgetedDays { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int? ActualDays { get; set; }
        public int? ActivityStatusId { get; set; }
        public int? RequestID { get; set; }
        public float? Sequence { get; set; }
        public int? ParentId { get; set; }
        public int? PhaseId { get; set; }
        public int? PriorityId { get; set; }
        public bool IsFinalStage { get; set; }
        public int? ApproverId { get; set; }
        public int? ApproverStatusId { get; set; }
        public string FinalRemark { get; set; }
        public int? TMStatusId { get; set; }
        public bool IsMandetory { get; set; }
        public decimal? ActualComplition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool isActive { get; set; }
        public int? SprintId { get; set; }
        public string Remark { get; set; }
        public bool ContainsSubtask { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

    public class T_ActivityMembers
    {
        [Key]
        public long ActivityMemberId { get; set; }
        public int? RequestActivityId { get; set; }
        public int RequestUserMappingID { get; set; }
        public string UserRemark { get; set; }
        [ForeignKey("UserStatus")]
        public int? UserStatusId { get; set; }
        public string InitiatorRemark { get; set; }
        [ForeignKey("RoleMaster")]
        public int? ActivityRole { get; set; }

        [ForeignKey("InitiatorStatus")]
        public int? InitiatorStatusId { get; set; }
        [ForeignKey("Sprint")]
        public int? SprintId { get; set; }
        [ForeignKey("SprintStatus")]
        public int? SprintStatusId { get; set; }

        private bool _isactive = true;
        public bool isActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual T_RequestActivities RequestActivity { get; set; }
        public virtual RequestUserMapping RequestUserMapping { get; set; }
        public virtual M_MileStoneStatus UserStatus { get; set; }
        public virtual M_Status InitiatorStatus { get; set; }
        public virtual M_Status SprintStatus { get; set; }
        public virtual M_RoleMaster RoleMaster { get; set; }
        public virtual T_Sprint Sprint { get; set; }

    }
    public class T_ActivityUserRemarks
    {
        [Key]
        public long ActivityUserRemarkID { get; set; }
        public int? RequestActivityId { get; set; }
        public long? ActivityMemberId { get; set; }
        public int? RequestUserMappingID { get; set; }
        public int? StatusId { get; set; }
        public int? SprintId { get; set; }
        [ForeignKey("SprintStatus")]
        public int? SprintStatusId { get; set; }
        public string Remark { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Size { get; set; }
        public string FilePath { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string ActivityMemberRole { get; set; }
        
        public virtual T_RequestActivities RequestActivities { get; set; }
        public virtual T_ActivityMembers ActivityMembers { get; set; }
        public virtual M_MileStoneStatus Status { get; set; }
        public virtual M_Status SprintStatus { get; set; }
        public virtual T_Sprint Sprint { get; set; }
        public virtual RequestUserMapping RequestUserMapping { get; set; }
    }

    public class T_ActivityUserRemarksHistory
    {
        [Key]
        public long ActivityUserRemarkHistoryID { get; set; }
        public long ActivityUserRemarkID { get; set; }
        public int? RequestActivityId { get; set; }
        public long? ActivityMemberId { get; set; }
        public int? RequestUserMappingID { get; set; }
        public int? StatusId { get; set; }
        public int? SprintStatusId { get; set; }
        public int? SprintId { get; set; }
        public string Remark { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Size { get; set; }
        public string FilePath { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string ActivityMemberRole { get; set; }
        public DateTime? DeletedOn { get; set; }
        
    }
    public class T_ActivityUploads
    {
        [Key]
        public long ActivityUploadID { get; set; }
        //public int? RequestActivityId { get; set; }
        //public long? ActivityMemberId { get; set; }
        public long? ActivityUserRemarkID { get; set; }
        [Required]
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Size { get; set; }
        [Required]
        public string FilePath { get; set; }
        //public virtual T_RequestActivities RequestActivities { get; set; }
        //public virtual T_ActivityMembers ActivityMembers { get; set; }
        public virtual T_ActivityUserRemarks ActivityUserRemarks { get; set; }
    }
    public class T_TaskBaseLine { 
        [Key]
        public int BaseLineId { get; set; }
        public int RequestActivityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
    public class T_Sprint
    {
        private bool _isactive = true;
        [Key]
        public int Id { get; set; }
        public string SprintName { get; set; }
        public int? RequestID { get; set; }
        public int? RebaseCount { get; set; }
        public int? StatusId { get; set; }
        public string CreateBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool isActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        public virtual T_Request Request { get; set; }
        public virtual M_Status Status { get; set; }
    }

    public class T_SprintHistory
    {
        [Key]
        public int SprintHistoryId { get; set; }
        public int Id { get; set; }
        public string SprintName { get; set; }
        public int? RequestID { get; set; }
        public int? StatusId { get; set; }
        public string CreateBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool isActive { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
    //public class T_ActivityLog
    //{
    //    [Key]
    //    public long ActivityLogId { get; set; }
    //    public int? RequestActivityId { get; set; }
    //    public long? ActivityMemberId { get; set; }
    //    public int? StatusId { get; set; }
    //    public string Remark { get; set; }
    //    public DateTime? UpdatedDate { get; set; }

    //    public virtual T_RequestActivities RequestActivities { get; set; }
    //    public virtual T_ActivityMembers ActivityMembers { get; set; }
    //    public virtual M_Status Status { get; set; }
    //}
}