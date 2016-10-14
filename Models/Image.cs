using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
        public int FK_Stack { get; set; }
        public int FK_Creator { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("FK_Stack")]
        public virtual Stack Stack { get; set; }
        [ForeignKey("FK_Creator")]
        public virtual User User { get; set; }
    }
}