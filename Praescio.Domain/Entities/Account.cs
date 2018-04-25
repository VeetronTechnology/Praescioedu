using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.Domain.Entities
{
    public class PasswordResetAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Account")]
        public int? UserAccountId { get; set; }
        public bool? isEmailConfirmed { get; set; }
        public DateTime? CreatedOn { get; set; }
        public virtual Mst_Account Account { get; set; }
    }
}
