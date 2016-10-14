using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imgstack.Models
{
    public class StackViewModel
    {
        public Stack Stack { get; set; }
        public User User { get; set; }
        public virtual List<Comment> Comments { get; set; }
        [AllowHtml]
        public string NewComment { get; set; }
    }
}