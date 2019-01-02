using System.ComponentModel.DataAnnotations;
using System.Web;

namespace webapp.Models
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage ="Du måste fylla i en beskrivning")]
        [StringLength(100, ErrorMessage ="Beskrivningen måste innehålla minst 2 bokstäver", MinimumLength =2)]
        public string Description { get; set; }
        public HttpPostedFileBase ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        [Required(ErrorMessage = "Du måste fylla i en favoritkaka ")]
        [StringLength(100, ErrorMessage = "Favoritkakan måste innehålla minst 2 bokstäver", MinimumLength = 2)]
        public string Favoritkaka { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett namn")]
        [StringLength(100, ErrorMessage = "Namnet måste innehålla minst 2 bokstäver", MinimumLength = 2)]
        public string Namn { get; set; }

    }
}