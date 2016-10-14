using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class GalleryUser
    {
        public int ID { get; set; }
        public int FK_Stack { get; set; }
        public int FK_User { get; set; }
    }
}