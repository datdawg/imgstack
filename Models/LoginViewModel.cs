using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Alias { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Keep me logged in")]
        public bool RememberLogin { get; set; }
    }
}