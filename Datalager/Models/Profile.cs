using System.ComponentModel.DataAnnotations;

namespace Datalager.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        public string ProfileImageUrl { get; set; }
        public string Description { get; set; }
        public string Favoritkaka { get; set; }
        public string UserId { get; set; }


    }
}