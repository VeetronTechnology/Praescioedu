using System;
using System.ComponentModel.DataAnnotations;

namespace AOPM.Domain.Entities
{
    public class AuditLog
    {
        [Key]
        public long Id { get; set; }
        public string TableName { get; set; }
        public string UserId { get; set; }
        public string Actions { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public long? TableIdValue { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}