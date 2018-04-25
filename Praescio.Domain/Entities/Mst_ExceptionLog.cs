using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praescio.Domain.Entities
{
    public class Mst_ExceptionLog
    {
        [Key]
        public int ID { get; set; }
        public string LoggerName { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string IPAddress { get; set; }
        public string URL { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
