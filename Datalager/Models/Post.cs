using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Datalager.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        public virtual Profile Avsändare { get; set; }
        public virtual Profile Mottagare { get; set; }

    }
}
