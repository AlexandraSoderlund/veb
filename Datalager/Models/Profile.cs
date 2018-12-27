using System.ComponentModel.DataAnnotations;

namespace Datalager.Models
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