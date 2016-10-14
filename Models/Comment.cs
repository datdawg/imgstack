using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace imgstack.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int FK_Stack { get; set; }
        public int? FK_Image { get; set; }
        public int FK_User { get; set; }
        [ForeignKey("FK_Stack")]
        public virtual Stack Stack { get; set; }
        [ForeignKey("FK_User")]
        public virtual User User { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }
    }
}