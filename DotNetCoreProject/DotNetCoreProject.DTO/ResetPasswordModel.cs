using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreProject.DTO
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password can't be blank.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirmation can't be blank.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Password Confirmation are not match.")]
        [Display(Name = "Password Confirmation")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Current Password can't be blank.")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password can't be blank.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "New Confirm Password can't be blank.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "New Password and New Confirmation Password is not match.")]
        [Display(Name = "New Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string? Id { get; set; }
    }
}
