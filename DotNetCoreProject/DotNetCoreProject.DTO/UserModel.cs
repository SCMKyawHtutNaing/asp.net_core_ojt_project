using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotNetCoreProject.DTO
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Name can't be blank.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email can't be blank.")]
        [StringLength(30, ErrorMessage = "30 characters is the maximum allowed.")]
        [EmailAddress(ErrorMessage = "Email format is invalid.")]
        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password can't be blank.")]
        [StringLength(20, ErrorMessage = "20 characters is the maximum allowed.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password can't be blank.")]
        [StringLength(20, ErrorMessage = "20 characters is the maximum allowed.")]
        [Compare("Password", ErrorMessage = "Password and Password Confirmation is not match.")]
        [Display(Name = "Password Confirmation")]
        public string ConfirmedPassword { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "Phone")]
        public string? Phone { get; set; }
        [Display(Name = "Date of Birth")]
        public string? DOB { get; set; }
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "Created User")]
        public string? CreatedUser { get; set; }
        [Display(Name = "Created Date")]
        public string? CreatedDate { get; set; }
        [Display(Name = "Updated User")]
        public string? UpdatedUser { get; set; }
        [Display(Name = "Updated Date")]
        public string? UpdatedDate { get; set; }
        public string? ProfileString { get; set; }
        public byte[]? ProfileBytes { get; set; }
        public List<UserViewModel>? users { get; set; }
    }

    public class ProfileViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Name can't be blank.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email can't be blank.")]
        [StringLength(30, ErrorMessage = "30 characters is the maximum allowed.")]
        [EmailAddress(ErrorMessage = "Email format is invalid.")]
        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
        [Display(Name = "Phone")]
        public string? Phone { get; set; }
        [Display(Name = "Date of Birth")]
        public string? DOB { get; set; }
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "Created User")]
        public string? CreatedUser { get; set; }
        [Display(Name = "Created Date")]
        public string? CreatedDate { get; set; }
        [Display(Name = "Updated User")]
        public string? UpdatedUser { get; set; }
        [Display(Name = "Updated Date")]
        public string? UpdatedDate { get; set; }
        public string? ProfileString { get; set; }
        public byte[]? ProfileBytes { get; set; }
    }
}
