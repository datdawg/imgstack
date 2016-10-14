using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class StackCreateViewModel
    {
        [Required]
        [Display(Name = "Stack name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Public")]
        public bool Public { get; set; }
    }
}