using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.Domain.Entities
{
    public class TeacherStandardMapping
    {
        public int Id { get; set; }
        [ForeignKey("UserAccount")]
        public int? TeacherId { get; set; }
        [ForeignKey("Standard")]
        public int? StandardId { get; set; }
        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Mst_Account UserAccount { get; set; }
    }
}
