using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int FK_Creator { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }
    }
}