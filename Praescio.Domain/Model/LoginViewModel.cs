using Praescio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Praescio.Domain.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Userid is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }

    public class AccountViewModel
    {
        public Mst_Account Account { get; set; }
        public bool? hasError { get; set; }
        public string errorMessage { get; set; }
        public Mst_Account ParentAccount { get; set; }
        public Mst_Account StudentAccount { get; set; }

    }

    public class ChangePasswordViewModel
    {
        public int AccountId { get; set; }
        public string Username { get; set; }

        [Required(ErrorMessage = "Current PAssword is required.")]
        public string PasswordCurrent { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string PasswordConfirm { get; set; }
    }
}
