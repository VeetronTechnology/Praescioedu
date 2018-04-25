using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AOPM.Domain.Entities
{
    public class M_User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(500)]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(500)]
        public string Password { get; set; }

        public string ProfileType { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Department { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(1500)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [StringLength(500)]
        public string Mobile { get; set; }

        [StringLength(250)]
        public string LandlineNo { get; set; }

        [StringLength(400)]
        public string Address { get; set; }

        public string ProfilePic { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
       

    }
}