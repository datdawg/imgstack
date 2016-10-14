using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class TagRelation
    {
        public int ID { get; set; }
        public int FK_TagID { get; set; }
        public int FK_StackID { get; set; }
        public int FK_ImageID { get; set; }
    }
}