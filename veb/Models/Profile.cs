using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace veb.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        //public virtual ApplicationUser User { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Description { get; set; }


    }
}