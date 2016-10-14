using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "No spaces allowed.")]
        [StringLength(15, ErrorMessage = "Alias cannot be longer than 15 characters.")]
        public string Alias { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public HttpPostedFileBase upimg { get; set; }
    }
}