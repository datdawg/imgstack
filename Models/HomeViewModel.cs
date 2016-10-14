using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class HomeViewModel
    {
        public List<Image> Images { get; set; }
        public List<User> Users { get; set; }
    }
}