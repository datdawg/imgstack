using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imgstack.Models
{
    public class ImageUploadViewModel
    {
        public HttpPostedFileBase upimg { get; set; }
        public string Name { get; set; }
        public IEnumerable<Stack> Stacks { get; set; }
        public int FK_Stack { get; set; }
        //public int FK_Creator { get; set; }
        //public string Filename { get; set; }
        //public string FileType { get; set; }
    }
}