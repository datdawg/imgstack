using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public List<Stack> Stacks { get; set; }
    }
}