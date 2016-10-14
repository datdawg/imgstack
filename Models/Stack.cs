using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class Stack
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Public { get; set; }
        public int FK_Creator { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}